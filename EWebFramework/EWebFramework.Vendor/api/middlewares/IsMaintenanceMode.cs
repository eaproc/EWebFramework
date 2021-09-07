using EWebFramework.Vendor.api.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EWebFramework.Vendor.api.middlewares
{
    public class IsMaintenanceMode :api.middlewares.IMiddlewareCheckConstraint
    {


        public bool Check(HttpRequest request, HttpResponse response, HttpSessionState session)
        {
            if (!SilentCheck(request, response, session))
            {
                //redirect
                if(request.WantJSON())
                    throw new Exception("Application is not in Maintenance MODE!");
                else
                    response.Redirect("?p=/home", true);
            }

            return true;

        }

        public bool SilentCheck(HttpRequest request, HttpResponse response, HttpSessionState session)
        {
            return new MaintenanceMode().IsInMaintenanceMode();
        }


    }
}