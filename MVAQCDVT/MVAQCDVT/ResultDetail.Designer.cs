namespace MVAQCDVT
{
    partial class ResultDetail
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
            this.dg_resultDetail = new System.Windows.Forms.DataGridView();
            this.resultDetailIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testItemIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.channel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cycle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_save = new System.Windows.Forms.Button();
            this.flp_testProperty = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_testItemIndex = new System.Windows.Forms.ComboBox();
            this.btn_cycleAnalysis = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_time = new System.Windows.Forms.RadioButton();
            this.rb_cycle = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dg_resultDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg_resultDetail
            // 
            this.dg_resultDetail.AllowUserToAddRows = false;
            this.dg_resultDetail.AllowUserToDeleteRows = false;
            this.dg_resultDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_resultDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.resultDetailIndex,
            this.testItemIndex,
            this.name,
            this.spec,
            this.value,
            this.channel,
            this.cycle,
            this.time,
            this.description,
            this.result});
            this.dg_resultDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_resultDetail.Location = new System.Drawing.Point(0, 0);
            this.dg_resultDetail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dg_resultDetail.Name = "dg_resultDetail";
            this.dg_resultDetail.ReadOnly = true;
            this.dg_resultDetail.RowHeadersVisible = false;
            this.dg_resultDetail.RowTemplate.Height = 24;
            this.dg_resultDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_resultDetail.Size = new System.Drawing.Size(948, 424);
            this.dg_resultDetail.TabIndex = 1;
            // 
            // resultDetailIndex
            // 
            this.resultDetailIndex.HeaderText = "resultDetailIndex";
            this.resultDetailIndex.Name = "resultDetailIndex";
            this.resultDetailIndex.ReadOnly = true;
            this.resultDetailIndex.Visible = false;
            // 
            // testItemIndex
            // 
            this.testItemIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.testItemIndex.FillWeight = 10F;
            this.testItemIndex.HeaderText = "tIndex";
            this.testItemIndex.Name = "testItemIndex";
            this.testItemIndex.ReadOnly = true;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.FillWeight = 20F;
            this.name.HeaderText = "name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // spec
            // 
            this.spec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.spec.FillWeight = 20F;
            this.spec.HeaderText = "spec";
            this.spec.Name = "spec";
            this.spec.ReadOnly = true;
            // 
            // value
            // 
            this.value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.value.FillWeight = 20F;
            this.value.HeaderText = "value";
            this.value.Name = "value";
            this.value.ReadOnly = true;
            // 
            // channel
            // 
            this.channel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.channel.FillWeight = 20F;
            this.channel.HeaderText = "channel";
            this.channel.Name = "channel";
            this.channel.ReadOnly = true;
            // 
            // cycle
            // 
            this.cycle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cycle.FillWeight = 20F;
            this.cycle.HeaderText = "cycle";
            this.cycle.Name = "cycle";
            this.cycle.ReadOnly = true;
            // 
            // time
            // 
            this.time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.time.FillWeight = 30F;
            this.time.HeaderText = "time";
            this.time.Name = "time";
            this.time.ReadOnly = true;
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.description.FillWeight = 30F;
            this.description.HeaderText = "description";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            // 
            // result
            // 
            this.result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.result.FillWeight = 20F;
            this.result.HeaderText = "result";
            this.result.Name = "result";
            this.result.ReadOnly = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_save);
            this.splitContainer1.Panel1.Controls.Add(this.flp_testProperty);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dg_resultDetail);
            this.splitContainer1.Size = new System.Drawing.Size(950, 583);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.TabIndex = 2;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(11, 108);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(110, 31);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "SaveLog";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // flp_testProperty
            // 
            this.flp_testProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flp_testProperty.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flp_testProperty.Location = new System.Drawing.Point(293, 3);
            this.flp_testProperty.Name = "flp_testProperty";
            this.flp_testProperty.Size = new System.Drawing.Size(652, 148);
            this.flp_testProperty.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cb_testItemIndex);
            this.groupBox1.Controls.Add(this.btn_cycleAnalysis);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(11, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 99);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cycle Analysis";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "TestItem Index";
            // 
            // cb_testItemIndex
            // 
            this.cb_testItemIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_testItemIndex.FormattingEnabled = true;
            this.cb_testItemIndex.Location = new System.Drawing.Point(115, 21);
            this.cb_testItemIndex.Name = "cb_testItemIndex";
            this.cb_testItemIndex.Size = new System.Drawing.Size(58, 22);
            this.cb_testItemIndex.TabIndex = 2;
            // 
            // btn_cycleAnalysis
            // 
            this.btn_cycleAnalysis.Location = new System.Drawing.Point(187, 21);
            this.btn_cycleAnalysis.Name = "btn_cycleAnalysis";
            this.btn_cycleAnalysis.Size = new System.Drawing.Size(81, 64);
            this.btn_cycleAnalysis.TabIndex = 1;
            this.btn_cycleAnalysis.Text = "Start";
            this.btn_cycleAnalysis.UseVisualStyleBackColor = true;
            this.btn_cycleAnalysis.Click += new System.EventHandler(this.btn_cycleAnalysis_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_time);
            this.groupBox2.Controls.Add(this.rb_cycle);
            this.groupBox2.Location = new System.Drawing.Point(6, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(167, 39);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "X Axis";
            // 
            // rb_time
            // 
            this.rb_time.AutoSize = true;
            this.rb_time.Location = new System.Drawing.Point(98, 16);
            this.rb_time.Name = "rb_time";
            this.rb_time.Size = new System.Drawing.Size(54, 18);
            this.rb_time.TabIndex = 1;
            this.rb_time.TabStop = true;
            this.rb_time.Text = "Time";
            this.rb_time.UseVisualStyleBackColor = true;
            // 
            // rb_cycle
            // 
            this.rb_cycle.AutoSize = true;
            this.rb_cycle.Location = new System.Drawing.Point(15, 16);
            this.rb_cycle.Name = "rb_cycle";
            this.rb_cycle.Size = new System.Drawing.Size(58, 18);
            this.rb_cycle.TabIndex = 0;
            this.rb_cycle.TabStop = true;
            this.rb_cycle.Text = "Cycle";
            this.rb_cycle.UseVisualStyleBackColor = true;
            // 
            // ResultDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 583);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ResultDetail";
            this.Text = "ResultDetail";
            ((System.ComponentModel.ISupportInitialize)(this.dg_resultDetail)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_resultDetail;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultDetailIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn testItemIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.DataGridViewTextBoxColumn channel;
        private System.Windows.Forms.DataGridViewTextBoxColumn cycle;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn result;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_cycleAnalysis;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rb_time;
        private System.Windows.Forms.RadioButton rb_cycle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_testItemIndex;
        private System.Windows.Forms.FlowLayoutPanel flp_testProperty;
        private System.Windows.Forms.Button btn_save;
    }
}