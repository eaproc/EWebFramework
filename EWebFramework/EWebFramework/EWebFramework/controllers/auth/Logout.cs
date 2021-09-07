using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;
using System.IO;
using EPRO.Library.Objects;

namespace EWebFramework.controllers.auth
{
    public class Logout
    {


        public static void Index(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {

            //session.Clear();
            //session.Abandon();

            //// If you need to completely change the session id
            //// Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));


            //var sh = new SessionHelper(session, request);
            //var service = new api.services.SessionService(sh);
            //service.RemoveSession(session.SessionID);
            api.controllers.auth.Logout.Index(request, response, session);
            response.Clear();

            //redirect
            response.Redirect("?p=/auth/login", true);

        }


    }
}