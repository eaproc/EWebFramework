using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DatabaseVersioning.StatePersisting
{
    class RegistryPersistent : IPersistableState
    {
        public string masterDBServer {
            get {
                return Interaction.GetSetting(AppName: System.AppDomain.CurrentDomain.FriendlyName, 
                        Section: this.GetType().Name,
                            Key: "masterDBServer"
                            );
            }
            set {
                Interaction.SaveSetting(AppName: System.AppDomain.CurrentDomain.FriendlyName,
                        Section: this.GetType().Name,
                            Key: "masterDBServer", Setting: value
                            );
            }
        }

        public string masterDBName
        {
            get
            {
                return Interaction.GetSetting(AppName: System.AppDomain.CurrentDomain.FriendlyName,
                        Section: this.GetType().Name,
                            Key: "masterDBName"
                            );
            }
            set
            {
                Interaction.SaveSetting(AppName: System.AppDomain.CurrentDomain.FriendlyName,
                        Section: this.GetType().Name,
                            Key: "masterDBName", Setting: value
                            );
            }
        }
        public string masterUserName
        {
            get
            {
                return Interaction.GetSetting(AppName: System.AppDomain.CurrentDomain.FriendlyName,
                        Section: this.GetType().Name,
                            Key: "masterUserName"
                            );
            }
            set
            {
                Interaction.SaveSetting(AppName: System.AppDomain.CurrentDomain.FriendlyName,
                        Section: this.GetType().Name,
                            Key: "masterUserName", Setting: value
                            );
            }
        }
        public string masterUserPassword
        {
            get
            {
                return Interaction.GetSetting(AppName: System.AppDomain.CurrentDomain.FriendlyName,
                        Section: this.GetType().Name,
                            Key: "masterUserPassword"
                            );
            }
            set
            {
                Interaction.SaveSetting(AppName: System.AppDomain.CurrentDomain.FriendlyName,
                        Section: this.GetType().Name,
                            Key: "masterUserPassword", Setting: value
                            );
            }
        }
        public string versioningFolder
        {
            get
            {
                return Interaction.GetSetting(AppName: System.AppDomain.CurrentDomain.FriendlyName,
                        Section: this.GetType().Name,
                            Key: "versioningFolder"
                            );
            }
            set
            {
                Interaction.SaveSetting(AppName: System.AppDomain.CurrentDomain.FriendlyName,
                        Section: this.GetType().Name,
                            Key: "versioningFolder", Setting: value
                            );
            }
        }
    }
}
