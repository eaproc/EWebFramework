using EWebFramework.Vendor.consoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EWebFramework.consoles
{
    /// <summary>
    /// Summary description for AppOn
    /// </summary>
    public class AppOn:BaseConsole
    {
        public AppOn():base()
        {
            //
            // TODO: Add constructor logic here
            //
            
            
        }

        public override string ConsoleName()
        {
            return "AppOn"; //@@ replace
        }

        public override void Handle(HttpRequest request, HttpResponse response, HttpSessionState session)
        {
            EWebFramework.Vendor.MaintenanceMode.TurnOffMaintenance();
            Log("Application is now LIVE!");
        }
    }
}
 