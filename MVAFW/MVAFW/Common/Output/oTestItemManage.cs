using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Xml;

using MVAFW.Common.Entity;
using MVAFW.Config;
using MVAFW.TestItemColls;
using MVAFW.DB;

namespace MVAFW.Common.Output
{
    public static class oTestItemManage
    {
        private static SortedDictionary<string, string> dicClassName;
        private static Dictionary<uint, eTestItemManage> testItemManage;
       

        public static void DelTestItem(string name)
        {
            if (dicClassName.ContainsKey(name))
            {
                dicClassName.Remove(name);
            }
        }

        public static void AddTestItem(string name, string className)
        {
            if (dicClassName == null)
            {
                dicClassName = new SortedDictionary<string, string>();
            }

            if (dicClassName.ContainsKey(name) == false)
            {
                dicClassName.Add(name, className);
            }
        }

        public static DataTable GetTestItemProperty(int testItemIndex, uint cardNumber)
        {
            return MvaDSManager.MainDB.getTestItemProperty(testItemIndex, cardNumber);
        }

        private static Error crateTestItemAndAssignProperty(uint cardNumber, string name, int testItemIndex, params string[] parameters)
        {
            //new testitem
            TestItem testItem = null;
            if(name[0]=='(')
            {
                int rightBraceIndex = name.IndexOf(')');
                name = name.Substring(rightBraceIndex+1, name.Length - rightBraceIndex-1);
            }
            Type type = Type.GetType(dicClassName[name], true);

            //below should be enhanced in the future
            if (type.GetConstructors()[0].GetParameters().Length == 0)
            {
                testItem = (TestItem)Activator.CreateInstance(type);
            }
            else if(type.GetConstructors()[0].GetParameters()[0].ParameterType.Name=="String")
            {
                if (parameters.Length < 2)
                {
                    testItem = (TestItem)Activator.CreateInstance(type, "");
                }
                else
                {
                    testItem = (TestItem)Activator.CreateInstance(type, parameters[1]);
                }
            }
            testItem.Index = testItemIndex;
            testItem.Name = name;
            //assign property
            DataTable dt = GetTestItemProperty(testItemIndex, cardNumber);
            foreach (DataRow dr in dt.Rows)
            {
                string pName = dr["name"].ToString();
                string pValue = dr["value"].ToString();
                PropertyInfo pInfo = testItem.GetType().GetProperty(pName);

                if(pInfo==null)
                {
                    continue;
                }

                if (pName == "ChannelGainQueueString")
                {
                    if (pValue == "")
                    {
                        continue;
                    }
                    ushort totalChannelCount = (ushort)pValue.Split(':')[0].Split('=')[1].Split(',').Length;
                    string[] settings = pValue.Split(':');

                    pInfo = testItem.GetType().GetProperty("ChannelNumbers");
                    pInfo.SetValue(testItem, totalChannelCount, null);

                    for (int i = 0; i < settings.Length; i++)
                    {
                        pName = settings[i].Split('=')[0];
                        pInfo = testItem.GetType().GetProperty(pName);

                        ushort[] values = Array.ConvertAll(settings[i].Split('=')[1].Split(','), new Converter<string, ushort>(ushort.Parse));
                        pInfo.SetValue(testItem, values, null);
                    }
                }
                else if (pInfo.PropertyType.IsEnum == true)
                {
                    pInfo.SetValue(testItem, System.Enum.Parse(pInfo.PropertyType, pValue, true), null);
                }
                else if (pInfo.PropertyType.Name == "String[]")
                {
                    if (pValue != string.Empty)
                    {
                        //double[] values = Array.ConvertAll(pValue.Split(','), new Converter<string, double>(Double.Parse));
                        string[] values = pValue.Split(':');
                        pInfo.SetValue(testItem, values, null);
                    }
                }
                else
                {
                    //FileName property means exe and bat type, need to store the exe and bat file name.
                    if (pName == "FileName")
                    {
                        pInfo.SetValue(testItem, pValue, null);
                    }
                    else
                    {
                        pInfo.SetValue(testItem, Convert.ChangeType(pValue, pInfo.PropertyType), null);
                    }
                }
            }

            addTestItemColls(cardNumber, testItem);

            return Error.NoError;
        }

