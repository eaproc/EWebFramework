﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPArtisan.Utils.JsonReplies
{
    public class FailedResult : ResponseMessage
    {

        public FailedResult(String pMessage = "FAILED", object pData = null):base(pSuccess:false, pMessage:pMessage, pData:pData)
        {
            
        }









    }

}