using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using EPRO.Library;
using EPRO.Library.Objects;
using EWebFramework.api.services;
using EWebFramework.Vendor.api.utils.DataExports.Excel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace EWebFramework.documents
{
    public class EvaluateGroupTemplate: IEPPlusExcelExportCustomizer
    {


        public enum ColumnsToImport
        {
            StudentNumber,
            FirstName,
            LastName,
            ScoredRate
            //EvaluatedRate
        }





        public EvaluateGroupTemplate(
            DataTable dataTable):base(dataTable: dataTable) {

        }




        protected override int startAtIndex => 2;

        protected override string templateFileFullPath => ClientService.FetchFromFileStore("templates/instructor/evaluate_course.xlsx");






        public override void doSomeCustomization(ISheet sheet1)
        {
            //Let's Change the content header of Scored Rate And Grade Rate

            //int ScoredRateColumnIndex = (int)ColumnsToImport.ScoredRate;
            //int EvaluatedRateColumnIndex = (int)ColumnsToImport.EvaluatedRate;


            // Start with the Main Header
            sheet1.SetCellValue("B1", "44");



        }


    }
}