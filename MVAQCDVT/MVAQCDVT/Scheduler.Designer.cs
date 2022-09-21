namespace MVAQCDVT
{
    partial class Scheduler
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
            this.nud_minutes = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.rb_autoRestart = new System.Windows.Forms.RadioButton();
            this.rb_autoShutDown = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_none = new System.Windows.Forms.RadioButton();
            this.chk_autoStartWhenBoot = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nud_minutes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nud_minutes
            // 
            this.nud_minutes.Location = new System.Drawing.Point(370, 81);
            this.nud_minutes.Name = "nud_minutes";
            this.nud_minutes.Size = new System.Drawing.Size(65, 22);
            this.nud_minutes.TabIndex = 1;
            this.nud_minutes.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nud_minutes.ValueChanged += new System.EventHandler(this.nud_minutes_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Restart PC Period(Miniutes)";
            // 
            // rb_autoRestart
            // 
            this.rb_autoRestart.AutoSize = true;
            this.rb_autoRestart.Location = new System.Drawing.Point(6, 47);
            this.rb_autoRestart.Name = "rb_autoRestart";
            this.rb_autoRestart.Size = new System.Drawing.Size(100, 18);
            this.rb_autoRestart.TabIndex = 3;
            this.rb_autoRestart.TabStop = true;
            this.rb_autoRestart.Text = "AutoRestart";
            this.rb_autoRestart.UseVisualStyleBackColor = true;
            // 
            // rb_autoShutDown
            // 
            this.rb_autoShutDown.AutoSize = true;
            this.rb_autoShutDown.Location = new System.Drawing.Point(6, 71);
            this.rb_autoShutDown.Name = "rb_autoShutDown";
            this.rb_autoShutDown.Size = new System.Drawing.Size(127, 18);
            this.rb_autoShutDown.TabIndex = 4;
            this.rb_autoShutDown.TabStop = true;
            this.rb_autoShutDown.Text = "AutoShoutDown";
            this.rb_autoShutDown.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_none);
            this.groupBox1.Controls.Add(this.rb_autoRestart);
            this.groupBox1.Controls.Add(this.rb_autoShutDown);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(148, 95);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Turn Off Type";
            // 
            // rb_none
            // 
            this.rb_none.AutoSize = true;
            this.rb_none.Location = new System.Drawing.Point(6, 23);
            this.rb_none.Name = "rb_none";
            this.rb_none.Size = new System.Drawing.Size(58, 18);
            this.rb_none.TabIndex = 5;
            this.rb_none.TabStop = true;
            this.rb_none.Text = "None";
            this.rb_none.UseVisualStyleBackColor = true;
            // 
            // chk_autoStartWhenBoot
            // 
            this.chk_autoStartWhenBoot.AutoSize = true;
            this.chk_autoStartWhenBoot.Location = new System.Drawing.Point(18, 125);
            this.chk_autoStartWhenBoot.Name = "chk_autoStartWhenBoot";
            this.chk_autoStartWhenBoot.Size = new System.Drawing.Size(152, 18);
            this.chk_autoStartWhenBoot.TabIndex = 6;
            this.chk_autoStartWhenBoot.Text = "AutoStartWhenBoot";
            this.chk_autoStartWhenBoot.UseVisualStyleBackColor = true;
            this.chk_autoStartWhenBoot.CheckedChanged += new System.EventHandler(this.chk_autoStartWhenBoot_CheckedChanged);
            // 
            // Scheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 155);
            this.Controls.Add(this.chk_autoStartWhenBoot);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nud_minutes);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Scheduler";
            this.Text = "Scheduler";
            ((System.ComponentModel.ISupportInitialize)(this.nud_minutes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nud_minutes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_autoRestart;
        private System.Windows.Forms.RadioButton rb_autoShutDown;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_none;
        private System.Windows.Forms.CheckBox chk_autoStartWhenBoot;
    }
}