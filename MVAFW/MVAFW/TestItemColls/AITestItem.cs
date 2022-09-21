using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using MVAFW.Config;

namespace MVAFW.TestItemColls
{
    public class AITestItem:AIOTestItem
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

        private double[][] scaledDatas;
        [Category("Test Result"), Description("Test detail scaled data")]
        public double[][] ScaledDatas
        {
            get
            {
                return scaledDatas;
            }
            internal set
            {
                scaledDatas = value;
            }
        }

        private double[][] spectrumsAbsolute;
        [Category("Dynamic Performance"), Description("Test detail of absolute spectrums data")]
        public double[][] SpectrumsAbsolute
        {
            get
            {
                return spectrumsAbsolute;
            }
            internal set
            {
                spectrumsAbsolute = value;
            }
        }

        private double[][] spectrums;
        [Category("Dynamic Performance"), Description("Test detail spectrums data")]
        public double[][] Spectrums
        {
            get
            {
                return spectrums;
            }
            internal set
            {
                spectrums = value;
            }
        }

        private double[][] power;
        [Category("Dynamic Performance"), Description("Test detail power data")]
        public double[][] Power
        {
            get
            {
                return power;
            }
            internal set
            {
                power = value;
            }
        }

        private double[][] phase;
        [Category("Dynamic Performance"), Description("Test detail phase data")]
        public double[][] Phase
        {
            get
            {
                return phase;
            }
            internal set
            {
                phase = value;
            }
        }

        private double[][] frequency;
        [Category("Dynamic Performance"), Description("Test detail frequency data")]
        public double[][] Frequency
        {
            get
            {
                return frequency;
            }
            internal set
            {
                frequency = value;
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
                this.ScaledDatas = new double[Math.Max(value, SpecificChannel + 1)][];
                this.Frequency = new double[Math.Max(value, SpecificChannel + 1)][];
                this.Spectrums = new double[Math.Max(value, SpecificChannel + 1)][];
                this.SpectrumsAbsolute = new double[Math.Max(value, SpecificChannel + 1)][];
                this.Power = new double[Math.Max(value, SpecificChannel + 1)][];
                this.Phase = new double[Math.Max(value, SpecificChannel + 1)][];                
                this.Snr = new double[Math.Max(value, SpecificChannel + 1)];
                this.Sinad = new double[Math.Max(value, SpecificChannel + 1)];
                this.Thd = new double[Math.Max(value, SpecificChannel + 1)];
                this.Sfdr = new double[Math.Max(value, SpecificChannel + 1)];
                this.Enob = new double[Math.Max(value, SpecificChannel + 1)];
                this.MainFrequency = new double[Math.Max(value, SpecificChannel + 1)];
                this.MainPhase = new double[Math.Max(value, SpecificChannel + 1)];
                this.DataCount = this.dataCount;
            }
        }

        private double sampleRate = 100000;
        [Category("AI setting"), Description("Sample rate"), DAQFuncAttribute(DAQFunction.BasicSetting)]
        public double SampleRate
        {
            get
            {
                return sampleRate;
            }
            set
            {
                sampleRate = value;
            }
        }        

        private bool logData = false;
        [Category("AI setting")]
        public bool LogData
        {
            get
            {
                return logData;
            }
            set
            {
                logData = value;
            }
        }

        private bool plotData = true;
        [Category("AI setting")]
        public bool PlotData
        {
            get
            {
                return plotData;
            }
            set
            {
                plotData = value;
            }
        }

        private bool assignData = true;
        [Category("AI setting"), Description("select 'False' to prevent the large data allocation, such as the Throught put test")]
        public bool AssignData
        {
            get
            {
                return assignData;
            }
            set
            {
                assignData = value;
                this.DataCount = dataCount;
            }
        }

        private bool dataFormat = true;
        [Category("AI setting")]
        public bool DataFormat
        {
            get
            {
                return dataFormat;
            }
            set
            {
                dataFormat = value;
            }
        }
        
        
        [Category("AI setting"), Description("Trigger Level"), DAQFuncAttribute(DAQFunction.TriggerLevel)]
        public ushort TrgLevel { get; set; }

        [Category("AI setting")]
        public AIAcquireModeConfig AcquireMode { get; set; }

        [Category("AI setting")]
        public uint DoubleBufferCount { get; set; }

        [Category("AI setting"), Description("Re Trigger Cnt"), DAQFuncAttribute(TestItemColls.DAQFunction.ReTriggerCount)]
        public uint ReTriggerCount { get; set; }
        
        [Category("AI setting")]
        public uint TimeBase { get; set; }

        [Category("AI setting")]
        public uint Delay1 { get; set; }

        [Category("AI setting")]
        public uint Delay2 { get; set; }

