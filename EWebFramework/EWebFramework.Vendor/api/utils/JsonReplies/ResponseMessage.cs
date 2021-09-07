using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;


namespace EWebFramework.Vendor.api.utils.JsonReplies
{
    public class ResponseMessage : IArrayable, IJsonable
    {



        public String message;
        public bool success;
        public object data;





        public ResponseMessage(bool pSuccess=true, String pMessage = null, object pData = null )
        {


            this.success = pSuccess;
            this.message = pMessage;
            this.data = pData;

        }










        /// <summary>
        /// NOT Implemented
        /// </summary>
        /// <returns></returns>
       public IEnumerable<object> toArray()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Converts the response to JSON with length maximum integer value
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
            return serializer.Serialize(this);
        }



    }
}