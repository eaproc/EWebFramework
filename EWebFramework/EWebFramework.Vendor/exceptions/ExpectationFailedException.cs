using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.exceptions
{
    /// <summary>
    /// This exception is only for logged in Users, with Role Selected
    /// </summary>
    public class ExpectationFailedException : Exception
    {
        public ExpectationFailedException(String pMessage):base(pMessage)
        {

        }
    }
}