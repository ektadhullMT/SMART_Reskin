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
    public class TestSuite08_Reskin_MyExportsPage : Base
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
        MyExportsPage myExportsPage;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite08_Reskin_MyExportsPage).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite08_Reskin_MyExportsPage).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            userProfile = new UserProfile(driver, test);
            carousels = new Carousels(driver, test);
            brandDashboard = new BrandDashboard(driver, test);
            charts = new Charts(driver, test);
            myExportsPage = new MyExportsPage(driver, test);

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
        public void TC001_2_3_4_VerifyMyExportsPage(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001_2_3_4-Verify My Exports page.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePageInDetail();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC001_2_3_4");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyThatIfTheDownloadOfTheReportIsEnabled_WhenTheExportStatusIsSuccess_TextShouldBeInBOLDAndClickingReportShouldDownloadTheAssets(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify if the download of the report is Enabled(When the Export is Success) Text should be in BOLD and Clicking Report should download the assets");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                string[] fileNameAndType = myExportsPage.VerifyThatClickingOnExportNameDownloadsAsset("", false, "", "excel");
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen("", "*." + fileNameAndType[1]);
                myExportsPage.deleteDownloadedFile(fileName);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_12_VerifyWhenTheExportIsUnsuccessful(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006_12-Verify When the Export is Unsuccessful");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                myExportsPage.findAndVerifyUnsuccessfulDownloads();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC006_12");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyTheRecentlyDownloadedExportShouldDisplayedFirst_TopOfTheReport(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify the recently downloaded export should displayed first/top of the report");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                myExportsPage.VerifyThatMostRecentDownloadsAppearOnTopInMyExportsTable();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_9_VerifyThatTheReportsExportedByTheUserShouldDisplayAsType_Adhoc_AndScheduledExportsAs_Scheduled(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008_9-Verify the Reports Exported by the User should display as type 'Adhoc' and Scheduled Exports as 'Scheduled'");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                myExportsPage.FindSpecificValueInAColumn("Type", "adhoc");
                myExportsPage.FindSpecificValueInAColumn("Type", "scheduled");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC008_9");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyInMyExportsPageThatFormatColumnShouldDisplayTheFormatOfTheFileExcel_PowerpointAndAsset(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify in 'My Exports' page, Format column  should display the format of the file 'excel', 'powerpoint', 'asset'");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                myExportsPage.FindSpecificValueInAColumn("Format", "excel");
                myExportsPage.FindSpecificValueInAColumn("Format", "powerpoint");
                myExportsPage.FindSpecificValueInAColumn("Format", "zip");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyInMyExportsPageThatCreatedColumnShouldDisplayTheTimeSinceTheExportWasCreated(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify in 'My Exports' page, Created column should display the time since the export was created");
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
                myExportsPage.deleteDownloadedFile(fileName);

                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                myExportsPage.FindSpecificValueInAColumn("Created", "second");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyThatExpiredReport_AssetHaveCautionIconDisplayedInCreatedColumn(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify if the Report/asset is expired (no longer downloadable), its 'created' column should display text stating - 'Expired' with caution icon");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                myExportsPage.findAndVerifyUnsuccessfulDownloads("Created");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyThatTheReportShowsStatus_InProgress_WhileItIsRunning(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify that the report shows status 'InProgress' while it is running");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                charts.VerifyPivotTable();
                string[,] dataGrid = charts.captureDataFromPivotTable();
                charts.choosePivotBulkActionsForDataFromPivotTable("Download Grid", false);

                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                myExportsPage.FindSpecificValueInAColumn("Status", "inprogress");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_VerifyThatIfTheDownloadOfTheReportIsEnabled_WhenTheExportStatusIsSuccess_ThenClickingDownloadFromActionsDDLShouldDownloadTheAssets(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify if the download of the report is Enabled(When the Export is Success) Clicking Download From Actions DDL should download the assets");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                string[] fileNameAndType = myExportsPage.VerifyThatClickingOnExportNameDownloadsAsset("", true, "", "powerpoint");
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen("", "*." + fileNameAndType[1]);
                myExportsPage.deleteDownloadedFile(fileName);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC015");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_VerifyReportFormatAndDataIsSameWhenDownloadedFromEllipsesAndReportHyperlink(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016-Verify report format and data is same when downloaded from ellipses and report hyperlink");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                string[] fileNameAndType = myExportsPage.VerifyThatClickingOnExportNameDownloadsAsset("", true, "", "excel");
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen("", "*." + fileNameAndType[1]);
                string[,] dataGrid = myExportsPage.captureDataFromExcelSheet(fileName);
                myExportsPage.deleteDownloadedFile(fileName);

                fileNameAndType = myExportsPage.VerifyThatClickingOnExportNameDownloadsAsset(fileNameAndType[0], false, fileNameAndType[2]);
                fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen("", "*." + fileNameAndType[1]);
                myExportsPage.compareDataFromExcelSheet(fileName, dataGrid);
                myExportsPage.deleteDownloadedFile(fileName);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC016");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC017_VerifyTheDownloadWithFileFormatPNG_JPEG_PDFEtc_OtherThanExcel_PowerPoint_Asset_ShouldNotBeDisplayedInMyExportsPage(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017-Verify the download with file format PNG, JPEG, PDF etc. (other than excel, PowerPoint, asset) should not be displayed in 'My Exports' page");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string chartTitle = charts.VerifyCharts();
                charts.VerifyExportChartFunctionality("JPEG");
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen("MediaType", "*.jpeg");
                myExportsPage.deleteDownloadedFile(fileName);

                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                myExportsPage.FindSpecificValueInAColumn("Created", "second", false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC017");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_VerifyThatMyExportsOfOneUserShouldNotReflectInThatOfOtherUserAccount(String Bname)
        {
            TestFixtureSetUp(Bname, "TC018-Verify User's My Exports should not be reflected in other User's account");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                string[] exportsList1 = myExportsPage.captureExportNamesListFromMyExportsPage();

                homePage.selectOptionFromSideNavigationBar("Settings");
                loginPage.signOutOfApplication();

                loginPage.navigateToLoginPage().clickButtonOnNewLoginPage("different email").VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                string[] exportsList2 = myExportsPage.captureExportNamesListFromMyExportsPage();

                fieldOptions.compareListOfItemsInOrder(exportsList1, exportsList2, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC018");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC021_VerifyInMyExportsPageThatCreatedColumnShouldDisplayTheTimeSinceTheExportWasCreated(String Bname)
        {
            TestFixtureSetUp("ie", "TC021-Verify in 'My Exports' page, Created column should display the time since the export was created");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                myExportsPage.FindSpecificValueInAColumn("Created", "minute");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC021");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC023_VerifyLoadMoreFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC023-Verify Load more functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                myExportsPage.VerifyLoadMoreButtonFunctionality();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite08_Reskin_MyExportsPage_TC023");
                throw;
            }
            driver.Quit();
        }



        #endregion
    }
}
