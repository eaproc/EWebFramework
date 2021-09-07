using EWebFramework.Vendor.api.middlewares;
using EWebFramework.Vendor.api.utils;
using static EWebFramework.Vendor.PageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EWebFramework.api.middlewares
{
    public class InsertOrUpdateSessionRequest : IMiddlewareCheckConstraint
    {

        public bool Check(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            var service = new api.services.SessionService(new SessionHelper(session, request));
            return service.InsertOrReplace();
        }

        public bool SilentCheck(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session)
        {
            try
            {
                return Check(request, response,session);
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return false;
            }
        }
    }
}