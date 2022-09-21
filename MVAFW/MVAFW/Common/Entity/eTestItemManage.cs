using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MVAFW.TestItemColls;

namespace MVAFW.Common.Entity
{
    public class eTestItemManage
    {
        public Dictionary<int, TestItem> TestItemColls = new Dictionary<int, TestItem>();
        public string SN;
        public long ResultIndex;
    }
}
