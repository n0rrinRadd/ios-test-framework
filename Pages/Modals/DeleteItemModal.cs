using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;

namespace ios_tests.Pages.Modals
{
    public class DeleteItemModal : BaseModal
    {
        private IWebDriver _webDriver;
        private const string ModalClassName = "modal--small";
        private const string DeleteButtonId = "modal__btn-apply";

        private IWebElement DeleteButton => _webDriver.SafeFindClickableElementByDataAutoId(DeleteButtonId);
        
        public DeleteItemModal(IWebDriver webDriver) : base(webDriver, ModalClassName)
        {
            _webDriver = webDriver;
            Assert.IsTrue(IsModalDisplayed());
        }
        
        public void DeleteItem()
        {
            DeleteButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(ModalWindow));
        }

        public T DeleteItem<T>()
        {
            DeleteItem();
            return (T)Activator.CreateInstance(typeof(T), _webDriver);
        }

        public T CancelDelete<T>()
        {
            ClickCancelButton();
            return (T)Activator.CreateInstance(typeof(T), _webDriver);
        }

        public string GetDeleteButtonLabel()
        {
            return DeleteButton.Text;
        }
    }
}
