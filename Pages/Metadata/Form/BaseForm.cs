using NUnit.Framework;
using OpenQA.Selenium;
using ios_tests.Helpers;

namespace ios_tests.Pages.Metadata.Form
{
    public class BaseForm
    {
        private readonly IWebDriver _webDriver;

        private const string CancelButtonId = "btn-metadata__cancel";
        private const string ApplyButtonId = "btn-metadata__apply";

        protected IWebElement CancelButton => _webDriver.SafeFindClickableElementByDataAutoId(CancelButtonId);
        protected IWebElement ApplyButton => _webDriver.SafeFindElementByDataAutoId(ApplyButtonId);

        public BaseForm(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        public bool IsApplyEnabled()
        {
            return ApplyButton.Enabled;
        }

        protected void ClickApply()
        {
            Assert.IsTrue(IsApplyEnabled());
            ApplyButton.Click();
        }
    }
}
