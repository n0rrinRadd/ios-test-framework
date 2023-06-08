using Framework.Selenium;
using Framework.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using ios_tests.Helpers;

namespace ios_tests.Pages.PageBodies
{
    public class SettingsBody
    {
        private IWebDriver _webDriver;
        private const string AboutLinkId = "btn-about";
        private const string LogoutButtonId = "btn-logout";
        private const string TenantButtonId = "btn-tenants";
        private const string AppVersionSettingId = "version-number";
        private const string PrivacyPolicyLinkId = "privacy-policy-link";
        private const string PrivacyPolicyURL = "https://www.shyftanalytics.com/privacy-policy/";
        private const string ClientBoxClassName = "box__selection";
        private const string TenantOneSelector = "item-option-M00000000";
        private const string TenantTwoSelector = "item-option-M00010000";
        private readonly By _clientBoxSelector = By.ClassName(ClientBoxClassName);

        public SettingsBody(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private IWebElement AboutLink => _webDriver.SafeFindClickableElementByDataAutoId(AboutLinkId);

        private IWebElement AppVersion => _webDriver.SafeFindElementByDataAutoId(AppVersionSettingId);

        private IWebElement PrivacyPolicyLink => _webDriver.SafeFindClickableElementByDataAutoId(PrivacyPolicyLinkId);

        private IWebElement LogoutButton => _webDriver.SafeFindClickableElementByDataAutoId(LogoutButtonId);

        private IWebElement TenantButton => _webDriver.SafeFindClickableElementByDataAutoId(TenantButtonId);

        private ElementFactory ClientList => new ElementFactory(_webDriver, _clientBoxSelector);

        public IWebElement TenantOneOption => _webDriver.SafeFindClickableElementByDataAutoId(TenantOneSelector);

        public IWebElement TenantTwoOption => _webDriver.SafeFindClickableElementByDataAutoId(TenantTwoSelector);

        public SettingsBody ClickAboutLink()
        {
            AboutLink.Click();
            return this;
        }

        public bool VersionNumberIsProperlyFormatted()
        {
            string[] versionNumberSplit = ReadAppVersion().Split('.');
            return (versionNumberSplit.Length == 4);
        }

        public string ReadAppVersion()
        {
            return AppVersion.Text;
        }

        public void OpenPrivacyPolicy()
        {
            PrivacyPolicyLink.Click();
            Assert.IsTrue(PrivacyPolicyLink.GetAttribute("href").Contains(PrivacyPolicyURL), $"privacy policy link contains {PrivacyPolicyURL}");
        }

        public void GoToPrivacyPolicy()
        {
            OpenPrivacyPolicy();
            Utils.GoToTab(_webDriver, 1);
            Assert.IsTrue(_webDriver.Url.Contains(PrivacyPolicyURL), $"new tab generated from clicking privacy policy link contains url {PrivacyPolicyURL}");
        }

        public void ClickLogoutButton()
        {
            LogoutButton.Click();
        }

        public void OpenTenantSection()
        {
            TenantButton.Click();
        }

        public void ChangeClient(string clientName)
        {
            var boxFilter = ClientList.elements[0];
            boxFilter.Click();

            if (clientName == "M00000000")
            {
                TenantOneOption.Click();
            }
            else
            {
                TenantTwoOption.Click();
            }
        }

        public void AssertClientSelected(string clientName)
        {
            var boxFilter = ClientList.elements[0];
            Assert.AreEqual(boxFilter.Text, clientName);
        }
    }
}
