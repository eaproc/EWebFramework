using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;
using System.IO;
using EPRO.Library.Objects;
using EWebFramework.Vendor.api.utils;
using EWebFramework.Vendor.response_handlers;
using EWebFramework.Vendor.api.utils.JsonReplies;

namespace EWebFramework.api.controllers.auth
{
    public class Login
    {


        public static void Index(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {

            RequestHelper requestHelper = new RequestHelper(request);
            var v = new RequestValidator();
            if (v.Validate(requestHelper, response, session,
                new RequestValidator.Rule("username", true, 2, 30),
                new RequestValidator.Rule("user_password", true, 1, 30)
                    )
                )
            {


                //var sh = new SessionHelper(session, request);
                //var service = new EWebFramework.api.services.Auth.Login(sh);

                //String username = EStrings.valueOf(requestHelper.Get("username"));
                //String user_password = EStrings.valueOf(requestHelper.Get("user_password"));


                //try
                //{
                //    T___Users loggedIn = service.AttemptLogin(Username: username, Password: user_password);

                //    //here credentials are accepted
                //    SessionHelper.StoreUserID((int)loggedIn.ID);

                //    var auth = SessionSelector.LoggedInUser;
                //    response.OutputJSON(new SuccessResult(pMessage: "Logged In Successful.", pData:
                //        new
                //        {
                //            id = auth.ID,
                //            user_name = auth.Username,
                //            first_name = auth.FirstName,
                //            last_name = auth.LastName
                //        }
                //        ));


                //    //Login in History
                //    service.KeepLoginHistory();
                //    service.TimeOutPendingWebView();



                //}
                //catch (Exception ex)
                //{
                //    response.OutputJSON(new FailedResult(
                //                pData: new { errors = ex.Message })
                //        );
                //}

            }
            else
            {
                response.OutputJSON(new FailedResult(pData: v.errors));
            }

        }




    }
}