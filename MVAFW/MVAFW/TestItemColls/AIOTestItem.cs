using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;

using MVAFW.Config;
using MVAFW.DB;
using MVAFW.Common.Entity;

namespace MVAFW.TestItemColls
{
    public class AIOTestItem:TestItem
    {
        private string channelSettings = "Not set yet";
        [Editor(typeof(MVAFW.SettingForm.ChannelSettingEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Category("Basic Setting")]
        public virtual string ChannelSettings
        {
            set
            {
                channelSettings = value;
            }
            get
            {
                return channelSettings;
            }
        }

        private ushort channelNumbers = 1;
        [Category("Basic Setting"), Description("Channel Numbers to be tested"), ReadOnly(true)]
        public override ushort ChannelNumbers
        {
            get
            {
                return channelNumbers;
            }
            set
            {
                base.ChannelNumbers = value;
                channelNumbers = value;

                if (IsChannelGainQueueEnable == false)
                {
                    this.Channels = new ushort[value];
                    for (ushort channel = 0; channel < this.Channels.Length; channel++)
                    {
                        this.Channels[channel] = channel;
                    }
                }
            }
        }

        private ushort[] adRange = new ushort[1] { ushort.MaxValue };
        [Browsable(false)]
        public ushort[] AdRanges
        {
            get
            {
                return adRange;
            }
            protected set
            {
                adRange = value;
            }
        }

        [Browsable(false)]
        public byte Resolution 
        { get; protected set; }
        
        private bool simultaneous = false;
        [Category("Basic Setting")]
        public bool Simultaneous
        {
            get
            {
                return simultaneous;
            }
            set
            {
                simultaneous = value;
            }
        }

        private Dictionary<string, string> dicChannelGainQueue = new Dictionary<string,string>();
        [Browsable(false)]
        public Dictionary<string, string> DicChannelGainQueue
        {
            get
            {
                return dicChannelGainQueue;
            }
            protected set
            {
                dicChannelGainQueue = value;
            }
        }

        [Browsable(false)]
        public string ChannelGainQueueString { get; set; }

        private int pollingInterval = 100;
        [Browsable(false), Description("Polling Interval(ms)"), DAQFuncAttribute(DAQFunction.PollingInterval)]
        public int PollingInterval
        {
            get
            {
                return pollingInterval;
            }
            set
            {
                pollingInterval = value;
            }
        }

        private ushort[][] extendConfigs;
        [Browsable(false)]
        public ushort[][] ExtendConfigs
        {
            get
            {
                return extendConfigs;
            }
            protected set
            {
                extendConfigs = value;
            }
        } //used for the flexibility like the USB-2405 1210, the invalid config way!!!

        public override void doTest()
        {
            base.doTest();
        }

        public override string ToChString(int channel)
        {
            if (SpecificChannel != -1)
            {
                return "CH" + SpecificChannel.ToString();                
            }
            else
            {
                return "CH" + Channels[channel].ToString();
            }
        }

        public override short ToChShort(short channel)
        {
            return (short)Channels[channel];
        }
    }
}
