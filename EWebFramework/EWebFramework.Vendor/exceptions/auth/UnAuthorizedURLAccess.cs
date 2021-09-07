using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.exceptions.auth
{
    /// <summary>
    /// Needs you to log in to access this URL
    /// </summary>
    public class UnAuthorizedURLAccess:Exception
    {
        public  UnAuthorizedURLAccess():base("You are not allowed to access this URL! You must be logged in!"){

        }
    }
}