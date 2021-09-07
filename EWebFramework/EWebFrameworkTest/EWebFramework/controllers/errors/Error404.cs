using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EWebFramework.response_handlers;
using EWebFramework.api.services;
using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;
using System.IO;
using EPRO.Library.Objects;
using EWebFramework.Vendor.response_handlers;

namespace EWebFramework.controllers.errors
{
    public class Error404
    {


        public static void Index(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {

            BasicResponse.OutputPage("views/errors/Error404.html");

        }


    }
}