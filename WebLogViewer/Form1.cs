using System.Runtime.InteropServices.Marshalling;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using System.Data.Common;
using System.Transactions;


namespace WebLogViewer
{
    public partial class formMain : Form
    {
        DataTable logTable = new DataTable();
        DataTable addTable = new DataTable();

        public formMain()
        {
            InitializeComponent();
        }

        private void rbFile_CheckedChanged(object sender, EventArgs e)
        {
            // Clear existing path info.
            txtFileName.Text = "";
        }

        private void rbDirectory_CheckedChanged(object sender, EventArgs e)
        {
            // Clear existing path info.
            txtFileName.Text = "";
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                // If loading a single file, open the open file dialog and get the filename.
                // Otherwise, open the folder browser and get the path.

                if (rbFile.Checked)
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "log files (*.log)|*.log|Zip files (*.zip)|*.zip|GZip Files(*.gz)|*.gz";

                    if (fileDialog.ShowDialog() == DialogResult.OK)
                        txtFileName.Text = fileDialog.FileName;

                    // If a compressed file is selected set the program to process them.
                    if (txtFileName.Text.EndsWith(".GZ", true, LogProcess.ci) || txtFileName.Text.EndsWith(".ZIP", true, LogProcess.ci))
                        chkProcessZIP.Checked = true;
                }
                else
                {
                    FolderBrowserDialog folderDialog = new FolderBrowserDialog();

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                        txtFileName.Text = folderDialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ...");
            }
        }

        private async void cmdLoad_Click(object sender, EventArgs e)
        {
            try
            {
                tssStatus.Text = "Loading log file(s) ...";

                // Get event from LogProcess class for status bar update.
                LogProcess.FileOp += (value) =>
                {
                    if (InvokeRequired)
                        Invoke(new Action(() => tssStatus.Text = value));
                    else
                        tssStatus.Text = value;
                };

                tsStatus.Invalidate();
                Application.DoEvents();

                cmdLoad.Enabled = false;
                // Single file or directory?
                if (rbFile.Checked)
                    addTable = LogProcess.LoadFile(txtFileName.Text, chkProcessZIP.Checked);
                else
                    addTable = await Task.Run(() => LogProcess.LoadDirectory(txtFileName.Text, chkProcessZIP.Checked));

                // Get grid data source if this is an append.
                if (chkAddToList.Checked && dgvLogs.DataSource != null)
                {
                    logTable = (DataTable)dgvLogs.DataSource;
                    logTable.Merge(addTable);
                    dgvLogs.DataSource = logTable;
                }
                else
                {
                    // Otherwise, just put new table into data grid.
                    dgvLogs.DataSource = null;
                    dgvLogs.DataSource = addTable;
                    tssStatus.Text = "Displaying " + addTable.Rows.Count.ToString() + " records.";
                }

                // Show the number of records in the status bar.
                tssStatus.Text = "Displaying " + ((DataTable)dgvLogs.DataSource).Rows.Count + " records.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ...");
                tssStatus.Text = " ";
            }
            finally
            {
                cmdLoad.Enabled = true;
            }



        }

        private void cmdXML_Click(object sender, EventArgs e)
        {
            DataTable dtSave = new DataTable();
            string fileName = "";

            try
            {
                cmdXML.Enabled = false;

                if (dgvLogs.DataSource != null)
                {
                    SaveFileDialog fileDialog = new SaveFileDialog();
                    fileDialog.Filter = "XML files (*.xml)|*.xml";

                    if (fileDialog.ShowDialog() == DialogResult.OK)
                        fileName = fileDialog.FileName;

                    dtSave = (DataTable)dgvLogs.DataSource;
                    dtSave.TableName = "LogData";
                    dtSave.WriteXml(fileName);

                    MessageBox.Show("File saved to :" + fileName);
                }
                else
                {
                    MessageBox.Show("No data is available to export. Please load log files before attempting to export to XML.", "No data loaded ...");
                }
            }
            catch (Exception ex)
            {
                {
                    MessageBox.Show(ex.Message, "Error ...");
                    tssStatus.Text = " ";
                }
            }
            finally { cmdXML.Enabled = true; }
        }

        private void cmdSQLite_Click(object sender, EventArgs e)
        {
            DataTable dtSave = new DataTable();
            string fileName = "";
            SqliteConnection connSQL;
            SqliteCommand cmdSQL;
            SqliteTransaction transSQL;
            String connString, columnDefs, columnList, paramList, tableName;
            int RowCount = 0;

            try
            {
                cmdSQLite.Enabled = false;

                if (dgvLogs.DataSource != null)
                {
                    // Select existing database to receive data.
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "DB files (*.db)|*.db";

                    if (fileDialog.ShowDialog() == DialogResult.OK)
                        fileName = fileDialog.FileName;

                    // Get name for table.
                    tableName = (txtTableName.Text.Length > 0) ? txtTableName.Text : "LogData";

                    // Get current data from grid.
                    dtSave = (DataTable)dgvLogs.DataSource;
                    dtSave.TableName = tableName;

                    // SQLite connection and command
                    connString = $"Data Source = {fileName}";
                    connSQL = new SqliteConnection(connString);
                    connSQL.Open();
                    cmdSQL = connSQL.CreateCommand();

                    // Create columns and parameters from datatable.
                    columnDefs = string.Join(", ", dtSave.Columns.Cast<DataColumn>().Select(c => $"[{c.ColumnName}] TEXT"));
                    columnList = string.Join(", ", dtSave.Columns.Cast<DataColumn>().Select(c => $"[{c.ColumnName}]"));
                    paramList = string.Join(", ", dtSave.Columns.Cast<DataColumn>().Select((c, i) => $"@p{i}"));

                    // Create the table in the database if it doesn't already exist.
                    cmdSQL.CommandText = $"CREATE TABLE IF NOT EXISTS [{dtSave.TableName}] ({columnDefs})";
                    cmdSQL.ExecuteNonQuery();

                    // Create INSERT command and transaction.
                    cmdSQL.CommandText = $"INSERT INTO [{dtSave.TableName}] ({columnList}) VALUES ({paramList})";
                    transSQL = connSQL.BeginTransaction();
                    cmdSQL.Transaction = transSQL;

                    // Setup parameters
                    for (int i = 0; i < dtSave.Columns.Count; i++)
                        cmdSQL.Parameters.Add(new SqliteParameter($"@p{i}", ""));

                    // Iterate through rows, update status bar for every 100 rows
                    foreach (DataRow row in dtSave.Rows)
                    {
                        RowCount++;
                        if (RowCount % 100 == 0)
                        {
                            tssStatus.Text = $"Processing row {RowCount} ...";
                            tsStatus.Invalidate();
                            Application.DoEvents();
                        }
                        // Insert values from current row to parameters and save.
                        for (int i = 0; i < dtSave.Columns.Count; i++)
                            cmdSQL.Parameters[i].Value = row[i] ?? DBNull.Value;

                        cmdSQL.ExecuteNonQuery();
                    }

                    // Commit transaction and update status.
                    transSQL.Commit();
                    tssStatus.Text = ("Data saved to: " + fileName);
                }
                else
                {
                    MessageBox.Show("No data is available to export. Please load log files before attempting to export to XML.", "No data loaded ...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ...");
                tssStatus.Text = " ";
            }
            finally { cmdSQLite.Enabled = true; }

        }
    }
}
