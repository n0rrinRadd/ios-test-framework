using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.PageBodies;

namespace ios_tests.Pages.Modals
{
    public class VisualizeModal : BaseModal
    {
        private IWebDriver _webDriver;
        private const string ModalClassName = "modal";
        private const string RadioButtonLabelClassName = "radio-button__label";
        private readonly By RadioButtonLabelClassNameLocator = By.ClassName(RadioButtonLabelClassName);
        public const string ChartTypeBarId = "visualize-menu_bar";
        public const string ChartTypeGroupedBarId = "visualize-menu_groupedbar";
        public const string ChartTypeAreaId = "visualize-menu_area";
        public const string ChartTypeLineId = "visualize-menu_line";
        public const string ChartTypeGridId = "visualize-menu_grid";
        public const string ChartTypeScoreCardId = "visualize-menu_property_list";
        public const string ChartTypeKpiMetricId = "visualize-menu_kpi_metric";
        public const string ChartTypeUnsupported = "unsupported";

        public VisualizeModal(IWebDriver webDriver) : base(webDriver, ModalClassName)
        {
            _webDriver = webDriver;
            Assert.IsTrue(IsModalDisplayed());
        }

        private IWebElement ChartTypeBar => _webDriver.SafeFindClickableElementByDataAutoId(ChartTypeBarId);
        private IWebElement ChartTypeGroupedBar => _webDriver.SafeFindClickableElementByDataAutoId(ChartTypeGroupedBarId);
        private IWebElement ChartTypeArea => _webDriver.SafeFindClickableElementByDataAutoId(ChartTypeAreaId);
        private IWebElement ChartTypeLine => _webDriver.SafeFindClickableElementByDataAutoId(ChartTypeLineId);
        private IWebElement ChartTypeGrid => _webDriver.SafeFindClickableElementByDataAutoId(ChartTypeGridId);

        public void ValidateVisualizeSelection(string insightHeaderTitle)
        {
            // Verify that the Visualize form title is the same as insight's, and all the displayed visualize have
            // the correct button label
            Assert.IsTrue(GetModalHeaderTitle().Equals(insightHeaderTitle));
            Assert.IsTrue(ChartTypeBar.FindElement(RadioButtonLabelClassNameLocator).Text.Equals(VisualizeLabel.Bar));
            Assert.IsTrue(ChartTypeGroupedBar.FindElement(RadioButtonLabelClassNameLocator).Text.Equals(VisualizeLabel.GroupedBar));
            Assert.IsTrue(ChartTypeArea.FindElement(RadioButtonLabelClassNameLocator).Text.Equals(VisualizeLabel.Area));
            Assert.IsTrue(ChartTypeLine.FindElement(RadioButtonLabelClassNameLocator).Text.Equals(VisualizeLabel.Line));
            Assert.IsTrue(ChartTypeGrid.FindElement(RadioButtonLabelClassNameLocator).Text.Equals(VisualizeLabel.Grid));
        }

        public InsightBody SelectBar()
        {
            ChartTypeBar.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(ModalWindow));
            return new InsightBody(_webDriver);
        }

        public InsightBody SelectGroupedBar()
        {
            ChartTypeGroupedBar.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(ModalWindow));
            return new InsightBody(_webDriver);
        }

        public InsightBody SelectArea()
        {
            ChartTypeArea.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(ModalWindow));
            return new InsightBody(_webDriver);
        }

        public InsightBody SelectLine()
        {
            ChartTypeLine.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(ModalWindow));
            return new InsightBody(_webDriver);
        }

        public InsightBody SelectGrid()
        {
            ChartTypeGrid.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(ModalWindow));
            return new InsightBody(_webDriver);
        }
    }
}
