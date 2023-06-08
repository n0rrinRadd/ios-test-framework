using Framework.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Page.Modals;
using ios_tests.Pages.Metadata;
using ios_tests.Pages.Modals;

namespace ios_tests.Pages.ActionBars
{
    public class DesignModeBar
    {
        private readonly IWebDriver _webDriver;
        private const string DiscardButtonId = "btn-design-mode_discard";
        private const string SaveButtonId = "btn-design-mode_publish";
        private const string VersionButtonId = "btn-versions";

        private const string MetadataButtonId = "btn-design-mode_metadata";
        private const string METADATA_ROUTE = "metadata";

        public DesignModeBar(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            Assert.True(IsDesignModeActive());
        }

        private IWebElement DiscardButton => _webDriver.SafeFindClickableElementByDataAutoId(DiscardButtonId);

        private IWebElement SaveButton => _webDriver.SafeFindClickableElementByDataAutoId(SaveButtonId);

        private IWebElement VersionButton => _webDriver.SafeFindClickableElementByDataAutoId(VersionButtonId);

        private IWebElement MetadataButton => _webDriver.SafeFindClickableElementByDataAutoId(MetadataButtonId);

        public bool IsDesignModeActive()
        {
            Assert.IsTrue(DiscardButton.Displayed);
            Assert.IsTrue(SaveButton.Displayed);
            return true;
        }

        public DiscardSessionModal DiscardDesignSession()
        {
            DiscardButton.Click();
            return new DiscardSessionModal(_webDriver);
        }

        public SaveVersionModal SaveDesignVersion()
        {
            SaveButton.Click();
            return new SaveVersionModal(_webDriver);
        }

        public void SaveDesignVersion(string version, string description)
        {            
            var saveVersionModal = SaveDesignVersion();
            Assert.IsTrue(version.Split('.').Length - 1 == 2);
            string[] versionTags = version.Split('.');
            saveVersionModal.TypeVersionTag(versionTags[0], versionTags[1], versionTags[2]);
            saveVersionModal.TypeVersionDescription(description);
            saveVersionModal.ConfirmSave();
        }

        public VersionListModal ShowVersions()

        {
            VersionButton.Click();
            return new VersionListModal(_webDriver);
        }
        
        public MetadataPage EnterStoryMetaData()
        {
            MetadataButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.UrlContains(METADATA_ROUTE));
            return new MetadataPage(_webDriver);
        }
    }
}
