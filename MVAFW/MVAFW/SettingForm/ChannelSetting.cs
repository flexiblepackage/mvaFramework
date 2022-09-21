using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Reflection;

using MVAFW.TestItemColls;
using MVAFW.DB;
using MVAFW.Common.Entity;
using MVAFW.Common.Output;
using MVAFW.Config;

namespace MVAFW.SettingForm
{
    public partial class ChannelSetting : Form
    {
        ITypeDescriptorContext context;
        Assembly assembly;
        Dictionary<string, string> dictAllName;
        int maxChannelNumber;
        ushort[] channels;
        public StringBuilder ChannerStr;

        public ChannelSetting(int maxChannelNumber, Dictionary<string, string> dictAllName, ITypeDescriptorContext context)
        {
            InitializeComponent();

            this.dg_channelSetting.EditMode = DataGridViewEditMode.EditOnEnter;
            this.context = context;
            this.dictAllName = dictAllName;
            this.maxChannelNumber = maxChannelNumber;
            this.dg_channelSetting.DataError += new DataGridViewDataErrorEventHandler(dg_channelSetting_DataError);

            assembly = Assembly.LoadFrom(System.Windows.Forms.Application.StartupPath + "\\MVAFW.dll");

            DataGridViewTextBoxColumn channel = new DataGridViewTextBoxColumn();
            channel.Name = "Channels";
            channel.HeaderText = "Channels";
            channel.Width = 50;
            dg_channelSetting.Columns.Add(channel);

            foreach (KeyValuePair<string, string> pair in dictAllName)
            {                
                DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
                column.Name = pair.Key;
                column.HeaderText = pair.Key;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.FillWeight = 100 / dictAllName.Count;
                column.FlatStyle = FlatStyle.Flat;

                Type type = assembly.GetType(pair.Value);
                column.DataSource = Enum.GetValues(type);
                column.ValueType = type;

                dg_channelSetting.Columns.Add(column);
            }

            for (int i = 0; i < maxChannelNumber; i++)
            {
                dg_channelSetting.Rows.Add(false, "CH" + i.ToString());
            }

            displayExistedSetting();
        }

