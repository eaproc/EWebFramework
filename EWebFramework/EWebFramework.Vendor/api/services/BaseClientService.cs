using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using EPRO.Library.Objects;
using EWebFramework.Vendor.api.utils;
using EWebFramework.Vendor.api.controllers;
using System.Data;
using System.Data.SqlClient;
using EPRO.Library;
using EWebFramework.Vendor.api.services.DataTablesNET;
using System.Text;
using static EWebFramework.Vendor.PageHandlers;
using System.IO;

namespace EWebFramework.Vendor.api.services
{
    /// <summary>
    /// Extend for Client Service
    /// </summary>
    public class BaseClientService
    {


        /// <summary>
        /// Use for DB
        /// </summary>
        protected CredentialManager credentialManager;
        /// <summary>
        /// Handles Session
        /// </summary>
        public SessionHelper sessionHelper;


        /// <summary>
        /// Initialize 
        /// </summary>
        /// <param name="sessionHelper"></param>
        public BaseClientService(SessionHelper sessionHelper)
        {

            this.sessionHelper = sessionHelper;

        }

        /// <summary>
        /// Use this if you are accessing it from CronJob. No session will be used
        /// </summary>
        public BaseClientService():this(null)
        {}





        /// <summary>
        /// Turn this on if you want to trace sql query
        /// </summary>
        public static bool TRACE_DEBUG_SQL = false;


        /// <summary>
        /// Addressing apostrophe means you will pass string value with this \'{0}\'
        /// </summary>
        /// <param name="pSQL"></param>
        /// <param name="AddressApostrophe"></param>
        /// <returns>DataTable or null</returns>
        public virtual System.Data.DataTable GetSQLTable(String pSQL, bool AddressApostrophe = false)
        {

            if (TRACE_DEBUG_SQL) Logger.Print(pSQL);
            if (this.credentialManager == null) throw new InvalidOperationException("Please, set the credential manager on the inherited Client Service!");

            try
            {
                var db = (DB.Abstracts.All__DBs)this.credentialManager.GetDBConn();
                var p = db.getRS(pSQL, AddressApostrophe);
                return (p != null && p.Tables.Count > 0) ? p.Tables[0] : null;
            }
            catch (Exception ex)
            {
                Logger.Print(ex);
                throw ex;
            }
            
        }



        /// <summary>
        /// Addressing apostrophe means you will pass string value with this \'{0}\'
        /// </summary>
        /// <param name="pSQL"></param>
        /// <param name="AddressApostrophe"></param>
        /// <returns>boolean</returns>
        public virtual bool ExecuteQuery(String pSQL, bool AddressApostrophe = false)
        {

            if (TRACE_DEBUG_SQL) Logger.Print(pSQL);



            var db = (DB.Abstracts.All__DBs)this.credentialManager.GetDBConn();
            var p = db.dbExec(SQL: pSQL, Address_Apostrophe_Issue: AddressApostrophe);
            return p;
        }



        #region "Utilities"

        /// <summary>
        /// Checks if table is not null and has rows
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static bool HasRows(DataTable dataTable)
        {
            return dataTable != null && dataTable.Rows.Count > 0;
        }


        #endregion




        #region DBTransaction


        private SqlConnection DBTransactionConn = null;
        private SqlCommand DBTransactionCommand = null;
        private SqlTransaction DBTransaction = null;

        /// <summary>
        /// This should create the DB Connection and get it ready for Transaction
        /// </summary>
        public void BeginDBTransaction()
        {
            if (this.DBTransactionConn != null) throw new Exception("This class has opened transaction connection.");
            this.DBTransactionConn = this.credentialManager.GetDBConn().getSQLConnection();
            this.DBTransactionCommand = DBTransactionConn.CreateCommand();
            // Start a local transaction.
            // Maximum Identifier length | Transaction Name is 32 characters
            this.DBTransaction = DBTransactionConn.BeginTransaction("TRANS: " + DateTime.Now.ToString());

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            DBTransactionCommand.Connection = DBTransactionConn;
            DBTransactionCommand.Transaction = DBTransaction;
        }

        /// <summary>
        /// UPDATE, INSERT, and DELETE. Returns numbers of rows affected. -1 if unsuccessful or different statement
        /// </summary>
        /// <param name="pSQL"></param>
        /// <returns>boolean</returns>
        public int ExecuteTransactionQuery(String pSQL)
        {

            if (TRACE_DEBUG_SQL) Logger.Print(pSQL);

            this.DBTransactionCommand.CommandText = pSQL;
            return this.DBTransactionCommand.ExecuteNonQuery();

        }

