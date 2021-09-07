using EPRO.Library.Objects;
using EWebFramework.Vendor.api.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.controllers
{
    public static class Controller
    {

        public static readonly string MIME_TYPE_JPG =  "image/jpg";


        public static IEnumerable<T> Append<T>(this IEnumerable<T> p, T x )
        {
            List<T> y = p.ToList();
            y.Add(x);
            return y;
        }

        public static IEnumerable<T> AddRange<T>(this IEnumerable<T> p, params T[] x)
        {
            List<T> y = p.ToList();
            if(x.Count()>0) y.AddRange(x);
            return y;
        }




        public static Dictionary<string, object> ToDictionary(this DataRow pRow)
        {
            if (pRow == null) return new Dictionary<String, object>();

            var p = from c in pRow.Table.Columns.Cast<DataColumn>()
                    select new KeyValuePair<string, object>(c.ColumnName, pRow[c.ColumnName]);

            return p.ToDictionary(x => x.Key, x => x.Value);

        }



        public static List<Dictionary<string, object>> ToDictionary(this DataTable dt)
        {
            var pRows = new List<Dictionary<string, object>>();
            if (dt != null)
            {
                pRows = (from dRow in dt.AsEnumerable()
                         select Controller.ToDictionary(dRow)
                        ).ToList();

            }
            return pRows;
        }


        public static Dictionary<string, object> FirstRowDictionary (this DataTable dataTable)
        {
            var d = dataTable.ToDictionary();
            return d.Count > 0 ? d.First() : null;
        }


        public static Dictionary<String, Object> CompactForInjection(params KeyValuePair<String, Object>[] p)
        {
            return new Dictionary<String, Object>(p.ToDictionary(x=>x.Key, x=>x.Value));
        }




        public static String ToTitleCase(this String v)
        {
            // Creates a TextInfo based on the "en-US" culture.
            System.Globalization.TextInfo myTI = new System.Globalization.CultureInfo("en-US", false).TextInfo;
            return myTI.ToTitleCase(v);

        }

        /// <summary>
        /// Converts the Target to Title Case onyl if it is lower case
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static String ToTitleCaseIfLowerCase(this String v)
        {
            if (v == v.ToLower()) return v.ToTitleCase();
            return v;
        }




        /// <summary>
        /// Try to differentiate if this request. Checks for Http POST or URL contains /api/
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool WantJSON(this HttpRequest v)
        {
            //RequestHelper rh = new RequestHelper(v);
            //String url = rh.ContainsKey("p") ? EStrings.valueOf(rh.Get("p")) : String.Empty;

            //Logger.Print(v.Url.AbsoluteUri);
            //Logger.Print(v.Url.Host);

            return (v.HttpMethod == "POST" || v.Url.AbsoluteUri.IndexOf(v.Url.Host + "/api/")>0) ;
        }


    }
}