using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;


namespace EPArtisan.Utils.JsonReplies
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











       public object toArray()
        {
            throw new NotImplementedException();
        }



        public string ToJson()
        {
            return new JavaScriptSerializer().Serialize(this);
        }



    }
}