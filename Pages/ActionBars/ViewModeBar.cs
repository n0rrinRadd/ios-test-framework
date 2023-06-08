using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Page.Modals;

namespace ios_tests.Pages.ActionBars
{
    public class ViewModeBar
    {
        private IWebDriver _webDriver;
        private const string DesignModeButtonId = "btn-design-mode_enter";
        private const string VersionButtonId = "btn-versions";

        public ViewModeBar(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private IWebElement VersionButton => _webDriver.SafeFindClickableElementByDataAutoId(VersionButtonId);

        private IWebElement DesignModeButton => _webDriver.SafeFindClickableElementByDataAutoId(DesignModeButtonId);

        public DesignModeBar EnterDesignMode_ThruButton()
        {
            return ClickDesignModeButton();
        }

        private DesignModeBar ClickDesignModeButton()
        {
            IWebElement element = DesignModeButton;
            //IOSDriver<IWebElement> _iOSDriver = (IOSDriver<IWebElement>)_webDriver;
            //_iOSDriver.Context = "WEBVIEW";
            element.Click();
            IOSDriver<IWebElement> _iOSDriver = (IOSDriver<IWebElement>)_webDriver;
            // Switching to iOS webview
            //_webDriver.WaitUntil(ExpectedConditions.StalenessOf(element), 60);
            return new DesignModeBar(_webDriver);
        }

        public VersionListModal ShowVersions()
        {
            VersionButton.Click();
            return new VersionListModal(_webDriver);
        }
    }
}
