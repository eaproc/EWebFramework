using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using EPRO.Library;
using EPRO.Library.AppConfigurations;
using EPRO.Library.Objects;
using static EWebFramework.Vendor.PageHandlers;


namespace EWebFramework.Vendor.api.utils
{
    public class ENV
    {

        private IniReader reader;

        public enum ENVIRONMENT { DEVELOPMENT, PRODUCTION, UNKNOWN }


        /// <summary>
        /// Uses RootPath(".env") if location is not specified
        /// </summary>
        /// <param name="EnvFileLocation"></param>
        public ENV(string EnvFileLocation = null)
        {
            var pPath = EnvFileLocation == null ? RootPath(".env") : EnvFileLocation;
            if (!System.IO.File.Exists(pPath))
                throw new Exception("Add .env file to your app!");

            this.reader = new IniReader(pPath, true, true);
        }




        protected string GetValue(String pSectionName, String pVariableName, String pDefaultValue = null)
        {
            ENV e = new ENV();
            var v = e.reader.getValue(pSectionName + IniReader.SECTION__NAME___SEPERATOR + pVariableName);

            return v == String.Empty ? pDefaultValue : v;

        }





        public static string DbConnection(String pVariableName, String pDefaultValue = null)
        {

            return new ENV().GetValue("DATABASE_CONNECTION", pVariableName, pDefaultValue);

        }




        public static string General(String pVariableName, String pDefaultValue = null)
        {

            return new ENV().GetValue("GENERAL", pVariableName, pDefaultValue);

        }

        /// <summary>
        /// Returns the APP_URL set in .env file
        /// </summary>
        public static string APP_URL
        {
            get { return General("APP_URL"); }
        }



        public static string MailgunSMTP(String pVariableName, String pDefaultValue = null)
        {

            return new ENV().GetValue("MailgunSMTP", pVariableName, pDefaultValue);

        }





        public static string InterServerSMTP(String pVariableName, String pDefaultValue = null)
        {

            return new ENV().GetValue("InterServerSMTP", pVariableName, pDefaultValue);

        }


        public static string MailTrapSMTP(String pVariableName, String pDefaultValue = null)
        {

            return new ENV().GetValue("MailTrapSMTP", pVariableName, pDefaultValue);

        }



        public static string Minio(String pVariableName, String pDefaultValue = null)
        {

            return new ENV().GetValue("Minio", pVariableName, pDefaultValue);

        }



        /// <summary>
        /// Indicate if app is in Debug mode
        /// </summary>
        /// <returns></returns>
        public static bool APP_DEBUG()
        {
            return EBoolean.valueOf(General("APP_DEBUG"));
        }



        /// <summary>
        /// Indicate you will be using the DB environment [ DbConnection ]
        /// </summary>
        /// <returns></returns>
        public static bool USE_APP_DB()
        {
            return EBoolean.valueOf(General("USE_APP_DB"));
        }




        /// <summary>
        /// Get application environment
        /// </summary>
        /// <returns></returns>
        public static ENVIRONMENT GetEnvironment()
        {
            switch (ENV.General("RUNNING_MODE"))
            {
                case "DEVELOPMENT":
                    return ENVIRONMENT.DEVELOPMENT;
                case "PRODUCTION":
                    return ENVIRONMENT.PRODUCTION;
                default:
                    return ENVIRONMENT.UNKNOWN;
            }
        }


    }
  }