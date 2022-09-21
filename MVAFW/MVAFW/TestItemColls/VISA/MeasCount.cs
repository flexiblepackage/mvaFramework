using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MVAFW.TestItemColls.VISA
{

    public class MeasCount : AITestItem
    {
        public MeasCount()
        {
            this.MaxChannelNumber = 4;
        }

        public int TimeOut_sec { get; set; }
        public string Address { get; set; }
        public ConnectType ConnectionType { get; set; }
        public enum ConnectType
        {
            LAN = 1,
            USB = 2,
            GPIB = 3,
        }

        public override void doTest()
        {
            base.doTest();

            StringBuilder Read = new StringBuilder(2048);
            int err, RM, ch;
            int SessionId = 0;

            err = visa32.viOpenDefaultRM(out RM);

            switch ((ushort)ConnectionType)
            {
                case 1:
                    err = visa32.viOpen(RM, "TCPIP0::" + Address + "::INSTR", 0, 0, out SessionId);
                    break;

                case 2:
                    err = visa32.viOpen(RM, "USB0::" + Address + "::INSTR", 0, 0, out SessionId);
                    break;

                case 3:
                    err = visa32.viOpen(RM, "GPIB0::" + Address + "::INSTR", 0, 0, out SessionId);
                    break;
            }

            err = visa32.viSetAttribute(SessionId, visa32.VI_ATTR_TERMCHAR, 13); //Set the termination character to carriage return (i.e., 13);
            err = visa32.viSetAttribute(SessionId, visa32.VI_ATTR_TERMCHAR_EN, 1); //Set the flag to terminate when receiving a termination character
            err = visa32.viSetAttribute(SessionId, visa32.VI_ATTR_TMO_VALUE, TimeOut_sec * 1000); //Set timeout in milliseconds; set the timeout for your requirements
            err = visa32.viPrintf(SessionId, ":MEAS:CLE"  + "\n");

            for (int i = 0; i < ChannelNumbers; i++)
            {
                ch = Channels[i] + 1;
                err = visa32.viPrintf(SessionId, ":MEAS:COUN? CHAN" + ch + "\n");
                err = visa32.viScanf(SessionId, "%2048s", Read);

                Values[i] = Read.ToString();
            }

            if (err != 0) throw new Exception("Err = " + err);

            visa32.viClose(SessionId);
            visa32.viClose(RM);
        }
    }
}
