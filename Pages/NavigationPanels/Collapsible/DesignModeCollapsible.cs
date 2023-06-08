using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.PageBodies;

namespace ios_tests.Pages.NavigationPanels.Collapsible
{
    public class DesignModeCollapsible : BaseCollapsible
    {        
        private const string DesignNewChapterButtonClassName = "create-chapter";
        private readonly By _designNewChapterButtonLocator = By.ClassName(DesignNewChapterButtonClassName);

        public DesignModeCollapsible(IWebDriver webDriver) : base(webDriver)
        {
            // do additional work here
        }
                
        private IWebElement NewChapterButton => WebDriver.SafeFindClickableElement(_designNewChapterButtonLocator);
        
        public ChapterBody CreateNewChapter()
        {
            var newChapterButton = NewChapterButton;
            newChapterButton.Click();
            WebDriver.WaitUntil(ExpectedConditions.UrlContains("chapters"));

            return new ChapterBody(WebDriver);
        }
    }
}
