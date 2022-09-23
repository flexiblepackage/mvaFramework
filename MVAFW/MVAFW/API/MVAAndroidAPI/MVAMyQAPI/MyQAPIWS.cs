using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace MVAFW.API.MVAAndroidAPI.MVAMyQAPI
{
    class MyQAPIWS : AndroidAPIMyQ
    {

        public override int Deletion(AndroidDriver<IWebElement> driver, string logpath, string userName, string targetDevice, double explicitWaitSec, double offsetX, double offsetY)
        {
            var err = -1;
          
            return err;
        }

        public override int Provision(AndroidDriver<IWebElement> driver, string logpath, string userName, string targetDevice, string routerName, string routerPW,
                                                        double explicitWaitSec, double offsetX, double offsetY)
        {
            int err = -1;         

            return err;
        }

    }
}
