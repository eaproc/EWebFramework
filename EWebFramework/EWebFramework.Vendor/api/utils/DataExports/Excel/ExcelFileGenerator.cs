using EPRO.Library;
using EPRO.Library.Objects;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.utils.DataExports.Excel
{
    public class ExcelFileGenerator
    {

        // https://github.com/closedxml/closedxml
        //


        //
        //http://epplus.codeplex.com/downloads/get/1591093
        //http://techbrij.com/export-excel-xls-xlsx-asp-net-npoi-epplus
        //



        //
        // http://zeeshanumardotnet.blogspot.com.cy/2011/06/creating-reports-in-excel-2007-using.html
        //


            /// <summary>
            /// This works fine as well. It uses EPPlus Library that is added manually
            /// </summary>
            /// <param name="pFilePath"></param>
            /// <param name="data"></param>
            /// <returns></returns>
        public static bool ExportListUsingEPPlus(String pFilePath, DataTable data)
        {
            EIO.DeleteFileIfExists(_FileName: pFilePath);
            if (!Directory.Exists(EIO.getDirectoryFullPath(pFilePath))) EIO.DeleteAndRecreateDirectory(EIO.getDirectoryFullPath(pFilePath));



            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromDataTable(data, true);
            excel.SaveAs(new FileInfo(pFilePath));

            return File.Exists(pFilePath);

        }



        /// <summary>
        /// This works nice to generate the file and output. It uses NPOI library. Added from Nuggets
        /// </summary>
        /// <param name="pFilePath"></param>
        /// <param name="addHeader"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool ExportListUsingNPOI(String pFilePath, bool addHeader, params INPOIExcelExport[] data)
        {
            EIO.DeleteFileIfExists(_FileName: pFilePath);
            if (!Directory.Exists(EIO.getDirectoryFullPath(pFilePath))) System.IO.Directory.CreateDirectory(EIO.getDirectoryFullPath(pFilePath));


            IWorkbook workbook;
            workbook = new XSSFWorkbook();
           

            int iCount = 1;
            int currentRowIndex = 0; // currentRowIndex


            foreach (INPOIExcelExport d in data)
            {


                ISheet sheet1 = workbook.CreateSheet("Sheet"+iCount);

                if(addHeader)
                {
                    //make a header row
                    IRow row1 = sheet1.CreateRow(0);

                    int j = 0;
                    foreach (String s in d.GetColumnNames())
                    {

                        ICell cell = row1.CreateCell(j);
                        cell.SetCellValue(s);
                        j++;
                    }

                    currentRowIndex++;

                }



                //loops through data
                foreach (var r in d.GetData())
                {
                    IRow row = sheet1.CreateRow(currentRowIndex);
                    for (int j = 0; j < r.Count; j++)
                    {

                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(EStrings.valueOf( r.ElementAt(j).Value ));
                    }


                    currentRowIndex++;

                }



                iCount++;



            }

            using (var fs = new FileStream(pFilePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
               
            }

            return File.Exists(pFilePath);

        }






    }
}