using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

using MVAFW.Common.Output;
using MVAFW.Common.Entity;
using MVAFW.Config;

namespace MVAQCDVT
{
    public partial class productSetting : Form
    {
        public productSetting(bool qcLock)
        {
            InitializeComponent();

            if(qcLock==true)
            {
                foreach(Control groupBox in this.Controls)
                {
                    foreach (Control control in groupBox.Controls)
                    {
                        if (control is TextBox && ((TextBox)control).Name == "txt_instrument")
                        {
                            continue;
                        }
                        else if (control is Button && ((Button)control).Name == "btn_save")
                        {
                            continue;
                        }
                        else
                        {
                            control.Enabled = false;
                        }
                    }
                }
            }
        }

        void list_testProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txt_profileName.Text = ((DataRowView)list_testProfile.SelectedItem).Row["profileName"].ToString();
        }

        private void productSetting_Load(object sender, EventArgs e)
        {
            display();
            this.list_testProfile.SelectedIndexChanged += new EventHandler(list_testProfile_SelectedIndexChanged);
        }

        private void display()
        {
            //product info
            this.txt_modelName.Text = eProductSetting.ModelName;
            this.txt_driverVersion.Text = eProductSetting.DriverVersion;
            this.txt_engineer.Text = eProductSetting.Engineer;
            this.txt_firmwareVersion.Text = eProductSetting.FirmwareVersion;
            this.txt_fpgaVersion.Text = eProductSetting.FpgaVersion;
            this.txt_instrument.Text = eProductSetting.TestInstrument;
            this.txt_qcVersion.Text = eProductSetting.QcProgramVersion;
            this.txt_macID.Text = eProductSetting.MacID;
            this.txt_mcuVersion.Text = eProductSetting.McuVersion;
            this.txt_productDesciption.Text = eProductSetting.ProductDescription;

            //TestItemManagement
            list_testItemMgmt.Items.Clear();
            List<string> classNames = oProductSetting.GetAllTestItemClass();
            foreach(string className in classNames)
            {
                list_testItemMgmt.Items.Add(className);
            }

            //TestProfileManagement
            DataTable dt = oProductSetting.GetAllProfile();
            list_testProfile.DataSource = dt;
            list_testProfile.DisplayMember = "profileName";
            this.txt_profileName.Text = string.Empty;

            //WebLog
            this.txt_WebPath.Text = eProductSetting.WebPath;
            this.txt_WebID.Text = eProductSetting.WebID;
            this.txt_WebPassword.Text = eProductSetting.WebPassword;
            this.txt_WebDisk.Text = eProductSetting.WebDisk;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Error result = updateModelName();
                     
            if (result == Error.NoError)
            {
                 result = oProductSetting.UpdateProductSetting(txt_modelName.Text, txt_fpgaVersion.Text, txt_mcuVersion.Text,
                                                                              txt_firmwareVersion.Text, txt_qcVersion.Text, txt_macID.Text, txt_productDesciption.Text,
                                                                              txt_driverVersion.Text, txt_engineer.Text, txt_instrument.Text);

                MessageBox.Show("Save Successfully!");
                oProductSetting.GetProductSetting();               
                display();
            }
            else
            {
                MessageBox.Show("Save Fail!");
            }
        }
       
        private Error updateModelName()
        {
            Error result = oTestItemSetting.UpdateTestItemModelName("ModelName", txt_modelName.Text);
            if (result == Error.NoError)

            
            if (result != Error.NoError)
            {
                MessageBox.Show("Save Fail!");
                return result;
            }

            return Error.NoError;
        }

        private void btn_addTestItemClass_Click(object sender, EventArgs e)
        {
            if (dia_chooseClass.ShowDialog() == DialogResult.OK)
            {
                Error err = Error.NoError;
                string path = Path.GetFullPath(dia_chooseClass.FileName);
                string extenstion = Path.GetExtension(path);

                if (extenstion == ".cs")
                {
                    Regex patNameSpace = new Regex(@"^namespace\s+(?<namespace>\S+)$");
                    Regex patClassName = new Regex(@"class\s+(?<className>\w+):?");
                    string line;
                    StreamReader sr = new StreamReader(path);
                    string nameSpace = string.Empty;
                    string className = string.Empty;
                    string classDisplayName= string.Empty;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (patNameSpace.IsMatch(line) == true)
                        {
                            nameSpace = patNameSpace.Matches(line)[0].Groups["namespace"].Value;
                        }
                        if (patClassName.IsMatch(line) == true)
                        {
                            className = patClassName.Matches(line)[0].Groups["className"].Value;
                            classDisplayName = patClassName.Matches(line)[0].Groups["className"].Value + ":" + nameSpace.Substring(nameSpace.LastIndexOf('.') + 1, (nameSpace.Length - 1) - nameSpace.LastIndexOf('.'));
                        }
                    }
                    sr.Close();

                    if (nameSpace == string.Empty)
                    {
                        MessageBox.Show("Can't find namespace!");
                    }
                    else if (className == string.Empty)
                    {
                        MessageBox.Show("Can't find class Name!");
                    }
                    else
                    {
                        err = oTestItemSetting.AddTestItemClass(classDisplayName, nameSpace + "." + className);                     
                    }
                }
                else if (extenstion == ".bat" || extenstion == ".exe")
                {
                    err = oTestItemSetting.AddTestItemClass(Path.GetFileName(path), "MVAFW.TestItemColls.batexe");
                }
                else
                {
                    MessageBox.Show("Wrong file!");
                    err = Error.GeneralError;
                }

                if (err == Error.NoError)
                {
                    MessageBox.Show("Add Test Item Class Success!");
                    display();
                }
                else
                {
                    MessageBox.Show("Add Test Item Class Fail!");
                }
            }           
        }

        private void btn_delTestItemClass_Click(object sender, EventArgs e)
        {
            if (list_testItemMgmt.SelectedItem == null)
            {
                MessageBox.Show("Please select test item class!");
                return;
            }

            Error err = oTestItemSetting.DelTestItemClass(list_testItemMgmt.SelectedItem.ToString());

            if (err != Error.NoError)
            {
                MessageBox.Show(err.ToString());
            }
            else
            {
                list_testItemMgmt.Items.Remove(list_testItemMgmt.SelectedItem);
            }
        }

        private void btn_addProfile_Click(object sender, EventArgs e)
        {
            if (this.txt_profileName.Text.ToString().Trim() == string.Empty)
            {
                MessageBox.Show("Profile Name empty!");
                return;
            }

            oProductSetting.AddProfile(this.txt_profileName.Text.ToString());
            display();
        }

        private void btn_delProfile_Click(object sender, EventArgs e)
        {
            if (this.list_testProfile.SelectedItem == null)
            {
                MessageBox.Show("Please select the profile!");
                return;
            }

            string profileName = ((DataRowView)list_testProfile.SelectedItem).Row["profileName"].ToString();
            int profileIndex = int.Parse( ((DataRowView)list_testProfile.SelectedItem).Row["profileIndex"].ToString());
            if ((MessageBox.Show("Are you sure deleting profile " + profileName,"Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information)) == DialogResult.Yes)
            {
                oProductSetting.DelProfile(profileIndex);
                display();
            }
        }
    }
}
