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

using static EWebFramework.Vendor.response_handlers.VarDump;
using static EWebFramework.Vendor.PageHandlers;
using static EWebFramework.Vendor.response_handlers.BasicResponse;

namespace EWebFramework.response_handlers
{
    public static class CustomResponse
    {


        public static void OutputMasterPageInherited(String pFileName, int ResponseCode=200)
        {
            OutputMasterPageInherited(pFileName, pCompactData: new Dictionary<String, Object>());
        }

            public static void OutputMasterPageInherited(String pFileName, 
            IDictionary<string,object> pCompactData, int ResponseCode = 200)
        {

            //// 
            //// Inject More Data
            ////
            //pCompactData.Add("user", EWebFramework.api.selectors.SessionSelector.LoggedInUser);
            //pCompactData.Add("selected_role", EWebFramework.api.selectors.SessionSelector.Role);
            //pCompactData.Add("links_permitted", EWebFramework.api.selectors.SessionSelector.Links);

            //api.services.Auth.Notifications notificationsService = new api.services.Auth.Notifications(new SessionHelper());
            //pCompactData.Add("top_notifications", notificationsService.FetchTop5Notifications().ToDictionary());
            //pCompactData.Add("unread_notifications_count", notificationsService.FetchUnreaNotificationCount());






            //var pJSInject = "";
            //if(pCompactData!=null && pCompactData.Count > 0 )
            //{
            //    var p = (from r in pCompactData
            //             select String.Format("var {0} = JSON.parse('{1}');", r.Key, new JavaScriptSerializer().Serialize(r.Value))
            //            ).ToArray();
            //    pJSInject = String.Join(Environment.NewLine, p);
            //}



            //String pMasterContent = GetMasterFileContent().Replace("//@@[PAGE-JS-INJECTION]", pJSInject).Replace(
            //    "<!--@@SIDE-MENU-->", api.selectors.SessionSelector.MenuLinks
            //    );
            //String pChildContent = System.IO.File.ReadAllText(RootPath(pFileName));


            //Int32 pChildContentSplit = pChildContent.IndexOf("<!--@@yield_to_master-->");
            //String pChildContentExtract = pChildContent.Substring(0, pChildContentSplit );
            //String pChildContentRemains = pChildContent.Substring(pChildContentSplit);

            //pMasterContent = pMasterContent.Replace("<!--@@yield_to_master-->", pChildContentRemains);



            //OutputPageContent(pMasterContent.Replace(
            //    "@@BodyContent", pChildContentExtract).Replace(JS, JS_VERSIONING).Replace(CSS, CSS_VERSIONING).Replace(
            //    STATIC_FILE_VERSIONING_SYMBOL, AlphaNumericCodeGenerator.RandomString(18)
            //    ).Replace(
            //       JS_ENVIRONMENT_EXTENSION, (api.utils.ENV.GetEnvironment() == api.utils.ENV.ENVIRONMENT.DEVELOPMENT) ? JS_DEVELOPMENT_EXTENSION : JS_PRODUCTION_EXTENSION
            //        ).Replace(CSS, CSS_VERSIONING).Replace(
            //            CSS_ENV_EXT, (api.utils.ENV.GetEnvironment() == api.utils.ENV.ENVIRONMENT.DEVELOPMENT) ? CSS_DEVELOPMENT_EXTENSION : CSS_PRODUCTION_EXTENSION
            //        ),
            //        ResponseCode: ResponseCode
            //        );
        }



    }
}