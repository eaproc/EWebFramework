using DB.v4.IIS.MSSQL;
using EPRO.Library.Objects;
using EWebFramework.Vendor.api.services;
using EWebFramework.Vendor.api.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.api.services
{

    public  class CredentialManagerImplementer : CredentialManager
    {
        public CredentialManagerImplementer()
        {
            if( ENV.USE_APP_DB()) throw new NotImplementedException("Please, set the database credentials if you need database acess!");

            //  You can set the necessary schema database here
            //  DatabaseInit.DBConnectInterface 
            //      = new DatabaseInit(new DefaultDatabaseInit(MASTER_RECORD__DBNAME, MASTER_RECORD__USERNAME, MASTER_RECORD__USERPASSWORD, DATABASE_PORT, DATABASE_IP) );


        }




        public override Server GetDBConn()
        {
            throw new NotImplementedException();
            //return scadware_logic.DatabaseSchema.DatabaseInit.DBConnectInterface.getDBConn(); // SET THIS
        }



        public string GetConnectionString()
        {
            // ;Connection Timeout=600
            return string.Format("Data Source={0},{1};Network Library=DBMSSOCN;Initial Catalog={2};User ID={3};Password={4};Connection Timeout=600", DATABASE_IP, DATABASE_PORT, MASTER_RECORD__DBNAME, MASTER_RECORD__USERNAME, MASTER_RECORD__USERPASSWORD);
        }


    }

}