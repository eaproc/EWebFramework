using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using EWebFramework.Vendor.loggers;

namespace EWebFramework.Vendor.consoles
{
    /// <summary>
    /// Summary description for BaseConsole
    /// </summary>
    public abstract class BaseConsole
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseConsole()
        {
            //
            // TODO: Add constructor logic here
            //
            this.Loggers = new List<ILoggable>
            {
                //this.Loggers.Add(new EWebFramework.loggers.ConsoleLogger());


                new ResponseLogger()
            };

            if (!HttpContext.Current.Request.IsLocal) throw new Exception("Consoles can only be run locally");

            this.sbJobInfo = new StringBuilder();


        }

        /// <summary>
        /// Use to store information on current cycle that can be sent to database
        /// </summary>
        protected StringBuilder sbJobInfo;

        protected List<ILoggable> Loggers;

        public abstract String ConsoleName();

        public abstract void Handle(HttpRequest request, HttpResponse response, System.Web.SessionState.HttpSessionState session);


        public void Log(String content)
        {
            foreach (ILoggable Logger in this.Loggers) Logger.Write(content);
        }
        /// <summary>
        /// Use to store information on current cycle that can be sent to database
        /// </summary>
        /// <param name="content"></param>
        public void Info(string content)
        {
            this.sbJobInfo.AppendLine(content);
        }

    }

}
