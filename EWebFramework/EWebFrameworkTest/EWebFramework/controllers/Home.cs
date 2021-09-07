using EPRO.Library;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;

using System.IO;
using EWebFramework.Vendor.api.controllers;
using EWebFramework.Vendor.response_handlers;
using EWebFramework.Vendor.api.utils;

namespace EWebFramework.controllers
{
    public class Home : BaseController
    {

        private readonly SessionHelper sh;
        public Home() : base()
        {
            sh = new SessionHelper();

        }




        public void Index()
        {

            BasicResponse.OutputPage("views/Home.html");

        }


    }
}