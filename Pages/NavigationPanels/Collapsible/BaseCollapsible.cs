using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.PageBodies;

namespace ios_tests.Pages.NavigationPanels.Collapsible
{
    public class BaseCollapsible
    {
        internal IWebDriver WebDriver;
        private const string NavBarCollapsibleId = "nav-bar-collapsible";
        private readonly By _hideableContentLocator = By.ClassName("nav-bar__collapsible-content");
        private const string ToggleExpandPanelButtonId = "nav-bar__expander";
        internal const string ChapterLinkPrefix = "chapter-link-";
        internal const string StoryLinkPrefix = "story-link-";
        private const string HeaderId = "nav-bar__header";
        public const string ChapterListIdPrefix = "nav-bar_chapter-list-";
        private readonly By _storyLinkLocator = By.XPath($".//*[contains(@data-auto-id, '{StoryLinkPrefix}')]");
        private readonly By _chapterListLocator = By.XPath($".//*[contains(@data-auto-id, '{ChapterListIdPrefix}')]");
        private readonly By _chapterLinkLocator = By.XPath($".//*[contains(@data-auto-id, '{ChapterLinkPrefix}')]");
        private const string IsActiveClassName = "is-active";
        private const string FocusedSectionId = "nav-bar-collapsible--focused";
        private const string OverviewSectionId = "nav-bar-collapsible--overview";

