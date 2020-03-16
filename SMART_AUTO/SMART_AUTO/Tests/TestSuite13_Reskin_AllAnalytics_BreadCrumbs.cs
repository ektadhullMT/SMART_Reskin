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
    public class TestSuite13_Reskin_AllAnalytics_BreadCrumbs : Base
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
        AllAnalytics_BreadCrumbs allAnalyticsBreadCrumbs;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite13_Reskin_AllAnalytics_BreadCrumbs).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite13_Reskin_AllAnalytics_BreadCrumbs).Name);

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
            allAnalyticsBreadCrumbs = new AllAnalytics_BreadCrumbs(driver, test); ;

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
        public void TC001_VerifyDefaultBreadcrumb(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify Default Bread crumb.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePageInDetail();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite13_Reskin_AllAnalytics_BreadCrumbs_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_5_VerifyReportListBreadcrumb(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002_5-Verify Report list Bread Crumb.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePageInDetail("QA Testing - Brand Canada");
                allAnalyticsBreadCrumbs.VerifyReportsListBreadcrumbAndSelectReport(false, "", "QA Testing - Brand Canada");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite13_Reskin_AllAnalytics_BreadCrumbs_TC002_5");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifyBrandedContentLinkShouldOnlyBeDisplayedInBrandUSAccount(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify Brand content link should be only displayed in Brand US account only.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                allAnalyticsBreadCrumbs.VerifyReportsListBreadcrumbAndSelectReport(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite13_Reskin_AllAnalytics_BreadCrumbs_TC003");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyThatUserIsAbleToChangeUserAccountFromBreadcrumbList(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify User able to change user account from Bread crumb list.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePageInDetail("QA Testing - Brand Canada");
                allAnalyticsBreadCrumbs.VerifyReportsListBreadcrumbAndSelectReport(false, "Settings", "QA Testing - Brand Canada");
                userProfile.selectAccountNameOnUserScreen("QA Testing - Brand");
                homePage.newVerifyHomePage();
                Results.WriteStatus(test, "Pass", "Switched, Current logged in account is 'QA Testing - Brand'");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite13_Reskin_AllAnalytics_BreadCrumbs_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyAllAnalyticsBreadcrumbFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify All Analytics bread crumb functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                allAnalyticsBreadCrumbs.VerifyReportsListBreadcrumbAndSelectReport(true, "Random", "QA Testing - Brand Canada");
                allAnalyticsBreadCrumbs.VerifyChartSelectedFromAllAnalyticsBreadcrumb();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite13_Reskin_AllAnalytics_BreadCrumbs_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyThatUserIsAbleToChangeUserAccountFromBreadcrumbList(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify User able to change user account from Bread crumb list.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePageInDetail("QA Testing - Brand Canada");
                allAnalyticsBreadCrumbs.VerifyReportsListBreadcrumbAndSelectReport(true, "All Analytics", "QA Testing - Brand Canada");
                driver._click("xpath", "//cft-sidebar-navigation//button[contains(@class, 'sidebar-toggle')]");
                Assert.AreEqual("Print Dynamics Dashboard (Ad)", homePage.getActiveScreenNameFromSideNavigationBar(), "Browser not redirected to 'Print Dynamics Dashboard (Ad)' Page");
                Results.WriteStatus(test, "Pass", "Switched, Current logged in account is 'QA Testing - Brand'");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite13_Reskin_AllAnalytics_BreadCrumbs_TC007");
                throw;
            }
            driver.Quit();
        }

        //TC008 is pending due to data issue

        #endregion

    }
}
