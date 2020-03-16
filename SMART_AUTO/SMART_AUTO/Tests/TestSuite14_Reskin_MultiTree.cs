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
    public class TestSuite14_Reskin_MultiTree : Base
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
            Results.WriteTestSuiteHeading(typeof(TestSuite14_Reskin_MultiTree).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite14_Reskin_MultiTree).Name);

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
        public void TC001_3_VerifyCompanyDivisionsBrandsFieldInSearchOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001_3-Verify Company divisions Brands field in Search options.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite14_Reskin_MultiTree_TC001_3");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_5_VerifySelectFunctionalityInCompaniesDivisionsBrandsField(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify 'Select' functionality in Companies Divisions Brands field.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("1 PAPERBOAT");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite14_Reskin_MultiTree_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyLoadMoreFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify 'Load more' functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.clickAndVerifyLoadMoreButtonOnMultiTreeSearchOptions();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite14_Reskin_MultiTree_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_7_9_VerifyThatAnyPartial_FullySelectedCompany_DivisionFromLeftSectionShouldBeDisplayedAsSelectedInRightSection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006_7_9-Verify, Any partial/fully selected Company/division from Left section should be displayed as Selected in right section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("General Mills");
                searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.clickAndVerifyClearSelectedButtonOnMultiTreeSearchOptions();
                searchPage.clickAndVerifyButtonsOnFilterBarsSearchOptions("Clear");
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("General Mills Inc");
                searchPage.readSelectedOptionsOnSearchOptions();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite14_Reskin_MultiTree_TC006_7_9");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifySelectDisplayedFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify Select Displayed functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.clickAndVerifyButtonsOnFilterBarsSearchOptions("Select Displayed");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite14_Reskin_MultiTree_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_11_VerifyThatPivotAndAgGridDataResultShouldBeAccordingToPartially_FullySelectedDivisionFromAnyMultitree_Company(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010_11-Verify, Pivot and AgGrid data result should be according to Partially/Fully selected Division from any Multitree/Company.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Category");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.VerifyNewDateRangeSectionOnScreen("Last Media Week");
                searchPage.setCustomDateRange("05/01/2015");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("General Mills");
                searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.clickButtonOnSearchOptions("Apply");
                pivotGrid.VerifyAppliedFilterInPivotGridAndAgGrid("Company", new string[] { "GENERAL MILLS INC" });

                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.VerifyNewDateRangeSectionOnScreen("Last Media Week");
                searchPage.setCustomDateRange("05/01/2015");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("General Mills");
                searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.clickButtonOnSearchOptions("Apply");
                pivotGrid.VerifyAppliedFilterInPivotGridAndAgGrid("Company", new string[] { "GENERAL MILLS INC" });
                pivotGrid.VerifyAppliedFilterInPivotGridAndAgGrid("Division", new string[] { "PILLSBURY DIV", "GENERAL MILLS DIV", "LIBERTE DIV" });
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite14_Reskin_MultiTree_TC010_11");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyThatPivotAndAgGridDataResultShouldBeAccordingToFullySelectedDivision(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify, Pivot and AgGrid data result should be according to Fully selected Division.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Category");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.VerifyNewDateRangeSectionOnScreen("Last Media Week");
                searchPage.setCustomDateRange("05/01/2015");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("General Mills Div");
                searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.clickButtonOnSearchOptions("Apply");
                pivotGrid.VerifyAppliedFilterInPivotGridAndAgGrid("Division", new string[] { "GENERAL MILLS DIV" });
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite14_Reskin_MultiTree_TC012");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyIterationOfPartialSelect_AllSelectAndResetFunctionalityAfterOneCycle_MainTree(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify, Iteration of Partial select, All select and Reset functionality after one cycle (Main Tree).");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Category");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.VerifyNewDateRangeSectionOnScreen("Last Media Week");
                searchPage.setCustomDateRange("05/01/2015");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("General Mills");
                searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("General Mills");
                searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("General Mills");
                searchPage.readSelectedOptionsOnSearchOptions();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite14_Reskin_MultiTree_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyExcludeFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify 'Exclude' functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Category");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.VerifyNewDateRangeSectionOnScreen("Last Media Week");
                searchPage.setCustomDateRange("05/01/2015");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("General Mills", 2);
                searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.clickAndVerifyButtonsOnFilterBarsSearchOptions("Exclude");
                searchPage.clickButtonOnSearchOptions("Apply");
                pivotGrid.VerifyAppliedFilterInPivotGridAndAgGrid("Company", new string[] { "GENERAL MILLS INC" }, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite14_Reskin_MultiTree_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_VerifyThatSelectionShouldBeRemovedAfterUserHasUncheckedOptionsFromSelectedItemsSection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify, Selection should be  removed  after user has unchecked options from 'selected items section'.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Report by Category");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.VerifyNewDateRangeSectionOnScreen("Last Media Week");
                searchPage.setCustomDateRange("05/01/2015");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("General Mills Inc", 2);
                searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.DeselectFromSelectedOptionsInMultiTree("General Mills Div");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite14_Reskin_MultiTree_TC015");
                throw;
            }
            driver.Quit();
        }

        #endregion

    }
}
