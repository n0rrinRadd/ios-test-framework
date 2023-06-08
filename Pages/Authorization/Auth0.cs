using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ios_tests.Helpers;

namespace ios_tests.Pages.Authorization
{
    public class Auth0
    {
        private IWebDriver _webDriver;
        private const string LockCredPaneClassName = "auth0-lock-cred-pane";
        private const string IsNoSubmitClassName = "auth0-lock-no-submit";
        private const string IsMovingClassName = "auth0-lock-moving";
        private const string InputClassName = "auth0-lock-input";
        private const string SubmitButtonClassName = "auth0-lock-submit";
        private readonly By LockCredPaneLocator = By.ClassName(LockCredPaneClassName);
        private readonly By IsNoSubmitLocator = By.ClassName(IsNoSubmitClassName);
        private readonly By IsMovingLocator = By.ClassName(IsMovingClassName);
        private readonly By InputLocator = By.ClassName(InputClassName);
        private readonly By SubmitButtonLocator = By.ClassName(SubmitButtonClassName);

        public Auth0(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _webDriver.WaitUntil(driver => !IsMoving() && CanSubmit());
        }

        private IWebElement LockCredPane => _webDriver.SafeFindElement(LockCredPaneLocator);

        private IWebElement EmailInput => _webDriver.SafeFindElement(new ByAll(InputLocator, By.Name("email")));

        private IWebElement PasswordInput => _webDriver.SafeFindElement(new ByAll(InputLocator, By.Name("password")));

        private IWebElement SubmitButton => _webDriver.SafeFindClickableElement(SubmitButtonLocator);

        private bool IsMoving()
        {
            var elements = _webDriver.FindElements(IsMovingLocator);
            return elements.Count > 0;
        }

        private bool CanSubmit()
        {
            bool AreInputsVisible = (EmailInput?.Displayed ?? false) && (PasswordInput?.Displayed ?? false);
            var elements = _webDriver.FindElements(IsNoSubmitLocator);

            return AreInputsVisible && elements.Count == 0;
        }

        public void TypeEmail(string email)
        {
            EmailInput.SendKeys(email);
        }

        public void TypePassword(string password)
        {
            PasswordInput.SendKeys(password);
        }

        public void Submit()
        {
            Assert.IsTrue(CanSubmit());
            SubmitButton.Submit();
            _webDriver.WaitForJQueryAndJSToLoad();
        }
    }
}
