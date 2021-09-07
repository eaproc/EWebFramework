using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using EPRO.Library;
using EPRO.Library.Objects;
using EWebFramework.Vendor.api.utils.DataExports.Excel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace EWebFramework.Vendor.api.utils.DataExports.Excel
{
    /// <summary>
    /// Allows the exporting of fully customized class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NPOIFullRowsExporter<T>
    {

        /// <summary>
        /// List of Rows to be exported
        /// </summary>
        protected readonly List<T> itemRows;

        /// <summary>
        /// Initialize the Exporter with a List of your own Data Type
        /// </summary>
        /// <param name="ts"></param>
        public NPOIFullRowsExporter(IEnumerable<T> ts) {
            if (ts == null) throw new InvalidOperationException("You can't pass null list");
            this.itemRows = ts.ToList();

            this.StartAtRowIndex = 0;
        }



        /// <summary>
        /// The row to start row data population from
        /// </summary>
        protected  int StartAtRowIndex ;

        /// <summary>
        /// Specify the location to the template to be used for this export
        /// </summary>
        protected abstract string TemplateFileFullPath { get;  }

        /// <summary>
        /// Allows you to perform any extra customization after all rows and data has been populated
        /// </summary>
        /// <param name="sheet1"></param>
        public virtual void DoSomeCustomization(ISheet sheet1)
        {
        }

        /// <summary>
        /// return custom headers here if necessary
        /// </summary>
        /// <returns></returns>
        public abstract string[] GetColumnNames();

        /// <summary>
        /// use [sheet1.AutoSizeColumn()] to fit a column
        /// </summary>
        /// <param name="row"></param>
        /// <param name="data"></param>
        protected abstract void SetValues(IRow row, T data);



        /// <summary>
        /// Supports XLS and XLSX. 
        /// </summary>
        /// <param name="FilePath">Destination file full path</param>
        /// <param name="IsXLS">TRUE WHEN USING XLS and FALSE WHEN USING XLSX FILE</param>
        /// <param name="FillHeaderNames">TRUE IF YOU WANT TO CALL COLUMN NAMES FOR AUTO FILLING HEADER NAMES</param>
        /// <param name="HeaderRowIndex">ROW INDEX OF THE COLUMN HEADER IS STARTING FROM</param>
        /// <param name="HeaderColumnIndex">COLUMN INDEX WHICH SHOULD MATCH LEFT WHERE THE DATA STARTS FROM</param>
        public void ExportTo(string FilePath, bool IsXLS =true, bool FillHeaderNames = true, int HeaderRowIndex=0, int HeaderColumnIndex = 0)
        {
            EIO.DeleteFileIfExists(_FileName: FilePath);
            if (!Directory.Exists(EIO.getDirectoryFullPath(FilePath))) System.IO.Directory.CreateDirectory(EIO.getDirectoryFullPath(FilePath));



            String InnerTempFilePath = String.Format("{0}/2{1}", EIO.getDirectoryFullPath(FilePath), EIO.getFileName(FilePath));
            //Take a copy of the Template FIle
            File.Copy(this.TemplateFileFullPath, InnerTempFilePath, true);




            IWorkbook workbook;

            // Use it for generating the file
            using (FileStream file = new FileStream(InnerTempFilePath, FileMode.Open, FileAccess.Read))
            {
                if(IsXLS)
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
             
                IRow row =  sheet1.GetOrCreate(currentRowIndex);

                this.SetValues(row, d);

                currentRowIndex++;
            }


            this.DoSomeCustomization(sheet1); // after all entries, Send the sheet to the user to do any last customization.

            using (var fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);

            }
        }

    }
}