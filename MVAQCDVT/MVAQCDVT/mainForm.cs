using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting.Utilities;
using System.Reflection;
using System.IO;

using MVAFW;
using MVAFW.Config;
using MVAFW.DB;
using MVAFW.Common.Entity;
using MVAFW.Common.Output;
using MVAFW.Analysis;
using MVAFW.API;
using MVAFW.TestItemColls;
using MVAFW.SettingForm;


namespace MVAQCDVT
{
    public partial class mainForm : Form
    {
        int count;
        private Error err;

        Thread tStartTest;
        List<Thread> testThreadColls = new List<Thread>();
        private delegate void delUpdateResult(TestItem testItem, uint sIndex);
        private delegate void delUpdateTestItemResult(TestResult testResult, int rowIndex);
        private delegate void delPlotGraph(AITestItem testItem);
        private delegate void delIncreasePB();
        private delegate void delCloseMain();
        private delegate void delStartPlotTimer();
        private delegate void delStopPlotTimer();
        private delegate void delStartBtnEnable(bool enable);
        private delUpdateResult dUpdateResult;
        private delUpdateTestItemResult dUpdateTestItemResult;
        private delPlotGraph dPlotGraph;
        private delIncreasePB dIncreasePB;
        private delCloseMain dCloseMain;
        private delStartPlotTimer dStartPlotTimer;
        private delStopPlotTimer dStopPlotTimer;
        private delStartBtnEnable dStartBtnEnable;
        int selectedTestItemIndex = -1;
        int selectedDgTestItemIndex = -1;
        uint cardNumber = 99;
        int profileIndex = -1;
        int globalCycle = -1;
        bool autoStart = false;
        string[] parameters;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer plotTimer = new System.Windows.Forms.Timer();
        public ManualResetEvent delayDone = new ManualResetEvent(false);

        private ProcessState State;

        List<eTestItemSetting> testItemSettingColl = new List<eTestItemSetting>();

        public mainForm(bool autoStart, params string[] parameters)
        {
            InitializeComponent();

            this.autoStart = autoStart;
            this.parameters = parameters;
            MVAUAPI.InitialApiMapping();

            this.dg_testResult.CellPainting += new DataGridViewCellPaintingEventHandler(dg_testResult_CellPainting);
            this.dg_testItem.CellValueChanged += new DataGridViewCellEventHandler(dg_testItem_CellValueChanged);
            this.dg_testItem.CurrentCellDirtyStateChanged += new EventHandler(dg_testItem_CurrentCellDirtyStateChanged);
            this.dg_testItem.CellClick += new DataGridViewCellEventHandler(dg_testItem_CellClick);
            this.dg_testItem.CellDoubleClick += new DataGridViewCellEventHandler(dg_testItem_CellDoubleClick);
            this.dg_testResult.CellDoubleClick += new DataGridViewCellEventHandler(dg_testResult_CellDoubleClick);
            this.dg_testItem.SelectionChanged += new EventHandler(dg_testItem_SelectionChanged);
            this.property_testItem.PropertyValueChanged += new PropertyValueChangedEventHandler(property_testItem_PropertyValueChanged);
            this.txt_globalCycle.TextChanged += new EventHandler(txt_globalCycle_TextChanged);
            this.txt_globalCycleDelay.TextChanged += txt_globalCycleDelay_TextChanged;
            this.txt_sn.TextChanged += new EventHandler(txt_sn_TextChanged);
            this.txt_pn.TextChanged += new EventHandler(txt_pn_TextChanged);
            this.cb_slot.SelectedIndexChanged += cb_slot_SelectedIndexChanged;
            this.timer.Tick += new EventHandler(timer_Tick);
            this.plotTimer.Tick += new EventHandler(plotTimer_Tick);
            this.FormClosing += mainForm_FormClosing;
            this.dg_testItem.EditMode = DataGridViewEditMode.EditOnEnter;
            //MvaAPIManager.PCIDaskAPI.AiDataReceived+=new MVADASKAPI.AiDataReceivedEventHandler(AiDataReceived);
            //MvaAPIManager.PCIDaskAPI9112.AiDataReceived += new MVADASKAPI.AiDataReceivedEventHandler(AiDataReceived);
            //MvaAPIManager.PCIDaskAPI9221.AiDataReceived += new MVADASKAPI.AiDataReceivedEventHandler(AiDataReceived);
            //MvaAPIManager.USBDaskAPI.AiDataReceived += new MVADASKAPI.AiDataReceivedEventHandler(AiDataReceived);
            //MvaAPIManager.DSADaskAPI9527.AiDataReceived += new MVADASKAPI.AiDataReceivedEventHandler(AiDataReceived);
            //MvaAPIManager.USBDaskAPI1210.AiDataReceived+=new MVADASKAPI.AiDataReceivedEventHandler(AiDataReceived);

            dUpdateResult = new delUpdateResult(updateResult);
            dUpdateTestItemResult = new delUpdateTestItemResult(updateTestItemResult);
            dPlotGraph = new delPlotGraph(plotGraph);
            dIncreasePB = new delIncreasePB(IncreasePB);
            dCloseMain = new delCloseMain(closeMain);
            dStartPlotTimer = new delStartPlotTimer(startPlotTimer);
            dStopPlotTimer = new delStopPlotTimer(stopPlotTimer);
            dStartBtnEnable = new delStartBtnEnable(startBtnEnable);
        }

        void cb_slot_SelectedIndexChanged(object sender, EventArgs e)
        {
            eProductSetting.SlotID = int.Parse(this.cb_slot.SelectedItem.ToString());
        }

        private void startBtnEnable(bool enable)
        {
            this.btn_start.Enabled = enable;
        }

        void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tStartTest != null)
            {
                tStartTest.Abort();
            }

