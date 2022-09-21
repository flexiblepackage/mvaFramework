using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

using MVAFW.Config;
using MVAFW.DB;
using MVAFW.Common.Entity;

namespace MVAFW.TestItemColls
{
    public class TestItem /* : CollectionBase, ICustomTypeDescriptor*/
    {
        #region Test Item Event
        public delegate void TestItemEventHandler(object sender, TestItem e);
        public event TestItemEventHandler TestItemEvent;

        protected virtual void OnTestItemEvent(TestItem e)
        {
            if (TestItemEvent != null)
            {
                Control target = TestItemEvent.Target as Control;
                if (target != null && target.InvokeRequired)
                {
                    target.Invoke(TestItemEvent, new object[] { this, e });
                }
                else
                {
                    TestItemEvent(this, e);
                }
            }
        }

        #endregion


        //#region Custom Property Implement
        ///// <summary>
        ///// Add CustomProperty to Collectionbase List
        ///// </summary>
        ///// <param name="Value"></param>
        //public void Add(CustomProperty Value)
        //{
        //    base.List.Add(Value);
        //}

        ///// <summary>
        ///// Remove item from List
        ///// </summary>
        ///// <param name="Name"></param>
        //public void Remove(string Name)
        //{
        //    foreach (CustomProperty prop in base.List)
        //    {
        //        if (prop.Name == Name)
        //        {
        //            base.List.Remove(prop);
        //            return;
        //        }
        //    }
        //}
        ///// <summary>
        ///// Indexer
        ///// </summary>
        //public CustomProperty this[int index]
        //{
        //    get
        //    {
        //        return (CustomProperty)base.List[index];
        //    }
        //    set
        //    {
        //        base.List[index] = (CustomProperty)value;
        //    }
        //}
        //#endregion

        //#region "TypeDescriptor Implementation"
        ///// <summary>
        ///// Get Class Name
        ///// </summary>
        ///// <returns>String</returns>
        //public String GetClassName()
        //{
        //    return TypeDescriptor.GetClassName(this, true);
        //}

        ///// <summary>
        ///// GetAttributes
        ///// </summary>
        ///// <returns>AttributeCollection</returns>
        //public AttributeCollection GetAttributes()
        //{
        //    return TypeDescriptor.GetAttributes(this, true);
        //}

        ///// <summary>
        ///// GetComponentName
        ///// </summary>
        ///// <returns>String</returns>
        //public String GetComponentName()
        //{
        //    return TypeDescriptor.GetComponentName(this, true);
        //}

        ///// <summary>
        ///// GetConverter
        ///// </summary>
        ///// <returns>TypeConverter</returns>
        //public TypeConverter GetConverter()
        //{
        //    return TypeDescriptor.GetConverter(this, true);
        //}

        ///// <summary>
        ///// GetDefaultEvent
        ///// </summary>
        ///// <returns>EventDescriptor</returns>
        //public EventDescriptor GetDefaultEvent()
        //{
        //    return TypeDescriptor.GetDefaultEvent(this, true);
        //}

        ///// <summary>
        ///// GetDefaultProperty
        ///// </summary>
        ///// <returns>PropertyDescriptor</returns>
        //public PropertyDescriptor GetDefaultProperty()
        //{
        //    return TypeDescriptor.GetDefaultProperty(this, true);
        //}

        ///// <summary>
        ///// GetEditor
        ///// </summary>
        ///// <param name="editorBaseType">editorBaseType</param>
        ///// <returns>object</returns>
        //public object GetEditor(Type editorBaseType)
        //{
        //    return TypeDescriptor.GetEditor(this, editorBaseType, true);
        //}

        //public EventDescriptorCollection GetEvents(Attribute[] attributes)
        //{
        //    return TypeDescriptor.GetEvents(this, attributes, true);
        //}

        //public EventDescriptorCollection GetEvents()
        //{
        //    return TypeDescriptor.GetEvents(this, true);
        //}

        //public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        //{
        //    PropertyDescriptorCollection oriPDC = TypeDescriptor.GetProperties(this);
        //    PropertyDescriptor[] newProps = new PropertyDescriptor[oriPDC.Count + this.Count];

        //    for (int i = 0; i < oriPDC.Count; i++)
        //    {
        //        if (oriPDC[i].Name == "Capacity" || oriPDC[i].Name == "Count") { continue; }
        //        newProps[i] = oriPDC[i];                
        //    }

