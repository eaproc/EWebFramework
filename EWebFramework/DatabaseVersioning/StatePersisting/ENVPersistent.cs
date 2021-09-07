using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DatabaseVersioning.StatePersisting
{
    class ENVPersistent : IPersistableState
    {
        public string masterDBServer
        {
            get
            {
                return ENV.DbConnection(pVariableName: "HOST");
            }
            set
            {
                ENV.IDbConnection(pVariableName: "HOST", pValue: value);
            }
        }

        public string masterDBName
        {
            get
            {
                return ENV.DbConnection(pVariableName: "DATABASE_NAME");
            }
            set
            {
                ENV.IDbConnection(pVariableName: "DATABASE_NAME", pValue: value);

            }
        }
        public string masterUserName
        {
            get
            {
                return ENV.DbConnection(pVariableName: "DATABASE_USER_NAME");
            }
            set
            {
                ENV.IDbConnection(pVariableName: "DATABASE_USER_NAME", pValue: value);
            }
        }
        public string masterUserPassword
        {
            get
            {
                return ENV.DbConnection(pVariableName: "DATABASE_USER_PASSWORD");

            }
            set
            {
                ENV.IDbConnection(pVariableName: "DATABASE_USER_PASSWORD", pValue: value);

            }
        }
        public string versioningFolder
        {
            get
            {
                return ENV.General(pVariableName: "versioningFolder");

            }
            set
            {
                ENV.IGeneral(pVariableName: "versioningFolder", pValue: value);
            }
        }
    }
}
