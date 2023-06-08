using OpenQA.Selenium;
using ios_tests.Pages.Metadata.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ios_tests.Pages.Metadata
{
    public class HierarchyPage : BasePage
    {
        private readonly IWebDriver _webDriver;

        public HierarchyPage(IWebDriver webDriver) : base(webDriver, "hierarchy")
        {
            _webDriver = webDriver;
        }

        public List<string> GetHierarchyNames()
        {
            return GetItemNames();
        }

        public bool DoesHierarchyExist(string name)
        {
            return DoesItemExist(name);
        }

        public HierarchyForm ClickAddHierarchy()
        {
            return ClickAddItem<HierarchyForm>();
        }

        public HierarchyForm GoToHierarchy(string hierarchyName)
        {
            return GoToItem<HierarchyForm>(hierarchyName);
        }

        public HierarchyPage DeleteHierarchy(string hierarchyName)
        {
            return DeleteItem<HierarchyPage>(hierarchyName);
        }
    }
}
