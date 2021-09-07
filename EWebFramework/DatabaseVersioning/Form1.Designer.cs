namespace DatabaseVersioning
{
    partial class Form1
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
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoadVersions = new System.Windows.Forms.Button();
            this.btnBrowseVersioningFolder = new System.Windows.Forms.Button();
            this.txtVersioningFolderLocation = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkTables = new System.Windows.Forms.CheckedListBox();
            this.btnLoadTables = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboConvertToVersion = new System.Windows.Forms.ComboBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtConvertedResults = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(9, 49);
            this.txtServer.MaxLength = 50;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(252, 20);
            this.txtServer.TabIndex = 1;
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDatabaseName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 239);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Master Dabase Details";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(6, 204);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '+';
            this.txtPassword.Size = new System.Drawing.Size(252, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "User Password";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(6, 154);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(252, 20);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "User Name";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(6, 103);
            this.txtDatabaseName.MaxLength = 50;
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(252, 20);
            this.txtDatabaseName.TabIndex = 1;
            this.txtDatabaseName.TextChanged += new System.EventHandler(this.txtDatabaseName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoadVersions);
            this.groupBox2.Controls.Add(this.btnBrowseVersioningFolder);
            this.groupBox2.Controls.Add(this.txtVersioningFolderLocation);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(320, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 239);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SQL Versioning Folder";
            // 
            // btnLoadVersions
            // 
            this.btnLoadVersions.Location = new System.Drawing.Point(9, 188);
            this.btnLoadVersions.Name = "btnLoadVersions";
            this.btnLoadVersions.Size = new System.Drawing.Size(115, 36);
            this.btnLoadVersions.TabIndex = 4;
            this.btnLoadVersions.Text = "Load Versions";
            this.btnLoadVersions.UseVisualStyleBackColor = true;
            this.btnLoadVersions.Click += new System.EventHandler(this.btnLoadVersions_Click);
            // 
            // btnBrowseVersioningFolder
            // 
            this.btnBrowseVersioningFolder.Location = new System.Drawing.Point(142, 188);
            this.btnBrowseVersioningFolder.Name = "btnBrowseVersioningFolder";
            this.btnBrowseVersioningFolder.Size = new System.Drawing.Size(118, 36);
            this.btnBrowseVersioningFolder.TabIndex = 3;
            this.btnBrowseVersioningFolder.Text = "browse ...";
            this.btnBrowseVersioningFolder.UseVisualStyleBackColor = true;
            this.btnBrowseVersioningFolder.Click += new System.EventHandler(this.btnBrowseVersioningFolder_Click);
            // 
            // txtVersioningFolderLocation
            // 
            this.txtVersioningFolderLocation.Location = new System.Drawing.Point(9, 49);
            this.txtVersioningFolderLocation.MaxLength = 50;
            this.txtVersioningFolderLocation.Multiline = true;
            this.txtVersioningFolderLocation.Name = "txtVersioningFolderLocation";
            this.txtVersioningFolderLocation.ReadOnly = true;
            this.txtVersioningFolderLocation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtVersioningFolderLocation.Size = new System.Drawing.Size(252, 125);
            this.txtVersioningFolderLocation.TabIndex = 1;
            this.txtVersioningFolderLocation.TextChanged += new System.EventHandler(this.txtVersioningFolderLocation_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Location:";
            // 
            // chkTables
            // 
            this.chkTables.FormattingEnabled = true;
            this.chkTables.Location = new System.Drawing.Point(12, 349);
            this.chkTables.Name = "chkTables";
            this.chkTables.Size = new System.Drawing.Size(575, 319);
            this.chkTables.TabIndex = 4;
            // 
            // btnLoadTables
            // 
            this.btnLoadTables.Location = new System.Drawing.Point(16, 289);
            this.btnLoadTables.Name = "btnLoadTables";
            this.btnLoadTables.Size = new System.Drawing.Size(118, 36);
            this.btnLoadTables.TabIndex = 5;
            this.btnLoadTables.Text = "Load tables";
            this.btnLoadTables.UseVisualStyleBackColor = true;
            this.btnLoadTables.Click += new System.EventHandler(this.btnLoadTables_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboConvertToVersion);
            this.groupBox3.Controls.Add(this.btnConvert);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(766, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(267, 239);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Run Versioning";
            // 
            // cboConvertToVersion
            // 
            this.cboConvertToVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConvertToVersion.FormattingEnabled = true;
            this.cboConvertToVersion.Location = new System.Drawing.Point(8, 56);
            this.cboConvertToVersion.Name = "cboConvertToVersion";
            this.cboConvertToVersion.Size = new System.Drawing.Size(251, 21);
            this.cboConvertToVersion.TabIndex = 4;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(142, 188);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(118, 36);
            this.btnConvert.TabIndex = 3;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Convert Selected Tables to Version :";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtConvertedResults);
            this.groupBox4.Location = new System.Drawing.Point(625, 344);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(408, 324);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Conversion Result";
            // 
            // txtConvertedResults
            // 
            this.txtConvertedResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConvertedResults.Location = new System.Drawing.Point(3, 16);
            this.txtConvertedResults.MaxLength = 50;
            this.txtConvertedResults.Multiline = true;
            this.txtConvertedResults.Name = "txtConvertedResults";
            this.txtConvertedResults.ReadOnly = true;
            this.txtConvertedResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConvertedResults.Size = new System.Drawing.Size(402, 305);
            this.txtConvertedResults.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(634, 315);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(396, 10);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Value = 5;
            // 
            // progressBar2
            // 
            this.progressBar2.ForeColor = System.Drawing.SystemColors.Info;
            this.progressBar2.Location = new System.Drawing.Point(634, 299);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(396, 10);
            this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar2.TabIndex = 7;
            this.progressBar2.Value = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 680);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnLoadTables);
            this.Controls.Add(this.chkTables);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Database Versioning Tool";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBrowseVersioningFolder;
        private System.Windows.Forms.TextBox txtVersioningFolderLocation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox chkTables;
        private System.Windows.Forms.Button btnLoadTables;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cboConvertToVersion;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtConvertedResults;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnLoadVersions;
        private System.Windows.Forms.ProgressBar progressBar2;
    }
}

