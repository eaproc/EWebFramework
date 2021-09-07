using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EWebFramework.response_handlers;
using EWebFramework.api.services;
using System.Web.Script.Serialization;
using System.Data;
using System.Web.SessionState;
using System.IO;
using EPRO.Library.Objects;
using EWebFramework.api.controllers;



using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;
using EPRO.Library;
using System.Threading;
using EWebFramework.Vendor.api.controllers;

namespace EWebFramework.controllers
{
    public class Test:BaseController
    {


        public Test():base()
        {

         

        }



        public void Index()
        {
            //Normal Access
            response.Write("This is a test page! SCADWARE &copy; 2018");


        }



    }
}