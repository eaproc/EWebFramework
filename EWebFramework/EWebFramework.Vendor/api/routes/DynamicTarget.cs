using System.Linq;
using System.Web;
using static EWebFramework.Vendor.PageHandlers;


namespace EWebFramework.Vendor.api.routes
{
    public class DynamicTarget<T> : IRouteDefinition where T: new()
    {
        public string path;
        public string[] httpVerb;
        public string closure;

        public DynamicTarget(string pPath, string[] pHttpVerb, string pClosure, params middlewares.IMiddlewareCheckConstraint[] pMiddlewares):base(pMiddlewares)
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

            try
            {

                T caller = new T();
                var methods = caller.GetType().GetMethods().Where(x => x.Name == this.closure).ToList();
                if (methods.Count == 0) throw new InvalidRouteTargetException(closure, T: typeof ( T ) ) ;

                methods.First().Invoke(caller, new object[] { });

            }
            catch (InvalidRouteTargetException) { throw;  }
            catch(System.Reflection.TargetInvocationException ex)
            {
                if(ex.InnerException != null ) throw ex.InnerException;
                throw ex;
            }
            catch (System.Threading.ThreadAbortException) { throw; }
            catch (System.Exception ex)
            {
                Logger.Print("ex.GetType().FullName: " + ex.GetType().FullName);
                throw ex;
            }

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