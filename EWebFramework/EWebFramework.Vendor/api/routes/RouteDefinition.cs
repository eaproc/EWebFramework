using System.Linq;
using System.Web;
using static EWebFramework.Vendor.api.routes.IRouteDefinition;
using static EWebFramework.Vendor.PageHandlers;


namespace EWebFramework.Vendor.api.routes
{
    public class RouteDefinition 
    {
        public string path;
        public string[] httpVerb;
        public CallableRoute closure;
        public middlewares.IMiddlewareCheckConstraint[] middleware;
        protected RouteDefinition[] routingDefinitions;


        public RouteDefinition(params RouteDefinition[] pRoutingDefinitions
           )
        {
            this.routingDefinitions = pRoutingDefinitions;
        }


        public RouteDefinition(middlewares.IMiddlewareCheckConstraint[] pMiddlewares,
                            params RouteDefinition[] pRoutingDefinitions
            )
        {
            this.routingDefinitions = pRoutingDefinitions;
            this.middleware = pMiddlewares;
        }

        public RouteDefinition(string pPath, string[] pHttpVerb, CallableRoute pClosure, params middlewares.IMiddlewareCheckConstraint[] pMiddlewares)
        {
            this.closure = pClosure;
            this.path = pPath;
            this.httpVerb = pHttpVerb;
            this.middleware = pMiddlewares;
        }



        /// <summary>
        /// Checks through path to be sure there is a match
        /// </summary>
        /// <param name="phttpVerb"></param>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public bool Matches(string phttpVerb, string pPath)
        {
            // return this.httpVerb.Contains(phttpVerb) && this.path == pPath;

            if (!this.IsRouteGroup()) return this.httpVerb.Contains(phttpVerb) && this.path == pPath;
            if (this.routingDefinitions == null) return false;

            var p = from r in routingDefinitions
                    where r.Matches(phttpVerb: phttpVerb, pPath: pPath)
                    select r;

            return p.Count() > 0;


        }



        /// <summary>
        /// Executes Middlewares along the way as it tries to go inside. It has to match before you call this.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="session"></param>

        public void Execute(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session, string phttpVerb, string pPath)
        {
            if (this.middleware != null && this.middleware.Count() > 0)
            {
                //Check middleware
                var p = (from l in this.middleware
                         where !l.Check(request, response, session)
                         select l).ToList();
                if (p.Count > 0) return;
            }




            if (this.IsRouteGroup())
            {
                var pURLS = (from l in this.routingDefinitions
                             where l.Matches(phttpVerb: phttpVerb, pPath: pPath)
                             select l).ToList();

                if (pURLS.Count > 0)
                {
                    pURLS.First().Execute(request: request, response: response, session: session, phttpVerb: phttpVerb, pPath: pPath);
                    return;
                }
            }
            else this.closure(request, response, session);


        }




        public bool IsRouteGroup()
        {
            return this.closure == null;
        }



    }

}