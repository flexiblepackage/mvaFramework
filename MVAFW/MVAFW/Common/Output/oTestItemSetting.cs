using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;

using MVAFW.DB;
using MVAFW.Config;
using MVAFW.Common.Entity;
using MVAFW.TestItemColls;
using MVAFW.SettingForm;

namespace MVAFW.Common.Output
{
    public class oTestItemSetting
    {
        public static Error ResetBaseValue(TestItem testItem, uint cardNumber)
        {
            Error err = Error.NoError;

            MessageBox.Show("You might re set the Base Value if you used");

            //give default value to DB to prevent no re set Base Value by user
            StringBuilder pValues = new StringBuilder();
            for (int i = 0; i < Math.Max(testItem.ChannelNumbers, testItem.SpecificChannel + 1); i++)
            {
                pValues.Append("0");
                pValues.Append(":");
            }
            string pValue = pValues.ToString().Substring(0, pValues.Length - 1);
            string pName = "BaseValue";
            int rowCount;
            err = MvaDSManager.MainDB.updateTestItemProperty(testItem.Index, pName, pValue, eProductSetting.ModelName, out rowCount, cardNumber);
            if (err != Error.NoError) { return Error.DBError; }

            return err;
        }

        public static Error UpdateTestItemProperty(TestItem testItem, PropertyValueChangedEventArgs e, uint cardNumber)
        {
            Error err = Error.NoError;
            int rowCount = 0;
            string pName = e.ChangedItem.Label;
            string pValue = e.ChangedItem.Value.ToString();

            if ((e.OldValue==null || pValue != e.OldValue.ToString()) && !e.ChangedItem.PropertyDescriptor.IsReadOnly)
            {
                if (e.ChangedItem.Parent != null && e.ChangedItem.Parent.PropertyDescriptor != null && e.ChangedItem.Parent.PropertyDescriptor.PropertyType.Name == "String[]")
                {
                    StringBuilder pValues = new StringBuilder();
                    for (int i = 0; i < Math.Max(testItem.ChannelNumbers, testItem.SpecificChannel + 1); i++)
                    {
                        pValues.Append(((string[])e.ChangedItem.Parent.PropertyDescriptor.GetValue(testItem))[i]);
                        pValues.Append(":");
                    }
                    pValue = pValues.ToString().Substring(0, pValues.Length - 1);
                    pName = e.ChangedItem.Parent.Label;
                }
                err = MvaDSManager.MainDB.updateTestItemProperty(testItem.Index, pName, pValue, eProductSetting.ModelName, out rowCount, cardNumber);

                if (err != Error.NoError && err!=Error.NoData) { return Error.DBError; }

                //base value should be re assign because of the array has already re new 
                if (pName == "ChannelNumbers" || pName=="SpecificChannel")
                {                    
                    //give default value to DB to prevent no re set Base Value by user
                    err = ResetBaseValue(testItem, cardNumber);
                    if (err != Error.NoError) { return Error.DBError; }
                }

                //new testitem initial
                if (rowCount == 0)
                {
                    err = MvaDSManager.MainDB.addTestItemProperty(testItem.Index, pName, pValue, cardNumber);
                }
            }

            return err;
        }

        public static Error UpdateTestItemAliasName(int testItemIndex, string aliasName)
        {
            return MvaDSManager.MainDB.updateAliasName(testItemIndex, aliasName);
        }

        public static Error UpdateTestItemSequence(string columnName, uint[] value, uint[] testItemIndex, uint cardNumber)
        {
            return MvaDBDataFetcher.MvaDB.ExecTransToUpdateSeq(columnName, value, testItemIndex, cardNumber);
        }

        public static Error UpdateTestItem(string columnName, string value, int index, uint cardNumber)
        {
            return MvaDSManager.MainDB.updateTestItemSetting(columnName, value, index, cardNumber);
        }

        public static Error UpdateTestItemPropertyDB(int testItemIndex, string pName, string pValue, string modelName, uint cardNumber)
        {
            int count=0;
            Error err = MvaDSManager.MainDB.updateTestItemProperty(testItemIndex, pName, pValue, modelName, out count, cardNumber);
            if (err != Error.NoError)
            {
                return err;
            }
            if (count <= 0)
            {
                return Error.DBError;
            }

            return Error.NoError;
        }

        public static Error UpdateTestItemModelName(string pName, string pValue)
        {
            Error err = MvaDSManager.MainDB.updateTestItemModelName(pName, pValue);
            if (err != Error.NoError)
            {
                return err;
            }
           
            return Error.NoError;
        }

        public static Error UpdateTestItemMulti(string columnName, string value, string[] testItemIndex, uint cardNumber)
        {
            return MvaDSManager.MainDB.ExecToUpdateMultiTI(columnName, value, testItemIndex, cardNumber);
        }