        private SyncModeConfig syncMode = SyncModeConfig.ASYNC;
        [Category("AI setting")]
        public SyncModeConfig SyncMode
        {
            get
            {
                return syncMode;
            }
            set
            {
                syncMode = value;
            }
        }

        private uint dataCount = 4096;
        [Category("AI setting"),Description("Data Count"), DAQFuncAttribute(DAQFunction.BasicSetting)]
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

                    if (assignData == true)
                    {
                        datas[channel] = new double[value];
                        scaledDatas[channel] = new double[value];                        
                        frequency[channel] = new double[value / 2];
                        spectrums[channel] = new double[value / 2];
                        spectrumsAbsolute[channel] = new double[value / 2];
                    }
                    else
                    {
                        datas[channel] = null;
                        scaledDatas[channel] = null;
                        frequency[channel / 2] = null;
                        spectrums[channel / 2] = null;
                        spectrumsAbsolute[channel] = null;
                    }
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

                    if (value != -1 && value >channelNumbers)
                    {
                        this.Datas = new double[Math.Max(channelNumbers, value + 1)][];
                        this.ScaledDatas = new double[Math.Max(channelNumbers, value + 1)][];
                        this.Frequency = new double[Math.Max(channelNumbers, value + 1)][];
                        this.Spectrums = new double[Math.Max(channelNumbers, value + 1)][];
                        this.Power = new double[Math.Max(channelNumbers, value + 1)][];
                        this.Phase = new double[Math.Max(channelNumbers, value + 1)][];                        
                        this.SpectrumsAbsolute = new double[Math.Max(channelNumbers, value + 1)][];
                        this.Snr = new double[Math.Max(value, SpecificChannel + 1)];
                        this.Sinad = new double[Math.Max(value, SpecificChannel + 1)];
                        this.Thd = new double[Math.Max(value, SpecificChannel + 1)];
                        this.Sfdr = new double[Math.Max(value, SpecificChannel + 1)];
                        this.Enob = new double[Math.Max(value, SpecificChannel + 1)];
                        this.MainFrequency = new double[Math.Max(value, SpecificChannel + 1)];
                        this.MainPhase = new double[Math.Max(value, SpecificChannel + 1)];
                        this.DataCount = this.dataCount;
                    }
                }
            }
        }

        private uint harmonicCount;
        [Category("Dynamic Setting"), Description("Total Harmonic count")]
        public virtual uint HarmonicCount
        {
            get
            {
                return harmonicCount;
            }
            set
            {
                harmonicCount = value;
            }
        }

        private uint span;
        [Category("Dynamic Setting"), Description("Span")]
        public virtual uint Span
        {
            get
            {
                return span;
            }
            set
            {
                span = value;
            }
        }

        private uint fullScaleRawdata;
        [Category("Dynamic Setting"), Description("The full Scale Rawdata set to get the absolute FFT")]
        public virtual uint FullScaleRawdata
        {
            get
            {
                return fullScaleRawdata;
            }
            set
            {
                fullScaleRawdata = value;
            }
        }

        private double[] sinad;
        [Category("Dynamic Performance"), Description("Sinad")]
        public virtual double[] Sinad
        {
            get
            {
                return sinad;
            }
            internal set
            {
                sinad = value;
            }
        }

        private double[] snr;
        [Category("Dynamic Performance"), Description("Snr")]
        public virtual double[] Snr
        {
            get
            {
                return snr;
            }
            internal set
            {
                snr = value;
            }
        }

        private double[] thd;
        [Category("Dynamic Performance"), Description("Thd")]
        public virtual double[] Thd
        {
            get
            {
                return thd;
            }
            internal set
            {
                thd = value;
            }
        }

        private double[] sfdr;
        [Category("Dynamic Performance"), Description("sfdr")]
        public virtual double[] Sfdr
        {
            get
            {
                return sfdr;
            }
            internal set
            {
                sfdr = value;
            }
        }

        private double[] enob;
        [Category("Dynamic Performance"), Description("enob")]
        public virtual double[] Enob
        {
            get
            {
                return enob;
            }
            internal set
            {
                enob = value;
            }
        }

        private double[] mainFrequency;
        [Category("Dynamic Performance"), Description("Main Frequency")]
        public virtual double[] MainFrequency
        {
            get
            {
                return mainFrequency;
            }
            internal set
            {
                mainFrequency = value;
            }
        }

        private double[] mainPhase;
        [Category("Dynamic Performance"), Description("Main Phase")]
        public virtual double[] MainPhase
        {
            get
            {
                return mainPhase;
            }
            internal set
            {
                mainPhase = value;
            }
        }

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
        [Browsable(false)]
        public ushort ReTriggerCtrl { get; set; }

        public enum SyncModeConfig
        {
            ASYNC=0,
            SYNC=1
        }


        public override void doTest()
        {
            base.doTest();
        }
    }
}
