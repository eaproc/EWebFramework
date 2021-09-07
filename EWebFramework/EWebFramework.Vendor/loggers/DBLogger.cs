using CODERiT.Logger.v._3._5;
using EWebFramework.Vendor.api.utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor.loggers
{
    public class DBLogger : ILoggable
    {

         
        public DBLogger()
        {
           

        }




        public void Write(string contents)
        {
            try
            {
                SqlConnection conn= new SqlConnection(
                        String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
                            ENV.DbConnection("HOST"), 
                            ENV.DbConnection("DATABASE_NAME"), ENV.DbConnection("DATABASE_USER_NAME"), ENV.DbConnection("DATABASE_USER_PASSWORD")
                        )
                    );
                conn.Open();

                using (SqlConnection connection = conn)
                {
                    SqlCommand command = connection.CreateCommand();

                    command.Connection = connection;

                    command.CommandText = String.Format("INSERT INTO {0} (Comments) VALUES ('{1}')", ENV.DbConnection("DATABASE_LOG_TABLE"), 
                        DB.Abstracts.All__DBs.PrepareStringForDatabaseInsertOrUpdate(contents)
                        );
                    command.ExecuteNonQuery();



                }

             }
            catch (Exception ex)
            {

                // Ignore error on Log
                Logger.Print("ERROR LOGGING USING DB LOGGER", ex);

            }


        }
    }
}
