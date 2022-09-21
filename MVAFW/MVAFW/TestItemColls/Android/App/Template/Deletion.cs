using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium;
using System.Threading;
using System.ComponentModel;
using MVAFW.API;
using MVAFW.API.MVAAndroidAPI.MVAMyQAPI;
using System.Threading.Tasks;
using System;
using MVAFW.Common.Entity;

namespace MVAFW.TestItemColls.Android.App.Template
{

    public class Deletion : AppTestItem
    {

        public Deletion()
        {
            Account = "";
            Password = "";
            RouterName = "";
            RouterPW = "";
            TargetDevice = "";
            ServerSelect = serverList.Develop;
            UserName = "";
            AppFinishDelaySec = 10;
        }

        [Category("App Setting")]
        public double AppFinishDelaySec { get; set; }

        private int appTest(MyQAPIWS api, AndroidDriver<IWebElement> driver, string logpath)
        {
            api.Login(driver, logpath, ServerSelect.ToString(), Account, Password, BtnOffsetX, BtnOffsetY);
            return api.Deletion(driver, logpath, UserName, TargetDevice, ExplicitWaitSec, BtnOffsetX, BtnOffsetY);
        }

        public override void doTest()
        {
            base.doTest();

            var api = (MyQAPIWS)MVAUAPI.LookupApi(this);

            var logpath = LogPathCycle();

            var uartToken = new CancellationTokenSource();

            var tuple = api.UartLogOpen(COM1, COM1Baud, COM1Enable, uartToken.Token);

            var err = api.AppProcess(ref driver, DeviceName, UDID, PlatformVersion, AppPackage, AppActivity, ImplicitWaitSec, AppReset, logpath,
                                                        () => appTest(api, driver, logpath));

            Thread.Sleep(TimeSpan.FromSeconds(AppFinishDelaySec));

            uartToken.Cancel();

            api.UartLogClose(tuple.Item1, tuple.Item2, COM1Enable, logpath);

            eMVACollection.MVAStringCollection["filterkey1"] = logpath;

            Values[0] = err;
        }
    }
}
