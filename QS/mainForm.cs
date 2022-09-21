using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

using MVAFW;
using MVAFW.Config;
using MVAFW.DB;
using MVAFW.Common.Entity;
using MVAFW.Common.Output;
using MVAFW.Analysis;
using MVAFW.API;
using MVAFW.TestItemColls;

namespace QS
{
    public enum STEP
    {
        AlongWithChannel,
        AcquireMode,
        BasicSetting,
        TriggerSetting
    }

    public partial class mainForm : Form
    {
        private Assembly assembly;
        private STEP step;
        private DataGridView dg_channelSetting;
        private AITestItem aiTestItem;
        private PropertyInfo[] aiPropertyInfos;
        private Thread aiThread;
        //private uapi;
        private int groupBoxWidth = 340;
        private int betweenLabel = 130;
        private System.Windows.Forms.Timer pollingTimer = new System.Windows.Forms.Timer();

        public mainForm()
        {
            InitializeComponent();
            assembly = Assembly.LoadFrom(System.Windows.Forms.Application.StartupPath + "\\MVAFW.dll");
            eMVACollection.Initial();

            this.flp_dynamic.SizeChanged += flp_dynamic_SizeChanged;
        }

        void flp_dynamic_SizeChanged(object sender, EventArgs e)
        {
            if (dg_channelSetting != null)
            {
                dg_channelSetting.Size = new System.Drawing.Size(this.flp_dynamic.Size.Width - 10, this.flp_dynamic.Size.Height - 10);
            }
        }

        private void btn_ai_Click(object sender, EventArgs e)
        {
            step = STEP.AlongWithChannel;
            this.btn_next.Enabled = true;

            Type type = assembly.GetType(eMVACollection.TestItemMap["USB-1210"].TestItem);
            PropertyInfo[] propertyInfos = type.GetProperties();
            AITestItem aiTestItem = (AITestItem)Activator.CreateInstance(type);
            this.aiTestItem = aiTestItem;
            this.aiPropertyInfos = propertyInfos;
            aiStep1AlongWithChannel(propertyInfos, aiTestItem);
        }

        #region Create AI Channel Select UI
        private void createDefaultChannelSelectionColumn()
        {
            if (dg_channelSetting != null)
            {
                dg_channelSetting.Rows.Clear();
                this.flp_dynamic.Controls.Remove(dg_channelSetting);
            }
            //Create DataGridView for Channel and Config selection
            dg_channelSetting = new DataGridView();
            dg_channelSetting.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            dg_channelSetting.Size = new System.Drawing.Size(this.flp_dynamic.Size.Width-10, this.flp_dynamic.Size.Height-10);
            dg_channelSetting.Name = "dg_channelSetting";
            dg_channelSetting.RowHeadersVisible = false;
            dg_channelSetting.RowTemplate.Height = 24;
            dg_channelSetting.AllowUserToAddRows = false;
            dg_channelSetting.EditMode = DataGridViewEditMode.EditOnEnter;
            dg_channelSetting.DataError += new DataGridViewDataErrorEventHandler(dg_channelSetting_DataError);
            this.flp_dynamic.Controls.Add(dg_channelSetting);

            //select column
            DataGridViewCheckBoxColumn Selected = new DataGridViewCheckBoxColumn();
            Selected.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Selected.Width = 70;
            Selected.HeaderText = "Selected";
            Selected.Name = "Selected";
            dg_channelSetting.Columns.Add(Selected);

            //channel column
            DataGridViewTextBoxColumn channel = new DataGridViewTextBoxColumn();
            channel.Name = "Channels";
            channel.HeaderText = "Channels";
            channel.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dg_channelSetting.Columns.Add(channel);
        }

        private void createDynamicColumn(PropertyInfo[] propertyInfos, AITestItem aiTestItem)
        {
            int columnCount = 0;

            //create dynamic column
            foreach (PropertyInfo property in propertyInfos)
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(property))
                {
                    if (attr.GetType() == typeof(DAQFuncAttribute) && ((DAQFuncAttribute)attr).DAQFunction == DAQFunction.AlongWithChannel)
                    {
                        columnCount++;

                        DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
                        column.Name = property.Name;
                        column.HeaderText = property.Name;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        column.FlatStyle = FlatStyle.Flat;

                        if (property.PropertyType.BaseType == typeof(System.Array))
                        {
                            column.DataSource = Enum.GetValues(property.PropertyType.GetElementType());
                            column.ValueType = property.PropertyType.GetElementType();
                        }
                        else
                        {
                            column.DataSource = Enum.GetValues(property.PropertyType);
                            column.ValueType = property.PropertyType;
                        }

                        dg_channelSetting.Columns.Add(column);
                    }
                }
            }

