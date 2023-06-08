using Framework.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.PageBodies.Components;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ios_tests.Pages.PageBodies
{
    public class StoryBody
    {
        protected IWebDriver _webDriver;
        private const string StoryHeaderTitleClassName = "story-page-header__title";
        private readonly By _storyHeaderTitleLocator = By.ClassName(StoryHeaderTitleClassName);
        private const string ChapterTileIdPrefix = "chapter-tile-";
        private readonly By _chapterTileLocator = By.XPath($".//*[contains(@data-auto-id,'{ChapterTileIdPrefix}')]");
        private const string ChapterTileTitleClassName = "chapter-tile__title";
        private readonly By _chapterTileTitleLocator = By.ClassName(ChapterTileTitleClassName);
        private const string ChapterTileContentClassName = "chapter-tile__content";
        private readonly By _chapterTileContentLocator = By.ClassName(ChapterTileContentClassName);
        private const string DesignStoryTitleClassName = "design-story__title";
        private const string DesignStoryNewChapterClassDataAutoId = "design-story__content__new-chapter";
        private readonly By _designStoryTitleLocator = By.ClassName(DesignStoryTitleClassName);
        private readonly By _designStoryNewChapterTileLocator = By.XPath($".//*[@data-auto-id='{DesignStoryNewChapterClassDataAutoId}']");
        private const string OrderChapterButtonId = "design-chapter-tile__order-btn";
        private readonly By _orderChapterButtonLocator = By.XPath($".//*[contains(@data-auto-id, '{OrderChapterButtonId}')]");

        public StoryBody(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string StoryHeaderTitle => _webDriver.SafeFindClickableElement(_storyHeaderTitleLocator).Text;

        public ElementFactory ChapterTiles => new ElementFactory(_webDriver, _chapterTileLocator);

        private IWebElement NewChapterTile => _webDriver.SafeFindClickableElement(_designStoryNewChapterTileLocator);

        public EditName EditName => new EditName(_webDriver, _designStoryTitleLocator);

        public ChapterBody ClickNewChapterTile()
        {
            var newChapterTile = NewChapterTile;
            newChapterTile.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(newChapterTile));
            return new ChapterBody(_webDriver);
        }

        public bool ChaptersAreDisplayed()
        {
            return ChapterTiles.elements.Count > 0;
        }

        public ChapterBody OpenChapterTile(int chapterTilePosition = 0)
        {
            Assert.IsTrue(ChaptersAreDisplayed());
            Assert.IsTrue(chapterTilePosition < ChapterTiles.elements.Count);
            int currentIndex = 0;
            foreach (var tile in ChapterTiles.elements)
            {
                if (currentIndex == chapterTilePosition)
                {
                    tile.Click();
                    return new ChapterBody(_webDriver);
                }
                currentIndex++;
            }
            Assert.Fail();
            return null;
        }

        public string GetChapterTileTitle(int chapterTilePosition = 0)
        {
            Assert.IsTrue(ChaptersAreDisplayed());
            Assert.IsTrue(chapterTilePosition < ChapterTiles.elements.Count);
            int currentIndex = 0;
            foreach (var tile in ChapterTiles.elements)
            {
                if (currentIndex == chapterTilePosition)
                {
                    return tile.FindElement(_chapterTileTitleLocator).Text;
                }
                currentIndex++;
            }
            Assert.Fail();
            return null;
        }

        public int GetNumberOfInsightsDispalyedInChapterTile(int chapterTilePosition = 0)
        {
            Assert.IsTrue(ChaptersAreDisplayed());
            Assert.IsTrue(chapterTilePosition < ChapterTiles.elements.Count);
            int currentIndex = 0;
            foreach (var tile in ChapterTiles.elements)
            {
                if (currentIndex == chapterTilePosition)
                {
                    var insightCount = Regex.Match(tile.FindElement(_chapterTileContentLocator).Text, @"\d+").Value;
                    return Int32.Parse(insightCount);
                }
                currentIndex++;
            }
            Assert.Fail();
            return 0;
        }

        public void DragAndDropChapters(string fromChapterTileGuid, string toChapterTileGuid)
        {
            var fromTile = ChapterTiles.elements.FirstOrDefault(t => t.GetAttribute("data-auto-id").Contains(fromChapterTileGuid));
            var toTile = ChapterTiles.elements.FirstOrDefault(t => t.GetAttribute("data-auto-id").Contains(toChapterTileGuid));
            var fromChapterOrderButton = fromTile.FindElement(_orderChapterButtonLocator);
            var toChapterOrderButton = toTile.FindElement(_orderChapterButtonLocator);

            _webDriver.DragAndDropElement(fromChapterOrderButton, toChapterOrderButton);
            _webDriver.WaitForJQueryAndJSToLoad();
        }
    }
}
