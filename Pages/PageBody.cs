using OpenQA.Selenium;
using ios_tests.Pages.PageBodies;

namespace ios_tests.Pages
{
    public class PageBody
    {
        // BodyPanel: The viewable area that contains Chapter Tiles, Insight Tiles, etc.
        protected IWebDriver _webDriver;

        public PageBody(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public StoryBody StoryBody => new StoryBody(_webDriver);

        public ChapterBody ChapterBody => new ChapterBody(_webDriver);

        public InsightBody InsightBody => new InsightBody(_webDriver);

        public SettingsBody SettingsBody => new SettingsBody(_webDriver);
    }
}