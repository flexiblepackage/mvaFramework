using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using MVAFW.TestItemColls;
using MVAFW.Config;
using MVAFW.Common.Entity;

namespace MVAFW.API
{
    unsafe public abstract class MVADASKAPI
    {
        public TestItem TestItem { get; set; }
        private static Object thisLock = new Object();
        
    }
}
