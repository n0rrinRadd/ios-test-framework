using OpenQA.Selenium;
using ios_tests.Pages.ActionBars;

namespace ios_tests.Pages
{
    public class ActionBar
    {
        private IWebDriver _webDriver;

        public ActionBar(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public DesignModeBar DesignModeBar => new DesignModeBar(_webDriver);

        public ViewModeBar ViewModeBar => new ViewModeBar(_webDriver);
    }
}
