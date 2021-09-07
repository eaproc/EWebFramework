using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.routes
{
    /// <summary>
    /// 
    /// </summary>
    public class Target: IRouteDefinition
    {
        public string path;
        public string[] httpVerb;
        public CallableRoute closure;

        public Target(string pPath, string[] pHttpVerb, CallableRoute pClosure, params middlewares.IMiddlewareCheckConstraint[] pMiddlewares):base(pMiddlewares)
        {
            this.closure = pClosure;
            this.path = pPath;
            this.httpVerb = pHttpVerb;
        }





        /// <summary>
        /// Executes Middlewares along the way as it tries to go inside. It has to match before you call this.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="session"></param>

        public override void Execute(string phttpVerb, string pPath)
        {
            base.Execute(phttpVerb: phttpVerb, pPath: pPath);


            this.closure(request, response, session);

        }




        /// <summary>
        /// Checks through path to be sure there is a match
        /// </summary>
        /// <param name="phttpVerb"></param>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public override bool Matches(string phttpVerb, string pPath)
        {
            return this.httpVerb.Contains(phttpVerb) && this.path == pPath;

        }

      



    }

}