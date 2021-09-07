using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;
using System.IO;
using EPRO.Library.Objects;
using EWebFramework.Vendor.api.utils.JsonReplies;
using EWebFramework.Vendor.response_handlers;

namespace EWebFramework.Vendor.api.controllers.system
{
    public class MaintenanceMode
    {


        public static void Index(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {

            response.OutputJSON(new SuccessResult(pData: new
            {
                message = "Application is in Maintenance Mode"
            }), ResponseCode: 503);

        }


    }
}