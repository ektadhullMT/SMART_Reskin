﻿using NUnit.Framework;
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
    public class TestSuite012_BM_AnnualSummaryByCategory : Base
    {
        string userName = "Brand Monthly";

        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        Search searchPage;
        SummaryByCategory summaryByCategory;
        PromoDashboard promoDashboard;
        BrandMonthlyReport brandMonthlyReport;
        PivotReportScreen pivotReportScreen;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite012_BM_AnnualSummaryByCategory).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite012_BM_AnnualSummaryByCategory).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            summaryByCategory = new SummaryByCategory(driver, test);
            promoDashboard = new PromoDashboard(driver, test);
            brandMonthlyReport = new BrandMonthlyReport(driver, test);
            pivotReportScreen = new PivotReportScreen(driver, test);

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
        public void TC001_VerifyHeaderPanel(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify Header panel.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();
                homePage.VerifyHomePage();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifySearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify Search functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifySummaryByCategoryScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Monthly");
                searchPage.VerifyFieldMenuAndClickOnItOnSearchScreen("Date Range").selectDateRangeOptionFromSection();
                searchPage.VerifyFieldMenuAndClickOnItOnSearchScreen("Media").selectMediaCheckboxOptionForAnnualSummary();
                searchPage.clickButtonOnSearchScreen("Save As");
                searchPage.VerifySaveAsSectionAfterClickingOnSaveAsButton().enterSearchValueOnSearchScreen();
                searchPage.clickButtonOnSearchScreen("Save!").clickButtonOnSearchScreen("Apply Search");
                summaryByCategory.VerifySummaryByCategoryScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifySaveSearchCardInSavedSearchScreen(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify Save search Card in Saved search screen.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifySummaryByCategoryScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Monthly");
                searchPage.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC003");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyUserAbleToResetSavedSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify User able to Reset saved search.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifySummaryByCategoryScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Monthly").selectDateRangeOptionFromSection();
                searchPage.VerifyFieldMenuAndClickOnItOnSearchScreen("Media").selectMediaCheckboxOptionForAnnualSummary();
                searchPage.clickButtonOnSearchScreen("Reset").VerifyResetChangesMessageOnScreen(true);
                searchPage.VerifyAppliedSearchFieldInChartDetailsSection("None Selected");
                searchPage.VerifyFieldsRefreshIconDisableOnSummaryDetailSection();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyDeleteSavedSearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify Delete Saved Search Functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifySummaryByCategoryScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Monthly");
                searchPage.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                searchPage.clickDeleteButtonForSavedSearchRecordFromListAndVerifyMessage(true, false);
                searchPage.clickDeleteButtonForSavedSearchRecordFromListAndVerifyMessage(true, true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyEditSavedSearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify Edit saved Search functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifySummaryByCategoryScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Monthly");
                searchPage.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false).clickButtonOnSearchScreen("Edit Search");
                searchPage.VerifyMySearchScreen("Brand Monthly");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyAfterClickingApplySearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify After Clicking Apply search.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifySummaryByCategoryScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Monthly");
                searchPage.createNewSearchOrClickSavedSearchToApplySearchOnScreen(true);
                summaryByCategory.VerifySummaryByCategoryScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyFilterBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify Filter bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifySummaryByCategoryScreen();
                summaryByCategory.VerifyFilterBarSectionOnScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyDateRangeFilter(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify Date Range Filter.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifySummaryByCategoryScreen();
                summaryByCategory.clickOnDateFilterFieldAndSelectOption("Custom Range");
                summaryByCategory.VerifyFromAndToDateSelectionSectionOnScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyAllMediaFilter(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify All Media Filter.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifySummaryByCategoryScreen();
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("All Media");
                promoDashboard.VerifyFilterSectionOnScreen("All Media", false);
                promoDashboard.clickButtonOnFilterSectionOnScreen("Select Displayed");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(true);
                promoDashboard.clickButtonOnFilterSectionOnScreen("Slash").VerifyExcludedButtonLabelOnFilterSection();

                promoDashboard.clickButtonOnFilterSectionOnScreen("Slash Remove").clickButtonOnFilterSectionOnScreen("Selected");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(true).clickButtonOnFilterSectionOnScreen("Clear Selected");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyPivotOptionsButtonFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify Pivot options button functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifySummaryByCategoryScreen();
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("All Media");
                promoDashboard.VerifyFilterSectionOnScreen("All Media", false);
                promoDashboard.clickButtonOnFilterSectionOnScreen("Select Displayed");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(true);
                promoDashboard.clickButtonOnFilterSectionOnScreen("Slash").VerifyExcludedButtonLabelOnFilterSection();

                promoDashboard.clickButtonOnFilterSectionOnScreen("Slash Remove").clickButtonOnFilterSectionOnScreen("Selected");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(true).clickButtonOnFilterSectionOnScreen("Clear Selected");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyPivotFieldsCheckboxOptionsFromPivotOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify Pivot fields checkbox options from Pivot Options.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.clickButtonOnPivotScreen("Pivot Options");
                pivotReportScreen.VerifyPivotOptionsSectionOnScreen(true);

                pivotReportScreen.clickButtonOnPivotScreen("Pivot Options");
                pivotReportScreen.VerifyPivotOptionsSectionOnScreen(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC012");
                throw;
            }
            driver.Quit();
        }

        // Pending
        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyPivotFieldsCheckboxOptionsFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify Pivot fields checkbox options functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyUserAbleToChangeOrderOfPivotFieldsCheckboxOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify User able to change order of Pivot fields checkbox options.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.clickButtonOnPivotScreen("Pivot Options");
                pivotReportScreen.VerifyPivotOptionsSectionOnScreen(true);
                pivotReportScreen.dragAndDropFieldFromPivotFieldsSection();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_VerifyFormattingOptionFieldsInPivotOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify Formatting option fields in 'Pivot options'.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.clickButtonOnPivotScreen("Pivot Options");
                pivotReportScreen.VerifyPivotOptionsSectionOnScreen(true);
                pivotReportScreen.VerifyFormattingOptionsFieldsOnPivotOptionsSection();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC015");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_VerifyFormattingOptionInPivotOptionsFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016-Verify Formatting option in 'Pivot options' functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.clickButtonOnPivotScreen("Pivot Options");
                pivotReportScreen.VerifyPivotOptionsSectionOnScreen(true);
                pivotReportScreen.VerifyFormattingOptionsFieldsOnPivotOptionsSection();
                pivotReportScreen.selectOptionFromFormattingSection("Spend in Thousands");
                pivotReportScreen.VerifyPivotGridDataWithProperFormat(false);
                pivotReportScreen.selectOptionFromFormattingSection("Spend in Dollars");
                pivotReportScreen.VerifyPivotGridDataWithProperFormat(true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC016");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC017_VerifyRankOnFunctionalityInOtherOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017-Verify 'Rank On' functionality in Other options.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");
                //homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("Brand Canada");
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Market Spend");

                //summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.clickButtonOnPivotScreen("Pivot Options");
                pivotReportScreen.VerifyPivotOptionsSectionOnScreen(true);
                pivotReportScreen.VerifyOtherOptionsSectionOnPivotOptions();

                string[] option = { "Total Spend Current Period (CP)", "Market Spend Current Period (CP)" };
                pivotReportScreen.VerifyMetricsSectionOnPivotOptions(option);

                pivotReportScreen.selectMetricsFromSection("Total Spend Last Year (LY)");
                pivotReportScreen.checkedOUnCheckedOptionFromOtherOptionsSection("Rank on", false);
                pivotReportScreen.clickRankOnDropdownAndSelectOptionFromList("Total - Spend LY");
                string[] header = { "Rank" };
                pivotReportScreen.VerifyPivotFieldsHeaderOnPivotGrid(header, false);
                pivotReportScreen.clickRankOnDropdownAndSelectOptionFromList("Random");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC017");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_VerifyRankOnFunctionalityShouldBeWorkWithOtherMetricsOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC018-Verify 'Rank On' functionality should be work with other metrics options.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");
                //homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("Brand Canada");
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Market Spend");

                //summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.clickButtonOnPivotScreen("Pivot Options");
                pivotReportScreen.VerifyPivotOptionsSectionOnScreen(true);

                pivotReportScreen.checkedOUnCheckedOptionFromOtherOptionsSection("Rank on", false);
                pivotReportScreen.clickRankOnDropdownAndSelectOptionFromList("Total - Spend CP");
                string[] header = { "Rank" };
                pivotReportScreen.VerifyPivotFieldsHeaderOnPivotGrid(header, false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC018");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC019_VerifyRankOnDropdownOptionWhenNoMetricsAreSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC019-Verify Rank on dropdown option when no 'Metrics' are selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");
                //homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("Brand Canada");
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Market Spend");

                summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.clickButtonOnPivotScreen("Pivot Options");
                pivotReportScreen.VerifyPivotOptionsSectionOnScreen(true);
                pivotReportScreen.VerifyOtherOptionsSectionOnPivotOptions();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC019");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC020_VerifyShowSummaryTotalsFunctionalityOInOtherOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC020-Verify 'Show summary totals' functionality in Other options.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.clickButtonOnPivotScreen("Pivot Options");
                pivotReportScreen.VerifyPivotOptionsSectionOnScreen(true).VerifyOtherOptionsSectionOnPivotOptions();

                pivotReportScreen.checkedOUnCheckedOptionFromOtherOptionsSection("Show Summary Totals", false);
                pivotReportScreen.VerifyTotalSummarySectionBelowGridOnScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC020");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC021_VerifyUserAbleToFilterRecordsByFractionalValues(String Bname)
        {
            TestFixtureSetUp(Bname, "TC021-Verify User able to filter records by fractional values.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                string columnId = pivotReportScreen.clickColumnHeaderFilterIconAndVerifyFilterSection("Spend CP");
                pivotReportScreen.selectConditionFromFilterOption(columnId, "Greater than or equals");
                string searchValue = pivotReportScreen.enterOrClearValueFromFilterTextArea(columnId, false);
                pivotReportScreen.VerifyFilteredValueOnGridForColumn(columnId, searchValue, "Greater than or equals");
                pivotReportScreen.enterOrClearValueFromFilterTextArea(columnId, true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC021");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC022_VerifyFilterResultWhenUserEnterCharacterValues(String Bname)
        {
            TestFixtureSetUp(Bname, "TC022-Verify Filter result when user enter character values.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                string columnId = pivotReportScreen.clickColumnHeaderFilterIconAndVerifyFilterSection("Spend CP");
                pivotReportScreen.selectConditionFromFilterOption(columnId, "Greater than or equals");
                string searchValue = pivotReportScreen.enterOrClearValueFromFilterTextArea(columnId, false, "Qwerty");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC022");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC023_VerifyUserAbleToFilterRecordDirectlyFromPivotGrid_PivotFieldOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC023-Verify User able to filter record directly from Pivot Grid (Pivot field options).");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.selectRecordsFromPivotGrid("Category", 1);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC023");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC024_VerifyUserAbleToPerformFilterOnMultipleRowsOfSameColumn(String Bname)
        {
            TestFixtureSetUp(Bname, "TC024-Verify User able to perform filter on multiple rows of same column.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.selectRecordsFromPivotGrid("Category", 3);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC024");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC025_VerifyUserCanNotSelectSameRowAsFilterFromEachColumn(String Bname)
        {
            TestFixtureSetUp(Bname, "TC025-Verify User can not select same row as filter from each column.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.selectEachColumnRecordAndVerifyRemovedPreviousOptionFromPivotGrid();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC025");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC026_VerifyTableGridViewResultWhenSelectedRecordFromParticularColumnHasBeenDisabledFromPivotOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC026-Verify Table grid view result when selected record from particular column has been disabled from Pivot options.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media Spend");

                pivotReportScreen.clickButtonOnPivotScreen("Pivot Options").VerifyPivotOptionsSectionOnScreen(true, "Pivot Fields");
                pivotReportScreen.checkedOrUnCheckedPivotFieldsFromOptions("Class", true);
                pivotReportScreen.VerifyColumnPresentOrNotOnPivotGrid("Class", false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC026");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC027_VerifyTableGridViewResultWhenSelectedRecordFromParticularColumnHasBeenUncheckedFromPivotGridView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC027-Verify Table grid view result when selected record from particular column has been Unchecked from Pivot Grid view.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                pivotReportScreen.selectRecordsFromPivotGrid("Category", 1);
                pivotReportScreen.unSelectRecordsFromPivotGrid("Category", 1);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC027");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC028_VerifyPivotGridColumnShouldBeDisplayedAccordingToSelectedReports(String Bname)
        {
            TestFixtureSetUp(Bname, "TC028-Verify Pivot Grid column should be displayed according to Selected reports.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("Brand Canada");
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage("Print Dynamics", "Media");
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC028");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC029_VerifyUserAbleToFilterRecordBySelectingRowsFromPivotGrid_MetricsFieldOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC029-Verify User able to filter record by selecting rows from Pivot Grid (Metrics field options).");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("Brand Canada");
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage("Print Dynamics", "Media");
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                pivotReportScreen.selectRecordsFromPivotViewReportGrid("Spend CP", 2);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC029");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC030_VerifyViewSelectedButtonWhenNoRecordIsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC030-Verify View selected button when no Record is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("Brand Canada");
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage("Print Dynamics", "Media");
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                pivotReportScreen.VerifyButtonDisableOrNotOnScreen("View Selected", true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC030");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC031_VerifyViewSelectedFunctionalityWhenRecordIsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC031-Verify View selected functionality when Record is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("Brand Canada");
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage("Print Dynamics", "Media");
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails().selectRecordsFromPivotGrid("Company", 2);
                pivotReportScreen.VerifyButtonDisableOrNotOnScreen("View Selected", false);
                pivotReportScreen.clickButtonAndVerifyButtonCheckedOrNot("View Selected", true);
                pivotReportScreen.clickButtonAndVerifyButtonCheckedOrNot("View Selected", false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC031");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC032_VerifyResetSelectedButtonWhenNoRecordIsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC032-Verify Reset selected button when no Record is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("Brand Canada");
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage("Print Dynamics", "Media");
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                pivotReportScreen.VerifyButtonDisableOrNotOnScreen("Reset Selected", true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC032");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC033_VerifyResetSelectedFunctionalityWhenRecordIsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC033-Verify Reset selected functionality when Record is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("Brand Canada");
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage("Print Dynamics", "Media");
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails().selectRecordsFromPivotGrid("Company", 2);
                pivotReportScreen.VerifyButtonDisableOrNotOnScreen("Reset Selected", false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC033");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC034_VerifyPivotGridWhenHoverMouseOnAnyRecords(String Bname)
        {
            TestFixtureSetUp(Bname, "TC034-Verify Pivot Grid when hover mouse on any records.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("Brand Canada");
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage("Print Dynamics", "Media");
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                pivotReportScreen.mouseHoverOnPivotFieldGridRecordAndVerifyColor();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC034");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC035_VerifyCompanyRankingPeriodOverPeriodMultiSeriesColumnChartSection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC035-Verify 'Company Ranking Period over Period' Multi Series Column chart section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyCompanyRankingPeriodOverPeriodChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC035");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC036_VerifyDownloadFunctionalityForMultiSeriesColumnChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC036-Verify download functionality for Multi Series Column chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyCompanyRankingPeriodOverPeriodChart().clickIconButtonOnScreenForChart("Company Ranking Period over Period", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download JPEG");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("TimePeriod", "*.jpeg");

                summaryByCategory.clickIconButtonOnScreenForChart("Company Ranking Period over Period", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download PNG");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("TimePeriod", "*.png");

                summaryByCategory.clickIconButtonOnScreenForChart("Company Ranking Period over Period", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download PDF");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("TimePeriod", "*.pdf");

                summaryByCategory.clickIconButtonOnScreenForChart("Company Ranking Period over Period", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download Excel");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("company_ranking_period_over_period-chart", "*.xlsx");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC036");
                throw;
            }
            driver.Quit();
        }

        // Pending
        //[Test]
        //[TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC037_VerifyScheduledExportFunctionalityForMultiSeriesColumnChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC037-Verify scheduled Export functionality for Multi Series Column chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyCompanyRankingPeriodOverPeriodChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC037");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC038_VerifyChartOnDisabilityOfLegendsForMultiSeriesColumnChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC038-Verify chart on disability of Legends for Multi Series Column chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyCompanyRankingPeriodOverPeriodChart();
                summaryByCategory.VerifyLegendToClickAndVerifyLegendColor("Company Ranking Period over Period");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC038");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC039_VerifyCompanyShareOfSpendPiChartSection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC039-Verify 'Company Share of Spend' Pi Chart section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyCompanyShareOfSpendChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC039");
                throw;
            }
            driver.Quit();
        }

        // Pending
        //[Test]
        //[TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC040_VerifyDrillDownFunctionalityForPiChartSection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC040-Verify Drill down functionality for Pi Chart Section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyCompanyShareOfSpendChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC040");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC041_VerifyDownloadFunctionalityForPiChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC041-Verify download functionality for Pi Chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyCompanyShareOfSpendChart().clickIconButtonOnScreenForChart("Company Share of Spend", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download JPEG");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("customCompSOV", "*.jpeg");

                summaryByCategory.clickIconButtonOnScreenForChart("Company Share of Spend", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download PNG");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("customCompSOV", "*.png");

                summaryByCategory.clickIconButtonOnScreenForChart("Company Share of Spend", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download PDF");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("customCompSOV", "*.pdf");

                summaryByCategory.clickIconButtonOnScreenForChart("Company Share of Spend", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download Excel");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("company_share_of_spend-chart", "*.xlsx");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC041");
                throw;
            }
            driver.Quit();
        }

        // Pending
        //[Test]
        //[TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC042_VerifyScheduledExportFunctionalityForPiChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC042-Verify scheduled Export functionality For Pi chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyCompanyShareOfSpendChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC042");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC043_VerifyChartOnDisabilityOfLegendsForPiChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC043-Verify chart on disability of Legends for Pi chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyCompanyRankingPeriodOverPeriodChart();
                summaryByCategory.VerifyLegendToClickAndVerifyLegendColor("Company Share of Spend");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC043");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC044_VerifyLeadingCompanyShareOfMediaStackColumnChartSection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC044-Verify 'Leading Company Share of Media' stack column Chart section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyLeadingCompanyShareOfMediaChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC043");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC045_VerifyDownloadFunctionalityForStackColumnChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC045-Verify download functionality for stack column Chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyLeadingCompanyShareOfMediaChart();
                summaryByCategory.clickIconButtonOnScreenForChart("Leading Company Share of Media", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download JPEG");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("Submedia", "*.jpeg");

                summaryByCategory.clickIconButtonOnScreenForChart("Leading Company Share of Media", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download PNG");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("Submedia", "*.png");

                summaryByCategory.clickIconButtonOnScreenForChart("Leading Company Share of Media", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download PDF");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("Submedia", "*.pdf");

                summaryByCategory.clickIconButtonOnScreenForChart("Leading Company Share of Media", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download Excel");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("leading_company_share_of_media-chart", "*.xlsx");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC045");
                throw;
            }
            driver.Quit();
        }

        // Pending
        //[Test]
        //[TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC046_VerifyScheduledExportFunctionalityForMultiSeriesColumnChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC046-Verify scheduled Export functionality for Multi Series Column chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyLeadingCompanyShareOfMediaChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC046");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC047_VerifyChartOnDisabilityOfLegendsForStackColumnChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC047-Verify chart on disability of Legends for stack column Chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyLeadingCompanyShareOfMediaChart();
                summaryByCategory.VerifyLegendToClickAndVerifyLegendColor("Leading Company Share of Media");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC047");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC048_VerifyCompanyBySpendSingleSeriesBarChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC048-Verify 'Company by Spend' Single Series Bar chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails();
                summaryByCategory.VerifyCompanyBySpendChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC048");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC049_VerifyDownloadFunctionalityForSingleSeriesBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC049-Verify download functionality for Single Series Bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails().VerifyCompanyBySpendChart();
                summaryByCategory.clickIconButtonOnScreenForChart("Company by Spend", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download JPEG");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("Unknown", "*.jpeg");

                summaryByCategory.clickIconButtonOnScreenForChart("Company by Spend", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download PNG");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("Unknown", "*.png");

                summaryByCategory.clickIconButtonOnScreenForChart("Company by Spend", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download PDF");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("Unknown", "*.pdf");

                summaryByCategory.clickIconButtonOnScreenForChart("Company by Spend", "Download");
                summaryByCategory.VerifyDownloadPopupWindowAndClickOnOption("Download Excel");
                summaryByCategory.VerifyFileDownloadedOrNotOnScreen("company_by_spend-chart", "*.xlsx");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC049");
                throw;
            }
            driver.Quit();
        }

        // Pending
        //[Test]
        //[TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC050_VerifyScheduledExportFunctionalityForSingleSeriesBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC050-Verify scheduled Export functionality For Single Series Bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails().VerifyCompanyBySpendChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC050");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC051_VerifyChartOnDisabilityOfLegendsForSingleSeriesBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC051-Verify chart on disability of Legends for Single Series Bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Annual Summary by Category");

                summaryByCategory.VerifyReportScreenDetails().VerifyCompanyBySpendChart();
                summaryByCategory.VerifyLegendToClickAndVerifyLegendColor("Company by Spend");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite012_BM_AnnualSummaryByCategory_TC051");
                throw;
            }
            driver.Quit();
        }

        #endregion
    }
}