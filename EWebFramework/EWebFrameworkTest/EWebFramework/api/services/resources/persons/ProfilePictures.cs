
using DB.Abstracts;

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPRO.Library.Objects;
using EPRO.Library;
using System.IO;
using EWebFramework.Vendor.api.utils;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.api.services.resources.persons
{
    public class ProfilePictures:ClientService
    {

        public ProfilePictures(SessionHelper sessionHelper) : base(sessionHelper)
        {

        }

        /// <summary>
        /// Fetches the xtra small file if it exists, else it returns default file full path
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string FetchXS(string FileName, int ID)
        {
            if (FileName == null) return ResourcesPath( Default__RelativeFilePath_ProfilePic_XS() );

            String requestedFile = ResourcesPath( RelativeFilePath_ProfilePic_XS(ID, FileName) );

            return System.IO.File.Exists(requestedFile) ? requestedFile : ResourcesPath(Default__RelativeFilePath_ProfilePic_XS());

        }



        /// <summary>
        /// Fetches the small file if it exists, else it returns default file full path
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string FetchSM(string FileName, int ID)
        {
            if (FileName == null) return ResourcesPath(Default__RelativeFilePath_ProfilePic_SM());

            String requestedFile = ResourcesPath(RelativeFilePath_ProfilePic_SM(ID, FileName));

            return System.IO.File.Exists(requestedFile) ? requestedFile : ResourcesPath(Default__RelativeFilePath_ProfilePic_SM());

        }



        /// <summary>
        /// Fetches the normal file if it exists, else it returns default file full path
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string Fetch(string FileName, int ID)
        {
            if (FileName == null) return ResourcesPath(Default__RelativeFilePath_ProfilePic());

            String requestedFile = ResourcesPath(RelativeFilePath_ProfilePic(ID, FileName));

            return System.IO.File.Exists(requestedFile) ? requestedFile : ResourcesPath(Default__RelativeFilePath_ProfilePic());

        }


        public void Store(string PictureFileName, HttpPostedFile File__PROFILE_PICTURE, int PersonID)
        {
            //
            // Clear previous profile pictures
            //
            String d = EIO.getDirectoryFullPath ( ResourcesPath(RelativeFilePath_ProfilePic(pPersonID: PersonID, StoredFileName: PictureFileName) ));
            if (Directory.Exists(d))
                foreach (var v in Directory.GetFiles(d))
                    EIO.DeleteFileIfExists(v);


            var pDst = ResourcesPath(RelativeFilePath_ProfilePic(pPersonID: PersonID, StoredFileName: PictureFileName)); //400px
            var pDstSmall = ResourcesPath(RelativeFilePath_ProfilePic_SM(pPersonID: PersonID, StoredFileName: PictureFileName)); //200px
            var pDstXtraSmall = ResourcesPath(RelativeFilePath_ProfilePic_XS(pPersonID: PersonID, StoredFileName: PictureFileName)); //50px

            StoreImage(pDst, pUploadedFile: File__PROFILE_PICTURE);
            StoreImage(pDstSmall, pUploadedFile: File__PROFILE_PICTURE, pResizeWidth: 200, pResizeHeight: 200);
            StoreImage(pDstXtraSmall, pUploadedFile: File__PROFILE_PICTURE, pResizeWidth: 50, pResizeHeight: 50);
        }






        #region PersonProfilePictures

        /// <summary>
        /// Default Uploaded Profile Picture With Requested Size and Dimension
        /// </summary>
        /// <param name="pPersonID"></param>
        /// <param name="StoredFileName"></param>
        /// <returns></returns>
        public string RelativeFilePath_ProfilePic(int pPersonID, String StoredFileName)
        {
            return String.Format(@"{0}\profile-pictures\{1}", RelativeFilePath_Persons(pPersonID: pPersonID), StoredFileName);
        }

        /// <summary>
        /// Uploaded Profile Picture Xtra Small Size 50px x 50px
        /// </summary>
        /// <param name="pPersonID"></param>
        /// <param name="StoredFileName"></param>
        /// <returns></returns>
        public string RelativeFilePath_ProfilePic_XS(int pPersonID, String StoredFileName)
        {
            return String.Format(@"{0}\profile-pictures\50_50_{1}", RelativeFilePath_Persons(pPersonID: pPersonID), StoredFileName);
        }

        /// <summary>
        /// Uploaded Profile Picture Small Size 200px x 200px
        /// </summary>
        /// <param name="pPersonID"></param>
        /// <param name="StoredFileName"></param>
        /// <returns></returns>
        public string RelativeFilePath_ProfilePic_SM(int pPersonID, String StoredFileName)
        {
            return String.Format(@"{0}\profile-pictures\200_200_{1}", RelativeFilePath_Persons(pPersonID: pPersonID), StoredFileName);
        }



        public string Default__RelativeFilePath_ProfilePic()
        {
            return RelativeFilePath_ProfilePic(pPersonID: 0, StoredFileName: "person.png");
        }
        public string Default__RelativeFilePath_ProfilePic_XS()
        {
            return RelativeFilePath_ProfilePic_XS(pPersonID: 0, StoredFileName: "person.png");
        }
        public string Default__RelativeFilePath_ProfilePic_SM()
        {
            return RelativeFilePath_ProfilePic_SM(pPersonID: 0, StoredFileName: "person.png");
        }


        #endregion



    }
}