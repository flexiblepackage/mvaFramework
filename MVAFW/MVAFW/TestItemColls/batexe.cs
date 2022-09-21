using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.ComponentModel;

using MVAFW.Config;

namespace MVAFW.TestItemColls
{
    public class batexe : TestItem
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

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            //p.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            p.StartInfo.WorkingDirectory = System.Windows.Forms.Application.StartupPath;
            p.StartInfo.Verb = "runas";
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            StreamWriter cmd = p.StandardInput;
            cmd.AutoFlush = true;
            cmd.WriteLine(fileName + " " + parameter);
            cmd.WriteLine("exit");
            string output = p.StandardOutput.ReadToEnd();

            string[] result;
            int resultStartIndex = 4;


            result = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            if (MultiChannel == false)
            {
                Values[SpecificChannel] = result[resultStartIndex];
            }
            else if (MultiChannel == true)
            {
                for (int channel = 0; channel < ChannelNumbers; channel++)
                {
                    Values[channel] = result[channel + resultStartIndex];
                }
            }            

            p.WaitForExit();
            cmd.Flush();
        }
    }
}
