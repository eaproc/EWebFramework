using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.services.DataTablesNET
{
    [Serializable()]
    public class DataTableReturnFormat
    {
        public int draw, recordsTotal, recordsFiltered;
        public object data, totalPagesSummary;

        public DataTableReturnFormat()
        { }

        public DataTableReturnFormat(DataTableRequestFields dTR)
        {
            this.recordsTotal = 0;
            this.recordsFiltered = 0;
            this.draw = dTR.Draw;
        }

    }
}