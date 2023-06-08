using OpenQA.Selenium;
using ios_tests.Helpers;
using ios_tests.Pages.PageBodies.Components;

namespace ios_tests.Pages.Metadata.Form
{
    public class TimePeriodForm : BaseForm
    {
        private readonly IWebDriver _webDriver;

        private const string TimePeriodNameInputId = "text-input__time-period";
        private const string YearUnitId = "item-option-Year";

        public By TimePeriodNameLocator = By.XPath($".//*[contains(@data-auto-id,'{TimePeriodNameInputId}')]");

        public EditName TimePeriodNameInput => new EditName(_webDriver, TimePeriodNameLocator);
        private IWebElement _yearUnit => _webDriver.SafeFindClickableElementByDataAutoId(YearUnitId);

        public TimePeriodForm(IWebDriver webDriver) : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public void SelectYearUnit()
        {
            _yearUnit.Click();
        }

        public TimePeriodPage ApplyTimePeriod()
        {
            base.ClickApply();
            return new TimePeriodPage(_webDriver);
        }
    }
}
