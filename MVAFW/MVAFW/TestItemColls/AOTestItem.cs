using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MVAFW.TestItemColls
{
    public class AOTestItem : AIOTestItem
    {
        private double[][] datas;
        [Category("Test Result"), Description("Test detail data")]
        public double[][] Datas
        {
            get
            {
                return datas;
            }
            internal set
            {
                datas = value;
            }
        }

        private ushort channelNumbers = 1;
        [Category("Basic Setting"), Description("Channel Numbers to be tested")]
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

                this.Datas = new double[Math.Max(value, SpecificChannel + 1)][];
                this.DataCount = this.dataCount;
            }
        }

        private uint updateInterval;
        [Category("AO setting"), Description("AI Sampling rate")]
        public virtual uint UpdateInterval
        {
            get
            {
                return updateInterval;
            }
            set
            {
                updateInterval = value;
            }
        }

        [Category("AO setting")]
        public ushort TrgLevel { get; set; }

        [Category("AO setting")]
        public bool DoubleBuffer { get; set; }

        [Category("AO setting")]
        public uint ReTriggerCount { get; set; }

        [Category("AO setting"), ReadOnly(true)]
        public uint TimeBase { get; set; }

        [Category("AO setting")]
        public uint Delay1 { get; set; }

        [Category("AO setting")]
        public uint Delay2 { get; set; }

        [Category("AO setting")]
        public virtual SyncModeConfig SyncMode { get; set; }

        private uint dataCount = 4096;
        [Category("AO setting")]
        public uint DataCount
        {
            get
            {
                return dataCount;
            }
            set
            {
                dataCount = value;

                for (int channel = 0; channel < Math.Max(ChannelNumbers, SpecificChannel + 1); channel++)
                {
                    if (datas == null)
                    {
                        break;
                    }
                    datas[channel] = new double[value];
                }
            }
        }

        private short specificChannel = -1;
        [Category("Basic Setting"), Description("Display the specific channel test result")]
        public override short SpecificChannel
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
                    base.SpecificChannel = value;
                    specificChannel = value;

                    if (value != -1 && value >= channelNumbers)
                    {
                        this.Datas = new double[Math.Max(channelNumbers, value + 1)][];
                        this.DataCount = this.dataCount;
                    }
                    else if (value == -1 &&  channelNumbers > 0)
                    {
                        this.Datas = new double[channelNumbers][];
                        this.DataCount = this.dataCount;
                    }
                    else
                    {

                    }
                }
            }
        }

        [ Browsable(false)]
        public ushort AdRange { get; protected set; }
        [Browsable(false)]
        public ushort AdMode { get; protected set; }
        [Browsable(false)]
        public ushort AdTrgSrc { get; protected set; }
        [Browsable(false)]
        public ushort AdTrgMode { get; protected set; }
        [Browsable(false)]
        public ushort AdTrgPolarity { get; protected set; }
        [Browsable(false)]
        public ushort AdTimeBaseSrc { get; protected set; }
        [Browsable(false)]
        public ushort AdConverSrc { get; protected set; }

        public enum SyncModeConfig
        {
            ASYNC =0,
            SYNC = 1
        }

        public override void doTest()
        {
            base.doTest();
        }
    }
}
