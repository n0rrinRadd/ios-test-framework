using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;
using ios_tests.Pages.NavigationPanels.Collapsible;
using ios_tests.Pages.PageBodies.Components;

namespace ios_tests.Pages.Modals
{
    public class NewStoryModal : BaseModal
    {
        private IWebDriver _webDriver;
        private const string ModalClassName = "modal--small";
        private const string CreateButtonId = "modal__btn-apply";
        private readonly By ModalClassLocator = By.ClassName(ModalClassName);

        public NewStoryModal(IWebDriver webDriver) : base(webDriver, ModalClassName)
        {
            _webDriver = webDriver;
            Assert.IsTrue(IsModalDisplayed());
        }
        
        private IWebElement CreateButton => _webDriver.SafeFindClickableElementByDataAutoId(CreateButtonId);

        public EditName StoryNameInput => new EditName(_webDriver, ModalClassLocator);

        public ActionBar CreateNewStory(string storyName)
        {
            StoryNameInput.SetName_WithoutCheck(storyName);
            CreateButton.Click();
            _webDriver.WaitUntil(ExpectedConditions.StalenessOf(ModalWindow));

            return new ActionBar(_webDriver);
        }

        public OverviewCollapsible CancelNewStory()
        {
            ClickCancelButton();            
            return new OverviewCollapsible(_webDriver);
        }
    }
}