        //    for (int i = 0; i < this.Count; i++)
        //    {
        //        CustomProperty prop = (CustomProperty)this[i];
        //        newProps[i + oriPDC.Count] = new CustomPropertyDescriptor(ref prop, attributes);
        //    }

        //    return new PropertyDescriptorCollection(newProps);
        //}

        //public PropertyDescriptorCollection GetProperties()
        //{
        //    return TypeDescriptor.GetProperties(this, true);
        //}

        //public object GetPropertyOwner(PropertyDescriptor pd)
        //{
        //    return this;
        //}
        //#endregion

        //private FailOperationType failOperation = FailOperationType.None;
        //[Category("Fail Operation")]
        //public FailOperationType FailOperation 
        //{ 
        //    get
        //    {
        //        return failOperation;
        //    }
        //    set
        //    {
        //        if (value == FailOperationType.GotoTestItemIndex)
        //        {
        //            this.Add(new CustomProperty("GotoTestItemIndex", 0, typeof(int), false, true, "Fail Operation"));
        //        }
        //        else if (value == FailOperationType.TestTestItemIndex)
        //        {
        //            this.Add(new CustomProperty("TestTestItemIndex", 0, typeof(int), false, true, "Fail Operation"));
        //        }

        //        this.Add(new CustomProperty("TimesLimited", 0, typeof(int), false, true, "Fail Operation"));

        //        failOperation = value;
        //    }
        //}

        //private bool popUpSelectCardNumber = false;
        //[Category("Basic Setting"), Description("Select Card Number will affect below identical test item automatically")]
        //public bool PopUpSelectCardNumber
        //{
        //    get
        //    {
        //        return popUpSelectCardNumber;
        //    }
        //    set
        //    {
        //        popUpSelectCardNumber = value;
        //    }
        //}

        //internal bool cardNumberSet =false;

        private TurnOffType turnOfftype = TurnOffType.AutoShoutDown;
        [Category("Basic Setting")]
        public TurnOffType TurnOfftype
        {
            get
            {
                return turnOfftype;
            }
            set
            {
                turnOfftype = value;
            }
        }

        [Category("Basic Setting")]
        public bool Restart { get; set; }

        [Category("Basic Setting")]
        public bool ForceRegister { get; set; }

        private int cycle;
        [Browsable(false)]
        public int Cycle
        {
            get
            {
                return cycle;
            }
            set
            {
                cycle = value;
            }
        }

        [Browsable(false)]
        internal ushort CardType { get; set; }

        

        [Category("Basic Setting")]
        public int MaxChannelNumber { get; protected set; }

        [Category("Basic Setting"), Description("Should the same as the test item cycle"), ReadOnly(true)]
        public uint CycleSet { get; set; }

        private bool multiChannel;
        [Category("Basic Setting")]
        public bool MultiChannel
        {
            get
            {
                return multiChannel;
            }
            protected set
            {
                multiChannel = value;
            }
        }

        private string spec;
        [Category("Value Compare")]
        public string Spec
        {
            get { return spec; }
            set { spec = value; }
        }

        private bool independentThread = false;
        [Category("Advance Setting"), Description("Set to true it will be the Independent Thread to prevent lock at this test item.")]
        public bool IndependentThread
        {
            get
            {
                return independentThread;
            }
            set
            {
                independentThread = value;
            }
        }

        private int index;
        [Category("Basic Setting"), Description("Test item unique index")]
        public int Index
        {
            get { return index; }
            internal set
            {
                if (value < 0)
                {
                    throw new Exception("TestItemIndex could not less than zero");
                }
                else
                {
                    index = value;
                }
            }
        }

        private string name;
        [Category("Basic Setting"), Description("Test Item Name")]
        public string Name
        {
            get { return name; }
            internal set
            {
                if (value.Trim() == String.Empty)
                {
                    throw new Exception("TestItemName could not only contain empty string");
                }
                name = value;
            }
        }

        [Category("Basic Setting"), Description("Base Description used to dynamic generate the Description, if don't need dynamic description, ignore it")]
        public string DescriptionBase { get; set; }

        [Category("Basic Setting"), Description("Describe the test item")]
        public string Description { get; set; }

        public List<string> DescriptionByChannel;

       [Category("Basic Setting"), Description("For Fixture use")]
        public bool AutoMode { get; set; }

