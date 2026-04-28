namespace WebLogViewer
{
    partial class formMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            rbDirectory = new RadioButton();
            rbFile = new RadioButton();
            txtFileName = new TextBox();
            cmdBrowse = new Button();
            cmdLoad = new Button();
            chkProcessZIP = new CheckBox();
            chkAddToList = new CheckBox();
            dgvLogs = new DataGridView();
            statusMain = new StatusStrip();
            tsStatus = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            tssStatus = new ToolStripStatusLabel();
            cmdXML = new Button();
            cmdSQLite = new Button();
            txtTableName = new TextBox();
            lblTableName = new Label();
            tipHelpText = new ToolTip(components);
            btnClear = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            statusMain.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(rbDirectory);
            panel1.Controls.Add(rbFile);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(121, 59);
            panel1.TabIndex = 0;
            // 
            // rbDirectory
            // 
            rbDirectory.AutoSize = true;
            rbDirectory.Location = new Point(3, 28);
            rbDirectory.Name = "rbDirectory";
            rbDirectory.Size = new Size(112, 19);
            rbDirectory.TabIndex = 1;
            rbDirectory.TabStop = true;
            rbDirectory.Text = "Import Directory";
            tipHelpText.SetToolTip(rbDirectory, "Import contents of a directory.");
            rbDirectory.UseVisualStyleBackColor = true;
            rbDirectory.CheckedChanged += rbDirectory_CheckedChanged;
            // 
            // rbFile
            // 
            rbFile.AutoSize = true;
            rbFile.Location = new Point(3, 3);
            rbFile.Name = "rbFile";
            rbFile.Size = new Size(82, 19);
            rbFile.TabIndex = 0;
            rbFile.TabStop = true;
            rbFile.Text = "Import File";
            tipHelpText.SetToolTip(rbFile, "Select single file for import.");
            rbFile.UseVisualStyleBackColor = true;
            rbFile.CheckedChanged += rbFile_CheckedChanged;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(139, 40);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(525, 23);
            txtFileName.TabIndex = 1;
            // 
            // cmdBrowse
            // 
            cmdBrowse.Location = new Point(139, 69);
            cmdBrowse.Name = "cmdBrowse";
            cmdBrowse.Size = new Size(75, 23);
            cmdBrowse.TabIndex = 4;
            cmdBrowse.Text = "Browse";
            tipHelpText.SetToolTip(cmdBrowse, "Search for directory or file.");
            cmdBrowse.UseVisualStyleBackColor = true;
            cmdBrowse.Click += cmdBrowse_Click;
            // 
            // cmdLoad
            // 
            cmdLoad.Location = new Point(220, 69);
            cmdLoad.Name = "cmdLoad";
            cmdLoad.Size = new Size(75, 23);
            cmdLoad.TabIndex = 5;
            cmdLoad.Text = "Load";
            tipHelpText.SetToolTip(cmdLoad, "Load specified files into the form's grid for review.");
            cmdLoad.UseVisualStyleBackColor = true;
            cmdLoad.Click += cmdLoad_Click;
            // 
            // chkProcessZIP
            // 
            chkProcessZIP.AutoSize = true;
            chkProcessZIP.Checked = true;
            chkProcessZIP.CheckState = CheckState.Checked;
            chkProcessZIP.Location = new Point(338, 72);
            chkProcessZIP.Name = "chkProcessZIP";
            chkProcessZIP.Size = new Size(157, 19);
            chkProcessZIP.TabIndex = 6;
            chkProcessZIP.Text = "Process compressed files";
            tipHelpText.SetToolTip(chkProcessZIP, "When checked, any ZIP or GZ files specified will be extracted and the log files will be imported.");
            chkProcessZIP.UseVisualStyleBackColor = true;
            // 
            // chkAddToList
            // 
            chkAddToList.AutoSize = true;
            chkAddToList.Location = new Point(501, 72);
            chkAddToList.Name = "chkAddToList";
            chkAddToList.Size = new Size(163, 19);
            chkAddToList.TabIndex = 7;
            chkAddToList.Text = "Add records to current list";
            tipHelpText.SetToolTip(chkAddToList, "Append specified files to currently displayed entries.");
            chkAddToList.UseVisualStyleBackColor = true;
            // 
            // dgvLogs
            // 
            dgvLogs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Location = new Point(12, 98);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.Size = new Size(814, 340);
            dgvLogs.TabIndex = 8;
            // 
            // statusMain
            // 
            statusMain.Items.AddRange(new ToolStripItem[] { tsStatus, toolStripStatusLabel1, tssStatus });
            statusMain.Location = new Point(0, 479);
            statusMain.Name = "statusMain";
            statusMain.Size = new Size(854, 22);
            statusMain.TabIndex = 9;
            statusMain.Text = "statusStrip1";
            // 
            // tsStatus
            // 
            tsStatus.Name = "tsStatus";
            tsStatus.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // tssStatus
            // 
            tssStatus.Name = "tssStatus";
            tssStatus.Size = new Size(10, 17);
            tssStatus.Text = " ";
            // 
            // cmdXML
            // 
            cmdXML.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdXML.Location = new Point(719, 444);
            cmdXML.Name = "cmdXML";
            cmdXML.Size = new Size(107, 23);
            cmdXML.TabIndex = 10;
            cmdXML.Text = "Export to XML";
            tipHelpText.SetToolTip(cmdXML, "Export displayed data to an XML file.");
            cmdXML.UseVisualStyleBackColor = true;
            cmdXML.Click += cmdXML_Click;
            // 
            // cmdSQLite
            // 
            cmdSQLite.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdSQLite.Location = new Point(593, 444);
            cmdSQLite.Name = "cmdSQLite";
            cmdSQLite.Size = new Size(120, 23);
            cmdSQLite.TabIndex = 11;
            cmdSQLite.Text = "Export to SQLite";
            tipHelpText.SetToolTip(cmdSQLite, "Export displayed data to a SQLite database file.");
            cmdSQLite.UseVisualStyleBackColor = true;
            cmdSQLite.Click += cmdSQLite_Click;
            // 
            // txtTableName
            // 
            txtTableName.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtTableName.Location = new Point(433, 445);
            txtTableName.Name = "txtTableName";
            txtTableName.Size = new Size(154, 23);
            txtTableName.TabIndex = 12;
            txtTableName.Text = "LogData";
            tipHelpText.SetToolTip(txtTableName, "Enter a name for the export table or accept the default of 'LogData'.");
            // 
            // lblTableName
            // 
            lblTableName.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblTableName.AutoSize = true;
            lblTableName.Location = new Point(261, 448);
            lblTableName.Name = "lblTableName";
            lblTableName.Size = new Size(166, 15);
            lblTableName.TabIndex = 13;
            lblTableName.Text = "Enter name for exported table:";
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClear.Location = new Point(12, 444);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 14;
            btnClear.Text = "Clear Grid";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // formMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(854, 501);
            Controls.Add(btnClear);
            Controls.Add(lblTableName);
            Controls.Add(txtTableName);
            Controls.Add(cmdSQLite);
            Controls.Add(cmdXML);
            Controls.Add(statusMain);
            Controls.Add(dgvLogs);
            Controls.Add(chkAddToList);
            Controls.Add(chkProcessZIP);
            Controls.Add(cmdLoad);
            Controls.Add(cmdBrowse);
            Controls.Add(txtFileName);
            Controls.Add(panel1);
            Name = "formMain";
            Text = "Web Log Viewer";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            statusMain.ResumeLayout(false);
            statusMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private RadioButton rbDirectory;
        private RadioButton rbFile;
        private TextBox txtFileName;
        private Button cmdBrowse;
        private Button cmdLoad;
        private CheckBox chkProcessZIP;
        private CheckBox chkAddToList;
        private DataGridView dgvLogs;
        private StatusStrip statusMain;
        private ToolStripStatusLabel tsStatus;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel tssStatus;
        private Button cmdXML;
        private Button cmdSQLite;
        private TextBox txtTableName;
        private Label lblTableName;
        private ToolTip tipHelpText;
        private Button btnClear;
    }
}
