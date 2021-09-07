
using DB.Abstracts;

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPRO.Library.Objects;
using EWebFramework.Vendor.api.utils;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.api.services.resources.persons
{
    public class ImportedStudents:ClientService
    {

        public ImportedStudents(SessionHelper sessionHelper) : base(sessionHelper)
        {

        }





        /// <summary>
        /// Fetches the normal file if it exists, else it returns empty string
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string Fetch(string FileName)
        {
            String requestedFile = ResourcesPath(RelativeFilePath_ImportedStudents(FileName));
            return System.IO.File.Exists(requestedFile) ? requestedFile : String.Empty;
        }


        /// <summary>
        /// Fetches the normal file if it exists, else it returns empty string
        /// </summary>
        /// <returns></returns>
        public string FetchTemplate()
        {
            String requestedFile = ResourcesPath(RelativeFilePath_ImportedStudents("import_students_template.xlsx"));
            return requestedFile;
        }



        public bool Store(string DocumentFileName, HttpPostedFile File__DOCUMENT)
        {
            var pDst = ResourcesPath(RelativeFilePath_ImportedStudents(StoredFileName: DocumentFileName));
            return StoreFile(pDst, pUploadedFile: File__DOCUMENT);
        }


        public string RelativeFilePath_ImportedStudents(String StoredFileName)
        {
            return String.Format(@"{0}\ImportedStudents\{1}", RelativeFilePath_Persons(), StoredFileName);
        }







    }
}