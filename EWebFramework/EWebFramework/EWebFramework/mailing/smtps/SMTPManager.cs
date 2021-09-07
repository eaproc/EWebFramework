using EWebFramework;
using EWebFramework.mailing.smtps;
using EWebFramework.Vendor.mailing.smtps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.mailing.smtps
{
    public static class SMTPManager
    {


        public static ISMTP Get()
        {
            if (ENV.APP_DEBUG()) return new InterServerSMTP();

            return new MailTrapSMTP();

        }


    }
}