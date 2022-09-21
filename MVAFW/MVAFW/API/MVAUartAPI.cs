using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Diagnostics;
using MVAFW.Config;
using MVAFW.TestItemColls;

namespace MVAFW.API
{
    class MVAUartAPI : MVADASKAPI
    {

        public Tuple<SerialPort, StringBuilder> UartLogOpen(string COM, int Baud, bool UartEnable, CancellationToken uartToken)
        {
            SerialPort sp = null;
            StringBuilder sb = null;

            if (UartEnable)
            {
                sp = new SerialPort(COM, Baud, Parity.None, 8, StopBits.One);
                sp.Handshake = Handshake.None;
                sp.Open();

                sb = new StringBuilder();

                var timeNow = DateTime.Now;
                var stopwatch = new Stopwatch();
                stopwatch.Restart();

                Task.Run(() =>
                {
                    try
                    {
                        while (!uartToken.IsCancellationRequested)
                        {
                            if (sp.BytesToRead > 0)
                            {
                                var buf = sp.ReadLine();

                                var timeStamp = (timeNow + stopwatch.Elapsed).ToString(@"[yyyy-MM-dd HH\:mm\:ss\.fff]");

                                var log = String.Format("{0} {1}", timeStamp, buf.Trim());

                                sb.AppendLine(log);
                            }
                        }

                        stopwatch.Stop();
                    }

                    catch (IOException)
                    {
                        //Thread.Sleep(1000);
                    }
                });
            }

            return Tuple.Create(sp, sb);
        }

        public void UartLogClose(SerialPort sp, StringBuilder sb, bool UartEnable, string LogPath)
        {
            if (UartEnable)
            {
                Thread.Sleep(1000);
                var patterns = new List<string> { @"[\u001b\u0007]", @"\[\d;\d\dm", @"\[m" };
                var sw = new StreamWriter(LogPath + ".log");
                sw.Write(Regex.Replace(sb.ToString(), string.Join("|", patterns), string.Empty));
                sw.Close();
                sp.Close();
            }
        }


    }
}
