using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.exceptions
{
    public class InternalServerException:Exception
    {
        public InternalServerException(Exception exception = null):base(message: "Internal Server Error! Please, report to the administrator", 
            innerException: exception)
        {

        }
    }
}