using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPRO.Library;
using EPRO.Library.v3._5.AppConfigurations;
using static EPRO.Library.v3._5.AppConfigurations.Settings;

namespace ESiteBackup
{
    public class ENV
    {

        private IniReader reader;


        public ENV()
        {
            var pPath = Program.AppPath(".env");
            if (!System.IO.File.Exists(pPath))
                throw new Exception("Add .env file to your app!");

            this.reader = new IniReader(pPath, true, true);

        }




        private static string GetValue(String pSectionName, String pVariableName, String pDefaultValue = null)
        {
            ENV e = new ENV();
            var v = e.reader.getValue(pSectionName + IniReader.SECTION__NAME___SEPERATOR + pVariableName);

            return v == String.Empty ? pDefaultValue : v;

        }


        private static bool SetValue(String pSectionName, String pVariableName, String pValue )
        {
            return WRITE_APP_CONFIG(pSectionName, pVariableName, pValue, iniFileName: Program.AppPath(".env"));
        }



        public static string DbConnection(String pVariableName, String pDefaultValue=null)
        {
          
            return GetValue("DATABASE_CONNECTION", pVariableName,pDefaultValue);

        }


        public static string ESiteBackup(String pVariableName, String pDefaultValue = null)
        {

            return GetValue("ESiteBackup", pVariableName, pDefaultValue);

        }




        public static string General(String pVariableName, String pDefaultValue = null)
        {

            return GetValue("GENERAL", pVariableName, pDefaultValue);

        }




        public static bool IGeneral(String pVariableName, String pValue)
        {
            return SetValue("GENERAL", pVariableName, pValue);
        }

        public static bool IDbConnection(String pVariableName, String pValue)
        {
            return SetValue("DATABASE_CONNECTION", pVariableName, pValue);

        }
        public static bool IESiteBackup(String pVariableName, String pValue)
        {
            return SetValue("ESiteBackup", pVariableName, pValue);

        }





    }
}