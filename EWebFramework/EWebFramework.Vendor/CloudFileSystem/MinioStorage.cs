using EPRO.Library.Objects;
using EWebFramework;
using EWebFramework.Vendor.api.services;
using EWebFramework.Vendor.api.utils;
using Minio;
using Minio.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWebFramework.Vendor.CloudFileSystem
{

    // 3.1.9: Minio
    //
    // Minio require restsharp 106.3.1
    // Therefore if you are not using that exact version
    // update your .config file or Web.config to use app redirect
    // <bindingRedirect oldVersion="0.0.0.0-106.3.1.0" newVersion="106.0.0.0" />




    /// <summary>
    /// Requires installation of Minio nugget
    /// https://docs.min.io/docs/dotnet-client-quickstart-guide.html
    /// </summary>
    public class MinioStorage:FileSystemCompatible
    {

        private readonly MinioClient Minio;
        private readonly string Bucket;

        /// <summary>
        /// Create a Minio storage instance
        /// </summary>
        /// <param name="Endpoint">example.com with no protocol</param>
        /// <param name="AccessKey">AccessKey</param>
        /// <param name="SecretKey">SecretKey</param>
        /// <param name="Bucket">Bucket/Folder</param>
        /// <param name="SSL">Use https or http</param>
        public MinioStorage(string Endpoint, string AccessKey, string SecretKey, string Bucket, bool SSL=true)
        {

             this.Minio = new MinioClient( endpoint: Endpoint,
              accessKey: AccessKey,
              secretKey: SecretKey
              );

            if (SSL) this.Minio = this.Minio.WithSSL();

            this.Bucket = Bucket;

        }



        /// <summary>
        /// [Minio]
        /// MINIO_KEY=svr2FILE2020-.LOC
        /// MINIO_SECRET=svr2FILE2020-.LOC
        /// MINIO_BUCKET=bucket-name
        /// MINIO_ENDPOINT=example.com:9000
        /// MINIO_USE_SSL=false        
        /// </summary>
        /// <returns></returns>
        public static MinioStorage Instance()
        {
            return new MinioStorage(
                  Endpoint: ENV.Minio("MINIO_ENDPOINT"),
                  AccessKey: ENV.Minio("MINIO_KEY"),
                  SecretKey: ENV.Minio("MINIO_SECRET"),
                  Bucket: ENV.Minio("MINIO_BUCKET"),
                  SSL: EBoolean.valueOf(ENV.Minio("MINIO_USE_SSL") )
              );
        }






        /// <summary>
        /// Becareful, if wrong file path is specified, it will return a url to error message.
        /// </summary>
        /// <param name="ObjectPath">FileName - Full relative path to the bucket</param>
        /// <param name="Attachment">If attachment, downloadable else inline disposition</param>
        /// <param name="DownloadAsFileName"></param>
        /// <param name="ContentType">Implicate the type of response content type header</param>
        /// <param name="ExpiryTimeInSeconds">Maximum is 604800  - 7 days</param>
        /// <returns>null | string</returns>
        public override string CreateTemporaryURL(string ObjectPath, bool Attachment, string DownloadAsFileName,
            string ContentType, int ExpiryTimeInSeconds)
        {
            try
            {
                Dictionary<string, string> reqParams = new Dictionary<string, string> {
                    { "response-content-type",  ContentType },
                    { "response-content-disposition",
                        string.Format(
                            "{0}{1}", Attachment? "attachment" : "inline",
                             string.Format("; filename=\"{0}\"", DownloadAsFileName )
                            )
                    },
                };
                string presignedUrl = this.Minio.PresignedGetObjectAsync(this.Bucket, objectName: ObjectPath,
                    expiresInt: ExpiryTimeInSeconds,  //
                   reqParams: reqParams
                    ).Result;
                return presignedUrl;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unfortunately the url could not be created!", e);
            }
        }



        /// <summary>
        /// Check if Current Bucket exists
        /// </summary>
        /// <returns></returns>
        public override bool StorageExist()
        {
            return this.Minio.BucketExistsAsync (this.Bucket).Result;
        }


        /// <summary>
        /// Upload file to bucket
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <param name="FileFullPath"></param>
        public override void SaveFile(string ObjectPath, string FileFullPath)
        {

            try
            {
                if( this.StorageExist() )
                {
                    // Upload a file to bucket.
                    this.Minio.PutObjectAsync(this.Bucket, ObjectPath, FileFullPath).Wait();
                }
               
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unfortunately the file could not be uploaded!", e);
            }
        }

        /// <summary>
        /// Remember to delete file if not needed again
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <returns></returns>
        public override TemporaryFile GetFileAsDownloaded(string ObjectPath)
        {
            try
            {
                //string DestinationFileFullPath = ToCloudLocalStorage(GetUniqueFileName());
                TemporaryFile DestinationFileFullPath = new TemporaryFile();
                this.Minio.GetObjectAsync(bucketName: Bucket, objectName: ObjectPath, fileName: DestinationFileFullPath.FileFullPath, sse: null).Wait();

                return DestinationFileFullPath;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unfortunately the file could not be downloaded!", e);
            }
        }

        /// <summary>
        /// Checks if object exists
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <returns></returns>
        public override bool ObjectExists(string ObjectPath)
        {
            try
            {
                return GetFileInfo(ObjectPath) != null;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Get file details
        /// </summary>
        /// <param name="ObjectPath"></param>
        /// <returns></returns>
        public  ObjectStat GetFileInfo (string ObjectPath)
        {
            try
            {
                return this.Minio.StatObjectAsync(this.Bucket, ObjectPath).Result;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// Delete file
        /// </summary>
        /// <param name="ObjectPath"></param>
        public override void DeleteObject(string ObjectPath)
        {
            try
            {
                this.Minio.RemoveObjectAsync(bucketName: Bucket, objectName: ObjectPath).Wait();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unfortunately the file could not be deleted!", e);
            }
        }
    }
}
