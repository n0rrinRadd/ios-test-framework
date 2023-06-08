using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.Modals;
using ios_tests.Pages.PageBodies;

namespace ios_tests.Pages.NavigationPanels.Collapsible
{
    public class OverviewCollapsible : BaseCollapsible
    {
        private const string StoryListId = "nav-bar_story-list";
        private const string ToggleExpandChaptersButtonId = "chapter-expander";
        private readonly By _storyListLocator = By.XPath($".//*[@data-auto-id='{StoryListId}']");
        private const string NewStoryButtonID = "btn-new-story";

        public OverviewCollapsible(IWebDriver webDriver) : base(webDriver)
        {
            Assert.IsTrue(StoryList.Displayed);
        }

        private IWebElement StoryList => WebDriver.SafeFindElement(_storyListLocator);

        private IWebElement ToggleExpandChaptersButton => WebDriver.SafeFindClickableElementByDataAutoId(ToggleExpandChaptersButtonId);

        private IWebElement NewStoryButton => WebDriver.SafeFindClickableElementByDataAutoId(NewStoryButtonID);

        /// <summary>
        /// Click link of a story snapshot identified by snapshot Guid
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <returns></returns>
        public StoryBody GoToStorySnapshot(string snapshotId)
        {
            Assert.IsTrue(!IsContentHidden());
            Assert.IsTrue(StoryList.Displayed);
            Assert.IsTrue(IsOverviewSectionActive());

            var storyLink = StoryLinks.FirstOrDefault(l => l.GetAttribute("data-auto-id") == string.Concat(StoryLinkPrefix, snapshotId));
            storyLink.Click();

            WebDriver.WaitUntil(driver => IsFocusedSectionActive());
            Assert.IsTrue(WebDriver.Url.Contains("stories"));
            Assert.IsTrue(AreAnyChaptersShown());
            //Assert.IsTrue(ChapterLinks.Count == StoryData.ChaptersCount(ActiveStoryId));

            return new StoryBody(WebDriver);
        }

        /// <summary>
        /// Click link of a story snapshot based on relative position of link
        /// Defaults to the first link
        /// </summary>
        /// <param name="snapshotPosition"></param>
        /// <returns></returns>
        public StoryBody GoToStorySnapshot(int snapshotPosition = 1)
        {
            Assert.IsTrue(!IsContentHidden());
            Assert.IsTrue(StoryList.Displayed);
            Assert.IsTrue(IsOverviewSectionActive());
            Assert.GreaterOrEqual(StoryLinks.Count, snapshotPosition);

            var storyLink = StoryLinks.ElementAtOrDefault(snapshotPosition - 1);
            storyLink.Click();

            WebDriver.WaitUntil(driver => IsFocusedSectionActive());
            Assert.IsTrue(WebDriver.Url.Contains("stories"));
            Assert.IsTrue(AreAnyChaptersShown());
            //Assert.IsTrue(ChapterLinks.Count == StoryData.ChaptersCount(ActiveStoryId));

            return new StoryBody(WebDriver);
        }

        /// <summary>
        /// Click link of a chapter identified by chapter Guid & story Guid
        /// Story Guid is required because chapterIds are only unique within an individual story
        /// </summary>
        /// <param name="storyId"></param>
        /// <param name="chapterId"></param>
        /// <returns></returns>
        public ChapterBody GoToChapter(string storyId, string chapterId)
        {
            Assert.IsTrue(!IsContentHidden());
            Assert.IsTrue(IsOverviewSectionActive());
            Assert.IsTrue(ChapterList.Displayed);

            var chapterLinks = GetStoryChapterLinks(storyId);
            var chapterLink = chapterLinks.FirstOrDefault(l => l.GetAttribute("data-auto-id") == string.Concat(ChapterLinkPrefix, chapterId));
            chapterLink.Click();

            WebDriver.WaitUntil(ExpectedConditions.UrlContains("chapters"));            
                        
            return new ChapterBody(WebDriver);
        }

        public NewStoryModal ClickNewStoryButton()
        {
            var newStoryButton = NewStoryButton;
            newStoryButton.Click();

            return new NewStoryModal(WebDriver);
        }

        // Return a list of stories that are displayed in the left nav panel
        // Does not include Chapter names
        public List<String> ReadStoriesList() => StoryLinks.Select(s => s.Text).ToList();
    }
}
