using CODERiT.Logger.v._3._5;
using EPRO.Library.Objects;
using EWebFramework.Vendor.api.routes;
using EWebFramework.Vendor.api.utils;
using EWebFramework.Vendor.consoles;
using System;
using System.Linq;

namespace EWebFramework.Vendor
{
    /// <summary>
    /// Use for handling request
    /// </summary>
    public static class PageHandlers
    {




        static PageHandlers()
        {
            var logFile = StoragePath(String.Format("{0}__EPWebFramework.Core.log", DateTime.Now.ToString("yyyy_MM_dd")));

            Logger = new Log1(logFile, Log1.Modes.FILE, false);
        }





        /// <summary>
        /// Calls SetRequestBodyContent
        /// </summary>
        /// <param name="preScript"></param>
        /// <param name="postScript"></param>

        public static void HandleConsolePageRequest(Action preScript = null , Action postScript = null)
        {

          
            preScript?.Invoke();

            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
            System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
            System.Web.SessionState.HttpSessionState Session = System.Web.HttpContext.Current.Session;


            RequestHelper rh = new RequestHelper(Request);

            String pConsoleName = EStrings.valueOf(rh.Get("p"));

            Object handle = GetInstance("EWebFramework.consoles." + pConsoleName);
            BaseConsole bc = (BaseConsole)handle;

            if (bc == null) throw new Exception(pConsoleName + " does not exists in consoles!");

            bc.Handle(request: Request, response: Response, session: Session);






            postScript?.Invoke();


        }



        /// <summary>
        /// Handles Web Route
        /// </summary>
        /// <param name="routingList"></param>
        /// <param name="preScript"></param>
        /// <param name="postScript"></param>
        public static void HandleWebPageRequest(IRoutingList routingList, Action preScript = null, Action postScript = null)
        {


            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
            System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
            System.Web.SessionState.HttpSessionState Session = System.Web.HttpContext.Current.Session;



            //
            //
            //      Check if it is needed to force url to start with www
            //
            //

            if (EBoolean.valueOf(api.utils.ENV.General("REDIRECT_WWW")) && !Request.Url.Host.ToLower().StartsWith("www."))
            {
                Response.Redirect(Request.Url.ToString().Replace(Request.Url.Host, "www." + Request.Url.Host));
                return;
            }





            //
            //
            //      Check if it isn't running on https and it isn't localhost
            //      Redirect to secure url
            //
            //

            if (!Request.IsSecureConnection && (!Request.IsLocal && EBoolean.valueOf(api.utils.ENV.General("REDIRECT_HTTPS"))))
            {
                Response.Redirect(Request.Url.ToString().Replace("http://", "https://"));
                return;
            }





            //
            // Check if we are in maintenance mode and the URL is not maintenance mode url
            // redirectly to maintenance mode url
            //
            if (!MaintenanceMode.IsMaintenanceModeURL(Request) && new MaintenanceMode().IsInMaintenanceMode())
            {
                //redirect
                Response.Redirect("?p=" + MaintenanceMode.URL, true);
                return;
            }





            //
            // Calls PreScript
            //
            preScript?.Invoke();



            //
            // Do matching
            //

            if (Request.QueryString.Cast<String>().Contains("p"))
            {
                var url = Request.QueryString["p"];
                var pURLS = (from l in routingList.RoutingDefinitions
                             where l.Matches(Request.HttpMethod, url)
                             select l).ToList();

                if (pURLS.Count > 0)
                {
                    pURLS.First().Execute(Request.HttpMethod, url);








                    //
                    // Calls postScript
                    //
                    postScript?.Invoke();




                    return;
                }


                // 
                // this will run if the page contains [p] parameter but with unknown path
                Response.Redirect("?p=/errors/404&url=" + url, true);


            }
            else
            {
                //redirect
                Response.Redirect("?p=/home", true);
            }







        }


        /// <summary>
        /// Handles API Route
        /// </summary>
        /// <param name="routingList"></param>
        /// <param name="preScript"></param>
        /// <param name="postScript"></param>
        public static void HandleAPIPageRequest(IRoutingList routingList, Action preScript = null, Action postScript = null)
        {


            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
            System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
            System.Web.SessionState.HttpSessionState Session = System.Web.HttpContext.Current.Session;



            //
            // Check if we are in maintenance mode and the URL is not maintenance mode url
            // redirectly to maintenance mode url
            //
            if (!MaintenanceMode.IsMaintenanceModeURL(Request) && new MaintenanceMode().IsInMaintenanceMode())
            {
                //redirect
                //Response.Redirect("?p=" + MaintenanceMode.URL, true);
                // API doesn't support redirection.
                // It will terminate
                api.controllers.system.MaintenanceMode.Index(Request, Response, Session);
                return;
            }



            //
            // Calls PreScript
            //
            preScript?.Invoke();




            if (Request.QueryString.Cast<String>().Contains("p"))
            {
                var url = Request.QueryString["p"];
                var pURLS = (from l in routingList.RoutingDefinitions
                             where l.Matches(Request.HttpMethod, url)
                             select l).ToList();

                if (pURLS.Count > 0)
                {
                    pURLS.First().Execute(phttpVerb: Request.HttpMethod, pPath: url);







                    //
                    // Calls postScript
                    //
                    postScript?.Invoke();





                    return;
                }


            }


            throw new exceptions.InvalidURLException(Request);




        }










        /// <summary>
        /// It is created once as static in this class and it is used within this module
        /// EWebFramework.Vendor
        /// </summary>
        public static Log1 Logger { get; private set; }






        /// <summary>
        /// Used to create instance of a class specified as string to Object
        /// </summary>
        /// <param name="strFullyQualifiedName"></param>
        /// <returns></returns>
        public static object GetInstance(string strFullyQualifiedName)
        {
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null)
                return Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(strFullyQualifiedName);
                if (type != null)
                    return Activator.CreateInstance(type);
            }
            return null;
        }



        /// <summary>
        /// Refers to the app Virtual Director
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public static string RootPath(string pPath)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "/" + pPath;
            //return HttpContext.Current.Server.MapPath("~/" + pPath);
        }


        /// <summary>
        /// Refers to App_Resources folder
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public static string ResourcesPath(string pPath)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "/App_Resources/" + pPath;
            //return HttpContext.Current.Server.MapPath("~/App_Resources/" + pPath);
        }


        /// <summary>
        /// Returns path relative to the value specified in 
        /// ENV as REDIRECTED_RESOURCE_PATH=D:/
        /// </summary>
        /// <param name="RelativePath"></param>
        /// <returns></returns>
        public static string RedirectedResourcePath(string RelativePath)
        {
            return ENV.General("REDIRECTED_RESOURCE_PATH") + "\\" + RelativePath;
        }


        /// <summary>
        /// Returns path to the App_Data folder
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public static string StoragePath(string pPath)
        {
            // if(Logger != null) Logger.Print(pPath);
            return AppDomain.CurrentDomain.BaseDirectory + "/App_Data/" + pPath;
            //return HttpContext.Current.Server.MapPath("~/App_Data/" + pPath);
        }



    }
}
