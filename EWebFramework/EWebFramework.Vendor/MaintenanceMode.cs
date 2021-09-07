using EPRO.Library;
using EPRO.Library.AppConfigurations;
using EPRO.Library.Objects;
using EWebFramework.Vendor.api.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor
{
    /// <summary>
    /// Summary description for MaintenanceMode
    /// </summary>
    public class MaintenanceMode
    {

        public const string URL = "/maintenance-mode";

        private readonly IniReader reader;

        public MaintenanceMode()
        {
            //
            // TODO: Add constructor logic here
            //

            var pPath = StoragePath("maintenance.mode.lock");
            if (System.IO.File.Exists(pPath))
            {
                this.reader = new IniReader(pPath, true, true);

            }
            else this.reader = null;
        }




        public bool IsInMaintenanceMode()
        {
            return this.reader != null;
        }



        public static bool TurnOnMaintenance()
        {
            if (new MaintenanceMode().IsInMaintenanceMode()) throw new Exception("It is already in maintenance mode!");
            var pPath = RootPath("maintenance.mode");
            if (!System.IO.File.Exists(pPath)) throw new Exception("Maintenance Mode file could not be located!");

            System.IO.File.Copy(pPath, StoragePath ( "maintenance.mode.lock" ), true);

            return true;

        }



        public static bool TurnOffMaintenance()
        {
            if (!new MaintenanceMode().IsInMaintenanceMode()) throw new Exception("Maintenance mode is already turned off!");
            var pPath = StoragePath("maintenance.mode.lock");
            if (!System.IO.File.Exists(pPath)) throw new Exception("Maintenance Mode Lock file could not be located!");

            EIO.DeleteFileIfExists(pPath);

            return true;
        }



        public static bool IsMaintenanceModeURL(HttpRequest httpRequest)
        {
            RequestHelper rh = new RequestHelper(httpRequest);
            return rh.ContainsKey("p") && EStrings.valueOf(rh.Get("p")) == URL;
        }



    }
}