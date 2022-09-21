using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using MVAFW.Common.Entity;


namespace MVAFW.TestItemColls
{
    public class BasicTestItem:TestItem
    {
        public BasicTestItem()
        {
            LogName = "Default";
        }

        [Category("Basic Setting"), Description("Used to keep the same logname cycle")]
        public string LogName
        {
            get
            {
                return logname;
            }
            set
            {
                logname = value;
                eMVACollection.MVACollection[logname] = 1;
            }
        }
        private string logname;

        public string LogPathCycle()
        {
            string defaultPath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "QC LOG FILE";
            return defaultPath + "\\" + LogName + " Cycle " + ((int)eMVACollection.MVACollection[LogName]++).ToString("D4");
        }

        public override void doTest()
        {
            base.doTest();
        }
    }
}
