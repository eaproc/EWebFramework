using CODERiT.Logger.v._3._5;
using EPRO.Library.v3._5.AppConfigurations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CODERiT.PopUps.v3._5.ScrollablePopUp;

namespace ESiteBackup
{
    static class Program
    {
        private static Log1 __Logger;
        public static Log1 Logger
        {
            get { return __Logger; }
        }



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            __Logger = new Log1(source: typeof(Program)).Load(mode: Log1.Modes.FILE, Overwrite_Previous: true);




            try
            {
                Console.Write(ENV.DbConnection("HOST")); // Just to make sure .env file is present
            }
            catch (Exception ex)
            {

                errorMsg(ex.Message);
                return;
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

          
            new Framework(new MainWindow(), Framework.ShutdownModes.CLOSE_WHEN_LAST_FORM_CLOSES, ForceRunAsAdministrator: true, SingleInstanceApplication: false, 
                EnableWinXp_VisualStyles: true);

        }



        /// <summary>
        /// This is the location ESiteBackup is running from
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public static string RootPath(string pPath)
        {
            return System.Windows.Forms.Application.StartupPath + "\\" + pPath;
        }


        /// <summary>
        /// This is the root directory of the target app
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public static string AppPath(string pPath = "")
        {

            ////FOR DEBUB
            //return @"D:\Repos\scadware\highschools\gabus-scadware\VERSION_ASP.NET\scadware\scadware" + "\\" + pPath;

            //One step Up
            return System.Windows.Forms.Application.StartupPath + "\\..\\" + pPath;
        }




    }
}
