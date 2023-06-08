using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.Metadata.Form;

namespace ios_tests.Pages.Metadata
{
    public class TimePeriodPage : BasePage
    {
        private readonly IWebDriver _webDriver;

        private const string CalendarButtonId = "btn-metadata__calendar";

        private IWebElement CalendarButton => _webDriver.SafeFindClickableElementByDataAutoId(CalendarButtonId);

        public TimePeriodPage(IWebDriver webDriver) : base(webDriver, "timePeriod")
        {
            _webDriver = webDriver;
        }

        public CalendarForm ClickCalendarButton()
        {
            CalendarButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.UrlContains("calendar"));
            return new CalendarForm(_webDriver);
        }

        public List<string> GetTimePeriodNames()
        {
            return GetItemNames();
        }

        public bool DoesTimePeriodExist(string name)
        {
            return DoesItemExist(name);
        }

        public TimePeriodForm ClickAddTimePeriod()
        {
            return ClickAddItem<TimePeriodForm>();
        }

        public TimePeriodForm GoToTimePeriod(string timePeriodName)
        {
            return GoToItem<TimePeriodForm>(timePeriodName);
        }

        public TimePeriodPage DeleteTimePeriod(string timePeriodName)
        {
            return DeleteItem<TimePeriodPage>(timePeriodName);
        }
    }
}
