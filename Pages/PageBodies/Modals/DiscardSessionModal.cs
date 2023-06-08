using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.ActionBars;

namespace ios_tests.Pages.Modals
{
    public class DiscardSessionModal : BaseModal
    {
        private IWebDriver _webDriver;
        private const string ModalClassName = "modal--small";
        private const string ApplyButtonId = "modal__btn-apply";
        
        public DiscardSessionModal(IWebDriver webDriver) : base(webDriver, ModalClassName)
        {
            _webDriver = webDriver;
            Assert.IsTrue(IsModalDisplayed());
        }

        private IWebElement ApplyButton => _webDriver.SafeFindClickableElementByDataAutoId(ApplyButtonId);

        public ViewModeBar ConfirmDiscard()
        {
            IWebElement modalWindow = ModalWindow;
            ApplyButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(modalWindow));
            return new ViewModeBar(_webDriver);
        }

        public DesignModeBar CancelDiscard()
        {
            ClickCancelButton();            
            return new DesignModeBar(_webDriver);
        }
    }
}
