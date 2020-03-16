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
    public class TestSuite04_Reskin_Export : Base
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
        Charts charts;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite04_Reskin_Export).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite04_Reskin_Export).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            userProfile = new UserProfile(driver, test);
            carousels = new Carousels(driver, test);
            brandDashboard = new BrandDashboard(driver, test);
            charts = new Charts(driver, test);

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
        public void TC001_VerifyHomeScreenAfterLoginIntoApplication(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify Home screen after login into Application.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePageInDetail();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyExportButton_WhenNoItemsAreSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify Export button (When no items are selected).");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.clickOnExportOptions("", 0, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifyThatClickingExportButtonShouldOpenThe_SelectAnExportType_SmallModalWithExportOptions_CancelAndSendButtons(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify Clicking Export button should open the “Select an Export Type ...“ small modal with Export options, Cancel and Send buttons.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                carousels.clickOnExportOptions("", 0, true);
                carousels.VerifySelectAnExportTypePopup();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC003");
                throw;
            }
            driver.Quit();
        }

        //Only one type of file export is verified in the TC, since the rest file type were covered under TestSuite0003_Carousel
        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyThatUserIsAbleToExportItemsInASelectedFormat(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify that User is able to Export items in a selected format");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string[] columnNames = fieldOptions.captureColumnNamesOfAgGridInOrder();
                string[,] dataGrid = carousels.convertColumnNameArrayIntoDatagrid(columnNames, null);
                carousels.clickOnExportOptions("", 0);
                carousels.VerifySelectAnExportTypePopup();
                carousels.selectOptionAndClickButtonOnSelectAnExportTypePopup("Send", "Occurrence Report - XLS");
                string screenName = homePage.getActiveScreenNameFromSideNavigationBar();
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen("occurrence", "*.xlsx");
                carousels.VerifyDataInExportedFileFromCarousel(fileName, screenName, dataGrid);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyExportButtonWhenRecordsAreSelectedByUser(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify Export button when records are selected by user");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string adCode = carousels.selectRecordFromResults("Table");
                carousels.clickOnExportOptions("", 1);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyInExportButtonCounterShouldBeIncreasesDecreasesByOneEachTimeWhenUserSelect_DeselectAnyRecordFromTheAgGrid_Carousel(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify in 'Export' button counter should be increases/decreases by 1 each time when user select/Deselect any record from the AgGrid/Carousel");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string adCode1 = carousels.selectRecordFromResults("Table");
                carousels.clickOnExportOptions("", 1);
                string adCode2 = carousels.selectRecordFromResults("Table");
                carousels.clickOnExportOptions("", 2);
                string adCode3 = carousels.selectRecordFromResults("Table");
                carousels.clickOnExportOptions("", 3);
                carousels.selectRecordFromResults("Table", false, adCode1);
                carousels.clickOnExportOptions("", 2);
                carousels.selectRecordFromResults("Table", false, adCode2);
                carousels.clickOnExportOptions("", 1);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_9_VerifyInAsACounterOfSelectedItems_ClickingTheExportButtonInThisStateShouldExpandTheSecondaryExportActions_ExportSelectedAndResetSelected_AndTheCancelButton(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007_9-Verify in As a counter of selected items - Clicking the export button in this state should expand the secondary export actions (Export Selected and Reset Selected) and the Cancel button");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifySelectAdFunctionality(false);
                carousels.clickOnExportOptions("", 1);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC007_9");
                throw;
            }
            driver.Quit();
        }

        //This TC is pending due to a bug in Cancel Button functionality.
        //Cancel button does not appear in place of export button when export button is expanded, while exporting selected data.

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyCancelButtonFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify Cancel button Functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifySelectAdFunctionality(false);
                carousels.clickOnExportOptions("", 1, false, true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_11_VerifyResetButtonFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010_11-Verify Reset button Functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                homePage.VerifyAndModifySearchOptions(false);
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Print");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                carousels.VerifyCarousels();
                string adCode1 = carousels.getAdCodeFromCarousel();
                carousels.VerifySelectAdFunctionality(false);
                string adCode2 = carousels.getAdCodeFromCarousel();
                carousels.VerifySelectAdFunctionality(false);
                string adCode3 = carousels.getAdCodeFromCarousel();
                carousels.VerifySelectAdFunctionality(false);
                carousels.clickOnExportOptions("Reset", 3);
                carousels.VerifyCheckboxInAgGrid(adCode1, "Carousel", false);
                carousels.VerifyCheckboxInAgGrid(adCode2, "Carousel", false);
                carousels.VerifyCheckboxInAgGrid(adCode3, "Carousel", false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC010_11");
                throw;
            }
            driver.Quit();
        }

        //This TC is pending due to a bug in Cancel Button functionality.
        //Cancel button does not appear in place of export button when export button is expanded, while exporting selected data.

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyTheTextForTheTooltipOf_CloseExport_buttonIsCancel(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify The text for the tooltip of 'Close Export' button  is “Cancel“");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifySelectAdFunctionality(false);
                carousels.clickOnExportOptions("", 1, false, true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC012");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyOnClickingTheCancelButtonInTheSmallModalWithExportOptionsShouldCloseTheWindow(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify on clicking the 'Cancel' button in the small modal with Export options should close the window");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.clickOnExportOptions("", 0);
                carousels.VerifySelectAnExportTypePopup();
                carousels.selectOptionAndClickButtonOnSelectAnExportTypePopup("Cancel");
                carousels.VerifySelectAnExportTypePopup(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyOnClickingTheCancelButtonInTheSmallModalWithExportOptionsShouldCloseTheWindow(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify on clicking the 'Cancel' button in the small modal with Export options should close the window");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                charts.VerifyCharts();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_16_22_VerifyThatUserIsAbleToExportChartInExcelFormat(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015_16_22-Verify User able to Export chart in Excel format");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string chartTitle = charts.VerifyCharts();
                string[,] dataGrid = charts.captureDataFromChart(chartTitle);
                charts.VerifyExportChartFunctionality("Excel");
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen(chartTitle, "*.xlsx");
                charts.VerifyDataFromChartInExportedFile(fileName, "Dashboard", chartTitle.Replace('_', ' '), dataGrid);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC015_16_22");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC017_VerifyThatUserIsAbleToExportChartInJPEGFormat(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017-Verify User able to Export chart in JPEG format");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                charts.VerifyCharts();
                charts.VerifyExportChartFunctionality("JPEG");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("MediaType", "*.jpeg");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC017");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_VerifyThatUserIsAbleToExportChartInPNGFormat(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015_16-Verify User able to Export chart in PNG format");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                charts.VerifyCharts();
                charts.VerifyExportChartFunctionality("PNG");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("MediaType", "*.png");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC018");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC019_VerifyThatUpdatedChartShouldBeExportedAfterUserUpdatesAnyLegends(String Bname)
        {
            TestFixtureSetUp(Bname, "TC019-Verify Updated chart should be Exported after user updates any Legends");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                charts.VerifyCharts();
                charts.VerifyExportChartFunctionality("PDF");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("Creatives", "*.pdf");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC019");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC020_VerifyThatUpdatedChartIsExportedAfterUserUpdatesAnyLegends(String Bname)
        {
            TestFixtureSetUp(Bname, "TC020-Verify Updated chart should be Exported after user updates any Legends");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                string chartTitle = charts.VerifyCharts();
                string[] newLegendSelection = charts.updateAndVerifyLegendsInCharts();
                charts.VerifyExportChartFunctionality("Excel");
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen(chartTitle, "*.xlsx");
                charts.VerifyExportChartFunctionality("JPEG");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("Creatives", "*.jpeg");
                charts.VerifyExportChartFunctionality("PNG");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("Creatives", "*.png");
                charts.VerifyExportChartFunctionality("PDF");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("Creatives", "*.pdf");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC020");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC021_VerifyThatUpdatedChartIsExportedAfterUserDrillDownInAnyPieChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC021-Verify Updated chart should be Exported after user drill down in any pie chart");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                string chartTitle = charts.VerifyCharts();
                string[] chartInfo = charts.updateAndVerifypieChart();
                charts.VerifyExportChartFunctionality("Excel");
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen(chartTitle, "*.xlsx");
                charts.VerifyExportChartFunctionality("JPEG");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("Creatives", "*.jpeg");
                charts.VerifyExportChartFunctionality("PNG");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("Creatives", "*.png");
                charts.VerifyExportChartFunctionality("PDF");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("Creatives", "*.pdf");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC021");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC023_VerifyThatUserIsAbleToExportPivotData(String Bname)
        {
            TestFixtureSetUp(Bname, "TC023-Verify User able to Export Pivot data");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                charts.VerifyPivotTable();
                string[,] dataGrid = charts.captureDataFromPivotTable();
                charts.choosePivotBulkActionsForDataFromPivotTable("Download Grid");
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen("brand_canada_-_", "*.xlsx");
                charts.VerifyDataFromPivotTableInExportedExcelFile(fileName, dataGrid);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite04_Reskin_Export_TC023");
                throw;
            }
            driver.Quit();
        }

        #endregion
    }
}
