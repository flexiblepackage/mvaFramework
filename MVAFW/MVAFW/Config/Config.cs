using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVAFW.Config
{
    public enum AIAcquireModeConfig
    {
        Polling = 0,
        OneShot = 1,
        DoubleBuffer = 2,
        TwoStep = 99,
    }

    public enum displayDataType
    {
        rawdata,
        scaledData,
        fft,
    }
}
