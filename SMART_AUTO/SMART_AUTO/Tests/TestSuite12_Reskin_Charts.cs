using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace SMART_AUTO.SMART_AUTO
{
    public class TestSuite12_Reskin_Charts : Base
    {
        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        Search searchPage;
        FieldOptions fieldOptions;
        UserProfile userProfile;
        Schedule schedule;
        BrandDashboard brandDashboard;
        MyExportsPage myExportsPage;
        SecondaryButtons secondaryButtons;
        SummaryTags summaryTags;
        Charts charts;
        PivotGrid pivotGrid;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite12_Reskin_Charts).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite12_Reskin_Charts).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            userProfile = new UserProfile(driver, test);
            schedule = new Schedule(driver, test);
            brandDashboard = new BrandDashboard(driver, test);
            myExportsPage = new MyExportsPage(driver, test);
            secondaryButtons = new SecondaryButtons(driver, test);
            summaryTags = new SummaryTags(driver, test);
            charts = new Charts(driver, test);
            pivotGrid = new PivotGrid(driver, test);

            return driver;
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
            extent.Flush();
            driver.Quit();
        }

        #endregion

        #region Test Methods

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC001_VerifyTooltipOfChart_Bar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify Tooltip of Chart (Bar)");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                charts.VerifyTooltipsOnCharts();
                schedule.VerifyScheduleIcon();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
         public void TC002_3_VerifyTooltipOfChartAfterChangingAnyLegend(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002_3-Verify Tooltip of chart after change any legend");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                charts.VerifyCharts();
                charts.updateAndVerifyLegendsInCharts(true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC002_3");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyChartShouldGetUpdatedAccordingToUserAppliedFilterFromSearchOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify Chart should get updated according to user applied filter from search options");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                charts.VerifyCharts();
                homePage.VerifyAndModifySearchOptions();
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.selectNewDateRangeOptionFromSection();
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                charts.VerifyDateOnChart();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyChartShouldGetUpdatedWhileUserApplyAnySavedSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify Chart should get updated while user apply any saved search");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                charts.VerifyCharts();
                string[,] dataGrid1 = charts.captureDataFromChart("");
                homePage.selectSavedSearchOrCreateNewSavedSearch();
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                string[,] dataGrid2 = charts.captureDataFromChart("");
                charts.VerifyDataFromTwoCharts(dataGrid1, dataGrid2, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyExpandChartFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify Expand Chart functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                charts.VerifyCharts();
                charts.VerifyExpandChartFunctionality();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_17_VerifyAgGridDataShouldBeDisplayedAccordingToSelectedParticularBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007_17-Verify AgGrid data should be displayed according to selected particular bar");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                charts.VerifyCharts();
                charts.VerifyExpandChartFunctionality();
                charts.VerifyAgGridAsPerSelectedBarFromExpandedChart();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC007_17");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_18_VerifyExpandChartFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008_18-Verify User able to Go back to original Chart");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                charts.VerifyCharts();
                charts.VerifyExpandChartFunctionality(true);
                charts.VerifyCharts();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC008_18");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyTooltipOfPieChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify Tooltip of Pie chart");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                schedule.VerifyScheduleIcon();
                charts.VerifyTooltipsOnCharts("Count of Creatives Running by Competitor");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyTooltipOfPieChartAfterDeselectingAnyLegend(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify Tooltip of pie chart after user Deselect any legend");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                charts.VerifyCharts();
                charts.VerifyTooltipsOnCharts("Count of Creatives Running by Competitor");
                charts.updateAndVerifyLegendsInCharts(true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyDrillDownFunctionalityOfPieChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify Drill down functionality of Pie chart");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                charts.VerifyCharts();
                charts.VerifyTooltipsOnCharts("Count of Creatives Running by Competitor");
                string[] chartInfo = charts.updateAndVerifypieChart();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyTooltipAfterUserHasDrilledDownOnAnyWedgeOfPieChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify Tooltip after user Drill down in any wedges of Pie chart");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                charts.VerifyCharts();
                charts.VerifyTooltipsOnCharts("Count of Creatives Running by Competitor");
                string[] chartInfo = charts.updateAndVerifypieChart();
                charts.VerifyTooltipsOnCharts("Count of Creatives Running by Competitor");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC012");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyBackToTopCompaniesHyperLink(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify 'Back to Top companies' hyper link");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                charts.VerifyCharts();
                charts.VerifyTooltipsOnCharts("Count of Creatives Running by Competitor");
                string[] chartInfo = charts.updateAndVerifypieChart(true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC013");
                throw;
            }
            driver.Quit();
        }

        //TC014 is pending for Lack of Charts with high number of legends.

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_VerifyChartValueShouldGetUpdatedAccordingToFilterFromPivot(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify Chart value should get updated according to filter from Pivot");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Monthly");
                homePage.selectOptionFromSideNavigationBar("4. Ad Ex Trend - Yearly");
                charts.VerifyCharts(false);
                string[,] dataGrid1 = charts.captureDataFromChart("");
                pivotGrid.selectValueFromPivotGrid();
                charts.VerifyCharts(false);
                string[,] dataGrid2 = charts.captureDataFromChart("");
                charts.VerifyDataFromTwoCharts(dataGrid1, dataGrid2, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC015");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_VerifyChartValueShouldGetUpdatedAccordingToFilterFromPivotOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016-Verify Chart value should get updated according to filter from Pivot Options");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Monthly");
                homePage.selectOptionFromSideNavigationBar("4. Ad Ex Trend - Yearly");
                charts.VerifyCharts(false);
                string[,] dataGrid1 = charts.captureDataFromChart("");
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Total Media Spend Last Year (LY)");
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
                charts.VerifyCharts(false);
                string[,] dataGrid2 = charts.captureDataFromChart("");
                charts.VerifyDataFromTwoCharts(dataGrid1, dataGrid2, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite12_Reskin_Charts_TC016");
                throw;
            }
            driver.Quit();
        }

        ///TC016, TC017 and TC018 are pending due to defects WEB-7617 and WEB-7431

        #endregion
    }
}
