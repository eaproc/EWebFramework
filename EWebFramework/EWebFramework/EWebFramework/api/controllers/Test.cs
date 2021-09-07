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
using EWebFramework.Vendor.api.utils.JsonReplies;
using EWebFramework.Vendor.api.utils;

namespace EWebFramework.api.controllers
{
    public class Test:BaseController
    {

        public Test() : base()
        {
            
        }


 

        public void Index()
        {


            var v = new RequestHelper( request: request);
            var v2 = new RequestHelper( request: request);

            
            response.OutputJSON(new SuccessResult( pData: new {
                message = "This is a test page! EAPROC Copyright 2021",
                data1 = v.Get("data"),
                data2 = v2.Get("data")
            } ));



        }







    }
}