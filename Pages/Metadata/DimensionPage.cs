using System.Collections.Generic;
using OpenQA.Selenium;
using ios_tests.Pages.Metadata.Form;

namespace ios_tests.Pages.Metadata
{
    public class DimensionPage : BasePage
    {
        private readonly IWebDriver _webDriver;

        public DimensionPage(IWebDriver webDriver) : base(webDriver, "dimension")
        {
            _webDriver = webDriver;
        }

        public List<string> GetDimensionNames()
        {
            return GetItemNames();
        }

        public bool DoesDimensionExist(string name)
        {
            return DoesItemExist(name);
        }

        public DimensionForm ClickAddDimension()
        {
            return ClickAddItem<DimensionForm>();
        }

        public DimensionForm GoToDimension(string dimensionName)
        {
            return GoToItem<DimensionForm>(dimensionName);
        }

        public DimensionPage DeleteDimension(string dimensionName)
        {
            return DeleteItem<DimensionPage>(dimensionName);
        }
    }
}
