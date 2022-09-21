using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace MVAFW.API.MVAAndroidAPI
{
    class AndroidAPIMyQ : AndroidAPI
    {

        public virtual void Login(AndroidDriver<IWebElement> driver, string logpath, string server, string account, string password, double offsetX, double offsetY)
        {
            if (server == "Production")
            {
                Press(driver, "DEV", offsetX, offsetY);
                Press(driver, "Dev", offsetX, offsetY);
                var pbtn = driver.FindElementByXPath("//android.widget.RadioButton[3]").Location;
                new TouchAction(driver).Tap(pbtn.X + offsetX, pbtn.Y + offsetY).Perform();

                var nbtn = driver.FindElementByAccessibilityId("Navigate up").Location;
                new TouchAction(driver).Tap(nbtn.X + offsetX, nbtn.Y + offsetY).Perform();
                new TouchAction(driver).Tap(nbtn.X + offsetX, nbtn.Y + offsetY).Perform();
            }

            Press(driver, "Already have an account? Sign In", offsetX, offsetY);

            while (DetectText(driver, "Welcome Back!", 1, false, false, logpath) == 0)
            {
                driver.FindElementByXPath("//android.view.View[1]/android.view.View/android.widget.EditText").SendKeys(account);
                driver.FindElementByXPath("//android.view.View[2]/android.view.View/android.widget.EditText").SendKeys(password);
                driver.PressKeyCode(AndroidKeyCode.Keycode_TAB);
                driver.PressKeyCode(AndroidKeyCode.Enter);
                Thread.Sleep(3000);
            }
        }

        public virtual int Deletion(AndroidDriver<IWebElement> driver, string logpath, string userName, string targetDevice, double explicitWaitSec, double offsetX, double offsetY)
        {
            var err = -1;

            DetectText(driver, "Home", explicitWaitSec, false, false, logpath);
            Press(driver, "Shared", offsetX, offsetY);
            Press(driver, userName + "'s Home", offsetX, offsetY);
            driver.FindElementByAccessibilityId("Navigate up").Click();

            Press(driver, "Device Management", offsetX, offsetY);
            Press(driver, targetDevice, offsetX, offsetY);
            driver.FindElementByAccessibilityId("Delete Place").Click();
            Press(driver, "DELETE", offsetX, offsetY);
            err = DetectText(driver, "HUBS", explicitWaitSec, true, false, logpath);
            return err;
        }

        public virtual int Provision(AndroidDriver<IWebElement> driver, string logpath, string userName, string targetDevice, string routerName, string routerPW,
                                                        double explicitWaitSec, double offsetX, double offsetY)
        {
            int err = -1;

            DetectText(driver, "Home", explicitWaitSec, false, false, logpath);
            Press(driver, "Shared", offsetX, offsetY);
            Press(driver, userName + "'s Home", offsetX, offsetY);
            driver.FindElementByAccessibilityId("Navigate up").Click();

            Press(driver, "Device Management", offsetX, offsetY);
            driver.FindElementByAccessibilityId("Add New Device").Click();

            Press(driver, "Garage Door Opener", offsetX, offsetY);
            Press(driver, "Ceiling Installed", offsetX, offsetY);

            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/wgdo_row_check_box").Click();
            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/wifi_row_check_box").Click();
            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/wifi_password_row_check_box").Click();
            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/bluetooth_row_check_box").Click();
            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/location_row_check_box").Click();
            Press(driver, "I'm Ready", offsetX, offsetY);

            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/button_wall_control_880lm").Click();
            Press(driver, "Next", offsetX, offsetY);
            Press(driver, "Yes", offsetX, offsetY);

            Press(driver, targetDevice, offsetX + 300, offsetY);

            DetectText(driver, "配對", explicitWaitSec, false, false, logpath);
            Press(driver, "配對", offsetX, offsetY);

            DetectText(driver, "Open Camera", explicitWaitSec, false, false, logpath);

            Press(driver, "Next", offsetX + 50, offsetY);

            DetectText(driver, routerName, explicitWaitSec, false, false, logpath);
            Press(driver, routerName, offsetX, offsetY);
            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/edit_network_password").SendKeys(routerPW);
            Press(driver, "Next", offsetX, offsetY);

            DetectText(driver, "Name", explicitWaitSec, false, false, logpath);
            var editDevice = driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/edit_device_name");
            editDevice.Clear();
            editDevice.SendKeys(targetDevice);

            Press(driver, "Next", offsetX, offsetY);

            if (DetectText(driver, "Finish", explicitWaitSec, false, false, logpath) == 0)
            {
                Press(driver, "Finish", offsetX, offsetY);
                err = DetectText(driver, "In-Garage Delivery", explicitWaitSec, true, false, logpath);
            }
            else
                err = DetectText(driver, "Schedules have moved", explicitWaitSec, true, false, logpath);

            return err;
        }


    }
}
