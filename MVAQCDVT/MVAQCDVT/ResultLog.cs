using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MVAFW.Common.Output;
using MVAFW.Common.Entity;

namespace MVAQCDVT
{
    public partial class ResultLog : Form
    {
        public ResultLog()
        {
            InitializeComponent();
            this.dg_resultLog.CellDoubleClick += new DataGridViewCellEventHandler(dg_resultLog_CellDoubleClick);

            if (eProductSetting.Lock == true)
            {
                this.btn_deleteResult.Enabled = false;
            }
            else if (eProductSetting.Lock == false)
            {
                this.btn_deleteResult.Enabled = true;
            }
        }

        private void dg_resultLog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int resultIndex = int.Parse(dg_resultLog.Rows[e.RowIndex].Cells["resultIndex"].Value.ToString());
            uint cardNumber = uint.Parse(dg_resultLog.Rows[e.RowIndex].Cells["cardNumber"].Value.ToString());

            ResultDetail fm = new ResultDetail(resultIndex, cardNumber);
            fm.Show();
            fm.Display();
        }

        private void resultLog_Load(object sender, EventArgs e)
        {
            initialDataTime();
            initialCondition();

            showResult();
        }

        private void initialCondition()
        {
            List<string> cardNumbers = oTestItemResult.GetEffectedValues("cardNumber");
            List<string> profileNames = oTestItemResult.GetEffectedValues("profileName");
            List<string> testResults = oTestItemResult.GetEffectedValues("result");

            cb_cardNumber.Items.Clear();
            this.cb_cardNumber.Items.Add("None");
            foreach (string cardNumber in cardNumbers)
            {
                this.cb_cardNumber.Items.Add(cardNumber);
            }

            cb_profile.Items.Clear();
            this.cb_profile.Items.Add("None");
            foreach (string profileName in profileNames)
            {
                this.cb_profile.Items.Add(profileName);
            }
            cb_profile.SelectedIndex = 0;


            cb_result.Items.Clear();
            this.cb_result.Items.Add("None");
            foreach (string testResult in testResults)
            {
                this.cb_result.Items.Add(testResult);
            }
            cb_result.SelectedIndex = 0;
        }

        private void initialDataTime()
        {
            this.dt_to.Value = DateTime.Now;
            this.dt_from.Value = this.dt_to.Value.AddMonths(-3);
        }

        private void dt_from_ValueChanged(object sender, EventArgs e)
        {
            eTestItemResult.StartTime = dt_from.Value;
            showResult();
        }

        private void dt_to_ValueChanged(object sender, EventArgs e)
        {
            eTestItemResult.EndTime = dt_to.Value;
            showResult();
        }

        private void showResult()
        {
            string queryCondition = string.Empty;

            if (cb_cardNumber.SelectedItem != null && cb_cardNumber.SelectedItem.ToString()!="None")
            {
                queryCondition += " AND cardNumber=" + cb_cardNumber.SelectedItem.ToString();
            }
            if (cb_profile.SelectedItem != null && cb_profile.SelectedItem.ToString()!="None")
            {
                queryCondition += " AND profileName='" + cb_profile.SelectedItem.ToString()+"'";
            }
            if (cb_result.SelectedItem != null && cb_result.SelectedItem.ToString()!="None")
            {
                queryCondition += " AND result='" + cb_result.SelectedItem.ToString() + "'";
            }
            if (txt_sn.Text.Trim() != string.Empty)
            {
                queryCondition += " AND sn like '%" + txt_sn.Text + "%'";
            }

            DataTable results =  oTestItemResult.GetTestResult(queryCondition);

            dg_resultLog.Rows.Clear();
            foreach (DataRow dr in results.Rows)
            {
                dg_resultLog.Rows.Add(int.Parse(dr["resultIndex"].ToString()),
                                                dr["sn"].ToString(),
                                                dr["result"].ToString(),
                                                dr["startTime"].ToString(),
                                                dr["endTime"].ToString(),
                                                int.Parse(dr["cardNumber"].ToString()),
                                                dr["profileName"].ToString()
                    );

                if (dr["result"].ToString() == "FAIL")
                {
                    dg_resultLog.Rows[dg_resultLog.Rows.Count - 1].DefaultCellStyle.BackColor = Color.IndianRed;
                }
                else if (dr["result"].ToString() == "UNKNOWN")
                {
                    dg_resultLog.Rows[dg_resultLog.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Pink;
                }
            }
        }

        private void cb_cardNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            showResult();
        }

        private void cb_profile_SelectedIndexChanged(object sender, EventArgs e)
        {
            showResult();
        }

        private void cb_result_SelectedIndexChanged(object sender, EventArgs e)
        {
            showResult();
        }

        private void btn_deleteResult_Click(object sender, EventArgs e)
        {
            if (dg_resultLog.SelectedRows.Count == 0)
            {
                MessageBox.Show("No Test Result selected!");
                return;
            }

            int[] resultIndexes = new int[dg_resultLog.SelectedRows.Count];

            for (int i = 0; i < resultIndexes.Length; i++)
            {
                resultIndexes[i] = int.Parse(dg_resultLog.SelectedRows[i].Cells["resultIndex"].Value.ToString());
            }

            oTestItemResult.DeleteResult(resultIndexes);

            showResult();
        }

        private void txt_sn_TextChanged(object sender, EventArgs e)
        {
            showResult();
        }

        private void btn_releaseDiskSpace_Click(object sender, EventArgs e)
        {
            oTestItemResult.ReleaseDiskSpace();
        }
    }
}
