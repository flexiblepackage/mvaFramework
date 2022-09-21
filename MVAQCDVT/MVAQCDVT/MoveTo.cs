using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MVAQCDVT
{
    public partial class MoveTo : Form
    {
        public string sColumn;

        public MoveTo()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            doOK();
        }

        private void txt_ColumnNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
                e.Handled = false;
            if (e.KeyChar == (char)Keys.Enter)
                doOK();
        }

        private void doOK()
        {
            if (txt_ColumnNo.Text == string.Empty)
            {
                System.Windows.Forms.MessageBox.Show("Please enter the sIndex number.");
            }
            else if (txt_ColumnNo.Text == "0")
            {
                System.Windows.Forms.MessageBox.Show("sIndex number can't be 0.");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                sColumn = txt_ColumnNo.Text;
            }
        }
    }
}
