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
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.startingIdTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.itemsCountTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.apiTextBox = new System.Windows.Forms.TextBox();
            this.stopBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.onlineTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.playtimeTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.profilesCountTextBox = new System.Windows.Forms.TextBox();
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
            this.resultsDataGrid = new System.Windows.Forms.DataGridView();
            this.clearResultsBtn = new System.Windows.Forms.Button();
            this.Column13 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectedDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.itemsTab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 518);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(801, 22);
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
            this.menuStrip1.Size = new System.Drawing.Size(801, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateSchemaToolStripMenuItem,
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
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Starting ID";
            // 
            // startingIdTextBox
            // 
            this.startingIdTextBox.Location = new System.Drawing.Point(92, 19);
            this.startingIdTextBox.Name = "startingIdTextBox";
            this.startingIdTextBox.Size = new System.Drawing.Size(266, 20);
            this.startingIdTextBox.TabIndex = 3;
            this.startingIdTextBox.Text = global::tf2_item_scanner.Properties.Settings.Default.StartingId;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.itemsCountTextBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.apiTextBox);
            this.groupBox1.Controls.Add(this.stopBtn);
            this.groupBox1.Controls.Add(this.startBtn);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.onlineTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.playtimeTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.valueTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.profilesCountTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.startingIdTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(773, 104);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(25, 29);
            this.linkLabel1.Location = new System.Drawing.Point(459, 45);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(157, 17);
            this.linkLabel1.TabIndex = 18;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "You can get your API key here";
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(386, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Items count";
            // 
            // itemsCountTextBox
            // 
            this.itemsCountTextBox.Location = new System.Drawing.Point(462, 70);
            this.itemsCountTextBox.Name = "itemsCountTextBox";
            this.itemsCountTextBox.Size = new System.Drawing.Size(69, 20);
            this.itemsCountTextBox.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(386, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "API key";
            // 
            // apiTextBox
            // 
            this.apiTextBox.Location = new System.Drawing.Point(462, 19);
            this.apiTextBox.Name = "apiTextBox";
            this.apiTextBox.Size = new System.Drawing.Size(283, 20);
            this.apiTextBox.TabIndex = 14;
            this.apiTextBox.Text = global::tf2_item_scanner.Properties.Settings.Default.ApiKey;
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(657, 67);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(89, 26);
            this.stopBtn.TabIndex = 13;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(563, 67);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(88, 26);
            this.startBtn.TabIndex = 12;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Last online";
            // 
            // onlineTextBox
            // 
            this.onlineTextBox.Location = new System.Drawing.Point(285, 70);
            this.onlineTextBox.Name = "onlineTextBox";
            this.onlineTextBox.Size = new System.Drawing.Size(72, 20);
            this.onlineTextBox.TabIndex = 10;
            this.onlineTextBox.Text = "1000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Playtime";
            // 
            // playtimeTextBox
            // 
            this.playtimeTextBox.Location = new System.Drawing.Point(92, 71);
            this.playtimeTextBox.Name = "playtimeTextBox";
            this.playtimeTextBox.Size = new System.Drawing.Size(74, 20);
            this.playtimeTextBox.TabIndex = 8;
            this.playtimeTextBox.Text = "1000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Backpack value";
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(285, 45);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(72, 20);
            this.valueTextBox.TabIndex = 6;
            this.valueTextBox.Text = "1000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Profiles to scan";
            // 
            // profilesCountTextBox
            // 
            this.profilesCountTextBox.Location = new System.Drawing.Point(92, 45);
            this.profilesCountTextBox.Name = "profilesCountTextBox";
            this.profilesCountTextBox.Size = new System.Drawing.Size(74, 20);
            this.profilesCountTextBox.TabIndex = 4;
            this.profilesCountTextBox.Text = "100";
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
            this.selectedDataGrid.Size = new System.Drawing.Size(543, 303);
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
            this.stockColumn.FalseValue = "false";
            this.stockColumn.FillWeight = 36.1223F;
            this.stockColumn.HeaderText = "STK";
            this.stockColumn.IndeterminateValue = "false";
            this.stockColumn.Name = "stockColumn";
            this.stockColumn.TrueValue = "true";
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
            this.leftListBox.Size = new System.Drawing.Size(183, 303);
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
            this.tabControl1.Location = new System.Drawing.Point(12, 135);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(777, 380);
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
            this.itemsTab.Size = new System.Drawing.Size(769, 354);
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
            this.tabPage2.Size = new System.Drawing.Size(769, 354);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Results";
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
            this.resultsDataGrid.Size = new System.Drawing.Size(766, 322);
            this.resultsDataGrid.TabIndex = 0;
            this.resultsDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cellContent_Click);
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
            // Column13
            // 
            this.Column13.FillWeight = 19.77071F;
            this.Column13.HeaderText = "Item found";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.FillWeight = 80F;
            this.Column11.HeaderText = "Name";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.FillWeight = 20F;
            this.Column12.HeaderText = "Value";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // stateColumn
            // 
            this.stateColumn.FillWeight = 16.5784F;
            this.stateColumn.HeaderText = "State";
            this.stateColumn.Name = "stateColumn";
            this.stateColumn.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.FillWeight = 42.0031F;
            this.Column14.HeaderText = "Profile";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Text = "Backpack.tf";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(801, 540);
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
            ((System.ComponentModel.ISupportInitialize)(this.selectedDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.itemsTab.ResumeLayout(false);
            this.itemsTab.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).EndInit();
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
        private Button deleteButton;
        private LinkLabel linkLabel1;
        private Button clearResultsBtn;
        private DataGridViewImageColumn Column13;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn stateColumn;
        private DataGridViewLinkColumn Column14;
    }
}