        //private bool gpibPopUpSelectCardNumber = false;
        //[Category("GPIB Setting"), Description("Enable GPIB card number pop up")]
        //public virtual bool GPIBPopUpSelectCardNumber 
        //{ 
        //    get
        //    {
        //        return gpibPopUpSelectCardNumber;
        //    }
        //    set
        //    {
        //        gpibPopUpSelectCardNumber = value;
        //    }
        //}

        //internal bool gpibCardNumberSet = false;

        private CompareType compareType = CompareType.LessThanPercent;
        [Category("Basic Setting"), Description("Used to generate the Test Result")]
        public CompareType CompareType
        {
            get
            {
                return compareType;
            }
            set
            {
                compareType = value;

                //if (value == CompareType.equalVersion)
                //{
                //    setPropertyToBrowsable(new string[] { "Values" }, false);
                //}
            }
        }

        [Category("GPIB Setting"), Description("GPIB card number")]
        public virtual ushort GPIBCardNumber { get; set; }

        [Category("Value Compare")]
        public bool SkipBaseChannel { get; set; }

        [Category("Value Compare")]
        public int BaseChannel { get; set; }

        [Category("Value Compare")]
        public double FSR { get; set; }

        private void setPropertyToBrowsable(string[] propertyName, bool browsable)
        {
            foreach(string name in propertyName)
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(this.GetType())[name];
                BrowsableAttribute attrib = (BrowsableAttribute)descriptor.Attributes[typeof(BrowsableAttribute)];
                FieldInfo isBrowsable = attrib.GetType().GetField("browsable",BindingFlags.Instance|BindingFlags.NonPublic);
                isBrowsable.SetValue(attrib, browsable);
                descriptor.SetValue(attrib, browsable);
            }
        }


        private string errCode;
        [Category("Basic Setting"), Description("Error Code")]
        public string ErrCode
        {
            get
            {
                return errCode;
            }
            set
            {
                errCode = value;
            }
        }

        private double[] percentage;
        [Category("Test Result"), Description("The percentage to display the pass fail bar")]
        public double[] Percentage
        {
            get
            {
                return percentage;
            }
            protected set
            {
                percentage = value;
            }
        }

        private double[] realPercentage;
        [Category("Test Result"), Description("The percentage of base value")]
        public double[] RealPercentage
        {
            get
            {
                return realPercentage;
            }
            protected set
            {
                realPercentage = value;
            }
        }

        private string[] values ;
        [Category("Value Compare"), Description("Used to generate the result"),Browsable(true)]
        public string[] Values
        {
            get
            {
                return values;
            }
            protected set
            {
                values = value;
            }
        }

        private string[] baseValue;
        [Category("Value Compare"), Description("The base value to be compared"),Browsable(true)]
        public string[] BaseValue
        {
            get
            {
                return baseValue;
            }
            set
            {
                baseValue = value;
            }
        }

        [Category("Value Compare"), Description("If compare value use absolute value")]
        public bool AbsoluteValue { get; set; }

        private short cardNumber;
        [Category("Basic Setting"), Description("The device identifier to be tested")]
        public short CardNumber
        {
            get
            {
                return cardNumber;
            }
            set
            {
                cardNumber = value;
            }
        }

        private TestResult[] results;
        [Category("Test Result"), Description("Test result of each channel"), RefreshProperties(RefreshProperties.None)]
        public TestResult[] Results
        {
            get
            {
                return results;
            }
            protected set
            {
                results = value;
            }
        }

        private TestResult finalResult;
        [Category("Test Result"), Description("Test result of test item")]
        public TestResult FinalResult
        {
            get
            {
                return finalResult;
            }
            protected set
            {
                finalResult = value;
            }
        }

        [Browsable(false)]
        public bool IsChannelGainQueueEnable { get; set; }

        private ushort[] channels = new ushort[1] { ushort.MaxValue };
        [Browsable(false)]
        public ushort[] Channels
        {
            get
            {
                return channels;
            }
            protected set
            {
                channels = value;
            }
        }

