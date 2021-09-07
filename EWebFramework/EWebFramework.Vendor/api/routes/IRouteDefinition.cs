using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.routes
{
    public abstract class IRouteDefinition
    {

        public middlewares.IMiddlewareCheckConstraint[] middleware;

        protected HttpRequest request;
        protected HttpResponse response;
        protected System.Web.SessionState.HttpSessionState session;
        public delegate void CallableRoute(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session);

        public IRouteDefinition(middlewares.IMiddlewareCheckConstraint[] middlewares)
        {
            this.response = HttpContext.Current.Response;
            this.request = HttpContext.Current.Request;
            this.session = HttpContext.Current.Session;
            this.middleware = middlewares;
        }



        public virtual void Execute(string phttpVerb, string pPath)
        {
            if (this.middleware != null && this.middleware.Count() > 0)
            {
                //Check middleware
                var p = (from l in this.middleware
                         where !l.Check(request, response, session)
                         select l).ToList();
                if (p.Count > 0) return;
            }

        }


        public abstract bool Matches(string phttpVerb, string pPath);



    }



}