using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;

namespace ios_tests.Pages.Metadata
{
    public class BasePage
    {
        private readonly IWebDriver _webDriver;

        private string _addButtonId;
        private const string AddButtonIdPrefix = "btn-metadata__add-";
        private const string ListItemDataIdPrefix = "searchable-list-item__";

        protected SearchableList ListOfItems;
        protected IWebElement AddButton => _webDriver.SafeFindClickableElementByDataAutoId(_addButtonId);

        protected BasePage(IWebDriver webDriver, string type)
        {
            _webDriver = webDriver;
            _addButtonId = AddButtonIdPrefix + type;
            ListOfItems = new SearchableList(_webDriver, type);
        }

        protected List<string> GetItemNames()
        {
            return ListOfItems.ItemLinks.Select(d => d.Text).ToList();
        }

        protected bool DoesItemExist(string name)
        {
            return GetItemNames().Contains(name);
        }

        protected T GoToItem<T>(string name)
        {
            ListOfItems.GoToItem(name);
            return (T)Activator.CreateInstance(typeof(T), _webDriver);
        }

        protected T ClickAddItem<T>()
        {
            AddButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.UrlContains("new"));
            return (T)Activator.CreateInstance(typeof(T), _webDriver);
        }

        protected T DeleteItem<T>(string name)
        {
            return ListOfItems.DeleteItem(name).DeleteItem<T>();
        }

        public void WaitForItemToBeAdded(string name)
        {
            By listItemDataAutoId = By.XPath($".//*[contains(@data-auto-id,'{ListItemDataIdPrefix}{name}')]");

            _webDriver.WaitUntil(ExpectedConditions.ElementExists(listItemDataAutoId));
        }
    }
}
