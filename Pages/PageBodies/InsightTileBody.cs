using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Linq;
using ios_tests.Helpers;
using ios_tests.Pages.Modals;
using NUnit.Framework;
using System.Collections.Generic;

namespace ios_tests.Pages.PageBodies
{
    public class InsightTileBody
    {
        private IWebDriver _webDriver;
        private IWebElement insightRoot;

        private const string InsightTileHeaderClassName = "insight-header__title";
        private readonly By InsightTileHeaderClassNameLocator = By.ClassName(InsightTileHeaderClassName);
        private const string ChartSvgContainerClassName = "svg-container";
        private readonly By ChartSvgContainerClassNameLocator = By.ClassName(ChartSvgContainerClassName);
        private const string GridVizClassPrefix = "__grid-basic__";
        private readonly By GridVizClassPrefixLocator = By.XPath($".//*[contains(@class,'{GridVizClassPrefix}')]");
        private const string ScoreCardVizClassPrefix = "flex-box--column";
        private readonly By ScoreCardVizClassPrefixLocator = By.XPath($".//*[contains(@class,'{ScoreCardVizClassPrefix}')]");
        private const string ChartLegendClassName = "legend__item";
        private readonly By ChartLegendClassNameLocator = By.ClassName(ChartLegendClassName);

        private ReadOnlyCollection<IWebElement> ChartLegends => insightRoot.FindElements(ChartLegendClassNameLocator);

        public InsightTileBody(IWebDriver webDriver, string tileInsightGuid)
        {
            _webDriver = webDriver;
            insightRoot = _webDriver.SafeFindElementByDataAutoId(tileInsightGuid);
        }

        private IWebElement InsightTileHeader => _webDriver.SafeFindElement(InsightTileHeaderClassNameLocator);

        public string GetInsightTileHeader() => InsightTileHeader.Text;

        // Visualize container in chapter tile is different from the one in insight screen/body
        public void ClickOnLegend(string legendName, bool toggleDataPoint)
        {
            string deEmphasized = "is-deemphasized";
            string isDeEmphasizedSet;
            int numOfLegends = ChartLegends.Count;

            string vizType = GetVisualizeType();
            if (vizType == VisualizeType.Grid || vizType == VisualizeType.PropertyList || numOfLegends == 0)
            {
                Assert.Fail("Visualize type has no legends");
            }

            List<IWebElement> dynamicChartLegends = ChartLegends.ToList();
            int index = 0;
            var chartLegend = dynamicChartLegends.ElementAt(index);

            // Chart data point is highlighted when its corresponding legend is clicked for the first time
            if (!toggleDataPoint)
            {
                while (index < numOfLegends)
                {
                    if (chartLegend.Text.Equals(legendName))
                    {
                        // When a legend is clicked, alll other unselected legend/data becomes "Deemphasized"
                        chartLegend.Click();
                        for (int i = 0; i < numOfLegends; i++)
                        {
                            if (i > index)
                            {
                                isDeEmphasizedSet = ChartLegends.ElementAt(i).GetAttribute("class");
                                Assert.IsTrue(isDeEmphasizedSet.Contains(deEmphasized));
                            }
                        }
                    }

                    index++;
                    if (index < numOfLegends)
                    {
                        // Legend elements are freshed after each click, the data need to be udpated to avoid staleness
                        dynamicChartLegends = ChartLegends.ToList();
                        chartLegend = dynamicChartLegends.ElementAt(index);
                    }
                }
            }
            else  // Chart data point is toggle when its corresponding legend is clicked 1+ time
            {
                while (index < numOfLegends)
                {
                    if (chartLegend.Text.Equals(legendName))
                    {
                        chartLegend.Click();

                        // When a legend is clicked, the selected legend/data toggle except the last datapoint which resets everything when click
                        if (index < numOfLegends - 1)
                        {
                            isDeEmphasizedSet = ChartLegends.ElementAt(index).GetAttribute("class");
                            Assert.IsTrue(isDeEmphasizedSet.Contains(deEmphasized));
                        }
                    }

                    index++;
                    if (index < numOfLegends)
                    {
                        // Legend elements are freshed after each click, the data need to be udpated to avoid staleness
                        dynamicChartLegends = ChartLegends.ToList();
                        chartLegend = dynamicChartLegends.ElementAt(index);
                    }
                }
            }
        }


        public List<string> GetChartLegends()
        {
            string vizType = GetVisualizeType();
            if (vizType == VisualizeType.Grid || vizType == VisualizeType.PropertyList || ChartLegends.Count == 0)
            {
                return null;
            }

            List<string> chartLengendLabels = new List<string>();
            foreach (var chartLegend in ChartLegends)
            {
                chartLengendLabels.Add(chartLegend.Text);
            }
            return chartLengendLabels;
        }

        // Visualize container in chapter tile is different from the one in insight screen/body
        public string GetVisualizeType()
        {
            string vizType = null;
            const string GridTableClassName = "table";
            By GridTableClassNameLocator = By.ClassName(GridTableClassName);

            // Look for the visualize type in display, grid viz has no "svg_container" only chart
            ReadOnlyCollection<IWebElement> svgContainers = insightRoot.FindElements(ChartSvgContainerClassNameLocator);
            ReadOnlyCollection<IWebElement> gridBasicContainers = insightRoot.FindElements(GridVizClassPrefixLocator);
            ReadOnlyCollection<IWebElement> scoreCardContainers = insightRoot.FindElements(ScoreCardVizClassPrefixLocator);
            if (svgContainers.Count > 0)
            {
                IWebElement svgContainer = svgContainers.FirstOrDefault();
                vizType = svgContainer.GetAttribute("data-auto-id");
            }
            else if (gridBasicContainers.Count > 0 && gridBasicContainers.FirstOrDefault().FindElements(By.ClassName("table")).Count > 0)
            {
                vizType = VisualizeType.Grid;
            }
            else if (scoreCardContainers.Count > 0 && scoreCardContainers.FirstOrDefault().FindElements(By.ClassName("property-list")).Count > 0)
            {
                vizType = VisualizeType.PropertyList;
            }
            else if (scoreCardContainers.Count > 0 && scoreCardContainers.FirstOrDefault().FindElements(By.ClassName("kpi__main-metric")).Count > 0)
            {
                vizType = VisualizeType.KpiMetric;
            }

            // Map chart-type to visualize-menu-type as vizType since Grid and other doesn't have a chart-type value
            switch (vizType)
            {
                case VisualizeType.Bar:
                    vizType = VisualizeModal.ChartTypeBarId;
                    break;
                case VisualizeType.GroupedBar:
                    vizType = VisualizeModal.ChartTypeGroupedBarId;
                    break;
                case VisualizeType.Area:
                    vizType = VisualizeModal.ChartTypeAreaId;
                    break;
                case VisualizeType.Line:
                    vizType = VisualizeModal.ChartTypeLineId;
                    break;
                case VisualizeType.Grid:
                    vizType = VisualizeModal.ChartTypeGridId;
                    break;
                case VisualizeType.PropertyList:
                    vizType = VisualizeModal.ChartTypeScoreCardId;
                    break;
                case VisualizeType.KpiMetric:
                    vizType = VisualizeModal.ChartTypeKpiMetricId;
                    break;
                default:
                    break;
            }

            return vizType;
        }
    }
}
