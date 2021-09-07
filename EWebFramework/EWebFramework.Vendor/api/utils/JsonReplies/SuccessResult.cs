using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.utils.JsonReplies
{
    public class SuccessResult : ResponseMessage
    {

        public SuccessResult(String pMessage = "SUCCESS", object pData = null):base(pSuccess:true, pMessage:pMessage, pData:pData)
        {
            
        }









    }

}