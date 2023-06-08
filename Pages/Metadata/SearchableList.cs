using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ios_tests.Pages.Modals;

namespace ios_tests.Pages.Metadata
{
    public class SearchableList
    {
        private readonly IWebDriver _webDriver;

        private string SearchableListId;
        private const string SearchableListItemPrefix = "searchable-list-item__";
        private const string SearchableListItemLinkId = "item-link";
        private const string SearchableListItemDeleteButtonId = "btn-item__delete";


        private readonly By _listItemLocator = By.XPath($".//*[contains(@data-auto-id, '{SearchableListItemPrefix}')]");
        private readonly By _listItemLinkLocator = By.XPath($".//*[@data-auto-id='{SearchableListItemLinkId}']");

        internal IReadOnlyCollection<IWebElement> ItemLinks => _webDriver.FindElements(_listItemLinkLocator);

        public SearchableList(IWebDriver webDriver, string type)
        {
            _webDriver = webDriver;
            SearchableListId = "searchable-list__" + type;
        }

        public IReadOnlyCollection<IWebElement> GetItemLinks()
        {
            return ItemLinks;
        }

        public void GoToItem(string name)
        {
            string itemId = SearchableListItemPrefix + name;
            By itemLocator = By.XPath($".//*[contains(@data-auto-id, '{itemId}')]");

            IWebElement itemLink = _webDriver.FindElement(new ByChained(itemLocator, By.XPath($".//*[@data-auto-id='{SearchableListItemLinkId}']")));
            itemLink.Click();
        }

        public DeleteItemModal DeleteItem(string name)
        {
            string itemId = "searchable-list-item__" + name;
            By itemLocator = By.XPath($".//*[contains(@data-auto-id, '{itemId}')]");

            IWebElement deleteItemButton = _webDriver.FindElement(new ByChained(itemLocator, By.XPath($".//*[@data-auto-id='{SearchableListItemDeleteButtonId}']")));
            deleteItemButton.Click();

            return new DeleteItemModal(_webDriver);
        }
    }
}
