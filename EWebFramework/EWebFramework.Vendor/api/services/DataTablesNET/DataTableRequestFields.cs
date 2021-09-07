using EPRO.Library.Objects;
using System;
using EWebFramework.Vendor.api.utils;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor.api.services.DataTablesNET
{
    public class DataTableRequestFields
    {
        public int Start;
        public int Length;
        public int Draw;
        public int OrderByColumnIndex;
        public String OrderByColumnDirection;
        public String OrderByColumnName;
        public bool SortUsingColumnName;
        public String SearchValue;

        // date will be read in this order only
        // yyyy-MM-dd
        public const string DATE_FORMAT = "yyyy-MM-dd";
        /// <summary>
        /// Time will be read in this order. Hours:Mins 24Hr
        /// </summary>
        public const string TIME_FORMAT = "HH:mm";

        public DateTime? StartDate, EndDate;


        // time will be accepted in 24-hrs only
        // HH:mm



        public static DateTime? ParseDate(String pValue)
        {

            String[] pParts = pValue.Split('-');

            try
            {
                if (pParts.Length == 3 &&
                pParts[0].Length == 4 &&
                pParts[1].Length == 2 &&
                pParts[2].Length == 2 &&
                (EInt.valueOf(pParts[0]) > 0 && EInt.valueOf(pParts[0]) < 4000) &&
                (EInt.valueOf(pParts[1]) > 0 && EInt.valueOf(pParts[1]) < 13) &&
                (EInt.valueOf(pParts[2]) > 0 && EInt.valueOf(pParts[2]) < 32)
                )
                {
                    int pYr = EInt.valueOf(pParts[0]), pMonth = EInt.valueOf(pParts[1]), pDay = EInt.valueOf(pParts[2]);
                    return new DateTime(pYr, pMonth, pDay);     //this may throw exception

                }

            }
            catch (Exception e)
            { Logger.Print(e); }


            // Logger.Print(String.Format("value: {0}, LengthSplit: {1}", pValue, pParts.Length));
            return null;    //Invalid Date

        }

        public static DateTime? ParseTime(object pValue)
        {

            if (pValue == null || !(pValue is string)) return null;

            String[] pParts = pValue.ToString().Split(':');

            try
            {
                if (pParts.Length == 2 &&
                pParts[0].Length == 2 &&
                pParts[1].Length == 2 &&
                (EInt.valueOf(pParts[0]) >= 0 && EInt.valueOf(pParts[0]) < 24) &&
                (EInt.valueOf(pParts[1]) >= 0 && EInt.valueOf(pParts[1]) < 60)
                )
                {
                    int pHr = EInt.valueOf(pParts[0]), pMin = EInt.valueOf(pParts[1]);
                    return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, pHr, pMin, 0);     //this may throw exception

                }

            }
            catch (Exception e)
            { Logger.Print(e); }


            // Logger.Print(String.Format("value: {0}, LengthSplit: {1}", pValue, pParts.Length));
            return null;    //Invalid Time

        }



        public static DataTableRequestFields ProcessDataTableRequestFields(RequestHelper requestHelper)
        {
            return new DataTableRequestFields
            {
                Start = Convert.ToInt32(requestHelper.Get("start")),
                Length = Convert.ToInt32(requestHelper.Get("length")),
                Draw = Convert.ToInt32(requestHelper.Get("draw")),
                OrderByColumnIndex = Convert.ToInt32(requestHelper.Get("order[0][column]")),
                OrderByColumnDirection = requestHelper.Get("order[0][dir]").ToString(),
                OrderByColumnName = requestHelper.ContainsKey("order[0][name]") ? requestHelper.Get("order[0][name]").ToString() : String.Empty,
                SortUsingColumnName = requestHelper.ContainsKey("SortUsingColumnName") ? EBoolean.valueOf(requestHelper.Get("SortUsingColumnName")) : false,
                SearchValue = requestHelper.ContainsKey("search[value]") ? requestHelper.Get("search[value]").ToString() : "",
                StartDate = requestHelper.ContainsKey("start_date") ? DataTableRequestFields.ParseDate(requestHelper.Get("start_date").ToString()) : null,
                EndDate = requestHelper.ContainsKey("end_date") ? DataTableRequestFields.ParseDate(requestHelper.Get("end_date").ToString()) : null
            };
        }


        public static readonly RequestValidator.Rule[] DataTableRequiredFields = {
                new RequestValidator.Rule("draw", true,  0, 1000, RequestValidator.Rule.ParamTypes.INTEGER),
                new RequestValidator.Rule("start", true, 0, int.MaxValue, RequestValidator.Rule.ParamTypes.INTEGER),
                new RequestValidator.Rule("length", true, 0, int.MaxValue, RequestValidator.Rule.ParamTypes.INTEGER),
                new RequestValidator.Rule("order[0][column]", true, 0, 30, RequestValidator.Rule.ParamTypes.INTEGER),
                new RequestValidator.Rule("order[0][name]", false, 0, int.MaxValue, RequestValidator.Rule.ParamTypes.STRING),
                new RequestValidator.Rule("order[0][dir]", true, 0, int.MaxValue, RequestValidator.Rule.ParamTypes.STRING),
                new RequestValidator.Rule("SortUsingColumnName]", false, 0, 50, RequestValidator.Rule.ParamTypes.BOOLEAN),
        };



        public static readonly RequestValidator.Rule[] DataTableRequiredFieldsWithDateRange ={
                new RequestValidator.Rule("draw", true,  0, 1000, RequestValidator.Rule.ParamTypes.INTEGER),
                new RequestValidator.Rule("start", true, 0, int.MaxValue, RequestValidator.Rule.ParamTypes.INTEGER),
                new RequestValidator.Rule("length", true, 0, int.MaxValue, RequestValidator.Rule.ParamTypes.INTEGER),
                new RequestValidator.Rule("order[0][column]", true, 0, 30, RequestValidator.Rule.ParamTypes.INTEGER),
                  new RequestValidator.Rule("order[0][name]", false, 0, int.MaxValue, RequestValidator.Rule.ParamTypes.STRING),
                new RequestValidator.Rule("order[0][dir]", true, 0, int.MaxValue, RequestValidator.Rule.ParamTypes.STRING),
                  new RequestValidator.Rule("SortUsingColumnName]", false, 0, 50, RequestValidator.Rule.ParamTypes.BOOLEAN),
                new RequestValidator.Rule("start_date", true, 0, 1000, RequestValidator.Rule.ParamTypes.DATE),
                new RequestValidator.Rule("end_date", true, 0, 1000, RequestValidator.Rule.ParamTypes.DATE)
        };





    }

}