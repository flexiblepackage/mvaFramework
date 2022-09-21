using System;
using System.ComponentModel;
using MVAFW.Common.Entity;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MVAFW.TestItemColls
{
    public class UartTestItem : BasicTestItem
    {
        public UartTestItem()
        {
            COM1 = "COM12";
            COM1Baud = 115200;
            COM1Enable = false;
        }

        [Category("Uart Setting")]
        public string COM1 { get; set; }
        [Category("Uart Setting")]
        public int COM1Baud { get; set; }
        [Category("Uart Setting")]
        public bool COM1Enable { get; set; }
    }
}
