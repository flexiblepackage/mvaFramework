using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MVAFW.Common.Output;

namespace MVAQCDVT
{
    public partial class ResultDetail : Form
    {
        private int resultIndex;
        private uint CardNumber;

        public ResultDetail(int resultIndex, uint cardNumber)
        {
            InitializeComponent();
            this.dg_resultDetail.CellDoubleClick += new DataGridViewCellEventHandler(dg_resultDetail_CellDoubleClick);
            this.dg_resultDetail.KeyPress += new KeyPressEventHandler(dg_resultDetail_KeyPress);
            this.dg_resultDetail.CellClick += new DataGridViewCellEventHandler(dg_resultDetail_CellClick);

            this.resultIndex = resultIndex;
            this.CardNumber = cardNumber;
            //Display();
        }

        private void dg_resultDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex==-1)
            {
                return;
            }

            int testItemIndex=int.Parse( dg_resultDetail.Rows[e.RowIndex].Cells["testItemIndex"].Value.ToString());
            List<string> propertys = oTestItemResult.GetTestProperty(testItemIndex);

            flp_testProperty.Controls.Clear();
            foreach (string property in propertys)
            {
                Label lbl_property = new Label();
                lbl_property.Size = new System.Drawing.Size(300,25);
                lbl_property.ForeColor = Color.Blue;
                lbl_property.Text = property;

                flp_testProperty.Controls.Add(lbl_property);
            }
        }

        private void dg_resultDetail_KeyPress(object sender, KeyPressEventArgs e)
        {      
            //13 means enter
            if (e.KeyChar != (char)Keys.Enter || dg_resultDetail.SelectedRows.Count == 0)
            {   
                return;
            }

            int selectionCount = dg_resultDetail.SelectedRows.Count;
            double[][] datas;
            double[][] scaledDatas;
            int[] resultDetailIndexs = new int[selectionCount];
            int[] channels = new int[selectionCount];

            for (int i = 0; i < selectionCount; i++)
            {
                resultDetailIndexs[i] = int.Parse(dg_resultDetail.SelectedRows[i].Cells["resultDetailIndex"].Value.ToString());
                channels[i] = int.Parse(dg_resultDetail.SelectedRows[i].Cells["channel"].Value.ToString());
            }

            int dataCount = oTestItemResult.GetDatas(resultDetailIndexs, channels, out datas, out scaledDatas);

            if (dataCount == 0)
            {
                MessageBox.Show("No data found!");
                return;
            }

            DataDisplay fm = new DataDisplay(datas, scaledDatas, channels);
            fm.Show();
        }

        private void dg_resultDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            double[][] datas;
            double[][] scaledDatas;
            int[] resultDetailIndex = new int[1];
            int[] channels = new int[1];
            resultDetailIndex[0] = int.Parse(dg_resultDetail.Rows[e.RowIndex].Cells["resultDetailIndex"].Value.ToString());
            channels[0] = int.Parse(dg_resultDetail.Rows[e.RowIndex].Cells["channel"].Value.ToString());
            int dataCount = oTestItemResult.GetDatas(resultDetailIndex, channels, out datas, out scaledDatas);

            if (dataCount == 0)
            {
                MessageBox.Show("No data found!");
                return;
            }

            DataDisplay fm = new DataDisplay(datas, scaledDatas, channels);
            fm.Show();
        }

        public void Display()
        {
            DataTable dt = oTestItemManage.GetTestResultDetail(this.resultIndex, this.CardNumber);

            dg_resultDetail.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                dg_resultDetail.Rows.Add(int.Parse(dr["resultDetailIndex"].ToString()),
                                                  int.Parse(dr["testItemIndex"].ToString()),
                                                  dr["name"].ToString(),
                                                  dr["spec"].ToString(),
                                                  dr["value"].ToString(),
                                                  int.Parse(dr["channel"].ToString()),
                                                  int.Parse(dr["cycle"].ToString()),
                                                  DateTime.Parse(dr["time"].ToString()),
                                                  dr["Description"].ToString(),
                                                  dr["resultDetail"].ToString()                                                
                    );

                if (cb_testItemIndex.Items.Contains(dr["testItemIndex"].ToString()) == false)
                {
                    cb_testItemIndex.Items.Add(dr["testItemIndex"].ToString());
                }
            }          
        }

        private void btn_cycleAnalysis_Click(object sender, EventArgs e)
        {
            if (cb_testItemIndex.SelectedIndex < 0)
                return;
            int testItemIndex = int.Parse(cb_testItemIndex.SelectedItem.ToString());
            DataTable dt = oTestItemResult.GetResultDetail(this.resultIndex, testItemIndex);
            DataTable dtChannels = oTestItemResult.GetGroupValuesOfCycle(this.resultIndex, testItemIndex, "channel");
            DataTable dtTimes = oTestItemResult.GetValuesOfCycle(this.resultIndex, testItemIndex);

            int[] channels = new int[dtChannels.Rows.Count];
            for (int i = 0; i < channels.Length; i++)
            {
                channels[i] = int.Parse(dt.Rows[i]["channel"].ToString());
            }

            double[][] values = new double[channels.Length][];
            int cycle = dt.Rows.Count / channels.Length;
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = new double[cycle];
                DataRow[] drs = dt.Select("channel=" + channels[i].ToString());
                for (int dpIndex = 0; dpIndex < drs.Length; dpIndex++)
                {
                    double value = double.Parse(drs[dpIndex]["value"].ToString());

                    values[i][dpIndex]=value;                    
                }
            }

            string[] cycleX = new string[cycle];
            for (int i = 0; i < cycle; i++)
            {
                if (rb_cycle.Checked == true)
                {
                    cycleX[i] = (i + 1).ToString();
                }
                else if (rb_time.Checked == true)
                {
                    cycleX[i] = DateTime.Parse(dtTimes.Rows[i]["time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }

            DataDisplay fm = new DataDisplay(values, cycleX, channels);
            fm.Show();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {  
            SaveFileDialog savefile = new SaveFileDialog();            
            savefile.Filter = "csv files (*.csv)|*.csv";
            savefile.FileName = "testlog.csv";

            if(savefile.ShowDialog() == DialogResult.OK)
            {               
                btn_save.Text = "saving......";
                btn_save.Enabled = false;

                StreamWriter sw = new StreamWriter(savefile.OpenFile(), Encoding.Default);
                string sTemp = string.Empty;
                sTemp = sTemp = "tIndex" + ",name" + ",spec" + ",value" + ",channel" + ",cycle" + ",time" + ",description" + ",result\r\n";
                for (int i = 0; i < dg_resultDetail.Rows.Count; i++)
                {
                    sTemp = sTemp + dg_resultDetail.Rows[i].Cells["testItemIndex"].Value.ToString();
                    sTemp = sTemp + "," + dg_resultDetail.Rows[i].Cells["name"].Value.ToString();
                    sTemp = sTemp + "," + dg_resultDetail.Rows[i].Cells["spec"].Value.ToString();
                    sTemp = sTemp + "," + dg_resultDetail.Rows[i].Cells["value"].Value.ToString();
                    sTemp = sTemp + "," + dg_resultDetail.Rows[i].Cells["channel"].Value.ToString();
                    sTemp = sTemp + "," + dg_resultDetail.Rows[i].Cells["cycle"].Value.ToString();
                    sTemp = sTemp + "," + dg_resultDetail.Rows[i].Cells["time"].Value.ToString();
                    sTemp = sTemp + "," + dg_resultDetail.Rows[i].Cells["description"].Value.ToString();
                    sTemp = sTemp + "," + dg_resultDetail.Rows[i].Cells["result"].Value.ToString() + "\r\n";
                }
                sw.Write(sTemp);
                sw.Close();
                MessageBox.Show("Save file OK!");
                btn_save.Text = "SaveLog";
                btn_save.Enabled = true;
            }
        }
    }
}
