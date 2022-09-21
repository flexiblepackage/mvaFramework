using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using System.ComponentModel;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;

namespace MVAFW.TestItemColls.Android
{
    public class AndroidTestItem : UartTestItem
    {

        public AndroidTestItem()
        {
            AppPackage = "";
            AppActivity = "";
            ImplicitWaitSec = 10;
            ExplicitWaitSec = 60;
            BtnOffsetY = 90;
            BtnOffsetX = 0;
            DeviceName = "";
            UDID = "";
            PlatformVersion = "";
            AppReset = true;
            //Process.Start("C:\Program Files\Appium Server GUI\Appium Server GUI.exe");
            //Process.Start("cmd", "/C appium -a 127.0.0.1 -p 4723 -bp 4724 --allow-cors");
            //Process.Start("cmd", "/C appium  --allow-cors");
        }

        [Category("Appium Setting")]
        public string AppPackage { get; set; }
        [Category("Appium Setting")]
        public string AppActivity { get; set; }
        [Category("Appium Setting")]
        public ushort ExplicitWaitSec { get; set; }
        [Category("Appium Setting")]
        public ushort ImplicitWaitSec { get; set; }
        [Category("Appium Setting")]
        public string DeviceName { get; set; }
        [Category("Appium Setting")]
        public string UDID { get; set; }
        [Category("Appium Setting")]
        public string PlatformVersion { get; set; }
        [Category("Appium Setting")]
        public bool AppReset { get; set; }
        [Category("Appium Setting")]
        public double BtnOffsetY { get; set; }
        [Category("Appium Setting")]
        public double BtnOffsetX { get; set; }

        public AndroidDriver<IWebElement> driver = null;
    }
}

