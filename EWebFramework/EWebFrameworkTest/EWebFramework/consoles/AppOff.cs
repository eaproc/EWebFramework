using EWebFramework.Vendor.consoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EWebFramework.consoles
{
    /// <summary>
    /// Summary description for AppOff
    /// </summary>
    public class AppOff:BaseConsole
    {
        public AppOff():base()
        {
            //
            // TODO: Add constructor logic here
            //

            
        }

        public override string ConsoleName()
        {
            return "AppOff"; //@@ replace
        }

        public override void Handle(HttpRequest request, HttpResponse response, HttpSessionState session)
        {
            EWebFramework.Vendor.MaintenanceMode.TurnOnMaintenance();
            Log("Application is now in Maintenance Mode!");
        }
    }
}
 