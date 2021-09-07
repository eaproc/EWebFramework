using EPRO.Library.Objects;
using EWebFramework.Vendor.api.services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWebFramework.Vendor.api.utils.DataExports.Excel
{
    public abstract class INPOIExcelExport
    {


        protected DataTable __data;


        public INPOIExcelExport(DataTable data)
        {
            this.__data = data;
        }




        public List<Dictionary<string, object>> GetData()
        {
            return this.ToDictionary();
        }




        public virtual String[] GetColumnNames()
        {
            List<String> p = new List<string>();

            foreach (DataColumn c in this.__data.Columns)
            {
                p.Add(c.ColumnName);
            }

            return p.ToArray();

        }




        /// <summary>
        /// You can override this to have more access to each column's format
        /// </summary>
        /// <param name="value"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public virtual object GetReadableFormat(object value, DataColumn col)
        {
            return value;
            //return false;
        }


        public  Dictionary<string, object> ToDictionary(DataRow pRow)
        {
            if (pRow == null) return new Dictionary<String, object>();

            var p = from c in pRow.Table.Columns.Cast<DataColumn>()
                    select new KeyValuePair<string, object>(c.ColumnName,
                        GetReadableFormat(pRow[c.ColumnName], c)
                    );

            return p.ToDictionary(x => x.Key, x => x.Value);

        }




        public List<Dictionary<string, object>> ToDictionary( )
        {
            DataTable dt = this.__data;

            var pRows = new List<Dictionary<string, object>>();
            if (dt != null)
            {
                pRows = (from dRow in dt.AsEnumerable()
                         select ToDictionary(dRow)
                        ).ToList();

            }
            return pRows;
        }








    }
}
