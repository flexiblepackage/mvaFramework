namespace MVAQCDVT
{
    partial class profileSelect
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
            this.label1 = new System.Windows.Forms.Label();
            this.cb_profile = new System.Windows.Forms.ComboBox();
            this.btn_select = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profile";
            // 
            // cb_profile
            // 
            this.cb_profile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_profile.FormattingEnabled = true;
            this.cb_profile.Location = new System.Drawing.Point(64, 6);
            this.cb_profile.Name = "cb_profile";
            this.cb_profile.Size = new System.Drawing.Size(138, 22);
            this.cb_profile.TabIndex = 1;
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(208, 5);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(57, 23);
            this.btn_select.TabIndex = 2;
            this.btn_select.Text = "OK";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // profileSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 34);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.cb_profile);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "profileSelect";
            this.Text = "profileSelect";
            this.Load += new System.EventHandler(this.profileSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_profile;
        private System.Windows.Forms.Button btn_select;
    }
}