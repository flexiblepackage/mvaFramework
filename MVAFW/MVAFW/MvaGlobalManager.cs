using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MVAFW.DB;
using MVAFW.Common.Output;
using MVAFW.Config;

namespace MVAFW
{
    public class MvaGlobalManager
    {
        public static Error InitProductSetting()
        {
            return oProductSetting.GetProductSetting();
        }
    }
}
