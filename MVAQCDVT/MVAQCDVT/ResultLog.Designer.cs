namespace MVAQCDVT
{
    partial class ResultLog
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_releaseDiskSpace = new System.Windows.Forms.Button();
            this.btn_deleteResult = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_sn = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_result = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_profile = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_cardNumber = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dt_to = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dt_from = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dg_resultLog = new System.Windows.Forms.DataGridView();
            this.resultIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_resultLog)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.btn_releaseDiskSpace);
            this.splitContainer1.Panel1.Controls.Add(this.btn_deleteResult);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dg_resultLog);
            this.splitContainer1.Size = new System.Drawing.Size(977, 587);
            this.splitContainer1.SplitterDistance = 115;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_releaseDiskSpace
            // 
            this.btn_releaseDiskSpace.Location = new System.Drawing.Point(676, 63);
            this.btn_releaseDiskSpace.Name = "btn_releaseDiskSpace";
            this.btn_releaseDiskSpace.Size = new System.Drawing.Size(150, 30);
            this.btn_releaseDiskSpace.TabIndex = 4;
            this.btn_releaseDiskSpace.Text = "Release DiskSpace";
            this.btn_releaseDiskSpace.UseVisualStyleBackColor = true;
            this.btn_releaseDiskSpace.Click += new System.EventHandler(this.btn_releaseDiskSpace_Click);
            // 
            // btn_deleteResult
            // 
            this.btn_deleteResult.Location = new System.Drawing.Point(676, 24);
            this.btn_deleteResult.Name = "btn_deleteResult";
            this.btn_deleteResult.Size = new System.Drawing.Size(150, 30);
            this.btn_deleteResult.TabIndex = 3;
            this.btn_deleteResult.Text = "Delete Result";
            this.btn_deleteResult.UseVisualStyleBackColor = true;
            this.btn_deleteResult.Click += new System.EventHandler(this.btn_deleteResult_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_sn);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cb_result);
            this.groupBox3.Location = new System.Drawing.Point(464, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(206, 103);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Condition";
            // 
            // txt_sn
            // 
            this.txt_sn.Location = new System.Drawing.Point(71, 65);
            this.txt_sn.Name = "txt_sn";
            this.txt_sn.Size = new System.Drawing.Size(121, 22);
            this.txt_sn.TabIndex = 5;
            this.txt_sn.TextChanged += new System.EventHandler(this.txt_sn_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "SN";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 14);
            this.label6.TabIndex = 3;
            this.label6.Text = "Result";
            // 
            // cb_result
            // 
            this.cb_result.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_result.FormattingEnabled = true;
            this.cb_result.Location = new System.Drawing.Point(71, 25);
            this.cb_result.Name = "cb_result";
            this.cb_result.Size = new System.Drawing.Size(121, 22);
            this.cb_result.TabIndex = 2;
            this.cb_result.SelectedIndexChanged += new System.EventHandler(this.cb_result_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cb_profile);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cb_cardNumber);
            this.groupBox2.Location = new System.Drawing.Point(209, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 103);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Condition";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "Profile";
            // 
            // cb_profile
            // 
            this.cb_profile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_profile.FormattingEnabled = true;
            this.cb_profile.Location = new System.Drawing.Point(102, 63);
            this.cb_profile.Name = "cb_profile";
            this.cb_profile.Size = new System.Drawing.Size(121, 22);
            this.cb_profile.TabIndex = 4;
            this.cb_profile.SelectedIndexChanged += new System.EventHandler(this.cb_profile_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "Card Number";
            // 
            // cb_cardNumber
            // 
            this.cb_cardNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cardNumber.FormattingEnabled = true;
            this.cb_cardNumber.Location = new System.Drawing.Point(102, 25);
            this.cb_cardNumber.Name = "cb_cardNumber";
            this.cb_cardNumber.Size = new System.Drawing.Size(121, 22);
            this.cb_cardNumber.TabIndex = 2;
            this.cb_cardNumber.SelectedIndexChanged += new System.EventHandler(this.cb_cardNumber_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dt_to);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dt_from);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date";
            // 
            // dt_to
            // 
            this.dt_to.Location = new System.Drawing.Point(54, 62);
            this.dt_to.Name = "dt_to";
            this.dt_to.Size = new System.Drawing.Size(140, 22);
            this.dt_to.TabIndex = 3;
            this.dt_to.ValueChanged += new System.EventHandler(this.dt_to_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "To";
            // 
            // dt_from
            // 
            this.dt_from.Location = new System.Drawing.Point(54, 22);
            this.dt_from.Name = "dt_from";
            this.dt_from.Size = new System.Drawing.Size(140, 22);
            this.dt_from.TabIndex = 1;
            this.dt_from.ValueChanged += new System.EventHandler(this.dt_from_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "From";
            // 
            // dg_resultLog
            // 
            this.dg_resultLog.AllowUserToAddRows = false;
            this.dg_resultLog.AllowUserToDeleteRows = false;
            this.dg_resultLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_resultLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.resultIndex,
            this.sn,
            this.result,
            this.startTime,
            this.endTime,
            this.cardNumber,
            this.profileName});
            this.dg_resultLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_resultLog.Location = new System.Drawing.Point(0, 0);
            this.dg_resultLog.Name = "dg_resultLog";
            this.dg_resultLog.ReadOnly = true;
            this.dg_resultLog.RowHeadersVisible = false;
            this.dg_resultLog.RowTemplate.Height = 24;
            this.dg_resultLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_resultLog.Size = new System.Drawing.Size(975, 466);
            this.dg_resultLog.TabIndex = 0;
            // 
            // resultIndex
            // 
            this.resultIndex.HeaderText = "resultIndex";
            this.resultIndex.Name = "resultIndex";
            this.resultIndex.ReadOnly = true;
            this.resultIndex.Visible = false;
            // 
            // sn
            // 
            this.sn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sn.FillWeight = 30F;
            this.sn.HeaderText = "sn";
            this.sn.Name = "sn";
            this.sn.ReadOnly = true;
            // 
            // result
            // 
            this.result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.result.FillWeight = 20F;
            this.result.HeaderText = "result";
            this.result.Name = "result";
            this.result.ReadOnly = true;
            // 
            // startTime
            // 
            this.startTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.startTime.FillWeight = 25F;
            this.startTime.HeaderText = "startTime";
            this.startTime.Name = "startTime";
            this.startTime.ReadOnly = true;
            // 
            // endTime
            // 
            this.endTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.endTime.FillWeight = 25F;
            this.endTime.HeaderText = "endTime";
            this.endTime.Name = "endTime";
            this.endTime.ReadOnly = true;
            // 
            // cardNumber
            // 
            this.cardNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cardNumber.FillWeight = 15F;
            this.cardNumber.HeaderText = "cardNumber";
            this.cardNumber.Name = "cardNumber";
            this.cardNumber.ReadOnly = true;
            // 
            // profileName
            // 
            this.profileName.FillWeight = 15F;
            this.profileName.HeaderText = "profileName";
            this.profileName.Name = "profileName";
            this.profileName.ReadOnly = true;
            // 
            // ResultLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 587);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(850, 200);
            this.Name = "ResultLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ResultLog";
            this.Load += new System.EventHandler(this.resultLog_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_resultLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dg_resultLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dt_to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dt_from;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_result;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_profile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_cardNumber;
        private System.Windows.Forms.Button btn_deleteResult;
        private System.Windows.Forms.TextBox txt_sn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_releaseDiskSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn sn;
        private System.Windows.Forms.DataGridViewTextBoxColumn result;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn profileName;
    }
}