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

namespace EWebFramework.Vendor.controllers.system
{
    public class MaintenanceMode
    {


        public static void Index(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            //ASP.NET doesn't display other status as HTML on LIVE
            //503
            BasicResponse.OutputPage(pFileName: "views/system/MaintenanceMode.html",
                ResponseCode: 200
                );

        }


    }
}