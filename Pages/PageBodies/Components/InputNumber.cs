using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ios_tests.Helpers;

namespace ios_tests.Pages.PageBodies.Components
{
    public class InputNumber
    {
        protected IWebDriver _webDriver;

        private const string IncrementId = "input_number__button--up";
        private const string DecrementId = "input_number__button--down";

        private By _rootLocator;
        private ByChained InputLocator => new ByChained(_rootLocator, By.TagName("input"));
        private ByChained IncrementButtonLocator => new ByChained(_rootLocator, By.XPath($"//*[contains(@data-auto-id,'{IncrementId}')]"));
        private ByChained DecrementButtonLocator => new ByChained(_rootLocator, By.XPath($"//*[contains(@data-auto-id,'{DecrementId}')]"));

        private IWebElement NumberInput => _webDriver.SafeFindElement(InputLocator);
        private IWebElement IncrementButton => _webDriver.SafeFindElement(IncrementButtonLocator);
        private IWebElement DecrementButton => _webDriver.SafeFindElement(DecrementButtonLocator);

        public InputNumber(IWebDriver webDriver, By rootLocator)
        {
            _webDriver = webDriver;
            _rootLocator = rootLocator;
        }

        public string SetValue(string value)
        {
            NumberInput.Clear();
            NumberInput.SendKeys(value);

            return GetValue();
        }

        public string GetValue()
        {
            return NumberInput.GetAttribute("value");
        }

        public void Increment(int repeat = 1)
        {
            for (int i = 0; i < repeat; i++)
            {
                IncrementButton.Click();
            }
        }

        public void Decrement(int repeat = 1)
        {
            for (int i = 0; i < repeat; i++)
            {
                DecrementButton.Click();
            }
        }

        public bool IsIncrementEnabled => IncrementButton.Enabled;

        public bool IsDecrementEnabled => DecrementButton.Enabled;
    }
}
