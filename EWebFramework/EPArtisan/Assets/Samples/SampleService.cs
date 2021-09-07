
using DB.Abstracts;

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPRO.Library.Objects;
using EPRO.Library.Modules;
using EWebFramework.Vendor.api.utils;

namespace EWebFramework.api.services
{
    public class SampleService:ClientService
    {

        public SampleService(SessionHelper sessionHelper) : base(sessionHelper)
        {

        }

    }
}