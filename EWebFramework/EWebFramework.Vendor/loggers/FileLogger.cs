using CODERiT.Logger.v._3._5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor.loggers
{
    public class FileLogger : ILoggable
    {

        private Log1 ___Logger;
         
        public FileLogger()
        {
            var logFile = StoragePath(String.Format("{0}__FileLogger.log", DateTime.Now.ToString("dd_MMM_yyy")));

            this.___Logger = new Log1(logFile, Log1.Modes.FILE, false);

        }




        public void Write(string contents)
        {
            this.___Logger.Write(contents);
        }
    }
}
