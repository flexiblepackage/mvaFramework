using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.IO;
using MVAFW.Common.Entity;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace MVAFW.TestItemColls.Camera.Utility
{
    public class UartLog : BasicTestItem
    {
        public UartLog()
        {
            COM = "COM12";
            Baud = 115200;
            LogTimeSec = 100;
        }

        [Category("Uart Setting")]
        public string COM { get; set; }
        [Category("Uart Setting")]
        public int Baud { get; set; }
        [Category("Uart Setting")]
        public double LogTimeSec { get; set; }


        public override void doTest()
        {
            base.doTest();

            var logpath = LogPathCycle();

            UartLogForm form = new UartLogForm(COM, Baud, logpath, LogTimeSec);

            form.ShowDialog();

            eMVACollection.MVAStringCollection["filterkey1"] = logpath;

            Values[0] = "Finish";
        }
    }
}
