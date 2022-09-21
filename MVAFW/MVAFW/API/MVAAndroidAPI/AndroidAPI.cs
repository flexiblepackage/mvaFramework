using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;

namespace MVAFW.API.MVAAndroidAPI
{
    class AndroidAPI : MVAUartAPI
    {

        public int DetectScroll(AndroidDriver<IWebElement> driver, string resourceId, string text, bool screenShot, bool screenBool, string savePath)
        {
            try
            {
                var element = driver.FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector()" + ".resourceId(\"\")).scrollIntoView(".Insert(13, resourceId) + "new UiSelector().text(\"\"));".Insert(23, text));

                if (screenShot && screenBool)
                {
                    var timeNow = DateTime.Now;
                    driver.GetScreenshot().SaveAsFile(savePath + " detected " + timeNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
                }

                return 0;
            }
            catch (Exception)
            {
                if (screenShot && !screenBool)
                {
                    var timeNow = DateTime.Now;
                    driver.GetScreenshot().SaveAsFile(savePath + " detected " + timeNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
                }

                return -1;
            }
        }

        public int DetectToast(AndroidDriver<IWebElement> driver, string text, double time, bool screenShot, bool screenBool, string savePath)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@text='']".Insert(11, text))));

                if (screenShot && screenBool)
                {
                    var timeNow = DateTime.Now;
                    driver.GetScreenshot().SaveAsFile(savePath + " detected " + timeNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
                }

                return 0;
            }
            catch (Exception)
            {
                if (screenShot && !screenBool)
                {
                    var timeNow = DateTime.Now;
                    driver.GetScreenshot().SaveAsFile(savePath + " detected " + timeNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
                }

                return -1;
            }
        }

        public int DetectText(AndroidDriver<IWebElement> driver, string text, double time, bool screenShot, bool screenBool, string savePath)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@text='']".Insert(11, text))));

                if (screenShot && screenBool)
                {
                    var timeNow = DateTime.Now;
                    driver.GetScreenshot().SaveAsFile(savePath + " detected " + timeNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
                }
                //driver.FindElementByAndroidUIAutomator("new UiSelector().text(\"\")".Insert(23, text));
                //driver.FindElementsByXPath("//*[@text='']".Insert(11, text));
                return 0;
            }
            catch (Exception)
            {
                if (screenShot && !screenBool)
                {
                    var timeNow = DateTime.Now;
                    driver.GetScreenshot().SaveAsFile(savePath + " detected " + timeNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
                }

                return -1;
            }
        }

        public int DetectElementID(AndroidDriver<IWebElement> driver, string text, double time, bool screenShot, bool screenBool, string savePath)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(text)));

                if (screenShot && screenBool)
                {
                    var timeNow = DateTime.Now;
                    driver.GetScreenshot().SaveAsFile(savePath + " detected " + timeNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
                }

                return 0;
            }
            catch (Exception)
            {
                if (screenShot && !screenBool)
                {
                    var timeNow = DateTime.Now;
                    driver.GetScreenshot().SaveAsFile(savePath + " detected " + timeNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
                }

                return -1;
            }
        }

        public int DetectXPath(AndroidDriver<IWebElement> driver, string text, double time, bool screenShot, bool screenBool, string savePath)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(text)));

                if (screenShot && screenBool)
                {
                    var timeNow = DateTime.Now;
                    driver.GetScreenshot().SaveAsFile(savePath + " detected " + timeNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
                }

                return 0;
            }
            catch (Exception)
            {
                if (screenShot && !screenBool)
                {
                    var timeNow = DateTime.Now;
                    driver.GetScreenshot().SaveAsFile(savePath + " detected " + timeNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".png");
                }

                return -1;
            }
        }

        public void Press(AndroidDriver<IWebElement> driver, string text, double offsetX, double offsetY)
        {
            var btn = driver.FindElementByAndroidUIAutomator("new UiSelector().text(\"\")".Insert(23, text)).Location;
            new TouchAction(driver).Tap(btn.X + offsetX, btn.Y + offsetY).Perform();
        }

        private void Initial(ref AndroidDriver<IWebElement> driver, string deviceName, string udid, string platformVersion, string appPackage, string appActivity, double implicitWaitSec, bool appReset)
        {
            var options = new AppiumOptions { PlatformName = "Android" };
            options.AddAdditionalCapability("deviceName", deviceName);
            options.AddAdditionalCapability("udid", udid);
            options.AddAdditionalCapability("platformVersion", platformVersion);
            options.AddAdditionalCapability("autoGrantPermissions", true);
            options.AddAdditionalCapability("unicodeKeyboard", true);
            options.AddAdditionalCapability("resetKeyboard", true);
            options.AddAdditionalCapability("newCommandTimeout", 0); //下一個cmd要在幾秒內
            options.AddAdditionalCapability("–session-override", true);
            options.AddAdditionalCapability("appPackage", appPackage);
            options.AddAdditionalCapability("appActivity", appActivity);
            options.AddAdditionalCapability(MobileCapabilityType.AutomationName, AutomationName.AndroidUIAutomator2);
            options.AddAdditionalCapability("fullReset", "false");

            if (appReset)
                options.AddAdditionalCapability("noReset", "false");
            else
                options.AddAdditionalCapability("noReset", "true");

            driver = new AndroidDriver<IWebElement>(new Uri("http://localhost:4723/wd/hub"), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitSec);
        }

        public string AppProcess(ref AndroidDriver<IWebElement> driver, string deviceName, string udid, string platformVersion, string appPackage, string appActivity,
                                                     double implicitWaitSec, bool reset, string filePath, Func<int> MyAppScript)
        {

            try
            {
                Initial(ref driver, deviceName, udid, platformVersion, appPackage, appActivity, implicitWaitSec, reset);

                var err = MyAppScript();

                driver.Quit();

                return err.ToString(); ;
            }
            catch (NoSuchElementException)
            {
                var act = driver.CurrentActivity;
                driver.GetScreenshot().SaveAsFile(filePath + " NoSuchElementException" + ".png");
                driver.Quit();
                return "NoSuchElementException " + act;
            }
            catch (WebDriverException)
            {
                var act = driver.CurrentActivity;
                driver.GetScreenshot().SaveAsFile(filePath + " WebDriverException" + ".png");
                driver.Quit();
                return "WebDriverException " + act;
            }
        }

    }
}
