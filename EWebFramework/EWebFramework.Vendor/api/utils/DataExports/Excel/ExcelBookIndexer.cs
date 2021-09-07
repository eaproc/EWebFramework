using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.utils.DataExports.Excel
{
    public static class ExcelBookIndexer
    {

        private readonly static List<String> Pool;

        static ExcelBookIndexer()
        {
            var Az = Enumerable.Range(97, 26).Select(x => ((char)x).ToString()).ToList();
            //var b = ( 
            //    from y in Az
            //            select Enumerable.Range(97, 26).Select(x => y + ((char)x).ToString())
            //            ).ToArray();

            //foreach (var s in b) Az = Az.Concat(Az);
            var l = Az.ToArray();
            foreach (var x in l)
                foreach (var y in l)
                    Az.Add(x + y);

                    Pool = new List<string>(Az);
            
            
        }

        public struct Location
        {
            public Location(int pRow, int pColumn, bool pZeroBased = true) { Row = pRow; Column = pColumn; this.ZeroBased = pZeroBased; }
            public readonly int Column, Row;
            public bool ZeroBased;
        }


        public static string ToExcelFormat(this Location location)
        {
            return location.ZeroBased? Pool[location.Column] + location.Row+1 : Pool[location.Column-1] + location.Row;
        }


        /// <summary>
        /// Translates Excel Notification into Location Coordinates
        /// </summary>
        /// <param name="ExcelFormat"></param>
        /// <param name="ZeroBased"></param>
        /// <returns></returns>
        public static Location Cell(String ExcelFormat, bool ZeroBased = true)
        {
            try
            {
                var intPart = EPRO.Library.InputsParsing.TextParsing.parseOutDoubleAsString(ExcelFormat);
                var strPart = ExcelFormat.Substring(0,ExcelFormat.Length - intPart.Length ).ToLower() ;
                if (!Pool.Contains(strPart)) throw new Exception("Can't locate this part in the pool: " + strPart);
                int row = int.Parse(intPart);
                int column = Pool.IndexOf(strPart) + 1;
                if (row <= 0) throw new Exception("The integer part must be minimum of 1");
                return ZeroBased ? new Location(row - 1, column - 1) : new Location(row, column, false);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The excel format is not understandable or not Suppoted. Maximum A to ZZ with Number 1 above", ex);
            }
        }







        /// <summary>
        /// Returns Null or ICell
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="ExcelFormat"></param>
        /// <returns></returns>
        public static NPOI.SS.UserModel.ICell CellV(this NPOI.SS.UserModel.ISheet sheet, String ExcelFormat)
        {
            Location location = Cell(ExcelFormat: ExcelFormat);
            var r = sheet.GetRow(location.Row);
            return r!=null ? r.GetCell(location.Column) : null;
        }


        /// <summary>
        /// Sets the value in the excel coordinate and throws Exception if it is not writable
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="ExcelFormat"></param>
        /// <param name="value"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void SetCellValue(this NPOI.SS.UserModel.ISheet sheet, String ExcelFormat, string value)
        {
            var c = sheet.CellV(ExcelFormat);
            if (c == null) throw new InvalidOperationException(string.Format("The excel sheet provided doesn't have this cell [{0}] as writable cell!!", ExcelFormat));
            c.SetCellValue(value);
        }


        /// <summary>
        /// Sets the value in the excel coordinate if it is writable else it ignores it
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="ExcelFormat"></param>
        /// <param name="value"></param>
        public static void SetCellValueIf(this NPOI.SS.UserModel.ISheet sheet, String ExcelFormat, string value)
        {
            var c = sheet.CellV(ExcelFormat);
            if (c == null) return;
            c.SetCellValue(value);
        }




        /// <summary>
        /// Create ICell or Gets it if it already exists
        /// </summary>
        /// <returns></returns>
        public static NPOI.SS.UserModel.ICell GetOrCreate(this NPOI.SS.UserModel.IRow row, int index)
        {
            var c = row.GetCell(index);
            return c ?? row.CreateCell(index);
        }

        /// <summary>
        /// Create IRow or Gets it if it already exists
        /// </summary>
        /// <returns></returns>
        public static NPOI.SS.UserModel.IRow GetOrCreate(this NPOI.SS.UserModel.ISheet sheet, int index)
        {
            var c = sheet.GetRow(index);
            return c ?? sheet.CreateRow(index);
        }




























    }
}