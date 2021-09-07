using CODERiT.Logger.v._3._5;
using System;


namespace EWebFramework
{
    /// <summary>
    /// Summary description for Program
    /// </summary>
    public class Program
    {
        
        static Program()
        {
            var logFile = StoragePath(String.Format("{0}__errors.log", DateTime.Now.ToString("yyyy_MM_dd")));

            ___Logger = new Log1(logFile, Log1.Modes.FILE, false);
        }



        private static Log1 ___Logger;
        public static Log1 Logger
        {
            get { return ___Logger; }
        }






    }
}
