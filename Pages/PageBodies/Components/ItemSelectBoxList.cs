using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ios_tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ios_tests.Pages.PageBodies.Components
{
    public class ItemSelectBoxList
    {
        protected IWebDriver _webDriver;
        private By _rootLocator;
        private const string OptionIdPrefix = "item-option-";
        private const string OptionDisabledClass = "is-disabled";


        private readonly By _listItemLinkLocator = By.XPath($".//*[contains(@data-auto-id, '{OptionIdPrefix}')]");
        private readonly By _disabledOptionLocator = By.XPath($".//*[contains(@class, '{OptionDisabledClass}')]");

        internal IReadOnlyCollection<IWebElement> ItemLinks => _webDriver.FindElements(_listItemLinkLocator);
        internal IReadOnlyCollection<IWebElement> DisabledItemLinks => _webDriver.FindElements(_disabledOptionLocator);

        public ItemSelectBoxList(IWebDriver webDriver, By rootLocator)
        {
            _webDriver = webDriver;
            _rootLocator = rootLocator;
        }

        public void ClickOption(string selectedOption)
        {
            string dataAutoId = String.Concat(OptionIdPrefix, selectedOption);
            By locator = new ByChained(_rootLocator, By.XPath($"//*[contains(@data-auto-id,'{dataAutoId}')]"));

            if (!_webDriver.SafeFindElement(locator).Enabled)
            {
                return;
            }

            _webDriver.SafeFindClickableElement(locator).Click();
        }

        public List<string> GetAllEnabledOptionNames()
        {
            var listItems = ItemLinks.ToList();
            var disabledItems = DisabledItemLinks.ToList();

            return listItems.Except(disabledItems).Select(e => e.Text).ToList();
        }
    }
}
