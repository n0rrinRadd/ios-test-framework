using System;
using System.Linq;
using Helpers;
using NUnit.Framework;
using ios_tests.Data;
using ios_tests.Pages.PageBodies;

namespace ios_tests.Tests
{
    [TestFixture, Category("navigation")]
    public class NavigationTests : BaseTest
    {
        private const int MaxNumStoryToNavigate = 2;

        /// <summary>
        /// LUMI-156
        /// User can see list of Stories
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_See_List_Of_Stories()
        {
        }

        /// <summary>
        /// LUMI-156
        /// Users can see list of Chapters in each Story
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_See_List_Of_Chapters_In_Each_Story()
        {
        }

        /// <summary>
        /// LUMI-203
        /// User can collapse the left panel side navigation
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Users_Can_Hide_Left_Panel_Side_Navigation()
        {
        }

        /// <summary>
        /// LUMI-157
        /// Using left navigation panel, user can navigate to any Story
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Navigate_To_Any_StorySnapshot()
        {
        }

        /// <summary>
        /// LUMI-157
        /// Using left navigation panel, user can navigate to any Chapter through Focused View
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Navigate_To_Any_Chapter_Thru_Focused()
        {
        }

        /// <summary>
        /// LUMI-157
        /// Using left navigation panel, user can navigate to any Chapter through Overview View
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Navigate_To_Any_Chapter_Thru_Overview()
        {
        }

        /// <summary>
        /// LUMI-176
        /// User can access settings menu when left nav panel is expanded or collapsed
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Access_Settings_Menu_When_Nav_Collapsed()
        {
        }

        /// <summary>
        /// LUMI-176
        /// User can go back to most recently visited Story
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Go_Back_To_Most_Recently_Visited_Story()
        {
        }

        /// <summary>
        /// LUMI-176
        /// User can navigate to the settings menu from anywhere in the app
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Navigate_To_The_Settings_Menu_From_Anywhere_In_The_App()
        {
        }

        /// <summary>
        /// LUMI-176
        /// User can get out of the settings menu and go back to most recently visited Story
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Return_To_Most_Recent_Story_To_Exit_Settings()
        {
        }


        /// <summary>
        /// User can click story link after clicking chapter link
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Click_Story_Link_From_Chapter_Page()
        {
        }

        /// <summary>
        /// Chapter labels displays when clicking Chapter link
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Chapter_Name_Displays_In_Chapter_Page_Header()
        {
        }

        /// <summary>
        /// LUMI-884
        /// Navigating chapters and stories crashes front-end with JS errors.
        /// QA Regression test for bug fix - LUMI-652.
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Navigate_Between_Story_And_Chapter()
        {
        }

        /// <summary>
        /// LUMI-446
        /// Navigating through each My Stories, Chapters, Insights, then check for JS errors.
        /// QA Regression test suite case.
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Navigate_To_All_Stories_Chapters_And_Insights()
        {
        }

        /// <summary>
        /// LUMI-787
        /// Calendar displays in a new Story
        /// </summary>
        [Test, RetryDynamic, Category("DesignServer"), Category("CIRegressionTests")]
        public void Calendar_Displays_In_New_Story()
        {
        }

        /// <summary>
        /// LUMI-326-002
        /// Navigate from Table to Profile / Table Row Detail View
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Navigate_To_Table_Row_View()
        {
        }
    }
}