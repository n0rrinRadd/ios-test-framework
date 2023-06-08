using Framework.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.Authorization;
using System;
using System.Threading;

namespace ios_tests.Pages
{
    public class BasePage
    {
        protected static IWebDriver _webDriver;
        private static bool _isLoggedIn = false;

        private const string DESIGN_ROUTE = "design";
        internal const string PendingSnapshotPrefix = "Pending_";
        private const string Auth0LoginURL = "auth0.com/login";

        public BasePage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string Auth0LorginUrl => Auth0LoginURL;

        public static string PageUrl => _webDriver.Url;

        public string ActiveStory
        {
            get
            {
                var urlSegments = PageUrl.Replace(Config.BaseUrl, string.Empty).Split('/');

                if (!(urlSegments.Length >= 3 && urlSegments[1] == "stories"))
                {
                    return null;
                }

                var activeStory = PageUrl.Contains(PendingSnapshotPrefix) ? urlSegments[3] : urlSegments[2];
                return activeStory;
            }
        }

        public string ActiveChapter
        {
            get
            {
                var urlSegments = PageUrl.Replace(Config.BaseUrl, string.Empty).Split('/');

                if (!(urlSegments.Length >= 5 && urlSegments[3] == "chapters"))
                {
                    return null;
                }

                var activeChapter = PageUrl.Contains(PendingSnapshotPrefix) ? urlSegments[5] : urlSegments[4];
                return activeChapter;
            }
        }

        public static bool IsDesignModeUrlActive => PageUrl.Contains(DESIGN_ROUTE);

        public ActionBar ActionBar => new ActionBar(_webDriver);

        public NavigationPanel NavigationPanel => new NavigationPanel(_webDriver);

        public PageBody PageBody => new PageBody(_webDriver);

        public void GoToMainPage()
        {
            _webDriver.Navigate().GoToUrl(Config.BaseUrl);
            _webDriver.WaitUntil(ExpectedConditions.UrlContains(Config.BaseUrl));
            _webDriver.WaitUntil(ExpectedConditions.UrlContains("stories"));
        }

        public bool LoginToAuth0()
        {
            if (!_isLoggedIn)
            {
                try
                {
                    {
                        _webDriver.Navigate().GoToUrl(Config.BaseUrl);
                        Thread.Sleep(5000);
                        //_webDriver.WaitForJQueryAndJSToLoad();
                        if (_webDriver.Url.Contains(Auth0LoginURL))
                        {
                            LoginAuthorization();
                        }
                        _webDriver.WaitUntil(ExpectedConditions.UrlContains(Config.BaseUrl), 30);
                        _webDriver.WaitUntil(ExpectedConditions.UrlContains("/#/"));
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(">> Login errors:\n" + ex.ToString());
                    return false;
                }
            }

            return _isLoggedIn = true;
        }

        private void LoginAuthorization()
        {
            // Wait for all processes to complete
            _webDriver.WaitForJQueryAndJSToLoad();

            // After load, if you've been redirected, commence login.
            // Otherwise, assume login token has been preserved
            _webDriver.WaitUntil(driver =>
            {
                var currentUrl = PageUrl;

                // authorization saved from earlier session
                if (currentUrl.Equals(Config.BaseUrl))
                {
                    return true;
                }

                bool response = false;

                // authorization required
                if (currentUrl.Contains(Auth0LoginURL))
                {
                    response = Auth0Authorization();
                }

                return response;
            }, 30);
        }

        public void SetLoggedOut()
        {
            _isLoggedIn = false;
        }

        private bool Auth0Authorization()
        {
            string email = "testuser@nodomain.com";
            string password = "Shyft123";
            //_webDriver.Manage().Cookies.DeleteAllCookies();
            _webDriver.WaitUntil(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("email")), 15);
            IOSDriver<IWebElement> _iOSDriver = (IOSDriver<IWebElement>)_webDriver;
            IWebElement emailField = _iOSDriver.FindElement(By.Name("email"));
            emailField.Click();
            if (emailField.Text == "")_iOSDriver.Keyboard.SendKeys(email);
            IWebElement passwordField = _iOSDriver.FindElement(By.Name("password"));
            passwordField.Click();
            if (passwordField.Text == "")_iOSDriver.Keyboard.SendKeys(password);
            _webDriver.FindElement(By.XPath("//button[@type='submit']")).Click();
            return true;
        }

        public void WaitForChapterTilesRendered(int secondsTimeout = 15)
        {
            _webDriver.WaitUntil(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName("chapter-tile__title")), secondsTimeout);
        }
    }
}
