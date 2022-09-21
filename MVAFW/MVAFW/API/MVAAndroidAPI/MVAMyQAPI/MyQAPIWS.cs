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

            DetectText(driver, "Home", explicitWaitSec, false, false, logpath);
            Press(driver, "Shared", offsetX, offsetY);
            Press(driver, userName + "'s Home", offsetX, offsetY);
            driver.FindElementByAccessibilityId("Navigate up").Click();

            Press(driver, "Device Management", offsetX, offsetY);
            Press(driver, targetDevice, offsetX, offsetY);

            Press(driver, "Remove Camera", offsetX, offsetY);
            Press(driver, "DELETE", offsetX, offsetY);
            err = DetectText(driver, "HUBS", explicitWaitSec, true, false, logpath + " delete err");
            return err;
        }

        public override int Provision(AndroidDriver<IWebElement> driver, string logpath, string userName, string targetDevice, string routerName, string routerPW,
                                                        double explicitWaitSec, double offsetX, double offsetY)
        {
            int err = -1;

            DetectText(driver, "Home", explicitWaitSec, false, false, logpath);
            Press(driver, "Shared", offsetX, offsetY);
            Press(driver, userName + "'s Home", offsetX, offsetY);
            driver.FindElementByAccessibilityId("Navigate up").Click();

            Press(driver, "Device Management", offsetX, offsetY);
            driver.FindElementByAccessibilityId("Add New Device").Click();

            Press(driver, "Video Solution", offsetX, offsetY);
            Press(driver, "Smart Garage Camera", offsetX, offsetY);

            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/camera_row_check_box").Click();
            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/wifi_row_check_box").Click();
            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/wifi_password_row_check_box").Click();
            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/bluetooth_row_check_box").Click();
            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/location_row_check_box").Click();

            Press(driver, "I'm Ready", offsetX, offsetY);
            Press(driver, "Next", offsetX, offsetY);

            Press(driver, targetDevice, offsetX + 300, offsetY);

            Thread.Sleep(5000);

            DetectText(driver, "配對", explicitWaitSec, false, false, logpath);
            Press(driver, "配對", offsetX, offsetY);

            DetectText(driver, routerName, explicitWaitSec, false, false, logpath);
            Press(driver, routerName, offsetX, offsetY);

            if (routerPW != "")
            {
                driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/edit_network_password").SendKeys(routerPW);
                Press(driver, "Next", offsetX, offsetY);
            }
            DetectText(driver, "Name", explicitWaitSec, false, false, logpath);

            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/text_input_camera_name").SendKeys(targetDevice);

            Press(driver, "Select location", offsetX, offsetY);

            driver.PressKeyCode(AndroidKeyCode.Keycode_TAB);
            driver.PressKeyCode(AndroidKeyCode.Keycode_TAB);
            driver.PressKeyCode(AndroidKeyCode.Enter);

            Press(driver, "Next", offsetX, offsetY);

            DetectText(driver, "Finish", explicitWaitSec, false, false, logpath);
            Press(driver, "Finish", offsetX, offsetY);
            err = DetectText(driver, targetDevice, explicitWaitSec, true, false, logpath + " finish err");

            return err;
        }

    }
}
