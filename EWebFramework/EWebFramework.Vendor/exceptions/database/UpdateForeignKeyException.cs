using EPRO.Library.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EWebFramework.Vendor.exceptions.database
{
    /// <summary>
    /// Indicates the foreign key record doesn't exist
    /// </summary>
    public class UpdateForeignKeyException : Exception
    {
        public String ViolatedConstraint;
        public String ViolatedTable;

        public UpdateForeignKeyException(
            String pViolatedConstraint,
            String pViolatedTable,
            Exception exception = null) :base(message: "There is a wrong reference record!",
            innerException: exception)
        {
            this.ViolatedConstraint = pViolatedConstraint;
            this.ViolatedTable = pViolatedTable;
        }





        public static void DetectAndThrow(Exception exception)
        {
            UpdateForeignKeyException d = Detect(exception);
            if (d != null) throw d;
        }

        public static UpdateForeignKeyException Detect(Exception exception)
        {
            String s = exception.Message;
            if (s.IndexOf("conflicted with the FOREIGN KEY") <0) return null;

            String sVoilatedConstraint = DuplicateRowException.ExtractString(s, key1: "constraint \"", key2: "\""
                );

            String sViolatedTable = DuplicateRowException.ExtractString(s, key1: "table \"", key2: "\""
                );


            return new UpdateForeignKeyException(pViolatedConstraint: sVoilatedConstraint,
                pViolatedTable: sViolatedTable, exception: exception
                );

        }







    }
}