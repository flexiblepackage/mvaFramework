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

            for (var i = 0; i < switchCnt; i++)
            {
                //DetectText("Wi-Fi Networks", explicitWaitSec, false, false, logpath);

                //for (var j = 0; j < ChannelNumbers; j++)
                //{
                //    err[j] = DetectScroll("com.chamberlain.android.liftmaster.myq:id/networkListview", "CGI_inet_provision_result-" + j, true, false, logpath + " CGI" + j);
                //    Thread.Sleep(1000);

                //    if (j > 3 && j < 10)
                //        Thread.Sleep(TimeSpan.FromSeconds(ScanWifiDelay_Sec));
                //}
                err = DetectText(driver, routerName, explicitWaitSec, true, false, logpath + " list err");

                if (switchCnt > 1 && i < switchCnt - 1)
                {
                    //driver.FindElementByAccessibilityId("Exit").Click();
                    Press(driver, "Don't See Your Network", offsetX, offsetY);
                    Press(driver, "Try Again", offsetX, offsetY);
                }
            }

            return err;
        }

        public int DetailView(AndroidDriver<IWebElement> driver, string logpath, int switchCnt, double switchDelaySec, bool pttEnable, double pttDelaySec, string userName, string targetDevice,
                                                        double explicitWaitSec, double offsetX, double offsetY)
        {
            DetectText(driver, "Home", explicitWaitSec, false, false, logpath);
            Press(driver, "Shared", offsetX, offsetY);
            Press(driver, userName + "'s Home", offsetX, offsetY);

            driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/dashboard_menu_item_view_selector").Click();

            Thread.Sleep(60000);

            DetectText(driver, targetDevice + " Camera", explicitWaitSec, false, false, logpath);

            //var cntFirstFW = 0;
            var cnt = 0;

            while (cnt < switchCnt)
            {

                Press(driver, targetDevice + " Camera", 0, 0);

                //if (firmwareCheck && cntFirstFW < 1)
                //{
                //    Press("NOT NOW", offsetX, offsetY); //Firmware Update Available
                //    Press("OK", offsetX, offsetY); //Update Later
                //    cntFirstFW++;
                //}

                DetectElementID(driver, "com.chamberlain.android.liftmaster.myq:id/portrait_volume_button", explicitWaitSec, false, false, logpath);

                if (pttEnable)
                {
                    var audio = driver.FindElementByAccessibilityId("Enable playback audio").Location; //id com.chamberlain.android.liftmaster.myq:id/portrait_volume_button
                    new TouchAction(driver).Tap(audio.X + 100, audio.Y + 200).Perform();

                    var ptt = driver.FindElementByAccessibilityId("Enable speaker").Location; //id com.chamberlain.android.liftmaster.myq:id/portrait_mic_button
                    new TouchAction(driver).LongPress(ptt.X + 100, ptt.Y + 200).Wait((long)(pttDelaySec * 1000)).Release().Perform();
                }

                Thread.Sleep(TimeSpan.FromSeconds(switchDelaySec));

                var nbtn = driver.FindElementByAccessibilityId("Navigate up").Location;
                new TouchAction(driver).Tap(nbtn.X + offsetX, nbtn.Y + offsetY).Perform();

                DetectText(driver, targetDevice + " Camera", explicitWaitSec, false, false, logpath);

                Thread.Sleep(TimeSpan.FromSeconds(switchDelaySec));

                cnt++;
            }

            return cnt;
        }

        public  int MainView(AndroidDriver<IWebElement> driver, string logpath, int switchCnt, double switchDelaySec,  string userName, string targetDevice,
                                                        double explicitWaitSec, double offsetX, double offsetY)
        {
            var stopwatch = new Stopwatch();

            DetectText(driver, "Home", explicitWaitSec, false, false, logpath);
            Press(driver, "Shared", offsetX, offsetY);
            Press(driver, userName + "'s Home", offsetX, offsetY);

            if (DetectElementID(driver, "com.chamberlain.android.liftmaster.myq:id/camera_play_btn", explicitWaitSec, false, false, logpath) == 0)
            {
                var pbtn = driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/camera_play_btn").Location;
                new TouchAction(driver).Tap(pbtn.X + offsetX, pbtn.Y + offsetY).Perform();
            }

            stopwatch.Restart();
            while (stopwatch.Elapsed.TotalSeconds < 30)
            {
                if (DetectToast(driver, targetDevice + " Camera: Failed to Load Stream", 0.5, true, true, logpath+ " initView err") == 0)
                    break;
            }
            stopwatch.Stop();

            Thread.Sleep(TimeSpan.FromSeconds(switchDelaySec));

            var cnt = 0;

            while (cnt < switchCnt)
            {
                driver.FindElementByAccessibilityId("Navigate up").Click();
                DetectText(driver, "Device Management", explicitWaitSec, false, false, logpath);

                Thread.Sleep(TimeSpan.FromSeconds(switchDelaySec));

                driver.FindElementByAccessibilityId("Navigate up").Click();

                if (DetectElementID(driver, "com.chamberlain.android.liftmaster.myq:id/camera_play_btn", explicitWaitSec, false, false, logpath) == 0)
                {
                    var pbtn1 = driver.FindElementById("com.chamberlain.android.liftmaster.myq:id/camera_play_btn").Location;
                    new TouchAction(driver).Tap(pbtn1.X + offsetX, pbtn1.Y + offsetY).Perform();
                }

                stopwatch.Restart();
                while (stopwatch.Elapsed.TotalSeconds < 30)
                {
                    if (DetectToast(driver, targetDevice + " Camera: Failed to Load Stream", 0.5, true, true, logpath+ " loopView err") == 0)
                        break;                
                }
                stopwatch.Stop();

                Thread.Sleep(TimeSpan.FromSeconds(switchDelaySec));

                cnt++;
            }

            return cnt;
        }

    }
}
