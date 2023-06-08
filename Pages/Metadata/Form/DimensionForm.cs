using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using ios_tests.Helpers;
using ios_tests.Pages.PageBodies.Components;

namespace ios_tests.Pages.Metadata.Form
{
    public class DimensionForm : BaseForm
    {
        private readonly IWebDriver _webDriver;
        private const string DimensionNameInputId = "text-input__dimension";
        private const string ProgressionalOrderInputId = "input-switch__progressional-dimension";
        private const string ActiveClassName = "active";
        private const string DimensionPropertiesPrefix = "dimension-property__";
        private const string AddDimensionPropertyBtnId = "btn-add-property";
        private const string DeleteDimensionPropertyBtnId = "btn-item__delete";

        private By _dimensionNameLocator = By.XPath($".//*[contains(@data-auto-id,'{DimensionNameInputId}')]");
        private By _progressionalInputLocator = By.XPath($".//*[@data-auto-id='{ProgressionalOrderInputId}']");
        private readonly By _dimensionPropertyLocator = By.XPath($".//*[contains(@data-auto-id, '{DimensionPropertiesPrefix}')]");
        private readonly By _dimensionPropertyDeleteButtonLocator = By.XPath($".//*[@data-auto-id='{DeleteDimensionPropertyBtnId}']");

        private IWebElement ProgressionInputSwitch => _webDriver.SafeFindClickableElement(_progressionalInputLocator);
        private IReadOnlyCollection<IWebElement> DimensionProperties => _webDriver.FindElements(_dimensionPropertyLocator);

        public EditName DimensionNameInput => new EditName(_webDriver, _dimensionNameLocator);

        public IWebElement AddDimensionPropertyButton => _webDriver.SafeFindElementByDataAutoId(AddDimensionPropertyBtnId);
        
        public DimensionForm(IWebDriver webDriver) : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public DimensionPage ApplyDimension()
        {
            base.ClickApply();
            return new DimensionPage(_webDriver);
        }

        public bool ToggleProgressional()
        {
            ProgressionInputSwitch.Click();
            return IsProgressional();
        }

        public bool IsProgressional()
        {
            string elementClass = ProgressionInputSwitch.GetAttribute("class");

            return elementClass.Contains(ActiveClassName);
        }

        public void AddDimensionProperty()
        {
            AddDimensionPropertyButton.Click();
        }

        public List<string> ReadDimensionProperties()
        {
            return DimensionProperties.Select(ReadTextInputValue).ToList();
        }

        public void DeleteDimensionProperty(string name)
        {
            var dimensionProperty = DimensionProperties.FirstOrDefault(dp => ReadTextInputValue(dp) == name);
            Assert.IsTrue(IsDimensionPropertyEditable(name), $"{name} is not an editable dimension property");
            dimensionProperty.FindElement(_dimensionPropertyDeleteButtonLocator).Click();
        }

        public bool IsDimensionPropertyEditable(string name)
        {
            var dimensionProperty = DimensionProperties.FirstOrDefault(dp => ReadTextInputValue(dp) == name);
            var textInput = dimensionProperty.FindElement(By.TagName("input"));

            return textInput.Enabled;
        }

        public bool IsDimensionPropertyDeletable(string name)
        {
            var dimensionProperty = DimensionProperties.FirstOrDefault(dp => ReadTextInputValue(dp) == name);
            var elements = dimensionProperty.FindElements(_dimensionPropertyDeleteButtonLocator);
            return elements.Count > 0;
        }

        public void RenameDimensionProperty(string oldName, string newName)
        {
            var dimensionProperty = DimensionProperties.FirstOrDefault(dp => ReadTextInputValue(dp) == oldName);
            Assert.IsTrue(IsDimensionPropertyEditable(oldName), $"{oldName} is not an editable dimension property");
            var textInput = dimensionProperty.FindElement(By.TagName("input"));
            textInput.Clear();
            textInput.SendKeys(newName);
        }

        public bool IsAddPropertyDisplayed()
        {
            return AddDimensionPropertyButton.Displayed;
        }

        private string ReadTextInputValue(IWebElement element)
        {
            return element.FindElement(By.TagName("input")).GetAttribute("value");
        }
    }
}
