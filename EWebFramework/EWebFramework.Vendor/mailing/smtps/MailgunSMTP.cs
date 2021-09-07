using EPRO.Library.Objects;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EWebFramework.Vendor.api.utils;
using static EWebFramework.Vendor.PageHandlers;
using EWebFramework.Vendor.mailing.mailable;

namespace EWebFramework.Vendor.mailing.smtps
{
    public class MailgunSMTP:BasicSMTP
    {

        public override string Sender => ENV.MailgunSMTP("Sender");

        public override string Host => ENV.MailgunSMTP("Host");

        public override int Port =>  EInt.valueOf( ENV.MailgunSMTP("Port") );

        public override string Username => ENV.MailgunSMTP("Username");

        public override string Password => ENV.MailgunSMTP("Password");

        public override string Name => ENV.MailgunSMTP("Name");


        /// <summary>
        /// Incase you are creating a different Mailgun Account
        /// </summary>
        protected virtual String API_KEY
        {
            get
            {
                return ENV.MailgunSMTP("API_KEY");
            }
        }
        protected virtual String API_DOMAIN
        {
            get
            {
                return ENV.MailgunSMTP("API_DOMAIN");
            }
        }


        public MailgunSMTP() : base() { }







        public class APIResponse
        {
            public string id { get; set; }
            public string message { get; set; }

            public bool IsSuccessful()
            {

                //SUCCESS RESULT SAMPLE
                //            {
                //    "id": "<20180912093041.1.5D148A1F8A79BF25@sandboxcc4ac68c3e0b4ed7b88ad90a2339eeef.mailgun.org>",
                //  "message": "Queued. Thank you."
                return this.message != null && this.message != String.Empty && this.message.StartsWith("Queued");
            }

        }


        public override bool SendViaHTTP(IMailable mailable, String SenderEmailAddress = null)
        {


            try
            {

                SenderEmailAddress = SenderEmailAddress == null ? this.Sender : SenderEmailAddress;




                //https://documentation.mailgun.com/en/latest/api-sending.html#sending



                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator =
                    new HttpBasicAuthenticator("api", this.API_KEY);
                RestRequest request = new RestRequest();
                request.AddParameter("domain", this.API_DOMAIN, ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", SenderEmailAddress);
                request.AddParameter("to", mailable.Receiver);
                request.AddParameter("subject", mailable.Subject);
                //request.AddParameter("text", "Congratulations Ibukun Bello, you just sent an email with Mailgun!  <b>You</b> are truly awesome!"); //TEXT VERSION
                request.AddParameter("html", mailable.Message);


                //Add CCs if available
                if (mailable.CC != null && mailable.CC != string.Empty)
                {
                    request.AddParameter("cc", mailable.CC);
                }

                //Add BCCs if available
                if (mailable.BCC != null && mailable.BCC != string.Empty)
                {
                    request.AddParameter("bcc", mailable.BCC);
                }


                if (mailable.FileFullPathNames != null)
                    foreach (String s in mailable.FileFullPathNames)
                        request.AddFile("attachment", s);


                request.Method = Method.POST;
                RestResponse restResponse =  (RestResponse)client.Execute(request);

                APIResponse responseBody = JsonConvert.DeserializeObject<APIResponse>(restResponse.Content);



                //if(!responseBody.IsSuccessful())
                //    T___EmailUsage.add(
                //          pDeliveryStatusID: (int)T___SMSDeliveryStatusExtended.EnumTable.Failed, pCreatedAt: DateTime.Now.FromServerTimeZone(),
                //          pExceptionMessage: restResponse.Content, pStackTrace: "Using HTTP Method", pMessage: mailable.Message,
                //          pReceiver: mailable.Receiver, pSender: SenderEmailAddress,
                //          pSMTPName: this.Name, pSubject: mailable.Subject, pAttachmentNames: String.Join(", ", mailable.FileFullPathNames)
                //      );





                return responseBody.IsSuccessful();



            }
            catch (Exception ex)
            {
                // Log
                //try
                //{
                //    T___EmailUsage.add(
                //           pDeliveryStatusID: (int)T___SMSDeliveryStatusExtended.EnumTable.Failed, pCreatedAt: DateTime.Now.FromServerTimeZone(),
                //           pExceptionMessage: ex.Message, pStackTrace: ex.StackTrace, pMessage: mailable.Message,
                //           pReceiver: mailable.Receiver, pSender: SenderEmailAddress,
                //           pSMTPName: this.Name, pSubject: mailable.Subject, pAttachmentNames: String.Join(", ", mailable.FileFullPathNames)
                //       );
                //}
                //catch (Exception e)
                //{
                //    Logger.Print("Error Logging EMaIL to DB!", e);
                //}

                Logger.Print(ex);

                return false;
            }



        }






    }
}