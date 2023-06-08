using System.Collections.Generic;
using System.Linq;
using Framework.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ios_tests.Helpers;
using ios_tests.Pages.Modals;
using ios_tests.Pages.PageBodies.Components;

namespace ios_tests.Pages.PageBodies
{
    public class ChapterBody
    {
        protected IWebDriver _webDriver;
        private const string ChapterFilterButtonId = "btn-filter";
        private const string ChapterPageHeaderTitleClass = "chapter-page__header__title";
        private readonly By _chapterPageHeaderTitleClassLocator = By.ClassName(ChapterPageHeaderTitleClass);
        private const string DesignChapterNameDataAutoId = "text-input__chapter";
        private readonly By _designChapterNameLocator = By.XPath($".//*[contains(@data-auto-id,'{DesignChapterNameDataAutoId}')]");
        private const string InsightTileIdPrefix = "insight-tile-";
        private readonly By _insightTileLocator = By.XPath($".//*[contains(@data-auto-id,'{InsightTileIdPrefix}')]");
        private const string InsightDetailButtonPrefixId = "btn-insight-header__detail";
        private readonly By _insightDetailButtonLocator = By.XPath($".//*[contains(@data-auto-id,'{InsightDetailButtonPrefixId}')]");
        private const string DeleteChapterButtonId = "design__chapter-delete-btn";
        private const string DeleteInsightButtonId = "design-insight-tile__delete-btn";
        private readonly By _deleteInsightButtonLocator = By.XPath($".//*[contains(@data-auto-id, '{DeleteInsightButtonId}')]");
        private const string FilterItemClass = "filter-item__text";
        private readonly By _filterItemLocator = By.ClassName(FilterItemClass);
        private const string TimePeriodId = "time-period-filter-item";
        private const string FilterItemDeleteClass = "filter-item__delete-button";
        private readonly By _filterItemDeleteLocator = By.ClassName(FilterItemDeleteClass);
        private const string OrderInsightButtonId = "design-insight-tile__order-btn";
        private readonly By _orderInsightButtonLocator = By.XPath($".//*[contains(@data-auto-id, '{OrderInsightButtonId}')]");

        public ChapterBody(IWebDriver webDriver)
        {
            _webDriver = webDriver;

        }

        private IWebElement ChapterPageHeader => _webDriver.SafeFindElement(_chapterPageHeaderTitleClassLocator);

        private IWebElement ChapterFilterButton => _webDriver.SafeFindClickableElementByDataAutoId(ChapterFilterButtonId);

        public IWebElement DeleteChapterButton => _webDriver.SafeFindClickableElementByDataAutoId(DeleteChapterButtonId);

        public ElementFactory InsightTiles => new ElementFactory(_webDriver, _insightTileLocator);

        public int GetInsightTileCount() => InsightTiles.elements.Count;

        private ElementFactory InsightDetailButtons => new ElementFactory(_webDriver, _insightDetailButtonLocator);

        public ElementFactory FilterListItems => new ElementFactory(_webDriver, _filterItemLocator);

        public ElementFactory FilterListItemsDeleteButtons => new ElementFactory(_webDriver, _filterItemDeleteLocator);

        public IWebElement TimePeriodFilterListItem => _webDriver.SafeFindClickableElementByDataAutoId(TimePeriodId);

        public string GetChapterPageHeader() => ChapterPageHeader.Text;

        public EditName EditName => new EditName(_webDriver, _designChapterNameLocator);

        public FilterModal OpenChapterFilters()
        {
            Assert.IsTrue(ChapterFilterButton.Displayed);
            ChapterFilterButton.Click();
            return new FilterModal(_webDriver);
        }

        public bool InsightsAreDisplayed()
        {
            return GetInsightTileCount() > 0;
        }

        public List<string> GetInsightDetailsList()
        {
            var insightDetailsList = new List<string>();

            foreach (var tile in InsightTiles.elements)
            {
                if (tile.FindElements(_insightDetailButtonLocator).Any())
                {
                    var dataAutoId = tile.GetAttribute("data-auto-id");
                    insightDetailsList.Add(dataAutoId);
                }
            }
            return insightDetailsList;
        }

        public InsightBody GoToInsightDetail(string insightGuid)
        {
            ByChained insightTileLocator = new ByChained(By.XPath($".//*[@data-auto-id='{insightGuid}']"), _insightDetailButtonLocator);
            var detailButton = _webDriver.SafeFindClickableElement(insightTileLocator);
            detailButton.Click();
            return new InsightBody(_webDriver);
        }

        public InsightTileBody GetInsightTileByGuid(string insightGuid)
        {
            Assert.IsNotNull(insightGuid);
            return new InsightTileBody(_webDriver, insightGuid);
        }

        public string GetInsightTileGuidByIndex(int insightTileOrder)
        {
            Assert.IsTrue(insightTileOrder >= 0);
            Assert.IsTrue(insightTileOrder < GetInsightTileCount());
            return InsightTiles.elements[insightTileOrder].GetAttribute("data-auto-id");
        }

        public DeleteItemModal DeleteInsight(string insightGuid)
        {
            var tile = InsightTiles.elements.FirstOrDefault(t => t.GetAttribute("data-auto-id").Contains(insightGuid));
            var button = tile?.FindElements(_deleteInsightButtonLocator).FirstOrDefault();
            button?.Click();

            return new DeleteItemModal(_webDriver);
        }

        public DeleteItemModal ClickDeleteChapterButton()
        {
            Assert.IsTrue(DeleteChapterButton.Displayed);
            DeleteChapterButton.Click();
            return new DeleteItemModal(_webDriver);
        }

        public void DragAndDropInsights(string fromInsightGuid, string toInsightGuid)
        {
            var fromTile = InsightTiles.elements.FirstOrDefault(t => t.GetAttribute("data-auto-id").Contains(fromInsightGuid));
            var toTile = InsightTiles.elements.FirstOrDefault(t => t.GetAttribute("data-auto-id").Contains(toInsightGuid));
            var fromInsightOrderButton = fromTile.FindElement(_orderInsightButtonLocator);
            var toInsightOrderButton = toTile.FindElement(_orderInsightButtonLocator);

            _webDriver.DragAndDropElement(fromInsightOrderButton, toInsightOrderButton);
            _webDriver.WaitForJQueryAndJSToLoad();
        }
    }
}
