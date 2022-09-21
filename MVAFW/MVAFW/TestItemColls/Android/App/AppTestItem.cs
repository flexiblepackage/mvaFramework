using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using System.ComponentModel;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;

namespace MVAFW.TestItemColls.Android.App
{
    public class AppTestItem : AndroidTestItem
    {

        public AppTestItem()
        {
            AppPackage = "";
            AppActivity = "";   
            Account = "";
            Password = "";
            RouterName = "";
            RouterPW = "";
            TargetDevice = "";
            ServerSelect = serverList.Default;
            UserName = "";
            //Process.Start(@"C:\Program Files\Appium Server GUI\Appium Server GUI.exe");
            //Process.Start("cmd", "/C appium -a localhost -p 4723 -bp 4724 --allow-cors");
            //Process.Start("cmd", "/C appium  --allow-cors");
        }

        [Category("App Setting")]
        public string Account { get; set; }
        [Category("App Setting")]
        public string Password { get; set; }
        [Category("App Setting")]
        public string RouterPW { get; set; }
        [Category("App Setting")]
        public string RouterName { get; set; }
        [Category("App Setting")]
        public string TargetDevice { get; set; }
        [Category("App Setting")]
        public serverList ServerSelect { get; set; }
        public enum serverList
        {
            Default = '0',
            Production = '1',
            Develop = '2',
        }
        [Category("App Setting")]
        public string UserName { get; set; }
    }
}

