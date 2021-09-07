using EPRO.Library;
using System;

namespace EWebFramework.Vendor.api.services
{
    /// <summary>
    /// Allow to create a disposable temporary file
    /// </summary>
    public class TemporaryFile:IDisposable
    {
        /// <summary>
        /// The file full path that was simulated. Not created on constructor
        /// </summary>
        public readonly string FileFullPath;

        /// <summary>
        /// Create a temporary file using your own file full path
        /// </summary>
        public TemporaryFile(string FileFullPath)
        {
            this.FileFullPath = FileFullPath;
        }


        /// <summary>
        /// Create a temporary file. Note the file is not physically created
        /// </summary>
        /// <param name="ExtWithDot">Just additional string to append</param>
        /// <param name="appendRandom">If you want to generate the randomString Part with it</param>
        public TemporaryFile( string ExtWithDot = "", bool appendRandom = true):this(BaseClientService.GetSessionTempFileName(pExtWithDot: ExtWithDot, appendRandom: appendRandom) )
        {}


        /// <summary>
        /// Delete the file if it exists
        /// </summary>
        public void Dispose()
        {
            EIO.DeleteFileIfExists(this.FileFullPath);
        }
    }
}