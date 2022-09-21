using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MVAFW.TestItemColls.Camera.Utility
{
    public partial class UartLogForm : Form
    {
        List<string> patterns = new List<string> { @"[\u001b\u0007]", @"\[\d;\d\dm", @"\[m" };
        CancellationTokenSource ctsrc = null;
        StringBuilder uisb = null;
        StringBuilder sb = null;
        SerialPort sp = null;
        string path;
        double timeSec;

        public UartLogForm(string COM, int Baud, string logPath, double logTimeSec)//string FilePath, int LogTimeSec, string COM, int Baud
        {
            InitializeComponent();

            sp = new SerialPort(COM, Baud, Parity.None, 8, StopBits.One);
            sp.Handshake = Handshake.None;
            sp.Open();

            ctsrc = new CancellationTokenSource();
            var ct = ctsrc.Token;

            uisb = new StringBuilder();
            sb = new StringBuilder();

            var timeNow = DateTime.Now;
            var stopwatch = new Stopwatch();

            stopwatch.Restart();

            Task.Run(() =>
            {
                try
                {
                    while (!ct.IsCancellationRequested)
                    {
                        if (sp.BytesToRead > 0)
                        {
                            var buf = sp.ReadLine();

                            var timeStamp = (timeNow + stopwatch.Elapsed).ToString(@"[yyyy-MM-dd HH\:mm\:ss\.fff]");

                            var log = String.Format("{0} {1}", timeStamp, buf.Trim());

                            sb.AppendLine(log);

                            uisb.AppendLine(log);
                        }
                    }

                    stopwatch.Stop();
                }

                catch (IOException)
                {
                }
            });

            path = logPath;
            timeSec = logTimeSec;
        }

        private async void CodeTestForm_Shown(object sender, EventArgs e)
        {
            var progressReporter = new Progress<int>(ReportProgress);
            await TimeConsumeFunction(progressReporter);
            Close();
        }

        //refresh ui function
        private void ReportProgress(int val)
        {
            textbox1.AppendText(Regex.Replace(uisb.ToString(), string.Join("|", patterns), string.Empty));
            uisb.Clear();
            Text = "UARTLog  " + val.ToString("N0") + "/" + timeSec;
        }

        //while loop function
        private Task TimeConsumeFunction(IProgress<int> progressRptr)
        {
            var stopwatch = new Stopwatch();

            return Task.Run(() =>
            {
                stopwatch.Restart();

                while (stopwatch.Elapsed.TotalSeconds < timeSec)
                {
                    Thread.Sleep(1000);
                    progressRptr.Report((int)stopwatch.Elapsed.TotalSeconds);
                }

                stopwatch.Stop();
            });
        }

        private void CodeTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ctsrc.Cancel();
            UartLogClose(sp, sb, path);
        }

        public void UartLogClose(SerialPort sp, StringBuilder sb, string logPath)
        {
            Thread.Sleep(1000);
            var sw = new StreamWriter(logPath + ".log");
            sw.Write(Regex.Replace(sb.ToString(), string.Join("|", patterns), string.Empty));
            sw.Close();
            sp.Close();
        }

        //private void textbox1_TextChanged(object sender, EventArgs e)
        //{
        //    textbox1.ScrollBars = ScrollBars.Both;
        //    textbox1.WordWrap = false;
        //}
    }
}
