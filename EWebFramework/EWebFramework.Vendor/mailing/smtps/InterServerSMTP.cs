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
using EWebFramework.Vendor.mailing.smtps;
using System.Net.Mail;

namespace EWebFramework.Vendor.mailing.smtps
{
    public class InterServerSMTP : BasicSMTP
    {

        public override string Sender => ENV.InterServerSMTP("Sender");

        public override string Host => ENV.InterServerSMTP("Host");

        public override int Port =>  EInt.valueOf( ENV.InterServerSMTP("Port") );

        public override string Username => ENV.InterServerSMTP("Username");

        public override string Password => ENV.InterServerSMTP("Password");

        public override string Name => ENV.InterServerSMTP("Name");




        public InterServerSMTP() : base() { }



   


    }
}