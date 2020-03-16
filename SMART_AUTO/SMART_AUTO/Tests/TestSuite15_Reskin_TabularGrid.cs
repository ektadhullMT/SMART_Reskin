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
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class TestSuite15_Reskin_TabularGrid : Base
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
        PivotGrid pivotGrid;
        Charts charts;
        MyExportsPage myExportsPage;
        TabularGrid tabularGrid;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite15_Reskin_TabularGrid).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite15_Reskin_TabularGrid).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            userProfile = new UserProfile(driver, test);
            schedule = new Schedule(driver, test);
            brandDashboard = new BrandDashboard(driver, test);
            pivotGrid = new PivotGrid(driver, test);
            charts = new Charts(driver, test);
            myExportsPage = new MyExportsPage(driver, test);
            tabularGrid = new TabularGrid(driver, test);

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
        public void TC001_15_VerifyTheTabularFunctionalityInCharts(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify the tabular functionality in Charts");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_3_24_VerifyTheDownloadGridFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002_3_24-Verify the Download Grid Functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Advertiser", "Category", "Subcategory", "Product", "Media Month 12" });
                string[,] dataGrid = tabularGrid.CaptureDataFromTabularGrid();
                tabularGrid.VerifyTabularGridBulkActionsButtonAndChooseOption("Download Grid");
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen("tabular", "*.xlsx");
                tabularGrid.VerifyDataFromTabularGridInExportedExcelFile(fileName, dataGrid);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC002_3_24");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_16_VerifyTheViewSelectedFunctionality_IncludeWhenClickedAgainOnViewSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004_16-Verify the View Selected Functionality(Include when clicked again on view selected)");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Advertiser", "Category", "Subcategory", "Product", "Media Month 12" });
                tabularGrid.FindValuesToSelectRecordsFromGrid(true, false, 3);
                tabularGrid.VerifyTabularGridBulkActionsButtonAndChooseOption("View Selected", true);
                IList<IWebElement> rowsCollection = driver._findElements("xpath", "//cft-tabular-table//div[contains(@style, 'display: inline')]//div[@role='row' and @row-id]");
                Assert.AreEqual(3, rowsCollection.Count, "'View Selected' Functionality did not work.");
                Results.WriteStatus(test, "Pass", "Verified, 'View Selected' Functionality successfully.");

                tabularGrid.VerifyTabularGridBulkActionsButtonAndChooseOption("View Selected", true);
                rowsCollection = driver._findElements("xpath", "//cft-tabular-table//div[contains(@style, 'display: inline')]//div[@role='row' and @row-id]");
                Assert.Less(3, rowsCollection.Count, "Clicking again on 'View Selected' Functionality did not work.");
                IList<IWebElement> selectedCells = driver._findElements("xpath", "//cft-tabular-table//div[@role='row' and @row-id]//div[contains(@class, 'selected')]/span");
                Assert.AreEqual(3, selectedCells.Count, "Clicking on 'View Selected' did not retain selected cells.");
                Results.WriteStatus(test, "Pass", "Verified, Clicking again on 'View Selected' Functionality displays all rows including selected.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC004_16");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyTheResetSelectedFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify Reset selected functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Advertiser", "Category", "Subcategory", "Product", "Media Month 12" });
                tabularGrid.FindValuesToSelectRecordsFromGrid(true, false, 3);
                tabularGrid.VerifyTabularGridBulkActionsButtonAndChooseOption("Reset Selected", true);
                IList<IWebElement> selectedCells = driver._findElements("xpath", "//cft-tabular-table//div[@role='row' and @row-id]//div[contains(@class, 'selected')]/span");
                Assert.AreEqual(0, selectedCells.Count, "'Reset Selected' Functionality did not work.");
                Results.WriteStatus(test, "Pass", "Verified, 'Reset Selected' Functionality successfully.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_7_VerifySortingFunctionalityInTabularGrid(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006_7-Verify the sorting functionality in Tabular Grid");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Advertiser", "Category", "Subcategory", "Product", "Media Month 12" });
                tabularGrid.VerifySortingFunctionalityOnTabularGrid();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC006_7");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyThatTheTabularGridFieldsShouldBeAsPerTheChartData(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify the Tabular grid fields should be as per the Chart data");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts("Advertiser Rankings Period over Period");
                tabularGrid.VerifyThatDataColumnsFromTabularGridAreAsPerChartData("Advertiser Rankings Period over Period");

                driver.Navigate().Back();
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts("Leading Advertisers Media Mix");
                tabularGrid.VerifyThatDataColumnsFromTabularGridAreAsPerChartData("Leading Advertisers Media Mix");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC008");
                throw;
            }
            driver.Quit();
        }


        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_10_VerifyTheFilterFunctionalityForTheFirstColumn(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009_10-Verify the Filter Functionality for the First Column");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Product", "Media Month 12" });
                tabularGrid.VerifyFilterFunctionalityOnTextColumnsInTabularGrid();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC009_10");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_12_13_14_VerifyTheFilterFunctionalityForTheFirstColumn(String Bname)
        {
            TestFixtureSetUp(Bname, "TC0011_12_13_14-Verify the Filter Functionality for the First Column");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Product", "Media Month 12" });
                tabularGrid.VerifyFilterFunctionalityOnNumericColumnsInTabularGrid();
                tabularGrid.VerifyFilterFunctionalityOnNumericColumnsInTabularGrid("", "", "Not equal");
                tabularGrid.VerifyFilterFunctionalityOnNumericColumnsInTabularGrid("", "", "Less than");
                tabularGrid.VerifyFilterFunctionalityOnNumericColumnsInTabularGrid("", "", "Less than or equals");
                tabularGrid.VerifyFilterFunctionalityOnNumericColumnsInTabularGrid("", "", "Greater than");
                tabularGrid.VerifyFilterFunctionalityOnNumericColumnsInTabularGrid("", "", "Greater than or equals");
                tabularGrid.VerifyFilterFunctionalityOnNumericColumnsInTabularGrid("", "", "Greater than or equals");
                tabularGrid.VerifyFilterFunctionalityOnNumericColumnsInTabularGrid("", "10.50", "Not equal");
                tabularGrid.VerifyFilterFunctionalityOnNumericColumnsInTabularGrid("", "char");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC0011_12_13_14");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC017_VerifyThatTheMultipleFieldRecordsInSameRowsCannotBeSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017-Verify that the multiple field records in same rows cannot be selected");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Advertiser", "Category", "Subcategory", "Product", "Media Month 12" });
                tabularGrid.FindValuesToSelectRecordsFromGrid(false, true, 3);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC017");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_20_21_VerifyFieldsWith_NA_ValueShouldBeDisabled_NonClickable_AndShowRedIcon(String Bname)
        {
            TestFixtureSetUp(Bname, "TC018_20_21-Verify Fields with 'NA' value should be disabled (Non clickable) and show red icon");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Advertiser", "Category", "Subcategory", "Product", "Media Month 12" });
                tabularGrid.VerifyMousePointerWhenHoverOver0_Non0Elements();
            }
                catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC018_20_21");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC019_22_27_VerifyTheAgGridTableShouldUpdateAsPerTheSelectionMadeInTabularGrid(String Bname)
        {
            TestFixtureSetUp(Bname, "TC019_22_27-Verify the AgGrid table should update as per the selection made in Tabular grid");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                string filterValue = tabularGrid.FindValuesToSelectRecordsFromGrid(false, false, 1);
                tabularGrid.VerifyFilterAppliedOnTableGridViewBySelectingTabularGridCell(filterValue, true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC019_22_27");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC023_VerifyRecordsShouldRemainSelectedWhenUserApplyFilterOnAnyValue_BothNumericAndCharacterValue(String Bname)
        {
            TestFixtureSetUp(Bname, "TC023-Verify Records should be remain selected when user apply filter on any value (both numeric and character value)");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                string filterValue = tabularGrid.FindValuesToSelectRecordsFromGrid(false, false, 1);
                tabularGrid.VerifyFilterFunctionalityOnTextColumnsInTabularGrid();
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@role='gridcell' and contains(@class, 'selected')]"), "'Selected Rows not present after filter is applied.'");
                Results.WriteStatus(test, "Pass", "Verified, Records Remain Selected When User Apply Filter On Any Value");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC023");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC025_VerifyViewSelectedFunctionalityWhenRecordsAreDeselected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC025-Verify view selected functionality when records are de-selected");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Product", "Media Month 12" });
                tabularGrid.FindValuesToSelectRecordsFromGrid(true, false, 2);
                tabularGrid.VerifyTabularGridBulkActionsButtonAndChooseOption("View Selected", true);
                IList<IWebElement> rowsCollection = driver._findElements("xpath", "//cft-tabular-table//div[contains(@style, 'display: inline')]//div[@role='row' and @row-id]");
                Assert.AreEqual(3, rowsCollection.Count, "'View Selected' Functionality did not work.");
                Results.WriteStatus(test, "Pass", "Verified, 'View Selected' Functionality successfully.");

                tabularGrid.VerifyTabularGridBulkActionsButtonAndChooseOption("Reset Selected", true);
                IList<IWebElement> selectedCells = driver._findElements("xpath", "//cft-tabular-table//div[@role='row' and @row-id]//div[contains(@class, 'selected')]/span");
                Assert.AreEqual(0, selectedCells.Count, "Clicking on 'Reset Selected' did not deselected cells.");
                tabularGrid.VerifyTabularGridBulkActionsButtonAndChooseOption();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC025");
                throw;
            }
            driver.Quit();
        }

        //TC026 is pending due to creatives chart issue

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC028_29_30_31_32_33_VerifyTheTabularOptionsButtonFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC025-Verify the Tabular Options button functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Advertiser" }, false);
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Category", "Subcategory", "Product" }, true);
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Product", "Subcategory", "Category" }, false);
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Product" }, true);
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Category" }, false);
                tabularGrid.Select_DeselectColumnsFromTabularOptionsButton(new string[] { "Media Month 12" });
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC025");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC034_VerifyThatMediaMonthInTabularGridShouldBeDisplayedAsPerSelectedDateFromSearchOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC034-Verify, Media month in tabular Grid should be displayed as per selected Date from search options");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("QA Testing Monthly Report");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                tabularGrid.VerifyTabularFunctionalityOnCharts();
                homePage.VerifyAndModifySearchOptions(false, false, "Tabular Grid");
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.selectDateRangeOptionFromSection("Year To Date");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                tabularGrid.VerifyMediaMonthColumnsForAppliedDateFilter();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite15_Reskin_TabularGrid_TC034");
                throw;
            }
            driver.Quit();
        }


        #endregion
    }
}
