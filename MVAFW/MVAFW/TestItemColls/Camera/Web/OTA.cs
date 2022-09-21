using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using MVAFW.Common.Entity;
using MVAFW.API;
using MVAFW.API.MVAWebAPI;

namespace MVAFW.TestItemColls.Camera.Web
{
    public class OTA : UartTestItem
    {

        public OTA()
        {
            WebURL = "";
            ImagePath = @"";
            XPathUploadFile = "";
            XPathApply = "";
            XPathFileSize = "";
            DurationSec = 80;
            ImplicitWaitSec = 60;
            UploadDelaySec = 2;

            COM2 = "COM11";
            COM2Baud = 115200;
            COM2Enable = false;

        }

        [Category("OTA Setting")]
        public string WebURL { get; set; }
        [Category("OTA Setting")]
        public string ImagePath { get; set; }
        [Category("OTA Setting")]
        public double ImplicitWaitSec { get; set; }
        [Category("OTA Setting")]
        public double UploadDelaySec { get; set; }
        [Category("OTA Setting")]
        public double DurationSec { get; set; }
        [Category("OTA Setting")]
        public string XPathUploadFile { get; set; }
        [Category("OTA Setting")]
        public string XPathApply { get; set; }
        [Category("OTA Setting")]
        public string XPathFileSize { get; set; }

        [Category("Uart Setting")]
        public string COM2 { get; set; }
        [Category("Uart Setting")]
        public int COM2Baud { get; set; }
        [Category("Uart Setting")]
        public bool COM2Enable { get; set; }

        //private IWebDriver driver;

        public override void doTest()
        {
            base.doTest();

            var api = (WebAPI)MVAUAPI.LookupApi(this);

            var logpath = LogPathCycle();

            var uartToken = new CancellationTokenSource();

            var tuple1 = api.UartLogOpen(COM1, COM1Baud, COM1Enable, uartToken.Token);

            var tuple2 = api.UartLogOpen(COM2, COM2Baud, COM2Enable, uartToken.Token);

            var err = api.OTA(ImplicitWaitSec, WebURL, UploadDelaySec, ImagePath, XPathUploadFile, XPathApply, XPathFileSize);

            Thread.Sleep(TimeSpan.FromSeconds(DurationSec));

            uartToken.Cancel();

            api.UartLogClose(tuple1.Item1, tuple1.Item2, COM1Enable, logpath + " " + COM1);

            api.UartLogClose(tuple2.Item1, tuple2.Item2, COM2Enable, logpath + " " + COM2);

            eMVACollection.MVAStringCollection["filterkey1"] = logpath + " " + COM1;

            eMVACollection.MVAStringCollection["filterkey2"] = logpath + " " + COM2;

            Values[0] = err;
        }
    }
}
