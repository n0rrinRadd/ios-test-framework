using OpenQA.Selenium;
using ios_tests.Helpers;

namespace ios_tests.Pages.Metadata.Form
{
    public class CalendarForm : BaseForm
    {
        private readonly IWebDriver _webDriver;
        private const string _numberOfActiveYearsId = "input-number__calendar-years";
        private const string _yearUnitId = "item-option-Year";

        private readonly By _numberOfActiveYearsLocator = By.XPath($".//*[contains(@data-auto-id,'{_numberOfActiveYearsId}')]//input");
        private IWebElement _numberOfActiveYearsInput => _webDriver.SafeFindElement(_numberOfActiveYearsLocator);
        private IWebElement _yearUnit => _webDriver.SafeFindClickableElementByDataAutoId(_yearUnitId);

        public CalendarForm(IWebDriver webDriver) : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public void SetNumberOfYears(int years)
        {
            _numberOfActiveYearsInput.Clear();
            _numberOfActiveYearsInput.SendKeys(years.ToString());
        }

        public void SelectYearUnit()
        {
            _yearUnit.Click();
        }

        public TimePeriodPage ApplyCalendar()
        {
            base.ClickApply();
            return new TimePeriodPage(_webDriver);
        }
    }
}
