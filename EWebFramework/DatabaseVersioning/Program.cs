using CODERiT.Logger.v._3._5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DatabaseVersioning
{
    static class Program
    {
        private static Log1 __Logger;
        public static Log1 Logger
        {
            get { return __Logger;  }
        }



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            __Logger = new Log1(source: typeof( Program) ).Load(Log1.Modes.FILE, true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }





        /// <summary>
        /// This is the location artisan is running from
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
            //One step Up
            return System.Windows.Forms.Application.StartupPath + "\\..\\" + pPath;
        }

        /// <summary>
        /// This is the directory the compilable codes are going into
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public static string CodePath(string pPath = "")
        {
            //One step Up
            return System.Windows.Forms.Application.StartupPath + "\\..\\EWebFramework\\" + pPath;
        }





    }
}
