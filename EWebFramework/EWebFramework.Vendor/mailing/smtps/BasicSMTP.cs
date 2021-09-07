using static EWebFramework.Vendor.PageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using EWebFramework.Vendor.mailing.mailable;

namespace EWebFramework.Vendor.mailing.smtps
{
    public abstract class BasicSMTP : ISMTP
    {

        public abstract string Sender { get; }
        public abstract string Host { get; }
        public abstract int Port { get; }
        public abstract string Username { get; }
        public abstract string Password { get; }
        public abstract string Name { get; }

        string ISMTP.Name => throw new NotImplementedException();

        string ISMTP.Sender => throw new NotImplementedException();

        string ISMTP.Host => throw new NotImplementedException();

        int ISMTP.Port => throw new NotImplementedException();

        string ISMTP.Username => throw new NotImplementedException();

        string ISMTP.Password => throw new NotImplementedException();





        public BasicSMTP()
        {
        }

        /// <summary>
        /// Sends Via SMTP
        /// </summary>
        /// <param name="mailable"></param>
        /// <param name="SenderEmailAddress"></param>
        /// <returns></returns>
        public bool Send(IMailable mailable, String SenderEmailAddress)
        {


            try
            {

                MailMessage msg = new MailMessage(from: SenderEmailAddress, to: mailable.Receiver)
                {
                    Subject = mailable.Subject,
                    IsBodyHtml = true,
                    Body = mailable.Message
                };

                //Add CCs if available
                if(mailable.CC!=null && mailable.CC!=string.Empty)
                {
                    foreach (string s in mailable.CC.Split(','))
                        msg.CC.Add(s);
                }

                //Add BCCs if available
                if (mailable.BCC != null && mailable.BCC != string.Empty)
                {
                    foreach (string s in mailable.BCC.Split(','))
                        msg.Bcc.Add(s);
                }


                if (mailable.FileFullPathNames!=null)
                    foreach (String s in mailable.FileFullPathNames)
                        msg.Attachments.Add(new Attachment(fileName: s));


                SmtpClient smtpClient = new SmtpClient(host: this.Host, port: this.Port );
                smtpClient.Credentials = new System.Net.NetworkCredential(userName: this.Username, password: this.Password);

                smtpClient.Send(msg);


                // Log here if necessary
                return true;


            }
            catch (Exception ex)
            {
                //    // Log
                //    try
                //    {
                //        T___EmailUsage.add(
                //               pDeliveryStatusID: (int)T___SMSDeliveryStatusExtended.EnumTable.Failed, pCreatedAt: DateTime.Now.FromServerTimeZone(),
                //               pExceptionMessage: ex.Message, pStackTrace: ex.StackTrace, pMessage: mailable.Message,
                //               pReceiver: mailable.Receiver, pSender: SenderEmailAddress,
                //               pSMTPName: this.Name, pSubject: mailable.Subject, pAttachmentNames: String.Join(", ", mailable.FileFullPathNames)
                //           );
                //    }
                //    catch (Exception e)
                //    {
                //        Logger.Print("Error Logging EMaIL to DB!", e);
                //    }

                Logger.Print(ex);


                return false;
            }

        }

        /// <summary>
        /// Sends Via SMTP
        /// </summary>
        /// <param name="mailable"></param>
        /// <returns></returns>
        public bool Send(IMailable mailable)
        {
            return ((ISMTP)this).Send(mailable, this.Sender);
        }

     



        void IDisposable.Dispose()
        {
            // DO NOTHING
            //throw new NotImplementedException();
        }

        /// <summary>
        /// If you don't override it and needs it then you will get not implemented exception
        /// </summary>
        /// <param name="mailable"></param>
        /// <param name="SenderEmailAddress"></param>
        /// <returns></returns>
        public virtual bool SendViaHTTP(IMailable mailable, string SenderEmailAddress)
        {
            throw new NotImplementedException("Please, override this method");
        }

        /// <summary>
        /// If you don't override it and needs it then you will get not implemented exception
        /// </summary>
        /// <param name="mailable"></param>
        /// <param name="SenderEmailAddress"></param>
        /// <returns></returns>
        public virtual bool SendViaCustom(IMailable mailable, string SenderEmailAddress)
        {
            throw new NotImplementedException("Please, override this method");
        }


    }
}