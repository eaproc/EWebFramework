using EWebFramework.Vendor.api.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.controllers
{
    public class BaseController
    {

        protected HttpRequest request;
        protected HttpResponse response;
        protected System.Web.SessionState.HttpSessionState session;
        protected RequestHelper requestHelper;

        public BaseController()
        {
            this.response = HttpContext.Current.Response;
            this.request = HttpContext.Current.Request;
            this.session = HttpContext.Current.Session;
            this.requestHelper = new RequestHelper(request);
        }



    }
}