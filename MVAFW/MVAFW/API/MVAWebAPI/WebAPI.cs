using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MVAFW.API.MVAWebAPI
{
    class WebAPI : MVAUartAPI
    {

        public string OTA(double implicitWaitSec, string otaURL, double uploadDelaySec,
                                         string imgPath, string xpathUploadFile, string xpathApply, string xpathFileSize)
        {
            var driver = new ChromeDriver();

            try
            {
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(implicitWaitSec);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitSec);
                driver.Navigate().GoToUrl(otaURL);
                driver.FindElement(By.XPath(xpathUploadFile)).SendKeys(imgPath);
                driver.FindElement(By.XPath(xpathApply)).Click();
                driver.FindElement(By.XPath(xpathFileSize));

                Thread.Sleep(TimeSpan.FromSeconds(uploadDelaySec));

                driver.Quit();
                return "Finish";
            }
            catch (NoSuchElementException)
            {
                driver.Quit();
                return "NoSuchElementException";
            }
            catch (WebDriverException)
            {
                driver.Quit();
                return "WebDriverException";
            }
        }

    }
}
