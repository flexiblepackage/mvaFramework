using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Diagnostics;

namespace MVAFW.API.MVAAndroidAPI.MVAMyQAPI
{
    class MyQAPIFalcon : AndroidAPIMyQ
    {

        public int scanBleWifi(AndroidDriver<IWebElement> driver, string logpath, int switchCnt, string userName, string targetDevice, string routerName, string routerPW,
                                                        double explicitWaitSec, double offsetX, double offsetY)
        {
            var err = -1;        

            return err;
        }

        public int DetailView(AndroidDriver<IWebElement> driver, string logpath, int switchCnt, double switchDelaySec, bool pttEnable, double pttDelaySec, string userName, string targetDevice,
                                                        double explicitWaitSec, double offsetX, double offsetY)
        {
           
            return 0;
        }

        public  int MainView(AndroidDriver<IWebElement> driver, string logpath, int switchCnt, double switchDelaySec,  string userName, string targetDevice,
                                                        double explicitWaitSec, double offsetX, double offsetY)
        {
            return 0;
        }

    }
}
