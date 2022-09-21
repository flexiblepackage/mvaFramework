using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MVAFW.Common.Output;
using MVAFW.Config;

namespace MVAQCDVT
{
    public partial class aliasNameSetting : Form
    {
        private int testItemIndex;
        public string AliasName { get; set; }
        public bool UpdateSuccess { get; set; }

        public aliasNameSetting(int testItemIndex, string AliasName)
        {
            InitializeComponent();
            this.AcceptButton = btn_ok;
            this.testItemIndex = testItemIndex;
            txt_aliasName.Text = AliasName;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.AliasName = this.txt_aliasName.Text;
            if(AliasName.Length < 0)
            {
                this.Close();
            }

            Error err = oTestItemSetting.UpdateTestItemAliasName(this.testItemIndex, this.AliasName);

            if(err != Error.NoError)
            {
                MessageBox.Show("Update FAIL");
                this.UpdateSuccess = false;
            }
            else
            {
                this.UpdateSuccess = true;
                this.Close();
            }
        }
    }
}
