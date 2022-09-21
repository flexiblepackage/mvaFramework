using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MVAFW.TestItemColls.COMMON
{
    public class MessageDialog : BasicTestItem
    {
        public string Message { get; set; }
        public string ImagePath { get; set; }

        private void fn_HelpRequested(object sender, System.Windows.Forms.HelpEventArgs hlpevent)
        {
            // This event is raised when the F1 key is pressed or the
            // Help cursor is clicked on any of the address fields.
            // The Help text for the field is in the control's
            // Tag property. It is retrieved and displayed in the label.

            Control requestingControl = (Control)sender;
            //helpLabel.Text = (string)requestingControl.Tag;
            hlpevent.Handled = true;
        }


        public override void doTest()
        {
            base.doTest();
            //new System.Diagnostics.Process
            //{
            //    StartInfo = new System.Diagnostics.ProcessStartInfo(@"D:\foo.PNG")
            //    {
            //        UseShellExecute = true
            //    }
            //}.Start();
            PictureMsgForm msgBox = null;
            if (System.IO.File.Exists(ImagePath))
                msgBox = new PictureMsgForm(Message, ImagePath);

            this.Values[0] = "1";

            if (msgBox != null && msgBox.ShowDialog() == DialogResult.Yes)
            {
                this.Values[0] = "0";
            }
            if (msgBox == null && MessageBox.Show(Message, "Test Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Values[0] = "0";
            }
        }

        public MessageDialog()
        {
            this.Message = "You could modify this message at property setting!!!";
            this.ImagePath = "C:\foo.png";

        }
    }
}
