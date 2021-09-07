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
    public class SampleController : BaseController
    {

        private readonly api.services.SampleService service;
        private readonly SessionHelper sh;
        public SampleController() : base()
        {
            sh = new SessionHelper();
            service = new api.services.SampleService(sh);

        }




        public void Index()
        {

            //Contents

        }


    }
}