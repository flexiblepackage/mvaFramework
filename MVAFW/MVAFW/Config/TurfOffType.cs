using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVAFW.Config
{
    public enum TurnOffType
    {
        None,
        AutoRestart,
        AutoShoutDown
    }

    public enum FailOperationType
    {
        None,
        GotoTestItemIndex,
        TestTestItemIndex,
    }
}
