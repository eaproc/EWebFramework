using EPRO.Library;
using EPRO.Library.Objects;
using EWebFramework.Vendor.api.controllers;
using EWebFramework.Vendor.api.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor.response_handlers
{
    public static class BasicResponse
    {

        public const String STATIC_FILE_VERSIONING_SYMBOL = "@@CODE-FILE-VERSION@@";
        public const String JS_ENVIRONMENT_EXTENSION = "@@JS_ENVIRONMENT_EXTENSION@@";
        public const String JS_VERSIONING = JS_ENVIRONMENT_EXTENSION + "?v=" + STATIC_FILE_VERSIONING_SYMBOL;
        public const String JS = "@@JS@@";
        public const String JS_DEVELOPMENT_EXTENSION = ".js";
        public const String JS_PRODUCTION_EXTENSION = ".min.js";
        public const String CSS = "@@CSS@@";
        public const String CSS_DEVELOPMENT_EXTENSION = ".css";
        public const String CSS_PRODUCTION_EXTENSION = ".min.css";
        public const String CSS_ENV_EXT = "@@CSS_ENV_EXT@@";
        public const String CSS_VERSIONING = CSS_ENV_EXT + "?v=" + STATIC_FILE_VERSIONING_SYMBOL;

        public static void OutputPage( String pFileName, int ResponseCode = 200)
        {
            OutputPage(
                pFileName: pFileName, ResponseCode: ResponseCode, pInjectData: new Dictionary<string, object>().ToArray()
                );
        }




        public static string GetMasterFileContent()
        {
            return System.IO.File.ReadAllText(RootPath("views/layouts/master.html"));
        }



        /// <summary>
        /// 
        /// Page must have this
        /// //@@[JS-INJECTION]
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="pCompactData"></param>
        public static void OutputPage(String pFileName, int ResponseCode=200,
           params KeyValuePair<string, object>[] pInjectData )
        {

            IDictionary<string, object> pCompactData = pInjectData.ToDictionary(x => x.Key, x => x.Value);

            var pJSInject = "";
            if (pCompactData != null && pCompactData.Count > 0)
            {
                var p = (from r in pCompactData
                         select String.Format("var {0} = JSON.parse('{1}');", r.Key, new JavaScriptSerializer().Serialize(r.Value))
                        ).ToArray();
                pJSInject = String.Join(Environment.NewLine, p);
            }



            String pChildContent = System.IO.File.ReadAllText(RootPath(pFileName));
            pChildContent = pChildContent.Replace("//@@[PAGE-JS-INJECTION]", pJSInject);
            pChildContent = pChildContent.Replace(JS, JS_VERSIONING).Replace(CSS, CSS_VERSIONING).Replace(
                STATIC_FILE_VERSIONING_SYMBOL, AlphaNumericCodeGenerator.RandomString(10)
                ).Replace(
                   JS_ENVIRONMENT_EXTENSION, (api.utils.ENV.GetEnvironment() == api.utils.ENV.ENVIRONMENT.DEVELOPMENT) ? JS_DEVELOPMENT_EXTENSION : JS_PRODUCTION_EXTENSION
                    ).Replace(CSS, CSS_VERSIONING).Replace(
                        CSS_ENV_EXT, (api.utils.ENV.GetEnvironment() == api.utils.ENV.ENVIRONMENT.DEVELOPMENT) ? CSS_DEVELOPMENT_EXTENSION : CSS_PRODUCTION_EXTENSION
                    );


            OutputPageContent(pChildContent, ResponseCode);

        }


        public static void OutputPageContent(String pContents, int ResponseCode)
        {
            HttpResponse pResponse = HttpContext.Current.Response;
            pResponse.Clear();
            pResponse.StatusCode = ResponseCode;
            pResponse.Write(pContents);
        }




        public static void OutputJSON(this Page pPage, String json, int ResponseCode = 200)
        {
            var Response = pPage.Response;
            Response.Clear();
            Response.StatusCode = ResponseCode;
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(json);
           // Response.End();
        }

        public static void OutputJSON(this Page pPage, api.utils.IJsonable json, int ResponseCode = 200)
        {
            OutputJSON(pPage, json.ToJson(), ResponseCode);
        }


        public static void OutputJSON(this HttpResponse Response, String json, int ResponseCode = 200)
        {
            Response.Clear();
            Response.StatusCode = ResponseCode;
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(json);
        }


        public static void OutputJSON(this HttpResponse Response, api.utils.IJsonable json, int ResponseCode = 200)
        {
            OutputJSON(Response, json.ToJson(), ResponseCode: ResponseCode);
        }


        public static void OutputJSON(this HttpResponse Response, object serializableObj)
        {
            OutputJSON(Response, new JavaScriptSerializer().Serialize(serializableObj));
        }











        public static void OutputFileAsStream(this HttpResponse response, String pFilePath, String MimeType, bool isAttachment)
        {
            response.Clear();
            response.ContentType = MimeType;
            response.AppendHeader("Content-Disposition", String.Format(isAttachment? "attachment": "inline" + "; filename=\"{0}\"", EIO.getFileName(pFilePath)));

            using (MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(pFilePath)))
            {
                ms.WriteTo(response.OutputStream);
            }
        }


        public static void OutputFileAsStream(this HttpResponse response, String pFilePath)
        {
            String MimeType = MimeTypes.MimeTypeMap.GetMimeType(EIO.GetFileExtension(pFilePath));
            OutputFileAsStream(response: response, pFilePath: pFilePath, MimeType: MimeType, isAttachment: true);
        }

        
        public static void OutputFileAsStream(this HttpResponse response, String pFilePath,  bool isAttachment )
        {
            String MimeType = MimeTypes.MimeTypeMap.GetMimeType(EIO.GetFileExtension(pFilePath));
            OutputFileAsStream(response: response, pFilePath: pFilePath, MimeType: MimeType, isAttachment: isAttachment);
        }




        public static void OutputFileAsStream(this HttpResponse response, String pFilePath, String MimeType, String pCustomDownloadName, bool isAttachment)
        {
            response.Clear();
            response.ContentType = MimeType;

            if (pCustomDownloadName == null) pCustomDownloadName = EIO.getFileName(pFilePath);

            response.AppendHeader("Content-Disposition", String.Format("attachment; filename=\"{0}\"", pCustomDownloadName));


            using (MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(pFilePath)))
            {
                ms.WriteTo(response.OutputStream);
            }
        }
        

        public static void OutputFileAsStream(this HttpResponse response, String pFilePath, String MimeType, String pCustomDownloadName)
        {
            OutputFileAsStream(response: response, pFilePath: pFilePath, MimeType: MimeType, pCustomDownloadName: pCustomDownloadName, isAttachment: true);
        }


        private static readonly Dictionary<char, char> AndroidAllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ._-+,@£$€!½§~'=()[]{}0123456789".ToDictionary(c => c);

        private static string MakeAndroidSafeFileName(string fileName)
        {
            char[] newFileName = fileName.ToCharArray();
            for (int i = 0; i < newFileName.Length; i++)
            {
                if (!AndroidAllowedChars.ContainsKey(newFileName[i]))
                    newFileName[i] = '_';
            }
            return new string(newFileName);
        }

        public static void OutputFileAsStreamCustom(this HttpResponse response, String pFilePath, String pCustomDownloadName)
        {
            String MimeType = MimeTypes.MimeTypeMap.GetMimeType(EIO.GetFileExtension(pFilePath));
            OutputFileAsStream(response: response, pFilePath: pFilePath, MimeType: MimeType, pCustomDownloadName: pCustomDownloadName);
        }





        //public static void OutputFileContent(HttpResponse response, String pContent, String MimeType = "text/plain")
        //{
        //    response.Clear();
        //    response.ContentType = MimeType;
        //    response.Write(pContent);
        //}


    }
}