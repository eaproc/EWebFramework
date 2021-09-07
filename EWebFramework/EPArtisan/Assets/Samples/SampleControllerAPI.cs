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
using EWebFramework.Vendor.api.utils.JsonReplies;

namespace EWebFramework.api.controllers
{
    public class SampleController:BaseController
    {
        //if api controller

        private readonly  services.SampleService service;
        private readonly SessionHelper sh;
        public SampleController():base()
        {
             sh = new SessionHelper();
            service = new services.SampleService(sh);

        }




        public  void Index()
        {

            //Contents

        }


    }
}