            if (eProductSetting.Result == "PASS")
            {
                Console.WriteLine(3);
                Environment.Exit(3);
            }
            else if (eProductSetting.Result == "FAIL")
            {
                Console.WriteLine(4);
                Environment.Exit(4);
            }
        }

        void txt_globalCycleDelay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double globalCycleDelay = double.Parse(this.txt_globalCycleDelay.Text.ToString());
                oProductSetting.UpdateGlobalCycleDelay(globalCycleDelay);
                eProductSetting.GlobalCycleDelay = globalCycleDelay;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        void dg_testItem_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void dg_testItem_DragDrop(object sender, DragEventArgs e)
        {
            if (((TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode")).Parent == null)
            {
                MessageBox.Show("Please drag test item");
                return;
            }

            string name = ((TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode")).Text + ":" + ((TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode")).Parent.Text;
            addtoTIGridView(name);
        }

        void startPlotTimer()
        {
            plotTimer.Interval = 100;
            plotTimer.Start();
        }

        void stopPlotTimer()
        {
            plotTimer.Stop();
            plotTimer_Tick(this, new EventArgs());
        }

        void plotTimer_Tick(object sender, EventArgs e)
        {
            if (eMVACollection.AiTestData.testItem == null)
            {
                return;
            }

            lock (eMVACollection.AiTestData)
            {
                plotGraph(eMVACollection.AiTestData);
            }
        }

        //void AiDataReceived(object sender, AITestItem e)
        //{
        //    plotGraph(e);
        //}

        private void closeMain()
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (eProductSetting.Lock == true)
            {
                switch (keyData)
                {
                    case Keys.Return:
                        this.btn_start.PerformClick();
                        break;
                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            return true;
        }

        private void dg_testResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { return; }

            ch_chart.Series.Clear();
            ch_histogram.Series.Clear();
            lbl_average.Text = "NA";
            lbl_max.Text = "NA";
            lbl_min.Text = "NA";
            lbl_nofc.Text = "NA";
            lbl_p2p.Text = "NA";
            lbl_rms.Text = "NA";
            lbl_std.Text = "NA";

            int testItemIndex = int.Parse(dg_testResult.Rows[e.RowIndex].Cells["index"].Value.ToString());
            TestItem testItem = oTestItemManage.GetTestItem(testItemIndex, cardNumber);
            int channel = int.Parse(dg_testResult.Rows[e.RowIndex].Cells["channel"].Value.ToString());
            int channelIndex = getChannelIndex(channel, testItem.Channels);


            if (!(testItem is AITestItem))
            {
                return;
            }

            double[] data = ((double[][])testItem.GetType().GetProperty("Datas").GetValue(testItem, null))[channelIndex];
            double[] scaledData = ((double[][])testItem.GetType().GetProperty("ScaledDatas").GetValue(testItem, null))[channelIndex];
            double[] frequency = ((double[][])testItem.GetType().GetProperty("Frequency").GetValue(testItem, null))[channelIndex];
            double[] spectrums = ((double[][])testItem.GetType().GetProperty("Spectrums").GetValue(testItem, null))[channelIndex];

            string seriesName = "CH" + channel.ToString();
            plotGraph((AITestItem)testItem, seriesName, channelIndex);
        }

        private int getChannelIndex(int channel, ushort[] channels)
        {
            for (int i = 0; i < channels.Length; i++)
            {
                if (channels[i] == channel)
                {
                    return i;
                }
            }

            return -1;
        }

        private void IncreasePB()
        {
            this.pb_bar.Value++;
        }

        void dg_testItem_SelectionChanged(object sender, EventArgs e)
        {
            if (dg_testItem.SelectedRows.Count == 0) return;

            updatePropertyGrid(dg_testItem.SelectedRows[0].Index);
        }

        void txt_pn_TextChanged(object sender, EventArgs e)
        {
            oProductSetting.UpdatePartNumber(this.txt_pn.Text);
            oProductSetting.GetProductSetting();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            turnOff(eProductSetting.TurnOff);
        }

        private void turnOff(TurnOffType turnOffType)
        {
            if (turnOffType == TurnOffType.AutoShoutDown)
            {
                Process.Start("shutdown", "/s /t 0");	// starts the shutdown application 
                // the argument /s is to shut down the computer
                // the argument /t 0 is to tell the process that the specified operation needs to be completed after 0 seconds
            }
            else if (turnOffType == TurnOffType.AutoRestart)
            {
                Process.Start("shutdown", "/r /t 0");  // the argument /r is to restart the computer
            }
        }

        void txt_sn_TextChanged(object sender, EventArgs e)
        {
            eProductSetting.SN = this.txt_sn.Text;
        }

        void txt_globalCycle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                globalCycle = int.Parse(this.txt_globalCycle.Text.ToString());
                oProductSetting.UpdateGlobalCycle(globalCycle);
                eProductSetting.GlobalCycle = globalCycle;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        void cb_profile_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.profileIndex = int.Parse(((DataRowView)this.cb_profile.SelectedItem).Row["profileIndex"].ToString());
            eProductSetting.ProfileIndex = this.profileIndex;
            eProductSetting.ProfileName = ((DataRowView)this.cb_profile.SelectedItem).Row["profileName"].ToString();
            oProductSetting.UpdateProfile(eProductSetting.ProfileIndex);

            //display existed testitem
            displayTestItem();
        }

        void InitialCardNumber()
        {
            this.cardNumber = 0;
            eProductSetting.CardNumber = this.cardNumber;
            oProductSetting.UpdateCardNumber(eProductSetting.CardNumber);
            //display existed testitem
            displayTestItem();
        }

        void dg_testItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex != dg_testItem.Columns["name"].Index) { return; }

            if (this.pb_bar.Value == this.pb_bar.Maximum)     //means the previous test is finished. So re-start progress bar value.
            {
                this.pb_bar.Maximum = 1;
                this.pb_bar.Value = 0;
            }
            else if (this.pb_bar.Value < this.pb_bar.Maximum) //means still under testing, so increasing the Progress bar maximum value
            {
                this.pb_bar.Maximum += 1;
            }

            State = ProcessState.Start;
            selectedDgTestItemIndex = e.RowIndex;

            oTestItemManage.StartTestLog(this.cardNumber);
            tStartTest = new Thread(new ThreadStart(this.singleTest));
            tStartTest.Start();
        }

        private void singleTest()
        {
            //try
            //{
            TestItem testItem = getTestItem(selectedDgTestItemIndex);
            test(selectedDgTestItemIndex, testItem);
            //oTestItemManage.ResultMessage(this.cardNumber, selectedTestItemIndex);
            oTestItemManage.ResultMessageFromDB(this.cardNumber);
            oTestItemManage.ExportLogFileToPE(this.cardNumber, false);
            oTestItemManage.ExportXMLFile(this.cardNumber);
            //oTestItemManage.ExportLogFileToWebDisk(this.cardNumber);
            testItem.Cycle = 0;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void property_testItem_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            err = oTestItemSetting.UpdateTestItemProperty((TestItem)property_testItem.SelectedObject, e, cardNumber);
            if (err != Error.NoError)
            {
                MessageBox.Show("Update Property Fail\n");
            }

            property_testItem.Refresh();
        }

        void dg_testItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updatePropertyGrid(e.RowIndex);
        }

        private void updatePropertyGrid(int rowIndex)
        {
            if (rowIndex == -1) { return; }
            string name = dg_testItem.Rows[rowIndex].Cells[(int)dgTestItemColumns.name].Value.ToString();
            selectedDgTestItemIndex = rowIndex;
            selectedTestItemIndex = int.Parse(dg_testItem.Rows[rowIndex].Cells[(int)dgTestItemColumns.index].Value.ToString());
            property_testItem.SelectedObject = oTestItemManage.GetTestItem(selectedTestItemIndex, cardNumber);
            property_testItem.CategoryForeColor = Color.Green;
        }

        void dg_testItem_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dg_testItem.IsCurrentCellDirty)
            {
                dg_testItem.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        void dg_testItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string value;
            string columnName = dg_testItem.Columns[e.ColumnIndex].Name;

            try
            {
                if (columnName == "enable" || columnName == "Retest" || columnName == "stopWhenFail")
                {
                    value = Convert.ToInt32(bool.Parse(dg_testItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())).ToString();
                }
                else
                {
                    value = dg_testItem.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Input is not valid number format!");
                return;
            }

            if (dg_testItem.Columns[e.ColumnIndex].ReadOnly == false)
            {
                int tindex = int.Parse(dg_testItem.Rows[e.RowIndex].Cells[(int)dgTestItemColumns.index].Value.ToString());
                err = oTestItemSetting.UpdateTestItem(columnName, value, tindex, this.cardNumber);

                if (err != Error.NoError)
                {
                    MessageBox.Show("Updae Test item setting fail!");
                }
                //else
                //{
                //    if (columnName == "seqIndex")
                //    {
                //        DisplayTestItem();
                //    }
                //}
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            //test item grid color reset to default and count testItem for progress bar
            pb_bar.Maximum = 0;
            for (int i = 0; i < dg_testItem.Rows.Count; i++)
            {
                dg_testItem.Rows[i].DefaultCellStyle.BackColor = Color.White;
                bool enable = bool.Parse(dg_testItem.Rows[i].Cells["enable"].Value.ToString());
                if (enable == true)
                {
                    pb_bar.Maximum++;
                }
            }

            pb_bar.Maximum *= int.Parse(txt_globalCycle.Text.ToString());

            //start test
            State = ProcessState.Start;
            dg_testResult.Rows.Clear();
            dg_testItem.ClearSelection();
            //-1 means normal test, shoule be refactoring in the future.
            if (eProductSetting.RestartIndex == -1)
            {
                tStartTest = new Thread(new ThreadStart(this.startTest));
            }
            else
            {
                tStartTest = new Thread(new ThreadStart(this.startTestAfterRestart));
            }

            tStartTest.Start();
        }

        //might be combined into the original startTest()
        private void startTestAfterRestart()
        {
            try
            {
                Dictionary<int, TestResult> dicTestResult = oTestItemResult.GetTestItemResult(cardNumber);


                int startIndex = 0;
                for (startIndex = 0; startIndex < dg_testItem.Rows.Count; startIndex++)
                {
                    int testItemIndex = int.Parse(dg_testItem.Rows[startIndex].Cells["tIndex"].Value.ToString());
                    bool enable = bool.Parse(dg_testItem.Rows[startIndex].Cells["enable"].Value.ToString());

                    if (enable == true)
                    {
                        TestResult testResult = dicTestResult[testItemIndex];
                        IAsyncResult async = this.BeginInvoke(dUpdateTestItemResult, testResult, startIndex);
                        this.EndInvoke(async);

                        updatePB();

                        if (testItemIndex == eProductSetting.RestartIndex)
                        {
                            startIndex++;
                            break;
                        }
                    }
                }

                for (int index = startIndex; index < dg_testItem.Rows.Count; index++)
                {
                    bool swf = bool.Parse(dg_testItem.Rows[index].Cells["stopWhenFail"].Value.ToString());
                    bool enable = bool.Parse(dg_testItem.Rows[index].Cells["enable"].Value.ToString());

                    if (enable == true)
                    {
                        TestItem testItem = getTestItem(index);
                        TestResult testResult = test(index, testItem);
                        if (swf == true && testResult == TestResult.FAIL)
                        {
                            break;
                        }
                        else if (testItem.Restart == true)
                        {
                            Scheduler.rkApp.SetValue("MVAQCDVT", Application.ExecutablePath.ToString() + " start");
                            oProductSetting.UpdateRestartIndex(testItem.Index);
                            turnOff(testItem.TurnOfftype);
                        }
                    }
                }

                //oTestItemManage.ResultMessage(this.cardNumber);

                oTestItemManage.ResultMessageFromDB(this.cardNumber);
                oTestItemManage.ExportLogFileToPE(this.cardNumber, true);
                oTestItemManage.ExportXMLFile(this.cardNumber);
                //oTestItemManage.ExportLogFileToWebDisk(this.cardNumber);
                /*
                oProductSetting.UpdateRestartIndex(-1);
                oProductSetting.GetProductSetting();
                if (eProductSetting.TurnOff == TurnOffType.None)
                {
                    Scheduler.rkApp.DeleteValue("MVAQCDVT", false);
                }
                */

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                oProductSetting.UpdateRestartIndex(-1);
                oProductSetting.GetProductSetting();
                if (eProductSetting.TurnOff == TurnOffType.None)
                {
                    Scheduler.rkApp.DeleteValue("MVAQCDVT", false);
                }
            }
        }

        private TestItem getTestItem(int index)
        {
            return oTestItemManage.GetTestItem(int.Parse(dg_testItem.Rows[index].Cells["tIndex"].Value.ToString()), cardNumber);
        }

        private void updatePB()
        {
            IAsyncResult async = this.BeginInvoke(dIncreasePB);
            this.EndInvoke(async);
        }

        private bool initialTestCheck()
        {
            if (dg_testItem.Rows.Count == 0)
            {
                MessageBox.Show("No Test Item!!");
                return false;
            }

            bool validTest = false;
            for (int index = 0; index < dg_testItem.Rows.Count; index++)
            {
                bool enable = bool.Parse(dg_testItem.Rows[index].Cells["enable"].Value.ToString());
                if (enable == true)
                {
                    validTest = true;
                }
            }

            if (validTest == false)
            {
                MessageBox.Show("No enabled Test Item!!");
                return false;
            }

            return true;
        }

        private TestResult testThread(int index, TestItem testItem)
        {
            return test(index, testItem);
        }

        private bool isSNValid()
        {
            if (eProductSetting.Lock == true && this.txt_sn.Text.Length != 10)
            {
                return false;
            }

            return true;
        }

        private void startTest()
        {
            this.BeginInvoke(dStartBtnEnable, false);
            DateTime cycleStarTime = DateTime.Now;
            //might be implemented at oTestItemManage in the future                            
            try
            {
                if (isSNValid() == false) { throw new Exception("SN length should be 10 digit!!!"); }

                if (initialTestCheck() == false)
                {
                    return;
                }

                oTestItemManage.ResetTestItemCycle(this.cardNumber);
                oTestItemManage.StartTestLog(this.cardNumber);

                for (int cycle = 0; cycle < eProductSetting.GlobalCycle; cycle++)
                {
                    if (State == ProcessState.Stop) { break; }

                    cycleStarTime = DateTime.Now;

                    for (int index = 0; index < dg_testItem.Rows.Count; index++)
                    {
                        bool swf = bool.Parse(dg_testItem.Rows[index].Cells["stopWhenFail"].Value.ToString());
                        bool enable = bool.Parse(dg_testItem.Rows[index].Cells["enable"].Value.ToString());

                        if (enable == true)
                        {
                            TestItem testItem = getTestItem(index);
                            this.dg_testItem.Rows[index].DefaultCellStyle.BackColor = SystemColors.Highlight;
                            // xCheckIfScroll();

                            testItem.TestItemEvent += testItem_TestItemEvent;

                            if (testItem.IndependentThread == false)
                            {
                                TestResult testResult = test(index, testItem);
                                if (swf == true && testResult == TestResult.FAIL)
                                {
                                    goto end2;
                                    break;
                                }
                                else if (testItem.Restart == true)
                                {
                                    Scheduler.rkApp.SetValue("MVAQCDVT", Application.ExecutablePath.ToString() + " start");
                                    oProductSetting.UpdateRestartIndex(testItem.Index);
                                    turnOff(testItem.TurnOfftype);
                                    goto end;
                                }
                            }
                            else if (testItem.IndependentThread == true)
                            {
                                int indexCopy = index;
                                Thread testT = new Thread(() => testThread(indexCopy, testItem));
                                testT.Start();

                                testThreadColls.Add(testT);
                            }
                            testItem.TestItemEvent -= testItem_TestItemEvent;

                            if (this.dg_testItem.Rows[index].DefaultCellStyle.BackColor == SystemColors.Highlight)
                                this.dg_testItem.Rows[index].DefaultCellStyle.BackColor = Color.White;
                        }
                    }

                    //waiting all thread end
                    foreach (Thread testT in testThreadColls)
                    {
                        testT.Join();
                    }

                    if (eProductSetting.GlobalCycleDelay != 0 && cycle < eProductSetting.GlobalCycle) { delay(eProductSetting.GlobalCycleDelay, cycleStarTime); }
                }
            //oTestItemManage.ResultMessage(this.cardNumber);
            end2: oTestItemManage.ResultMessageFromDB(this.cardNumber);
            end: oTestItemManage.ExportLogFileToPE(this.cardNumber, true);
                oTestItemManage.ExportXMLFile(this.cardNumber);
                //oTestItemManage.ExportLogFileToWebDisk(this.cardNumber);

                if (eProductSetting.AutoClose == true)
                {
                    this.BeginInvoke(dCloseMain);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                State = ProcessState.Stop;
                this.BeginInvoke(dStartBtnEnable, true);
            }
        }

        private int getSeqIndex(int testItemIndex)
        {
            foreach (DataGridViewRow row in dg_testItem.Rows)
            {
                if (row.Cells["tIndex"].Value.ToString() == testItemIndex.ToString())
                {
                    return int.Parse(row.Cells["seqIndex"].Value.ToString());
                }
            }

            return -1;
        }

        private void testItem_TestItemEvent(object sender, TestItem e)
        {
            int seqIndex = getSeqIndex(e.Index);
            if (e.IndependentThread == false)
            {
                test(seqIndex, e);
            }
            else
            {
                Thread testT = new Thread(() => testThread(seqIndex, e));
                testT.Start();
            }
        }

        private TestResult test(int index, TestItem testItem)
        {
            uint testCycle = 0;
            uint retestCount = 0;

            bool retest = bool.Parse(dg_testItem.Rows[index].Cells["Retest"].Value.ToString());
            uint totalCycle = uint.Parse(dg_testItem.Rows[index].Cells["Cycle"].Value.ToString());
            uint sIndex = uint.Parse(dg_testItem.Rows[index].Cells["seqIndex"].Value.ToString());
            uint retestNUpperLimit = uint.Parse(dg_testItem.Rows[index].Cells["retestNUpperLimit"].Value.ToString());
            string until = dg_testItem.Rows[index].Cells["until"].Value.ToString();
            double cycleDelaySecond = double.Parse(dg_testItem.Rows[index].Cells["cycleDelay"].Value.ToString());
            DateTime cycleStarTime = DateTime.Now;

            Console.WriteLine(string.Format("Index:{0}, cycle:{1},delay:{2}", index, totalCycle, cycleDelaySecond));
            //initial the array again if is reset by others, might be enhanced in the future for this comparision.
            if (testItem is AITestItem && ((AITestItem)testItem).ReTriggerCount == 0 && (((AITestItem)testItem).Datas == null || ((AITestItem)testItem).DataCount != ((AITestItem)testItem).Datas[0].Length))
            {
                testItem.ChannelNumbers = testItem.ChannelNumbers;
                ((AITestItem)testItem).DataCount = ((AITestItem)testItem).DataCount;
            }

        test:
            cycleStarTime = DateTime.Now;
            if (State == ProcessState.Stop)
            {
                return TestResult.UNKNOWN;
            }

            if (testItem is AITestItem && ((AITestItem)testItem).PlotData == true)
            {
                this.BeginInvoke(dStartPlotTimer);
            }

            testItem.CycleSet = totalCycle;
            testItem.doTest();     //start test
            testItem.AddDescription();
            testItem.getResult(false);
            IAsyncResult async = this.BeginInvoke(dUpdateResult, testItem, sIndex);
            this.EndInvoke(async);

            if (testItem is AITestItem && ((AITestItem)testItem).PlotData == true)
            {
                this.BeginInvoke(dStopPlotTimer);
            }

            if (retest == true && testItem.FinalResult == TestResult.FAIL && retestCount < retestNUpperLimit)
            {
                retestCount++;
                goto test;
            }

            oTestItemManage.AddResultDetail(testItem, cardNumber);

            testCycle++;

            delay(cycleDelaySecond, cycleStarTime);
            if (testCycle < totalCycle && (until.Equals("None") || (until.Equals("PASS") && testItem.FinalResult == TestResult.FAIL) || (until.Equals("FAIL") && testItem.FinalResult == TestResult.PASS)))
            {
                goto test;
            }

            if (until.Equals("PASS") && testItem.FinalResult == TestResult.FAIL)
            {
                goto test;
            }
            else if (until.Equals("FAIL") && testItem.FinalResult == TestResult.PASS)
            {
                goto test;
            }

            updatePB();
            async = this.BeginInvoke(dUpdateTestItemResult, testItem.FinalResult, index);
            this.EndInvoke(async);

            return testItem.FinalResult;
        }

        private void delay(double cycleDelaySecond, DateTime testStartTime)
        {
            if (cycleDelaySecond == 0)
            {
                return;
            }

            int delayMiniSeconds = (int)(cycleDelaySecond * 1000) - (int)(DateTime.Now - testStartTime).TotalMilliseconds;
            if (delayMiniSeconds < 0)
            {
                delayMiniSeconds = 0;
            }

            delayDone.WaitOne(delayMiniSeconds);
            Console.WriteLine(delayMiniSeconds);
        }

        private void plotGraph(AITestItem testItem, string seriesName, double[][] Datas, double[][] ScaledDatas, int channel)
        {
            ch_chart.Series.Add(seriesName);
            ch_chart.Series[seriesName].ChartType = SeriesChartType.Line;

            if (rb_rawdata.Checked == true)
            {
                ch_chart.Series[seriesName].Points.DataBindY(Datas[channel]);
            }
            else if (rb_scaledData.Checked == true)
            {
                ch_chart.Series[seriesName].Points.DataBindY(ScaledDatas[channel]);
            }
            else if (rb_fft.Checked == true)
            {

                analysis.GetDynamicPerformance(testItem, ScaledDatas[channel], false, channel);

                if (chk_fftAbsolute.Checked == false)
                {
                    ch_chart.Series[seriesName].Points.DataBindXY(testItem.Frequency[channel], testItem.Spectrums[channel]);
                }
                else if (chk_fftAbsolute.Checked == true)
                {
                    ch_chart.Series[seriesName].Points.DataBindXY(testItem.Frequency[channel], testItem.SpectrumsAbsolute[channel]);
                }

                if (property_testItem.GetContainerControl().ActiveControl != null)
                {
                    if (selectedTestItemIndex != -1 && property_testItem.GetContainerControl().ActiveControl.GetType().Name != "GridViewEdit")
                    {
                        property_testItem.SelectedObject = oTestItemManage.GetTestItem(selectedTestItemIndex, cardNumber);
                    }
                }
            }

            if (chk_meas.Checked == true)
            {
                double average, max, min, rms, std, p2p;
                int nofc;

                analysis.getDCAnalysis(Datas[channel], ScaledDatas[channel], out average, out min, out max, out rms, out std, out p2p, out nofc);

                this.lbl_average.Text = average.ToString("F4");
                this.lbl_max.Text = max.ToString("F4");
                this.lbl_min.Text = min.ToString("F4");
                this.lbl_rms.Text = rms.ToString("F4");
                this.lbl_std.Text = std.ToString("F4");
                this.lbl_nofc.Text = nofc.ToString();
                this.lbl_p2p.Text = p2p.ToString("F4");


                try
                {
                    ch_histogram.Series.Clear();
                    ch_histogram.Series.Add("histoData");

                    double[] histoDatas = new double[testItem.DataCount];
                    for (int i = 0; i < histoDatas.Length; i++)
                    {
                        histoDatas[i] = (double)(short)Datas[channel][i];
                    }
                    ch_histogram.Series["histoData"].Points.DataBindY(histoDatas);

                    HistogramChartHelper Hist_Chart = new HistogramChartHelper();
                    if (nofc < 20)
                    {
                        Hist_Chart.SegmentIntervalWidth = 1;
                    }
                    else
                    {
                        Hist_Chart.SegmentIntervalWidth = double.NaN;
                    }
                    Hist_Chart.CreateHistogram(ch_histogram, "histoData", "histoGram");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void plotGraph(AITestItem testItem, string seriesName, int channel)
        {
            ch_chart.Series.Add(seriesName);
            ch_chart.Series[seriesName].ChartType = SeriesChartType.Line;

            if (rb_rawdata.Checked == true)
            {
                ch_chart.Series[seriesName].Points.DataBindY(testItem.Datas[channel]);
            }
            else if (rb_scaledData.Checked == true)
            {
                ch_chart.Series[seriesName].Points.DataBindY(testItem.ScaledDatas[channel]);
            }
            else if (rb_fft.Checked == true)
            {
                analysis.GetDynamicPerformance(testItem, testItem.ScaledDatas[channel], false, channel);

                if (chk_fftAbsolute.Checked == false)
                {
                    ch_chart.Series[seriesName].Points.DataBindXY(testItem.Frequency[channel], testItem.Spectrums[channel]);
                }
                else if (chk_fftAbsolute.Checked == true)
                {
                    ch_chart.Series[seriesName].Points.DataBindXY(testItem.Frequency[channel], testItem.SpectrumsAbsolute[channel]);
                }

                if (property_testItem.GetContainerControl().ActiveControl != null)
                {
                    if (selectedTestItemIndex != -1 && property_testItem.GetContainerControl().ActiveControl.GetType().Name != "GridViewEdit")
                    {
                        property_testItem.SelectedObject = oTestItemManage.GetTestItem(selectedTestItemIndex, cardNumber);
                    }
                }
            }

            double average, max, min, rms, std, p2p;
            int nofc;

            analysis.getDCAnalysis(testItem.Datas[channel], this.rb_scaledData.Checked == true ? testItem.ScaledDatas[channel] : testItem.Datas[channel], out average, out min, out max, out rms, out std, out p2p, out nofc);

            this.lbl_average.Text = average.ToString("F4");
            this.lbl_max.Text = max.ToString("F4");
            this.lbl_min.Text = min.ToString("F4");
            this.lbl_rms.Text = rms.ToString("F4");
            this.lbl_std.Text = std.ToString("F4");
            this.lbl_nofc.Text = nofc.ToString();
            this.lbl_p2p.Text = p2p.ToString("F4");

            try
            {
                ch_histogram.Series.Clear();
                ch_histogram.Series.Add("histoData");

                double[] histoDatas = new double[testItem.DataCount];
                for (int i = 0; i < histoDatas.Length; i++)
                {
                    histoDatas[i] = (double)(short)testItem.Datas[channel][i];
                }
                ch_histogram.Series["histoData"].Points.DataBindY(histoDatas);

                HistogramChartHelper Hist_Chart = new HistogramChartHelper();
                if (nofc < 20)
                {
                    Hist_Chart.SegmentIntervalWidth = 1;
                }
                else
                {
                    Hist_Chart.SegmentIntervalWidth = double.NaN;
                }
                Hist_Chart.CreateHistogram(ch_histogram, "histoData", "histoGram");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void plotGraph(TestData testData)
        {
            AITestItem testItem = testData.testItem;
            if (testItem.PlotData == true)
            {
                ch_chart.Series.Clear();
                for (int channel = 0; channel < Math.Max(testItem.ChannelNumbers, testItem.SpecificChannel + 1); channel++)
                {
                    if ((testItem.ChannelIndex == -1 && testItem.SpecificChannel == -1) || testItem.SpecificChannel == channel || testItem.ChannelIndex == channel)
                    {
                        string seriesName = testItem.ToChString(channel);
                        plotGraph(testItem, seriesName, testData.Datas, testData.ScaledDatas, channel);
                    }
                }
            }

            testData.testItem = null;
        }

        private void plotGraph(AITestItem testItem)
        {
            //if (testItem.Values[Math.Max(0, (int)testItem.SpecificChannel)] == null)
            //{
            //    return;
            //}
            if (testItem.PlotData == true)
            {
                ch_chart.Series.Clear();
                for (int channel = 0; channel < Math.Max(testItem.ChannelNumbers, testItem.SpecificChannel + 1); channel++)
                {
                    if (testItem.SpecificChannel == -1 || testItem.SpecificChannel == channel)
                    {
                        string seriesName = "CH" + channel.ToString();
                        plotGraph(testItem, seriesName, channel);
                    }
                }
            }
        }

        private void updateResult(TestItem testItem, uint sIndex)
        {
            for (int channel = 0; channel < Math.Max(testItem.ChannelNumbers, testItem.SpecificChannel + 1); channel++)
            {
                string channelDescription = channel < testItem.DescriptionByChannel.Count ? testItem.DescriptionByChannel[channel] : testItem.Description;
                if (testItem.SpecificChannel == -1 || testItem.SpecificChannel == channel)
                {
                    dg_testResult.Rows.Add(testItem.Index,
                                                            testItem.Cycle,
                                                            //testItem.Name + (testItem.CompareType != CompareType.equalVersion ?":CH" + channel.ToString():""), 
                                                            testItem.Name + ":" + testItem.ToChString(channel),
                                                            testItem.CompareType == CompareType.LessThanPercentFSR ? string.Format("{0}:{1:F3}%", channelDescription, testItem.RealPercentage[channel]) : channelDescription,
                                                            //testItem.Description,
                                                            //testItem.CompareType != CompareType.equalVersion ? testItem.Values[channel].ToString() : testItem.Version,
                                                            testItem.Values[channel] == null ? "" : testItem.Values[channel].ToString(),
                                                            testItem.Percentage[channel],
                                                            testItem.Results[channel],
                                                            testItem.CompareType,
                                                            testItem.SpecificChannel != -1 ? testItem.SpecificChannel : (short)testItem.Channels[channel]);
                    dg_testResult.FirstDisplayedScrollingRowIndex = dg_testResult.Rows.Count - 1;

                    //  Application.DoEvents();
                }
            }

            //for display the updated property

            if (property_testItem.GetContainerControl().ActiveControl == null)
            {
                return;
            }

            if (selectedTestItemIndex != -1 && property_testItem.GetContainerControl().ActiveControl.GetType().Name != "GridViewEdit")
            {
                property_testItem.SelectedObject = oTestItemManage.GetTestItem(selectedTestItemIndex, cardNumber);
                //property_testItem.Refresh();
            }
        }

        private void updateTestItemResult(TestResult testResult, int rowIndex)
        {
            if (testResult == TestResult.PASS)
            {
                this.dg_testItem.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LimeGreen;
            }
            else if (testResult == TestResult.FAIL)
            {
                this.dg_testItem.Rows[rowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
            }
        }

        void dg_testResult_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //   Avoids rows or columns out of data area
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            //   Checks if it's painting percent column
            if (e.ColumnIndex != (int)dgTestResultColumns.TestResult)
                return;

            int value = (int)double.Parse(dg_testResult.Rows[e.RowIndex].Cells["percentage"].FormattedValue.ToString());

            System.Drawing.Color oColor;
            if (value >= 50)
            {
                oColor = Color.Green;
            }
            else
            {
                oColor = Color.Red;
            }
            Bar.PintaDegradado(oColor, e, value, 50, Color.Orange);
        }

        private enum dgTestResultColumns
        {
            TestItem = 1,
            TestValue = 4,
            TestResult = 6,
            ComparisionType = 7,
        }

        private enum dgTestItemColumns
        {
            index = 2,
            name = 3
        }

        private enum ProcessState
        {
            Start,
            Stop
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            this.State = ProcessState.Stop;
        }

        private void btn_addTestItem_Click(object sender, EventArgs e)
        {
            MVAFW.SettingForm.TestItemSelection fm = new MVAFW.SettingForm.TestItemSelection();

            fm.AddedTestItems += new MVAFW.SettingForm.TestItemSelection.addtestitemHandler(addTestItemToGridView);
            fm.Show();
        }

        private void addTestItemToGridView(object sender, AddtestitemEventArg e)
        {
            string sStr = e.SelectedNode.ToString();
            addtoTIGridView(sStr);
        }

        private void addtoTIGridView(string sAddnode)
        {
            err = oTestItemManage.CreateRealTestItem(sAddnode, cardNumber);
            if (err == Error.NoData)
            {
                MessageBox.Show("Please drag test item");
                return;
            }
            else if (err != Error.NoError)
            {
                MessageBox.Show("Add test item into DB fail!");
                return;
            }

            displayTestItem();
            dg_testItem.ClearSelection();
            dg_testItem.FirstDisplayedScrollingRowIndex = dg_testItem.Rows.Count - 1;
            dg_testItem.Rows[dg_testItem.RowCount - 1].Selected = true;
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            productSetting fm = new productSetting(eProductSetting.Lock);
            fm.ShowDialog();

            addAvailableTestItemAndProfileToCB();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            //Read LogServer.ini for log sever
            StringBuilder sbPath = new StringBuilder(512), sbID = new StringBuilder(512), sbPW = new StringBuilder(512), sbDisk = new StringBuilder(512);

            eProductSetting.WebPath = sbPath.ToString();
            eProductSetting.WebID = sbID.ToString();
            eProductSetting.WebPassword = sbPW.ToString();
            eProductSetting.WebDisk = sbDisk.ToString();

            //initial setting and check
            err = MvaGlobalManager.InitProductSetting();
            if (err == Error.GeneralError || err == Error.DBError)
            {
                MessageBox.Show("Init ProductSetting Error");
                this.Close();
            }

            if (eProductSetting.ModelName == null)
            {
                MessageBox.Show("Please set up the Model Name first!");
                productSetting fm = new productSetting(eProductSetting.Lock);
                fm.ShowDialog();
            }

            if (eProductSetting.ModelName == null)
            {
                this.Close();
            }

            disableGridviewSorting(dg_testItem, dg_testItem.Columns.Count);
            this.Text = eProductSetting.ModelName + " Ver " + eProductSetting.QcProgramVersion;
            this.txt_globalCycle.Text = eProductSetting.GlobalCycle.ToString();
            this.txt_globalCycleDelay.Text = eProductSetting.GlobalCycleDelay.ToString();
            this.txt_pn.Text = eProductSetting.PartNumber;
            this.cb_slot.SelectedIndex = 0;

            addAvailableTestItemAndProfileToCB();
            setUpScheduler();
            updateSNDisplay();
            updateDataDisplayOption();
            controlPanelEnableConfig();
            this.cb_profile.SelectedIndexChanged += new EventHandler(cb_profile_SelectedIndexChanged);
            this.cb_profile.SelectedIndex = this.cb_profile.FindStringExact(eProductSetting.ProfileName);

            if (autoStart == true)
            {
                this.btn_start_Click(new object(), new EventArgs());
            }

            InitialCardNumber();
        }

        private void updateDataDisplayOption()
        {
            if (eProductSetting.Display == displayDataType.rawdata)
            {
                this.rb_rawdata.Checked = true;
            }
            else if (eProductSetting.Display == displayDataType.scaledData)
            {
                this.rb_scaledData.Checked = true;
            }
            else if (eProductSetting.Display == displayDataType.fft)
            {
                this.rb_fft.Checked = true;
            }
        }

        private void updateSNDisplay()
        {
            if (eProductSetting.RestartIndex != -1)
            {
                string sn;
                oTestItemManage.SetResultIndex(cardNumber, oTestItemManage.GetLastResultIndex(this.cardNumber, out sn));
                this.txt_sn.Text = sn;
            }
        }

        private void displayTestItem()
        {
            selectedTestItemIndex = -1;
            dg_testItem.Rows.Clear();

            testItemSettingColl = oTestItemSetting.GetTestItem(cardNumber);

            initialTestItem();

            foreach (eTestItemSetting testItemSetting in testItemSettingColl)
            {
                dg_testItem.Rows.Add(testItemSetting.Enable, testItemSetting.SequenceIndex, testItemSetting.TestItemIndex, testItemSetting.AliasName == null ? testItemSetting.Name : "(" + testItemSetting.AliasName + ")" + testItemSetting.Name,
                                               testItemSetting.Retest, testItemSetting.RetestNUpperLimit, testItemSetting.Cycle, testItemSetting.CycleDelayMiniutes,
                                               testItemSetting.SWF, testItemSetting.Until);
            }
        }

        private void initialTestItem()
        {
            err = oTestItemManage.RefreshAllTestItemColls(cardNumber);
            if (err != Error.NoError)
            {
                MessageBox.Show("Refresh test item Fail!");
            }

            err = oTestItemManage.InitialAllTestItem(cardNumber, testItemSettingColl, parameters);
            if (err != Error.NoError)
            {
                MessageBox.Show("Initial Test Item Fail!");
            }

            if (dg_testItem.Rows.Count != 0)
            {
                dg_testItem.Rows[0].Selected = true;
            }
        }

        private void addAvailableTestItemAndProfileToCB()
        {
            //test item class
            cb_testItem.Items.Clear();
            List<KeyValuePair<string, string>> testItemList = oTestItemManage.GetSortedTestItemList();
            if (testItemList != null)
            {
                foreach (KeyValuePair<string, string> testItem in testItemList)
                {
                    cb_testItem.Items.Add(testItem.Key);
                }
            }

            //test profile
            string curProfile = eProductSetting.ProfileName;
            DataTable dt = oProductSetting.GetAllProfile();
            this.cb_profile.ComboBox.DataSource = dt;
            this.cb_profile.ComboBox.DisplayMember = "profileName";

            this.cb_profile.SelectedIndex = this.cb_profile.FindStringExact(curProfile);
        }

        private void disableGridviewSorting(DataGridView grid, int columnCount)
        {
            for (int i = 0; i < columnCount; i++)
            {
                grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btn_delTestItem_Click(object sender, EventArgs e)
        {
            int count;
            int nSelected = dg_testItem.SelectedRows.Count;
            this.Cursor = Cursors.WaitCursor;
            for (int n = nSelected - 1; n >= 0; n--)
            {

                int nRowIdx = dg_testItem.SelectedCells[n].RowIndex;
                int ntIndex = int.Parse(dg_testItem.Rows[nRowIdx].Cells["tIndex"].Value.ToString());
                //err = oTestItemManage.delTestItem(selectedTestItemIndex, out count, this.cardNumber);
                err = oTestItemManage.DelTestItem(ntIndex, out count, this.cardNumber);

                if (selectedDgTestItemIndex == -1)
                {
                    MessageBox.Show("Please select test item!");
                }
                else if (err != Error.NoError)
                {
                    MessageBox.Show(err.ToString());
                }
                else if (count == 0)
                {
                    MessageBox.Show("No test item found");
                }
                else
                {
                    //dg_testItem.Rows.RemoveAt(selectedDgTestItemIndex);
                    dg_testItem.Rows.RemoveAt(nRowIdx);
                }

            }
            updateTestItemSequence();
            updateUISeqIndex();
            this.Cursor = Cursors.Default;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.dg_testResult.Rows.Clear();
        }

        private void btn_log_Click(object sender, EventArgs e)
        {
            ResultLog fm = new ResultLog();
            fm.Show();
        }

        private void btn_scheduler_Click(object sender, EventArgs e)
        {
            Scheduler fm = new Scheduler();
            fm.ShowDialog();

            oProductSetting.GetProductSetting();
            setUpScheduler();
        }

        private void setUpScheduler()
        {
            if (eProductSetting.TurnOff == TurnOffType.None)
            {
                this.btn_scheduler.BackColor = System.Drawing.SystemColors.Control;
                timer.Stop();
            }
            else
            {
                timer.Stop();
                this.btn_scheduler.BackColor = Color.Pink;
                timer.Interval = eProductSetting.SchedulerTime * 1000 * 60;
                timer.Start();
            }
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            if (dg_testItem.Rows.Count == 0) return;

            if (dg_testItem.SelectedRows.Count == 0)
            {
                if (dg_testItem.CurrentCell.RowIndex > -1)
                {
                    dg_testItem.Rows[dg_testItem.CurrentCell.RowIndex].Selected = true;
                }
                else
                {
                    return;
                }
            }

            List<DataGridViewRow> SelectedRows = new List<DataGridViewRow>();
            foreach (DataGridViewRow dgvr in dg_testItem.SelectedRows)
            {
                SelectedRows.Add(dgvr);
            }

            SelectedRows.Sort(dataGridViewRowIndexCompare);

            for (int i = 0; i <= SelectedRows.Count - 1; i++)
            {
                int selRowIndex = SelectedRows[i].Index;
                if (selRowIndex > 0)
                {
                    //dataGridView1.Rows[selRowIndex].Selected = false;

                    dg_testItem.Rows[selRowIndex].Selected = false;
                    dg_testItem.Rows.Remove(SelectedRows[i]);
                    //SelectedRows[i].Selected = false;
                    dg_testItem.Rows.Insert(selRowIndex - 1, SelectedRows[i]);
                    dg_testItem.CurrentCell.Selected = false;
                    dg_testItem.Rows[selRowIndex - 1].Selected = true;
                }
            }

            if (dg_testItem.SelectedRows.Count == 1)
            {
                dg_testItem.CurrentCell = dg_testItem.Rows[SelectedRows[0].Index].Cells[0];
            }

            updateTestItemSequence();
            updateUISeqIndex();
        }

        private void updateTestItemSequence()
        {
            uint[] testItemIndexs = new uint[dg_testItem.Rows.Count];
            uint[] seqIndexs = new uint[dg_testItem.Rows.Count];

            for (uint rowIndex = 0; rowIndex < dg_testItem.Rows.Count; rowIndex++)
            {
                testItemIndexs[rowIndex] = uint.Parse(dg_testItem.Rows[(int)rowIndex].Cells[(int)dgTestItemColumns.index].Value.ToString());
                seqIndexs[rowIndex] = rowIndex;
            }

            oTestItemSetting.UpdateTestItemSequence("seqIndex", seqIndexs, testItemIndexs, cardNumber);
        }

        private static int dataGridViewRowIndexCompare(DataGridViewRow x, DataGridViewRow y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    int retval = x.Index.CompareTo(y.Index);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater.
                        //
                        return retval;
                    }
                    else
                    {
                        // If the strings are of equal length,
                        // sort them with ordinary string comparison.
                        //
                        return x.Index.CompareTo(y.Index);
                    }
                }
            }
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            if (dg_testItem.Rows.Count == 0) return;

            if (dg_testItem.SelectedRows.Count == 0)
            {
                if (dg_testItem.CurrentCell.RowIndex > -1)
                {
                    dg_testItem.Rows[dg_testItem.CurrentCell.RowIndex].Selected = true;
                }
                else
                {
                    return;
                }
            }

            List<DataGridViewRow> SelectedRows = new List<DataGridViewRow>();
            foreach (DataGridViewRow dgvr in dg_testItem.SelectedRows)
            {
                SelectedRows.Add(dgvr);
            }

            SelectedRows.Sort(dataGridViewRowIndexCompare);

            for (int i = SelectedRows.Count - 1; i >= 0; i--)
            {
                int selRowIndex = SelectedRows[i].Index;
                if ((selRowIndex <= dg_testItem.Rows.Count - 1) && (!(selRowIndex == dg_testItem.Rows.Count - 1)))
                {

                    dg_testItem.Rows.Remove(SelectedRows[i]);
                    dg_testItem.Rows[selRowIndex].Selected = false;
                    dg_testItem.Rows.Insert(selRowIndex + 1, SelectedRows[i]);
                    dg_testItem.Rows[selRowIndex + 1].Selected = true;
                }
            }
            updateTestItemSequence();
            updateUISeqIndex();
        }

        private void rb_dataType_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_rawdata.Checked == true)
            {
                oProductSetting.UpdateDataTypeDisplay(displayDataType.rawdata);
            }
            else if (rb_scaledData.Checked == true)
            {
                oProductSetting.UpdateDataTypeDisplay(displayDataType.scaledData);
            }
            else if (rb_fft.Checked == true)
            {
                oProductSetting.UpdateDataTypeDisplay(displayDataType.fft);
            }
            oProductSetting.GetProductSetting();
        }

        private void btn_copyAndNew_Click(object sender, EventArgs e)
        {
            profileSelect fm = new profileSelect(eProductSetting.ProfileName);
            int oriProfileIdx = eProductSetting.ProfileIndex;
            fm.ShowDialog();

            if (fm.ProfileIndex == -1)
            {
                return;
            }

            eProductSetting.ProfileIndex = fm.ProfileIndex;
            copyAndPaste(oriProfileIdx, true, fm.ProfileName);
        }

        private void btn_copyAndPaste_Click(object sender, EventArgs e)
        {
            int oriProfileIdx = eProductSetting.ProfileIndex;
            copyAndPaste(oriProfileIdx, false, "");
        }

        private void copyAndPaste(int nOriProfileIdx, bool bToNewProfile, string sProfileName)
        {
            this.Cursor = Cursors.WaitCursor;
            for (int index = dg_testItem.SelectedRows.Count - 1; index >= 0; index--)
            {
                DataGridViewRow dgr = dg_testItem.SelectedRows[index];
                int fromTestItemIndex = int.Parse(dgr.Cells["tIndex"].Value.ToString());
                err = oTestItemManage.CopyAndNewRealTestItem(fromTestItemIndex, nOriProfileIdx, cardNumber);
                if (err != Error.NoError)
                {
                    MessageBox.Show("Add test item into DB fail!");
                    return;
                }
            }

            if (bToNewProfile == true)
                this.cb_profile.SelectedIndex = cb_profile.FindStringExact(sProfileName);

            displayTestItem();

            this.Cursor = Cursors.Default;
            dg_testItem.ClearSelection();
            dg_testItem.FirstDisplayedScrollingRowIndex = dg_testItem.Rows.Count - 1;
            dg_testItem.Rows[dg_testItem.RowCount - 1].Selected = true;
        }

        private void btn_aliasName_Click(object sender, EventArgs e)
        {
            int testItemIndex = int.Parse(dg_testItem.SelectedRows[0].Cells["tIndex"].Value.ToString());
            string salisaname = dg_testItem.SelectedRows[0].Cells["name"].Value.ToString();
            string saname = string.Empty;
            int nStart = salisaname.IndexOf('(');
            if (nStart >= 0)
            {
                int nEnd = salisaname.IndexOf(')');
                saname = salisaname.Substring(nStart + 1, nEnd - nStart - 1);
            }
            aliasNameSetting fm = new aliasNameSetting(testItemIndex, saname);
            fm.ShowDialog();

            if (fm.UpdateSuccess == true)
            {
                string name = this.dg_testItem.SelectedRows[0].Cells["name"].Value.ToString();

                if (name[0] != '(') //without alias name
                {
                    this.dg_testItem.SelectedRows[0].Cells["name"].Value = "(" + fm.AliasName + ")" + name;
                }
                else
                {
                    this.dg_testItem.SelectedRows[0].Cells["name"].Value = "(" + fm.AliasName + ")" + name.Substring(name.IndexOf(")") + 1, name.Length - name.IndexOf(")") - 1);
                }
            }
        }

        private void btn_exportProfile_Click(object sender, EventArgs e)
        {
            if (sfd_profileExport.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = sfd_profileExport.FileName;
                    oTestItemManage.ExportProfile(filePath, cardNumber);
                    MessageBox.Show("Export Profile " + eProductSetting.ProfileName + " Success!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export Profile " + eProductSetting.ProfileName + " Fail!, " + ex.ToString());
                }
            }
        }

        private void btn_importProfile_Click(object sender, EventArgs e)
        {
            if (ofd_profileImport.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd_profileImport.FileName;

                if (MessageBox.Show("The original profile " + eProductSetting.ProfileName + " will be override, are you sure?", "Import Profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Error err = oTestItemManage.ImportProfile(filePath, cardNumber);
                    if (err == Error.NoError)
                    {
                        MessageBox.Show("Import Success!");
                        displayTestItem();
                    }
                    else
                    {
                        MessageBox.Show("Import Fail!");
                    }
                }
            }
        }

        private void controlPanelEnableConfig()
        {
            if (eProductSetting.Lock == true)
            {
                //                this.dg_testItem.Enabled = false;
                this.dg_testItem.Columns["enable"].ReadOnly = true;
                this.dg_testItem.Columns["ReTest"].ReadOnly = true;
                this.dg_testItem.Columns["Cycle"].ReadOnly = true;
                this.dg_testItem.Columns["retestNUpperLimit"].ReadOnly = true;
                this.dg_testItem.Columns["until"].ReadOnly = true;
                this.dg_testItem.Columns["cycleDelay"].ReadOnly = true;
                this.dg_testItem.Columns["stopWhenFail"].ReadOnly = true;

                this.property_testItem.Enabled = false;
                this.cb_profile.Enabled = false;
                this.txt_globalCycle.Enabled = false;
                this.txt_globalCycleDelay.Enabled = false;
                this.btn_scheduler.Enabled = false;
                this.btn_addTestItem.Enabled = false;
                this.btn_delTestItem.Enabled = false;
                this.btn_up.Enabled = false;
                this.btn_down.Enabled = false;
                this.cb_testItem.Enabled = false;
            }
            else if (eProductSetting.Lock == false)
            {

                this.dg_testItem.Columns["enable"].ReadOnly = false;
                this.dg_testItem.Columns["ReTest"].ReadOnly = false;
                this.dg_testItem.Columns["Cycle"].ReadOnly = false;
                this.dg_testItem.Columns["retestNUpperLimit"].ReadOnly = false;
                this.dg_testItem.Columns["until"].ReadOnly = false;
                this.dg_testItem.Columns["cycleDelay"].ReadOnly = false;
                this.dg_testItem.Columns["stopWhenFail"].ReadOnly = false;

                this.dg_testItem.Enabled = true;
                this.property_testItem.Enabled = true;
                this.cb_profile.Enabled = true;
                this.txt_globalCycle.Enabled = true;
                this.txt_globalCycleDelay.Enabled = true;
                this.btn_scheduler.Enabled = true;
                this.btn_addTestItem.Enabled = true;
                this.btn_delTestItem.Enabled = true;
                this.btn_up.Enabled = true;
                this.btn_down.Enabled = true;
                this.cb_testItem.Enabled = true;
            }

            if (eProductSetting.Lock == false)
            {
                this.btn_lock.Text = "Lock";
            }
            else if (eProductSetting.Lock == true)
            {
                this.btn_lock.Text = "UnLock";
            }

            if (eProductSetting.AutoClose == true)
            {
                this.btn_autoClose.Text = "AutoClose Disable";
            }
            else if (eProductSetting.AutoClose == false)
            {
                this.btn_autoClose.Text = "AutoClose Enable";
            }

        }

        private void btn_lock_Click(object sender, EventArgs e)
        {
            Password fm = new Password();
            fm.ShowDialog();

            if (fm.PASSWORD == "mvarock")
            {
                if (this.btn_lock.Text == "Lock")
                {
                    oProductSetting.UpdateLock(1);
                }
                else if (this.btn_lock.Text == "UnLock")
                {
                    oProductSetting.UpdateLock(0);
                }
                oProductSetting.GetProductSetting();

                controlPanelEnableConfig();
            }
            else
            {
                MessageBox.Show("Wrong pass word");
            }
        }

        private void btn_autoClose_Click(object sender, EventArgs e)
        {
            if (this.btn_autoClose.Text == "AutoClose Enable")
            {
                oProductSetting.UpdateAutoClose(1);
                this.btn_autoClose.Text = "AutoClose Disable";
            }
            else if (this.btn_autoClose.Text == "AutoClose Disable")
            {
                oProductSetting.UpdateAutoClose(0);
                this.btn_autoClose.Text = "AutoClose Enable";
            }
            oProductSetting.GetProductSetting();
        }

        private void writeToFile(string filePath, double[] data)
        {
            int dataNumber = data.Length;
            StreamWriter sw = new StreamWriter(filePath);

            for (int dpIndex = 0; dpIndex < dataNumber; dpIndex++)
            {
                sw.WriteLine(data[dpIndex] + ",");
            }
            sw.Close();

            MessageBox.Show("Export done!");
        }

        private void btn_exportScaledData_Click(object sender, EventArgs e)
        {
            ExportData("ScaledDatas");
        }

        private void btn_exportRawData_Click(object sender, EventArgs e)
        {
            ExportData("Datas");
        }

        private void ExportData(string DataType)
        {
            if (sfd_export.ShowDialog() == DialogResult.OK)
            {
                string filePath = sfd_export.FileName;

                int testItemIndex = int.Parse(dg_testResult.Rows[this.dg_testResult.CurrentRow.Index].Cells["index"].Value.ToString());
                int channel = int.Parse(dg_testResult.Rows[this.dg_testResult.CurrentRow.Index].Cells["channel"].Value.ToString());
                TestItem testItem = oTestItemManage.GetTestItem(testItemIndex, cardNumber);
                double[] data = ((double[][])testItem.GetType().GetProperty(DataType).GetValue(testItem, null))[channel];
                writeToFile(filePath, data);
            }
        }

        private void btn_repair_Click(object sender, EventArgs e)
        {
            this.dg_testItem.Enabled = true;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            btn_delTestItem_Click(sender, e);
        }

        private void btn_moveTo_Click(object sender, EventArgs e)
        {
            MoveTo MoveForm = new MoveTo();
            if (MoveForm.ShowDialog() == DialogResult.OK)
            {
                string sIndex = MoveForm.sColumn;
                int nRowCount = dg_testItem.Rows.Count;
                int nNewColumn = Convert.ToInt16(sIndex);

                if (nRowCount < Convert.ToInt16(sIndex))
                {
                    MessageBox.Show("please check the sIndex number again!");
                    return;
                }

                int n = dg_testItem.SelectedRows.Count;
                for (int i = n - 1; i >= 0; i--)
                {
                    int nOldColumn = dg_testItem.SelectedCells[i].RowIndex;
                    DataGridViewRow dr = dg_testItem.Rows[nOldColumn];
                    dg_testItem.Rows.RemoveAt(nOldColumn);
                    dg_testItem.Rows.Insert(nNewColumn - 1, dr);
                }
                updateTestItemSequence();
                updateUISeqIndex();
                dg_testItem.ClearSelection();
                int nPos = nNewColumn - 5;
                if (nPos < 0)
                    nPos = 0;
                dg_testItem.FirstDisplayedScrollingRowIndex = nPos;

                dg_testItem.Rows[nNewColumn - 1].Selected = true;
            }
        }

        private void btn_copyTo_Click(object sender, EventArgs e)
        {

        }

        private void btn_runRangeTestItem_Click(object sender, EventArgs e)
        {
            RunTo runfrm = new RunTo();
            if (runfrm.ShowDialog() == DialogResult.OK)
            {
                State = ProcessState.Start;
                pb_bar.Maximum = 0;
                int nFrom = Convert.ToInt32(runfrm.sRunFrom);
                int nTo = Convert.ToInt32(runfrm.sRunTo);
                pb_bar.Maximum = (nTo - nFrom) + 1;
                pb_bar.Maximum *= int.Parse(txt_globalCycle.Text.ToString());
                nFrom--;
                nTo--;

                for (int i = 0; i < dg_testItem.Rows.Count; i++)
                {
                    dg_testItem.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                dg_testItem.ClearSelection();
                tStartTest = new Thread(runRange);
                string sPara = nFrom.ToString() + "," + nTo.ToString();
                tStartTest.Start(sPara);
            }
        }

        private void runRange(object oPara)
        {
            string myStr = (string)oPara;
            string[] str = myStr.Split(',');
            int nStart = Convert.ToInt16(str[0]);
            int nEnd = Convert.ToInt16(str[1]);
            this.BeginInvoke(dStartBtnEnable, false);
            DateTime cycleStarTime = DateTime.Now;

            try
            {
                if (initialTestCheck() == false)
                {
                    return;
                }

                oTestItemManage.ResetTestItemCycle(this.cardNumber);
                oTestItemManage.StartTestLog(this.cardNumber);

                if (State == ProcessState.Stop) { return; }

                cycleStarTime = DateTime.Now;

                for (int index = nStart; index <= nEnd; index++)
                {
                    this.dg_testItem.Rows[index].DefaultCellStyle.BackColor = SystemColors.Highlight;

                    TestItem testItem = getTestItem(index);
                    testItem.TestItemEvent += testItem_TestItemEvent;

                    if (testItem.IndependentThread == false)
                    {
                        TestResult testResult = test(index, testItem);
                    }
                    else if (testItem.IndependentThread == true)
                    {
                        int indexCopy = index;
                        Thread testT = new Thread(() => testThread(indexCopy, testItem));
                        testT.Start();

                        testThreadColls.Add(testT);
                    }
                    testItem.TestItemEvent -= testItem_TestItemEvent;
                    if (this.dg_testItem.Rows[index].DefaultCellStyle.BackColor == SystemColors.Highlight)
                        this.dg_testItem.Rows[index].DefaultCellStyle.BackColor = Color.White;
                }
                //waiting all thread end
                foreach (Thread testT in testThreadColls)
                {
                    testT.Join();
                }

                oTestItemManage.ResultMessageFromDB(this.cardNumber);
                oTestItemManage.ExportLogFileToPE(this.cardNumber, true);
                oTestItemManage.ExportXMLFile(this.cardNumber);
                //oTestItemManage.ExportLogFileToWebDisk(this.cardNumber);

                if (eProductSetting.AutoClose == true)
                {
                    this.BeginInvoke(dCloseMain);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                State = ProcessState.Stop;
                this.BeginInvoke(dStartBtnEnable, true);
            }
        }

        private void txt_globalCycle_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkIfDigital(e);
        }

        private void txt_globalCycleDelay_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkIfDigital(e);
        }

        private void checkIfDigital(KeyPressEventArgs e)
        {
            e.Handled = true;
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
                e.Handled = false;
        }
        private void updateUISeqIndex()
        {
            int nRowCount = dg_testItem.RowCount;
            for (int i = 0; i < nRowCount; i++)
            {
                dg_testItem.Rows[i].Cells["seqIndex"].Value = i + 1;
            }
        }

        private void btn_enableSelectedTestItems_Click(object sender, EventArgs e)
        {
            updateSelectedStatus(true);
        }

        private void btn_disableSelectedTestItems_Click(object sender, EventArgs e)
        {
            updateSelectedStatus(false);
        }

        private void updateSelectedStatus(bool bStatus)
        {
            this.Cursor = Cursors.WaitCursor;
            int n = dg_testItem.SelectedRows.Count;
            uint[] tiIdex = new uint[n];
            uint[] value = new uint[n];
            for (int i = 0; i < n; i++)
            {
                uint ntindex = Convert.ToUInt16(dg_testItem.SelectedRows[i].Cells["tindex"].Value.ToString());
                tiIdex[i] = ntindex;
                if (bStatus == true)
                    value[i] = 1;
                else
                    value[i] = 0;
                dg_testItem.SelectedRows[i].Cells["enable"].Value = bStatus;
            }

            oTestItemSetting.UpdateTestItemSequence("enable", value, tiIdex, cardNumber);
            this.Cursor = Cursors.Default;
        }

        private void btn_deleteAll_Click(object sender, EventArgs e)
        {
            DialogResult Res = MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Res == DialogResult.Yes)
            {
                oTestItemManage.DelProfileTestItem(eProductSetting.ProfileIndex, cardNumber);
                dg_testItem.Rows.Clear();
            }
        }
    }
}