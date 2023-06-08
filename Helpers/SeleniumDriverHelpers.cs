using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Framework.Config;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium;

namespace ios_tests.Helpers
{
    public static class SeleniumDriverHelpers
    {
        public static bool SafeDoesElementExist(this IWebDriver driver, By by)
        {
            return driver.FindElements(by).Count > 0;
        }

        public static bool SafeIsElementDisplayed(this IWebDriver driver, By by)
        {
            return driver.FindElements(by).FirstOrDefault()?.Displayed ?? false;
        }

        public static IWebElement SafeFindElement(this IWebDriver driver, By by)
        {
            return driver.FindElements(by).FirstOrDefault();
        }

        public static IWebElement SafeFindElementByDataAutoId(this IWebDriver driver, string dataAutoId)
        {
            if (AppConfig.session.Contains("appium"))
                return (IOSElement)driver.FindElement(By.XPath($".//*[@data-auto-id='{dataAutoId}']"));
            else
                return driver.SafeFindElement(By.XPath($".//*[@data-auto-id='{dataAutoId}']"));
        }

        public static IWebElement SafeFindElementById(this IWebDriver driver, string dataAutoId)
        {
            if (AppConfig.session.Contains("appium"))
                return (IOSElement)driver.FindElement(By.XPath($".//*[@id='{dataAutoId}']"));
            else
                return driver.SafeFindElement(By.XPath($".//*[@id='{dataAutoId}']"));
        }

        public static IWebElement SafeFindClickableElementByDataAutoId(this IWebDriver driver, string dataAutoId)
        {
            if (AppConfig.session.Contains("appium"))
            {

                var blah = (IOSElement)driver.FindElement(By.XPath($".//*[@data-auto-id='{dataAutoId}']"));
                return (IOSElement)driver.FindElement(By.XPath($".//*[@data-auto-id='{dataAutoId}']"));
            }
            else
                return driver.SafeFindClickableElement(By.XPath($".//*[@data-auto-id='{dataAutoId}']"));
        }

        public static IWebElement SafeFindClickableElementById(this IWebDriver driver, string id)
        {
            if (AppConfig.session.Contains("appium"))
                return (IOSElement)driver.FindElement(By.XPath($".//*[@id='{id}']"));
            else
                return driver.SafeFindClickableElement(By.XPath($".//*[@id='{id}']"));
        }

        public static IWebElement SafeFindClickableElement(this IWebDriver driver, IWebElement element)
        {
                return driver.WaitUntil(ExpectedConditions.ElementToBeClickable(element));
        }

        public static IWebElement SafeFindClickableElement(this IWebDriver driver, By by)
        {
                return driver.WaitUntil(ExpectedConditions.ElementToBeClickable(by));
        }

        public static void DragAndDropElement(this IWebDriver driver, IWebElement source, IWebElement destination)
        {
            try
            {
                Actions actionsBuilder = new Actions(driver);
                actionsBuilder.ClickAndHold(source).MoveToElement(destination).Release(destination).Build().Perform();
                WaitForJQueryAndJSToLoad(driver);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        public static TResult WaitUntil<TResult>(this IWebDriver driver, Func<IWebDriver, TResult> condition, int secondsTimeout = 15)
        {
            if (AppConfig.session.Contains("appium"))
            {
                secondsTimeout = 30;
                //IOSDriver<IWebElement> _iOSDriver = (IOSDriver<IWebElement>)driver;
                //_iOSDriver.Context = "WEBVIEW";
            }
            try
            {
                if (AppConfig.session.Contains("appium"))
                {
                    WaitForJQueryAndJSToLoad(driver, secondsTimeout);
                    IOSDriver<IWebElement> _iOSDriver = (IOSDriver<IWebElement>)driver;
                    var fluentWait = new DefaultWait<IOSDriver<IWebElement>>(_iOSDriver)
                    {
                        Timeout = TimeSpan.FromSeconds(secondsTimeout),
                        PollingInterval = TimeSpan.FromMilliseconds(250)
                    };
                    fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    fluentWait.IgnoreExceptionTypes(typeof(InvalidOperationException));
                    return fluentWait.Until(condition);
                }
                else
                {
                    WaitForJQueryAndJSToLoad(driver, secondsTimeout);
                    var fluentWait = new DefaultWait<IWebDriver>(driver)
                    {
                        Timeout = TimeSpan.FromSeconds(secondsTimeout),
                        PollingInterval = TimeSpan.FromMilliseconds(250)
                    };
                    fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    //IOSDriver<IWebElement> _iOSDriver = (IOSDriver<IWebElement>)driver;
                    //_iOSDriver.Context = "WEBVIEW";
                    return fluentWait.Until(condition);
                }
            }
            catch (WebDriverTimeoutException exTimeout)
            {
                Assert.Fail(exTimeout.ToString());
                return default(TResult);
            }
        }

        public static void WaitForJQueryAndJSToLoad(this IWebDriver driver, int secondsTimeout = 15)
        {
            if (AppConfig.session.Contains("appium")) secondsTimeout = 30;
            else
            {

                var waitDriver = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsTimeout));
                waitDriver.Until(dr =>
                {
                // wait for jQuery to load
                bool jQueryLoad;
                    try
                    {
                        jQueryLoad = (bool)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0");
                    }
                    catch (Exception)
                    {
                    // no jQuery present
                    jQueryLoad = true;
                    }

                // wait for Javascript to load
                bool jsLoad = false;
                    try
                    {
                        jsLoad = ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString().Equals("complete");
                    }
                    catch (Exception exJSLoad)
                    {
                        Assert.Fail(exJSLoad.ToString());
                    }

                    return jQueryLoad && jsLoad;
                });
            }
        }

        // Alternative to Thread.Sleep.  Default timer delay is 5 sec with max allow time of up to 60 sec.
        public static void Delay(this IWebDriver driver, int milliSecondsDelay = 5000, int maxTimeOutSeconds = 60)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 0, maxTimeOutSeconds));
            var delay = new TimeSpan(0, 0, 0, 0, milliSecondsDelay);
            var timestamp = DateTime.Now;
            wait.Until(webDriver => (DateTime.Now - timestamp) > delay);
        }


        // Search for a downloaded file using the Windows setting that Chrome as default
        public static FileInfo GetDownloadedFileInfo(this IWebDriver driver, string fileName)
        {
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = Path.Combine(pathUser, "Downloads");
            string[] filePaths = Directory.GetFiles(pathDownload);
            foreach (string filePath in filePaths)
            {
                if (filePath.Contains(fileName))
                {
                    FileInfo thisFile = new FileInfo(filePath);
                    if (thisFile.Length > 0)
                    {
                        return thisFile;
                    }
                }
            }

            return null;
        }
    }
}
