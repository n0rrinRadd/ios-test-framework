using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.ActionBars;

namespace ios_tests.Pages.Metadata
{
    public class MetadataPage
    {
        private readonly IWebDriver _webDriver;

        private const string ExitMetadataButtonId = "btn-exit-metadata";
        private const string navBarClassName = "metadata__nav-bar";
        private const string DimensionMetadataButtonId = "metadata__nav-bar__dimensions";
        private const string MetricMetadataButtonId = "metadata__nav-bar__metrics";
        private const string HierarchyMetadataButtonId = "metadata__nav-bar__hierarchies";
        private const string TimePeriodMetadataButtonId = "metadata__nav-bar__time-periods";

        private readonly By _navBarLocator = By.ClassName(navBarClassName);

        private IWebElement ExitMetadataButton => _webDriver.SafeFindClickableElementByDataAutoId(ExitMetadataButtonId);
        private IWebElement MetadataNavBar => _webDriver.SafeFindElement(_navBarLocator);

        private IWebElement DimensionMetadataButton => _webDriver.SafeFindClickableElementByDataAutoId(DimensionMetadataButtonId);
        private IWebElement MetricMetadataButton => _webDriver.SafeFindClickableElementByDataAutoId(MetricMetadataButtonId);
        private IWebElement HierarchyMetadataButton => _webDriver.SafeFindClickableElementByDataAutoId(HierarchyMetadataButtonId);
        private IWebElement TimePeriodMetadataButton => _webDriver.SafeFindClickableElementByDataAutoId(TimePeriodMetadataButtonId);

        public MetadataPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public DesignModeBar ExitMetadata()
        {
            ExitMetadataButton.Click();
            return new DesignModeBar(_webDriver);
        }

        public DimensionPage ClickDimension()
        {
            DimensionMetadataButton.Click();
            return new DimensionPage(_webDriver);
        }

        public TimePeriodPage ClickTimePeriod()
        {
            TimePeriodMetadataButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.UrlContains("time-periods"));
            return new TimePeriodPage(_webDriver);
        }

        public MetricPage ClickMetric()
        {
            MetricMetadataButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.UrlContains("metrics"));
            return new MetricPage(_webDriver);
        }

        public HierarchyPage ClickHierarchy()
        {
            HierarchyMetadataButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.UrlContains("hierarchies"));
            return new HierarchyPage(_webDriver);
        }
    }
}
