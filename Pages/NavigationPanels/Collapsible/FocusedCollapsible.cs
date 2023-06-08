using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;

namespace ios_tests.Pages.NavigationPanels.Collapsible
{
    public class FocusedCollapsible : BaseCollapsible
    {
        private const string BackToStoryButtonId = "back-to-story-list-btn";
        
        public FocusedCollapsible(IWebDriver webDriver) : base(webDriver)
        {
            // do additional work here
        }

        private IWebElement ReturnToMyStoriesButton => WebDriver.SafeFindClickableElementByDataAutoId(BackToStoryButtonId);

        public OverviewCollapsible ReturnToMyStories()
        {
            var element = ReturnToMyStoriesButton;
            element.Click();
            WebDriver.WaitUntil(ExpectedConditions.StalenessOf(element));
            WebDriver.WaitForJQueryAndJSToLoad(10);
            return new OverviewCollapsible(WebDriver);
        }        
    }
}
