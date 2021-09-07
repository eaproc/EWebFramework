// SampleConsole Description
using EWebFramework.Vendor.consoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EWebFramework.consoles
{
    /// <summary>
    /// Summary description for SampleConsole
    /// </summary>
    public class SampleConsole:BaseConsole
    {
        public SampleConsole():base()
        {
            //
            // TODO: Add constructor logic here
            //

            //@@CREATE_LOGGERS
        }

        public override string ConsoleName()
        {
            return "SampleConsole"; //@@ replace
        }

        public override void Handle(HttpRequest request, HttpResponse response, HttpSessionState session)
        {
            throw new NotImplementedException();
        }
    }
}
 