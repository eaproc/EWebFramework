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

namespace EWebFramework.api.controllers
{
    public class Test:BaseController
    {

        public Test() : base()
        {
            
        }


 

        public void Index()
        {

          




            response.OutputJSON("{\"message\": \"This is a test page! EAPROC Copyright 2018\"}");



        }







    }
}