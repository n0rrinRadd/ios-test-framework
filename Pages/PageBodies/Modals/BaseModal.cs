using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;

namespace ios_tests.Pages.Modals
{
    public class BaseModal
    {
        private readonly IWebDriver _webDriver;
        private string ModalClassName = "modal--small";
        private const string ModalHeaderId = "modal-header";
        private const string CancelButtonId = "modal__btn-cancel";        

        protected IWebElement ModalWindow => _webDriver.SafeFindElement(By.ClassName(ModalClassName));
        private IWebElement ModalHeader => _webDriver.SafeFindClickableElementByDataAutoId(ModalHeaderId);
        private IWebElement CancelButton => _webDriver.SafeFindClickableElementByDataAutoId(CancelButtonId);
        

        public BaseModal(IWebDriver webDriver, string modalClassName)
        {
            _webDriver = webDriver;
            ModalClassName = modalClassName;
        }

        public bool IsModalDisplayed()
        {
            Assert.IsTrue(ModalWindow.Displayed);
            Assert.IsTrue(ModalWindow.Enabled);
            return true;
        }

        public string GetModalHeaderTitle()
        {
            return ModalHeader.Text;
        }

        public bool IsCancelEnabled()
        {
            return CancelButton.Enabled;
        }

        public void ClickCancelButton()
        {
            Assert.IsTrue(IsCancelEnabled());
            CancelButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(ModalWindow));
        }
    }
}
