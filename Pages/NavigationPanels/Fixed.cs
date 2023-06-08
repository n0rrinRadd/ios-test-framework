using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.PageBodies;

namespace ios_tests.Pages.NavigationPanels
{
    public class Fixed
    {
        private IWebDriver _webDriver;
        private NavigationPanel _parentNav;
        private const string iosIconExpandButtonId = "btn-nav-bar-fixed__expand";
        private const string StoryButtonId = "btn-nav-bar-fixed__story";
        private const string SettingsButtonId = "btn-nav-bar-fixed__settings";


        public Fixed(NavigationPanel parent, IWebDriver webDriver)
        {
            _parentNav = parent;
            _webDriver = webDriver;
        }

        private IWebElement ExpandNavButton => _webDriver.SafeFindClickableElementByDataAutoId(iosIconExpandButtonId);

        private IWebElement StoryButton => _webDriver.SafeFindClickableElementByDataAutoId(StoryButtonId);

        private IWebElement SettingsButton => _webDriver.SafeFindClickableElementByDataAutoId(SettingsButtonId);

        public PageBody ReturnToStory()
        {
            StoryButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.UrlContains("stories"));
            return new PageBody(_webDriver);
        }

        public SettingsBody OpenSettings()
        {
            SettingsButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.UrlContains("settings"));
            Assert.IsTrue(_webDriver.Url.Contains("settings"));
            return new SettingsBody(_webDriver);
        }

        public void CloseSettings()
        {
            var tabs = _webDriver.WindowHandles;
            if (tabs.Count > 1)
            {
                _webDriver.SwitchTo().Window(tabs[1]);
                _webDriver.Close();
                _webDriver.SwitchTo().Window(tabs[0]);
            }
        }

        public void ExpandCollapsibleNavBar()
        {
            ExpandNavButton.Click();

            _webDriver.WaitUntil(driver => !_parentNav.FocusedCollapsible.IsNavPanelAnimating());
        }
    }
}