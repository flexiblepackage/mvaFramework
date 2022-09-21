using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MVAFW.TestItemColls.COMMON
{
    public class DiskRW : BasicTestItem
    {
        public string testDriver { get; set; }
        public double dataSizeMB { get; set; }
        public ReadWriteConfig readWriteConfig { get; set; }
      

        public enum ReadWriteConfig
        {
            Write = 0,
            Read = 1
        }

        public override void doTest()
        {
            base.doTest();

            double speedMB = 0.0;
            string[] deriveAvailable= GetRemovableDriveLetters();

            foreach (string driver in deriveAvailable)
            {
                if (testDriver.Equals(driver, StringComparison.OrdinalIgnoreCase))
                {
                    speedMB = TestSpeed();
                }
            }    

            this.Values[0] = speedMB.ToString();
        }

        private string[] GetRemovableDriveLetters()
        {
            System.Collections.ArrayList RemovableDriveLetters = new System.Collections.ArrayList();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType == DriveType.Removable)
                {
                    RemovableDriveLetters.Add(d.Name.Substring(0, 1));
                }
            }
            return RemovableDriveLetters.ToArray(typeof(string)) as string[];
        }

        private double TestSpeed()
        {
            double speedMB = 0.0;
            try
            {
                int appendIterations = 10 * Convert.ToInt16(dataSizeMB);
                DateTime startTime;
                DateTime stopTime;
                string randomText = RandomString(100000);
                string srcPath = System.Environment.CurrentDirectory + "\\test.tmp";
                string desPath = testDriver + ":\\test.tmp";
                if (readWriteConfig == ReadWriteConfig.Read)
                {
                    var tmp = srcPath;
                    srcPath = desPath;
                    desPath = tmp;
                }


                if (File.Exists(srcPath))
                {
                    File.Delete(srcPath);
                }

                if (File.Exists(desPath))
                {
                    File.Delete(desPath);
                }

                StreamWriter sWriter = new StreamWriter(srcPath, true);
                for (int i = 1; i <= appendIterations; i++)
                {
                    sWriter.Write(randomText);
                }
                sWriter.Close();
                startTime = DateTime.Now;
                File.Copy(srcPath, desPath);
                stopTime = DateTime.Now;
                File.Delete(srcPath);
                File.Delete(desPath);
                TimeSpan interval = stopTime - startTime;
                speedMB =  dataSizeMB / interval.TotalMilliseconds * 1000;//MB/Sec
            }
            catch (Exception e)
            {
                speedMB = 0.0;
            }
            return speedMB;
        }

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public DiskRW()
        {
            dataSizeMB = 10.0;
            testDriver = "X";
        }
    }
}