       // public static Error copyAndNewRealTestItem(int fromTestItemIndex, string name,  uint cardNumber)
        public static Error CopyAndNewRealTestItem(int fromTestItemIndex, int oriProfileIdx, uint cardNumber)
        {
            Error err;
            int testItemIndex;

            uint seqIndex = GetTestItemCount(eProductSetting.ProfileIndex);// +1;           
            //err = addTestItemToDB(seqIndex, name, cardNumber, out testItemIndex);
            err = addExistedTestItemToDB(seqIndex, oriProfileIdx, fromTestItemIndex, cardNumber, out testItemIndex);
            if (err != Error.NoError)
            {
                return err;
            }

             err = MvaDSManager.MainDB.addTestItemProperty(testItemIndex, fromTestItemIndex, oriProfileIdx, cardNumber);  
             if (err != Error.NoError)
            {
                return err;
            }
                       
            return err;
        }        

        private static Error addTestItemToDB(uint seqIndex, string name, uint cardNumber, out int testItemIndex)
        {
            Error err;
            testItemIndex = -1;

            err = MvaDSManager.MainDB.addTestItem(seqIndex, name, cardNumber);
            if (err != Error.NoError)
            {
                return Error.DBError;
            }

            DataTable dt = MvaDSManager.MainDB.getLastInsertTestItem();
            testItemIndex = int.Parse(dt.Rows[0]["last_insert_rowid()"].ToString());

            return Error.NoError;
        }

        private static Error addExistedTestItemToDB(uint seqIndex, int OriProfileIdx ,int fromOriTestitemindex, uint cardNumber, out int testItemIndex)
        {
            Error err;
            testItemIndex = -1;

            err = MvaDSManager.MainDB.addTestItem(seqIndex, OriProfileIdx,fromOriTestitemindex, cardNumber);
            if (err != Error.NoError)
            {
                return Error.DBError;
            }

            DataTable dt = MvaDSManager.MainDB.getLastInsertTestItem();
            testItemIndex = int.Parse(dt.Rows[0]["last_insert_rowid()"].ToString());

            return Error.NoError;
        }

        public static Error CreateRealTestItem(string name, uint cardNumber)
        {
            int testItemIndex;
            Error err;

            if(dicClassName.ContainsKey(name)==false)
            {
                return Error.NoData;
            }

            uint seqIndex = GetTestItemCount(eProductSetting.ProfileIndex);// +1;
            err = addTestItemToDB(seqIndex, name, cardNumber, out testItemIndex);
            if (err != Error.NoError)
            {
                return err;
            }

            //create a temp object
            Type type = Type.GetType(dicClassName[name], true);
            TestItem testItem = null;
            if (type.GetConstructors()[0].GetParameters().Length == 0)
            {
                testItem = (TestItem)Activator.CreateInstance(type);
            }
            else if (type.GetConstructors()[0].GetParameters()[0].ParameterType.Name == "String")
            {
                testItem = (TestItem)Activator.CreateInstance(type, "");
            }

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(testItem);
            List<string> names = new List<string>();
            List<string> values = new List<string>();
            foreach (PropertyDescriptor propertie in props)
            {
                string pName = propertie.DisplayName;
                if (pName == "Capacity" || pName == "Count") { continue; }
                else if (propertie.PropertyType.Name == "String[]"){ continue; }

                string pValue = propertie.GetValue(testItem) == null ? string.Empty : propertie.GetValue(testItem).ToString();
                
                if (!propertie.IsReadOnly)
                {
                    names.Add(pName);
                    values.Add(pValue);
                }
            }

            err = MvaDBDataFetcher.MvaDB.ExecTransToAddProperty(testItemIndex, names.ToArray(), values.ToArray(), cardNumber);
            if (err != Error.NoError)
            {
                return err;
            }

            //err = crateTestItemAndAssignProperty(cardNumber, name, testItemIndex);

            return err;
        }

        public static Error AddTestItemProperty(int testItemIndex, string name, string value, uint cardNumber)
        {
            return MvaDSManager.MainDB.addTestItemProperty(testItemIndex, name, value, cardNumber);
        }

        public static Error InitialAllTestItem(uint cardNumber, List<eTestItemSetting> testItemSettings, params string[] parameters)
        {
            foreach (eTestItemSetting testItemSetting in testItemSettings)
            {
                int testItemIndex = testItemSetting.TestItemIndex;
                string name = testItemSetting.Name;

                crateTestItemAndAssignProperty(cardNumber, name, testItemIndex, parameters);
            }         

            return Error.NoError;
        }

