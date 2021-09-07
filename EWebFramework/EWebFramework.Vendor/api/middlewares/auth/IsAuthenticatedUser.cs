using EWebFramework.Vendor.api.utils;
using EWebFramework.Vendor.exceptions.auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EWebFramework.Vendor.api.middlewares.auth
{
    public class IsAuthenticatedUser : IMiddlewareCheckConstraint
    {


        public virtual bool Check(HttpRequest request, HttpResponse response, HttpSessionState session)
        {
           
                SessionHelper s = new SessionHelper(session, request);
                if (s.userId == null ) throw new UnAuthorizedURLAccess();
            return true;
        }

        public bool SilentCheck(HttpRequest request, HttpResponse response, HttpSessionState session)
        {
            try
            {
                SessionHelper s = new SessionHelper(session, request);
                if (s.userId == null ) throw new UnAuthorizedURLAccess();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}