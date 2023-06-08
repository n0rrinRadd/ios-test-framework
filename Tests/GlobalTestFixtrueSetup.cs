using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using Framework.Config;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;

namespace ios_tests.Tests
{
    [SetUpFixture]
    public class GlobalTestFixtrueSetup
    {
        private static IWebDriver _webDriver;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _webDriver = GetAppiumDriver();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            try
            {
                if (WebDriver != null)
                {
                    WebDriver.Close();
                    WebDriver.Quit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("=> " + TestContext.CurrentContext.Test.FullName.ToString() + " TearDown errors:  " + ex.ToString());
            }
        }

        public static IWebDriver WebDriver
        {
            get { return _webDriver; }
        }

        public static IOSDriver<IWebElement> GetAppiumDriver()
        {
            DesiredCapabilities cap = new DesiredCapabilities();
            cap.SetCapability("platformName", "iOS");
            cap.SetCapability("platformVersion", "12.1");
            cap.SetCapability("deviceName", AppConfig.deviceName);
            cap.SetCapability(MobileCapabilityType.Udid, AppConfig.UDID);
            cap.SetCapability("automationName", "XCUITest");
            cap.SetCapability("xcodeOrgId", AppConfig.xcodeOrgId);
            cap.SetCapability("xcodeSigningId", AppConfig.xcodeSigningId);
            //cap.SetCapability("updatedWDABundleId", "io.appium.WebDriverAgentRunner");
            cap.SetCapability("nativeWebTap", true);
            //cap.SetCapability("showXcodeLog", true);
            //cap.SetCapability("resetKeyboard", true);
            //cap.SetCapability("autoWebView", true);
            cap.SetCapability(MobileCapabilityType.BrowserName, "Safari");
            cap.SetCapability(MobileCapabilityType.NewCommandTimeout, 100);
            return new IOSDriver<IWebElement>(new Uri("http://" + AppConfig.appiumServerIP + ":" + AppConfig.appiumServerPort + "/wd/hub"), cap);
        }
    }
}
