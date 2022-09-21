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
    public partial class Password : Form
    {
        public string PASSWORD { get; set; }

        public Password()
        {            
            InitializeComponent();
            this.AcceptButton = btn_ok;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.PASSWORD = txt_passwd.Text;
            this.Close();
        } 
    }
}
