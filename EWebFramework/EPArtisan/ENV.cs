using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using EPRO.Library;
using EPRO.Library.AppConfigurations;

namespace EPArtisan
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





        public static string DbConnection(String pVariableName, String pDefaultValue=null)
        {
          
            return GetValue("DATABASE_CONNECTION", pVariableName,pDefaultValue);

        }




        public static string General(String pVariableName, String pDefaultValue = null)
        {

            return GetValue("GENERAL", pVariableName, pDefaultValue);

        }





    }
}