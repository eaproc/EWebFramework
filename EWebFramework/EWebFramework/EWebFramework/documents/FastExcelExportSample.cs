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
    public class FastExcelExportSample : NPOIGridExportGenericBlank<(string StudentNumber, string FirstName, string LastName, string Class)>
    {
        public FastExcelExportSample(DataTable data) : base(data.AsEnumerable().Select(
            (x) => (
                        StudentNumber: x.Field<string>("StudentNumber"),
                        Class: x.Field<string>("Class"),
                        FirstName: x.Field<string>("FirstName"),
                        LastName: x.Field<string>("LastName")
                )
            )
         )
        {
        }

        protected override string[] GetColumnNames()
        {
            return new string[] { "Student Number", "First Name", "Last Name", "Class" };
        }

        public override string ExportTo()
        {
            // This should be made protected
            return this.ExportTo(IsXLS: false, FillHeaderNames: true);
        }



        protected override void SetValues(IRow row, (string StudentNumber, string FirstName, string LastName, string Class) data)
        {
            row.SetCellValueX(0, data.StudentNumber);
            row.SetCellValueX(1, data.FirstName);
            row.SetCellValueX(2, data.LastName);
            row.SetCellValueX(3, data.Class);
        }
    }

}