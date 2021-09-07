using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EWebFramework.Vendor.api.middlewares
{
   public interface IMiddlewareCheckConstraint
    {
        /// <summary>
        /// Throws exception on failure
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        bool Check(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session);

        /// <summary>
        /// No exception, returns false in such cases
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        bool SilentCheck(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session);

    }
}
