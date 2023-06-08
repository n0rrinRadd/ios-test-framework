using Helpers;
using NUnit.Framework;
using ios_tests.Helpers;
using ios_tests.Pages.PageBodies;
using System.Linq;

namespace ios_tests.Tests
{
    [TestFixture, Category("profileEntity")]
    public class ProfileEntityTests : BaseTest
    {
        //private string _storyName = Domain.Constants.VisualObjectName.StoryProfileChapters;

        [OneTimeSetUp]
        public void SetUp()
        {
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
        }

        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanCollapseAndExpandEntityListPanel()
        {
        }

        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanSelectProfileCardByName()
        {
        }

        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void ProfileEntityHasNoInsights()
        {
        }

        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanSaveAndUnsaveAProfileCard()
        {
        }

        /// <summary>
        /// LUMI-328-004 Empty Saved Section
        /// Test the empty saved section has all valid UI string components.
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void EmptySavedSectionValidation()
        {
        }

        /// <summary>
        /// LUMI-328-001 Access Favorite Entity Profile from Saved Section
        /// Test that saved entity profile can be found and viewed in Saved Section
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanAccessFavoriteEntityProfile()
        {
        }

        /// <summary>
        /// LUMI-328-003 Remove Favorite Entity Profile from Saved Section
        /// Test that saved entity profile can be removed from Saved Section
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanRemoveFavoriteEntityProfileFromSavedSection()
        {
        }

        /// <summary>
        /// LUMI-329-002 Profile Search - Search String
        /// Test that entity profile can be found by Full Name
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanSearchEntityByFullName()
        {
        }

        /// <summary>
        /// LUMI-329-002 Profile Search - Search String
        /// Test that entity profile can be found by First Name
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanSearchEntityByFirstName()
        {
        }

        /// <summary>
        /// LUMI-329-002 Profile Search - Search String
        /// Test that entity profile can be found by Last Name
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanSearchEntityByLastName()
        {
        }

        /// <summary>
        /// LUMI-329-002 Profile Search - Search String
        /// Test that entity profile can be found by Partial wildcard
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanSearchEntityByWildCardStrings()
        {
        }

        /// <summary>
        /// LUMI-329-002 Profile Search - Search String
        /// Test that search with empty/null string returns all existing profiles
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanSearchEntityWithEmptyString()
        {
        }

        /// <summary>
        /// LUMI-329-002 Profile Search - Search String
        /// Test that user can search with special chars, and displays "No profile to display" when not found
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanSearchWithNonExistEntityNameAndSpecialCharacters()
        {
        }

        /// <summary>
        /// LUMI-329-002 Profile Search - Search String
        /// Verify that search input & result persist between entity tabs
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserSearchTokenAndResultPersistBetweenTabs()
        {
        }

        /// <summary>
        /// LUMI-329-002 Profile Search - Search String
        /// Verify the search and clear icon/button
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void UserCanPerformActionOnEntityClearandSearchIcon()
        {
        }
    }
}
