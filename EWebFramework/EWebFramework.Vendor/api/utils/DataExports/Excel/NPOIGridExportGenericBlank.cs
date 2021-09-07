using EPRO.Library;
using EWebFramework.Vendor.api.services;
using EWebFramework.Vendor.api.utils.DataExports.Excel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.utils.DataExports.Excel
{

    /// <summary>
    /// This class uses NPOI library to run your export using your own generic.
    ///  where T:class restrict it to reference types only
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NPOIGridExportGenericBlank< T > 
    {



        /// <summary>
        /// List of Rows to be exported
        /// </summary>
        protected readonly List<T> itemRows;

        /// <summary>
        /// Initialize the Exporter with a List of your own Data Type
        /// </summary>
        /// <param name="ts"></param>
        public NPOIGridExportGenericBlank(IEnumerable<T> ts)
        {
            if (ts == null) throw new InvalidOperationException("You can't pass null list");
            this.itemRows = ts.ToList();

        }



        /// <summary>
        /// The row to start row data population from
        /// </summary>
        private int StartAtRowIndex ;


        /// <summary>
        /// Allows you to perform any extra customization after all rows and data has been populated
        /// </summary>
        /// <param name="sheet1"></param>
        protected virtual void DoSomeCustomization(ISheet sheet1)
        {
        }


        /// <summary>
        /// return custom headers here if necessary
        /// </summary>
        /// <returns></returns>
        protected virtual string[] GetColumnNames()
        {
            return new string[] { };
        }


        /// <summary>
        /// use [sheet1.AutoSizeColumn()] to fit a column
        /// </summary>
        /// <param name="row"></param>
        /// <param name="data"></param>
        protected abstract void SetValues(IRow row, T data);


        public abstract string ExportTo();



        /// <summary>
        /// Supports XLS and XLSX. 
        /// </summary>
        /// <param name="IsXLS">TRUE WHEN USING XLS and FALSE WHEN USING XLSX FILE</param>
        /// <param name="FillHeaderNames">TRUE IF YOU WANT TO CALL COLUMN NAMES FOR AUTO FILLING HEADER NAMES</param>
        /// <returns>Destination file full path</returns>
        protected string ExportTo(bool IsXLS , bool FillHeaderNames)
        {
            int HeaderRowIndex = 0, HeaderColumnIndex = 0;

            string FilePath = BaseClientService.GetSessionTempFileName( pExtWithDot: IsXLS ? ".xls" : ".xlsx" );

            EIO.DeleteFileIfExists(_FileName: FilePath);
            if (!Directory.Exists(EIO.getDirectoryFullPath(FilePath))) System.IO.Directory.CreateDirectory(EIO.getDirectoryFullPath(FilePath));


            IWorkbook workbook;

            // Use it for generating the file
            if (IsXLS)
                workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(); // HSSFWorkbook is for .xls file while XSSFWorkbook is for .xlsx file
            else
                workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(); // HSSFWorkbook is for .xls file while XSSFWorkbook is for .xlsx file



            ISheet sheet1 = workbook.CreateSheet();// use first index

            if (FillHeaderNames)
            {
                //make a header row
                IRow row1 = sheet1.GetOrCreate(HeaderRowIndex);

                int j = HeaderColumnIndex;
                foreach (string s in this.GetColumnNames())
                {
                    ICell cell = row1.GetOrCreate(j);
                    cell.SetCellValue(s);
                    if (IsXLS)
                        SetHighlightedXLS(cell);
                    else
                        SetHighlighted(cell);
                    j++;
                }

            }


            this.StartAtRowIndex = FillHeaderNames ? 1 : 0;


            int currentRowIndex = this.StartAtRowIndex; // currentRowIndex

            foreach (T d in this.itemRows)
            {

                //loops through data

                IRow row = sheet1.GetOrCreate(currentRowIndex);

                this.SetValues(row, d);

                currentRowIndex++;
            }


            this.DoSomeCustomization(sheet1); // after all entries, Send the sheet to the user to do any last customization.

            using (var fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);

            }


            return FilePath;
        }




        private ICell SetHighlighted(ICell cell)
        {

            // create bordered cell style
            XSSFCellStyle borderedCellStyle = (XSSFCellStyle)cell.Sheet.Workbook.CreateCellStyle();
            borderedCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
            borderedCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
            borderedCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
            borderedCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;

            //borderedCellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            borderedCellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            borderedCellStyle.FillPattern = FillPattern.SolidForeground;


            cell.CellStyle = borderedCellStyle; 

            return cell;
        }


        private ICell SetHighlightedXLS(ICell cell)
        {

            // create bordered cell style
            HSSFCellStyle borderedCellStyle = (HSSFCellStyle)cell.Sheet.Workbook.CreateCellStyle();
            borderedCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
            borderedCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
            borderedCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
            borderedCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;

            //borderedCellStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            borderedCellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            borderedCellStyle.FillPattern = FillPattern.SolidForeground;


            cell.CellStyle = borderedCellStyle;

            return cell;
        }


    }
}