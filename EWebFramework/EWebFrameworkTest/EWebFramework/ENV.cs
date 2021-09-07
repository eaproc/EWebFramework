using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using EPRO.Library;

namespace EWebFramework
{
    public class ENV: EWebFramework.Vendor.utils.ENV
    {

        /**
         *  You can extend the configurations here for .env file
         ***/
        public ENV():base(EnvFileLocation:null) // Use default
        {
            
        }



        public static string KarixSMS(String pVariableName, String pDefaultValue = null)
        {

            return new ENV().GetValue("KarixSMS", pVariableName, pDefaultValue);

        }

        public static string BulkSMSNigeria(String pVariableName, String pDefaultValue = null)
        {

            return new ENV().GetValue("BulkSMSNigeria", pVariableName, pDefaultValue);

        }



        public static string Paystack(String pVariableName, String pDefaultValue = null)
        {

            return new ENV().GetValue("Paystack", pVariableName, pDefaultValue);

        }





    }
}