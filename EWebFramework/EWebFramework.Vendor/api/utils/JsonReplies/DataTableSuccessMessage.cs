using EWebFramework.Vendor.api.services.DataTablesNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.utils.JsonReplies
{
    public class DataTableSuccessMessage:SuccessResult
    {

        public DataTableSuccessMessage(DataTableReturnFormat pData):base(pData:pData.data)
        {
            this.draw = pData.draw;
            this.recordsFiltered = pData.recordsFiltered;
            this.recordsTotal = pData.recordsTotal;
            this.totalPagesSummary = pData.totalPagesSummary;
        }

        
        public int draw, recordsTotal, recordsFiltered;
        public object totalPagesSummary;


    }
}