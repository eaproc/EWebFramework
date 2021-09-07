using EPRO.Library;
using EPRO.Library.Objects;
using ESiteBackup.StatePersisting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CODERiT.PopUps.v3._5.ScrollablePopUp;

namespace ESiteBackup
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            this.AppIniStorage = new StatePersisting.ENVPersistent();
        }



        public enum Mode
        {
            BLOCKED,
            IDLE,
            RUNNING
        }

        private IPersistableState AppIniStorage;

        private bool EXIT_RUNNING_THREAD;

        private void RunThread()
        {
            this.EXIT_RUNNING_THREAD = false;

            try
            {
                this.setMode(Mode.RUNNING);

                while (!this.EXIT_RUNNING_THREAD)
                {



                    try
                    {

                        var curTime = DateTime.Now.TimeOfDay;
                        if(curTime.Hours == this.NextRunningTime.Hours && curTime.Minutes == this.NextRunningTime.Minutes)
                        {
                            // Time to work
                            // Back up database 
                            // ---------------------------------------------------------------------
                            string HOST = ENV.DbConnection("HOST");
                            string DATABASE_PORT = ENV.DbConnection("PORT");
                            string DATABASE_NAME = ENV.DbConnection("DATABASE_NAME");
                            string DATABASE_USER_NAME = ENV.DbConnection("DATABASE_USER_NAME");
                            string DATABASE_USER_PASSWORD = ENV.DbConnection("DATABASE_USER_PASSWORD");

                            SqlConnection cnn;
                            string connetionString = string.Format("Data Source={0},{1};Network Library=DBMSSOCN;Initial Catalog={2};User ID={3};Password={4};Connection Timeout=600", HOST, DATABASE_PORT,
                                "master", DATABASE_USER_NAME, DATABASE_USER_PASSWORD);
                            cnn = new SqlConnection(connetionString);
                            cnn.Open();

                            string BackUpFolderName = DateTime.Now.ToString("yyyy_MM_dd__HH_mm");
                            string BackUpLocation = string.Format("{0}\\{1}", this.AppIniStorage.TargetLocation, BackUpFolderName);
                            string BackUpDBFileFullPath = string.Format("{0}\\{1}__{2}.bak", BackUpLocation, BackUpFolderName, DATABASE_NAME);

                            if (!Directory.Exists(BackUpLocation)) EIO.DeleteAndRecreateDirectory(BackUpLocation);

                            string SQL = string.Format(
            "                  " + Environment.NewLine +
            "BACKUP DATABASE {0}                  " + Environment.NewLine +
            @" TO DISK = '{1}'                  " + Environment.NewLine +
            "    WITH FORMAT;                  ", DATABASE_NAME, BackUpDBFileFullPath
            );
                            SqlCommand cmd = new SqlCommand(SQL, cnn);
                            cmd.CommandTimeout = 0;  // no time out here
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.ExecuteNonQuery();

                            // ---------------------------------------------------------------------------------------------------------



                            // --------------------------------FILE SERVER BACK UP------------------------------------------------------

                            string ExtraLog = string.Empty;

                            try
                            {

                                string App_Resources = Program.AppPath("App_Resources");
                                string zipPath = string.Format("{0}\\{1}__App_Resources.zip", BackUpLocation, BackUpFolderName);

                                if(EBoolean.valueOf(this.AppIniStorage.BackupFileServer) && Directory.Exists(App_Resources))
                                    System.IO.Compression.ZipFile.CreateFromDirectory(App_Resources, zipPath);

                                ExtraLog = "Backed Up File Server as well.";

                            }
                            catch (Exception ex2)
                            {
                                ExtraLog = ex2.Message;
                            }

                            // ---------------------------------------------------------------------------------------------------------





                            this.setLastRunLog(string.Format("Job Done at {0}. {1}", DateTime.Now.ToString(), ExtraLog));

                            System.Threading.Thread.Sleep(50000); // Delay for a minute to be sure we are out of the current minute
                        }


                    }
                    catch (Exception ex)
                    {
                        // Error on Execution
                        this.setLastRunLog(ex.Message + " - Occurred at " + DateTime.Now.ToString() );
                        Program.Logger.Print(ex);
                    }




                    // Reset Next Time if it has passed for some reason
                    if (DateTime.Now.TimeOfDay > this.NextRunningTime) this.SetNextRunningTime();


                    System.Threading.Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {

                Program.Logger.Print(ex);
            }

            this.setMode(Mode.IDLE);

        }

        private TimeSpan NextRunningTime;
        private void SetNextRunningTime()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => this.SetNextRunningTime()));
                return;
            }

            var cTime = DateTime.Now.TimeOfDay;
            IEnumerable<TimeSpan> runningTimes =
                this.lstRunTimes.Items.Cast<string>().Select(x => new TimeSpan(
                    hours: EInt.valueOf(x.Split(':')[0]),
                minutes: EInt.valueOf(x.Split(':')[1]),
                seconds: 0)).OrderBy(x => x);

            var laterTime = runningTimes.Where(x => x > cTime).ToList();

            if (laterTime.Count > 0)
                this.NextRunningTime = laterTime.First();
            else
                this.NextRunningTime = runningTimes.First();


            this.lblNextRunTime.Text = this.NextRunningTime.ToString(@"hh\:mm");

        }


        public void setLastRunLog(string s)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => this.setLastRunLog(s)));
                return;
            }

            this.txtLastRunLog.Text = s;

        }

            public void setMode(Mode mode)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => this.setMode(mode)));
                return;
            }


            switch(mode)
            {
                case Mode.IDLE:
                    btnStart.Text = "Start";
                    btnStart.Enabled = true;

                    lblNextRunTime.Visible = false;

                    lblStatus.Text = "Stopped";
                    lblStatus.BackColor = Color.Gray;
                    lblStatus.ForeColor = Color.Gray;


                    btnAddRunTime.Enabled = true;
                    lstRunTimes.Enabled = true;
                    btnBrowseTargetLocation.Enabled = true;
                    txtTargetLocation.ReadOnly = false;

                    chkBackupFileServer.Enabled = true;

                    break;

                case Mode.BLOCKED:

                    btnStart.Text = "Processing";
                    btnStart.Enabled = false;



                    btnAddRunTime.Enabled = false;
                    lstRunTimes.Enabled = false;
                    btnBrowseTargetLocation.Enabled = false;
                    txtTargetLocation.ReadOnly = true;

                    chkBackupFileServer.Enabled = false;


                    break;

                case Mode.RUNNING:

                    btnStart.Text = "Stop";
                    btnStart.Enabled = true;

                    lblNextRunTime.Visible = true;

                    lblStatus.Text = "Running";
                    lblStatus.BackColor = Color.Green;
                    lblStatus.ForeColor = Color.Green;


                    btnAddRunTime.Enabled = false;
                    lstRunTimes.Enabled = false;
                    btnBrowseTargetLocation.Enabled = false;
                    txtTargetLocation.ReadOnly = true;

                    chkBackupFileServer.Enabled = false;

                    break;

            }

        }


        private void MainWindow_Load(object sender, EventArgs e)
        {


            string HOST = ENV.DbConnection("HOST");
            string DATABASE_PORT = ENV.DbConnection("PORT");
            string DATABASE_NAME = ENV.DbConnection("DATABASE_NAME");
            string DATABASE_USER_NAME = ENV.DbConnection("DATABASE_USER_NAME");
            string DATABASE_USER_PASSWORD = ENV.DbConnection("DATABASE_USER_PASSWORD");


            this.txtHost.Text = HOST;
            this.txtPort.Text = DATABASE_PORT;
            this.txtUsername.Text = DATABASE_USER_NAME;
            this.txtUserPassword.Text = DATABASE_USER_PASSWORD;
            this.txtDatabaseName.Text = DATABASE_NAME;


            this.chkBackupFileServer.Checked = EBoolean.valueOf(ENV.ESiteBackup("BackupFileServer"));
            this.txtTargetLocation.Text = ENV.ESiteBackup("TargetLocation");

            this.lstRunTimes.Items.Clear();
            var RunTimes = ENV.ESiteBackup("RunTimes");
            if(RunTimes!=null && RunTimes != string.Empty)
                this.lstRunTimes.Items.AddRange(RunTimes.Split(',') );



            this.Text = Application.ProductName + " - " + DATABASE_NAME;



            txtLastRunLog.Text = string.Empty;
            txtAddRunTime.Text = string.Empty;


            this.setMode(Mode.IDLE);

            tmrCurrentTime.Enabled = true;
            tmrCurrentTime_Tick(null, null);
        }

        private void tmrCurrentTime_Tick(object sender, EventArgs e)
        {
            this.lblCurrentTime.Text = DateTime.Now.ToString("HH:mm");
        }

        private void btnBrowseTargetLocation_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbg = new FolderBrowserDialog())
            {
                if (fbg.ShowDialog() == DialogResult.OK)
                {
                    this.txtTargetLocation.Text = fbg.SelectedPath;
                }
            }
        }

        private void lstRunTimes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lstRunTimes.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                lstRunTimes.Items.RemoveAt(index);
            }
        }

        private void btnAddRunTime_Click(object sender, EventArgs e)
        {
            try
            {
                string[] s = txtAddRunTime.Text.Split(':').Select(x => x.Trim()).ToArray();
                int hr = int.Parse(s[0]);
                int min = int.Parse(s[1]);
                if (hr < 0 || hr > 23 || min < 0 || min > 59) throw new Exception("Invalid time value");
                this.lstRunTimes.Items.Add(txtAddRunTime.Text);
                this.txtAddRunTime.Text = string.Empty;
            }
            catch (Exception)
            {
                errorMsg("Please, enter a valid 24hrs time!");
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            try
            {

                switch (btnStart.Text)
                {
                    case "Start":
                        if (this.lstRunTimes.Items.Count == 0) throw new Exception("Please, add running time.");

                        if (this.txtTargetLocation.Text == string.Empty || !Directory.Exists(txtTargetLocation.Text)) throw new Exception("Please, select a valid target path.");


                        this.SetNextRunningTime();


                        this.setMode(Mode.BLOCKED);

                        System.Threading.Thread thread = new System.Threading.Thread(this.RunThread);
                        thread.IsBackground = true;
                        thread.Start();


                        break;

                    case "Stop":
                        this.setMode(Mode.BLOCKED);
                        this.EXIT_RUNNING_THREAD = true;
                        break;
                }

                this.btnStart.Enabled = false;

                this.AppIniStorage.BackupFileServer = chkBackupFileServer.Checked.ToString();
                this.AppIniStorage.TargetLocation = txtTargetLocation.Text;
                this.AppIniStorage.RunTimes = string.Join(",", this.lstRunTimes.Items.Cast<string>().ToArray());


            }
            catch (Exception ex)
            {
                errorMsg(ex.Message);
            }

        }
    }
}
