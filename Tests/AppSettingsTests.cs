using Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;

namespace ios_tests.Tests
{
    [TestFixture, Category("settings")]
    public class AppSettingsTests : BaseTest
    {
        /// <summary>
        /// LUMI-173
        /// Users can access the version number of the app (e.g. 3.12.1)
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Access_The_Version_Number_Of_the_App()
        {
            var settings = _basePage.NavigationPanel.Fixed.OpenSettings();
            settings.ClickAboutLink();
            settings.VersionNumberIsProperlyFormatted();
        }

        /// <summary>
        /// LUMI-173
        /// Users can access the privacy policy
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Access_The_Privacy_Policy()
        {
            var settings = _basePage.NavigationPanel.Fixed.OpenSettings();
            settings.ClickAboutLink();
            settings.OpenPrivacyPolicy();
            _basePage.NavigationPanel.Fixed.CloseSettings();
        }

        /// <summary>
        /// LUMI-173
        /// Users can navigate to privacy policy
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Navigate_To_Privacy_Policy()
        {
            var settings = _basePage.NavigationPanel.Fixed.OpenSettings();
            settings.ClickAboutLink();
            settings.GoToPrivacyPolicy();
            _basePage.NavigationPanel.Fixed.CloseSettings();
        }

        /// <summary>
        /// LUMI-1084
        /// Users can see the correct log-in email address
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CISmokeTests")]
        public void User_Can_See_Displayed_Login_Email_Address()
        {
            var settings = _basePage.NavigationPanel.Fixed.OpenSettings();
            //Assert.IsTrue(settings.GetUserEmail().Equals(Domain.Constants.TenantAndUserId.TestUserEmail));
        }

        /// <summary>
        /// LUMI-528
        /// Left Navigation Panel not visible in Settings page
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Navigation_Panel_Not_Visible_When_Settings_Open()
        {
            Assert.IsFalse(_basePage.NavigationPanel.FocusedCollapsible.IsContentHidden());
            _basePage.NavigationPanel.Fixed.OpenSettings();
            Assert.IsTrue(_basePage.NavigationPanel.FocusedCollapsible.IsContentHidden());
        }

        ///// <summary>
        ///// LUMI-922
        ///// Users can click logout to sign out of Auth0 ios.
        ///// </summary>
        [Test, RetryDynamic, Category("ReportingServer")]
        public void User_Can_Sign_Out_Auth0()
        {
            var settings = _basePage.NavigationPanel.Fixed.OpenSettings();
            settings.ClickLogoutButton();
            _basePage.SetLoggedOut();
            //Assert.True(_basePage.LoginToAuth0(Domain.Constants.TenantAndUserId.TestUserEmail, Domain.Constants.TenantAndUserId.TestUserPassword));
        }


        /// <summary>
        /// LUMI-938 Tenant Switching Test
        /// Users can switch to a different tenant if multiple tenants are available
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CISmokeTests")]
        public void User_Can_Switch_Tenants_On_The_Client_Page()
        {
            var settingsPage = _basePage.NavigationPanel.Fixed.OpenSettings();
            settingsPage.OpenTenantSection();
            //settingsPage.AssertClientSelected(Domain.Constants.TenantAndUserId.TestTenantId1);

           // settingsPage.ChangeClient(Domain.Constants.TenantAndUserId.TestTenantId2);
           // _webDriver.WaitUntil(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(Domain.Constants.VisualObjectUrl.ParamStories));

            // Make sure the active tenant actually switched
            settingsPage = _basePage.NavigationPanel.Fixed.OpenSettings();
            settingsPage.OpenTenantSection();
            //settingsPage.AssertClientSelected(Domain.Constants.TenantAndUserId.TestTenantId2);

            // Then switch back
            //settingsPage.ChangeClient(Domain.Constants.TenantAndUserId.TestTenantId1);
            //_webDriver.WaitUntil(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains(Domain.Constants.VisualObjectUrl.ParamStories));
        }
    }
}