        public static Error AddTestItemClass(string name, string classFullName)
        {
            int count;
            Error err;
            err = MvaDSManager.MainDB.addTestItemClass(name, classFullName, out count);
            if (err != Error.NoError)
            {
                return err;
            }

            oTestItemManage.AddTestItem(name, classFullName);

            return Error.NoError;
        }

        public static Error DelTestItemClass(string name)
        {
            int count;
            Error err= MvaDSManager.MainDB.delTestItemClass(name, out count);

            if (err != Error.NoError)
            {
                return err;
            }
            else if (count == 0)
            {
                return Error.NoData;
            }

            oTestItemManage.DelTestItem(name);

            return Error.NoError;
        }

        public static List<eTestItemClass> GetTestItemClass()
        {
            //from old db data.
            List<eTestItemClass> testItemClasses = new List<eTestItemClass>();
            DataTable dt = MvaDSManager.MainDB.getAllTestItemClass();

            foreach (DataRow dr in dt.Rows)
            {
                eTestItemClass t = new eTestItemClass();
                t.Name = dr["name"].ToString();
                t.FullClassName = dr["classFullName"].ToString();

                testItemClasses.Add(t);
            }

            //from drag and drop test item
            foreach (eTestItemClass t in TestItemSelection.GetAllTestItemClasses())
            {
                testItemClasses.Add(t);
            }

            return testItemClasses;
        }

        public static eTestItemSetting GetSingleTestItem(uint cardNumber, int testItemIndex)
        {
            DataTable dt = MvaDSManager.MainDB.getSingleTestItem(cardNumber, testItemIndex);

            eTestItemSetting testItemSetting = new eTestItemSetting();

            DataRow dr = dt.Rows[0];
            testItemSetting.Enable = bool.Parse(dr["enable"].ToString());
            testItemSetting.SequenceIndex = uint.Parse(dr["seqIndex"].ToString());
            testItemSetting.TestItemIndex = int.Parse(dr["index"].ToString());
            testItemSetting.Retest = bool.Parse(dr["reTest"].ToString());
            testItemSetting.Cycle = uint.Parse(dr["cycle"].ToString());
            testItemSetting.Name = dr["name"].ToString();
            testItemSetting.SWF = bool.Parse(dr["stopWhenFail"].ToString());
            testItemSetting.RetestNUpperLimit = uint.Parse(dr["retestNUpperLimit"].ToString());
            testItemSetting.Until = dr["until"].ToString();
            testItemSetting.CycleDelayMiniutes = double.Parse(dr["cycleDelay"].ToString());
            //for backward compatibility
            try
            {
                testItemSetting.AliasName = dr["aliasName"].ToString();
            }
            catch { }

            return testItemSetting;
        }

        public static List<eTestItemSetting> GetTestItem(uint cardNumber)
        {
            List<eTestItemSetting> testItemSettings = new List<eTestItemSetting>();

            DataTable dt = MvaDSManager.MainDB.getTestItem(cardNumber);

            foreach (DataRow dr in dt.Rows)
            {
                eTestItemSetting testItemSetting = new eTestItemSetting();

                testItemSetting.Enable = bool.Parse(dr["enable"].ToString());
                //testItemSetting.SequenceIndex = uint.Parse(dr["seqIndex"].ToString());
                testItemSetting.SequenceIndex = uint.Parse(dr["seqIndex"].ToString())+1; 
                testItemSetting.TestItemIndex = int.Parse(dr["index"].ToString());
                testItemSetting.Retest = bool.Parse(dr["reTest"].ToString());
                testItemSetting.Cycle = uint.Parse(dr["cycle"].ToString());

                //for backward compatibility
                try
                {
                    if (dr["aliasName"].ToString() != string.Empty)
                    {
                        //testItemSetting.Name = "(" + dr["aliasName"].ToString() + ")" + dr["name"].ToString();
                        testItemSetting.AliasName = dr["aliasName"].ToString();
                    }
                    else
                    {
                        //testItemSetting.Name = dr["name"].ToString();
                    }
                }
                catch(ArgumentException)
                {
                    //testItemSetting.Name = dr["name"].ToString();
                }

                testItemSetting.Name = dr["name"].ToString();
                testItemSetting.SWF = bool.Parse(dr["stopWhenFail"].ToString());
                testItemSetting.RetestNUpperLimit = uint.Parse(dr["retestNUpperLimit"].ToString());
                testItemSetting.Until = dr["until"].ToString();
                testItemSetting.CycleDelayMiniutes = double.Parse(dr["cycleDelay"].ToString());

                testItemSettings.Add(testItemSetting);
            }

            return testItemSettings;
        }

        public static Error UpdateTestItemColumn(string columnName, string value, int index, uint cardNumber)
        {
            return MvaDSManager.MainDB.updateTestItemSetting(columnName, value, index, cardNumber);
        }
    }
}
