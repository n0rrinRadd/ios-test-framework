using System;
using System.Linq;
using Helpers;
using NUnit.Framework;
using ios_tests.Helpers;

namespace ios_tests.Tests
{
    [TestFixture, Category("storyManagement")]
    public class StoryManagementTests : BaseTest
    {
        /// <summary>
        /// LUMI-907
        /// View mode accuracy test of story and tile displayed info
        /// </summary>        
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Opened_Story_Title_Matches_Selected_Story_Title()
        {
        }

        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Displayed_Chapters_Title_Matches_Chapters_Listed()
        {
        }

        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Displayed_Insight_Count_Is_Accurate()
        {
        }

        /// <summary>
        /// PQA-300-001
        /// Set up QA regression test data via ios front-end        
        /// </summary>
        [Test, Category("GenerateQAEnvironment")]
        public void Setup_QA_Test_Data()
        {
        }
    }
}
