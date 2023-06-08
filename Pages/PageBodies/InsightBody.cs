using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.Modals;

namespace ios_tests.Pages.PageBodies
{
    public class InsightBody
    {
        private IWebDriver _webDriver;
        private const string InsightDetailWrapperClassName = "insight-detail__full-screen-wrapper";
        private readonly By InsightDetailWrapperLocator = By.ClassName(InsightDetailWrapperClassName);
        private const string InsightDetailHeaderClassName = "insight-detail__header";
        private readonly By InsightDetailHeaderLocator = By.ClassName(InsightDetailHeaderClassName);
        private const string ExportButtonId = "btn-export-insight-data";
        private const string VisualizeButtonId = "btn-visualize";
        private const string InsightDetailFilterButtonId = "btn-filter";
        private const string InsightDetailContentClassName = "insight-detail__content";
        private readonly By InsightDetailContentLocator = By.ClassName(InsightDetailContentClassName);
        private const string CloseInsightButtonClassName = "insight-detail__header__close";
        private readonly By CloseInsightButtonLocator = By.ClassName(CloseInsightButtonClassName);

        // InsightPanel: The detailed view of an insight
        public InsightBody(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private IWebElement InsightDetail => _webDriver.SafeFindElement(InsightDetailWrapperLocator);

        private IWebElement InsightDetailHeader => _webDriver.SafeFindElement(InsightDetailHeaderLocator);

        private IWebElement ExportButton => _webDriver.SafeFindClickableElementByDataAutoId(ExportButtonId);

        private IWebElement VisualizeButton => _webDriver.SafeFindClickableElementByDataAutoId(VisualizeButtonId);

        private IWebElement InsightDetailFilterButton => _webDriver.SafeFindClickableElementByDataAutoId(InsightDetailFilterButtonId);

        private ReadOnlyCollection<IWebElement> InsightDetailContent => InsightDetail.FindElements(InsightDetailContentLocator);

        private IWebElement CloseInsightDetailButton => _webDriver.SafeFindClickableElement(CloseInsightButtonLocator);

        public string GetInsightDetailHeader() => InsightDetailHeader.Text;

        public VisualizeModal OpenVisualizeSelection()
        {
            Assert.IsTrue(VisualizeButton.Displayed);
            VisualizeButton.Click();
            return new VisualizeModal(_webDriver);
        }

        public FilterModal OpenInsightDetailFilters()
        {
            Assert.IsTrue(InsightDetailFilterButton.Displayed);
            InsightDetailFilterButton.Click();
            return new FilterModal(_webDriver);
        }

        public bool InsightContentIsDisplayed()
        {
            return InsightDetailContent.Count > 0;
        }

        public ChapterBody CloseInsightDetail()
        {
            CloseInsightDetailButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(InsightDetail));
            return new ChapterBody(_webDriver);
        }

        public VisualizeBody GetVisualize()
        {
            if (!InsightContentIsDisplayed())
            {
                return null;
            }
            return new VisualizeBody(_webDriver);
        }

        public List<string> GetChartLegends()
        {
            var visualizeBody = new VisualizeBody(_webDriver);
            return visualizeBody.GetChartLegends();
        }

        public FileInfo ExportInsightData()
        {
            string currentWindow = _webDriver.CurrentWindowHandle;
            ExportButton.Click();
            _webDriver.Delay(10000);  // Added 10 sec delay to en-safe test on AppVeyor

            var fileInfo = _webDriver.GetDownloadedFileInfo(InsightDetailHeader.Text);
            Assert.IsNotNull(fileInfo);

            // Switch window focus back from the Chrome Info Bar to the ios Window
            _webDriver.SwitchTo().Window(currentWindow);

            return fileInfo;
        }

        public void ClickOnLegend(string legendName, bool toggleDataPoint)
        {
            var visualizeInDisplayed = GetVisualize();
            visualizeInDisplayed.ClickOnLegend(legendName, toggleDataPoint);
        }
    }
}
