using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.utils.JsonReplies
{
    public class DataTableFailedMessage:FailedResult
    {
        public int draw, recordsTotal=0, recordsFiltered=0;

        public DataTableFailedMessage(int draw, String pMessage="Error Fetching Data") :base(pMessage, new List<object>() )
        {
            this.draw = draw;
        }


    }
}