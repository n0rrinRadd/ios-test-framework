using System.Collections.Generic;
using OpenQA.Selenium;
using ios_tests.Pages.Metadata.Form;

namespace ios_tests.Pages.Metadata
{
    public class MetricPage : BasePage
    {
        private readonly IWebDriver _webDriver;

        public MetricPage(IWebDriver webDriver) : base(webDriver, "metric")
        {
            _webDriver = webDriver;
        }

        public List<string> GetMetricNames()
        {
            return GetItemNames();
        }

        public bool DoesMetricExist(string name)
        {
            return DoesItemExist(name);
        }

        public MetricForm ClickAddMetric()
        {
            return ClickAddItem<MetricForm>();
        }

        public MetricForm GoToMetric(string metricName)
        {
            return GoToItem<MetricForm>(metricName);
        }

        public MetricPage DeleteMetric(string metricName)
        {
            return DeleteItem<MetricPage>(metricName);
        }

    }
}
