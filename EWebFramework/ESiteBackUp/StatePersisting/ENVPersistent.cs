using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ESiteBackup.StatePersisting
{
    class ENVPersistent : IPersistableState
    {
       
        public string BackupFileServer { get => ENV.ESiteBackup(pVariableName: "BackupFileServer"); set => ENV.IESiteBackup(pVariableName: "BackupFileServer", pValue: value );  }
        public string TargetLocation { get => ENV.ESiteBackup(pVariableName: "TargetLocation"); set => ENV.IESiteBackup(pVariableName: "TargetLocation", pValue: value); }
        public string RunTimes { get => ENV.ESiteBackup(pVariableName: "RunTimes"); set => ENV.IESiteBackup(pVariableName: "RunTimes", pValue: value); }


    }
}
