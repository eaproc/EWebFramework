using EPRO.Library;
using System;
using System.IO;

namespace EWebFramework.Vendor.api.services
{
    /// <summary>
    /// Allow to create a disposable temporary file
    /// </summary>
    public class TemporaryFolder : IDisposable
    {
        /// <summary>
        /// The file full path that was simulated. Not created on constructor
        /// </summary>
        public readonly string FullPath;

        /// <summary>
        /// Create a temporary folder using your own full path
        /// </summary>
        public TemporaryFolder(string FullPath)
        {
            this.FullPath = FullPath;
        }


        /// <summary>
        /// Create a temporary folder. Note the folder is not physically created
        /// </summary>
        public TemporaryFolder():this(BaseClientService.GetSessionTempFileName() )
        {}


        /// <summary>
        /// This will create the directory if it doesn't exist
        /// </summary>
        /// <returns></returns>
        public TemporaryFolder Create()
        {
            if (!Directory.Exists(this.FullPath)) Directory.CreateDirectory(this.FullPath);
            return this;
        }



        /// <summary>
        /// Delete the Directory if it exists
        /// </summary>
        public void Dispose()
        {
            EIO.DeleteDirectory(this.FullPath);
        }
    }
}