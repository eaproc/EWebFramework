using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWebFramework.Vendor.loggers
{
    public class ResponseLogger : ILoggable
    {

        private System.Web.HttpResponse response;


        public ResponseLogger()
        {
            this.response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.ContentType = "text/html; charset=utf-8";
        }




        public void Write(string contents)
        {
            this.response.Write("<p>" + contents + "</p>");
        }
    }
}
