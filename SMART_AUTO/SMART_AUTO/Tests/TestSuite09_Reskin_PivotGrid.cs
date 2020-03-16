using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMART_AUTO.SMART_AUTO
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class TestSuite09_Reskin_PivotGrid : Base
    {
        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        Search searchPage;
        FieldOptions fieldOptions;
        UserProfile userProfile;
        Carousels carousels;
        BrandDashboard brandDashboard;
        PivotGrid pivotGrid;
        Charts charts;
        Schedule schedule;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite09_Reskin_PivotGrid).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite09_Reskin_PivotGrid).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            userProfile = new UserProfile(driver, test);
            carousels = new Carousels(driver, test);
            brandDashboard = new BrandDashboard(driver, test);
            pivotGrid = new PivotGrid(driver, test);
            charts = new Charts(driver, test);
            schedule = new Schedule(driver, test);

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
        public void TC001_VerifyPivotOptionsFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify Pivot options functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                homePage.newVerifyHomePage();
                charts.VerifyPivotTable();
                pivotGrid.VerifyPivotOptionsPopup();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyPivotFieldsCheckboxOptionsFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify Pivot fields checkbox options functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                charts.VerifyPivotTable();
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Division");
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Submedia");
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Division", false);
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Company", false);
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC002");
                throw;
            }
            driver.Quit();
        }

        //TC003 is pending because of WEB-7580

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyPivotGridWhenNoPivotFieldsOptionsAreSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify Pivot Grid when no Pivot fields options are selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                charts.VerifyPivotTable();
                pivotGrid.VerifyPivotOptionsPopup();
                string[] selectedOptions = pivotGrid.readSelectedPivotOptions();
                foreach (string selected in selectedOptions)
                    pivotGrid.select_deselectOptionFromPivotOptionsPopup(selected, false);
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
                pivotGrid.VerifyColumnsInPivotGrid(selectedOptions, null);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyMetricsFieldOptionsFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify Metrics Field options functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                charts.VerifyPivotTable();
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Total Spend Last Year (LY)");
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
                pivotGrid.VerifyPivotOptionsPopup(false);
                string[] columnNames = pivotGrid.readColumnNamesFromPivotGrid();
                string[] selectedOption = { "Spend LY"};
                pivotGrid.VerifyColumnsInPivotGrid(selectedOption, columnNames);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyFormattingOptionInPivotOptionsFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify Formatting option in 'Pivot options' functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                homePage.newVerifyHomePage();
                charts.VerifyPivotTable();
                decimal[] decValuesList1 = pivotGrid.captureSpendValueInTotalColumn();
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Spend in Thousands $(000)");
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
                pivotGrid.VerifyPivotOptionsPopup(false);
                decimal[] decValuesList2 = pivotGrid.captureSpendValueInTotalColumn();
                pivotGrid.VerifyThatSpendInDollarsOrThousandsIsApplied(decValuesList1, decValuesList2);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC006");
                throw;
            }
            driver.Quit();
        }

        //TC007 to TC009 are pending due to defect on Rank On Functionality

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_12_VerifyShowSummaryTotalsFunctionalityInOtherOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010_12-Verify 'Show summary totals' functionality in Other options ");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                charts.VerifyPivotTable();
                decimal[] decValuesList1 = pivotGrid.captureSpendValueInTotalColumn();
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Show Summary Totals");
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
                pivotGrid.VerifyPivotOptionsPopup(false);
                string[] columnNames = pivotGrid.readColumnNamesFromPivotGrid();
                pivotGrid.VerifySummaryTotalRowInPivotGrid(columnNames);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC010_12");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyResetButtonFunctionalityOfPivotOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify Reset button functionality of Pivot Options");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                charts.VerifyPivotTable();
                decimal[] decValuesList1 = pivotGrid.captureSpendValueInTotalColumn();
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Show Summary Totals");
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Submedia");
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Division", false);
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Company", false);
                pivotGrid.clickButtonOnPivotOptionsPopup("Reset");
                pivotGrid.VerifyPivotOptionsPopup(true, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifySortingFunctionalityOfPivotGrid(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify Sorting Functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                charts.VerifyPivotTable();
                pivotGrid.VerifySortingFunctionalityOfPivotGrid();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_15_VerifyUserAbleToMinimize_MaximizePivotGridColumn(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014_15-Verify User able to minimize-maximize pivot grid column");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                charts.VerifyPivotTable();
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Total Spend Last Year (LY)");
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
                pivotGrid.VerifyPivotOptionsPopup(false);
                charts.VerifyPivotTable();
                string[] pivotFieldOption = { "Spend CP", "Spend LY" };
                string[] columnNames = pivotGrid.readColumnNamesFromPivotGrid();
                pivotGrid.VerifyTheMinimizeIconOnTotalColumn(pivotFieldOption, columnNames);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC014_15");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_VerifyFilterFunctionalityOfPivotFieldsColumns_Class_CompanyEtc(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016-Verify Filter functionality of pivot fields columns (Class,Company etc…)");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                charts.VerifyPivotTable();
                pivotGrid.VerifyFilterFunctionalityOnTextFieldsInPivotGrid();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC016");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC017_18_19_20_VerifyFilterFunctionalityOfPivotFieldsColumns_SpendCP_PagesCPEtc(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017_18_19_20-VerifyFilterFunctionalityOfPivotFieldsColumns_SpendCP_PagesCPEtc");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                homePage.newVerifyHomePage();
                charts.VerifyPivotTable();
                pivotGrid.VerifyFilterFunctionalityOnNumericFieldsInPivotGrid();
                pivotGrid.VerifyFilterFunctionalityOnNumericFieldsInPivotGrid("Random", "Not Equal");
                pivotGrid.VerifyFilterFunctionalityOnNumericFieldsInPivotGrid("Random", "Less Than");
                pivotGrid.VerifyFilterFunctionalityOnNumericFieldsInPivotGrid("Random", "Less Than or Equals");
                pivotGrid.VerifyFilterFunctionalityOnNumericFieldsInPivotGrid("Random", "Greater Than", "5.5");
                pivotGrid.VerifyFilterFunctionalityOnNumericFieldsInPivotGrid("Random", "Greater Than or Equals");
                pivotGrid.VerifyFilterFunctionalityOnNumericFieldsInPivotGrid("Random", "In Range");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC017_18_19_20");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC021_VerifyFilterResultWhenUserEntersCharacterValues(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017-Verify Filter result when user enter character values");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                homePage.newVerifyHomePage();
                charts.VerifyPivotTable();
                pivotGrid.VerifyFilterFunctionalityOnNumericFieldsInPivotGrid("Random", "Equals", "Querty");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC017");
                throw;
            }
            driver.Quit();
        }

        //TC022 and TC023 is not created as it is failing in manual testing

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC023_VerifyThatUserCanSelectMultipleRecordsFromSameColumnAsFilterAndSelectedRecordFieldShouldDisplayInDeepTealColor(String Bname)
        {
            TestFixtureSetUp(Bname, "TC023-Verify User can  select multiple records from same column as filter and selected record field should display in deep teal color");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                homePage.newVerifyHomePage();
                charts.VerifyPivotTable();
                pivotGrid.findValuesToSelectRecordsFromGrid(true, false, 4);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC023");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC024_28_VerifyThatUserCannotSelectMultipleRecordsFromSameRowAsFilterAndSelectedRecordFieldShouldDisplayInDeepTealColor(String Bname)
        {
            TestFixtureSetUp(Bname, "TC024_28-Verify User can not select multiple records from same row as filter and selected record field should display in deep teal color");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Market");
                homePage.newVerifyHomePage();
                charts.VerifyPivotTable();
                pivotGrid.findValuesToSelectRecordsFromGrid(false, true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC024_28");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC025_VerifyThatPivotGridColumnShouldBeDisplayedAccordingToSelectedReports(String Bname)
        {
            TestFixtureSetUp(Bname, "TC025-Verify Pivot Grid column should be displayed according to Selected reports");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                pivotGrid.VerifyPivotGridColumnsForMarketAndMediaReports();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC025");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC026_VerifyThatUserIsAbleToFilterRecordBySelectingRowsFromPivotGrid_MetricsFieldOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC026-Verify User able to filter record by selecting rows from Pivot Grid (Metrics field options)");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Media");
                charts.VerifyPivotTable();
                pivotGrid.VerifyFilterAppliedOnTableGridViewBySelectingPivotGridCell("HORNBY MANAGEMENT INC");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC026");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC027_29_30_VerifyThatUserIsAbleToFilterRecordBySelectingRowsFromPivotGrid_MetricsFieldOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC027_29_30-Verify User able to filter record by selecting rows from Pivot Grid (Metrics field options)");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Media");
                charts.VerifyPivotTable();
                pivotGrid.VerifyMousePointerWhenHoverOver0_Non0Elements();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC027_29_30");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC031_32_33_44_VerifyPivotGridWhenUserSelectsRecordsFromPivotGridAndThenSelects_Deselects_ShowSummaryTotals_CheckboxFromPivotOptionsAndAppliesFilter(String Bname)
        {
            TestFixtureSetUp(Bname, "TC031_32_33_44-Verify Pivot Grid When user Select Records from Pivot grid and then selects  “Show Summary totals” checkbox from Pivot Options");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Media");
                charts.VerifyPivotTable();
                pivotGrid.findValuesToSelectRecordsFromGrid(false, false, 3);
                charts.choosePivotBulkActionsForDataFromPivotTable("View Selected");
                charts.VerifyPivotTable();
                IList<IWebElement> rowsCollection = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@role = 'row']");
                int selectedCount = rowsCollection.Count;
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Show Summary Totals");
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
                pivotGrid.VerifyPivotOptionsPopup(false);
                charts.VerifyPivotTable();
                string[] columnNames = pivotGrid.readColumnNamesFromPivotGrid();
                pivotGrid.VerifySummaryTotalRowInPivotGrid(columnNames);
                rowsCollection = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@role = 'row']");
                Assert.AreNotEqual(selectedCount, rowsCollection.Count, "Selection not removed after 'Show Summary Totals' is applied.");
                pivotGrid.VerifyFilterFunctionalityOnNumericFieldsInPivotGrid("Random", "In Range");
                pivotGrid.VerifySummaryTotalRowInPivotGrid(columnNames);

                pivotGrid.findValuesToSelectRecordsFromGrid(false, false, 3);
                charts.choosePivotBulkActionsForDataFromPivotTable("View Selected");
                charts.VerifyPivotTable();
                rowsCollection = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@role = 'row']");
                selectedCount = rowsCollection.Count;
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Show Summary Totals", false);
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
                pivotGrid.VerifyPivotOptionsPopup(false);
                charts.VerifyPivotTable();
                rowsCollection = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@role = 'row']");
                Assert.AreNotEqual(selectedCount, rowsCollection.Count, "Selection not removed after 'Show Summary Totals' is applied.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC031_32_33_44");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC034_VerifyPivotAndAgGridWhenUserClick_OpenAnyAdViewPopup(String Bname)
        {
            TestFixtureSetUp(Bname, "TC034-Verify Pivot and AgGrid when user click/open any Ad View Pop up ('view ad / details')");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Media");
                charts.VerifyPivotTable();
                pivotGrid.VerifyFilterAppliedOnTableGridViewBySelectingPivotGridCell("HORNBY MANAGEMENT INC", true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC034");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC035_39_40_41_VerifyDownloadGridFunctionlityAndThatTotalSummaryResultInPivotTableShouldBeAccurate(String Bname)
        {
            TestFixtureSetUp(Bname, "TC035_39_40_41-Verify Download Grid Functionlity And That Total summary result in pivot table should be accurate");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Media");
                charts.VerifyPivotTable();
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Show Summary Totals");
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
                pivotGrid.VerifyPivotOptionsPopup(false);
                charts.VerifyPivotTable();
                string[] columnNames = pivotGrid.readColumnNamesFromPivotGrid();
                pivotGrid.VerifySummaryTotalRowInPivotGrid(columnNames);
                string[,] dataGrid = charts.captureDataFromPivotTable();
                charts.choosePivotBulkActionsForDataFromPivotTable("Download Grid");
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen("brand_canada_-_media_spend-pivot", "*.xlsx");
                charts.VerifyDataFromPivotTableInExportedExcelFile(fileName, dataGrid);
                pivotGrid.VerifyColumnwiseSummaryTotalInExportedPivotData(fileName);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC035_39_40_41");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC036_VerifyThatSelectedRecordShouldRemainSelectedAfterSorting(String Bname)
        {
            TestFixtureSetUp(Bname, "TC036-Verify selected record should be remain selected after sorting");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Media");
                charts.VerifyPivotTable();
                pivotGrid.VerifySortingInPivotGrid();
                pivotGrid.findValuesToSelectRecordsFromGrid(false, false, 3);
                string[] selectedCells = pivotGrid.findSelectedValuesInPivotGrid();
                pivotGrid.VerifySortingInPivotGrid(false);
                pivotGrid.VerifySortingInPivotGrid();
                string[] sortedSelectedCells = pivotGrid.findSelectedValuesInPivotGrid();
                foreach(string cell in selectedCells)
                {
                    bool avail = false;
                    foreach(string sortedCell in sortedSelectedCells)
                        if (cell.ToLower().Equals(sortedCell.ToLower()))
                        {
                            avail = true;
                            break;
                        }
                    Assert.IsTrue(avail, "'" + cell + "' was not found selected after sorting.");
                }
                Results.WriteStatus(test, "Pass", "Verified, Selected Record Remains Selected After Sorting");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC036");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC037_VerifyPivotGridWhenNoMetricsOptionsAreSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC037-Verify Pivot Grid when no metrics options are selected");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Media");
                charts.VerifyPivotTable();
                pivotGrid.VerifyPivotOptionsPopup();
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Total Spend Current Period (CP)", false);
                pivotGrid.select_deselectOptionFromPivotOptionsPopup("Submedia Spend Current Period (CP)", false);
                pivotGrid.clickButtonOnPivotOptionsPopup("Apply");
                charts.VerifyPivotTable();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC037");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC038_VerifyThatPivotOptionsMetricsShouldBeDisplayedAccordingToSelecetdReport_QATesting_AdEx_MediaSpendAndMonthlySpend_InBrandMonthlyAccount(String Bname)
        {
            TestFixtureSetUp(Bname, "TC038-Verify Pivot options metrics should be displayed according to selecetd report (i.e QA Testing - Ad Ex - Media Spend  and Monthly spend) in Brand monthly account");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Monthly");
                homePage.selectOptionFromSideNavigationBar("QA Testing - Ad Ex - Media Spend");
                charts.VerifyPivotTable();
                pivotGrid.VerifyPivotOptionsPopup();
                string[] mediaOptions = pivotGrid.readSelectedMetricsOptions();
                pivotGrid.clickButtonOnPivotOptionsPopup("Cancel");
                pivotGrid.VerifyPivotOptionsPopup(false);
                homePage.selectOptionFromSideNavigationBar("QA Testing - Ad Ex - Monthly Spend");
                charts.VerifyPivotTable();
                pivotGrid.VerifyPivotOptionsPopup();
                string[] monthlyOptions = pivotGrid.readSelectedMetricsOptions();
                pivotGrid.clickButtonOnPivotOptionsPopup("Cancel");
                fieldOptions.compareListOfItemsInOrder(mediaOptions, monthlyOptions, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC038");
                throw;
            }
            driver.Quit();
        }

        //TC042 is pending for Defect on Rank On functionality

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC043_VerifyCreateAnAlertFunctionalityInPivotBulkActions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC043-Verify Create an Alert Functionality in Pivot Bulk Actions");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Media");
                charts.VerifyPivotTable();
                homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand Canada");
                charts.choosePivotBulkActionsForDataFromPivotTable("Create An Alert");
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "", "Weekly", true);
                schedule.clickButtonOnSchedulePopup("Confirm");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[text()=' Successfully created scheduled export! ']"), "Success message for Create an Alert not present.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC043");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC045_VerifyResetSelectedButtonFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC045-Verify Reset selected button functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Media");
                charts.VerifyPivotTable();
                pivotGrid.findValuesToSelectRecordsFromGrid(false, false, 3);
                charts.choosePivotBulkActionsForDataFromPivotTable("Reset Selected");
                pivotGrid.findSelectedValuesInPivotGrid();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC045");
                throw;
            }
            driver.Quit();
        }

        //TC046 is pending due to Creatives Chart Functionality

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC047_VerifyThatAllPivotDataIsDisplayed(String Bname)
        {
            TestFixtureSetUp(Bname, "TC047-Verify All Pivot data should display");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Media");
                charts.VerifyPivotTable();
                pivotGrid.findValuesToSelectRecordsFromGrid(false, false, 3);
                charts.choosePivotBulkActionsForDataFromPivotTable("View Selected");
                Thread.Sleep(2000);
                IList<IWebElement> rowsCollection = driver._findElements("xpath", "//cft-pivot-table//div[contains(@class, 'pinned-left')]//div[@role='row']");
                int selectedRows = rowsCollection.Count;
                charts.choosePivotBulkActionsForDataFromPivotTable("Reset Selected");
                rowsCollection = driver._findElements("xpath", "//cft-pivot-table//div[contains(@class, 'pinned-left')]//div[@role='row']");
                Assert.Less(selectedRows, rowsCollection.Count, "All rows are not displayed in Pivot Grid");
                Results.WriteStatus(test, "Pass", "All Pivot Data is visible after selection is removed");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite09_Reskin_PivotGrid_TC047");
                throw;
            }
            driver.Quit();
        }



        #endregion
    }
}
