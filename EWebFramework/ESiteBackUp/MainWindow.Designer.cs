namespace ESiteBackup
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddRunTime = new System.Windows.Forms.Button();
            this.txtAddRunTime = new System.Windows.Forms.TextBox();
            this.lstRunTimes = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBrowseTargetLocation = new System.Windows.Forms.Button();
            this.txtTargetLocation = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblNextRunTime = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.chkBackupFileServer = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtLastRunLog = new System.Windows.Forms.TextBox();
            this.tmrCurrentTime = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDatabaseName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Name to Backup";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabaseName.Location = new System.Drawing.Point(17, 37);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.ReadOnly = true;
            this.txtDatabaseName.Size = new System.Drawing.Size(329, 28);
            this.txtDatabaseName.TabIndex = 1;
            this.txtDatabaseName.Tag = "";
            this.txtDatabaseName.Text = "DFsdfdfs";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(565, 525);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(239, 53);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnAddRunTime);
            this.groupBox2.Controls.Add(this.txtAddRunTime);
            this.groupBox2.Controls.Add(this.lstRunTimes);
            this.groupBox2.Location = new System.Drawing.Point(440, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 248);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Running Intervals (24hrs format)";
            // 
            // btnAddRunTime
            // 
            this.btnAddRunTime.Location = new System.Drawing.Point(291, 39);
            this.btnAddRunTime.Name = "btnAddRunTime";
            this.btnAddRunTime.Size = new System.Drawing.Size(52, 28);
            this.btnAddRunTime.TabIndex = 2;
            this.btnAddRunTime.Text = "Add";
            this.btnAddRunTime.UseVisualStyleBackColor = true;
            this.btnAddRunTime.Click += new System.EventHandler(this.btnAddRunTime_Click);
            // 
            // txtAddRunTime
            // 
            this.txtAddRunTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddRunTime.Location = new System.Drawing.Point(12, 39);
            this.txtAddRunTime.Name = "txtAddRunTime";
            this.txtAddRunTime.Size = new System.Drawing.Size(273, 28);
            this.txtAddRunTime.TabIndex = 2;
            this.txtAddRunTime.Tag = "";
            this.txtAddRunTime.Text = "DFsdfdfs";
            // 
            // lstRunTimes
            // 
            this.lstRunTimes.FormattingEnabled = true;
            this.lstRunTimes.ItemHeight = 16;
            this.lstRunTimes.Location = new System.Drawing.Point(12, 101);
            this.lstRunTimes.Name = "lstRunTimes";
            this.lstRunTimes.Size = new System.Drawing.Size(336, 132);
            this.lstRunTimes.Sorted = true;
            this.lstRunTimes.TabIndex = 0;
            this.lstRunTimes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstRunTimes_MouseDoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBrowseTargetLocation);
            this.groupBox3.Controls.Add(this.txtTargetLocation);
            this.groupBox3.Location = new System.Drawing.Point(12, 282);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(792, 64);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Target Location: (App must have R/W Permission)";
            // 
            // btnBrowseTargetLocation
            // 
            this.btnBrowseTargetLocation.Location = new System.Drawing.Point(704, 33);
            this.btnBrowseTargetLocation.Name = "btnBrowseTargetLocation";
            this.btnBrowseTargetLocation.Size = new System.Drawing.Size(72, 25);
            this.btnBrowseTargetLocation.TabIndex = 1;
            this.btnBrowseTargetLocation.Text = "browse";
            this.btnBrowseTargetLocation.UseVisualStyleBackColor = true;
            this.btnBrowseTargetLocation.Click += new System.EventHandler(this.btnBrowseTargetLocation_Click);
            // 
            // txtTargetLocation
            // 
            this.txtTargetLocation.Location = new System.Drawing.Point(12, 22);
            this.txtTargetLocation.Multiline = true;
            this.txtTargetLocation.Name = "txtTargetLocation";
            this.txtTargetLocation.Size = new System.Drawing.Size(686, 36);
            this.txtTargetLocation.TabIndex = 1;
            this.txtTargetLocation.Text = "ghfhghfghfgh";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblCurrentTime);
            this.groupBox4.Location = new System.Drawing.Point(24, 375);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(159, 88);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Current Time";
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTime.Location = new System.Drawing.Point(9, 32);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(124, 46);
            this.lblCurrentTime.TabIndex = 5;
            this.lblCurrentTime.Text = "12:00";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblNextRunTime);
            this.groupBox5.Location = new System.Drawing.Point(199, 375);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(159, 88);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Next Execution Time";
            // 
            // lblNextRunTime
            // 
            this.lblNextRunTime.AutoSize = true;
            this.lblNextRunTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextRunTime.Location = new System.Drawing.Point(27, 32);
            this.lblNextRunTime.Name = "lblNextRunTime";
            this.lblNextRunTime.Size = new System.Drawing.Size(98, 38);
            this.lblNextRunTime.TabIndex = 5;
            this.lblNextRunTime.Text = "12:00";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Gray;
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(21, 561);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(53, 17);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Runing";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtUserPassword);
            this.groupBox6.Controls.Add(this.txtPort);
            this.groupBox6.Controls.Add(this.txtUsername);
            this.groupBox6.Controls.Add(this.txtHost);
            this.groupBox6.Location = new System.Drawing.Point(12, 149);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(364, 111);
            this.groupBox6.TabIndex = 50;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Server Details";
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserPassword.Location = new System.Drawing.Point(177, 80);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.ReadOnly = true;
            this.txtUserPassword.Size = new System.Drawing.Size(169, 22);
            this.txtUserPassword.TabIndex = 5;
            this.txtUserPassword.Tag = "";
            this.txtUserPassword.Text = "DFsdfdfs";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(271, 37);
            this.txtPort.Name = "txtPort";
            this.txtPort.ReadOnly = true;
            this.txtPort.Size = new System.Drawing.Size(75, 22);
            this.txtPort.TabIndex = 4;
            this.txtPort.Tag = "";
            this.txtPort.Text = "90031";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(17, 80);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.ReadOnly = true;
            this.txtUsername.Size = new System.Drawing.Size(154, 22);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.Tag = "";
            this.txtUsername.Text = "DFsdfdfs";
            // 
            // txtHost
            // 
            this.txtHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHost.Location = new System.Drawing.Point(17, 37);
            this.txtHost.Name = "txtHost";
            this.txtHost.ReadOnly = true;
            this.txtHost.Size = new System.Drawing.Size(248, 22);
            this.txtHost.TabIndex = 2;
            this.txtHost.Tag = "";
            this.txtHost.Text = "localhost\\SQLServer";
            // 
            // chkBackupFileServer
            // 
            this.chkBackupFileServer.AutoSize = true;
            this.chkBackupFileServer.Location = new System.Drawing.Point(29, 103);
            this.chkBackupFileServer.Name = "chkBackupFileServer";
            this.chkBackupFileServer.Size = new System.Drawing.Size(149, 21);
            this.chkBackupFileServer.TabIndex = 6;
            this.chkBackupFileServer.Text = "Backup File Server";
            this.chkBackupFileServer.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtLastRunLog);
            this.groupBox7.Location = new System.Drawing.Point(394, 375);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox7.Size = new System.Drawing.Size(410, 144);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Last Execution Result";
            // 
            // txtLastRunLog
            // 
            this.txtLastRunLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLastRunLog.Location = new System.Drawing.Point(5, 20);
            this.txtLastRunLog.Multiline = true;
            this.txtLastRunLog.Name = "txtLastRunLog";
            this.txtLastRunLog.ReadOnly = true;
            this.txtLastRunLog.Size = new System.Drawing.Size(400, 119);
            this.txtLastRunLog.TabIndex = 0;
            this.txtLastRunLog.Text = "ghfhghfghfgh";
            // 
            // tmrCurrentTime
            // 
            this.tmrCurrentTime.Interval = 5000;
            this.tmrCurrentTime.Tick += new System.EventHandler(this.tmrCurrentTime_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Double click an item to remove.";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 590);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.chkBackupFileServer);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(838, 637);
            this.MinimumSize = new System.Drawing.Size(838, 637);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddRunTime;
        private System.Windows.Forms.TextBox txtAddRunTime;
        private System.Windows.Forms.ListBox lstRunTimes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnBrowseTargetLocation;
        private System.Windows.Forms.TextBox txtTargetLocation;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblNextRunTime;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.CheckBox chkBackupFileServer;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtLastRunLog;
        private System.Windows.Forms.Timer tmrCurrentTime;
        private System.Windows.Forms.Label label1;
    }
}