        private void displayExistedSetting()
        {
            DataTable dt = oTestItemManage.GetTestItemProperty(((AIOTestItem)context.Instance).Index, (uint)((AIOTestItem)context.Instance).CardNumber);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["name"].ToString() == "ChannelGainQueueString")
                {
                    string value = dr["value"].ToString();
                    if (value == "")
                    {
                        break;
                    }

                    this.channels = Array.ConvertAll(value.Split(':')[0].Split('=')[1].Split(','), new Converter<string, ushort>(ushort.Parse));
                    string[] configs = value.Split(':');

                    for (int i = 0; i < configs.Length; i++)
                    {
                        string name = configs[i].Split('=')[0];
                        string[] values = configs[i].Split('=')[1].Split(',');

                        for (int j = 0; j < values.Length; j++)
                        {
                            if (name == "Channels")
                            {
                                this.dg_channelSetting.Rows[channels[j]].Cells["Selected"].Value = true;
                            }
                            else
                            {
                                this.dg_channelSetting.Rows[channels[j]].Cells[name].Value = ushort.Parse(values[j]);
                            }
                        }
                    }
                }
            }
        }

        private void dg_channelSetting_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
          
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            AIOTestItem testItem = (AIOTestItem)context.Instance;
            bool selectedChannelChanged = false;
            bool channelSelected = false;

            foreach (DataGridViewColumn dc in dg_channelSetting.Columns)
            {
                if (dc.Name == "Selected")
                {
                    continue;
                }

                sb.Append(dc.Name+"=");

                for (int channel = 0; channel < dg_channelSetting.Rows.Count; channel++)
                {
                    bool selected = bool.Parse(dg_channelSetting.Rows[channel].Cells["Selected"].Value.ToString());
                    if (selected == false)
                    {
                        continue;
                    }
                    else
                    {
                        channelSelected = true;
                    }

                    if (dc.Name == "Channels")
                    {
                        sb.Append(channel.ToString() + ",");
                    }
                    else
                    {
                        Type type = assembly.GetType(dictAllName[dc.Name]);
                        ushort value = (ushort)(int)System.Enum.Parse(type, dg_channelSetting.Rows[channel].Cells[dc.Name].Value.ToString());
                        sb.Append(value + ",");
                    }
                }

                string pName = sb.ToString().Split(':')[sb.ToString().Split(':').Length - 1].Split('=')[sb.ToString().Split(':')[sb.ToString().Split(':').Length - 1].Split('=').Length-2];
                PropertyInfo pInfo = testItem.GetType().GetProperty(pName);
                ushort[] values;
                if (channelSelected == false)
                {
                    values = null;
                    pInfo.SetValue(testItem, values, null);
                    continue;
                }
                else if (channelSelected == true)
                {
                    sb.Remove(sb.Length - 1, 1);

                    values = Array.ConvertAll(sb.ToString().Split(':')[sb.ToString().Split(':').Length - 1].Split('=')[1].Split(','), new Converter<string, ushort>(ushort.Parse));

                    pInfo.SetValue(testItem, values, null);

                    if (pName == "Channels")
                    {
                        if (channels == null)
                        {
                            selectedChannelChanged = true;
                        }
                        else
                        {
                            for (int i = 0; i < Math.Max(this.channels.Length, values.Length); i++)
                            {
                                try
                                {
                                    if (channels[i] != values[i])
                                    {
                                        selectedChannelChanged = true;
                                        break;
                                    }
                                }
                                catch (IndexOutOfRangeException ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                    selectedChannelChanged = true;
                                    break;
                                }
                            }
                        }
                    }

                    sb.Append(":");
                }
            }

            int rowCount;
            int channelCount=0;           
            if (channelSelected == false)
            {
                sb.Remove(0, sb.Length);
                channelCount = 0;
            }
            else if (channelSelected == true)
            {
                sb.Remove(sb.Length - 1, 1);
                channelCount = sb.ToString().Split(':')[0].Split('=')[1].Split(',').Length;
            }
            
            Error err = MvaDSManager.MainDB.updateTestItemProperty(testItem.Index,
                                                                                             "ChannelGainQueueString", sb.ToString(), eProductSetting.ModelName, out rowCount,
                                                                                             (uint)testItem.CardNumber);            


            if (err != Error.NoError || rowCount == 0)
            {
                MessageBox.Show("Set channel Fail!");
            }
            else
            {
                MessageBox.Show("Set Success!");
                ChannerStr = sb;

                if (channelSelected == false)
                {
                    testItem.ChannelNumbers = 1;
                    err = MvaDSManager.MainDB.updateTestItemProperty(testItem.Index,
                                                                                                     "ChannelNumbers", "1", eProductSetting.ModelName, out rowCount,
                                                                                                     (uint)testItem.CardNumber);
                    if (err != Error.NoError || rowCount == 0)
                    {
                        MessageBox.Show("Set channel Fail!");
                    }

                    testItem.IsChannelGainQueueEnable = false;
                    err = MvaDSManager.MainDB.updateTestItemProperty(testItem.Index,
                                                                                                     "IsChannelGainQueueEnable", "False", eProductSetting.ModelName, out rowCount,
                                                                                                     (uint)testItem.CardNumber);

                    if (err != Error.NoError || rowCount == 0)
                    {
                        MessageBox.Show("Set channel Fail!");
                    }
                }

                if (selectedChannelChanged == true)
                {
                    testItem.ChannelNumbers = (ushort)channelCount;
                    testItem.IsChannelGainQueueEnable = true;
                    err = MvaDSManager.MainDB.updateTestItemProperty(testItem.Index,
                                                                                                     "IsChannelGainQueueEnable", "True", eProductSetting.ModelName, out rowCount,
                                                                                                     (uint)testItem.CardNumber);

                    if (err != Error.NoError || rowCount == 0)
                    {
                        MessageBox.Show("Set channel Fail!");
                    }

                    oTestItemSetting.ResetBaseValue(((AIOTestItem)context.Instance), (uint)((AIOTestItem)context.Instance).CardNumber);
                }                
            }
        }
    }

    public class ChannelSettingEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            AIOTestItem testItem = ((AIOTestItem)context.Instance);
            //string[] columns =  testItem.ChannelGainQueue;
            Dictionary<string, string> dictAllName = testItem.DicChannelGainQueue;
            int maxChannelNumber = testItem.MaxChannelNumber;
            string str;
           
            if (svc != null)
            {
                using (ChannelSetting form = new ChannelSetting(maxChannelNumber, dictAllName, context))
                {
                    if (svc.ShowDialog(form) == DialogResult.OK)
                    {
                        StringBuilder sb = form.ChannerStr;
                        str = sb.ToString();
                        value = str;
                    }                
                }                          
            }
            return value;
        }
    }
}