            foreach (DataGridViewColumn column in dg_channelSetting.Columns)
            {
                column.FillWeight = 100 / (columnCount + 1);
            }

            for (int i = 0; i < aiTestItem.MaxChannelNumber; i++)
            {
                dg_channelSetting.Rows.Add(false, "CH" + i.ToString());
            }
        }

        #endregion

        #region Create AI Acquire Mode UI
        private void createAcquireModeUI()
        {
            string groupBoxName = "gb_acquire";

            createGroupBoxUI(groupBoxName, "Basic Setting");
            createControlUI(groupBoxName, DAQFunction.AcquireMode);
        }
        #endregion

        private void createTriggerSettingUI()
        {
            string groupBoxName = "gb_triggerSetting";

            createGroupBoxUI(groupBoxName, "Trigger Setting");
            createControlUI(groupBoxName,DAQFunction.TriggerSource);
            this.btn_next.Enabled = false;
        }

        private bool createBasicSettingUI()
        {
            return false;
        }

        void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string groupBoxName = textBox.Parent.Name;
            string textBoxName = textBox.Name;
            string value = ((TextBox)this.flp_dynamic.Controls[groupBoxName].Controls[textBoxName]).Text.ToString();

            assignValue(groupBoxName, textBoxName);
        }

        private bool isAttributeInProperty(PropertyInfo propertyInfo, DAQFunction daqFunction)
        {
            foreach (Attribute attr in propertyInfo.GetCustomAttributes(typeof(DAQFuncAttribute), false))
            {
                if (attr.GetType() == typeof(DAQFuncAttribute) && ((DAQFuncAttribute)attr).DAQFunction == daqFunction)
                {
                    return true;
                }
            }

            return false;
        }

        private void combox_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string groupBoxName = comboBox.Parent.Name;
            string comboBoxName = comboBox.Name;
            string labelName = comboBox.Text;
            string selectedValue = ((ComboBox)this.flp_dynamic.Controls[groupBoxName].Controls[comboBoxName]).SelectedItem.ToString();
            PropertyInfo propertyInfo = (PropertyInfo)comboBox.Tag;

            if(isAttributeInProperty(propertyInfo, DAQFunction.AcquireMode))
            {
                this.aiTestItem.DoubleBufferCount = uint.MaxValue;
                assignValue(groupBoxName, comboBoxName);

                //if polling, show interval, if DMA, show sampling rate
                if (this.aiTestItem.AcquireMode == AIAcquireModeConfig.Polling)
                {
                    createControlUI(groupBoxName, DAQFunction.PollingInterval);
                    this.btn_start.Enabled = true;
                    this.btn_stop.Enabled = true;
                }
                else
                {
                    createControlUI(groupBoxName, DAQFunction.BasicSetting);
                }
            }
            else if (isAttributeInProperty(propertyInfo, DAQFunction.TriggerSource))
            {               
                assignValue(groupBoxName, comboBoxName);

                if (!isEndAttribute(selectedValue))
                {
                    createTriggerModeSettingUI();
                }
                else
                {
                    removeNonSoftTriggerSetting(groupBoxName);
                    this.btn_start.Enabled = true;
                    this.btn_stop.Enabled = true ;
                }
            }
            else
            {
                assignValue(groupBoxName, comboBoxName);
            }
        }

        private void removeNonSoftTriggerSetting(string groupBoxName)
        {
            DAQFunction[] nonSoftTriggerSetting = new DAQFunction[] { DAQFunction.TriggerMode, DAQFunction.TriggerPolarity, 
                                                                                               DAQFunction.TriggerLevel, DAQFunction.ReTriggerCtrl, DAQFunction.ReTriggerCount };
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();

            foreach (DAQFunction daqFunction in nonSoftTriggerSetting)
            {
                propertyInfos = getPropertyInfo(DAQFunction.TriggerMode);
                if (propertyInfos.Count>0)
                {
                    removeControlUI(groupBoxName, getPropertyInfo(daqFunction)[0].Name);
                }
            }
        }

        private void createTriggerModeSettingUI()
        {
            createControlUI("gb_triggerSetting", DAQFunction.TriggerMode);
            createControlUI("gb_triggerSetting", DAQFunction.TriggerPolarity);
            createControlUI("gb_triggerSetting", DAQFunction.TriggerLevel);
            createControlUI("gb_triggerSetting", DAQFunction.ReTriggerCtrl);
            createControlUI("gb_triggerSetting", DAQFunction.ReTriggerCount);
        }

        private void removeControlUI(string groupBoxName, string controlName)
        {
            if (this.flp_dynamic.Controls[groupBoxName].Controls[controlName] == null) { return; }

            int controlIndex = getControlIndex(groupBoxName, controlName);
            this.flp_dynamic.Controls[groupBoxName].Controls.RemoveAt(controlIndex);
            this.flp_dynamic.Controls[groupBoxName].Controls.RemoveAt(controlIndex-1);

            int groupBoxHeight = this.flp_dynamic.Controls[groupBoxName].Size.Height;
            this.flp_dynamic.Controls[groupBoxName].Size = new System.Drawing.Size(groupBoxWidth, groupBoxHeight - 30);
        }

        private int getControlIndex(string groupBoxName, string controlName)
        {
            for(int i=0;i< this.flp_dynamic.Controls[groupBoxName].Controls.Count;i++)
            {
                if(this.flp_dynamic.Controls[groupBoxName].Controls[i].Name==controlName)
                {
                    return i;
                }
            }

            return -1;
        }

        private void assignValue(string groupBoxName, string controlName)
        {
            if (this.flp_dynamic.Controls[groupBoxName].Controls[controlName] is TextBox)
            {
                TextBox textBox = (TextBox)this.flp_dynamic.Controls[groupBoxName].Controls[controlName];
                PropertyInfo pInfo = (PropertyInfo)textBox.Tag;
                var value = Convert.ChangeType(textBox.Text, pInfo.PropertyType);
                pInfo.SetValue(aiTestItem, value, null);
            }
            else if(this.flp_dynamic.Controls[groupBoxName].Controls[controlName] is ComboBox)
            {
                ComboBox comboBox = (ComboBox)this.flp_dynamic.Controls[groupBoxName].Controls[controlName];
                string selectedValue = comboBox.SelectedValue.ToString();
                PropertyInfo pInfo = (PropertyInfo)comboBox.Tag;
                Type type = ((ComboBox)this.flp_dynamic.Controls[groupBoxName].Controls[controlName]).DataSource.GetType().GetElementType();
                int value = (int)Enum.Parse(type, selectedValue);
                pInfo.SetValue(aiTestItem, value, null);
            }
        }

        private void createControlUI(string groupBoxName, DAQFunction daqFunction)
        {            
            int x = 10;
            int y = -1;

            int totalCtrlInGB = this.flp_dynamic.Controls[groupBoxName].Controls.Count;
            y = totalCtrlInGB == 0 ? 20 : (this.flp_dynamic.Controls[groupBoxName].Controls[totalCtrlInGB - 1].Location.Y + 30);

            List<PropertyInfo> propertyInfos = getPropertyInfo(daqFunction);

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                string controlName = propertyInfo.Name;

                if (this.flp_dynamic.Controls[groupBoxName].Controls[controlName] != null) { continue; }

                if(getControlIndex(groupBoxName,controlName)!=-1)
                {
                    controlName = controlName + "1";
                }
                string description = ((DescriptionAttribute)(propertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)[0])).Description ?? "";
                Label lbl_samplingRate = new Label();
                lbl_samplingRate.Location = new Point(x, y);
                lbl_samplingRate.Text = description;
                lbl_samplingRate.ForeColor = Color.RoyalBlue;
                lbl_samplingRate.Size = new Size(130, 20);
                this.flp_dynamic.Controls[groupBoxName].Controls.Add(lbl_samplingRate);

                if (propertyInfo.PropertyType.IsEnum == false)
                {
                    TextBox textBox = new TextBox();
                    textBox.Name = controlName;
                    textBox.Tag = propertyInfo;
                    textBox.Location = new Point(x + betweenLabel, y);
                    textBox.TextAlign = HorizontalAlignment.Center;
                    this.flp_dynamic.Controls[groupBoxName].Controls.Add(textBox);
                    textBox.TextChanged += textBox_TextChanged;                    
                    textBox.Text = propertyInfo.GetValue(aiTestItem, null).ToString();                                        
                }
                else if(propertyInfo.PropertyType.IsEnum==true)
                {
                    ComboBox combox = new ComboBox();
                    combox.Name = controlName;
                    combox.Location = new Point(x + betweenLabel, y);
                    combox.DataSource = Enum.GetValues(propertyInfo.PropertyType);
                    combox.Size = new System.Drawing.Size(170, 20);
                    combox.Tag = propertyInfo;
                    this.flp_dynamic.Controls[groupBoxName].Controls.Add(combox);
                    combox.SelectedValueChanged += combox_SelectedValueChanged; 
                    combox.SelectedIndex = combox.FindStringExact(propertyInfo.GetValue(aiTestItem, null).ToString());
                }
                y += 30;
                totalCtrlInGB = this.flp_dynamic.Controls[groupBoxName].Controls.Count;
                this.flp_dynamic.Controls[groupBoxName].Size = new System.Drawing.Size(groupBoxWidth, this.flp_dynamic.Controls[groupBoxName].Controls[totalCtrlInGB - 1].Location.Y + 30);
            }

            this.step = STEP.BasicSetting;
        }

        private void createGroupBoxUI(string gbName, string gbText)
        {
            GroupBox gb_trigger = new GroupBox();
            gb_trigger.Text = gbText;
            gb_trigger.Name = gbName;
            gb_trigger.Size = new Size(groupBoxWidth, 50);
            gb_trigger.ForeColor = Color.CornflowerBlue;
            this.flp_dynamic.Controls.Add(gb_trigger);
        }
        
        private void aiStep1AlongWithChannel(PropertyInfo[] propertyInfos, AITestItem aiTestItem)
        {           
            createDefaultChannelSelectionColumn();
            createDynamicColumn(propertyInfos, aiTestItem);            
        }

        private bool assignAIChannelSetting(AITestItem aiTestItem)
        {            
            bool channelSelected = false;

            foreach (DataGridViewColumn dc in dg_channelSetting.Columns)
            {
                StringBuilder sb = new StringBuilder();

                if (dc.Name == "Selected") { continue; }

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
                        object oValue  = dg_channelSetting.Rows[channel].Cells[dc.Name].Value;
                        if(oValue==null)
                        {
                            MessageBox.Show(String.Format("CH{0} {1} not selected",channel,dc.Name));
                            return false;
                        }
                        ushort value = (ushort)(int)System.Enum.Parse(dc.ValueType, dg_channelSetting.Rows[channel].Cells[dc.Name].Value.ToString());
                        sb.Append(value + ",");
                    }
                }

                string pName = dc.Name;
                PropertyInfo pInfo = aiTestItem.GetType().GetProperty(pName);
                ushort[] values = null;

                if (pName == "Channels")
                {
                    if (sb.ToString() == string.Empty)
                    {
                        MessageBox.Show("No channel selected");
                        return false;
                    }
                    else
                    {
                        aiTestItem.ChannelNumbers = (ushort)(sb.ToString().Split(',').Length - 1);
                    }
                }

                if (channelSelected == false)
                {
                    pInfo.SetValue(aiTestItem, values, null);
                    //continue;
                }
                else if (channelSelected == true)
                {
                    sb.Remove(sb.Length - 1, 1);
                    values = Array.ConvertAll(sb.ToString().Split(','), new Converter<string, ushort>(ushort.Parse));

                    if (pInfo.PropertyType.IsArray == true && pInfo.PropertyType.GetElementType().IsEnum == true)
                    {
                        int[] enumValues = Array.ConvertAll(sb.ToString().Split(','), new Converter<string, int>(int.Parse));
                        pInfo.SetValue(aiTestItem,enumValues, null);
                    }
                    else
                    {
                        pInfo.SetValue(aiTestItem, values, null);
                    }
                }                
            }

            return true;
        }

        private void aiPollingStart()
        {
            short err = -1;
            err = MVAUAPI.LookupApi(aiTestItem).AiPolling();
            if (err != 0)
            {
                MessageBox.Show(String.Format("Error!!! Error Code={0}", err));
            }
        }

        private void aiAcquireStart()
        {
            short err = -1;
            err = MVAUAPI.LookupApi(aiTestItem).AiAcquire();
            if(err!=0)
            {
                MessageBox.Show(String.Format("Error!!! Error Code={0}", err));
            }
        }

        void PollingDataReceived(object sender, AITestItem e)
        {            
            for (int channel = 0; channel < aiTestItem.ChannelNumbers; channel++)
            {
                ch_chart.Series[aiTestItem.ToChString(channel)].Points.AddY(e.ScaledDatas[channel][0]);
            }
        }

        void API_AiDataReceived(object sender, AITestItem e)
        {
            ch_chart.Series.Clear();
            for (int channel = 0; channel < aiTestItem.ChannelNumbers; channel++)
            {
                string seriesName = aiTestItem.ToChString(channel);
                plotGraph(aiTestItem, seriesName, e.Datas, e.ScaledDatas, channel);
            }
        }

        private void plotGraph(AITestItem testItem, string seriesName, double[][] Datas, double[][] ScaledDatas, int channel)
        {
            ch_chart.Series.Add(seriesName);
            ch_chart.Series[seriesName].ChartType = SeriesChartType.Line;
            ch_chart.Series[seriesName].Points.DataBindY(ScaledDatas[channel]);
        }        

        private bool isEndAttribute(string propertyMember)
        {
            List<PropertyInfo> property = getPropertyInfo(DAQFunction.TriggerSource);
            MemberInfo[] members = property[0].PropertyType.GetMember(propertyMember.ToString());
            DAQFuncAttribute[] attributes;
            foreach (MemberInfo member in members)
            {
                attributes = (DAQFuncAttribute[])member.GetCustomAttributes(typeof(DAQFuncAttribute), false);
                foreach (DAQFuncAttribute attribute in attributes)
                {
                    if (attribute.DAQFunction == DAQFunction.End)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        private List<PropertyInfo> getPropertyInfo(DAQFunction daqFunction)
        {
            List<PropertyInfo> listPropertyInfos = new List<PropertyInfo>();
            foreach (PropertyInfo property in aiPropertyInfos)
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(property))
                {
                    if (attr.GetType() == typeof(DAQFuncAttribute) && ((DAQFuncAttribute)attr).DAQFunction == daqFunction)
                    {
                        listPropertyInfos.Add(property);
                    }
                }
            }

            return listPropertyInfos;
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            this.btn_previous.Enabled = true;
            
            if (step == STEP.AlongWithChannel)
            {
                if (assignAIChannelSetting(this.aiTestItem) == true)
                {
                    showGroupBoxExistedInFlp();
                    if (groupBoxExistedInFlp() == false)    //if setting does not existed...
                    {
                        createAcquireModeUI();
                    }

                    step = STEP.AcquireMode;
                    this.btn_previous.Enabled = true;

                    //final step, no more next.
                    if(finalStep()==true)
                    {
                        this.btn_next.Enabled = false;
                    }
                }
            }            
            else if (step == STEP.BasicSetting)
            {
                createTriggerSettingUI();
                step = STEP.TriggerSetting;
            }
            else if (step == STEP.TriggerSetting)
            {

            }
        }

        private bool finalStep()
        {
            foreach(Control control in flp_dynamic.Controls)
            {
                if(control.Name=="gb_triggerSetting")
                {
                    return true;
                }
            }

            return false;
        }

        private void showGroupBoxExistedInFlp()
        {            
            foreach (Control control in flp_dynamic.Controls)
            {
                if (control is GroupBox)
                {
                    control.Visible = true;
                }
                else if(control is DataGridView)
                {
                    control.Visible = false;
                }
            }
        }

        private bool groupBoxExistedInFlp()
        {
            foreach(Control control in flp_dynamic.Controls)
            {
                if(control is GroupBox)
                {
                    return true;
                }
            }

            return false;
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            this.flp_dynamic.Controls["dg_channelSetting"].Visible = true;

            foreach(Control control in flp_dynamic.Controls)
            {
                if(control is GroupBox)
                {
                    control.Visible = false;
                }
            }

            step = STEP.AlongWithChannel;
            this.btn_previous.Enabled = false;
            this.btn_next.Enabled = true;
        }

        private void dg_channelSetting_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (this.aiTestItem.AcquireMode != AIAcquireModeConfig.Polling)
            {
                aiThread = new Thread(aiAcquireStart);
                aiThread.Start();
            }
            else
            {
                createPollingChartUI();
                pollingTimer.Tick += pollingTimer_Tick;
                pollingTimer.Interval = aiTestItem.PollingInterval;
                pollingTimer.Start();
                aiPollingStart();          
            }            
        }

        void pollingTimer_Tick(object sender, EventArgs e)
        {
            aiPollingStart();
        }

        private void createPollingChartUI()
        {
            ch_chart.Series.Clear();            
            for(int channel=0; channel< aiTestItem.Channels.Length;channel++)
            {
                string seriesName = aiTestItem.ToChString(channel);
                ch_chart.Series.Add(seriesName);
                ch_chart.Series[seriesName].ChartType = SeriesChartType.Line;
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            short err = -1;

            if (aiTestItem.AcquireMode != AIAcquireModeConfig.Polling)
            {
                err = MVAUAPI.LookupApi(this).AiStop();
                if (err != 0)
                {
                    MessageBox.Show(String.Format("Error!!! Error Code={0}", err));
                }
            }
            else
            {
                this.pollingTimer.Stop();
            }
        }
    }
}
