using DB.v4.MS_SQL.Tools;
using EPRO.Library.v3._5;
using EPRO.Library.v3._5.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DatabaseVersioning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.IsInitializing = true;

            InitializeComponent();



            this.persistable = new StatePersisting.ENVPersistent();


            this.txtDatabaseName.Text = this.persistable.masterDBName;
            this.txtServer.Text = this.persistable.masterDBServer;
            this.txtUserName.Text = this.persistable.masterUserName;
            this.txtPassword.Text = this.persistable.masterUserPassword;
            this.txtVersioningFolderLocation.Text = this.persistable.versioningFolder;


            this.IsInitializing = false;

            this.setProgress(0, 0);
            this.setSubProgress(0, 0);

        }





        public void setProgress(int value, int Max, int Min = 0)
        {
            if (value >= Max)
            {
                this.progressBar1.Visible = false;
            }else
                this.progressBar1.Visible = true;


            this.progressBar1.Maximum = Max;
            this.progressBar1.Minimum = Min;
            this.progressBar1.Value = value;
            Application.DoEvents();

        }

        public void setSubProgress(int value, int Max, int Min = 0)
        {
            if (value >= Max)
            {
                this.progressBar2.Visible = false;
            }else
                this.progressBar2.Visible = true;


            this.progressBar2.Maximum = Max;
            this.progressBar2.Minimum = Min;
            this.progressBar2.Value = value;
            Application.DoEvents();

        }




        private bool IsInitializing;
        private StatePersisting.IPersistableState persistable;

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            if (!this.IsInitializing) this.persistable.masterDBServer = this.txtServer.Text;
        }

        private void txtDatabaseName_TextChanged(object sender, EventArgs e)
        {
            if (!this.IsInitializing) this.persistable.masterDBName = this.txtDatabaseName.Text;
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (!this.IsInitializing) this.persistable.masterUserName = this.txtUserName.Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!this.IsInitializing) this.persistable.masterUserPassword = this.txtPassword.Text;
        }

        private void txtVersioningFolderLocation_TextChanged(object sender, EventArgs e)
        {
            if (!this.IsInitializing) this.persistable.versioningFolder = this.txtVersioningFolderLocation.Text;
        }

        private void btnLoadTables_Click(object sender, EventArgs e)
        {
            try
            {
                chkTables.Items.Clear();

                var DBHostSplit = txtServer.Text.Split('\\');
                if (DBHostSplit.Length == 0) throw new Exception("Invalid Host");

                ServerHelperDynamicVersion vDBInit = new ServerHelperDynamicVersion(DatabaseName: txtDatabaseName.Text, pInstanceName: DBHostSplit.Last(),
                    pHostName: ".", pConnectionMode: ServerHelperDynamicVersion.SQLServerConnectionModes.WindowsAuthentication
                    );

                // Console.WriteLine(vDBInit.getConnectionString());
                SqlConnection conn = vDBInit.getSQLConnection();

                if ((conn == null)) throw new Exception("Connection Failed with parameters: " + vDBInit.getConnectionString());

                String SQL = "select                   " + Environment.NewLine +
"  ID,                  " + Environment.NewLine +
"  Description,                  " + Environment.NewLine +
"  Version                  " + Environment.NewLine +
"from system.DBInfo                  " + Environment.NewLine +
"                  ";

                DataTable databases = vDBInit.getRS(
                    SQL: SQL
                    ).Tables[0];

                foreach (DataRow dr in databases.AsEnumerable())
                {
                    chkTables.Items.Add(dr["ID"] + "-" + dr["Description"] + "-" + dr["Version"], true);

                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void btnBrowseVersioningFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbg = new FolderBrowserDialog())
            {
                if (fbg.ShowDialog() == DialogResult.OK)
                {
                    this.txtVersioningFolderLocation.Text = fbg.SelectedPath;
                    this.btnLoadVersions_Click(sender, e);
                }
            }
        }

        private void btnLoadVersions_Click(object sender, EventArgs e)
        {
            try
            {

                String[] files = System.IO.Directory.GetFiles(txtVersioningFolderLocation.Text, "*upgrade.sql");

                IEnumerable<String> versions = from String file in files
                               let v = EIO.getFileNameWithoutExtension(file).Split('-')[0]
                               select v;

                cboConvertToVersion.Items.Clear();
                cboConvertToVersion.Items.Add("0"); // for complete virgin database
                cboConvertToVersion.Items.AddRange(versions.OrderBy(x => x).ToArray());
                MessageBox.Show("Loaded");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {

            try
            {

                if (chkTables.CheckedItems.Count == 0) throw new Exception("Select database to convert!");
                if (cboConvertToVersion.SelectedIndex < 0) throw new Exception("Select a version to convert to");


                if (CODERiT.PopUps.v3._5.ScrollablePopUp.questionMsg("Are you sure ?") != DialogResult.Yes) return;




                Double targetVersion = EDouble.valueOf(cboConvertToVersion.SelectedItem);

                int intCount = 0;
                int checkedDatabaseCounts = chkTables.CheckedItems.Count;

                foreach (String dbToConvert in chkTables.CheckedItems)
                {

                    String[] dbToConvertParts = dbToConvert.Split('-');

                    int dbID = EInt.valueOf(dbToConvertParts[0]);
                    String dbName = EStrings.valueOf(dbToConvertParts[1]);
                    Double dbCurrentVersion = EDouble.valueOf(dbToConvertParts[2]);
                    Console.WriteLine("Processing ...... " + dbName);

                    try
                    {

                        // determine upgrade or downgrade
                        bool isDowngrading = targetVersion < dbCurrentVersion;

                        String[] versionsToConvert = isDowngrading ? cboConvertToVersion.Items.Cast<String>().OrderByDescending(x => x
                            ).Where(
                            x => EDouble.valueOf(x) <= dbCurrentVersion && EDouble.valueOf(x) > targetVersion).ToArray()
                            :
                            cboConvertToVersion.Items.Cast<String>().Where(
                                x => EDouble.valueOf(x) > dbCurrentVersion && EDouble.valueOf(x) <= targetVersion).ToArray();


                        // Target and master are on same database here
                        var DBHostSplit = txtServer.Text.Split('\\');
                        if (DBHostSplit.Length == 0) throw new Exception("Invalid Host");
                        ServerHelperDynamicVersion vDBInitTarget = new ServerHelperDynamicVersion(DatabaseName: txtDatabaseName.Text, pInstanceName: DBHostSplit.Last(),
                   pHostName: ".", pConnectionMode: ServerHelperDynamicVersion.SQLServerConnectionModes.WindowsAuthentication
                   );


                        foreach (String cVersion in versionsToConvert)
                        {

                            String SQLFile = String.Format("{1}\\" + (isDowngrading ? "{0}-downgrade.sql" : "{0}-upgrade.sql"), cVersion, txtVersioningFolderLocation.Text);

                            // Console.WriteLine(vDBInit.getConnectionString());
                            SqlConnection conn = vDBInitTarget.getSQLConnection();

                            //
                            // Process SQL File
                            Console.WriteLine(SQLFile);

                            int iSubCount = 0;
                            String[] queries = System.IO.File.ReadAllText(SQLFile, UTF8Encoding.UTF8).Split(new String[] { "GO;" }, options: StringSplitOptions.RemoveEmptyEntries);
                            using (SqlConnection connection = conn)
                            {
                                SqlCommand command = connection.CreateCommand();
                                SqlTransaction transaction;

                                // Start a local transaction.
                                transaction = connection.BeginTransaction("SampleTransaction");

                                // Must assign both transaction object and connection
                                // to Command object for a pending local transaction
                                command.Connection = connection;
                                command.Transaction = transaction;


                                try
                                {
                                  

                                    foreach (String query in queries)
                                    {
                                        try
                                        {
                                            command.CommandText = query;
                                            command.ExecuteNonQuery();
                                        }
                                        catch (Exception ex3 )
                                        {
                                            throw new Exception(ex3.Message + Environment.NewLine + "QUERY: " + query, ex3);
                                        }
                                        
                                        iSubCount++;
                                        this.setSubProgress(iSubCount, queries.Length);
                                    }


                                    // Attempt to commit the transaction.
                                    transaction.Commit();


                                    //
                                    //
                                    //
                                    String currentVersion = cVersion;
                                    if(isDowngrading)
                                    {
                                        // it should be a version lower
                                        int indexOfCurrentVersion = versionsToConvert.ToList().IndexOf(currentVersion);
                                        if (indexOfCurrentVersion != versionsToConvert.Count()-1)
                                            currentVersion = versionsToConvert.ToList()[indexOfCurrentVersion + 1];
                                        else
                                            currentVersion = targetVersion.ToString(); // we are Execution last downgrade file. So, I should be the target
                                    }


                                    String upVersionSQL = String.Format("UPDATE system.DBInfo set Version ={0} where ID ={1}", currentVersion, dbID);
                                    vDBInitTarget.dbExec(
                                           SQL: upVersionSQL
                                        );
                                    txtConvertedResults.Text += "Completed Database: " + dbName + "  Convertion to " + cVersion + Environment.NewLine + Environment.NewLine;


                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                                    Console.WriteLine("  Message: {0}", ex.Message);

                                    // Attempt to roll back the transaction.
                                    try
                                    {
                                        transaction.Rollback();
                                    }
                                    catch (Exception ex2)
                                    {
                                        // This catch block will handle any errors that may have occurred
                                        // on the server that would cause the rollback to fail, such as
                                        // a closed connection.
                                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                                        Console.WriteLine("  Message: {0}", ex2.Message);
                                    }



                                    txtConvertedResults.Text += "Error Converting Database: " + dbName + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine;


                                }



                            }


                        }





                    }
                    catch (Exception ex)
                    {
                        txtConvertedResults.Text += "Error Converting Database: " + dbName + Environment.NewLine +  ex.Message + Environment.NewLine + Environment.NewLine;
                    }
                  

                 

                    intCount++;
                    this.setProgress(intCount, checkedDatabaseCounts);
                }



                this.btnLoadTables_Click(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }

}
