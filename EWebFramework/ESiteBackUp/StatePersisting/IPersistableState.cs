using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESiteBackup.StatePersisting
{
    interface IPersistableState
    {

       
        String BackupFileServer { get; set; }
        String TargetLocation { get; set; }
        String RunTimes { get; set; }


    }
}
