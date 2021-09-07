using System;
using System.Collections.Generic;
using System.Linq;

namespace EWebFramework.Vendor.exceptions
{
    /// <summary>
    /// Indicates the file you are trying to download does not exist
    /// </summary>
    public class FileNotFoundOnServerException : Exception
    {
        public FileNotFoundOnServerException(String FileName) :base(message:String.Format("FILE ({0}) DOES NOT EXIST ON SCADWARE!", FileName))
        {

        }
    }
}