using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.PageBodies.Components;

namespace ios_tests.Pages.Metadata.Form
{
    public class HierarchyForm : BaseForm
    {
        private readonly IWebDriver _webDriver;

        private const string HierarchyNameInputId = "text-input__hierarchy";
        private const string AddLevelButtonId = "hierarchy-level__add-new";
        public const string DimensionListClassId = "box-list";
        public const string HierarchyLevelIdPrefix = "hierarchy-level__";

        public By HierarchyNameLocator = By.XPath($".//*[contains(@data-auto-id,'{HierarchyNameInputId}')]");
        public By DimensionListLocator = By.XPath($".//*[contains(@class, '{DimensionListClassId}')]");

        public EditName HierarchyNameInput => new EditName(_webDriver, HierarchyNameLocator);
        public IWebElement AddLevelButton => _webDriver.SafeFindElementById(AddLevelButtonId);
        private ItemSelectBoxList _dimensionList => new ItemSelectBoxList(_webDriver, DimensionListLocator);

        public HierarchyForm(IWebDriver webDriver) : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public ItemSelectBoxList ClickAddLevel()
        {
            AddLevelButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.ElementIsVisible(DimensionListLocator));
            return _dimensionList;
        }

        public void WaitTillHierarchyLevelExists(int level)
        {
            string levelId = string.Concat(HierarchyLevelIdPrefix, level.ToString());
            By hierarchyLevelLocator = By.XPath($".//*[contains(@id,'{levelId}')]");

            _webDriver.WaitUntil(ExpectedConditions.ElementIsVisible(hierarchyLevelLocator));
        }

        public HierarchyPage ApplyHierarchy()
        {
            base.ClickApply();
            return new HierarchyPage(_webDriver);
        }
    }
}
