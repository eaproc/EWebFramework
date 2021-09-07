using EPRO.Library.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EWebFramework.Vendor.exceptions.database
{
    /// <summary>
    /// Indicates the URL you are trying to open is invalid
    /// </summary>
    public class DuplicateRowException : Exception
    {
        public String ViolatedConstraint;
        public String ViolatedTable;
        public String DuplicatedValue;

        public DuplicateRowException(
            String pViolatedConstraint,
            String pViolatedTable,
            String pDuplicatedValue,
            Exception exception = null) :base(message: String.Format( "This value ({0}) will cause a duplicate record!", pDuplicatedValue),
            innerException: exception)
        {
            this.ViolatedConstraint = pViolatedConstraint;
            this.ViolatedTable = pViolatedTable;
            this.DuplicatedValue = pDuplicatedValue;
        }





        public static void DetectAndThrow(Exception exception)
        {
            DuplicateRowException d = Detect(exception);
            if (d != null) throw d;
        }

        public static DuplicateRowException Detect(Exception exception)
        {
            String s = exception.Message;
            if (s.IndexOf("Violation of UNIQUE KEY constraint")<0 && s.IndexOf("Cannot insert duplicate key row")<0 ) return null;

            bool IsFirstMethod = s.IndexOf("Violation of UNIQUE KEY constraint")>=0;

            String sVoilatedConstraint = IsFirstMethod ?
                ExtractString(s, key1: "Violation of UNIQUE KEY constraint '", key2: "'") :
                ExtractString(s, key1: "with unique index '", key2: "'");


            String sViolatedTable = IsFirstMethod ? 
                ExtractString(s, key1: "Cannot insert duplicate key in object '", key2: "'"):
                ExtractString(s, key1: "key row in object '", key2: "'");

            String sDuplicateValue = IsFirstMethod ? 
                ExtractString(s, key1: "The duplicate key value is (", key2: ")" ):
                ExtractString(s, key1: "The duplicate key value is (", key2: ")");


            return new DuplicateRowException(pViolatedConstraint: sVoilatedConstraint,
                pViolatedTable: sViolatedTable, pDuplicatedValue: sDuplicateValue, exception: exception
                );

        }



        public static String ExtractString(String originalString, String key1, String key2)
        {
            if (originalString.IndexOf(key1) == -1) return String.Empty;
            int keyword1StartIndex = originalString.IndexOf(key1) + key1.Length;
            int keyword1StopIndex = originalString.IndexOf(key2, keyword1StartIndex);
            String keywordValue = originalString.Substring(keyword1StartIndex, keyword1StopIndex - keyword1StartIndex);
            return keywordValue;
        }




    }
}