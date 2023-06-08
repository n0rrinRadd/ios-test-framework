using System;
using OpenQA.Selenium;
using ios_tests.Pages.PageBodies.Components;

namespace ios_tests.Pages.Metadata.Form
{
    public class MetricForm : BaseForm
    {
        private readonly IWebDriver _webDriver;
        private const string MetricNameInputId = "text-input__metric-name";
        private const string MetricFormatInputId = "item-select__metric-format";
        private const string MetricPrecisionInputId = "input-number__metric-format-precision";

        private By _metricNameLocator = By.XPath($".//*[contains(@data-auto-id,'{MetricNameInputId}')]");
        private By _metricFormatLocator = By.XPath($".//*[contains(@data-auto-id,'{MetricFormatInputId}')]");
        private By _metricFormatPrecisionLocator = By.XPath($".//*[contains(@data-auto-id,'{MetricPrecisionInputId}')]");

        private int? _precisionValue;

        public enum FormatType
        {
            Integer,
            Decimal,
            Percentage,
            Currency
        }

        public bool IsPrecisionAvailable { get; private set; }

        public FormatType SelectedOption
        {
            get
            {
                string selected = FormatItemSelect.SelectedOption;

                FormatType retEnum;
                switch (selected)
                {
                    case "Decimal":
                        retEnum = FormatType.Decimal;
                        break;
                    case "Percentage (%)":
                        retEnum = FormatType.Percentage;
                        break;
                    case "Currency ($)":
                        retEnum = FormatType.Currency;
                        break;
                    default:
                        retEnum = FormatType.Integer;
                        break;
                }
                return retEnum;
            }
        }

        public EditName MetricNameInput => new EditName(_webDriver, _metricNameLocator);
        public ItemSelect FormatItemSelect => new ItemSelect(_webDriver, _metricFormatLocator);
        public InputNumber FormatPrecisionInputNumber;

        public MetricForm(IWebDriver webDriver) : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public void SetFormat(FormatType format)
        {
            FormatItemSelect.ClickOption(format.ToString());

            switch (FormatItemSelect.SelectedOption)
            {
                case "Decimal":
                case "Percentage (%)":
                case "Currency ($)":
                    IsPrecisionAvailable = true;
                    FormatPrecisionInputNumber = new InputNumber(_webDriver, _metricFormatPrecisionLocator);
                    break;
                default:
                    IsPrecisionAvailable = false;
                    FormatPrecisionInputNumber = null;
                    break;
            }

        }

        public MetricPage ApplyMetric()
        {
            base.ClickApply();
            return new MetricPage(_webDriver);
        }

        public void SetPrecision(int precision)
        {
            var returnValue = FormatPrecisionInputNumber.SetValue(precision.ToString());

            int value;
            _precisionValue = int.TryParse(returnValue, out value) ? (int?)value : null;
        }

        public int? GetPrecision()
        {
            int value;

            return int.TryParse(FormatPrecisionInputNumber.GetValue(), out value) ? (int?)value : null;
        }

        public void IncrementPrecision(int repeat = 1)
        {
            FormatPrecisionInputNumber.Increment(repeat);
        }

        public void DecrementPrecision(int repeat = 1)
        {
            FormatPrecisionInputNumber.Decrement(repeat);
        }

        public bool IsIncrementEnabled => FormatPrecisionInputNumber.IsIncrementEnabled;

        public bool IsDecrementEnabled => FormatPrecisionInputNumber.IsDecrementEnabled;
    }
}
