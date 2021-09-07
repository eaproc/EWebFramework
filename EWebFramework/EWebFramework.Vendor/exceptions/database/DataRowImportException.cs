using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.exceptions.database
{
    public class DataRowImportException : Exception
    {

        public List<object> RowDetails;

        public DataRowImportException(Exception exception, DataRow dr):base(message: exception.Message, 
            innerException: exception)
        {
            RowDetails = new List<object>();
            if (dr != null) RowDetails.AddRange(dr.ItemArray);
        }

    }
}