        /// <summary>
        /// Commit DB Transaction Query
        /// </summary>
        public void CommitDBTransaction()
        {
            // Attempt to commit the transaction.
            this.DBTransaction.Commit();
            this.DBTransactionConn.Dispose();
            this.DBTransactionConn = null;
        }

        /// <summary>
        /// Rollback
        /// </summary>
        public void RollBackDBTransaction()
        {
            // Attempt to commit the transaction.
            this.DBTransaction.Rollback();
            this.DBTransactionConn.Dispose();
            this.DBTransactionConn = null;
        }



        #endregion




        #region Pagination

        /// <summary>
        /// For pagination
        /// </summary>
        /// <param name="pStart"></param>
        /// <param name="pLength"></param>
        /// <param name="pSQL"></param>
        /// <param name="pPreSQL"></param>
        /// <param name="OverrideOrderBy"></param>
        /// <param name="pOrderByColumn"></param>
        /// <param name="pOrderByColumnDirection"></param>
        /// <param name="pWhereClauseFilter"></param>
        /// <returns></returns>
        protected DataTable Paginate(int pStart, int pLength, string pSQL,
             string pPreSQL = "", String OverrideOrderBy = null,
            String pOrderByColumn = "1", String pOrderByColumnDirection = "asc", string pWhereClauseFilter = "")
        {
            // Injected filtered_record_count
            //
            String bSQL = String.Empty;
            if (OverrideOrderBy==null|| OverrideOrderBy==String.Empty)
            {
                bSQL = String.Format(
                                        pPreSQL + Environment.NewLine +
                                        "SELECT TOP {2} * FROM (                  " + Environment.NewLine +
                                        "     SELECT *                                                                                " + Environment.NewLine +
                                        "         FROM (                                                                                  " + Environment.NewLine +
                                        "                   SELECT *,                                                                     " + Environment.NewLine +
                                        "                       (                                                                         " + Environment.NewLine +
                                        "                          SELECT count(*) as filtered_record_count                               " + Environment.NewLine +
                                        "                          FROM (                                                                 " + Environment.NewLine +
                                        "                                   {0}                                                           " + Environment.NewLine +
                                        "                               ) as rows_fetched                                                 " + Environment.NewLine +
                                        "                          {3}                                                                    " + Environment.NewLine +
                                        "                       ) AS filtered_record_count                                                " + Environment.NewLine +
                                        "                   FROM (                                                                        " + Environment.NewLine +
                                        "                           {0}                                                                   " + Environment.NewLine +
                                        "                         ) as rows_fetched                                                       " + Environment.NewLine +
                                        "                   {3}                                                                           " + Environment.NewLine +
                                        "            ) as rows_fetched_with_count                                                         " + Environment.NewLine +
                                        "         ORDER BY {4} {5}                                                                        " + Environment.NewLine +
                                        "         OFFSET {1} ROWS                                                                         " + Environment.NewLine +
                                        "                      ) finalFetch                  " + Environment.NewLine +
                                        "                  ", pSQL, pStart, pLength, pWhereClauseFilter, pOrderByColumn, pOrderByColumnDirection
                                        );
            }
            else
            {
                bSQL = String.Format(
                                pPreSQL + Environment.NewLine +
                                        "SELECT TOP {2} * FROM (                  " + Environment.NewLine +
                                        "     SELECT *                                                                                " + Environment.NewLine +
                                        "         FROM (                                                                                  " + Environment.NewLine +
                                        "                   SELECT *,                                                                     " + Environment.NewLine +
                                        "                       (                                                                         " + Environment.NewLine +
                                        "                          SELECT count(*) as filtered_record_count                               " + Environment.NewLine +
                                        "                          FROM (                                                                 " + Environment.NewLine +
                                        "                                   {0}                                                           " + Environment.NewLine +
                                        "                               ) as rows_fetched                                                 " + Environment.NewLine +
                                        "                          {3}                                                                    " + Environment.NewLine +
                                        "                       ) AS filtered_record_count                                                " + Environment.NewLine +
                                        "                   FROM (                                                                        " + Environment.NewLine +
                                        "                           {0}                                                                   " + Environment.NewLine +
                                        "                         ) as rows_fetched                                                       " + Environment.NewLine +
                                        "                   {3}                                                                           " + Environment.NewLine +
                                        "            ) as rows_fetched_with_count                                                         " + Environment.NewLine +
                                        "         ORDER BY {4}                                                                        " + Environment.NewLine +
                                        "         OFFSET {1} ROWS                                                                         " + Environment.NewLine +
                                        "                      ) finalFetch                  " + Environment.NewLine +
                                        "                  ", pSQL, pStart, pLength, pWhereClauseFilter, OverrideOrderBy
                               );
            }

            return GetSQLTable(pSQL: bSQL);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSQL"></param>
        /// <param name="pOrderByColumnIndex"></param>
        /// <param name="pOrderByColumnDirection"></param>
        /// <param name="pWhereClauseFilter"></param>
        /// <returns></returns>
        protected DataTable ProcessForExport(string pSQL, int pOrderByColumnIndex = 1, String pOrderByColumnDirection = "asc", string pWhereClauseFilter = "")
        {
            // Injected filtered_record_count
            //

            return GetSQLTable(pSQL:
                String.Format("     SELECT *                                                              " + Environment.NewLine +
                                "     FROM (                                                                " + Environment.NewLine +
                                "               SELECT *                                                   " + Environment.NewLine +
                                "               FROM (                                                      " + Environment.NewLine +
                                "                       {0}                                                 " + Environment.NewLine +
                                "                     ) as rows_fetched                                     " + Environment.NewLine +
                                "               {1}                                                         " + Environment.NewLine +
                                "        ) as rows_fetched_with_count                                       " + Environment.NewLine +
                                "     ORDER BY {2} {3}                                                      " + Environment.NewLine +
                                "  ", pSQL, pWhereClauseFilter, pOrderByColumnIndex, pOrderByColumnDirection
                                )
                        );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSQL"></param>
        /// <param name="dTR"></param>
        /// <param name="pWhereClauseFilter"></param>
        /// <param name="OverrideOrderBy"></param>
        /// <param name="pSummarizeColumnNames"></param>
        /// <returns></returns>
        protected DataTableReturnFormat ProcessPaginate(String pSQL, 
                                               DataTableRequestFields dTR,
                                               string pWhereClauseFilter = "",
                                               String OverrideOrderBy = null,
                                               params String[] pSummarizeColumnNames
            )
        {
            var dtrf =  ProcessPaginate(pSQL: pSQL, pWhereClauseFilter: pWhereClauseFilter,
                pStart: dTR.Start, pLength: dTR.Length,
                pOrderByColumn: dTR.SortUsingColumnName ? dTR.OrderByColumnName : dTR.OrderByColumnIndex.ToString(),
                pOrderByColumnDirection: dTR.OrderByColumnDirection,
                pSummarizeColumnNames: pSummarizeColumnNames, OverrideOrderBy: OverrideOrderBy
                );

            dtrf.draw = dTR.Draw;


            return dtrf;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pPreSQL"></param>
        /// <param name="pSQL"></param>
        /// <param name="dTR"></param>
        /// <param name="pWhereClauseFilter"></param>
        /// <param name="pSummarizeColumnNames"></param>
        /// <returns></returns>
        protected DataTableReturnFormat ProcessPaginate(
            String pPreSQL, 
            String pSQL,
                                            DataTableRequestFields dTR,
                                            string pWhereClauseFilter = "",
                                            params String[] pSummarizeColumnNames
         )
        {
            var dtrf = ProcessPaginate(pSQL: pSQL, pPreSQL:pPreSQL, 
                pWhereClauseFilter: pWhereClauseFilter,
                pStart: dTR.Start, pLength: dTR.Length,
                pOrderByColumn: dTR.SortUsingColumnName ? dTR.OrderByColumnName : dTR.OrderByColumnIndex.ToString(),
                pOrderByColumnDirection: dTR.OrderByColumnDirection,
                pSummarizeColumnNames: pSummarizeColumnNames
                );

            dtrf.draw = dTR.Draw;


            return dtrf;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSQL"></param>
        /// <param name="pWhereClauseFilter"></param>
        /// <param name="pPreSQL"></param>
        /// <param name="pStart"></param>
        /// <param name="pLength"></param>
        /// <param name="pOrderByColumn"></param>
        /// <param name="pOrderByColumnDirection"></param>
        /// <param name="OverrideOrderBy"></param>
        /// <param name="pSummarizeColumnNames"></param>
        /// <returns></returns>
        protected DataTableReturnFormat ProcessPaginate(String pSQL,
            string pWhereClauseFilter = "",
            string pPreSQL = "",
            int pStart = 0, int pLength = 20, String pOrderByColumn = "1", String pOrderByColumnDirection = "asc",
             String OverrideOrderBy = null,
            params String[] pSummarizeColumnNames)
        {




            DataTable dCompleteTable = GetSQLTable(pSQL: pPreSQL + Environment.NewLine+  pSQL);


            DataTable dCompleteTableWithFilterSummary = null;


            if (pSummarizeColumnNames.Count() > 0)
            {
                String columns = String.Join(", ", pSummarizeColumnNames.Select(x => String.Format("SUM({0}) as SUM_{0}", x)));
                dCompleteTableWithFilterSummary = GetSQLTable(pSQL:
               String.Format("     {3}       " + Environment.NewLine +
                                "     SELECT  {2}       " + Environment.NewLine +
                               "               FROM (                                                      " + Environment.NewLine +
                               "                       {0}                                                 " + Environment.NewLine +
                               "                     ) as rows_fetched                                     " + Environment.NewLine +
                               "               {1}                                                         " + Environment.NewLine,
                                           pSQL, pWhereClauseFilter, columns, pPreSQL
                               )
                       );
            }




            int pTotalCountWithoutPagination = 0;
            if (dCompleteTable != null) pTotalCountWithoutPagination = dCompleteTable.Rows.Count;


            DataTable paginated = Paginate(pStart: pStart, pLength: pLength, pSQL: pSQL, pPreSQL: pPreSQL,
                pOrderByColumn: pOrderByColumn, pOrderByColumnDirection: pOrderByColumnDirection,
                pWhereClauseFilter: pWhereClauseFilter, OverrideOrderBy: OverrideOrderBy
                );



            DataTableReturnFormat dtrf = new DataTableReturnFormat()
            {
                recordsFiltered = paginated != null && paginated.Rows.Count > 0 ? EInt.valueOf(paginated.Rows[0]["filtered_record_count"]) : 0,
                recordsTotal = pTotalCountWithoutPagination,
                totalPagesSummary = dCompleteTableWithFilterSummary != null ? Controller.ToDictionary(dCompleteTableWithFilterSummary).First() : null
            };



            dtrf.data = Controller.ToDictionary(paginated);


            return dtrf;

        }



        #endregion





        #region FileStorages


        /// <summary>
        /// Help remove wrong characters from file name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetArbitraryValidFileName(string filename)
        {

            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(c, '_');
            }

            return filename;
        }



        /// <summary>
        /// Create a random file name for this session to temporary store file and delete
        /// </summary>
        /// <param name="pExtWithDot">Just additional string to append</param>
        /// <param name="appendRandom">If you want to generate the randomString Part with it</param>
        /// <param name="randomStringLength">The length of the random string if needed</param>
        /// <returns></returns>
        public static string GetSessionTempFileName(string pExtWithDot = "", bool appendRandom = true, uint randomStringLength = 6)
        {
            string s = appendRandom ? AlphaNumericCodeGenerator.RandomString((int)randomStringLength) : string.Empty;
            return StoragePath(String.Format(@"{0}_{1}{2}", HttpContext.Current.Session.SessionID, s, pExtWithDot));
        }







        /// <summary>
        /// Get Resized Image
        /// </summary>
        /// <param name="Contents"></param>
        /// <param name="pResizeWidth"></param>
        /// <param name="pResizeHeight"></param>
        /// <returns></returns>
        public byte[] GetResizedImage(byte[] Contents, uint pResizeWidth = 0, uint pResizeHeight = 0)
        {
            using (TemporaryFile tf = new TemporaryFile(".jpg"))
            {

                File.WriteAllBytes(tf.FileFullPath, Contents);


                //Resize
                //
                if (pResizeWidth != 0 && pResizeHeight != 0)
                {
                    using (Simple.ImageResizer.ImageResizer resizer = new Simple.ImageResizer.ImageResizer(Contents))
                    {
                        resizer.Resize(width: (int)pResizeWidth, height: (int)pResizeHeight, encoding: Simple.ImageResizer.ImageEncoding.Jpg100);
                        resizer.SaveToFile(tf.FileFullPath);
                    }
                }


                return File.ReadAllBytes(tf.FileFullPath);
            }

        }


        /// <summary>
        /// Get Resized Image
        /// </summary>
        /// <param name="Contents"></param>
        /// <param name="pResizeWidth"></param>
        /// <param name="pResizeHeight"></param>
        /// <returns></returns>
        public byte[] GetResizedImage(Stream Contents, uint pResizeWidth = 0, uint pResizeHeight = 0)
        {

            Contents.Seek(0, System.IO.SeekOrigin.Begin);
            // Read it without closing the stream. Because once the stream is closed, it is gone.
            // 
            return GetResizedImage(new BinaryReader(Contents, Encoding.UTF8, leaveOpen: true).ReadBytes((int)Contents.Length), pResizeWidth, pResizeHeight);

        }



        /// <summary>
        /// Store image in directory given
        /// </summary>
        /// <param name="pDstFileFullPath"></param>
        /// <param name="pUploadedFile"></param>
        /// <param name="pResizeWidth"></param>
        /// <param name="pResizeHeight"></param>
        /// <returns></returns>
        public bool StoreImage(String pDstFileFullPath, HttpPostedFile pUploadedFile, uint pResizeWidth = 0, uint pResizeHeight = 0)
        {
            try
            {

                if (!Directory.Exists(EIO.getDirectoryFullPath(pDstFileFullPath))) EIO.DeleteAndRecreateDirectory(EIO.getDirectoryFullPath(pDstFileFullPath));

                File.WriteAllBytes(pDstFileFullPath, this.GetResizedImage(pUploadedFile.InputStream, pResizeHeight: pResizeHeight, pResizeWidth: pResizeWidth));



                //using (TemporaryFile tf = new TemporaryFile(".jpg"))
                //{

                //    pUploadedFile.SaveAs(tf.FileFullPath);


                //    //Resize
                //    //
                //    if (pResizeWidth != 0 && pResizeHeight != 0)
                //    {
                //        using (Simple.ImageResizer.ImageResizer resizer = new Simple.ImageResizer.ImageResizer(tf.FileFullPath ))
                //        {
                //            resizer.Resize(width: (int)pResizeWidth, height: (int)pResizeHeight, encoding: Simple.ImageResizer.ImageEncoding.Jpg100);
                //            resizer.SaveToFile(pDstFileFullPath);

                //        }
                //    }
                //    else
                //    {
                //        // Just save the original file as it is
                //        EIO.CopyFile(tf.FileFullPath , pDstFileFullPath);
                //    }

                //}

                    //// Create destination Path if it doesn't exist
                    //if (!System.IO.Directory.Exists(EIO.getDirectoryFullPath(pDstFileFullPath))) EIO.DeleteAndRecreateDirectory(EIO.getDirectoryFullPath(pDstFileFullPath));

                //// 
                //// Save File in Temp Location
                //// Use it and delete it
                ////
                //var pDstTemp = GetSessionTempFileName(".jpg");
                //if (!System.IO.Directory.Exists(EIO.getDirectoryFullPath(pDstTemp))) EIO.DeleteAndRecreateDirectory(EIO.getDirectoryFullPath(pDstTemp));
                //pUploadedFile.SaveAs(pDstTemp);

                ////Resize
                ////
                //if (pResizeWidth != 0 && pResizeHeight != 0)
                //{
                //    using (Simple.ImageResizer.ImageResizer resizer = new Simple.ImageResizer.ImageResizer(pDstTemp))
                //    {
                //        resizer.Resize(width: (int)pResizeWidth, height: (int)pResizeHeight, encoding: Simple.ImageResizer.ImageEncoding.Jpg100);
                //        resizer.SaveToFile(pDstFileFullPath);

                //    }
                //}
                //else
                //{
                //    // Just save the original file as it is
                //    EIO.CopyFile(pDstTemp, pDstFileFullPath);
                //}


                ////Clean Up
                //// delete the temp file
                //EIO.DeleteFileIfExists(pDstTemp);


                return true;
            }
            catch (Exception ex)
            {
                Logger.Print(ex);
                return false;
            }

        }


        /// <summary>
        /// Store file in directory given
        /// </summary>
        /// <param name="pDstFileFullPath"></param>
        /// <param name="pUploadedFile"></param>
        /// <returns></returns>
        public bool StoreFile(String pDstFileFullPath, HttpPostedFile pUploadedFile)
        {
            try
            {

                // Create destination Path if it doesn't exist
                if (!System.IO.Directory.Exists(EIO.getDirectoryFullPath(pDstFileFullPath))) EIO.DeleteAndRecreateDirectory(EIO.getDirectoryFullPath(pDstFileFullPath));

                // 
                // Save File in Location
                //
                pUploadedFile.SaveAs(pDstFileFullPath);


                return true;
            }
            catch (Exception ex)
            {
                Logger.Print(ex);
                return false;
            }

        }


        #endregion









    }
}