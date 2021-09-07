using EPRO.Library.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPRO.Library;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using EWebFramework.Vendor.api.utils.DataExports.Excel;
using System.IO;
using EWebFramework.Vendor.api.services;

namespace EWebFramework.Vendor.api.utils.DataExports.Excel
{
    public abstract class IEPPlusExcelExportCustomizer: INPOIExcelExport
    {

        //Indicate where data rows should begin writing
        protected abstract int startAtIndex { get; }

        protected abstract string templateFileFullPath { get;  }

        public IEPPlusExcelExportCustomizer(DataTable dataTable) : base(data: dataTable) { }


        public abstract void doSomeCustomization(ISheet sheet1);


        public virtual void SetCellValue(ICell cell, ushort columnIndex,  object v)
        {
            // Default just sets it as string
            cell.SetCellValue(EStrings.valueOf(v));
        }


        public bool ExportListUsingNPOI(out String pFilePath)
        {
            pFilePath = BaseClientService.GetSessionTempFileName();


            //EIO.DeleteFileIfExists(pFilePath);
            if (!Directory.Exists(EIO.getDirectoryFullPath(pFilePath))) System.IO.Directory.CreateDirectory(EIO.getDirectoryFullPath(pFilePath));
            if (!System.IO.File.Exists(this.templateFileFullPath)) throw new FileNotFoundException("Template File Does not Exists - " + this.templateFileFullPath);




            String pFilePath2 = String.Format("{0}/2{1}", EIO.getDirectoryFullPath(pFilePath), EIO.getFileName(pFilePath));
            //Take a copy of the Template FIle
            File.Copy(this.templateFileFullPath, pFilePath2, true);



            IWorkbook workbook;
            // Use it for generating the file
            workbook = new XSSFWorkbook(pFilePath2);



            int currentRowIndex = this.startAtIndex; // currentRowIndex



            ISheet sheet1 = workbook.GetSheetAt(0); //First Sheet


            //If you need to work on the sheet
            this.doSomeCustomization(sheet1);



            //loops through data
            foreach (var r in this.GetData())
            {
                IRow row = sheet1.CreateRow(currentRowIndex);
                for (int j = 0; j < r.Count; j++)
                {

                    ICell cell = row.CreateCell(j);
                    this.SetCellValue(cell, (ushort)j, r.ElementAt(j).Value);
                }


                currentRowIndex++;

            }




            //It doesn't write if you don't call this
            // #BUG
            using (var fs = new FileStream(pFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                workbook.Write(fs);

            }

            workbook.Close();
            //Clean Up
            EIO.DeleteFileIfExists(pFilePath2);


            return File.Exists(pFilePath);

        }







    }
}
