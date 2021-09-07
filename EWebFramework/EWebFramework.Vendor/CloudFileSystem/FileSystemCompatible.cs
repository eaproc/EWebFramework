using EPRO.Library;
using EWebFramework;
using EWebFramework.Vendor.api.services;
using EWebFramework.Vendor.api.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWebFramework.Vendor.CloudFileSystem
{
    /// <summary>
    /// You can override features in this class. 
    /// However, it is meant to generalize the usage of filesystem switching
    /// PS: ObjectPath is FilePath in this Cloud storage location
    /// </summary>
    public abstract class FileSystemCompatible
    {




        /// <summary>
        /// It will return public accessible path for the file. Provided Publicity is set
        /// </summary>
        /// <param name="ObjectPath">FileName - Full relative path to the root container</param>
        /// <param name="Attachment">If attachment, downloadable else inline disposition</param>
        /// <param name="DownloadAsFileName"></param>
        /// <param name="ContentType"></param>
        /// <param name="ExpiryTimeInSeconds">Time for the file in the path return to expire. 1hr</param>
        /// <returns></returns>
        public virtual string CreateTemporaryURL(string ObjectPath, bool Attachment, string DownloadAsFileName,
            string ContentType, int ExpiryTimeInSeconds)
        {
            return ObjectPath;
        }

        /// <summary>
        /// It will return public accessible path for the file. Provided Publicity is set
        /// </summary>
        /// <param name="ObjectPath">FileName - Full relative path to the root container</param>
        /// <param name="Attachment">If attachment, downloadable else inline disposition</param>
        /// <param name="DownloadAsFileName"></param>
        /// <returns></returns>
        public virtual string CreateTemporaryURL(string ObjectPath, bool Attachment , string DownloadAsFileName)
        {
            return CreateTemporaryURL(
                 ObjectPath: ObjectPath,
                 Attachment: Attachment,
                 DownloadAsFileName: DownloadAsFileName,
                 ContentType: System.Web.MimeMapping.GetMimeMapping(EIO.getFileName(ObjectPath)),
                  ExpiryTimeInSeconds: 3600 // 1 hr
             );
        }


        /// <summary>
        /// It will return public accessible path for the file. Provided Publicity is set
        /// </summary>
        /// <param name="ObjectPath">FileName - Full relative path to the root container</param>
        /// <param name="Attachment">If attachment, downloadable else inline disposition</param>
        /// <returns></returns>
        public virtual string CreateTemporaryURL(string ObjectPath, bool Attachment)
        {
            return CreateTemporaryURL(
                ObjectPath: ObjectPath,
                Attachment: Attachment,
                DownloadAsFileName: BaseClientService.GetArbitraryValidFileName( EIO.getFileName(ObjectPath) )
            );
        }



        /// <summary>
        /// It will return public accessible path for the file. Provided Publicity is set
        /// </summary>
        /// <param name="ObjectPath">FileName - Full relative path to the root container</param>
        /// <returns></returns>
        public virtual string CreateTemporaryURL(string ObjectPath)
        {
            return CreateTemporaryURL(
                ObjectPath: ObjectPath,
                Attachment:true
            );
        }






        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <returns></returns>
        public abstract TemporaryFile GetFileAsDownloaded(string ObjectPath);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <param name="cleanUp"></param>
        /// <returns></returns>
        public virtual string GetFileAsString(string ObjectPath, bool cleanUp = true)
        {
            byte[] buffer = this.GetFileAsBinary(ObjectPath: ObjectPath, cleanUp: cleanUp);
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <param name="cleanUp"></param>
        /// <returns></returns>
        public virtual byte[] GetFileAsBinary(string ObjectPath, bool cleanUp = true)
        {
            try
            {

                TemporaryFile DestinationFileFullPath = this.GetFileAsDownloaded(ObjectPath: ObjectPath);
                byte[] b = File.ReadAllBytes(DestinationFileFullPath.FileFullPath);
                if (cleanUp) DestinationFileFullPath.Dispose();  // clean up

                return b;

            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unfortunately the file could not be downloaded!", e);
            }
        }

        /// <summary>
        /// Storage Exists
        /// </summary>
        /// <returns></returns>
        public abstract bool StorageExist();
        /// <summary>
        /// Save File
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <param name="FileFullPath"></param>
        public abstract void SaveFile(string ObjectPath, string FileFullPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <param name="Contents"></param>
        public virtual void SaveFileContent(string ObjectPath, byte[] Contents)
        {
            using (TemporaryFile tf = new TemporaryFile())
            {
                File.WriteAllBytes(tf.FileFullPath, Contents);
                this.SaveFile(ObjectPath: ObjectPath, FileFullPath: tf.FileFullPath);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <param name="Contents"></param>
        public virtual void SaveFileContent(string ObjectPath, string Contents)
        {
            this.SaveFileContent(ObjectPath: ObjectPath, Contents: Encoding.UTF8.GetBytes(Contents));

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <param name="Contents"></param>
        public virtual void SaveFileContent(string ObjectPath, Stream Contents)
        {
            // Store File
            using (var r = new BinaryReader(Contents))
            {
                SaveFileContent(ObjectPath: ObjectPath, Contents: r.ReadBytes((int)Contents.Length));
            }

        }

        /// <summary>
        /// File Exists
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <returns></returns>
        public abstract bool ObjectExists(string ObjectPath);

        /// <summary>
        /// Delete object
        /// </summary>
        /// <param name="ObjectPath"></param>
        public abstract void DeleteObject(string ObjectPath);


        //public static string GetUniqueFileName(string extWithDot = "")
        //{
        //    Guid g = Guid.NewGuid();
        //    string GuidString = g.ToString();
        //    //string GuidString = Convert.ToBase64String(g.ToByteArray());
        //    //GuidString = GuidString.Replace("=", "");
        //    //GuidString = GuidString.Replace("+", "");
        //    return GuidString + extWithDot;
        //}




        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public static string WritableLocalStorage()
        //{
        //    string s = ENV.General("CLOUD_LOCAL_STORAGE");
        //    return s == null || s == string.Empty ? AppDomain.CurrentDomain.BaseDirectory : s;
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="Path"></param>
        ///// <returns></returns>
        //public static string ToCloudLocalStorage(string Path)
        //{
        //    return WritableLocalStorage() + "/" + Path;
        //}




    }
}
