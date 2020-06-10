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
    public class TestSuite18_Reskin_ScheduledAlertsManager : Base
    {
        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        Search searchPage;
        ScheduledAlertManager scheduledAlertManager;
        Charts charts;
        FieldOptions fieldOptions;
        SecondaryButtons secondaryButtons;
        SummaryTags summaryTags;
        AgGrid agGrid;
        Carousels carousels;
        Schedule schedule;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite18_Reskin_ScheduledAlertsManager).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite18_Reskin_ScheduledAlertsManager).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            secondaryButtons = new SecondaryButtons(driver, test);
            scheduledAlertManager = new ScheduledAlertManager(driver, test);
            charts = new Charts(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            summaryTags = new SummaryTags(driver, test);
            agGrid = new AgGrid(driver, test);
            carousels = new Carousels(driver, test);
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
        public void TC001_2_3_8_VerifyTheAlertManagerOptionOnTheNavigationBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001_2_3_8-Verify the Alert Manager option on the Navigation bar");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.selectOptionFromSideNavigationBar("Alerts Manager");
                scheduledAlertManager.VerifyScheduledAlertsManagerScreen();
                scheduledAlertManager.VerifyReportNamesFromActiveReportsDropdown();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite18_Reskin_ScheduledAlertsManager_TC001_2_3_8");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_5_7_VerifyTheActiveReportDropDownOfAlertManagerShouldBeUpdatedAccordingToUserAccountChanged(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004_5_7-Verify the Active Report Drop Down of alert manager should be updated according to user account changed");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.selectOptionFromSideNavigationBar("Alerts Manager");
                scheduledAlertManager.VerifyScheduledAlertsManagerScreen();
                scheduledAlertManager.VerifyReportNamesFromActiveReportsDropdown();

                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectOptionFromSideNavigationBar("Alerts Manager");
                scheduledAlertManager.VerifyScheduledAlertsManagerScreen();
                scheduledAlertManager.VerifyReportNamesFromActiveReportsDropdown();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite18_Reskin_ScheduledAlertsManager_TC004_5_7");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyScheduledAlertsManagerListShouldGetUpdatedAccordingToUserChangeTheReport(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify 'Scheduled Alerts Manager' list should get updated according to user change the Report");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.selectOptionFromSideNavigationBar("Alerts Manager");
                scheduledAlertManager.VerifyScheduledAlertsManagerScreen();
                string[] savedSearches1 = scheduledAlertManager.CaptureSavedSearchesNameFromScheduledAlertManagerScreen();

                scheduledAlertManager.SelectActiveReportFromDropdown();
                string[] savedSearches2 = scheduledAlertManager.CaptureSavedSearchesNameFromScheduledAlertManagerScreen();

                fieldOptions.compareListOfItemsInOrder(savedSearches1, savedSearches2, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite18_Reskin_ScheduledAlertsManager_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyUserAbleToDeleteScheduleAlertSuccessfully(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify User able to Delete Schedule alert successfully");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.selectOptionFromSideNavigationBar("Alerts Manager");
                scheduledAlertManager.VerifyScheduledAlertsManagerScreen();
                scheduledAlertManager.VerifyActionsIconAndClickOption("Delete");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite18_Reskin_ScheduledAlertsManager_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyTheCancelButtonFunctionalityOfScheduleExportWindow(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify the Cancel Button Functionality of Schedule Export Window");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.selectOptionFromSideNavigationBar("Alerts Manager");
                scheduledAlertManager.VerifyScheduledAlertsManagerScreen();
                scheduledAlertManager.VerifyActionsIconAndClickOption("Modify");
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "", "weekly", true);
                schedule.clickButtonOnSchedulePopup("Cancel");
                schedule.VerifySchedulePopupOfSavedSearch("", false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite18_Reskin_ScheduledAlertsManager_TC010");
                throw;
            }
            driver.Quit();
        }
        //TC12 is failing as modified Schedule is not on top
        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_12_VerifyUserAbleToModify_UpdateAlertsSuccessfully(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011_12-Verify User able to modify/update alerts successfully");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.selectOptionFromSideNavigationBar("Alerts Manager");
                scheduledAlertManager.VerifyScheduledAlertsManagerScreen();
                string searchName = scheduledAlertManager.VerifyActionsIconAndClickOption("Modify");
                schedule.VerifyAndEditFrequencyViewOfSchedulePopup("", "10", "weekly", true);
                schedule.clickButtonOnSchedulePopup("Update");
                Thread.Sleep(2000);
                driver._scrollintoViewElement("xpath", "//tr[1]/td[1]");
                Assert.AreEqual(searchName, driver._getText("xpath", "//tr[1]/td[1]"), "Modified Schedule is not on top of the table.");
                Assert.AreEqual("monthly", driver._getText("xpath", "//tr[1]/td[2]"), "Schedule not modified to 'monthly'");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite18_Reskin_ScheduledAlertsManager_TC011_12");
                throw;
            }
            driver.Quit();
        }

        //TC013 functionality not confirmed
        //TC014 and TC015 are Email Reception related
        #endregion
    }
}
