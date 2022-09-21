using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using MVAFW.Common.Entity;

namespace MVAQCDVT
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool autoStart = false;

            if (args.Length == 0)
            {
                autoStart = false;
            }
            else if (args.Length >= 1 && args[0] == "start")
            {
                autoStart = true;
            }
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new mainForm(autoStart, args));
                //Application.Run(new mainForm(true, new string[] { "start", "C:\\QC LOG FILE\\54088.log" }));

                if (eProductSetting.Result == "PASS")
                {
                    Console.WriteLine(3);
                    Environment.Exit(3);
                }
                else if (eProductSetting.Result == "FAIL")
                {
                    Console.WriteLine(4);
                    Environment.Exit(4);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
