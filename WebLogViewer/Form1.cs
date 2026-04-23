using System.Runtime.InteropServices.Marshalling;
using System.Data;
using System.Threading.Tasks;


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
    }
}