        public BaseCollapsible(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        private IWebElement FocusedSection => WebDriver.SafeFindElementByDataAutoId(FocusedSectionId);

        private IWebElement OverviewSection => WebDriver.SafeFindElementByDataAutoId(OverviewSectionId);

        private IWebElement Header => WebDriver.SafeFindClickableElementByDataAutoId(HeaderId);

        private IWebElement HideableContent => WebDriver.SafeFindElement(_hideableContentLocator);

        internal IWebElement ChapterList => WebDriver.SafeFindElement(_chapterListLocator);

        internal IReadOnlyCollection<IWebElement> ChapterListContainers => WebDriver.FindElements(_chapterListLocator);

        private IWebElement ActiveStory => WebDriver.SafeFindElement(new ByAll(_storyLinkLocator, By.ClassName(IsActiveClassName)));

        private IWebElement ActiveChapter => WebDriver.SafeFindElement(new ByAll(_chapterLinkLocator, By.ClassName(IsActiveClassName)));

        public string ActiveStoryId => ActiveStory.GetAttribute("data-auto-id").Replace(StoryLinkPrefix, string.Empty);

        public string ActiveChapterId => ActiveChapter.GetAttribute("data-auto-id").Replace(ChapterLinkPrefix, string.Empty);

        private IWebElement ToggleExpandPanelButton => WebDriver.SafeFindClickableElementByDataAutoId(ToggleExpandPanelButtonId);

        internal IReadOnlyCollection<IWebElement> StoryLinks => WebDriver.FindElements(_storyLinkLocator);

        internal IReadOnlyCollection<IWebElement> ChapterLinks => WebDriver.FindElements(_chapterLinkLocator);

        private IWebElement NavBarCollapsible => WebDriver.SafeFindElementByDataAutoId(NavBarCollapsibleId);

        public BaseCollapsible CollapseNavBar()
        {
            ToggleExpandPanelButton.Click();
            WebDriver.WaitUntil(driver => !IsNavPanelAnimating());
            return this;
        }

        /// <summary>
        /// Click link of the story snapshot in focused view
        /// Returns to the story page of the current story
        /// </summary>
        /// <returns></returns>
        public StoryBody GoToStorySnapshot()
        {
            Assert.IsTrue(!IsContentHidden());
            Assert.IsTrue(IsFocusedSectionActive());

            StoryLinks.FirstOrDefault().Click();

            Assert.IsTrue(WebDriver.Url.Contains("stories"));
            Assert.IsTrue(AreAnyChaptersShown());
            //Assert.IsTrue(ChapterLinks.Count == StoryData.ChaptersCount(ActiveStoryId));

            return new StoryBody(WebDriver);
        }

        /// <summary>
        /// Click link of the story by name
        /// Returns to the story page of the current story
        /// </summary>
        /// <returns></returns>
        public StoryBody GoToStoryByName(string storyName)
        {
            Assert.IsTrue(!IsContentHidden());

            var storyLink = StoryLinks.FirstOrDefault(storyToSearch => storyToSearch.Text.Equals(storyName));
            if (storyLink == null)
            {
                Assert.Fail("Story is not found.");
            }

            storyLink.Click();
            WebDriver.WaitUntil(ExpectedConditions.UrlContains("stories"));

            return new StoryBody(WebDriver);
        }

        /// <summary>
        /// Click link of the story by its position
        /// Returns to the story page of the current story
        /// </summary>
        /// <returns></returns>
        public StoryBody GoToStoryByIndex(int storyPosition = 1)
        {
            Assert.IsTrue(!IsContentHidden());
            Assert.GreaterOrEqual(StoryLinks.Count, storyPosition);

            var storyLink = StoryLinks.ElementAtOrDefault(storyPosition - 1);
            storyLink.Click();
            WebDriver.WaitUntil(ExpectedConditions.UrlContains("stories"));

            return new StoryBody(WebDriver);
        }

        public StoryBody GetSelectedStory()
        {
            Assert.IsTrue(WebDriver.Url.Contains("stories"));
            return new StoryBody(WebDriver);
        }

        /// <summary>
        /// Click link of a chapter identified by chapter Guid
        /// </summary>
        /// <param name="chapterId"></param>
        /// <returns></returns>
        public ChapterBody GoToChapterById(string chapterId)
        {
            Assert.IsTrue(!IsContentHidden());
            Assert.IsTrue(ChapterList.Displayed);
            Assert.IsTrue(IsFocusedSectionActive());

            var chapterLink = ChapterLinks.FirstOrDefault(l => l.GetAttribute("data-auto-id") == string.Concat(ChapterLinkPrefix, chapterId));
            chapterLink.Click();
            WebDriver.WaitUntil(ExpectedConditions.UrlContains("chapters"));

            return new ChapterBody(WebDriver);
        }

        /// <summary>
        /// Click link of a chapter based on relative position of link
        /// Defaults to the first link
        /// </summary>
        /// <param name="chapterPosition"></param>
        /// <returns></returns>
        public ChapterBody GoToChapterByIndex(int chapterPosition = 1)
        {
            Assert.IsTrue(!IsContentHidden());
            Assert.IsTrue(ChapterList.Displayed);
            Assert.GreaterOrEqual(ChapterLinks.Count, chapterPosition);

            var chapterLink = ChapterLinks.ElementAtOrDefault(chapterPosition - 1);
            chapterLink.Click();
            WebDriver.WaitUntil(ExpectedConditions.UrlContains("chapters"));

            return new ChapterBody(WebDriver);
        }

        /// <summary>
        /// Click link of a chapter based on its name        
        /// </summary>
        /// <param name="chapterName"></param>
        /// <returns></returns>
        public ChapterBody GoToChapterByName(string chapterName)
        {
            Assert.IsTrue(!IsContentHidden());
            Assert.IsTrue(ChapterList.Displayed);

            var chapterLink = ChapterLinks.FirstOrDefault(e => e.Text.Equals(chapterName));
            if (chapterLink == null)
            {
                Assert.Fail();
            }

            chapterLink.Click();
            WebDriver.WaitUntil(ExpectedConditions.UrlContains("chapters"));

            return new ChapterBody(WebDriver);
        }

        public bool IsNavPanelAnimating()
        {
            var navBarSize = NavBarCollapsible.Size.Width;
            Thread.Sleep(100);

            return NavBarCollapsible.Size.Width != navBarSize;
        }

        public bool AreAnyChaptersShown() => ChapterList?.Displayed ?? false;

        public int NumOfStoriesWithExpandedChapterList() => ChapterListContainers?.Count ?? 0;

       //public bool AreAllChaptersShown() => StoryData.SnapshotChapterDictionary().Count == ChapterListCount();

        /*
        private int ChapterListCount()
        {
            var count = WebDriver
                .FindElements(_chapterListLocator)
                .Count(e => !e.GetAttribute("data-auto-id").Contains(StoryData.PendingSnapshotPrefix));
            return count;
        }
        */

        internal IReadOnlyCollection<IWebElement> GetStoryChapterLinks(string snapshotId)
        {
            IWebElement chapterList = GetStoryChapterList(snapshotId);
            var links = chapterList.FindElements(_chapterLinkLocator);
            return links;
        }

        private IWebElement GetStoryChapterList(string snapshotId)
        {
            By storyChapterListLocator = By.XPath($".//*[@data-auto-id='{ChapterListIdPrefix}{snapshotId}']");
            IWebElement listElement = WebDriver.SafeFindElement(storyChapterListLocator);
            return listElement;
        }

        // Evaluates if the collapsible content is currently hidden from view
        // Also returns false if the element is not found
        public bool IsContentHidden()
        {
            bool isHidden = !(HideableContent?.Displayed ?? false);
            return isHidden;
        }


        public List<string> ReadChaptersList(string currentSnapshotId)
        {
            Assert.IsTrue(AreAnyChaptersShown());
            var chapters = ChapterLinks.Select(c => c.Text).ToList();
            return chapters;
        }

        internal bool IsFocusedSectionActive() => FocusedSection?.Displayed ?? false;

        internal bool IsOverviewSectionActive() => OverviewSection?.Displayed ?? false;
    }
}
