namespace MVAQCDVT
{
    partial class RunTo
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
            this.label2 = new System.Windows.Forms.Label();
            this.txt_From = new System.Windows.Forms.TextBox();
            this.txt_To = new System.Windows.Forms.TextBox();
            this.btn_RunRange = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "sIndex From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "sIndex To:";
            // 
            // txt_From
            // 
            this.txt_From.Location = new System.Drawing.Point(95, 11);
            this.txt_From.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_From.Name = "txt_From";
            this.txt_From.Size = new System.Drawing.Size(109, 22);
            this.txt_From.TabIndex = 2;
            this.txt_From.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_From_KeyPress);
            // 
            // txt_To
            // 
            this.txt_To.Location = new System.Drawing.Point(95, 45);
            this.txt_To.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_To.Name = "txt_To";
            this.txt_To.Size = new System.Drawing.Size(108, 22);
            this.txt_To.TabIndex = 3;
            this.txt_To.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_To_KeyPress);
            // 
            // btn_RunRange
            // 
            this.btn_RunRange.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RunRange.Image = global::MVAQCDVT.Properties.Resources.Go;
            this.btn_RunRange.Location = new System.Drawing.Point(212, 15);
            this.btn_RunRange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_RunRange.Name = "btn_RunRange";
            this.btn_RunRange.Size = new System.Drawing.Size(65, 52);
            this.btn_RunRange.TabIndex = 4;
            this.btn_RunRange.Text = "GO";
            this.btn_RunRange.UseVisualStyleBackColor = true;
            this.btn_RunRange.Click += new System.EventHandler(this.btn_RunRange_Click);
            // 
            // RunTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 80);
            this.Controls.Add(this.btn_RunRange);
            this.Controls.Add(this.txt_To);
            this.Controls.Add(this.txt_From);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RunTo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partition Executeion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_From;
        private System.Windows.Forms.TextBox txt_To;
        private System.Windows.Forms.Button btn_RunRange;
    }
}