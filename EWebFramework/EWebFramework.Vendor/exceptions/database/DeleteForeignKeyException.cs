using EPRO.Library.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EWebFramework.Vendor.exceptions.database
{
    /// <summary>
    /// Explains the row you are trying to delete has some dependent records
    /// </summary>
    public class DeleteForeignKeyException : Exception
    {
        public String ViolatedConstraint;
        public String ViolatedTable;
        public String DependentColumn;

        public const string WARNING_MSG = "Some other records are referencing this record, please delete them first!";

        public DeleteForeignKeyException(
            String pViolatedConstraint,
            String pViolatedTable,
            String pDependentColumn,
            Exception exception = null) :base(message: "This record has some dependent records!",
            innerException: exception)
        {
            this.ViolatedConstraint = pViolatedConstraint;
            this.ViolatedTable = pViolatedTable;
            this.DependentColumn = pDependentColumn;
        }





        public static void DetectAndThrow(Exception exception)
        {
            DeleteForeignKeyException d = Detect(exception);
            if (d != null) throw d;
        }

        public static DeleteForeignKeyException Detect(Exception exception)
        {
            String s = exception.Message;
            if (s.IndexOf("The DELETE statement conflicted") <0) return null;

            String sVoilatedConstraint = DuplicateRowException.ExtractString(s, key1: "constraint \"", key2: "\""
                );

            String sViolatedTable = DuplicateRowException.ExtractString(s, key1: "table \"", key2: "\""
                );

            String sDependentColumn = DuplicateRowException.ExtractString(s, key1: "column '", key2: "'"
               );


            return new DeleteForeignKeyException(pViolatedConstraint: sVoilatedConstraint,
                pViolatedTable: sViolatedTable,pDependentColumn: sDependentColumn, exception: exception
                );

        }







    }
}