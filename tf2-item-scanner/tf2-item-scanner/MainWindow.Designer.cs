using tf2_item_scanner.utils;
using System.Drawing;
using System.Windows.Forms;
namespace tf2_item_scanner
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teamFortress2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSGOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.startingIdTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusRadio = new System.Windows.Forms.RadioButton();
            this.statusGroup = new System.Windows.Forms.GroupBox();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.groupScanningRadio = new System.Windows.Forms.RadioButton();
            this.depthRadio = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.apiTextBox = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.startBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.depthGroup = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.itemsCountTextBox = new System.Windows.Forms.TextBox();
            this.profilesCountTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.onlineTextBox = new System.Windows.Forms.TextBox();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.playtimeTextBox = new System.Windows.Forms.TextBox();
            this.selectedDataGrid = new System.Windows.Forms.DataGridView();
            this.selectedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.stockColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.strangeColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.vintageColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.uniqueColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.genuineColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.unusualColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.hauntedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.leftListBox = new System.Windows.Forms.ListBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.schemaUpdaterWorker = new System.ComponentModel.BackgroundWorker();
            this.scanningWorker = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.itemsTab = new System.Windows.Forms.TabPage();
            this.deleteButton = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.clearResultsBtn = new System.Windows.Forms.Button();
            this.resultsDataGrid = new System.Windows.Forms.DataGridView();
            this.Column13 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.treeViewGroupBox = new System.Windows.Forms.GroupBox();
            this.historyBtn = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusScanWorker = new System.ComponentModel.BackgroundWorker();
            this.groupScanningGroup = new System.Windows.Forms.GroupBox();
            this.groupNameTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.depthGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectedDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.itemsTab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).BeginInit();
            this.treeViewGroupBox.SuspendLayout();
            this.contextMenuTreeView.SuspendLayout();
            this.groupScanningGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 704);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(980, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(39, 17);
            this.StatusLabel.Text = "Ready";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Menu;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(980, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateSchemaToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.toolStripMenuItem2,
            this.saveScanToolStripMenuItem,
            this.loadScanToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // updateSchemaToolStripMenuItem
            // 
            this.updateSchemaToolStripMenuItem.Name = "updateSchemaToolStripMenuItem";
            this.updateSchemaToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.updateSchemaToolStripMenuItem.Text = "Update schema";
            this.updateSchemaToolStripMenuItem.Click += new System.EventHandler(this.updateSchemaToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.teamFortress2ToolStripMenuItem,
            this.cSGOToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // teamFortress2ToolStripMenuItem
            // 
            this.teamFortress2ToolStripMenuItem.Name = "teamFortress2ToolStripMenuItem";
            this.teamFortress2ToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.teamFortress2ToolStripMenuItem.Text = "Team Fortress 2";
            this.teamFortress2ToolStripMenuItem.Click += new System.EventHandler(this.teamFortress2ToolStripMenuItem_Click);
            // 
            // cSGOToolStripMenuItem
            // 
            this.cSGOToolStripMenuItem.Name = "cSGOToolStripMenuItem";
            this.cSGOToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.cSGOToolStripMenuItem.Text = "CS : GO";
            this.cSGOToolStripMenuItem.Click += new System.EventHandler(this.cSGOToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(153, 6);
            // 
            // saveScanToolStripMenuItem
            // 
            this.saveScanToolStripMenuItem.Name = "saveScanToolStripMenuItem";
            this.saveScanToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.saveScanToolStripMenuItem.Text = "Save Scans";
            this.saveScanToolStripMenuItem.Click += new System.EventHandler(this.saveScanToolStripMenuItem_Click);
            // 
            // loadScanToolStripMenuItem
            // 
            this.loadScanToolStripMenuItem.Name = "loadScanToolStripMenuItem";
            this.loadScanToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.loadScanToolStripMenuItem.Text = "Load Scans";
            this.loadScanToolStripMenuItem.Click += new System.EventHandler(this.loadScanToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(153, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Starting ID";
            // 
            // startingIdTextBox
            // 
            this.startingIdTextBox.Location = new System.Drawing.Point(98, 23);
            this.startingIdTextBox.Name = "startingIdTextBox";
            this.startingIdTextBox.Size = new System.Drawing.Size(254, 20);
            this.startingIdTextBox.TabIndex = 3;
            this.startingIdTextBox.Text = global::tf2_item_scanner.Properties.Settings.Default.StartingId;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.statusRadio);
            this.groupBox1.Controls.Add(this.statusGroup);
            this.groupBox1.Controls.Add(this.groupScanningRadio);
            this.groupBox1.Controls.Add(this.depthRadio);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.depthGroup);
            this.groupBox1.Location = new System.Drawing.Point(197, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(774, 284);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // statusRadio
            // 
            this.statusRadio.AutoSize = true;
            this.statusRadio.Location = new System.Drawing.Point(425, 78);
            this.statusRadio.Name = "statusRadio";
            this.statusRadio.Size = new System.Drawing.Size(14, 13);
            this.statusRadio.TabIndex = 22;
            this.statusRadio.UseVisualStyleBackColor = true;
            this.statusRadio.Click += new System.EventHandler(this.statusRadio_CheckedChanged);
            // 
            // statusGroup
            // 
            this.statusGroup.Controls.Add(this.statusTextBox);
            this.statusGroup.Enabled = false;
            this.statusGroup.Location = new System.Drawing.Point(445, 22);
            this.statusGroup.Name = "statusGroup";
            this.statusGroup.Size = new System.Drawing.Size(323, 128);
            this.statusGroup.TabIndex = 21;
            this.statusGroup.TabStop = false;
            this.statusGroup.Text = "Status scanning";
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(6, 19);
            this.statusTextBox.Multiline = true;
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.statusTextBox.Size = new System.Drawing.Size(311, 103);
            this.statusTextBox.TabIndex = 0;
            this.statusTextBox.Text = "Paste status here";
            // 
            // groupScanningRadio
            // 
            this.groupScanningRadio.AutoSize = true;
            this.groupScanningRadio.Checked = true;
            this.groupScanningRadio.Location = new System.Drawing.Point(13, 247);
            this.groupScanningRadio.Name = "groupScanningRadio";
            this.groupScanningRadio.Size = new System.Drawing.Size(14, 13);
            this.groupScanningRadio.TabIndex = 20;
            this.groupScanningRadio.TabStop = true;
            this.groupScanningRadio.UseVisualStyleBackColor = true;
            this.groupScanningRadio.Click += new System.EventHandler(this.groupScanningRadio_CheckedChanged);
            // 
            // depthRadio
            // 
            this.depthRadio.AutoSize = true;
            this.depthRadio.Checked = true;
            this.depthRadio.Location = new System.Drawing.Point(11, 74);
            this.depthRadio.Name = "depthRadio";
            this.depthRadio.Size = new System.Drawing.Size(14, 13);
            this.depthRadio.TabIndex = 20;
            this.depthRadio.TabStop = true;
            this.depthRadio.UseVisualStyleBackColor = true;
            this.depthRadio.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.apiTextBox);
            this.groupBox2.Controls.Add(this.linkLabel1);
            this.groupBox2.Controls.Add(this.startBtn);
            this.groupBox2.Controls.Add(this.stopBtn);
            this.groupBox2.Location = new System.Drawing.Point(33, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(734, 60);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "API key";
            // 
            // apiTextBox
            // 
            this.apiTextBox.Location = new System.Drawing.Point(58, 19);
            this.apiTextBox.Name = "apiTextBox";
            this.apiTextBox.Size = new System.Drawing.Size(270, 20);
            this.apiTextBox.TabIndex = 14;
            this.apiTextBox.Text = global::tf2_item_scanner.Properties.Settings.Default.ApiKey;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(25, 29);
            this.linkLabel1.Location = new System.Drawing.Point(81, 42);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(157, 17);
            this.linkLabel1.TabIndex = 18;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "You can get your API key here";
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(553, 19);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(80, 28);
            this.startBtn.TabIndex = 12;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(655, 19);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(73, 28);
            this.stopBtn.TabIndex = 13;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // depthGroup
            // 
            this.depthGroup.Controls.Add(this.label1);
            this.depthGroup.Controls.Add(this.label8);
            this.depthGroup.Controls.Add(this.startingIdTextBox);
            this.depthGroup.Controls.Add(this.itemsCountTextBox);
            this.depthGroup.Controls.Add(this.profilesCountTextBox);
            this.depthGroup.Controls.Add(this.label5);
            this.depthGroup.Controls.Add(this.label2);
            this.depthGroup.Controls.Add(this.onlineTextBox);
            this.depthGroup.Controls.Add(this.valueTextBox);
            this.depthGroup.Controls.Add(this.label4);
            this.depthGroup.Controls.Add(this.label3);
            this.depthGroup.Controls.Add(this.playtimeTextBox);
            this.depthGroup.Location = new System.Drawing.Point(33, 22);
            this.depthGroup.Name = "depthGroup";
            this.depthGroup.Size = new System.Drawing.Size(373, 128);
            this.depthGroup.TabIndex = 18;
            this.depthGroup.TabStop = false;
            this.depthGroup.Text = "Depth scanning";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Items count";
            // 
            // itemsCountTextBox
            // 
            this.itemsCountTextBox.Location = new System.Drawing.Point(98, 49);
            this.itemsCountTextBox.Name = "itemsCountTextBox";
            this.itemsCountTextBox.Size = new System.Drawing.Size(72, 20);
            this.itemsCountTextBox.TabIndex = 16;
            // 
            // profilesCountTextBox
            // 
            this.profilesCountTextBox.Location = new System.Drawing.Point(278, 49);
            this.profilesCountTextBox.Name = "profilesCountTextBox";
            this.profilesCountTextBox.Size = new System.Drawing.Size(74, 20);
            this.profilesCountTextBox.TabIndex = 4;
            this.profilesCountTextBox.Text = "100";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Last online";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Profiles to scan";
            // 
            // onlineTextBox
            // 
            this.onlineTextBox.Location = new System.Drawing.Point(98, 100);
            this.onlineTextBox.Name = "onlineTextBox";
            this.onlineTextBox.Size = new System.Drawing.Size(72, 20);
            this.onlineTextBox.TabIndex = 10;
            this.onlineTextBox.Text = "1000";
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(98, 75);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(72, 20);
            this.valueTextBox.TabIndex = 6;
            this.valueTextBox.Text = "1000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Playtime";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Backpack value";
            // 
            // playtimeTextBox
            // 
            this.playtimeTextBox.Location = new System.Drawing.Point(278, 75);
            this.playtimeTextBox.Name = "playtimeTextBox";
            this.playtimeTextBox.Size = new System.Drawing.Size(74, 20);
            this.playtimeTextBox.TabIndex = 8;
            this.playtimeTextBox.Text = "1000";
            // 
            // selectedDataGrid
            // 
            this.selectedDataGrid.AllowUserToAddRows = false;
            this.selectedDataGrid.AllowUserToResizeRows = false;
            this.selectedDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.selectedDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.selectedDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectedDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selectedColumn,
            this.nameColumn,
            this.imageColumn,
            this.stockColumn,
            this.strangeColumn,
            this.vintageColumn,
            this.uniqueColumn,
            this.genuineColumn,
            this.unusualColumn,
            this.hauntedColumn});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.selectedDataGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.selectedDataGrid.Location = new System.Drawing.Point(220, 45);
            this.selectedDataGrid.Name = "selectedDataGrid";
            this.selectedDataGrid.RowHeadersVisible = false;
            this.selectedDataGrid.Size = new System.Drawing.Size(543, 364);
            this.selectedDataGrid.TabIndex = 4;
            this.selectedDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.selectedCell_ContentClick);
            // 
            // selectedColumn
            // 
            this.selectedColumn.FalseValue = "false";
            this.selectedColumn.FillWeight = 26.28842F;
            this.selectedColumn.HeaderText = "";
            this.selectedColumn.IndeterminateValue = "false";
            this.selectedColumn.Name = "selectedColumn";
            this.selectedColumn.TrueValue = "true";
            // 
            // nameColumn
            // 
            this.nameColumn.FillWeight = 194.7304F;
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            // 
            // imageColumn
            // 
            this.imageColumn.FillWeight = 44.00686F;
            this.imageColumn.HeaderText = "Image";
            this.imageColumn.Name = "imageColumn";
            // 
            // stockColumn
            // 
            this.stockColumn.FillWeight = 36.1223F;
            this.stockColumn.HeaderText = "STK";
            this.stockColumn.IndeterminateValue = "false";
            this.stockColumn.Name = "stockColumn";
            // 
            // strangeColumn
            // 
            this.strangeColumn.FillWeight = 36.1223F;
            this.strangeColumn.HeaderText = "STR";
            this.strangeColumn.IndeterminateValue = "false";
            this.strangeColumn.Name = "strangeColumn";
            // 
            // vintageColumn
            // 
            this.vintageColumn.FillWeight = 36.1223F;
            this.vintageColumn.HeaderText = "VTG";
            this.vintageColumn.IndeterminateValue = "false";
            this.vintageColumn.Name = "vintageColumn";
            // 
            // uniqueColumn
            // 
            this.uniqueColumn.FillWeight = 36.1223F;
            this.uniqueColumn.HeaderText = "UNQ";
            this.uniqueColumn.IndeterminateValue = "false";
            this.uniqueColumn.Name = "uniqueColumn";
            // 
            // genuineColumn
            // 
            this.genuineColumn.FillWeight = 36.1223F;
            this.genuineColumn.HeaderText = "GEN";
            this.genuineColumn.IndeterminateValue = "false";
            this.genuineColumn.Name = "genuineColumn";
            // 
            // unusualColumn
            // 
            this.unusualColumn.FillWeight = 36.1223F;
            this.unusualColumn.HeaderText = "UNU";
            this.unusualColumn.IndeterminateValue = "false";
            this.unusualColumn.Name = "unusualColumn";
            // 
            // hauntedColumn
            // 
            this.hauntedColumn.FillWeight = 36.1223F;
            this.hauntedColumn.HeaderText = "HAU";
            this.hauntedColumn.IndeterminateValue = "false";
            this.hauntedColumn.Name = "hauntedColumn";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Search";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(55, 8);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(347, 20);
            this.searchTextBox.TabIndex = 2;
            this.searchTextBox.TextChanged += new System.EventHandler(this.Search_TextChanged);
            // 
            // leftListBox
            // 
            this.leftListBox.FormattingEnabled = true;
            this.leftListBox.Location = new System.Drawing.Point(6, 45);
            this.leftListBox.Name = "leftListBox";
            this.leftListBox.Size = new System.Drawing.Size(193, 368);
            this.leftListBox.TabIndex = 0;
            this.leftListBox.DoubleClick += new System.EventHandler(this.leftListBox_DoubleClick);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // schemaUpdaterWorker
            // 
            this.schemaUpdaterWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.schemaUpdaterWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // scanningWorker
            // 
            this.scanningWorker.WorkerReportsProgress = true;
            this.scanningWorker.WorkerSupportsCancellation = true;
            this.scanningWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.scanningWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.scanningWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.itemsTab);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(197, 317);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(778, 384);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabColtrol1_SelectedIndexChanged);
            // 
            // itemsTab
            // 
            this.itemsTab.BackColor = System.Drawing.Color.WhiteSmoke;
            this.itemsTab.Controls.Add(this.deleteButton);
            this.itemsTab.Controls.Add(this.clearBtn);
            this.itemsTab.Controls.Add(this.selectedDataGrid);
            this.itemsTab.Controls.Add(this.leftListBox);
            this.itemsTab.Controls.Add(this.searchTextBox);
            this.itemsTab.Controls.Add(this.label6);
            this.itemsTab.Location = new System.Drawing.Point(4, 22);
            this.itemsTab.Name = "itemsTab";
            this.itemsTab.Padding = new System.Windows.Forms.Padding(3);
            this.itemsTab.Size = new System.Drawing.Size(770, 358);
            this.itemsTab.TabIndex = 0;
            this.itemsTab.Text = "Items";
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(598, 6);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(688, 6);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 23);
            this.clearBtn.TabIndex = 5;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.clearResultsBtn);
            this.tabPage2.Controls.Add(this.resultsDataGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(770, 358);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Results";
            // 
            // clearResultsBtn
            // 
            this.clearResultsBtn.Location = new System.Drawing.Point(688, 3);
            this.clearResultsBtn.Name = "clearResultsBtn";
            this.clearResultsBtn.Size = new System.Drawing.Size(75, 23);
            this.clearResultsBtn.TabIndex = 1;
            this.clearResultsBtn.Text = "Clear";
            this.clearResultsBtn.UseVisualStyleBackColor = true;
            this.clearResultsBtn.Click += new System.EventHandler(this.clearResultsBtn_Click);
            // 
            // resultsDataGrid
            // 
            this.resultsDataGrid.AllowUserToAddRows = false;
            this.resultsDataGrid.AllowUserToDeleteRows = false;
            this.resultsDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultsDataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.resultsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column13,
            this.Column11,
            this.Column12,
            this.playedColumn,
            this.stateColumn,
            this.Column14});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.resultsDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.resultsDataGrid.Location = new System.Drawing.Point(3, 29);
            this.resultsDataGrid.Name = "resultsDataGrid";
            this.resultsDataGrid.ReadOnly = true;
            this.resultsDataGrid.RowHeadersVisible = false;
            this.resultsDataGrid.Size = new System.Drawing.Size(766, 326);
            this.resultsDataGrid.TabIndex = 0;
            this.resultsDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cellContent_Click);
            // 
            // Column13
            // 
            this.Column13.FillWeight = 27.72334F;
            this.Column13.HeaderText = "Item found";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.FillWeight = 112.1794F;
            this.Column11.HeaderText = "Name";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.FillWeight = 28.04486F;
            this.Column12.HeaderText = "Value";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // playedColumn
            // 
            this.playedColumn.FillWeight = 28.25911F;
            this.playedColumn.HeaderText = "Played";
            this.playedColumn.Name = "playedColumn";
            this.playedColumn.ReadOnly = true;
            // 
            // stateColumn
            // 
            this.stateColumn.FillWeight = 23.24694F;
            this.stateColumn.HeaderText = "State";
            this.stateColumn.Name = "stateColumn";
            this.stateColumn.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.FillWeight = 58.89855F;
            this.Column14.HeaderText = "Profile";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Text = "Backpack.tf";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // treeViewGroupBox
            // 
            this.treeViewGroupBox.Controls.Add(this.historyBtn);
            this.treeViewGroupBox.Controls.Add(this.treeView1);
            this.treeViewGroupBox.Location = new System.Drawing.Point(13, 27);
            this.treeViewGroupBox.Name = "treeViewGroupBox";
            this.treeViewGroupBox.Size = new System.Drawing.Size(178, 674);
            this.treeViewGroupBox.TabIndex = 6;
            this.treeViewGroupBox.TabStop = false;
            this.treeViewGroupBox.Text = "History";
            // 
            // historyBtn
            // 
            this.historyBtn.Location = new System.Drawing.Point(56, 12);
            this.historyBtn.Name = "historyBtn";
            this.historyBtn.Size = new System.Drawing.Size(75, 23);
            this.historyBtn.TabIndex = 1;
            this.historyBtn.Text = "Clean";
            this.historyBtn.UseVisualStyleBackColor = true;
            this.historyBtn.Click += new System.EventHandler(this.historyBtn_Click);
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(6, 41);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(166, 627);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "calendar.png");
            this.imageList1.Images.SetKeyName(1, "lupa.png");
            // 
            // contextMenuTreeView
            // 
            this.contextMenuTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem});
            this.contextMenuTreeView.Name = "contextMenuTreeView";
            this.contextMenuTreeView.Size = new System.Drawing.Size(139, 26);
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.newProjectToolStripMenuItem.Text = "New project";
            // 
            // statusScanWorker
            // 
            this.statusScanWorker.WorkerReportsProgress = true;
            this.statusScanWorker.WorkerSupportsCancellation = true;
            this.statusScanWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.statusScanWorker_DoWork);
            this.statusScanWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.statusScanWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // groupScanningGroup
            // 
            this.groupScanningGroup.Controls.Add(this.groupNameTextBox);
            this.groupScanningGroup.Controls.Add(this.label9);
            this.groupScanningGroup.Enabled = false;
            this.groupScanningGroup.Location = new System.Drawing.Point(230, 260);
            this.groupScanningGroup.Name = "groupScanningGroup";
            this.groupScanningGroup.Size = new System.Drawing.Size(734, 45);
            this.groupScanningGroup.TabIndex = 7;
            this.groupScanningGroup.TabStop = false;
            this.groupScanningGroup.Text = "Group scanning";
            // 
            // groupNameTextBox
            // 
            this.groupNameTextBox.Location = new System.Drawing.Point(83, 17);
            this.groupNameTextBox.Name = "groupNameTextBox";
            this.groupNameTextBox.Size = new System.Drawing.Size(245, 20);
            this.groupNameTextBox.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Group name";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(980, 726);
            this.Controls.Add(this.groupScanningGroup);
            this.Controls.Add(this.treeViewGroupBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "News Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusGroup.ResumeLayout(false);
            this.statusGroup.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.depthGroup.ResumeLayout(false);
            this.depthGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectedDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.itemsTab.ResumeLayout(false);
            this.itemsTab.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).EndInit();
            this.treeViewGroupBox.ResumeLayout(false);
            this.contextMenuTreeView.ResumeLayout(false);
            this.groupScanningGroup.ResumeLayout(false);
            this.groupScanningGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateSchemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox startingIdTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox profilesCountTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox playtimeTextBox;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox onlineTextBox;
        private System.Windows.Forms.ListBox leftListBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.DataGridView selectedDataGrid;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox apiTextBox;
        private System.ComponentModel.BackgroundWorker schemaUpdaterWorker;
        private System.ComponentModel.BackgroundWorker scanningWorker;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage itemsTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView resultsDataGrid;
        private Label label8;
        private TextBox itemsCountTextBox;
        private Button clearBtn;
        private Button deleteButton;
        private LinkLabel linkLabel1;
        private Button clearResultsBtn;
        private ToolStripMenuItem saveScanToolStripMenuItem;
        private ToolStripMenuItem loadScanToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
        private DataGridViewImageColumn Column13;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn playedColumn;
        private DataGridViewTextBoxColumn stateColumn;
        private DataGridViewLinkColumn Column14;
        private FolderBrowserDialog folderBrowserDialog1;
        private GroupBox treeViewGroupBox;
        private TreeView treeView1;
        private ImageList imageList1;
        private ContextMenuStrip contextMenuTreeView;
        private ToolStripMenuItem newProjectToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripSeparator toolStripMenuItem3;
        private GroupBox groupBox2;
        private RadioButton statusRadio;
        private GroupBox statusGroup;
        private TextBox statusTextBox;
        private RadioButton depthRadio;
        private GroupBox depthGroup;
        private System.ComponentModel.BackgroundWorker statusScanWorker;
        private Button historyBtn;
        private DataGridViewCheckBoxColumn selectedColumn;
        private DataGridViewTextBoxColumn nameColumn;
        private DataGridViewImageColumn imageColumn;
        private DataGridViewCheckBoxColumn stockColumn;
        private DataGridViewCheckBoxColumn strangeColumn;
        private DataGridViewCheckBoxColumn vintageColumn;
        private DataGridViewCheckBoxColumn uniqueColumn;
        private DataGridViewCheckBoxColumn genuineColumn;
        private DataGridViewCheckBoxColumn unusualColumn;
        private DataGridViewCheckBoxColumn hauntedColumn;
        private ToolStripMenuItem gameToolStripMenuItem;
        private ToolStripMenuItem teamFortress2ToolStripMenuItem;
        private ToolStripMenuItem cSGOToolStripMenuItem;
        private GroupBox groupScanningGroup;
        private RadioButton groupScanningRadio;
        private TextBox groupNameTextBox;
        private Label label9;
    }
}

