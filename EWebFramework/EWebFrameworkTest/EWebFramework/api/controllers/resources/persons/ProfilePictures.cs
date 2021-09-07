using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;
using System.IO;
using EPRO.Library.Objects;
using EPRO.Library;
using EWebFramework.Vendor.api.utils;
using EWebFramework.Vendor.response_handlers;
using EWebFramework.Vendor.api.utils.JsonReplies;

namespace EWebFramework.api.controllers.resources.persons
{
    public class ProfilePictures
    {
        private static services.resources.persons.ProfilePictures service;

        static ProfilePictures()
        {
            SessionHelper sh = new SessionHelper();
            service = new services.resources.persons.ProfilePictures(sh);

        }

        /// <summary>
        /// Fetch Small Size
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="session"></param>
        public static void FetchXS(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {

            RequestHelper requestHelper = new RequestHelper(request);
            var v = new RequestValidator();
            if (
                    v.Validate(requestHelper, response, session,
                        new RequestValidator.Rule("ID", pIsRequired: true, pParamMinSize: 0, pParamMaxSize: int.MaxValue, pParamType: RequestValidator.Rule.ParamTypes.INTEGER, pIsNullable: false),
                       new RequestValidator.Rule("FileName", pIsRequired: false, pParamMinSize: 0, pParamMaxSize: 100, pParamType: RequestValidator.Rule.ParamTypes.STRING, pIsNullable: true)
                    )
                )
            {

                int ID = EInt.valueOf(requestHelper.Get("ID"));
                String FileName = requestHelper.ContainsKey("FileName") ? EStrings.valueOf(requestHelper.Get("FileName")) : null;

                try
                {

                    String result = service.FetchXS(FileName: FileName, ID: ID);

                    if (File.Exists(result) && !result.EndsWith(service.Default__RelativeFilePath_ProfilePic_XS())) 
                    {
                        // original file
                        response.OutputFileAsStream(result);
                    }
                    else if(File.Exists(service.Fetch(FileName, ID)) && ID != 0 )
                    {
                        // original file still exists
                        //  else throw new exceptions.FileNotFoundOnServerException(FileName: EIO.getFileName(result));
                        // try bigger version till it is resolved
                        FetchSM(request, response, session);
                        return;
                    }
                    else if (File.Exists(result))
                    {
                        // Default File
                        response.OutputFileAsStream(result);
                    }
                    else throw new Vendor.exceptions.FileNotFoundOnServerException(FileName: EIO.getFileName(result));


                }
                catch (Exception ex)
                {
                    response.OutputJSON(new FailedResult(pData: new { message = ex.Message }));
                }

            }
            else
            {
                response.OutputJSON(new FailedResult(pData: v.OutputErrors()));
            }




        }


        /// <summary>
        /// Fetch Xtra Small Size
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="session"></param>
        public static void FetchSM(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {

            RequestHelper requestHelper = new RequestHelper(request);
            var v = new RequestValidator();
            if (
                    v.Validate(requestHelper, response, session,
                        new RequestValidator.Rule("ID", pIsRequired: true, pParamMinSize: 0, pParamMaxSize: int.MaxValue, pParamType: RequestValidator.Rule.ParamTypes.INTEGER, pIsNullable: false),
                       new RequestValidator.Rule("FileName", pIsRequired: false, pParamMinSize: 0, pParamMaxSize: 100, pParamType: RequestValidator.Rule.ParamTypes.STRING, pIsNullable: true)
                    )
                )
            {

                int ID = EInt.valueOf(requestHelper.Get("ID"));
                String FileName = requestHelper.ContainsKey("FileName") ? EStrings.valueOf(requestHelper.Get("FileName")) : null;
                try
                {


                    String result = service.FetchSM(FileName: FileName, ID: ID);

                    if (File.Exists(result) && !result.EndsWith(service.Default__RelativeFilePath_ProfilePic_SM()))
                    {
                        // original file
                        response.OutputFileAsStream(result);
                    }
                    else if (File.Exists(service.Fetch(FileName, ID)) && ID != 0)
                    {
                        // original file still exists
                        //  else throw new exceptions.FileNotFoundOnServerException(FileName: EIO.getFileName(result));
                        // try bigger version till it is resolved
                        Fetch(request, response, session);
                        return;
                    }
                    else if (File.Exists(result))
                    {
                        // Default File
                        response.OutputFileAsStream(result);
                    }
                    else throw new Vendor.exceptions.FileNotFoundOnServerException(FileName: EIO.getFileName(result));

                
                }
                catch (Exception ex)
                {
                    response.OutputJSON(new FailedResult(pData: new { message = ex.Message }));
                }

            }
            else
            {
                response.OutputJSON(new FailedResult(pData: v.OutputErrors()));
            }




        }



        /// <summary>
        /// Fetch Xtra Small Size
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="session"></param>
        public static void Fetch(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {

            RequestHelper requestHelper = new RequestHelper(request);
            var v = new RequestValidator();
            if (
                    v.Validate(requestHelper, response, session,
                        new RequestValidator.Rule("ID", pIsRequired: true, pParamMinSize: 0, pParamMaxSize: int.MaxValue, pParamType: RequestValidator.Rule.ParamTypes.INTEGER, pIsNullable: false),
                       new RequestValidator.Rule("FileName", pIsRequired: false, pParamMinSize: 0, pParamMaxSize: 100, pParamType: RequestValidator.Rule.ParamTypes.STRING, pIsNullable: true)
                    )
                )
            {

                int ID = EInt.valueOf(requestHelper.Get("ID"));
                String FileName = requestHelper.ContainsKey("FileName") ? EStrings.valueOf(requestHelper.Get("FileName")) : null;

                try
                {

                 

                    String result = service.Fetch(FileName: FileName, ID: ID);

                    // At this point Person is created
                    if (File.Exists(result))
                    {
                        response.OutputFileAsStream(result);
                    }
                    else throw new Vendor.exceptions.FileNotFoundOnServerException(FileName: EIO.getFileName(result));
                }
                catch (Exception ex)
                {
                    response.OutputJSON(new FailedResult(pData: new { message = ex.Message }));
                }

            }
            else
            {
                response.OutputJSON(new FailedResult(pData: v.OutputErrors()));
            }




        }









    }
}