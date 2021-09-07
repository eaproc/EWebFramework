using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.response_handlers
{
    public class VarDumpException:Exception
    {

        public object dumpedData;

        public VarDumpException(object pData):base("This is just an exception to show dumped data")
        {
            this.dumpedData = pData;

        }



    }
}