using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.routes
{
    public class InvalidRouteTargetException:Exception
    {
        public InvalidRouteTargetException(String pMethodName, Type T): base(String.Format("Unfortunately, the method ({0}) was not located in this class ({1})", pMethodName, T.FullName))
        {

        }
    }
}