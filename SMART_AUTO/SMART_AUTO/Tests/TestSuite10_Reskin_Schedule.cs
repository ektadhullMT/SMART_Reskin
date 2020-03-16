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
    public class TestSuite10_Reskin_Schedule : Base
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

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite10_Reskin_Schedule).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite10_Reskin_Schedule).Name);

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
        public void TC001_VerifyScheduleIconWhenNoSearchIsApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify Schedule icon when no search is applied");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                schedule.VerifyScheduleIcon();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyScheduleIconWhenSearchIsApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify Schedule icon when search is applied");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePageInDetail();
                charts.VerifyCharts();
                schedule.VerifyScheduleIcon();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifyThatUserIsAbleToScheduleDailyScheduleSuccessfully(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify, User should be able to schedule Daily schedule successfully");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch();
                schedule.VerifySchedulePopupOfSavedSearch(searchName);
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup();
                schedule.VerifyAndEditFileTypeViewOfSchedulePopup();
                schedule.clickButtonOnSchedulePopup("Confirm");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[text()=' Successfully created scheduled export! ']"), "'Success Text' not present.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC003");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyThatUserIsAbleToScheduleWeeklyScheduleSuccessfully(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify, User should be able to schedule Daily Weekly successfully");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch();
                schedule.VerifySchedulePopupOfSavedSearch(searchName);
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("today");
                schedule.VerifyAndEditFileTypeViewOfSchedulePopup();
                schedule.clickButtonOnSchedulePopup("Confirm");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[text()=' Successfully created scheduled export! ']"), "'Success Text' not present.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_6_VerifyThatUserIsAbleToScheduleMonthlyScheduleSuccessfully(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005_6-Verify, User should be able to schedule Monthly schedule successfully");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch();
                schedule.VerifySchedulePopupOfSavedSearch(searchName);
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "today");
                schedule.VerifyAndEditFileTypeViewOfSchedulePopup();
                schedule.clickButtonOnSchedulePopup("Confirm");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[text()=' Successfully created scheduled export! ']"), "'Success Text' not present.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC005_6");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyThatUserIsAbleToScheduleInEveryExportFormatOption(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify, User should be able to Schedule in every Export Format Option");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch();
                schedule.VerifySchedulePopupOfSavedSearch(searchName);
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup();
                schedule.VerifyAndEditFileTypeViewOfSchedulePopup("Occurrence Report - XLS");
                schedule.clickButtonOnSchedulePopup("Confirm");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[text()=' Successfully created scheduled export! ']"), "'Success Text' not present.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyThatCancelButtonFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify, Cancel button functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch();
                schedule.VerifySchedulePopupOfSavedSearch(searchName);
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup();
                schedule.VerifyAndEditFileTypeViewOfSchedulePopup();
                schedule.clickButtonOnSchedulePopup("Cancel");
                schedule.VerifySchedulePopupOfSavedSearch(searchName, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyScheduleButtonWhenASavedSearchIsAppliedButSearchModelIsModified(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify, Schedule button when a saved search is applied but search model is modified");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePageInDetail();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand");
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.VerifyNewDateRangeSectionOnScreen();
                searchPage.selectNewDateRangeOptionFromSection();
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                charts.VerifyCharts();
                schedule.VerifyScheduleIcon(true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyScheduleShouldShowOptionsAsPerTheReportType(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify schedule should show options as per the Report type");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch();
                schedule.VerifySchedulePopupOfSavedSearch(searchName);
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup();
                schedule.VerifyAndEditFileTypeViewOfSchedulePopup();
                schedule.clickButtonOnSchedulePopup("Cancel");
                schedule.VerifySchedulePopupOfSavedSearch(searchName, false);
                homePage.selectOptionFromSideNavigationBar("QA Testing - Brand - Weekly Report");
                homePage.newVerifyHomePage();
                searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                schedule.VerifySchedulePopupOfSavedSearch(searchName);
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "", "weekly");
                schedule.VerifyAndEditFileTypeViewOfSchedulePopup();
                schedule.clickButtonOnSchedulePopup("Cancel");
                schedule.VerifySchedulePopupOfSavedSearch(searchName, false);
                homePage.selectOptionFromSideNavigationBar("QA Testing - Brand - Monthly Report");
                homePage.newVerifyHomePage();
                searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                schedule.VerifySchedulePopupOfSavedSearch(searchName);
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "", "monthly");
                schedule.VerifyAndEditFileTypeViewOfSchedulePopup();
                schedule.clickButtonOnSchedulePopup("Cancel");
                schedule.VerifySchedulePopupOfSavedSearch(searchName, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyThatScheduleOptionIsNotAvailableQuaterlyAndYearlyReports(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify Quaterly and Yearly reports schedule are not available");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Trend - Quarterly");
                homePage.newVerifyHomePageInDetail();
                schedule.VerifyScheduleIcon(true);
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Monthly");
                homePage.selectOptionFromSideNavigationBar("5. Ad Ex Trend - Yearly");
                homePage.newVerifyHomePage();
                homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand Monthly");
                schedule.VerifyScheduleIcon(true);
                homePage.selectOptionFromSideNavigationBar("Annual Summary");
                homePage.newVerifyHomePage();
                homePage.selectSavedSearchOrCreateNewSavedSearch(true,"QA Testing - Brand Monthly");
                schedule.VerifyScheduleIcon(true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyScheduleIconFromPivotGridWhenSearchIsApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify, Schedule icon from Pivot when search is applied");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Category");
                homePage.newVerifyHomePageInDetail("QA Testing - Brand Canada");
                charts.choosePivotBulkActionsForDataFromPivotTable("Create an Alert");
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "", "Weekly", true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC012");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyScheduleIconFromPivotWhenNoSearchIsApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify, Schedule icon from Pivot when no search is applied");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Category");
                homePage.newVerifyHomePage();
                charts.choosePivotBulkActionsForDataFromPivotTable("");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyThatUserIsAbleToCreatePivotScheduleWeeklySuccessfully(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify, User able to create Pivot Schedule Weekly successfully ");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Category");
                homePage.newVerifyHomePageInDetail();
                charts.choosePivotBulkActionsForDataFromPivotTable("Create an Alert");
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("today", "", "Weekly", true);
                schedule.clickButtonOnSchedulePopup("Confirm");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[text()=' Successfully created scheduled export! ']"), "'Success Text' not present.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_VerifyThatUserIsAbleToCreatePivotScheduleMonthlySuccessfully(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify, User able to create Pivot Schedule Monthly successfully ");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Category");
                homePage.newVerifyHomePageInDetail();
                charts.choosePivotBulkActionsForDataFromPivotTable("Create an Alert");
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "today", "Weekly", true);
                schedule.clickButtonOnSchedulePopup("Confirm");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[text()=' Successfully created scheduled export! ']"), "'Success Text' not present.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC015");
                throw;
            }
            driver.Quit();
        }

        //TC016 is pending due to data issue in Brand Canada Account 

        //TC017 is Repetative as steps are already covered in TCTC001  

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_VerifyScheduleIconWhenSearchIsApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC018-Verify Schedule icon when search is applied");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePageInDetail();
                charts.VerifyCharts();
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "", "Weekly", true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC018");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC019_VerifyThatUserIsAbleToCreateChartScheduleInDifferentCadenceOptionSuccessfully(String Bname)
        {
            TestFixtureSetUp(Bname, "TC019-Verify, User able to create Chart Schedule in different cadence Option successfully ");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePageInDetail();
                charts.VerifyCharts();
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "", "Weekly", true);
                schedule.clickButtonOnSchedulePopup("Confirm");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[text()=' Successfully created scheduled export! ']"), "'Success Text' not present.");
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("today", "", "Weekly", true);
                schedule.clickButtonOnSchedulePopup("Confirm");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[text()=' Successfully created scheduled export! ']"), "'Success Text' not present.");
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "today", "Weekly", true);
                schedule.clickButtonOnSchedulePopup("Confirm");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[text()=' Successfully created scheduled export! ']"), "'Success Text' not present.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC019");
                throw;
            }
            driver.Quit();
        }

        //TC20 and TC21 are skipped due to issue in Email Functionality of Scheduled Exports

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC022_VerifyThatMyExportsPageShowsRecordsOfSchedules(String Bname)
        {
            TestFixtureSetUp(Bname, "TC022-Verify My Exports page should show records of schedules");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePageInDetail();
                charts.VerifyCharts();
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "", "Weekly", true);
                schedule.clickButtonOnSchedulePopup("Confirm");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[text()=' Successfully created scheduled export! ']"), "'Success Text' not present.");

                homePage.selectOptionFromSideNavigationBar("My Exports");
                myExportsPage.VerifyMyExportsPage();
                myExportsPage.FindSpecificValueInAColumn("Type", "scheduled");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite10_Reskin_Schedule_TC022");
                throw;
            }
            driver.Quit();
        }


        #endregion
    }
}
