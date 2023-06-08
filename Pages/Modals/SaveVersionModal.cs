using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.ActionBars;

namespace ios_tests.Pages.Modals
{
    public class SaveVersionModal : BaseModal
    {
        private IWebDriver _webDriver;
        private const string ModalClassName = "modal--small";
        private const string ApplyButtonId = "modal__btn-apply";        
        private const string VersionTagX = "release_tag_x";
        private const string VersionTagY = "release_tag_y";
        private const string VersionTagZ = "release_tag_z";
        private readonly By _versionTagXLocator = By.XPath($".//*[contains(@data-auto-id,'{VersionTagX}')]//input");
        private readonly By _versionTagYLocator = By.XPath($".//*[contains(@data-auto-id,'{VersionTagY}')]//input");
        private readonly By _versionTagZLocator = By.XPath($".//*[contains(@data-auto-id,'{VersionTagZ}')]//input");        
        private const string VersionDescriptionPlaceholder = "Enter description (up to 300 characters)";
        private readonly By ModalClassLocator = By.ClassName(ModalClassName);

        public SaveVersionModal(IWebDriver webDriver) : base(webDriver, ModalClassName)
        {
            _webDriver = webDriver;
            Assert.IsTrue(IsModalDisplayed());
            Assert.IsTrue(DoPlaceholdersExist());
        }
        
        private IWebElement VersionTagXInput => _webDriver.SafeFindElement(_versionTagXLocator);

        private IWebElement VersionTagYInput => _webDriver.SafeFindElement(_versionTagYLocator);

        private IWebElement VersionTagZInput => _webDriver.SafeFindElement(_versionTagZLocator);

        private IWebElement VersionDescriptionInput => _webDriver.SafeFindElement(new ByChained(ModalClassLocator, By.XPath($"//textarea[@placeholder='{VersionDescriptionPlaceholder}']")));

        private IWebElement ApplyButton => _webDriver.SafeFindElementByDataAutoId(ApplyButtonId);
        
        private bool DoPlaceholdersExist()
        {
            Assert.AreEqual(VersionDescriptionPlaceholder, VersionDescriptionInput.GetAttribute("placeholder"));
            return true;
        }

        private bool IsSaveEnabled()
        {
            Assert.IsTrue(ApplyButton.Displayed && ApplyButton.Enabled);
            return true;
        }

        public SaveVersionModal TypeVersionTag(string versionTagX, string versionTagY, string versionTagZ)
        {
            VersionTagXInput.Clear();
            VersionTagXInput.SendKeys(versionTagX);
            VersionTagYInput.Clear();
            VersionTagYInput.SendKeys(versionTagY);
            VersionTagZInput.Clear();
            VersionTagZInput.SendKeys(versionTagZ);
            return this;
        }

        public SaveVersionModal TypeVersionDescription(string description)
        {
            VersionDescriptionInput.SendKeys(description);
            return this;
        }

        public ViewModeBar ConfirmSave()
        {
            Assert.IsNotEmpty(VersionTagXInput.GetAttribute("value"));
            Assert.IsNotEmpty(VersionTagYInput.GetAttribute("value"));
            Assert.IsNotEmpty(VersionTagZInput.GetAttribute("value"));
            Assert.IsNotEmpty(VersionDescriptionInput.GetAttribute("value"));
            Assert.IsTrue(IsSaveEnabled());

            ApplyButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(ModalWindow));
            return new ViewModeBar(_webDriver);
        }

        public DesignModeBar CancelSave()
        {
            ClickCancelButton();
            return new DesignModeBar(_webDriver);
        }
    }
}
