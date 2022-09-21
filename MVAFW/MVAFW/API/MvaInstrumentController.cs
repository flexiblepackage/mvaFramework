using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVAFW.Gpib;
using MVAFW.Apx;

namespace MVAFW.API
{
    public static class MvaInstrumentController
    {
        private static MVAGPIB mvaGpib;
        public static MVAGPIB MvaGPIB
        {
            get
            {
                if (mvaGpib == null)
                {
                    mvaGpib = new MVAGPIB();
                }
                return mvaGpib;
            }
        }




        public enum DmmMeasType
        {
            VOLT,
            CURR,
            FRES,
            RES
        }

        public enum APOutputConfig  //0: Bal_float ; 1:Bal_gnd ; 2:Unbal_float ; 3:Unbal_gnd
        {
            Bal_gnd = 1,
            Unbal_float = 2
        }

        public enum Waveforms
        {
            DC = 1,
            Sine = 2,
            Square = 3,
            Pulse = 4,
            Ramp = 5,
        }
    
}
}
