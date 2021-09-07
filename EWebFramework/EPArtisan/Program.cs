using CODERiT.Logger.v._3._5;
using EPArtisan.Dumpers;
using EPArtisan.Loggers;
using EPArtisan.Utils.JsonReplies;
using EPRO.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using static EPArtisan.Dumpers.VarDump;

namespace EPArtisan
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Welcome to EWebFramework Artisan Console App");
            Console.WriteLine("---------------------------------------------------------------------");

            ConsoleLogger consoleLogger = new ConsoleLogger();


            try
            {

                String command = null;
                if (args.Length == 0)
                    command = "list";
                else
                    command = args[0];


                switch(command)
                {
                    case "list":
                        PrintCommandList();
                        break;
                    case "config":
                        PrintConfiguration();
                        break;
                    case "make:console":
                        MakeConsole(args);
                        break;
                    case "make:job":
                        MakeJob(args);
                        break;
                    case "make:item":
                        MakeItem(args);
                        break;
                    case "run:console":
                        RunConsole(args);
                        break;
                    //case "run:minify-js":
                    //    RunMinifyJs(args);
                    //    break;
                    default:
                        throw new Exception("Invalid Arguments");
                }




            }
            catch (VarDumpException ex)
            {
                consoleLogger.OutputJSON(new FailedResult(pMessage: ex.Message, pData: ex.dumpedData));
            }
            catch (Exception ex)
            {
                ConsoleError(ex.Message);
                consoleLogger.OutputJSON(new FailedResult(pMessage: ex.Message));
            }


            Console.WriteLine();

        }

   

        private static void MakeConsole(string[] args)
        {
            string targetFolder = CodePath();

            StringBuilder sbLoggers = new StringBuilder();

            // add more loggers

            if (args.Contains("--file-logger") || args.Contains("--all-loggers"))
                sbLoggers.AppendLine("this.Loggers.Add(new EWebFramework.Vendor.loggers.FileLogger());");

            if (args.Contains("--db-logger") || args.Contains("--all-loggers"))
                sbLoggers.AppendLine("this.Loggers.Add(new EWebFramework.Vendor.loggers.DBLogger());");





            if (args.Length < 2) throw new Exception("Invalid number of arguments, please check usage!");

            String ConsoleName = args[1];





            //consoles folder
            targetFolder += "\\consoles";

            String sample = System.IO.File.ReadAllText(Program.RootPath("Samples\\SampleConsole.cs"));

            sample = sample.Replace("//@@CREATE_LOGGERS", sbLoggers.ToString());
            sample = sample.Replace("SampleConsole", ConsoleName);

            String filePath = targetFolder + "\\" + ConsoleName + ".cs";


            System.IO.File.WriteAllText(filePath, sample);


            ConsoleSuccess(String.Format("{0} has been created in [{1}]!", ConsoleName, filePath));


        }

        private static void MakeJob(string[] args)
        {
            string targetFolder = CodePath();

            StringBuilder sbLoggers = new StringBuilder();

            // add more loggers

            if (args.Contains("--file-logger") || args.Contains("--all-loggers"))
                sbLoggers.AppendLine("this.Loggers.Add(new EWebFramework.Vendor.loggers.FileLogger());");

            if (args.Contains("--db-logger") || args.Contains("--all-loggers"))
                sbLoggers.AppendLine("this.Loggers.Add(new EWebFramework.Vendor.loggers.DBLogger());");



            if (args.Length < 2) throw new Exception("Invalid number of arguments, please check usage!");

            String JobName = args[1];





            //consoles folder
            targetFolder += "\\scheduled_jobs";

            String sample = System.IO.File.ReadAllText(Program.RootPath("Samples\\SampleJob.cs"));

            sample = sample.Replace("//@@CREATE_LOGGERS", sbLoggers.ToString());
            sample = sample.Replace("SampleJob", JobName);

            String filePath = targetFolder + "\\" + JobName + ".cs";


            System.IO.File.WriteAllText(filePath, sample);


            ConsoleSuccess(String.Format("{0} has been created in [{1}]!", JobName, filePath));


        }

    
        private static void MakeItem(string[] args)
        {
            string targetFolder = AppPath();
            String CodeFolder = CodePath();



            if (args.Length < 2) throw new Exception("Invalid number of arguments, please check usage!");

            String FullPathSpecified = args[1];

            //Parent Path contains forward slash at the begining
            String ParentPath = EIO.getDirectoryFullPath(FullPathSpecified.Replace("/", "\\"));

            String FileNameNoExtension = EIO.getFileName(FullPathSpecified.Replace("/", "\\"));

            bool isAPI = args.Contains("--api");
            bool noView = isAPI || args.Contains("--no-view");
            bool noService = !isAPI;
            bool noController = args.Contains("--no-controller");

            String FinalFolder = isAPI ? CodeFolder + "\\api" : CodeFolder;


            String ServiceFolder = System.IO.Path.Combine(FinalFolder + "\\services", ParentPath);
            String ControllerFolder = System.IO.Path.Combine(FinalFolder + "\\controllers", ParentPath);
            String ViewsFolder = System.IO.Path.Combine(targetFolder + "\\views", ParentPath);
            String ScriptsFolder = System.IO.Path.Combine(targetFolder + "\\assets\\pages\\scripts", ParentPath);

            String AppDomain = EIO.getFileName(targetFolder);


            String AppendNameSpace = ParentPath == null || ParentPath == "" ? ParentPath : "." + ParentPath.Replace("\\", ".");


            String sample = null;
            String filePath = null;

            if (!noView)
            {
                //create view and script
                var ForwardSlahParentPath = ParentPath.Replace("\\", "/");
                ForwardSlahParentPath = ForwardSlahParentPath.StartsWith("/") ? ForwardSlahParentPath : "/" + ForwardSlahParentPath;

                sample = System.IO.File.ReadAllText(Program.RootPath("Samples\\SampleView.html")).Replace("/SampleScript", ForwardSlahParentPath  + "/" + FileNameNoExtension);
                filePath = ViewsFolder + "\\" + FileNameNoExtension + ".html";
                if (!System.IO.Directory.Exists(EIO.getDirectoryFullPath(filePath))) EIO.DeleteAndRecreateDirectory((EIO.getDirectoryFullPath(filePath)));
                System.IO.File.WriteAllText(filePath, sample);

                ConsoleSuccess(String.Format("{0} created!", filePath));


                sample = System.IO.File.ReadAllText(Program.RootPath("Samples\\SampleScript.js"));
                filePath = ScriptsFolder + "\\" + FileNameNoExtension + ".js";
                if (!System.IO.Directory.Exists(EIO.getDirectoryFullPath(filePath))) EIO.DeleteAndRecreateDirectory((EIO.getDirectoryFullPath(filePath)));
                System.IO.File.WriteAllText(filePath, sample);

                ConsoleSuccess(String.Format("{0} created!", filePath));



            }
            if (!noService)
            {
                //create service
                sample = System.IO.File.ReadAllText(Program.RootPath("Samples\\SampleService.cs")).Replace("scadware", AppDomain).Replace("SampleService", FileNameNoExtension);

                sample = sample.Replace("api.services", "api.services" + AppendNameSpace);

                filePath = ServiceFolder + "\\" + FileNameNoExtension + ".cs";
                if (!System.IO.Directory.Exists(EIO.getDirectoryFullPath(filePath))) EIO.DeleteAndRecreateDirectory((EIO.getDirectoryFullPath(filePath)));
                System.IO.File.WriteAllText(filePath, sample);

                ConsoleSuccess(String.Format("{0} created!", filePath));


            }
            if (!noController)
            {
                //create controller
                sample = isAPI?
                     System.IO.File.ReadAllText(Program.RootPath("Samples\\SampleControllerAPI.cs")).Replace("scadware", AppDomain).Replace("SampleController", FileNameNoExtension)
                     :
                     System.IO.File.ReadAllText(Program.RootPath("Samples\\SampleController.cs")).Replace("scadware", AppDomain).Replace("SampleController", FileNameNoExtension);

                if (!isAPI)
                {
                    sample = sample.Replace("namespace EWebFramework.controllers", "namespace EWebFramework.controllers" + AppendNameSpace );
                    sample = sample.Replace("//Contents",
                        String.Format("CustomResponse.OutputMasterPageInherited(\"views/{0}.html\");", FullPathSpecified)
                        );
                }
                else
                {
                    sample = sample.Replace("namespace EWebFramework.api.controllers", "namespace EWebFramework.api.controllers" + AppendNameSpace);
                    sample = sample.Replace("//Contents", "response.OutputJSON(new SuccessResult(pData: new SessionHelper(session, request)));");
                }

                sample = sample.Replace("services.SampleService", String.Format("services{0}.{1}", AppendNameSpace, FileNameNoExtension));


                filePath = ControllerFolder + "\\" + FileNameNoExtension + ".cs";
                if (!System.IO.Directory.Exists(EIO.getDirectoryFullPath(filePath))) EIO.DeleteAndRecreateDirectory((EIO.getDirectoryFullPath(filePath)));
                System.IO.File.WriteAllText(filePath, sample);

                ConsoleSuccess(String.Format("{0} created!", filePath));

            }



        }

    
        private static void RunConsole(string[] args)
        {
            String targetURL = ENV.General("APP_URL");

            if (args.Length < 2) throw new Exception("Invalid number of arguments, please check usage!");

            String ConsoleName = args[1];

            targetURL += "/console?p=" + ConsoleName;

            HttpWebRequest request = WebRequest.Create(targetURL) as HttpWebRequest;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
            {
                string responseText = reader.ReadToEnd();
                ConsoleSuccess(responseText.Replace("<p>", "").Replace("</p>", "\n"));
            }


        }

        //private static void RunMinifyJs(string[] args)
        //{
        //    string targetFolder = AppPath();

        //    String ScriptsFolder = targetFolder + "\\assets\\pages\\scripts";


        //    String[] js_files = System.IO.Directory.EnumerateFiles(ScriptsFolder, "*.js", System.IO.SearchOption.AllDirectories).Where(
        //        x => !x.EndsWith(".min.js")
        //        ).ToArray();

        //    foreach (String sJsFile in js_files)
        //    {

        //        EPRO.Library.v3._5.MicrosoftOS.CommandPrompt cm = new EPRO.Library.v3._5.MicrosoftOS.CommandPrompt();
                
        //        Console.WriteLine("--------------------EXECUTING {0}-----------------", EIO.getFileName(sJsFile));

        //        String cmdString = String.Format("java -jar \"{0}\" --compilation_level ADVANCED --js_output_file \"{3}\\{1}.min.js\" --js \"{2}\"",
        //            Program.RootPath("closure-compiler-v20180610.jar"),
        //            EIO.getFileNameWithoutExtension(sJsFile),
        //            sJsFile,
        //            EIO.getDirectoryFullPath(sJsFile)
        //            );

        //        cm.Execute(cmdString);
        //        ConsoleSuccess(cm.Result);
        //        Console.WriteLine("--------------------EXECUTED {0}-----------------", EIO.getFileName(sJsFile));

        //    }




        //}

        private static void PrintConfiguration()
        {
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Configurations");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(System.IO.File.ReadAllText(Program.RootPath(".env")));
        }

        private static void PrintCommandList()
        {
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Available Commands");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("list \t -: show all available commands.");
            Console.WriteLine("config \t -: show all available commands.");
            Console.WriteLine("make");
            Console.WriteLine("  make:item Path/ItemName \t -: Create an item.");
            Console.WriteLine("    make:item \t -: --api  creates item in api folder with no view");
            Console.WriteLine("    make:item \t -: --no-view  creates item with no view");
            Console.WriteLine("    make:item \t -: --no-controller  creates item with no view");
            ConsoleInfo("---------------------------------------------------------------------------------");
            Console.WriteLine("  make:console ItemName \t -: Create a console with console logger.");
            Console.WriteLine("    make:console \t -: --all-loggers  add all available loggers");
            Console.WriteLine("    make:console \t -: --db-logger  add database logger");
            Console.WriteLine("    make:console \t -: --file-logger  add file logger");
            ConsoleInfo("---------------------------------------------------------------------------------");
            Console.WriteLine("  make:job ItemName \t -: Create a job with job logger.");
            Console.WriteLine("    make:job \t -: --all-loggers  add all available loggers");
            Console.WriteLine("    make:job \t -: --db-logger  add database logger");
            Console.WriteLine("    make:job \t -: --file-logger  add file logger");
            ConsoleInfo("---------------------------------------------------------------------------------");
            Console.WriteLine("run");
            Console.WriteLine("  run:console ItemName \t -: run a console.");

            //List Consoles indicated in .env files
            Console.WriteLine(@"  \***********************************************************************\");

            string targetFolder = CodePath("consoles");

            if (System.IO.Directory.Exists(targetFolder))
            {
                foreach (string s in System.IO.Directory.GetFiles(targetFolder))
                {
                    ConsoleInfo("         EPArtisan.exe run:console " + EIO.getFileNameWithoutExtension(s));
                    // Files first line for description
                    ConsoleInfo("                       ( " + System.IO.File.ReadLines(s).FirstOrDefault().Trim('/',' ') + " )" );
                }
            }
           
            Console.WriteLine(@"  \***********************************************************************\");


            //Console.WriteLine("  run:minify-js \t -: run optimizing on assets/pages/scripts/*.js");
            ConsoleInfo("---------------------------------------------------------------------------------");



        }

        static Program()
        {
            ___Logger = new Log1(typeof(Program)).Load(Log1.Modes.FILE, true);
        }

        public static void ConsoleInfo(String contents)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(contents);
            Console.ResetColor();
        }


        public static void ConsoleSuccess(String contents)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(contents);
            Console.ResetColor();
        }

        public static void ConsoleError(String contents)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(contents);
            Console.ResetColor();
        }

        private static readonly Log1 ___Logger;
        public static Log1 Logger
        {
            get { return ___Logger; }
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
        public static string CodePath(string pPath="")
        {
            //One step Up
            return System.Windows.Forms.Application.StartupPath + "\\..\\EWebFramework\\" + pPath;
        }







    }
}
