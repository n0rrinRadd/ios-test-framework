using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Framework.Selenium;
using Framework.Utils;
using Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using ios_tests.Pages;

namespace ios_tests.Tests
{
    [TestFixture]    
    [Parallelizable(ParallelScope.None)]
    public class BaseTest
    {
        protected static IWebDriver _webDriver;
        protected BasePage _basePage;
        private Stopwatch _stopwatch = new Stopwatch();
        
        [SetUp]
        public void GoToMainPage()
        {
            if (_webDriver == null)
            {
                _webDriver = GlobalTestFixtrueSetup.WebDriver;
            }

            _basePage = new BasePage(_webDriver);
            if (!_basePage.LoginToAuth0())
            {
                Assert.Fail(">> FAILED to initialize WebDriver.");
            }

            if (BasePage.IsDesignModeUrlActive)
            {
                CleanExistingDesignSession();
            }

            // Reset to main page
            _basePage.GoToMainPage();
            _stopwatch.Restart();

            System.Console.WriteLine("=> " + TestContext.CurrentContext.Test.FullName.ToString() + "...RUNNING");
        }

        [TearDown]
        public void PrintTestStatus()
        {
            Console.WriteLine($"=> {TestContext.CurrentContext.Test.FullName}...{TestContext.CurrentContext.Result.Outcome.Status.ToString().ToUpper()}  :  Elapsed Time: {_stopwatch.Elapsed}");
        }

        private void CleanExistingDesignSession()
        {
            var modal = _basePage.ActionBar.DesignModeBar.DiscardDesignSession();
            modal.ConfirmDiscard();
        }

        internal string GetScreenshot(string name)
        {
            string screenshotValue = DriverUtils.GetScreenshot(_webDriver, name);
            return screenshotValue;
        }

        public void Check_JavaScript_Errors()
        {
            // Six different values can be returned by the error name property
            var errorStrings = new List<string>
            {                
                "EvalError",                
                "RangeError",
                "ReferenceError",
                "SyntaxError",
                "TypeError",
                "URIError"
            };
                        
            var jsErrors = _webDriver.Manage().Logs.GetLog(LogType.Browser).Where(x => errorStrings.Any(e => x.Message.Contains(e)));            
            if (jsErrors.Any())
            {
                Assert.Fail();
            }
        }
    }
}
