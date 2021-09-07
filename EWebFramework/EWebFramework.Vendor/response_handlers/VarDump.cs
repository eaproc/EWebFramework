using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.response_handlers
{
    public static class VarDump
    {


        public static void dd (params object[] values)
        {
            throw new VarDumpException(values);
          
        }






    }
}