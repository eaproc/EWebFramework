using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.exceptions
{
    /// <summary>
    /// Indicates the URL you are trying to open is invalid
    /// </summary>
    public class InvalidURLException:Exception
    {
        public InvalidURLException(HttpRequest Request):base(
            String.Format("URL [{0}] NOT FOUND! Using Http Verb [ {1} ]", 
                             Request.QueryString.Cast<String>().Contains("p") ? Request.QueryString["p"] : Request.Url.ToString(),
                             Request.HttpMethod
                )
            )
        {

        }
    }
}