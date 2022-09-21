using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVAFW.TestItemColls
{
    public class DAQFuncAttribute : Attribute
    {
        private DAQFunction daqFunction;
        public DAQFunction DAQFunction
        {
            get { return daqFunction; }
            set { daqFunction = value; }
        }

        public DAQFuncAttribute(DAQFunction daqFunction)
        {
            this.daqFunction = daqFunction;
        }
    }

    public enum DAQFunction
    {
        AlongWithChannel,
        TriggerMode,
        TriggerSource,
        TriggerPolarity,
        TriggerLevel,
        ReTriggerCtrl,
        ReTriggerCount,
        AcquireMode,
        BasicSetting,
        AdvanceSetting,
        PollingInterval,
        End,
    }
}
