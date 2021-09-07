using EWebFramework.Vendor.api.utils;
using EWebFramework.Vendor.mailing.mailable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.mailing.mailable.auth
{
    public class ResetModePinCode : Mail
    {

        private String PinCode;


        public ResetModePinCode(String To, String PinCode):base()
        {
            this.Receiver = To;
            this.PinCode = PinCode;
            this.Subject = "RESET MODE PIN  | SCADWARE";
        }




        public override string Message
        {
            get
            {


                return this.Resource.Replace(
                    Variable("APP_URL"), ENV.APP_URL
                    ).Replace(
                    Variable("PIN_CODE"), this.PinCode
                    );


            }
        }


        public override IEnumerable<string> FileFullPathNames => null;


        public override string ResourceHTMLFile => "auth/ResetModePinCode.html";



    }
}