using EPRO.Library;
using EWebFramework.Vendor.api.services;
using EWebFramework.Vendor.api.utils.DataExports.Excel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.utils.DataExports.Excel
{

    /// <summary>
    /// This class uses NPOI library to run your export using your own generic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NPOIGridExportGeneric< T > where T:class
    {



        /// <summary>
        /// List of Rows to be exported
        /// </summary>
        protected readonly List<T> itemRows;

        /// <summary>
        /// Initialize the Exporter with a List of your own Data Type
        /// </summary>
        /// <param name="ts"></param>
        public NPOIGridExportGeneric(IEnumerable<T> ts)
        {
            if (ts == null) throw new InvalidOperationException("You can't pass null list");
            this.itemRows = ts.ToList();

        }



        /// <summary>
        /// The row to start row data population from
        /// </summary>
        protected abstract int StartAtRowIndex { get; }

        /// <summary>
        /// Specify the location to the template to be used for this export
        /// </summary>
        protected abstract string TemplateFileFullPath { get; }

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
        /// <param name="HeaderRowIndex">ROW INDEX OF THE COLUMN HEADER IS STARTING FROM</param>
        /// <param name="HeaderColumnIndex">COLUMN INDEX WHICH SHOULD MATCH LEFT WHERE THE DATA STARTS FROM</param>
        /// <returns>Destination file full path</returns>
        protected string ExportTo(bool IsXLS , bool FillHeaderNames, int HeaderRowIndex = 0, int HeaderColumnIndex = 0)
        {
            string FilePath = BaseClientService.GetSessionTempFileName();

            EIO.DeleteFileIfExists(_FileName: FilePath);
            if (!Directory.Exists(EIO.getDirectoryFullPath(FilePath))) System.IO.Directory.CreateDirectory(EIO.getDirectoryFullPath(FilePath));



            String InnerTempFilePath = String.Format("{0}/2{1}", EIO.getDirectoryFullPath(FilePath), EIO.getFileName(FilePath));
            //Take a copy of the Template FIle
            File.Copy(this.TemplateFileFullPath, InnerTempFilePath, true);




            IWorkbook workbook;

            // Use it for generating the file
            using (FileStream file = new FileStream(InnerTempFilePath, FileMode.Open, FileAccess.Read))
            {
                if (IsXLS)
                    workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file); // HSSFWorkbook is for .xls file while XSSFWorkbook is for .xlsx file
                else
                    workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(file); // HSSFWorkbook is for .xls file while XSSFWorkbook is for .xlsx file
            }



            ISheet sheet1 = workbook.GetSheetAt(0);// use first index

            if (FillHeaderNames)
            {
                //make a header row
                IRow row1 = sheet1.GetRow(HeaderRowIndex);

                int j = HeaderColumnIndex;
                foreach (string s in this.GetColumnNames())
                {
                    ICell cell = row1.GetOrCreate(j);
                    cell.SetCellValue(s);
                    j++;
                }

            }




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




    }
}