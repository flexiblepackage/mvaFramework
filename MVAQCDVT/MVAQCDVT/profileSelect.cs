using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MVAFW.Common.Output;

namespace MVAQCDVT
{
    public partial class profileSelect : Form
    {
        public int ProfileIndex { get; set; }
        public string ProfileName { get; set; }

        public profileSelect(string profileName)
        {
            InitializeComponent();
            this.ProfileName = profileName;
            this.AcceptButton = btn_select;
            this.ProfileIndex = -1;
        }

        private void profileSelect_Load(object sender, EventArgs e)
        {
            DataTable dt = oProductSetting.GetAllProfile();
            this.cb_profile.DataSource = dt;
            this.cb_profile.DisplayMember = "profileName";

            this.cb_profile.SelectedIndex = this.cb_profile.FindStringExact(ProfileName);
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            this.ProfileIndex = int.Parse(((DataRowView)this.cb_profile.SelectedItem).Row["profileIndex"].ToString());
            this.ProfileName = ((DataRowView)this.cb_profile.SelectedItem).Row["profileName"].ToString();
            this.Close();
        }
    }
}
