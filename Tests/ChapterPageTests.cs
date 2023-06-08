using System.Collections.Generic;
using System.Linq;
using Helpers;
using NUnit.Framework;
using ios_tests.Data;
using ios_tests.Helpers;
//using Shyft.VP.Domain.Deployment.MockData;

namespace ios_tests.Tests
{
    //using System.Data.SqlClient;
    //using Domain.ConfigurationSetting;

    [TestFixture, Category("chapterPage")]
    public class ChapterPageTests : BaseTest
    {
        private string _storyIdWithFilterOptions;
        private string _storyIdWithHierarchy;

        public ChapterPageTests()
        {
        }

        /// <summary>
        /// LUMI-285
        /// Clicking chapter filter button loads modal
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Clicking_Chapter_Filter_Button_Loads_Modal()
        {
        }

        /// <summary>
        /// LUMI-285
        /// If there are no chapter filters (i.e. no common filters among the Insights),
        /// the filter modal should have this text:
        /// 'Insights do not have common data. No filter options can be displayed.'
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Acknowledge_Lack_Of_Chapter_Filters()
        {
        }

        /// <summary>
        /// LUMI-285
        /// Chapter filter 'All' is always an option if filter has more than one values
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Chapter_Filter_All_Is_Always_An_Option()
        {
        }

        /// <summary>
        /// LUMI-285
        /// Chapter filter Default state is set to 'All' if options have more than one value
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Chapter_Filter_Default_State_Is_Set_To_All()
        {
        }

        /// <summary>
        /// LUMI-285
        /// For each filterable field, user must select 1 option from filter value list
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Only_One_Filter_Item_Is_Selectable_At_A_Time_Per_Filter()
        {
        }

        /// <summary>
        /// LUMI-154
        /// Name of insight is visible in both Chapter Tile and Insight Detail
        /// </summary>        
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Name_Of_Insight_Is_Visible()
        {
        }

        /// <summary>
        /// LUMI-50
        /// Checks that chapter insight charts display for each chapter
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Charts_Display()
        {
        }

        /// <summary>
        /// LUMI-285
        /// When filter is set, the data in each Insight in the Chapter are filtered according to filter definition
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Chapter_Filters_Affect_Data_Displayed()
        {
        }

        /// <summary>
        /// When "reset to default" is clicked, filter values return to their default states (typically this is "All")
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Reset_Filter_Returns_Selections_To_Defaults()
        {
        }

        /// <summary>
        /// Each active filter appears in the Filter List at the top of the chapter page
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Active_Filters_Appear_In_Filter_List()
        {
        }

        /// <summary>
        /// Clicking delete on an active filter button resets that filter to its default value
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Filter_List_Item_Delete_Resets_Filter_To_Default()
        {
        }
        /// <summary>
        /// LUMI-1723
        /// Canceling Insight Filters Reverts to the Chapter Filters
        /// </summary>
        [Test, RetryDynamic, Category("ReportingServer"), Category("CIRegressionTests")]
        public void Canceling_Insight_Filters_Reverts_To_Chapter_Filters()
        {
        }

        [TearDown]
        public void ClearSavedFilters()
        {
        }
    }
}
