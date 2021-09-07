using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;
using System.IO;
using EPRO.Library.Objects;
using EWebFramework.Vendor.response_handlers;

namespace EWebFramework.controllers.auth
{
    public class Login
    {


        public static void Index(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {

            BasicResponse.OutputPage("views/auth/Login.html");

        }


    }
}