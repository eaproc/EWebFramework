using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace EWebFramework // Change this to your app Namespace
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {


        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            //Logger.Print("Error on application at " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        }

        protected void Session_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            //Logger.Print("Ending application at " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}