        private static Error addTestItemColls(uint cardNumber, TestItem testItem)
        {
            if (testItemManage == null)
            {
                testItemManage = new Dictionary<uint, eTestItemManage>();
            }

            if (testItemManage.ContainsKey(cardNumber) == false)
            {
                testItemManage[cardNumber] = new eTestItemManage();
            }
            else
            {
                if (testItemManage[cardNumber].TestItemColls.ContainsKey(testItem.Index))
                {
                    return Error.GeneralError;
                }
            }
            testItemManage[cardNumber].TestItemColls.Add(testItem.Index, testItem);
            return Error.NoError;
        }

        public static Error RefreshAllTestItemColls(uint cardNumber)
        {
            try
            {
                List<eTestItemClass> testItemClasses = oTestItemSetting.GetTestItemClass();

                foreach (eTestItemClass t in testItemClasses)
                {                    
                    if (t.Category != null)//for backward compatibility
                    {
                        t.Name = t.Name + ":" + t.Product;
                    }
                    AddTestItem(t.Name, t.FullClassName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Error.GeneralError;
            }

            return Error.NoError;
        }

        public static Error DelTestItem(int tindex, out int count, uint cardNumber)
        {
            Error err = MvaDSManager.MainDB.delTestItem(tindex, out count, cardNumber);
            if (err != Error.NoError)
            {
                return err;
            }

            if (count == 0)
            {
                return Error.NoData;
            }

            oTestItemManage.testItemManage[cardNumber].TestItemColls.Remove(tindex);

            return Error.NoError;
        }

        public static bool IsTestResultPASS(uint cardNumber)
        {
            DataTable testDetail = GetTestResultDetail(testItemManage[cardNumber].ResultIndex, cardNumber);
            foreach (DataRow dr in testDetail.Rows)
            {
                string result = dr["resultDetail"].ToString();

                if(result=="FAIL")
                {
                    return false;
                }
            }

            return true;
        }

        public static void ResultMessageFromDB(uint cardNumber)
        {
            DataTable testDetail = GetTestResultDetail(testItemManage[cardNumber].ResultIndex, cardNumber);
            StringBuilder sb = new StringBuilder();
            bool fail=false;

            foreach (DataRow dr in testDetail.Rows)
            {
                string result = dr["resultDetail"].ToString();

                if (result != "PASS" && result!="NoCompare")
                {
                    fail = true;
                    sb.Append(dr["testItemIndex"].ToString());
                    sb.Append(",");
                    sb.Append(dr["name"].ToString());
                    sb.Append(",");
                    sb.Append(dr["channel"].ToString());
                    sb.Append(",");
                    sb.Append(dr["Description"].ToString());
                    sb.Append(",");
                    sb.Append(dr["ErrorCode"].ToString());
                    sb.AppendLine();
                }
            }

            if (fail == false)
            {
                MvaDSManager.MainDB.updateTestResult(testItemManage[cardNumber].ResultIndex, TestResult.PASS.ToString());
                eProductSetting.Result = "PASS";
                if (eProductSetting.AutoClose == false)
                {
                    MessageBox.Show(TestResult.PASS.ToString(), TestResult.PASS.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
            else
            {
                MvaDSManager.MainDB.updateTestResult(testItemManage[cardNumber].ResultIndex, TestResult.FAIL.ToString());
                eProductSetting.Result = "FAIL";
                if (eProductSetting.AutoClose == false)
                {
                    MessageBox.Show(sb.ToString(), "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
        }

        public static void ResultMessage(uint cardNumber, int testItemIndex)
        {
            TestItem testItem = oTestItemManage.testItemManage[cardNumber].TestItemColls[testItemIndex];
            testItem.Cycle = 0;
            StringBuilder sb = new StringBuilder();

            if (testItem.FinalResult == TestResult.FAIL || testItem.FinalResult==TestResult.UNKNOWN)
            {
                string channelDescription = String.Join(", ", testItem.DescriptionByChannel);

                sb.Append(testItem.Index);
                sb.Append(",");
                sb.Append(testItem.Name);
                sb.Append(",");
                sb.Append(testItem.DescriptionByChannel.Count > 0 ? String.Join(";", testItem.DescriptionByChannel) : testItem.Description);
                sb.Append(",");
                sb.Append(testItem.ErrCode);
                sb.AppendLine();
                MessageBox.Show(sb.ToString(), "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MvaDSManager.MainDB.updateTestResult(testItemManage[cardNumber].ResultIndex, TestResult.FAIL.ToString());
            }
            else
            {
                MessageBox.Show(TestResult.PASS.ToString(), TestResult.PASS.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                MvaDSManager.MainDB.updateTestResult(testItemManage[cardNumber].ResultIndex, TestResult.PASS.ToString());
            }
        }

        public static void ResetTestItemCycle(uint cardNumber)
        {
            foreach(int key in oTestItemManage.testItemManage[cardNumber].TestItemColls.Keys)
            {
                oTestItemManage.testItemManage[cardNumber].TestItemColls[key].Cycle = 0;
            }
        }

        public static void ResultMessage(uint cardNumber)
        {
            List<eTestItemSetting> testItemSettingColl = oTestItemSetting.GetTestItem(cardNumber);
            StringBuilder sb = new StringBuilder();
            bool fail=false;
            for (int i = 0; i < testItemSettingColl.Count; i++)
            {
                bool enable = testItemSettingColl[i].Enable;
                if (enable == false)
                {
                    continue;
                }

                int index = testItemSettingColl[i].TestItemIndex;
                TestItem testItem = oTestItemManage.testItemManage[cardNumber].TestItemColls[index];
                testItem.Cycle = 0;

                if (testItem.FinalResult == TestResult.FAIL || testItem.FinalResult==TestResult.UNKNOWN)
                {
                    fail = true;
                    sb.Append(testItem.Index);
                    sb.Append(",");
                    sb.Append(testItem.Name);
                    sb.Append(",");
                    sb.Append(testItem.DescriptionByChannel.Count > 0 ? String.Join(";", testItem.DescriptionByChannel) : testItem.Description);
                    sb.Append(",");
                    sb.Append(testItem.ErrCode);
                    sb.AppendLine();
                }
            }

            if (fail == false)
            {
                MessageBox.Show(TestResult.PASS.ToString(), TestResult.PASS.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                MvaDSManager.MainDB.updateTestResult(testItemManage[cardNumber].ResultIndex, TestResult.PASS.ToString());
            }
            else
            {
                MessageBox.Show(sb.ToString(), "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MvaDSManager.MainDB.updateTestResult(testItemManage[cardNumber].ResultIndex, TestResult.FAIL.ToString());
            }
        }

        public static void StartTestLog(uint cardNumber)
        {
            long rowId;
            Error err = MvaDSManager.MainDB.startTestLog(eProductSetting.SN, out rowId, cardNumber, eProductSetting.ProfileIndex);

            if (err != Error.NoError)
            {
                MessageBox.Show("oTestItemManage StartTestLog Fail!" + err.ToString());
                return;
            }
            else if (rowId == 0)
            {
                MessageBox.Show("oTestItemManage StartTestLog Fail Count=0!");
                return;
            }

            //int resultIndex = getLastResultIndex();
            testItemManage[cardNumber].ResultIndex = rowId;
            Console.WriteLine(rowId);
            testItemManage[cardNumber].SN = eProductSetting.SN;
        }

        public static int GetLastResultIndex()
        {
            DataTable dt = MvaDSManager.MainDB.getLastInsertResult();
            return int.Parse(dt.Rows[0]["last_insert_rowid()"].ToString());
        }

        public static int GetLastResultIndex(uint cardNumber, out string sn)
        {
            DataTable dt = MvaDSManager.MainDB.getLastResultIndex(cardNumber);
            sn = dt.Rows[0]["sn"].ToString();
            return int.Parse(dt.Rows[0]["resultIndex"].ToString());
        }

        public static void SetResultIndex(uint cardNumber, long index)
        {
            oTestItemManage.testItemManage[cardNumber].ResultIndex = index;
        }

        public static long GetResultIndex(uint cardNumber)
        {
            return oTestItemManage.testItemManage[cardNumber].ResultIndex;
        }

        public static void AddResultDetail(TestItem testItem, uint cardNumber)
        {
            Error err = Error.GeneralError;
            long resultDetailIndex;
            if (testItem.MultiChannel == false)
            {
                string channelDescription = testItem.SpecificChannel < testItem.DescriptionByChannel.Count ? testItem.DescriptionByChannel[testItem.SpecificChannel] : testItem.Description;
                err = MvaDSManager.MainDB.addResultDetail(
                                                                     testItem.Index,
                                                                     testItem.Spec.ToString(),
                                                                     testItem.Values[testItem.SpecificChannel].ToString(),
                                                                     testItem.Results[testItem.SpecificChannel],
                                                                     testItem.Cycle,
                                                                     cardNumber,
                                                                     eProductSetting.ProfileIndex,
                                                                     testItemManage[cardNumber].ResultIndex,
                                                                     testItem.SpecificChannel,
                                                                     testItem.ErrCode,
                                                                     channelDescription,
                                                                     testItem.CompareType.ToString(),
                                                                     out resultDetailIndex
                                                                     );

                if (testItem is AITestItem && ((AITestItem)testItem).LogData == true)
                {
                    addDataLog(testItem, resultDetailIndex, testItemManage[cardNumber].ResultIndex, testItem.SpecificChannel, testItem.SpecificChannel);
                }
            }
            else if (testItem.MultiChannel == true)
            {
                for (short channel = 0; channel < testItem.ChannelNumbers; channel++)
                {
                    string channelDescription = channel < testItem.DescriptionByChannel.Count ? testItem.DescriptionByChannel[channel] : testItem.Description;
                    err = MvaDSManager.MainDB.addResultDetail(
                                                                         testItem.Index,
                                                                         testItem.Spec.ToString(),
                                                                         testItem.Values[channel]==null? "" : testItem.Values[channel].ToString(),
                                                                         testItem.Results[channel],
                                                                         testItem.Cycle,
                                                                         cardNumber,
                                                                         eProductSetting.ProfileIndex,
                                                                         testItemManage[cardNumber].ResultIndex,
                                                                         testItem.ToChShort(channel),
                                                                         testItem.ErrCode,
                                                                         testItem.CompareType == CompareType.LessThanPercentFSR ? string.Format("{0}:{1:F3}%", channelDescription, testItem.RealPercentage[channel]) : channelDescription,
                                                                         testItem.CompareType.ToString(),
                                                                         out resultDetailIndex
                                                                         );
                    if (err != Error.NoError) 
                    { 
                        break; 
                    }

                    if (testItem is AITestItem && ((AITestItem)testItem).LogData == true)
                    {
                        addDataLog(testItem, resultDetailIndex, testItemManage[cardNumber].ResultIndex, channel, testItem.ToChShort(channel));
                    }
                }
            }

            if (err != Error.NoError)
            {
                MessageBox.Show("oTestItemManage AddResultDetail FAIL");
            }
        }

        private static int getLastInsertResultDetailIndex()
        {
            return int.Parse(MvaDSManager.MainDB.getLastInsertResultDetail().Rows[0]["last_insert_rowid()"].ToString());
        }

        private static void addDataLog(TestItem testItem, long resultDetailIndex, long resultIndex, short channelIndex, short displayChannel)
        {
            double[] data = ((double[][])testItem.GetType().GetProperty("Datas").GetValue(testItem, null))[channelIndex];
            double[] scaledData = ((double[][])testItem.GetType().GetProperty("ScaledDatas").GetValue(testItem, null))[channelIndex];

            Error err = MvaDBDataFetcher.MvaDataDB.ExecTransToAddData(resultDetailIndex, resultIndex, data, scaledData, displayChannel);
            if (err != Error.NoError)
            {
                MessageBox.Show("oTestItemManage addDataLog fail!");
            }
        }

        public static void ExportLogFileToPE(uint cardNumber, bool displayFinalResult)
        {
            string seperte = "=======================================================";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(seperte);
            sb.AppendLine(String.Format("Model Name : {0}", eProductSetting.ModelName));
            sb.AppendLine(String.Format("Part Number : {0}", eProductSetting.PartNumber));

            DataTable testDetail = GetTestResultDetail(testItemManage[cardNumber].ResultIndex, cardNumber);

            sb.AppendLine(String.Format("Test Date(Start) : {0}", testDetail.Rows[0]["startTime"].ToString()));
            sb.AppendLine(String.Format("Test Date(Finish) : {0}", testDetail.Rows[0]["endTime"].ToString()));
            sb.AppendLine(seperte);
            sb.AppendLine(String.Format("Serial Number : {0}", eProductSetting.SN));
            sb.AppendLine(String.Format("FPGA Version : {0}", eProductSetting.FpgaVersion));
            sb.AppendLine(String.Format("MCU Version : {0}", eProductSetting.McuVersion));
            sb.AppendLine(String.Format("Firmware Version : {0}", eProductSetting.FirmwareVersion));
            sb.AppendLine(String.Format("QC Program Version : {0}", eProductSetting.QcProgramVersion));
            if (!String.IsNullOrEmpty(eProductSetting.MacID))
                sb.AppendLine(String.Format("MAC Address 1 : {0}", eProductSetting.MacID));
            sb.AppendLine(seperte);
            sb.AppendLine(String.Format("Product Description : {0}", eProductSetting.ProductDescription));
            sb.AppendLine(String.Format("Driver Version : {0}", eProductSetting.DriverVersion));
            sb.AppendLine(String.Format("Enginner : {0}", eProductSetting.Engineer));

            string[] instruments = eProductSetting.TestInstrument.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < instruments.Length; i++)
            {
                sb.AppendLine(String.Format("Test Instrument{0} : {1}",i, instruments[i]));
            }
            sb.AppendLine(seperte);
            sb.AppendLine("Result  |  Test Item  |  Channel  |  SPEC  |  VALUE  |  Description  |  Error Code | CompareType");

            //generate testitem detail
            foreach (DataRow dr in testDetail.Rows)
            {
                string resultDetail = dr["resultDetail"].ToString();
                sb.AppendLine(String.Format("[{0}]     {1}::{2}  {3}  {4}  {5}  {6}  {7} {8}", resultDetail,
                                                                                                        dr["testItemIndex"].ToString(),
                                                                                                        dr["name"].ToString(),
                                                                                                        dr["channel"].ToString(),
                                                                                                        dr["spec"].ToString(),
                                                                                                        dr["value"].ToString(),
                                                                                                        dr["description"].ToString(),
                                                                                                        dr["errorCode"].ToString(),
                                                                                                        dr["compareType"].ToString()));
            }
            sb.AppendLine(seperte);

            if (displayFinalResult == true)
            {
                string result = testDetail.Rows[0]["result"].ToString();
                sb.AppendLine(string.Format("{0} {0} {0} {0} {0} {0} {0} {0} {0} {0} {0} {0} {0} {0}", result == "PASS" ? "PASS" : "FAIL"));
            }

            if (!(Directory.Exists(eProductSetting.LogPath)))
            {
                Directory.CreateDirectory(eProductSetting.LogPath);
            }

            StreamWriter sw = new StreamWriter(eProductSetting.LogPath + "\\" + eProductSetting.SN + ".log");
            sw.Write(sb.ToString());
            sw.Close();
        }


        public static void ExportLogFileToWebDisk(uint cardNumber)
        {

        }
            public static Error DelProfileTestItem(int profileIndex, uint cardNumber)
        {
            return MvaDSManager.MainDB.delProfileTestItem(profileIndex, cardNumber);
        }

        public static Error ImportProfile(string path, uint cardNumber)
        {
            Error err;
            err = DelProfileTestItem(eProductSetting.ProfileIndex, cardNumber);
            if (err != Error.NoError)
            {
                return err;
            }

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(path);
            XmlNodeList nodeLists = XmlDoc.SelectNodes( "Profile/TestItems" );

            uint testItemCount=0;
            foreach (XmlNode node in nodeLists[0].ChildNodes)
            {
                int testItemIndex;
                err = addTestItemToDB(testItemCount, node.Name, cardNumber, out testItemIndex);
                if (err != Error.NoError)
                {
                    return err;
                }

                string testItemName = string.Empty;
                string className = string.Empty;
                foreach (XmlNode childNode in node)
                {
                    string name = childNode.Name;
                    string value = childNode.InnerText;

                    if (name == "index")
                    {
                        continue;
                    }
                    else if (name != "Property")
                    {
                        if (name != "className")
                        {
                            err = oTestItemSetting.UpdateTestItem(name, value, testItemIndex, cardNumber);
                            if (err != Error.NoError)
                            {
                                return err;
                            }
                        }
                        
                        if (name == "name")
                        {
                            testItemName = value;
                        }
                        else if (name == "className")
                        {
                            className = value;
 //                           err = oTestItemSetting.addTestItemClass(testItemName, className);
                        }
                    }
                    else if (name == "Property")
                    {
                        foreach (XmlAttribute attribute in childNode.Attributes)
                        {
                            name = attribute.Name;
                            value = attribute.Value;

                            err = AddTestItemProperty(testItemIndex, name, value, cardNumber);                        
                            if (err != Error.NoError)
                            {
                                return err;
                            }
                        }
                    }
                }
            }

            return Error.NoError;
        }

        public static void ExportProfile(string path, uint cardNumber)
        {
            List<eTestItemSetting> testItemSettings = oTestItemSetting.GetTestItem(cardNumber);

            XmlDocument doc = new XmlDocument();
            XmlElement profile = createElement(doc, "Profile", eProductSetting.ProfileName);
            doc.AppendChild(profile);

            XmlElement testItems = doc.CreateElement("TestItems");
            profile.AppendChild(testItems);

            foreach (eTestItemSetting setting in testItemSettings)
            {
                XmlElement testItem = doc.CreateElement(setting.Name);
                testItems.AppendChild(testItem);

                int testItemIndex = int.Parse(setting.TestItemIndex.ToString());

                testItem.AppendChild(createElement(doc, "name", setting.Name));
                testItem.AppendChild(createElement(doc, "className", dicClassName[setting.Name]));
                testItem.AppendChild(createElement(doc, "seqIndex", setting.SequenceIndex.ToString()));
                testItem.AppendChild(createElement(doc, "index", setting.TestItemIndex.ToString()));
                testItem.AppendChild(createElement(doc, "enable", Convert.ToInt32(setting.Enable).ToString()));
                testItem.AppendChild(createElement(doc, "reTest", Convert.ToInt32(setting.Retest).ToString()));
                testItem.AppendChild(createElement(doc, "cycle", setting.Cycle.ToString()));
                testItem.AppendChild(createElement(doc, "stopWhenFail", Convert.ToInt32(setting.SWF).ToString()));
                testItem.AppendChild(createElement(doc, "reTestNUpperLimit", setting.RetestNUpperLimit.ToString()));
                testItem.AppendChild(createElement(doc, "until", setting.Until.ToString()));
                testItem.AppendChild(createElement(doc, "cycleDelay", setting.CycleDelayMiniutes.ToString()));

                if (setting.AliasName != null)
                {
                    testItem.AppendChild(createElement(doc, "aliasName", setting.AliasName.ToString()));
                }

                DataTable testPropertys = GetTestItemProperty(testItemIndex, cardNumber);
                XmlElement info = createElement(doc, "Property", string.Empty);

                foreach (DataRow testProperty in testPropertys.Rows)
                {
                    info.SetAttribute(testProperty["name"].ToString(), testProperty["value"].ToString());
                }

                testItem.AppendChild(info);
            }

            doc.Save(path);
        }

        public static void ExportXMLFile(uint cardNumber)
        {
            DataTable testDetail = GetTestResultDetail(testItemManage[cardNumber].ResultIndex, cardNumber);
            
            XmlDocument doc = new XmlDocument();
            XmlElement testReport = createElement(doc, "TestReport", string.Empty);
            testReport.SetAttribute("Result", testDetail.Rows[0]["result"].ToString());
            doc.AppendChild(testReport);

            XmlElement productSetting = createElement(doc, "ProductSetting", string.Empty);
            testReport.AppendChild(productSetting);

            productSetting.AppendChild(createElement(doc, "ModelName", eProductSetting.ModelName));
            productSetting.AppendChild(createElement(doc, "PartNumber", eProductSetting.PartNumber));
            productSetting.AppendChild(createElement(doc, "TestDate_Start", testDetail.Rows[0]["startTime"].ToString()));
            productSetting.AppendChild(createElement(doc, "TestDate_Finish", testDetail.Rows[0]["endTime"].ToString()));
            productSetting.AppendChild(createElement(doc, "SerialNumber", eProductSetting.SN));
            productSetting.AppendChild(createElement(doc, "FPGAVersion", eProductSetting.FpgaVersion));
            productSetting.AppendChild(createElement(doc, "MCUVersion", eProductSetting.McuVersion));
            productSetting.AppendChild(createElement(doc, "FirmwareVersion", eProductSetting.FirmwareVersion));
            productSetting.AppendChild(createElement(doc, "QcProgramVersion", eProductSetting.QcProgramVersion));
            productSetting.AppendChild(createElement(doc, "MacAddress1", eProductSetting.MacID));
            productSetting.AppendChild(createElement(doc, "ProductDescription", eProductSetting.ProductDescription));
            productSetting.AppendChild(createElement(doc, "DriverVersion", eProductSetting.DriverVersion));
            productSetting.AppendChild(createElement(doc, "Enginner", eProductSetting.Engineer));

            string[] instruments = eProductSetting.TestInstrument.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < instruments.Length; i++)
            {
                productSetting.AppendChild(createElement(doc, "TestInstrument"+i.ToString(), instruments[i]));
            }

            productSetting.AppendChild(createElement(doc, "CardNumber", cardNumber.ToString()));

            //test item
            XmlElement testItemColl = doc.CreateElement("TestItemCollections");
            testReport.AppendChild(testItemColl);

            foreach (DataRow dr in testDetail.Rows)
            {
                //XmlElement testItem = doc.CreateElement("TestItem");
                XmlElement testItem = doc.CreateElement(dr["name"].ToString() + "_" + dr["testItemIndex"].ToString());
                testItemColl.AppendChild(testItem);

                int testItemIndex =int.Parse( dr["testItemIndex"].ToString());
                testItem.AppendChild(createElement(doc, "TestItemIndex", testItemIndex.ToString()));
                testItem.AppendChild(createElement(doc, "Name", dr["name"].ToString()));
                testItem.AppendChild(createElement(doc, "Result",dr["resultDetail"].ToString()));
                testItem.AppendChild(createElement(doc, "Value", dr["value"].ToString()));
                testItem.AppendChild(createElement(doc, "SPEC", dr["spec"].ToString()));
                testItem.AppendChild(createElement(doc, "Channel", dr["channel"].ToString()));
                testItem.AppendChild(createElement(doc, "Time", dr["time"].ToString()));
                testItem.AppendChild(createElement(doc, "Cycle", dr["cycle"].ToString()));
                testItem.AppendChild(createElement(doc, "ErrorCode", dr["errorCode"].ToString()));
                testItem.AppendChild(createElement(doc, "Description", dr["Description"].ToString()));

                DataTable testPropertys = GetTestItemProperty(testItemIndex, cardNumber);
                XmlElement info = createElement(doc, "Detail", string.Empty);                
                foreach (DataRow testProperty in testPropertys.Rows)
                {
                    info.SetAttribute(testProperty["name"].ToString(), testProperty["value"].ToString());
                }

                testItem.AppendChild(info);
            }


            doc.Save(eProductSetting.LogPath + "\\" + eProductSetting.SN + ".xml");

        }

        private static XmlElement createElement(XmlDocument doc, string element, string text)
        {
            XmlElement info = doc.CreateElement(element);
            if (text.Trim() != string.Empty)
            {
                info.InnerText = text;
            }

            return info;
        }

        public static DataTable GetTestResultDetail(long resultIndex, uint cardNumber)
        {
            return MvaDSManager.MainDB.getTestResultWithDetail(resultIndex);
        }

        public static uint GetTestItemCount(int profileIndex)
        {
            DataTable dt = MvaDSManager.MainDB.getTestItemCount(profileIndex);

            return uint.Parse(dt.Rows[0]["count"].ToString());
        }

        public static TestItem GetTestItem(int index, uint cardNumber)
        {
            return testItemManage[cardNumber].TestItemColls[index];
        }

        public static bool IsValidTestItemIndex(int index, uint cardNumber)
        {
            return testItemManage[cardNumber].TestItemColls.ContainsKey(index);
        }

        public static List<KeyValuePair<string, string>> GetSortedTestItemList()
        {
            if (oTestItemManage.dicClassName != null)
            {
                List<KeyValuePair<string, string>> testItemList = dicClassName.ToList();
                testItemList.Sort((x, y) => x.Value.CompareTo(y.Value));
                return testItemList;
            }
            return null;
        }
    }
}
