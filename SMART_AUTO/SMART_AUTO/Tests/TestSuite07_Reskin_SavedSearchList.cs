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
    public class TestSuite07_Reskin_SavedSearchList : Base
    {
        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        Search searchPage;
        FieldOptions fieldOptions;
        UserProfile userProfile;
        SummaryTags summaryTags;
        SecondaryButtons secondaryButtons;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite07_Reskin_SavedSearchList).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite07_Reskin_SavedSearchList).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            userProfile = new UserProfile(driver, test);
            summaryTags = new SummaryTags(driver, test);
            secondaryButtons = new SecondaryButtons(driver, test);

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
        public void TC001_VerifySaved_Default_SearchListWhenNoSearchIsCreatedByUser(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify Saved(Default) search list when no search is created by user.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                homePage.selectSavedSearchOrCreateNewSavedSearch(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyUserIsAbleToCreateNewSearchSuccessfully(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify User able to create new Search successfully");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                homePage.newVerifyHomePageInDetail("QA Testing - Brand");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_4_5_VerifyMaximumOnly10SavedSearchShouldBeDisplayed(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003_4_5-Verify Maximum only 10 saved search should be displayed");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                searchPage.VerifyLoadMoreButtonOnSavedSearchDDL("QA Testing - Brand");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC003_4_5");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyThatUserIsAbleToApplyAnySavedSearchSuccessfully(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify, User should be able to apply any saved search successfully");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                string[] summary = { "Search Options", "Last 7 Days" };
                summaryTags.VerifySummaryTags(summary);
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch();
                summaryTags.VerifySummaryTags(new string[] { searchName });
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_8_VerifyDeleteSavedSearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007_8-Verify Deleted saved search functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                string[] summary = { "Search Options", "Last 7 Days" };
                summaryTags.VerifySummaryTags(summary);
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch();
                summaryTags.VerifySummaryTags(new string[] { searchName });
                homePage.clickOnDashboardButton("Delete");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[text()=' Successfully deleted saved search: " + searchName + " ']"), "'Deleted Successfully' Message not displayed.");
                searchPage.VerifyAndLoadSpecificSavedSearch(searchName, false);
                searchPage.VerifyLoadMoreButtonOnSavedSearchDDL("QA Testing - Brand", false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC007_8");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifySavedSearchShouldBeDisplayedInAscendingOrder(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify Saved search should be displayed in Ascending order");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                searchPage.VerifyThatSavedSearchNamesAreListedInAscendingOrder();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_18_VerifyLoadMoreSavedSearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010_18-Verify 'Load More' functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                searchPage.VerifyLoadMoreButtonOnSavedSearchDDL("QA Testing - Brand", true);
                searchPage.VerifyThatLoadMoreButtonLoadsMoreSearches("QA Testing - Brand");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC010_18");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyModifiedSearchAndSearchNameShouldBeReflectedInSavedSearchList(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify Modified Search and search name should be reflected in Saved search list");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                homePage.VerifyAndModifySearchOptions(true, false, "QA Testing - Brand");
                homePage.newVerifyHomePage();
                string searchName = homePage.saveNewSearch(false, true, "QA Testing - Brand");
                searchPage.VerifyAndLoadSpecificSavedSearch(searchName, true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyThatSavedSearchListIsSpecificForEachUserAccount(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify Saved search list should be specific for each user account");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                string[] savedSearchList1 = searchPage.VerifyThatSavedSearchNamesAreListedInAscendingOrder();
                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("Settings");
                userProfile.selectAccountNameOnUserScreen("QA Testing - Brand Canada");
                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                string[] savedSearchList2 = searchPage.VerifyThatSavedSearchNamesAreListedInAscendingOrder();
                fieldOptions.compareListOfItemsInOrder(savedSearchList1, savedSearchList2, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC012");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyThatUserIsNotAbleToCreateDuplicateSaveSearchListWhileCreateNewSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify User should not be able to create duplicate save search list while create new search");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                homePage.VerifyAndModifySearchOptions(true, false, "QA Testing - Brand");
                homePage.newVerifyHomePage();
                homePage.saveNewSearch(false, true, "QA Testing - Brand", searchName);
                Assert.IsTrue(driver._waitForElement("xpath", "//p[@class='text-danger' and text()='Please use a unique name for this saved search']"), "'Please use a unique name for this saved search' message not present for duplicate search name");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyThatUserIsNotAbleToCreateDuplicateSaveSearchListWhileUpdatingAnyAlreadyCreatedNewSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify User should not be able to create duplicate save search list while updating any already created new search");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                homePage.VerifyAndModifySearchOptions(true, false, "QA Testing - Brand");
                homePage.newVerifyHomePage();
                homePage.saveNewSearch(false, true, "QA Testing - Brand");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(true, false, "QA Testing - Brand");
                homePage.saveNewSearch(false, true, "QA Testing - Brand", searchName);
                Assert.IsTrue(driver._waitForElement("xpath", "//div[text()=' Please use a unique name for this saved search ']"), "'Please use a unique name for this saved search' message not present for duplicate search name");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_16_17_22_VerifyNewSearchFunctionalityAfterUserHasAppliedAnySearchFromSavedSearchList(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015_16_17_22-Verify New Search functionality after user have applied any search from saved search list");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                string[] summary = { "Search Options", "Running", "Last 7 Days"};
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch();
                summaryTags.VerifySummaryTags(new string[] { searchName });
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                homePage.newVerifyHomePage();
                summaryTags.VerifySummaryTags(summary);
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC015_16_17_22");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC020_21_VerifyNoOfCreativesByUpdatingAnAppliedSearchAndNewSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC020_21-Verify no. of creatives by updating applied search and new search");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                int currCreatives = searchPage.returnNumberOfCreativesOfASearch("New Search");
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions();
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                int newCreatives = searchPage.returnNumberOfCreativesOfASearch("New Search");
                Assert.AreNotEqual(currCreatives, newCreatives, "Creative count did not get updated for 'New Search'");
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch();
                homePage.newVerifyHomePage();
                currCreatives = searchPage.returnNumberOfCreativesOfASearch(searchName);
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions();
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                newCreatives = searchPage.returnNumberOfCreativesOfASearch(searchName);
                Assert.AreNotEqual(currCreatives, newCreatives, "Creative count did not get updated for '" + searchName + "'");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC020_21");
                throw;
            }
            driver.Quit();
        }

        //TC023 is pending to be created by the Manual Team

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC024_26_VerifyWhenTheSearchIsLoadingAndShowingNoItemsTheCountShouldShow0(String Bname)
        {
            TestFixtureSetUp(Bname, "TC024_26-Verify when the search is loading and showing no items the count should show 0");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                homePage.VerifyAndModifySearchOptions();
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.setCustomDateRange("01/01/2016");
                searchPage.clickButtonOnSearchOptions("Apply");
                string searchName = homePage.saveNewSearch();
                summaryTags.removeSearchNameSummaryTag(searchName);
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch(searchName, true, true);
                int currCreatives = searchPage.returnNumberOfCreativesOfASearch(searchName);
                homePage.newVerifyHomePage();
                int newCreatives = searchPage.returnNumberOfCreativesOfASearch(searchName);
                Assert.AreNotEqual(0, currCreatives, "Creative count is 0 for '" + searchName + "' after search is loaded and creatives are present");
                secondaryButtons.clickOnSecondaryButtons("Delete");
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                currCreatives = searchPage.returnNumberOfCreativesOfASearch("New Search");
                homePage.newVerifyHomePage();
                Assert.AreNotEqual(0, currCreatives, "Creative count is 0 for '" + searchName + "' after search is loaded and creatives are present");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC024_26");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC025_VerifyNoOfCreativesWhenNoRecordFound(String Bname)
        {
            TestFixtureSetUp(Bname, "TC025-Verify no. of creatives when no record found");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                homePage.VerifyAndModifySearchOptions();
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.selectNewDateRangeOptionFromSection("Yesterday");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Print");
                searchPage.clickButtonOnSearchOptions("Apply");
                string searchName = homePage.saveNewSearch();
                summaryTags.removeSearchNameSummaryTag(searchName);
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch(searchName, true, true);
                int newCreatives = searchPage.returnNumberOfCreativesOfASearch(searchName);
                Assert.AreEqual(0, newCreatives, "Creative count is not 0 for '" + searchName + "'");
                Assert.IsTrue(driver._isElementPresent("xpath", "//*[text()='No items found!']"), "Data is present for search that has 0 creative count displayed against it.");
                secondaryButtons.clickOnSecondaryButtons("Delete");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite07_Reskin_SavedSearchList_TC025");
                throw;
            }
            driver.Quit();
        }



        #endregion
    }
}
