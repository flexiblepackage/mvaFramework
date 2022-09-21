namespace MVAQCDVT
{
    partial class DataDisplay
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ch_data = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_export = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_scaledData = new System.Windows.Forms.RadioButton();
            this.rb_rawdata = new System.Windows.Forms.RadioButton();
            this.ch_histogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.flp_chart = new System.Windows.Forms.FlowLayoutPanel();
            this.sfd_export = new System.Windows.Forms.SaveFileDialog();
            this.chk_header = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ch_data)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ch_histogram)).BeginInit();
            this.SuspendLayout();
            // 
            // ch_data
            // 
            this.ch_data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ch_data.BackColor = System.Drawing.Color.Black;
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
            chartArea3.Position.Height = 94F;
            chartArea3.Position.Width = 80F;
            chartArea3.Position.X = 3F;
            chartArea3.Position.Y = 3F;
            this.ch_data.ChartAreas.Add(chartArea3);
            legend3.BackColor = System.Drawing.Color.Transparent;
            legend3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend3.ForeColor = System.Drawing.Color.SteelBlue;
            legend3.IsTextAutoFit = false;
            legend3.Name = "Legend1";
            legend3.Position.Auto = false;
            legend3.Position.Height = 40F;
            legend3.Position.Width = 15F;
            legend3.Position.X = 85F;
            legend3.Position.Y = 3F;
            legend3.TitleForeColor = System.Drawing.Color.SteelBlue;
            this.ch_data.Legends.Add(legend3);
            this.ch_data.Location = new System.Drawing.Point(-7, 0);
            this.ch_data.Name = "ch_data";
            this.ch_data.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            this.ch_data.Size = new System.Drawing.Size(627, 305);
            this.ch_data.TabIndex = 26;
            this.ch_data.Text = "chart2";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flp_chart);
            this.splitContainer1.Size = new System.Drawing.Size(1128, 550);
            this.splitContainer1.SplitterDistance = 307;
            this.splitContainer1.TabIndex = 27;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ch_histogram);
            this.splitContainer2.Panel2.Controls.Add(this.ch_data);
            this.splitContainer2.Size = new System.Drawing.Size(1128, 307);
            this.splitContainer2.SplitterDistance = 144;
            this.splitContainer2.TabIndex = 27;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_header);
            this.groupBox2.Controls.Add(this.btn_export);
            this.groupBox2.Location = new System.Drawing.Point(6, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(129, 91);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FILE";
            // 
            // btn_export
            // 
            this.btn_export.Location = new System.Drawing.Point(20, 52);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(75, 23);
            this.btn_export.TabIndex = 0;
            this.btn_export.Text = "Export";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_scaledData);
            this.groupBox1.Controls.Add(this.rb_rawdata);
            this.groupBox1.Location = new System.Drawing.Point(6, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option";
            // 
            // rb_scaledData
            // 
            this.rb_scaledData.AutoSize = true;
            this.rb_scaledData.Location = new System.Drawing.Point(20, 58);
            this.rb_scaledData.Name = "rb_scaledData";
            this.rb_scaledData.Size = new System.Drawing.Size(96, 18);
            this.rb_scaledData.TabIndex = 1;
            this.rb_scaledData.TabStop = true;
            this.rb_scaledData.Text = "ScaledData";
            this.rb_scaledData.UseVisualStyleBackColor = true;
            this.rb_scaledData.CheckedChanged += new System.EventHandler(this.rb_scaledData_CheckedChanged);
            // 
            // rb_rawdata
            // 
            this.rb_rawdata.AutoSize = true;
            this.rb_rawdata.Location = new System.Drawing.Point(20, 30);
            this.rb_rawdata.Name = "rb_rawdata";
            this.rb_rawdata.Size = new System.Drawing.Size(81, 18);
            this.rb_rawdata.TabIndex = 0;
            this.rb_rawdata.TabStop = true;
            this.rb_rawdata.Text = "Rawdata";
            this.rb_rawdata.UseVisualStyleBackColor = true;
            this.rb_rawdata.CheckedChanged += new System.EventHandler(this.rb_rawdata_CheckedChanged);
            // 
            // ch_histogram
            // 
            this.ch_histogram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ch_histogram.BackColor = System.Drawing.SystemColors.ControlDark;
            chartArea4.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea4.AxisX.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea4.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea4.BackColor = System.Drawing.Color.Black;
            chartArea4.Name = "ChartArea1";
            this.ch_histogram.ChartAreas.Add(chartArea4);
            legend4.BackColor = System.Drawing.Color.Transparent;
            legend4.BorderColor = System.Drawing.Color.White;
            legend4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend4.IsTextAutoFit = false;
            legend4.Name = "HistoLegend";
            legend4.TitleAlignment = System.Drawing.StringAlignment.Near;
            legend4.TitleBackColor = System.Drawing.Color.Transparent;
            this.ch_histogram.Legends.Add(legend4);
            this.ch_histogram.Location = new System.Drawing.Point(620, -1);
            this.ch_histogram.Name = "ch_histogram";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "HistoLegend";
            series2.Name = "histoData";
            this.ch_histogram.Series.Add(series2);
            this.ch_histogram.Size = new System.Drawing.Size(359, 306);
            this.ch_histogram.TabIndex = 28;
            this.ch_histogram.Text = "chart2";
            // 
            // flp_chart
            // 
            this.flp_chart.AutoScroll = true;
            this.flp_chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_chart.Location = new System.Drawing.Point(0, 0);
            this.flp_chart.Name = "flp_chart";
            this.flp_chart.Size = new System.Drawing.Size(1126, 237);
            this.flp_chart.TabIndex = 0;
            // 
            // sfd_export
            // 
            this.sfd_export.Filter = "csv 檔案|*.csv";
            // 
            // chk_header
            // 
            this.chk_header.AutoSize = true;
            this.chk_header.Location = new System.Drawing.Point(20, 28);
            this.chk_header.Name = "chk_header";
            this.chk_header.Size = new System.Drawing.Size(94, 18);
            this.chk_header.TabIndex = 1;
            this.chk_header.Text = "CH Header";
            this.chk_header.UseVisualStyleBackColor = true;
            // 
            // DataDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 550);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DataDisplay";
            this.Text = "DataDisplay";
            ((System.ComponentModel.ISupportInitialize)(this.ch_data)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ch_histogram)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ch_data;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flp_chart;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_scaledData;
        private System.Windows.Forms.RadioButton rb_rawdata;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.SaveFileDialog sfd_export;
        private System.Windows.Forms.DataVisualization.Charting.Chart ch_histogram;
        private System.Windows.Forms.CheckBox chk_header;
    }
}