        private ushort channelNumbers = 1;
        [Category("Basic Setting"), Description("The device identifier to be tested")]
        virtual public ushort ChannelNumbers
        {
            get
            {
                return channelNumbers;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("ChannelNumbers could not less than 1!");
                }
                else if(value>1)
                {
                    this.MultiChannel = true;
                }

                channelNumbers = value;

                initialValueSizes(Math.Max(value, SpecificChannel + 1));
            }
        }

        private short channelIndex = -1;
        [Browsable(false)]
        public  short ChannelIndex
        {
            get
            {
                return channelIndex;
            }
            protected set
            {
                channelIndex = value;
            }
        }

        private short specificChannel = -1;
        [Category("Basic Setting"), Description("The device identifier to be tested")]
        virtual public short SpecificChannel
        {
            get
            {
                return specificChannel;
            }
            set
            {
                if (value < -1)
                {
                    throw new ArgumentOutOfRangeException("SpecificChannel could only set to -1 if no need!");
                }
                else
                {
                    specificChannel = value;

                    if (value >= channelNumbers)
                    {
                        initialValueSizes(Math.Max(ChannelNumbers, value + 1));
                    }
                    else
                    {
                        initialValueSizes(ChannelNumbers);
                    }

                    int rowCount;
                    if (value == -1)
                    {
                        this.MultiChannel = true;
                        MvaDSManager.MainDB.updateTestItemProperty(this.Index, "MultiChannel", "True", eProductSetting.ModelName, out rowCount, eProductSetting.CardNumber);
                    }
                    else
                    {
                        this.MultiChannel = false;
                        MvaDSManager.MainDB.updateTestItemProperty(this.Index, "MultiChannel", "False", eProductSetting.ModelName, out rowCount, eProductSetting.CardNumber);
                    }
                }
            }
        }

        private void initialValueSizes(int channels)
        {
            this.Values = new string[channels];
            this.Results = new TestResult[channels];
            this.Percentage = new double[channels];
            this.RealPercentage = new double[channels];
            this.BaseValue = new string[channels];

            if (this.Channels.Length == 1 && this.Channels[0] == ushort.MaxValue)
            {
                this.Channels = new ushort[channels];
            }
            else
            {
                bool noNeedInitial = false;
                for(int channel =0; channel<this.Channels.Length;channel++)
                {
                    if(this.Channels[channel]!=0)
                    {
                        noNeedInitial = true;
                        break;
                    }
                }

                if(noNeedInitial==false)
                {
                    this.Channels = new ushort[channels];
                }
            }
        }

        public virtual void doTest()
        {
            cycle++;

            if (this.DescriptionBase != null && this.Description.Trim() == string.Empty)
            {
                this.Description = this.DescriptionBase;
            }

            //if (this.popUpSelectCardNumber == true)
            //{
            //    MVAFW.SettingForm.SelectCardNumber fm = new SettingForm.SelectCardNumber(this, false);
            //    fm.ShowDialog();
            //}

            //if (this.gpibPopUpSelectCardNumber == true)
            //{
            //    MVAFW.SettingForm.SelectCardNumber fm = new SettingForm.SelectCardNumber(this, true);
            //    fm.ShowDialog();
            //}
        }

        public virtual void AddDescription()
        {

        }

        public virtual string ToChString(int channel)
        {
            return string.Empty;
        }

        public virtual short ToChShort(short channel)
        {
            return channel;
        }

        private void lessThanPercent(int channelNumber)
        {
            if (Values[channelNumber] == null || BaseValue[channelNumber]==null)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("Values[" + channelNumber + "] or BaseValue[" + channelNumber + "] not assigned value yet");
            }

            if (BaseValue[channelNumber] == null)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("BaseValue[" + channelNumber + "] not assigned value yet");
            }

            string[] values = Values[channelNumber].Split(',');
            string[] baseValues = BaseValue[channelNumber].Split(',');
            double spec = double.Parse(this.Spec);

            results[channelNumber] = TestResult.PASS;
            percentage[channelNumber] = 100;

            if (values.Length != baseValues.Length)
            {
                MessageBox.Show("Value element number is not equal to baseValue element number!");
                results[channelNumber] = TestResult.UNKNOWN;
                percentage[channelNumber] = 0;
                finalResult = TestResult.UNKNOWN;
                return;
            }

            for (int i = 0; i < values.Length; i++)
            {
                double dValue, dBaseValue;

                if (values[i] == null || double.TryParse(baseValues[i], out dBaseValue) == false)
                {
                    throw new ArgumentOutOfRangeException("Please check the data count");
                }

                bool PharseResult = double.TryParse(values[i], out dValue);

                if (results[channelNumber] != TestResult.FAIL)
                {
                    if ((dValue - dBaseValue) == 0)
                    {
                        percentage[channelNumber] = 100;
                        realPercentage[channelNumber] = 0;
                    }
                    else
                    {
                        percentage[channelNumber] = (spec / Math.Abs(((dValue - dBaseValue) / dBaseValue) * 100)) * 50;
                        realPercentage[channelNumber] = ((dValue - dBaseValue) / dBaseValue) * 100;
                    }

                    if (percentage[channelNumber] < 50 || PharseResult == false)
                    {
                        results[channelNumber] = TestResult.FAIL;
                        finalResult = TestResult.FAIL;
                    }
                }
            }
        }

        private void noCompare(int channelNumber)
        {
            results[channelNumber] = TestResult.NoCompare;
            finalResult = TestResult.NoCompare;
        }

        private void between(int channelNumber)
        {
            if (Values[channelNumber] == null)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("Values[" + channelNumber + "] not assigned value yet");
            }

            if (BaseValue[channelNumber] == null)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("BaseValue[" + channelNumber + "] not assigned value yet");
            }

            string[] values = Values[channelNumber].Split(',');
            string[] specValues = Spec.Split(',');
            string[] baseValues=BaseValue[channelNumber].Split(',');

            results[channelNumber] = TestResult.PASS;
            percentage[channelNumber] = 100;

            if (values.Length != specValues.Length)
            {
                MessageBox.Show("Value element number is not equal to baseValue element number!");
                results[channelNumber] = TestResult.UNKNOWN;
                percentage[channelNumber] = 0;
                finalResult = TestResult.UNKNOWN;
                return;
            }

            for (int i = 0; i < values.Length; i++)
            {
                double dValue, dSpecValue, dBaseValue;

                if (values[i] == null || double.TryParse(specValues[i], out dSpecValue) == false || double.TryParse(baseValues[i], out dBaseValue) == false)
                {
                    throw new ArgumentOutOfRangeException("Please check the value format");
                }
              
                if (double.TryParse(values[i], out dValue) == false || Math.Abs(dValue - dBaseValue) > dSpecValue)
                {
                    percentage[channelNumber] = 0;
                    results[channelNumber] = TestResult.FAIL;
                    finalResult = TestResult.FAIL;
                }
                
            }
        }

        private void lessThanSpec(int channelNumber)
        {
            if (Values[channelNumber] == null)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("Values[" + channelNumber + "] not assigned value yet");
            }
            else if (Spec == null || Spec.Trim() == string.Empty)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("Spec not assigned value yet");
            }

            string[] values = Values[channelNumber].Split(',');
            string[] specValues = Spec.Split(',');

            results[channelNumber] = TestResult.PASS;
            percentage[channelNumber] = 100;

            if (values.Length != specValues.Length)
            {
                MessageBox.Show("Value element number is not equal to baseValue element number!");
                results[channelNumber] = TestResult.UNKNOWN;
                percentage[channelNumber] = 0;
                finalResult = TestResult.UNKNOWN;
                return;
            }

            for (int i = 0; i < values.Length; i++)
            {
                double dValue, dSpecValue;

                if (values[i] == null || double.TryParse(specValues[i], out dSpecValue) == false)
                {
                    throw new ArgumentOutOfRangeException("Please check the data count");
                }

                bool PharseResult = double.TryParse(values[i], out dValue);
                if (this.AbsoluteValue == true)
                {
                    dValue = Math.Abs(dValue);
                    dSpecValue = Math.Abs(dSpecValue);
                }

                if (dValue > dSpecValue || PharseResult == false)
                {
                    percentage[channelNumber] = 0;
                    results[channelNumber] = TestResult.FAIL;
                    finalResult = TestResult.FAIL;
                }
            }
        }

        private void greaterThanSpec(int channelNumber)
        {
            if (Values[channelNumber] == null)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("Values[" + channelNumber + "] not assigned value yet");
            }
            else if (Spec == null || Spec.Trim() == string.Empty)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("Spec not assigned value yet");
            }

            string[] values = Values[channelNumber].Split(',');
            string[] specValues = Spec.Split(',');

            results[channelNumber] = TestResult.PASS;
            percentage[channelNumber] = 100;

            if (values.Length != specValues.Length)
            {
                MessageBox.Show("Value element number is not equal to baseValue element number!");
                results[channelNumber] = TestResult.UNKNOWN;
                percentage[channelNumber] = 0;
                finalResult = TestResult.UNKNOWN;
                return;
            }

            for (int i = 0; i < values.Length; i++)
            {
                double dValue, dSpecValue;

                if (values[i] == null || double.TryParse(specValues[i], out dSpecValue) == false)
                {
                    throw new ArgumentOutOfRangeException("Check Values count");
                }

                bool PharseResult = double.TryParse(values[i], out dValue);

                if (this.AbsoluteValue == true)
                {
                    dValue = Math.Abs(dValue);
                    dSpecValue = Math.Abs(dSpecValue);
                }

                if (dValue < dSpecValue || PharseResult == false)
                {
                    percentage[channelNumber] = 0;
                    results[channelNumber] = TestResult.FAIL;
                    finalResult = TestResult.FAIL;
                }
            }
        }

        private void equalValue(int channelNumber)
        {
            if (Values[channelNumber] == null)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("Values[" + channelNumber + "] not assigned value yet");
            }

            if (BaseValue[channelNumber] == null)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("BaseValue[" + channelNumber + "] not assigned value yet");
            }
             
            string[] values = Values[channelNumber].Split(',');
            string[] baseValue = BaseValue[channelNumber].Split(',');

            results[channelNumber] = TestResult.PASS;
            percentage[channelNumber] = 100;

            if (values.Length != baseValue.Length)
            {
                MessageBox.Show("Value element number is not equal to baseValue element number!");
                results[channelNumber] = TestResult.UNKNOWN;
                percentage[channelNumber] = 0;
                finalResult = TestResult.UNKNOWN;
                return;
            }

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != baseValue[i])
                {
                    percentage[channelNumber] = 0;
                    results[channelNumber] = TestResult.FAIL;
                    finalResult = TestResult.FAIL;
                }
            }
        }

        public virtual void getResult(bool writeToLog)
        {
            finalResult = TestResult.PASS;

            for (int channel = 0; channel < Math.Max(ChannelNumbers, specificChannel+1); channel++)
            {
                if (multiChannel == false && channel != specificChannel)
                {
                    continue;
                }

                if (SkipBaseChannel == true && BaseChannel == channel)
                {
                    results[channel] = TestResult.PASS;
                    percentage[channel] = 100;
                    continue;
                }

                switch (compareType)
                {
                    case CompareType.LessThanPercent:
                        lessThanPercent(channel);
                        break;
                    case CompareType.LessThanPercentFSR:
                        lessThanPercentFSR(channel);
                        break;
                    case CompareType.EqualValue:
                        equalValue(channel);
                        break;
                    case CompareType.GreaterThanSpec:
                        greaterThanSpec(channel);
                        break;
                    case CompareType.LessThanSpec:
                        lessThanSpec(channel);
                        break;
                    case CompareType.Between:
                        between(channel);
                        break;
                    case CompareType.NoCompare:
                        noCompare(channel);
                        break;
                    //case CompareType.CheckValueGreaterThan:
                    //    checkvalue(channel, 1);
                    //    break;
                    //case CompareType.CheckValueLessThan:
                    //    checkvalue(channel, 0);
                    //    break;
                    default:
                        MessageBox.Show("No matched CompareType!");
                        break;
                }
            }
        }

        private void lessThanPercentFSR(int channelNumber)
        {
            if (Values[channelNumber] == null || BaseValue[channelNumber] == null)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("Values[" + channelNumber + "] or BaseValue[" + channelNumber + "] not assigned value yet");
            }

            if (BaseValue[channelNumber] == null)
            {
                results[channelNumber] = TestResult.UNKNOWN;
                finalResult = TestResult.FAIL;
                throw new Exception("BaseValue[" + channelNumber + "] not assigned value yet");
            }

            string[] values = Values[channelNumber].Split(',');
            string[] baseValues = BaseValue[channelNumber].Split(',');
            double spec = double.Parse(this.Spec);

            results[channelNumber] = TestResult.PASS;
            percentage[channelNumber] = 100;

            if (values.Length != baseValues.Length)
            {
                MessageBox.Show("Value element number is not equal to baseValue element number!");
                results[channelNumber] = TestResult.UNKNOWN;
                percentage[channelNumber] = 0;
                finalResult = TestResult.UNKNOWN;
                return;
            }

            for (int i = 0; i < values.Length; i++)
            {
                double dValue, dBaseValue;

                if (values[i] == null || double.TryParse(baseValues[i], out dBaseValue) == false)
                {
                    throw new ArgumentOutOfRangeException("Please check the data count");
                }

                bool PharseResult = double.TryParse(values[i], out dValue);

                //if (finalResult != TestResult.FAIL)
                //{
                if ((dValue - dBaseValue) == 0)
                    {
                        percentage[channelNumber] = 100;
                        realPercentage[channelNumber] = 0;
                    }
                    else
                    {
                        percentage[channelNumber] = (spec / Math.Abs(((dValue - dBaseValue) / FSR) * 100)) * 50;
                        realPercentage[channelNumber] = ((dValue - dBaseValue) / FSR) * 100;
                    }

                    if (percentage[channelNumber] < 50 || PharseResult == false)
                    {
                        results[channelNumber] = TestResult.FAIL;
                        finalResult = TestResult.FAIL;
                    }
                //}
            }
        }

        public virtual void OnError(short err)
        {
            throw new Exception("Error number = " + err.ToString());
        }

        public void MessageDisplay(string message)
        {
            MessageBox.Show(message);
        }

        public TestItem()
        {
            this.ChannelNumbers = 1;
            this.compareType = CompareType.NoCompare;
            this.DescriptionByChannel = new List<string>();
        }
    }

    ///// <summary>
    ///// Custom property class 
    ///// </summary>
    //public class CustomProperty
    //{
    //    private string sName = string.Empty;
    //    private bool bReadOnly = false;
    //    private bool bVisible = true;
    //    private object objValue = null;
    //    private string category = string.Empty;

    //    public CustomProperty(string sName, object value, Type type, bool bReadOnly, bool bVisible, string category)
    //    {
    //        this.sName = sName;
    //        this.objValue = value;
    //        this.type = type;
    //        this.bReadOnly = bReadOnly;
    //        this.bVisible = bVisible;
    //        this.category = category;
    //    }

    //    private Type type;
    //    public Type Type
    //    {
    //        get { return type; }
    //    }

    //    public string Category
    //    {
    //        get { return category; }
    //    }

    //    public bool ReadOnly
    //    {
    //        get
    //        {
    //            return bReadOnly;
    //        }
    //    }

    //    public string Name
    //    {
    //        get
    //        {
    //            return sName;
    //        }
    //    }

    //    public bool Visible
    //    {
    //        get
    //        {
    //            return bVisible;
    //        }
    //    }

    //    public object Value
    //    {
    //        get
    //        {
    //            return objValue;
    //        }
    //        set
    //        {
    //            objValue = value;
    //        }
    //    }

    //}


    ///// <summary>
    ///// Custom PropertyDescriptor
    ///// </summary>
    //public class CustomPropertyDescriptor : PropertyDescriptor
    //{
    //    CustomProperty m_Property;
    //    public CustomPropertyDescriptor(ref CustomProperty myProperty, Attribute[] attrs)
    //        : base(myProperty.Name, attrs)
    //    {
    //        m_Property = myProperty;
    //    }

    //    #region PropertyDescriptor specific

    //    public override bool CanResetValue(object component)
    //    {
    //        return false;
    //    }

    //    public override Type ComponentType
    //    {
    //        get { return null; }
    //    }

    //    public override object GetValue(object component)
    //    {
    //        return m_Property.Value;
    //    }

    //    public override string Description
    //    {
    //        get { return m_Property.Name; }
    //    }

    //    public override string Category
    //    {
    //        get { return m_Property.Category; }
    //    }

    //    public override string DisplayName
    //    {
    //        get { return m_Property.Name; }
    //    }

    //    public override bool IsReadOnly
    //    {
    //        get { return m_Property.ReadOnly; }
    //    }

    //    public override void ResetValue(object component)
    //    {
    //        //Have to implement
    //    }

    //    public override bool ShouldSerializeValue(object component)
    //    {
    //        return false;
    //    }

    //    public override void SetValue(object component, object value)
    //    {
    //        m_Property.Value = value;
    //    }

    //    public override Type PropertyType
    //    {
    //        get { return m_Property.Type; }
    //    }

    //    #endregion


    //}
}
