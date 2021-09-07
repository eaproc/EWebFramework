using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;
using System.IO;
using EPRO.Library.Objects;
using EPRO.Library.Types;
using EWebFramework.Vendor.api.utils;
using EWebFramework.Vendor.response_handlers;
using EWebFramework.Vendor.api.utils.JsonReplies;

namespace EWebFramework.api.controllers.auth
{
    public class Logout
    {


        public static void Index(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            
            //SessionHelper sh = new SessionHelper(session, request);

            //var service = new services.Auth.Logout(sh);


            //// Store the Last Login Time
            //// 
            //T___UserLoginHistory userLoginHistory = T___UserLoginHistoryExtended.Fetch(SessionID: session.SessionID, UserID: sh.userId.Value);
            //if (userLoginHistory.hasRows()) T___UserLoginHistory.update(userLoginHistory.ID, pLoggedOutTime: ClientService.ServerNowDateTime);




            //service.CheckoutMyPendingPage();




            //new SessionService(sh).RemoveSession(session.SessionID);

            session.Clear();
            session.Abandon();

            response.OutputJSON(new SuccessResult("User logged out successfully"));

        }


    }
}