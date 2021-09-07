using EPRO.Library.Objects;
using EWebFramework.Vendor.api.utils;
using EWebFramework.Vendor.mailing.smtps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.mailing.smtps
{
    public class MailTrapSMTP:BasicSMTP
    {


        public override string Sender => ENV.MailTrapSMTP("Sender");

        public override string Host => ENV.MailTrapSMTP("Host");

        public override int Port => EInt.valueOf(ENV.MailTrapSMTP("Port"));

        public override string Username => ENV.MailTrapSMTP("Username");

        public override string Password => ENV.MailTrapSMTP("Password");

        public override string Name => ENV.MailTrapSMTP("Name");






    }
}