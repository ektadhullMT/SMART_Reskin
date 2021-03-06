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
    public class TestSuite008_FilterBar : Base
    {
        string userName = "QA Testing - Brand";

        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        Search searchPage;
        PromoDashboard promoDashboard;
        Schedule schedule;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite008_FilterBar).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite008_FilterBar).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            promoDashboard = new PromoDashboard(driver, test);
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
        public void TC001_VerifyFilterBarForPromoDashboard(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify Filter bar for Promo Dashboard.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                promoDashboard.VerifyPromoDashboardScreen().VerifyFilterBarSectionOnScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite008_FilterBar_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyDateRangeFieldInFilterBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify Date Range Field in Filter bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                promoDashboard.VerifyFilterBarSectionOnScreen(false);
                string[] opt = { "Custom Range", "Today", "Yesterday", "Last 7 Days", "Last 14 Days", "Last Month", "Last 3 Months", "Last 6 Months", "Year To Date", "Last Year" };
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("Days", "", opt);
                promoDashboard.VerifyFromDateAndToDatePickerOnScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite008_FilterBar_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifyManufacturersFieldDropdownForFilterBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify 'Manufacturers' field drop down for Filter bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                promoDashboard.VerifyFilterBarSectionOnScreen(false);
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("Manufacturers");
                promoDashboard.VerifyFilterSectionOnScreen("Manufacturers", true);
                promoDashboard.clickButtonOnFilterSectionOnScreen("Select Displayed");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(true);
                promoDashboard.clickButtonOnFilterSectionOnScreen("Slash").VerifyExcludedButtonLabelOnFilterSection();
                promoDashboard.clickButtonOnFilterSectionOnScreen("Load More");
                promoDashboard.enterKeywordToSerachIntoFilterTextBox(1).VerifyTooltipOnFilterSection("Min Char Limit 2");
                promoDashboard.clearKeywordFromSearchTextBox();

                promoDashboard.clickButtonOnFilterSectionOnScreen("Browse").VerifyBrowseTabOnFilterSection();
                promoDashboard.clickButtonOnFilterSectionOnScreen("Excluded").VerifyExcludedButtonLabelOnFilterSection();
                string characterName = promoDashboard.selectCharacterFromBrowserTab();
                promoDashboard.VerifyFilterListRecordsValueWithSelectedCharacter(characterName);
                promoDashboard.clickButtonOnFilterSectionOnScreen("Excluded Remove").selectRecordsFromListOnFilterSection();
                promoDashboard.clickButtonOnFilterSectionOnScreen("Selected");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(true).clickButtonOnFilterSectionOnScreen("Clear Selected");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite008_FilterBar_TC003");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyMarketsFieldDropdownForFilterBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify 'Markets' field drop down for Filter bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");
                
                promoDashboard.VerifyFilterBarSectionOnScreen(false);
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("Markets");
                promoDashboard.VerifyFilterSectionOnScreen("Markets", false);
                promoDashboard.clickButtonOnFilterSectionOnScreen("Select Displayed");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(true);
                promoDashboard.clickButtonOnFilterSectionOnScreen("Slash").VerifyExcludedButtonLabelOnFilterSection();

                promoDashboard.clickButtonOnFilterSectionOnScreen("Slash Remove").clickButtonOnFilterSectionOnScreen("Selected");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(true).clickButtonOnFilterSectionOnScreen("Clear Selected");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite008_FilterBar_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyPageLocationsFieldDropdownForFilterBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify 'Page Locations' field drop down for Filter bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                promoDashboard.VerifyFilterBarSectionOnScreen(false);
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("Page Locations");
                promoDashboard.VerifyFilterSectionWithCheckbox("Page Locations");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite008_FilterBar_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyResetAllButtonWhenNoFilterIsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify 'Reset All' button when no Filter is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                promoDashboard.VerifyFilterBarSectionOnScreen(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite008_FilterBar_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyResetAllButtonWhenFilterIsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify 'Reset All' button when Filter is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                promoDashboard.VerifyFilterBarSectionOnScreen(false);
                promoDashboard.VerifyAndClickResetAllButtonOnFilterSection(false);
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("Days", "Last 6 Months");
                promoDashboard.VerifyAndClickResetAllButtonOnFilterSection(true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite008_FilterBar_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyPreviousIconOnFilterSlider(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify Previous icon on Filter Slider.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                promoDashboard.VerifyFilterBarSectionOnScreen(false);
                promoDashboard.VerifyAndClickIconOnFilterSlider("Previous");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite008_FilterBar_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyNextIconOnFilterSlider(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify Next icon on Filter Slider.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                promoDashboard.VerifyFilterBarSectionOnScreen(false);
                promoDashboard.VerifyAndClickIconOnFilterSlider("Next");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite008_FilterBar_TC009");
                throw;
            }
            driver.Quit();
        }

        #endregion
    }
}
