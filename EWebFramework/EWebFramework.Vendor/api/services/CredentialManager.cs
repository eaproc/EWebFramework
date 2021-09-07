using DB.v4.IIS.MSSQL;
using EPRO.Library.Objects;
using EWebFramework.Vendor.api.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor.api.services
{

    public abstract class CredentialManager : IDisposable
    {
        public CredentialManager()
        {

            //You can set the logger here
            DB.Abstracts.All__DBs.Logger = Logger;


            this.DATABASE_IP = ENV.DbConnection("HOST");
            this.DATABASE_PORT = EInt.valueOf(ENV.DbConnection("PORT"));
            this.MASTER_RECORD__DBNAME = ENV.DbConnection("DATABASE_NAME");
            this.MASTER_RECORD__USERNAME = ENV.DbConnection("DATABASE_USER_NAME");
            this.MASTER_RECORD__USERPASSWORD = ENV.DbConnection("DATABASE_USER_PASSWORD");


        }


        protected Int32 DATABASE_PORT;
        protected string DATABASE_IP;
        protected string MASTER_RECORD__DBNAME;
        protected string MASTER_RECORD__USERNAME;
        protected string MASTER_RECORD__USERPASSWORD;




        public abstract Server GetDBConn();




        private bool disposedValue; // To detect redundant calls

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
            }
            this.disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        // Protected Overrides Sub Finalize()
        // ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        // Dispose(False)
        // MyBase.Finalize()
        // End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}