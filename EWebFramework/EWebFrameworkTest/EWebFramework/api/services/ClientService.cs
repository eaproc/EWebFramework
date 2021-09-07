using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using EPRO.Library.Objects;
using EWebFramework.Vendor.api.utils;
using EWebFramework.Vendor.api.controllers;
using System.Data;
using System.Data.SqlClient;
using EPRO.Library;
using EWebFramework.Vendor.api.services.DataTablesNET;
using System.Text;
using static EWebFramework.Vendor.PageHandlers;
using EWebFramework.Vendor.api.services;

namespace EWebFramework.api.services
{
    public class ClientService:BaseClientService
    {

        /// <summary>
        /// This should be the unique name given to the owner of this application
        /// </summary>
        public const string APP_UNIQUE_ID = "GABUS";



        public ClientService(SessionHelper sessionHelper):base(sessionHelper)
        {
            this.credentialManager = new CredentialManagerImplementer();
        }

        /// <summary>
        /// Use this if you are accessing it from CronJob. No session will be used
        /// </summary>
        public ClientService():this(null)
        {}



        #region "Utilities"


        //public static DateTime ServerNowDateTime
        //{
        //    get {
        //        return DateTime.Now.FromServerTimeZone();
        //    }
        //}


        #endregion



        #region FileStorages
        /// <summary>
        /// Location of all files relative to the storage path
        /// </summary>
        /// <returns></returns>
        public static string RelativeFilePath()
        {
            return String.Format(@"{0}_FILE_SERVER", APP_UNIQUE_ID);
        }


        /// <summary>
        /// Returns Full File Path From the FILE Server we are Using
        /// </summary>
        /// <param name="pFilePath"></param>
        /// <returns></returns>
        public static string FetchFromFileStore(String pFilePath)
        {
            return ResourcesPath(String.Format("{0}/{1}", RelativeFilePath(), pFilePath));
        }

        


        #region PersonsStore


        public string RelativeFilePath_Persons(int pPersonID)
        {
            return String.Format(@"{0}\persons\{1}", RelativeFilePath(), pPersonID);
        }


        public string RelativeFilePath_Persons()
        {
            return String.Format(@"{0}\persons", RelativeFilePath());
        }




        #endregion


        #endregion







    }
}