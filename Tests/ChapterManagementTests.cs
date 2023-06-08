using System;
using Helpers;
using NUnit.Framework;
using System.Linq;
using ios_tests.Helpers;

namespace ios_tests.Tests
{
    [TestFixture, Category("chapterManagement")]
    public class ChapterManagementTests : BaseTest
    {
        /// <summary>
        /// LUMI-1569
        /// Chapter filter selection persists after navigation between chapter
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Chapter_Filter_Persistence_Navigation()
        {
        }

        /// <summary>
        /// LUMI-1569
        /// Chapter filter selection persists after browser refresh
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Chapter_Filter_Persistence_Browser_Refresh()
        {
        }

        /// <summary>
        /// LUMI-1569
        /// Chapter filter selection persists after re-login
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Chapter_Filter_Persistence_New_Session()
        {
        }
    }
}
