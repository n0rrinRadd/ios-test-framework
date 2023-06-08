using Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using ios_tests.Helpers;

namespace ios_tests.Tests
{
    [TestFixture, Category("settings")]
    public class Auth0LoginTest : BaseTest
    {
        /// <summary>
        /// Users can login
        /// </summary>
        [Test, RetryDynamic, Category("ViewServer"), Category("CIRegressionTests"), Category("iOS")]
        public void User_Can_Access_The_Version_Number_Of_the_App()
        {
            var a = "blah";
        }

    }
}
