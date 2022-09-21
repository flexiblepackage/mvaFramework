using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVAFW.TestItemColls.COMMON
{
    public partial class PictureMsgForm : Form
    {
        public PictureMsgForm(string msg, string img)
        {
            InitializeComponent();
            msgLabel.Text = msg;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            if (img != null && img !="")
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(img);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
