using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseVersioning.StatePersisting
{
    interface IPersistableState
    {

        String masterDBServer { get; set; }
        String masterDBName { get; set; }

        String masterUserName { get; set; }
        String masterUserPassword { get; set; }
        String versioningFolder { get; set; }
      


    }
}
