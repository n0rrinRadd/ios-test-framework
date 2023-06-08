using Framework.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;

namespace ios_tests.Pages.PageBodies.Components
{
    public class EditName
    {
        protected IWebDriver _webDriver;
        private readonly By _designTitleLocator;

        private const string EditNameButtonId = "design__title-edit-btn";
        private const string ApplyButtonId = "design__rename-apply-btn";
        private const string CancelButtonId = "design__rename-cancel-btn";
        private const string RenameWarningId = "design__rename-warning";

        private IWebElement EditNameInput => _webDriver.SafeFindElement(new ByChained(_designTitleLocator, By.XPath("//input")));        

        private IWebElement EditNameButton => _webDriver.SafeFindClickableElementByDataAutoId(EditNameButtonId);

        private IWebElement EditNameApplyButton => _webDriver.SafeFindElementByDataAutoId(ApplyButtonId);

        private IWebElement EditNameCancelButton => _webDriver.SafeFindClickableElementByDataAutoId(CancelButtonId);

        private IWebElement RenameWarning => _webDriver.SafeFindElementByDataAutoId(RenameWarningId);

        public EditName(IWebDriver webDriver, By titleLocator)
        {
            _webDriver = webDriver;
            _designTitleLocator = titleLocator;
        }

        public bool IsEditNameButtonDisplayed()
        {
            Assert.IsTrue(EditNameButton.Displayed);
            return true;
        }

        private EditName ClickEditButton()
        {
            var button = EditNameButton;
            button.Click();
            if (!AppConfig.session.Contains("appium")) _webDriver.WaitUntil(ExpectedConditions.StalenessOf(button));
            Assert.IsTrue(EditNameApplyButton.Displayed);
            Assert.IsTrue(EditNameCancelButton.Displayed);
            return this;
        }

        public string GetName()
        {
            return EditNameInput.GetAttribute("value");
        }

        private EditName SetName(string Name)
        {
            EditNameInput.Clear();
            Assert.IsFalse(EditNameApplyButton.Enabled);

            EditNameInput.SendKeys(Name);
            Assert.IsTrue(EditNameApplyButton.Enabled);
            return this;
        }

        public EditName SetName_WithoutCheck(string name)
        {
            EditNameInput.Clear();
            EditNameInput.SendKeys(name);
            return this;
        }

        private EditName ApplyName()
        {
            Assert.IsTrue(EditNameApplyButton.Enabled);
            var button = EditNameApplyButton;
            button.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(button));
            return this;
        }

        public EditName EditName_WithApply(string Name)
        {
            var returnPage =
                ClickEditButton()
                .SetName(Name)
                .ApplyName();
            Assert.AreEqual(Name, GetName());
            return returnPage;
        }

        public EditName EditName_WithUniqueCheck(string ChapterName)
        {
            string beforeRename = GetName();

            var returnPage =
                ClickEditButton()
                .SetName_WithoutCheck(ChapterName);

            Assert.IsFalse(EditNameApplyButton.Enabled);
            return returnPage;
        }

        public EditName DuplicateNameWarningDisplayed()
        {
            Assert.IsNotNull(RenameWarning);
            return this;
        }

        public string DuplicateNameWarningMessage()
        {
            Assert.IsNotNull(RenameWarning);
            return RenameWarning.Text;
        }
    }
}
