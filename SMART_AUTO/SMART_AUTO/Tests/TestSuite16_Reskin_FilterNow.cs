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
    public class TestSuite16_Reskin_FilterNow : Base
    {
        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        Search searchPage;
        Schedule schedule;
        Charts charts;
        TabularGrid tabularGrid;
        SecondaryButtons secondaryButtons;
        SummaryTags summaryTags;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite16_Reskin_FilterNow).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite16_Reskin_FilterNow).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            secondaryButtons = new SecondaryButtons(driver, test);
            schedule = new Schedule(driver, test);
            charts = new Charts(driver, test);
            tabularGrid = new TabularGrid(driver, test);
            summaryTags = new SummaryTags(driver, test);

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
        public void TC001_2_VerifyFilterNowFunctionalityFromFilterBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001_2-Verify Filter now functionality from filter bar");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Creative Search");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar();
                searchPage.SearchFromFilterNowBar("Food");
                searchPage.VerifyFilterNowBar("Food");
                secondaryButtons.VerifySecondaryButtons(true);
                summaryTags.VerifySummaryTags(new string[] { "Food in All Fields" }, false);
                summaryTags.VerifyTooltipsOfSummaryTags();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite16_Reskin_FilterNow_TC001_2");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifyThatEnteredTextInFilterBarShouldBeDisplayedInKeywordFieldSearchOption(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify Entered text in filter bar should be displayed in Keyword field search option");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Creative Search");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar();
                searchPage.SearchFromFilterNowBar("Food");
                searchPage.VerifyFilterNowBar("Food");
                summaryTags.VerifySummaryTags(new string[] { "Food in All Fields" }, false);

                summaryTags.clickOnSingleSummaryTag("Food in All Fields");
                searchPage.VerifyNewKeywordSectionOnSearchScreen("Food");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite16_Reskin_FilterNow_TC003");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyKeywordSearchOptionFromSearchOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify Keyword search option from search options");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Creative Search");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");

                searchPage.selectTabOnSearchOptions("Keyword");
                searchPage.VerifyNewKeywordSectionOnSearchScreen();
                searchPage.enterNewKeywordInSearchAreaOnScreen("Food");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar("Food");
                summaryTags.VerifySummaryTags(new string[] { "Food in All Fields" }, false);

                summaryTags.clickOnSingleSummaryTag("Food in All Fields");
                searchPage.VerifyNewKeywordSectionOnSearchScreen("Food");
                searchPage.enterNewKeywordInSearchAreaOnScreen("Food", "Headline");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar("Food");
                summaryTags.VerifySummaryTags(new string[] { "Food in Headline" }, false);

                summaryTags.clickOnSingleSummaryTag("Food in Headline");
                searchPage.VerifyNewKeywordSectionOnSearchScreen("Food");
                searchPage.enterNewKeywordInSearchAreaOnScreen("MILLION", "Lead Text");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar("MILLION");
                summaryTags.VerifySummaryTags(new string[] { "MILLION in Lead Text" }, false);

                summaryTags.clickOnSingleSummaryTag("MILLION in Lead Text");
                searchPage.VerifyNewKeywordSectionOnSearchScreen("MILLION");
                searchPage.enterNewKeywordInSearchAreaOnScreen("man", "Visual");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar("man");
                summaryTags.VerifySummaryTags(new string[] { "man in Visual" }, false);

                summaryTags.clickOnSingleSummaryTag("man in Visual");
                searchPage.VerifyNewKeywordSectionOnSearchScreen("man");
                searchPage.enterNewKeywordInSearchAreaOnScreen("book", "Description");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar("book");
                summaryTags.VerifySummaryTags(new string[] { "book in Description" }, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite16_Reskin_FilterNow_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyClearButtonFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify Clear button Functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Creative Search");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");

                searchPage.selectTabOnSearchOptions("Keyword");
                searchPage.VerifyNewKeywordSectionOnSearchScreen();
                searchPage.enterNewKeywordInSearchAreaOnScreen("Food");
                searchPage.VerifyNewKeywordSectionOnSearchScreen("Food", true);
                searchPage.VerifyNewKeywordSectionOnSearchScreen();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite16_Reskin_FilterNow_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyThatIfUserSearchesForDateDataThenItShouldBeConvertedIntoNumbers(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify If user search for date data then it should be converted into numbers");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Creative Search");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar();
                searchPage.SearchFromFilterNowBar("1/11/19");
                searchPage.VerifyFilterNowBar("11119");
                summaryTags.VerifySummaryTags(new string[] { "11119 in All Fields" }, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite16_Reskin_FilterNow_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifySummaryTagResetFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify Summary tag reset functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Creative Search");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar();
                searchPage.SearchFromFilterNowBar("Food");
                searchPage.VerifyFilterNowBar("Food");
                summaryTags.VerifySummaryTags(new string[] { "Food in All Fields" }, false);

                secondaryButtons.clickOnSecondaryButtons("Reset");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar();
                summaryTags.VerifySummaryTags(new string[] { "Search Options", "Last 7 Days" }, true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite16_Reskin_FilterNow_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyThatUserCanFilterDataWithOneOrMoreKeyword(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify User can filter data with one or more keyword");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Creative Search");
                homePage.newVerifyHomePage();
                searchPage.VerifyFilterNowBar();
                searchPage.SearchFromFilterNowBar("Food Book");
                searchPage.VerifyFilterNowBar("Food Book");
                summaryTags.VerifySummaryTags(new string[] { "( Food AND Book ) in All Fields" }, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite16_Reskin_FilterNow_TC008");
                throw;
            }
            driver.Quit();
        }

        #endregion
    }
}
