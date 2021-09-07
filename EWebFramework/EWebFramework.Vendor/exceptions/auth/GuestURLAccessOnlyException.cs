using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.exceptions.auth
{
    /// <summary>
    /// Indicates the URL you are trying to open is invalid
    /// </summary>
    public class GuestURLAccessOnlyException : Exception
    {
        public GuestURLAccessOnlyException():base("You have to be a guest to access this URL")
        {}
    }
}