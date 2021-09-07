using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPArtisan.Dumpers
{
    public static class VarDump
    {


        public static void dd (params object[] values)
        {
            throw new VarDumpException(values);
          
        }






    }
}