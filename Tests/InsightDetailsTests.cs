using Helpers;
using NUnit.Framework;
using ios_tests.Pages.Modals;
using System.Collections.Generic;
using System.Linq;
using ios_tests.Helpers;
using ios_tests.Pages.PageBodies;

namespace ios_tests.Tests
{
    [TestFixture, Category("insightDetails")]
    public class InsightDetailsTests : BaseTest
    {
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Clicking_Dots_Opens_Insight_Details()
        {
        }

        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Clicking_Insight_Filter_Button_Loads_Filter_Modal()
        {
        }

        /// <summary>
        /// LUMI-979
        /// Check that user can open and close the Chart Type selection form
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Access_Visualize_Chart_Type_Selection()
        {
        }

        /// <summary>
        /// LUMI-979
        /// Check that Visualize contains the expected Chart Type: Bar, GroupBar, Area, Line, Gride.
        /// Make sure each chart type contains the correct header.
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Validate_Visualize_Types()
        {
        }

        /// <summary>
        /// LUMI-992
        /// When user select a visualize, it will be displayed correctly in the Chapter and Insight view
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Select_Any_Visualize()
        {
        }

        /// <summary>
        /// LUMI-657
        /// Verify that default chart legends accurately displayed per dimension filter options
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Validate_Chart_Legends()
        {
        }

        /// <summary>
        /// LUMI-1082
        /// Download an insight data and confirm that the data file is of Excel format with size greater than 1K
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Export_Insight_Data_To_Excel()
        {
        }

        /// <summary>
        /// LUMI-1010
        /// Open the first chapter and insight content, select a Bar Chart.
        /// Click on each legend and verify all its corresponding data points. Check that the data
        /// are highlighted when click the first time, then de-emphasize when it is cliecked again.
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Chart_Legends_In_Insight_Detail_Can_Highlight_Toggle_Data_When_Clicked()
        {
        }

        /// <summary>
        /// LUMI-1010
        /// Open the first chapter and insight tile content, select a Line Chart.
        /// Click on each legend and verify all its corresponding data points. Check that the data
        /// are highlighted when click the first time, then de-emphasize when it is cliecked again.
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Chart_Legends_In_Insight_Tile_Can_Highlight_Toggle_Data_When_Clicked()
        {
        }

        /// <summary>
        /// LUMI-1066
        /// Insight table can scroll (page) down and table row is expected to increase
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Perform_Table_Paging()
        {
        }

        /// <summary>
        /// LUMI-1457        
        /// User can set insight filter and click reset to change everything back to its default selections.
        /// QA Regression test suite case (LUMI-286-002)
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void User_Can_Reset_Insight_Filter_Selections()
        {
        }

        /// <summary>
        /// LUMI-1722
        /// Validate Insight Filters
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Validate_Insight_Filters()
        {
        }

        /// <summary>
        /// LUMI-1570
        /// Visualization / chart selection persist after browser refresh
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("QARegressionTests")]
        public void Visualization_Persistence_Browser_Refresh()
        {
        }

        /// <summary>
        /// LUMI-1570
        /// Visualization / chart selection persist with new login session
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("QARegressionTests")]
        public void Visualization_Persistence_New_Login_Session()
        {
        }

        /// <summary>
        /// LUMI-1569
        /// Insight filter selection persists after navigating between chapters
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Insight_Filter_Persistence_Navigation()
        {
        }

        /// <summary>
        /// LUMI-1569
        /// Insight filter selection persists after browser refresh
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Insight_Filter_Persistence_Browser_Refresh()
        {
        }

        /// <summary>
        /// LUMI-1569
        /// Insight filter selection persists after browser refresh
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Insight_Filter_Persistence_New_Login_Session()
        {
             Check_JavaScript_Errors();
        }
    }
}
