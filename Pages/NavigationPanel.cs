using OpenQA.Selenium;
using ios_tests.Pages.NavigationPanels;
using ios_tests.Pages.NavigationPanels.Collapsible;

namespace ios_tests.Pages
{
    public class NavigationPanel
    {
        protected IWebDriver _webDriver;

        public NavigationPanel(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public Fixed Fixed => new Fixed(this, _webDriver);

        public FocusedCollapsible FocusedCollapsible => new FocusedCollapsible(_webDriver);

        public DesignModeCollapsible FocusedCollapsibleDesignMode => new DesignModeCollapsible(_webDriver);

        public OverviewCollapsible OverviewCollapsible => new OverviewCollapsible(_webDriver);
    }
}
