using ios_tests.Pages;
using ios_tests.Pages.Modals;

namespace ios_tests.Page.Modals
{
    using System.Collections.ObjectModel;
    using Helpers;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using Pages.ActionBars;
    using Pages.PageBodies;


    public class VersionListModal : BaseModal
    {
        private IWebDriver _webDriver;
        private const string ModalClassName = "modal";
        private const string TableRowClassName = "table__row";        
        private readonly By TableRowClassLocator = By.ClassName(TableRowClassName);
        
        private ReadOnlyCollection<IWebElement> TableRows => _webDriver.FindElements(TableRowClassLocator);
        
        public VersionListModal(IWebDriver webDriver) : base(webDriver, ModalClassName)
        {
            _webDriver = webDriver;
            _webDriver.WaitForJQueryAndJSToLoad();
            Assert.IsTrue(IsModalDisplayed());
        }
        
        public int GetVersionCount()
        {
            _webDriver.WaitForJQueryAndJSToLoad();

            // one less because the header also is a row
            return TableRows.Count -1;
        }

        public DesignModeBar Close()
        {
            ClickCancelButton();            
            return new DesignModeBar(_webDriver);
        }

        public StoryBody GoToVersionFromViewMode(int versionIndex)
        {
            Assert.IsFalse(BasePage.IsDesignModeUrlActive);
            TableRows[versionIndex].Click();
            _webDriver.WaitForJQueryAndJSToLoad();
           
            return new StoryBody(_webDriver);
        }

        public DiscardSessionModal GoToVersionFromDesign(int versionIndex)
        {
            Assert.IsTrue(BasePage.IsDesignModeUrlActive);
            TableRows[versionIndex].Click();
            _webDriver.WaitForJQueryAndJSToLoad();

            return new DiscardSessionModal(_webDriver);
        }
    }
}