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
    public partial class RunTo : Form
    {
        public string sRunFrom;
        public string sRunTo;   

        public RunTo()
        {            
            InitializeComponent();
        }

        private void btn_RunRange_Click(object sender, EventArgs e)
        {          
            if (txt_From.Text != string.Empty && txt_To.Text != string.Empty)
            {
                int nfrom = Convert.ToInt16(txt_From.Text);
                int nto = Convert.ToInt16(txt_To.Text);

                if (nto < nfrom)
                {
                    MessageBox.Show("Set Range fail, please check the enter range again!");
                }
                else if(nfrom == 0 || nto == 0)
                {
                    MessageBox.Show("sIndex can't be 0.");
                }
                else
                {
                    sRunFrom = txt_From.Text;
                    sRunTo = txt_To.Text;
                    this.DialogResult = DialogResult.OK;
                }               
            }
            else
            {
                MessageBox.Show("Please enter the range to test!");
            }
        }

        private void txt_From_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckifDigital(e);
        }

        private void txt_To_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckifDigital(e);
        }

        private void CheckifDigital(KeyPressEventArgs e)
        {
            e.Handled = true;
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
                e.Handled = false;
        }
    }
}
