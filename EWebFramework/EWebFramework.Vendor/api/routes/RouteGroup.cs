using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EWebFramework.Vendor.api.routes
{
    public class RouteGroup : IRouteDefinition
    {
        protected IRouteDefinition[] routingDefinitions;


        public RouteGroup(params IRouteDefinition[] pRoutingDefinitions
           ):base(null)
        {
            this.routingDefinitions = pRoutingDefinitions;
        }


        public RouteGroup(middlewares.IMiddlewareCheckConstraint[] pMiddlewares,
                            params IRouteDefinition[] pRoutingDefinitions
            ) : base(pMiddlewares)
        {
            this.routingDefinitions = pRoutingDefinitions;
        }





        public override void Execute(string phttpVerb, string pPath)
        {
            base.Execute(phttpVerb: phttpVerb, pPath: pPath);

                var pURLS = (from l in this.routingDefinitions
                             where l.Matches(phttpVerb: phttpVerb, pPath: pPath)
                             select l).ToList();

                if (pURLS.Count > 0)
                {
                    pURLS.First().Execute(phttpVerb: phttpVerb, pPath: pPath);
                    return;
                }

        }


        public override bool Matches(string phttpVerb, string pPath)
        {
            if (this.routingDefinitions == null) return false;

            var p = from r in routingDefinitions
                    where r.Matches(phttpVerb: phttpVerb, pPath: pPath)
                    select r;

            return p.Count() > 0;
        }



    }

}