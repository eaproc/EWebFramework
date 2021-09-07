using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.exceptions.auth
{
    /// <summary>
    /// Needs you to log in to access this URL
    /// </summary>
    public class URLAccessUnPermitted : Exception
    {
        public URLAccessUnPermitted():base("You do not have enough permission to access this URL"){

        }
    }
}