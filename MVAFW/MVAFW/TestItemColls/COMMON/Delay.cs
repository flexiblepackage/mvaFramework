using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace MVAFW.TestItemColls.COMMON
{
    public class Delay:BasicTestItem
    {
        public int DelayTime { get; set; }

        public override void doTest()
        {
            base.doTest();

            Thread.Sleep(DelayTime);
        }
    }
}
