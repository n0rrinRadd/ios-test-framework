using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ios_tests.Helpers;

namespace ios_tests.Pages.PageBodies.Components
{
    public class ItemSelect
    {
        protected IWebDriver _webDriver;
        private By _rootLocator;

        public ItemSelect(IWebDriver webDriver, By rootLocator)
        {
            _webDriver = webDriver;
            _rootLocator = rootLocator;
        }

        public void ClickOption(string selectedOption)
        {
            By locator = new ByChained(_rootLocator, By.XPath($"//*[contains(@data-auto-id,'{selectedOption}')]"));

            if (!_webDriver.SafeFindElement(locator).Enabled)
            {
                return;
            }

            _webDriver.SafeFindClickableElement(locator).Click();
        }

        public string SelectedOption
        {
            get
            {
                By locator = new ByChained(_rootLocator, By.XPath("//*[contains(@class,'disabled')]"));

                return _webDriver.SafeFindElement(locator).Text;
            }
        }
    }
}
