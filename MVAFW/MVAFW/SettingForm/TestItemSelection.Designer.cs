namespace MVAFW.SettingForm
{
    partial class TestItemSelection
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
            this.tv_testItem = new System.Windows.Forms.TreeView();
            this.cms_treeview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToTestItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_treeview.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv_testItem
            // 
            this.tv_testItem.AllowDrop = true;
            this.tv_testItem.ContextMenuStrip = this.cms_treeview;
            this.tv_testItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_testItem.Location = new System.Drawing.Point(0, 0);
            this.tv_testItem.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tv_testItem.Name = "tv_testItem";
            this.tv_testItem.Size = new System.Drawing.Size(404, 381);
            this.tv_testItem.TabIndex = 0;
            this.tv_testItem.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_testItem_BeforeSelect);
            this.tv_testItem.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tv_testItem_MouseDoubleClick);
            this.tv_testItem.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tv_testItem_MouseUp);
            // 
            // cms_treeview
            // 
            this.cms_treeview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToTestItem});
            this.cms_treeview.Name = "cms_treeview";
            this.cms_treeview.Size = new System.Drawing.Size(167, 26);
            // 
            // addToTestItem
            // 
            this.addToTestItem.Name = "addToTestItem";
            this.addToTestItem.Size = new System.Drawing.Size(166, 22);
            this.addToTestItem.Text = "Add to TestItem";
            this.addToTestItem.Click += new System.EventHandler(this.addToTestItem_Click);
            // 
            // TestItemSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 381);
            this.Controls.Add(this.tv_testItem);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "TestItemSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestItemSelection";
            this.cms_treeview.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv_testItem;
        private System.Windows.Forms.ContextMenuStrip cms_treeview;
        private System.Windows.Forms.ToolStripMenuItem addToTestItem;
    }
}