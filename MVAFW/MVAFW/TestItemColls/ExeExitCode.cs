using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace MVAFW.TestItemColls
{
    public class ExeExitCode : TestItem
    {
        private string fileName;
        [Category("Exe/Bat setting")]
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        private string parameter;
        [Category("Exe/Bat setting")]
        public string Parameter
        {
            get
            {
                return parameter;
            }
            set
            {
                parameter = value;
            }
        }

        public override void doTest()
        {
            base.doTest();

            Process ExternalProcess = new Process();
            ExternalProcess.StartInfo.FileName = FileName;
            ExternalProcess.Start();
            ExternalProcess.WaitForExit();

            this.Values[0] = ExternalProcess.ExitCode.ToString();
        }
    }
}
