namespace MVAQCDVT
{
    partial class mainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cb_profile = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.txt_globalCycle = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.txt_globalCycleDelay = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.lbl_sn = new System.Windows.Forms.ToolStripLabel();
            this.txt_sn = new System.Windows.Forms.ToolStripTextBox();
            this.lbl_pn = new System.Windows.Forms.ToolStripLabel();
            this.txt_pn = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.cb_slot = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_start = new System.Windows.Forms.ToolStripButton();
            this.btn_stop = new System.Windows.Forms.ToolStripButton();
            this.btn_clear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pb_bar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_setting = new System.Windows.Forms.ToolStripButton();
            this.btn_scheduler = new System.Windows.Forms.ToolStripButton();
            this.btn_log = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.btn_delTestItem = new System.Windows.Forms.Button();
            this.dg_testItem = new System.Windows.Forms.DataGridView();
            this.enable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.seqIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Retest = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.retestNUpperLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cycle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cycleDelay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stopWhenFail = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.until = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cms_testItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btn_copyAndNew = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_aliasName = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_copyAndPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_moveTo = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_copyTo = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_runRangeTestItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_enableSelectedTestItems = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_disableSelectedTestItems = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_deleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_addTestItem = new System.Windows.Forms.Button();
            this.cb_testItem = new System.Windows.Forms.ComboBox();
            this.property_testItem = new System.Windows.Forms.PropertyGrid();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dg_testResult = new System.Windows.Forms.DataGridView();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tCycle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.info = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comparisionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms_exportData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btn_exportRawData = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_exportScaledData = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer_Chart = new System.Windows.Forms.SplitContainer();
            this.lbl_rms = new System.Windows.Forms.Label();
            this.lbl_std = new System.Windows.Forms.Label();
            this.lbl_min = new System.Windows.Forms.Label();
            this.lbl_max = new System.Windows.Forms.Label();
            this.lbl_p2p = new System.Windows.Forms.Label();
            this.lbl_average = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_meas = new System.Windows.Forms.CheckBox();
            this.chk_fftAbsolute = new System.Windows.Forms.CheckBox();
            this.rb_fft = new System.Windows.Forms.RadioButton();
            this.rb_scaledData = new System.Windows.Forms.RadioButton();
            this.rb_rawdata = new System.Windows.Forms.RadioButton();
            this.ch_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lbl_nofc = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ch_histogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.檔案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_exportProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_importProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_lock = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_repair = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_autoClose = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_about = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sfd_profileExport = new System.Windows.Forms.SaveFileDialog();
            this.ofd_profileImport = new System.Windows.Forms.OpenFileDialog();
            this.sfd_export = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_testItem)).BeginInit();
            this.cms_testItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_testResult)).BeginInit();
            this.cms_exportData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Chart)).BeginInit();
            this.splitContainer_Chart.Panel1.SuspendLayout();
            this.splitContainer_Chart.Panel2.SuspendLayout();
            this.splitContainer_Chart.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ch_chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ch_histogram)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.cb_profile,
            this.toolStripSeparator5,
            this.toolStripLabel3,
            this.txt_globalCycle,
            this.toolStripLabel4,
            this.txt_globalCycleDelay,
            this.toolStripSeparator4,
            this.lbl_sn,
            this.txt_sn,
            this.lbl_pn,
            this.txt_pn,
            this.toolStripSeparator6,
            this.toolStripLabel5,
            this.cb_slot,
            this.toolStripSeparator1,
            this.btn_start,
            this.btn_stop,
            this.btn_clear,
            this.toolStripSeparator2,
            this.pb_bar,
            this.toolStripSeparator3,
            this.btn_setting,
            this.btn_scheduler,
            this.btn_log});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1384, 51);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(44, 48);
            this.toolStripLabel2.Text = "Profile";
            // 
            // cb_profile
            // 
            this.cb_profile.AutoSize = false;
            this.cb_profile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_profile.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cb_profile.Name = "cb_profile";
            this.cb_profile.Size = new System.Drawing.Size(150, 24);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 51);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(38, 48);
            this.toolStripLabel3.Text = "Cycle";
            // 
            // txt_globalCycle
            // 
            this.txt_globalCycle.Name = "txt_globalCycle";
            this.txt_globalCycle.Size = new System.Drawing.Size(50, 51);
            this.txt_globalCycle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_globalCycle_KeyPress);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(40, 48);
            this.toolStripLabel4.Text = "Delay";
            // 
            // txt_globalCycleDelay
            // 
            this.txt_globalCycleDelay.Name = "txt_globalCycleDelay";
            this.txt_globalCycleDelay.Size = new System.Drawing.Size(50, 51);
            this.txt_globalCycleDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_globalCycleDelay_KeyPress);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 51);
            // 
            // lbl_sn
            // 
            this.lbl_sn.Name = "lbl_sn";
            this.lbl_sn.Size = new System.Drawing.Size(25, 48);
            this.lbl_sn.Text = "SN";
            // 
            // txt_sn
            // 
            this.txt_sn.Name = "txt_sn";
            this.txt_sn.Size = new System.Drawing.Size(105, 51);
            // 
            // lbl_pn
            // 
            this.lbl_pn.Name = "lbl_pn";
            this.lbl_pn.Size = new System.Drawing.Size(25, 48);
            this.lbl_pn.Text = "PN";
            // 
            // txt_pn
            // 
            this.txt_pn.Name = "txt_pn";
            this.txt_pn.Size = new System.Drawing.Size(105, 51);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 51);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(38, 48);
            this.toolStripLabel5.Text = "SLOT";
            // 
            // cb_slot
            // 
            this.cb_slot.AutoSize = false;
            this.cb_slot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_slot.DropDownWidth = 40;
            this.cb_slot.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cb_slot.Name = "cb_slot";
            this.cb_slot.Size = new System.Drawing.Size(35, 24);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 51);
            // 
            // btn_start
            // 
            this.btn_start.AutoSize = false;
            this.btn_start.Image = ((System.Drawing.Image)(resources.GetObject("btn_start.Image")));
            this.btn_start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(70, 41);
            this.btn_start.Text = "Start";
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.AutoSize = false;
            this.btn_stop.Image = ((System.Drawing.Image)(resources.GetObject("btn_stop.Image")));
            this.btn_stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(70, 41);
            this.btn_stop.Text = "STOP";
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.AutoSize = false;
            this.btn_clear.Image = ((System.Drawing.Image)(resources.GetObject("btn_clear.Image")));
            this.btn_clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(70, 41);
            this.btn_clear.Text = "Clear";
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 51);
            // 
            // pb_bar
            // 
            this.pb_bar.ForeColor = System.Drawing.Color.OrangeRed;
            this.pb_bar.Maximum = 0;
            this.pb_bar.Name = "pb_bar";
            this.pb_bar.Size = new System.Drawing.Size(100, 48);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 51);
            // 
            // btn_setting
            // 
            this.btn_setting.AutoSize = false;
            this.btn_setting.Image = global::MVAQCDVT.Properties.Resources.下載;
            this.btn_setting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(70, 41);
            this.btn_setting.Text = "Setting";
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // btn_scheduler
            // 
            this.btn_scheduler.AutoSize = false;
            this.btn_scheduler.BackColor = System.Drawing.SystemColors.Control;
            this.btn_scheduler.Image = ((System.Drawing.Image)(resources.GetObject("btn_scheduler.Image")));
            this.btn_scheduler.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_scheduler.Name = "btn_scheduler";
            this.btn_scheduler.Size = new System.Drawing.Size(100, 41);
            this.btn_scheduler.Text = "Scheduler";
            this.btn_scheduler.ToolTipText = "Scheduler";
            this.btn_scheduler.Click += new System.EventHandler(this.btn_scheduler_Click);
            // 
            // btn_log
            // 
            this.btn_log.AutoSize = false;
            this.btn_log.Image = ((System.Drawing.Image)(resources.GetObject("btn_log.Image")));
            this.btn_log.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_log.Name = "btn_log";
            this.btn_log.Size = new System.Drawing.Size(70, 41);
            this.btn_log.Text = "Report";
            this.btn_log.ToolTipText = "Report";
            this.btn_log.Click += new System.EventHandler(this.btn_log_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(0, 79);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1384, 650);
            this.splitContainer1.SplitterDistance = 476;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.label8);
            this.splitContainer3.Panel1.Controls.Add(this.btn_down);
            this.splitContainer3.Panel1.Controls.Add(this.btn_up);
            this.splitContainer3.Panel1.Controls.Add(this.btn_delTestItem);
            this.splitContainer3.Panel1.Controls.Add(this.dg_testItem);
            this.splitContainer3.Panel1.Controls.Add(this.btn_addTestItem);
            this.splitContainer3.Panel1.Controls.Add(this.cb_testItem);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.property_testItem);
            this.splitContainer3.Size = new System.Drawing.Size(476, 650);
            this.splitContainer3.SplitterDistance = 292;
            this.splitContainer3.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(346, 258);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 23);
            this.label8.TabIndex = 7;
            this.label8.Text = "|";
            // 
            // btn_down
            // 
            this.btn_down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_down.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_down.Location = new System.Drawing.Point(421, 258);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(50, 25);
            this.btn_down.TabIndex = 6;
            this.btn_down.Text = "↓";
            this.btn_down.UseVisualStyleBackColor = true;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_up
            // 
            this.btn_up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_up.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_up.Location = new System.Drawing.Point(364, 258);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(50, 25);
            this.btn_up.TabIndex = 5;
            this.btn_up.Text = "↑";
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // btn_delTestItem
            // 
            this.btn_delTestItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delTestItem.Location = new System.Drawing.Point(210, 258);
            this.btn_delTestItem.Name = "btn_delTestItem";
            this.btn_delTestItem.Size = new System.Drawing.Size(135, 25);
            this.btn_delTestItem.TabIndex = 4;
            this.btn_delTestItem.Text = "DEL";
            this.btn_delTestItem.UseVisualStyleBackColor = true;
            this.btn_delTestItem.Click += new System.EventHandler(this.btn_delTestItem_Click);
            // 
            // dg_testItem
            // 
            this.dg_testItem.AllowDrop = true;
            this.dg_testItem.AllowUserToAddRows = false;
            this.dg_testItem.AllowUserToDeleteRows = false;
            this.dg_testItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg_testItem.BackgroundColor = System.Drawing.Color.Pink;
            this.dg_testItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_testItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enable,
            this.seqIndex,
            this.tIndex,
            this.name,
            this.Retest,
            this.retestNUpperLimit,
            this.Cycle,
            this.cycleDelay,
            this.stopWhenFail,
            this.until});
            this.dg_testItem.ContextMenuStrip = this.cms_testItem;
            this.dg_testItem.Location = new System.Drawing.Point(0, 0);
            this.dg_testItem.Name = "dg_testItem";
            this.dg_testItem.RowHeadersVisible = false;
            this.dg_testItem.RowTemplate.Height = 24;
            this.dg_testItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_testItem.Size = new System.Drawing.Size(471, 252);
            this.dg_testItem.TabIndex = 3;
            this.dg_testItem.DragDrop += new System.Windows.Forms.DragEventHandler(this.dg_testItem_DragDrop);
            this.dg_testItem.DragEnter += new System.Windows.Forms.DragEventHandler(this.dg_testItem_DragEnter);
            // 
            // enable
            // 
            this.enable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.enable.FillWeight = 17.6296F;
            this.enable.HeaderText = "Enable";
            this.enable.Name = "enable";
            this.enable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.enable.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // seqIndex
            // 
            this.seqIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.seqIndex.FillWeight = 17.6296F;
            this.seqIndex.HeaderText = "SIndex";
            this.seqIndex.Name = "seqIndex";
            this.seqIndex.ReadOnly = true;
            // 
            // tIndex
            // 
            this.tIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tIndex.FillWeight = 16F;
            this.tIndex.HeaderText = "TIndex";
            this.tIndex.Name = "tIndex";
            this.tIndex.ReadOnly = true;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.name.FillWeight = 50F;
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 68;
            // 
            // Retest
            // 
            this.Retest.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Retest.FillWeight = 17.6296F;
            this.Retest.HeaderText = "Retest";
            this.Retest.Name = "Retest";
            this.Retest.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Retest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // retestNUpperLimit
            // 
            this.retestNUpperLimit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.retestNUpperLimit.FillWeight = 20F;
            this.retestNUpperLimit.HeaderText = "RetestN";
            this.retestNUpperLimit.Name = "retestNUpperLimit";
            // 
            // Cycle
            // 
            this.Cycle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cycle.FillWeight = 17.6296F;
            this.Cycle.HeaderText = "Cycle";
            this.Cycle.Name = "Cycle";
            // 
            // cycleDelay
            // 
            this.cycleDelay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cycleDelay.FillWeight = 17F;
            this.cycleDelay.HeaderText = "Delay";
            this.cycleDelay.Name = "cycleDelay";
            // 
            // stopWhenFail
            // 
            this.stopWhenFail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.stopWhenFail.FillWeight = 19.41446F;
            this.stopWhenFail.HeaderText = "SWF";
            this.stopWhenFail.Name = "stopWhenFail";
            this.stopWhenFail.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.stopWhenFail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // until
            // 
            this.until.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.until.FillWeight = 30F;
            this.until.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.until.HeaderText = "Until";
            this.until.Items.AddRange(new object[] {
            "None",
            "PASS",
            "FAIL"});
            this.until.Name = "until";
            // 
            // cms_testItem
            // 
            this.cms_testItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_copyAndNew,
            this.btn_aliasName,
            this.btn_copyAndPaste,
            this.btn_delete,
            this.btn_moveTo,
            this.btn_copyTo,
            this.btn_runRangeTestItem,
            this.btn_enableSelectedTestItems,
            this.btn_disableSelectedTestItems,
            this.btn_deleteAll});
            this.cms_testItem.Name = "cms_testItem";
            this.cms_testItem.Size = new System.Drawing.Size(227, 224);
            // 
            // btn_copyAndNew
            // 
            this.btn_copyAndNew.Name = "btn_copyAndNew";
            this.btn_copyAndNew.Size = new System.Drawing.Size(226, 22);
            this.btn_copyAndNew.Text = "Copy to Profile";
            this.btn_copyAndNew.Click += new System.EventHandler(this.btn_copyAndNew_Click);
            // 
            // btn_aliasName
            // 
            this.btn_aliasName.Name = "btn_aliasName";
            this.btn_aliasName.Size = new System.Drawing.Size(226, 22);
            this.btn_aliasName.Text = "AliasName";
            this.btn_aliasName.Click += new System.EventHandler(this.btn_aliasName_Click);
            // 
            // btn_copyAndPaste
            // 
            this.btn_copyAndPaste.Name = "btn_copyAndPaste";
            this.btn_copyAndPaste.Size = new System.Drawing.Size(226, 22);
            this.btn_copyAndPaste.Text = "Copy and Paste";
            this.btn_copyAndPaste.Click += new System.EventHandler(this.btn_copyAndPaste_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(226, 22);
            this.btn_delete.Text = "Delete Item";
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_moveTo
            // 
            this.btn_moveTo.Name = "btn_moveTo";
            this.btn_moveTo.Size = new System.Drawing.Size(226, 22);
            this.btn_moveTo.Text = "Move To";
            this.btn_moveTo.Click += new System.EventHandler(this.btn_moveTo_Click);
            // 
            // btn_copyTo
            // 
            this.btn_copyTo.Name = "btn_copyTo";
            this.btn_copyTo.Size = new System.Drawing.Size(226, 22);
            this.btn_copyTo.Text = "Copy To";
            this.btn_copyTo.Visible = false;
            this.btn_copyTo.Click += new System.EventHandler(this.btn_copyTo_Click);
            // 
            // btn_runRangeTestItem
            // 
            this.btn_runRangeTestItem.Name = "btn_runRangeTestItem";
            this.btn_runRangeTestItem.Size = new System.Drawing.Size(226, 22);
            this.btn_runRangeTestItem.Text = "Parital Execute TestItem";
            this.btn_runRangeTestItem.Click += new System.EventHandler(this.btn_runRangeTestItem_Click);
            // 
            // btn_enableSelectedTestItems
            // 
            this.btn_enableSelectedTestItems.Name = "btn_enableSelectedTestItems";
            this.btn_enableSelectedTestItems.Size = new System.Drawing.Size(226, 22);
            this.btn_enableSelectedTestItems.Text = "Enable Selected TestItems";
            this.btn_enableSelectedTestItems.Click += new System.EventHandler(this.btn_enableSelectedTestItems_Click);
            // 
            // btn_disableSelectedTestItems
            // 
            this.btn_disableSelectedTestItems.Name = "btn_disableSelectedTestItems";
            this.btn_disableSelectedTestItems.Size = new System.Drawing.Size(226, 22);
            this.btn_disableSelectedTestItems.Text = "Disable Selected TestItems";
            this.btn_disableSelectedTestItems.Click += new System.EventHandler(this.btn_disableSelectedTestItems_Click);
            // 
            // btn_deleteAll
            // 
            this.btn_deleteAll.Name = "btn_deleteAll";
            this.btn_deleteAll.Size = new System.Drawing.Size(226, 22);
            this.btn_deleteAll.Text = "Delete All";
            this.btn_deleteAll.Click += new System.EventHandler(this.btn_deleteAll_Click);
            // 
            // btn_addTestItem
            // 
            this.btn_addTestItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_addTestItem.Location = new System.Drawing.Point(11, 258);
            this.btn_addTestItem.Name = "btn_addTestItem";
            this.btn_addTestItem.Size = new System.Drawing.Size(193, 25);
            this.btn_addTestItem.TabIndex = 2;
            this.btn_addTestItem.Text = "ADD";
            this.btn_addTestItem.UseVisualStyleBackColor = true;
            this.btn_addTestItem.Click += new System.EventHandler(this.btn_addTestItem_Click);
            // 
            // cb_testItem
            // 
            this.cb_testItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_testItem.FormattingEnabled = true;
            this.cb_testItem.Location = new System.Drawing.Point(3, 258);
            this.cb_testItem.Name = "cb_testItem";
            this.cb_testItem.Size = new System.Drawing.Size(10, 22);
            this.cb_testItem.TabIndex = 1;
            this.cb_testItem.Visible = false;
            // 
            // property_testItem
            // 
            this.property_testItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.property_testItem.LineColor = System.Drawing.SystemColors.ControlDark;
            this.property_testItem.Location = new System.Drawing.Point(0, 0);
            this.property_testItem.Name = "property_testItem";
            this.property_testItem.Size = new System.Drawing.Size(474, 352);
            this.property_testItem.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dg_testResult);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer_Chart);
            this.splitContainer2.Size = new System.Drawing.Size(903, 650);
            this.splitContainer2.SplitterDistance = 285;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // dg_testResult
            // 
            this.dg_testResult.AllowUserToAddRows = false;
            this.dg_testResult.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dg_testResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_testResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.tCycle,
            this.testItem,
            this.info,
            this.testValue,
            this.percentage,
            this.testResult,
            this.comparisionType,
            this.channel});
            this.dg_testResult.ContextMenuStrip = this.cms_exportData;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_testResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dg_testResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_testResult.Location = new System.Drawing.Point(0, 0);
            this.dg_testResult.Name = "dg_testResult";
            this.dg_testResult.ReadOnly = true;
            this.dg_testResult.RowHeadersWidth = 30;
            this.dg_testResult.RowTemplate.Height = 24;
            this.dg_testResult.Size = new System.Drawing.Size(901, 283);
            this.dg_testResult.TabIndex = 1;
            // 
            // index
            // 
            this.index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.index.FillWeight = 10F;
            this.index.HeaderText = "TIndex";
            this.index.Name = "index";
            this.index.ReadOnly = true;
            // 
            // tCycle
            // 
            this.tCycle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tCycle.FillWeight = 10F;
            this.tCycle.HeaderText = "Cycle";
            this.tCycle.Name = "tCycle";
            this.tCycle.ReadOnly = true;
            // 
            // testItem
            // 
            this.testItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.testItem.FillWeight = 30F;
            this.testItem.HeaderText = "Test Item";
            this.testItem.Name = "testItem";
            this.testItem.ReadOnly = true;
            this.testItem.Width = 92;
            // 
            // info
            // 
            this.info.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.info.FillWeight = 50F;
            this.info.HeaderText = "Description";
            this.info.Name = "info";
            this.info.ReadOnly = true;
            // 
            // testValue
            // 
            this.testValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.testValue.FillWeight = 50F;
            this.testValue.HeaderText = "Test Value";
            this.testValue.Name = "testValue";
            this.testValue.ReadOnly = true;
            // 
            // percentage
            // 
            this.percentage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.percentage.FillWeight = 50F;
            this.percentage.HeaderText = "percentage";
            this.percentage.Name = "percentage";
            this.percentage.ReadOnly = true;
            this.percentage.Visible = false;
            // 
            // testResult
            // 
            this.testResult.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.testResult.FillWeight = 50F;
            this.testResult.HeaderText = "Test Result";
            this.testResult.Name = "testResult";
            this.testResult.ReadOnly = true;
            this.testResult.Width = 102;
            // 
            // comparisionType
            // 
            this.comparisionType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.comparisionType.FillWeight = 30F;
            this.comparisionType.HeaderText = "CompareType";
            this.comparisionType.Name = "comparisionType";
            this.comparisionType.ReadOnly = true;
            // 
            // channel
            // 
            this.channel.FillWeight = 50F;
            this.channel.HeaderText = "channel";
            this.channel.Name = "channel";
            this.channel.ReadOnly = true;
            this.channel.Visible = false;
            this.channel.Width = 50;
            // 
            // cms_exportData
            // 
            this.cms_exportData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_exportRawData,
            this.btn_exportScaledData});
            this.cms_exportData.Name = "cms_testItem";
            this.cms_exportData.Size = new System.Drawing.Size(179, 48);
            // 
            // btn_exportRawData
            // 
            this.btn_exportRawData.Name = "btn_exportRawData";
            this.btn_exportRawData.Size = new System.Drawing.Size(178, 22);
            this.btn_exportRawData.Text = "ExportRawData";
            this.btn_exportRawData.Click += new System.EventHandler(this.btn_exportRawData_Click);
            // 
            // btn_exportScaledData
            // 
            this.btn_exportScaledData.Name = "btn_exportScaledData";
            this.btn_exportScaledData.Size = new System.Drawing.Size(178, 22);
            this.btn_exportScaledData.Text = "ExportScaledData";
            this.btn_exportScaledData.Click += new System.EventHandler(this.btn_exportScaledData_Click);
            // 
            // splitContainer_Chart
            // 
            this.splitContainer_Chart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer_Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_Chart.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer_Chart.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_Chart.Name = "splitContainer_Chart";
            // 
            // splitContainer_Chart.Panel1
            // 
            this.splitContainer_Chart.Panel1.Controls.Add(this.lbl_rms);
            this.splitContainer_Chart.Panel1.Controls.Add(this.lbl_std);
            this.splitContainer_Chart.Panel1.Controls.Add(this.lbl_min);
            this.splitContainer_Chart.Panel1.Controls.Add(this.lbl_max);
            this.splitContainer_Chart.Panel1.Controls.Add(this.lbl_p2p);
            this.splitContainer_Chart.Panel1.Controls.Add(this.lbl_average);
            this.splitContainer_Chart.Panel1.Controls.Add(this.label7);
            this.splitContainer_Chart.Panel1.Controls.Add(this.label6);
            this.splitContainer_Chart.Panel1.Controls.Add(this.label4);
            this.splitContainer_Chart.Panel1.Controls.Add(this.label3);
            this.splitContainer_Chart.Panel1.Controls.Add(this.label2);
            this.splitContainer_Chart.Panel1.Controls.Add(this.label1);
            this.splitContainer_Chart.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer_Chart.Panel1.Controls.Add(this.ch_chart);
            this.splitContainer_Chart.Panel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // splitContainer_Chart.Panel2
            // 
            this.splitContainer_Chart.Panel2.Controls.Add(this.lbl_nofc);
            this.splitContainer_Chart.Panel2.Controls.Add(this.label5);
            this.splitContainer_Chart.Panel2.Controls.Add(this.ch_histogram);
            this.splitContainer_Chart.Size = new System.Drawing.Size(903, 360);
            this.splitContainer_Chart.SplitterDistance = 646;
            this.splitContainer_Chart.TabIndex = 28;
            // 
            // lbl_rms
            // 
            this.lbl_rms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_rms.AutoSize = true;
            this.lbl_rms.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rms.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_rms.Location = new System.Drawing.Point(625, 329);
            this.lbl_rms.Name = "lbl_rms";
            this.lbl_rms.Size = new System.Drawing.Size(24, 14);
            this.lbl_rms.TabIndex = 38;
            this.lbl_rms.Text = "NA";
            // 
            // lbl_std
            // 
            this.lbl_std.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_std.AutoSize = true;
            this.lbl_std.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_std.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_std.Location = new System.Drawing.Point(623, 296);
            this.lbl_std.Name = "lbl_std";
            this.lbl_std.Size = new System.Drawing.Size(24, 14);
            this.lbl_std.TabIndex = 37;
            this.lbl_std.Text = "NA";
            // 
            // lbl_min
            // 
            this.lbl_min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_min.AutoSize = true;
            this.lbl_min.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_min.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_min.Location = new System.Drawing.Point(461, 329);
            this.lbl_min.Name = "lbl_min";
            this.lbl_min.Size = new System.Drawing.Size(24, 14);
            this.lbl_min.TabIndex = 36;
            this.lbl_min.Text = "NA";
            // 
            // lbl_max
            // 
            this.lbl_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_max.AutoSize = true;
            this.lbl_max.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_max.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_max.Location = new System.Drawing.Point(461, 296);
            this.lbl_max.Name = "lbl_max";
            this.lbl_max.Size = new System.Drawing.Size(24, 14);
            this.lbl_max.TabIndex = 35;
            this.lbl_max.Text = "NA";
            // 
            // lbl_p2p
            // 
            this.lbl_p2p.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_p2p.AutoSize = true;
            this.lbl_p2p.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_p2p.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_p2p.Location = new System.Drawing.Point(300, 329);
            this.lbl_p2p.Name = "lbl_p2p";
            this.lbl_p2p.Size = new System.Drawing.Size(24, 14);
            this.lbl_p2p.TabIndex = 34;
            this.lbl_p2p.Text = "NA";
            // 
            // lbl_average
            // 
            this.lbl_average.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_average.AutoSize = true;
            this.lbl_average.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_average.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_average.Location = new System.Drawing.Point(300, 296);
            this.lbl_average.Name = "lbl_average";
            this.lbl_average.Size = new System.Drawing.Size(24, 14);
            this.lbl_average.TabIndex = 33;
            this.lbl_average.Text = "NA";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(544, 329);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 14);
            this.label7.TabIndex = 32;
            this.label7.Text = "RMS : ";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(544, 296);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 14);
            this.label6.TabIndex = 31;
            this.label6.Text = "STD : ";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(389, 329);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 14);
            this.label4.TabIndex = 30;
            this.label4.Text = "MIN : ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(389, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 14);
            this.label3.TabIndex = 29;
            this.label3.Text = "MAX : ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(193, 329);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 14);
            this.label2.TabIndex = 28;
            this.label2.Text = "P2P : ";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(193, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 14);
            this.label1.TabIndex = 27;
            this.label1.Text = "Average : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.chk_meas);
            this.groupBox1.Controls.Add(this.chk_fftAbsolute);
            this.groupBox1.Controls.Add(this.rb_fft);
            this.groupBox1.Controls.Add(this.rb_scaledData);
            this.groupBox1.Controls.Add(this.rb_rawdata);
            this.groupBox1.Location = new System.Drawing.Point(3, 280);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 76);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option";
            // 
            // chk_meas
            // 
            this.chk_meas.AutoSize = true;
            this.chk_meas.Checked = true;
            this.chk_meas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_meas.Location = new System.Drawing.Point(102, 16);
            this.chk_meas.Name = "chk_meas";
            this.chk_meas.Size = new System.Drawing.Size(59, 18);
            this.chk_meas.TabIndex = 4;
            this.chk_meas.Text = "Meas";
            this.chk_meas.UseVisualStyleBackColor = true;
            // 
            // chk_fftAbsolute
            // 
            this.chk_fftAbsolute.AutoSize = true;
            this.chk_fftAbsolute.Location = new System.Drawing.Point(102, 57);
            this.chk_fftAbsolute.Name = "chk_fftAbsolute";
            this.chk_fftAbsolute.Size = new System.Drawing.Size(81, 18);
            this.chk_fftAbsolute.TabIndex = 3;
            this.chk_fftAbsolute.Text = "Absolute";
            this.chk_fftAbsolute.UseVisualStyleBackColor = true;
            // 
            // rb_fft
            // 
            this.rb_fft.AutoSize = true;
            this.rb_fft.Location = new System.Drawing.Point(6, 56);
            this.rb_fft.Name = "rb_fft";
            this.rb_fft.Size = new System.Drawing.Size(90, 18);
            this.rb_fft.TabIndex = 2;
            this.rb_fft.Text = "Frequency";
            this.rb_fft.UseVisualStyleBackColor = true;
            this.rb_fft.CheckedChanged += new System.EventHandler(this.rb_dataType_CheckedChanged);
            // 
            // rb_scaledData
            // 
            this.rb_scaledData.AutoSize = true;
            this.rb_scaledData.Checked = true;
            this.rb_scaledData.Location = new System.Drawing.Point(6, 36);
            this.rb_scaledData.Name = "rb_scaledData";
            this.rb_scaledData.Size = new System.Drawing.Size(96, 18);
            this.rb_scaledData.TabIndex = 1;
            this.rb_scaledData.TabStop = true;
            this.rb_scaledData.Text = "ScaledData";
            this.rb_scaledData.UseVisualStyleBackColor = true;
            this.rb_scaledData.CheckedChanged += new System.EventHandler(this.rb_dataType_CheckedChanged);
            // 
            // rb_rawdata
            // 
            this.rb_rawdata.AutoSize = true;
            this.rb_rawdata.Location = new System.Drawing.Point(6, 15);
            this.rb_rawdata.Name = "rb_rawdata";
            this.rb_rawdata.Size = new System.Drawing.Size(81, 18);
            this.rb_rawdata.TabIndex = 0;
            this.rb_rawdata.Text = "Rawdata";
            this.rb_rawdata.UseVisualStyleBackColor = true;
            this.rb_rawdata.CheckedChanged += new System.EventHandler(this.rb_dataType_CheckedChanged);
            // 
            // ch_chart
            // 
            this.ch_chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ch_chart.BackColor = System.Drawing.Color.Black;
            chartArea3.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea3.AxisX.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisX.LineColor = System.Drawing.Color.SeaShell;
            chartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea3.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea3.AxisX.MinorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.AxisX.TitleForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea3.AxisY.IsStartedFromZero = false;
            chartArea3.AxisY.LabelStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisY.LineColor = System.Drawing.Color.SeaShell;
            chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea3.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea3.AxisY.MajorTickMark.Enabled = false;
            chartArea3.AxisY.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisY.MinorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisY.TitleForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.BackColor = System.Drawing.Color.Black;
            chartArea3.BorderColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea3.CursorX.IsUserEnabled = true;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.CursorY.IsUserEnabled = true;
            chartArea3.CursorY.IsUserSelectionEnabled = true;
            chartArea3.Name = "ChartArea1";
            chartArea3.Position.Auto = false;
            chartArea3.Position.Height = 90F;
            chartArea3.Position.Width = 85F;
            chartArea3.Position.X = 3F;
            chartArea3.Position.Y = 3F;
            this.ch_chart.ChartAreas.Add(chartArea3);
            legend3.BackColor = System.Drawing.Color.Black;
            legend3.ForeColor = System.Drawing.Color.WhiteSmoke;
            legend3.Name = "Legend1";
            legend3.Position.Auto = false;
            legend3.Position.Height = 37.19512F;
            legend3.Position.Width = 8.22072F;
            legend3.Position.X = 90F;
            legend3.Position.Y = 3F;
            this.ch_chart.Legends.Add(legend3);
            this.ch_chart.Location = new System.Drawing.Point(3, 3);
            this.ch_chart.Name = "ch_chart";
            this.ch_chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            this.ch_chart.Size = new System.Drawing.Size(638, 277);
            this.ch_chart.TabIndex = 25;
            this.ch_chart.Text = "chart2";
            // 
            // lbl_nofc
            // 
            this.lbl_nofc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_nofc.AutoSize = true;
            this.lbl_nofc.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_nofc.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbl_nofc.Location = new System.Drawing.Point(85, 296);
            this.lbl_nofc.Name = "lbl_nofc";
            this.lbl_nofc.Size = new System.Drawing.Size(37, 23);
            this.lbl_nofc.TabIndex = 34;
            this.lbl_nofc.Text = "NA";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 23);
            this.label5.TabIndex = 28;
            this.label5.Text = "NOFC : ";
            // 
            // ch_histogram
            // 
            this.ch_histogram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ch_histogram.BackColor = System.Drawing.SystemColors.ControlDark;
            chartArea4.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea4.AxisX.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea4.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea4.BackColor = System.Drawing.Color.Black;
            chartArea4.Name = "ChartArea1";
            this.ch_histogram.ChartAreas.Add(chartArea4);
            legend4.Enabled = false;
            legend4.Name = "Legend1";
            this.ch_histogram.Legends.Add(legend4);
            this.ch_histogram.Location = new System.Drawing.Point(2, 3);
            this.ch_histogram.Name = "ch_histogram";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "histoData";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.ch_histogram.Series.Add(series2);
            this.ch_histogram.Size = new System.Drawing.Size(246, 277);
            this.ch_histogram.TabIndex = 27;
            this.ch_histogram.Text = "chart2";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.檔案ToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.databaseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1384, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 檔案ToolStripMenuItem
            // 
            this.檔案ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_exportProfile,
            this.btn_importProfile});
            this.檔案ToolStripMenuItem.Name = "檔案ToolStripMenuItem";
            this.檔案ToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.檔案ToolStripMenuItem.Text = "File";
            // 
            // btn_exportProfile
            // 
            this.btn_exportProfile.Name = "btn_exportProfile";
            this.btn_exportProfile.Size = new System.Drawing.Size(153, 22);
            this.btn_exportProfile.Text = "Export Profile";
            this.btn_exportProfile.Click += new System.EventHandler(this.btn_exportProfile_Click);
            // 
            // btn_importProfile
            // 
            this.btn_importProfile.Name = "btn_importProfile";
            this.btn_importProfile.Size = new System.Drawing.Size(153, 22);
            this.btn_importProfile.Text = "Import Profile";
            this.btn_importProfile.Click += new System.EventHandler(this.btn_importProfile_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_lock,
            this.btn_repair,
            this.btn_autoClose});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // btn_lock
            // 
            this.btn_lock.Name = "btn_lock";
            this.btn_lock.Size = new System.Drawing.Size(176, 22);
            this.btn_lock.Text = "Lock";
            this.btn_lock.Click += new System.EventHandler(this.btn_lock_Click);
            // 
            // btn_repair
            // 
            this.btn_repair.Name = "btn_repair";
            this.btn_repair.Size = new System.Drawing.Size(176, 22);
            this.btn_repair.Text = "Repair";
            this.btn_repair.Click += new System.EventHandler(this.btn_repair_Click);
            // 
            // btn_autoClose
            // 
            this.btn_autoClose.Name = "btn_autoClose";
            this.btn_autoClose.Size = new System.Drawing.Size(176, 22);
            this.btn_autoClose.Text = "AutoClose Enable";
            this.btn_autoClose.Click += new System.EventHandler(this.btn_autoClose_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_about});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // btn_about
            // 
            this.btn_about.Name = "btn_about";
            this.btn_about.Size = new System.Drawing.Size(111, 22);
            this.btn_about.Text = "About";
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // sfd_profileExport
            // 
            this.sfd_profileExport.Filter = "xml 檔案|*.xml";
            // 
            // ofd_profileImport
            // 
            this.ofd_profileImport.Filter = "xml 檔案|*.xml";
            // 
            // sfd_export
            // 
            this.sfd_export.Filter = "csv 檔案|*.csv";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 730);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MVA QC DVT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_testItem)).EndInit();
            this.cms_testItem.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_testResult)).EndInit();
            this.cms_exportData.ResumeLayout(false);
            this.splitContainer_Chart.Panel1.ResumeLayout(false);
            this.splitContainer_Chart.Panel1.PerformLayout();
            this.splitContainer_Chart.Panel2.ResumeLayout(false);
            this.splitContainer_Chart.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_Chart)).EndInit();
            this.splitContainer_Chart.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ch_chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ch_histogram)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox cb_profile;
        private System.Windows.Forms.ToolStripLabel lbl_pn;
        private System.Windows.Forms.ToolStripTextBox txt_sn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btn_start;
        private System.Windows.Forms.ToolStripButton btn_stop;
        private System.Windows.Forms.ToolStripProgressBar pb_bar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dg_testResult;
        private System.Windows.Forms.DataVisualization.Charting.Chart ch_chart;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btn_addTestItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btn_setting;
        private System.Windows.Forms.ToolStripLabel lbl_sn;
        private System.Windows.Forms.ToolStripTextBox txt_pn;
        private System.Windows.Forms.DataGridView dg_testItem;
        private System.Windows.Forms.PropertyGrid property_testItem;
        private System.Windows.Forms.Button btn_delTestItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton btn_clear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox txt_globalCycle;
        private System.Windows.Forms.ToolStripButton btn_log;
        private System.Windows.Forms.ToolStripButton btn_scheduler;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_scaledData;
        private System.Windows.Forms.RadioButton rb_rawdata;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.DataVisualization.Charting.Chart ch_histogram;
        private System.Windows.Forms.SplitContainer splitContainer_Chart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_rms;
        private System.Windows.Forms.Label lbl_std;
        private System.Windows.Forms.Label lbl_min;
        private System.Windows.Forms.Label lbl_max;
        private System.Windows.Forms.Label lbl_p2p;
        private System.Windows.Forms.Label lbl_average;
        private System.Windows.Forms.Label lbl_nofc;
        private System.Windows.Forms.RadioButton rb_fft;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 檔案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btn_exportProfile;
        private System.Windows.Forms.ToolStripMenuItem btn_importProfile;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btn_about;
        private System.Windows.Forms.ContextMenuStrip cms_testItem;
        private System.Windows.Forms.ToolStripMenuItem btn_copyAndNew;
        private System.Windows.Forms.SaveFileDialog sfd_profileExport;
        private System.Windows.Forms.OpenFileDialog ofd_profileImport;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btn_lock;
        private System.Windows.Forms.ToolStripMenuItem btn_autoClose;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip cms_exportData;
        private System.Windows.Forms.ToolStripMenuItem btn_exportRawData;
        private System.Windows.Forms.ToolStripMenuItem btn_exportScaledData;
        private System.Windows.Forms.SaveFileDialog sfd_export;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox txt_globalCycleDelay;
        private System.Windows.Forms.CheckBox chk_fftAbsolute;
        private System.Windows.Forms.CheckBox chk_meas;
        private System.Windows.Forms.ToolStripMenuItem btn_repair;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn tCycle;
        private System.Windows.Forms.DataGridViewTextBoxColumn testItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn info;
        private System.Windows.Forms.DataGridViewTextBoxColumn testValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn percentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn testResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn comparisionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn channel;
        private System.Windows.Forms.ToolStripMenuItem btn_aliasName;
        private System.Windows.Forms.ToolStripMenuItem btn_copyAndPaste;
        private System.Windows.Forms.ToolStripMenuItem btn_delete;
        private System.Windows.Forms.ComboBox cb_testItem;
        private System.Windows.Forms.ToolStripMenuItem btn_moveTo;
        private System.Windows.Forms.ToolStripMenuItem btn_copyTo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enable;
        private System.Windows.Forms.DataGridViewTextBoxColumn seqIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn tIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Retest;
        private System.Windows.Forms.DataGridViewTextBoxColumn retestNUpperLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cycle;
        private System.Windows.Forms.DataGridViewTextBoxColumn cycleDelay;
        private System.Windows.Forms.DataGridViewCheckBoxColumn stopWhenFail;
        private System.Windows.Forms.DataGridViewComboBoxColumn until;
        private System.Windows.Forms.ToolStripMenuItem btn_runRangeTestItem;
        private System.Windows.Forms.ToolStripMenuItem btn_enableSelectedTestItems;
        private System.Windows.Forms.ToolStripMenuItem btn_disableSelectedTestItems;
        private System.Windows.Forms.ToolStripMenuItem btn_deleteAll;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btn_changeDatabase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripComboBox cb_slot;

    }
}

