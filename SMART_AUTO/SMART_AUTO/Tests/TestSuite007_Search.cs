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
    public class TestSuite007_Search : Base
    {
        string userName = "Brand Canada";

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
            Results.WriteTestSuiteHeading(typeof(TestSuite007_Search).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite007_Search).Name);

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
        public void TC001_VerifyHeaderPanel(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify Header panel.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage();
                string[] menuIcons = { "User", "Files", "Help", "Search" };
                homePage.VerifyMenusIconButtonsOnTopOfScreen(menuIcons);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifySearchTab(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify search tab.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage();
                string[] menuIcons = { "User", "Files", "Help", "Search" };
                homePage.VerifyMenusIconButtonsOnTopOfScreen(menuIcons);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifyEditSearchScreen(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify Edit Search screen.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("QA Testing - Brand Canada");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC003");
                throw;
            }
            driver.Quit();
        }

        // Pending due to WEB-5861 & WEB-6084 issue
        //[Test]
        //[TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyDateRangeFieldForBasicFieldsInDetailedSection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify Date Range Field for Basic Fields in Detailed Section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen().selectDateRangeOptionFromSection();
                searchPage.checkedOrUnCheckedFixedDateRangeFromSearchScreen(true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC004");
                throw;
            }
            driver.Quit();
        }

        // WEB-6089 issue
        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyMediaFieldForBasicFieldsInDetailedSection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify Media Field for Basic Fields in Detailed Section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyMediaFieldSectionOnScreen();
                searchPage.selectRecordsFromRightSectionAndVerifyIntoSelectedSection();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifySelectDisplayFunctionalityForMediaFields(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify 'Select display' functionality for Media Fields.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyMediaFieldSectionOnScreen();
                searchPage.mouseHoverOnButtonToVerifyEffect().clickButtonOnSearchScreen("Select Displayed");
                searchPage.VerifySelectedRecordsOnSelectDisplayedSection();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyExcludeFunctionalityForMediaFields(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify Exclude Functionality for Media Fields.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyMediaFieldSectionOnScreen();
                searchPage.mouseHoverOnButtonToVerifyEffect("Exclude").clickButtonOnSearchScreen("Exclude");
                searchPage.VerifyExcludeButtonTitlesOnSearchScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyCoopAdvertisersFieldForOtherFieldsInDetailedSection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify Coop Advertisers Field for Other Fields in Detailed Section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyFieldMenuAndClickOnItOnSearchScreen("Coop Advertisers");
                searchPage.selectRecordsFromCoopAdvertisersSection().mouseHoverOnButtonToVerifyEffect("Coop Selected");
                searchPage.clickButtonOnSearchScreen("Coop Selected");
                searchPage.unCheckedItemFromCoopAdvertisersSectionAndVerify();
                searchPage.selectRecordsFromCoopAdvertisersSection(true).clickButtonOnSearchScreen("Coop Selected");
                searchPage.clickButtonOnSearchScreen("Coop Clear Selected");

                string keywordValue = searchPage.enterValueInCoopAdvertisersInputAreaOnScreen("Keyword");
                searchPage.VerifyFilterValueOnCoopAdvertisersSection(keywordValue);
                searchPage.clickButtonOnSearchScreen("Coop Cancel");

                string letter = searchPage.enterValueInCoopAdvertisersInputAreaOnScreen("Letter");
                searchPage.VerifyFilterValueOnCoopAdvertisersSection(letter);
                searchPage.clickButtonOnSearchScreen("Coop Cancel");

                string randomValue = searchPage.enterValueInCoopAdvertisersInputAreaOnScreen("Random");
                searchPage.VerifyFilterValueOnCoopAdvertisersSection(randomValue, true);
                searchPage.clickButtonOnSearchScreen("Coop Cancel");

                searchPage.VerifyTooltipForEachRecordsForCoopAdvertisersSection();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifySelectDisplayFunctionalityForCoopAdvertisers(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify 'Select display' functionality for Coop Advertisers.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyFieldMenuAndClickOnItOnSearchScreen("Coop Advertisers");
                searchPage.mouseHoverOnButtonToVerifyEffect("Coop Select Displayed");
                searchPage.clickButtonOnSearchScreen("Coop Select Displayed");
                searchPage.mouseHoverOnButtonToVerifyEffect("Coop Selected");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyExcludeFunctionalityForCoopAdvertisers(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify Exclude Functionality for Coop Advertisers.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyFieldMenuAndClickOnItOnSearchScreen("Coop Advertisers");
                searchPage.mouseHoverOnButtonToVerifyEffect("Coop Exclude").clickButtonOnSearchScreen("Coop Exclude");
                searchPage.VerifyExcludeButtonAfterClickOnItForCoopAdvertisers();
                searchPage.selectRecordsFromCoopAdvertisersSection(true).clickButtonOnSearchScreen("Coop Selected");
                searchPage.VerifyExcludeButtonAfterClickOnItForCoopAdvertisers(true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyBrowseTabForCoopAdvertisers(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify Browse tab for Coop Advertisers.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyFieldMenuAndClickOnItOnSearchScreen("Coop Advertisers");
                searchPage.clickButtonOnSearchScreen("Coop Browse").VerifyBrowseSectionForCoopAdvertisers();
                searchPage.selectAnyCharacterFromFilterAndVerifyRecords();

                searchPage.selectRecordsFromCoopAdvertisersSection().mouseHoverOnButtonToVerifyEffect("Coop Selected");
                searchPage.clickButtonOnSearchScreen("Coop Selected");
                searchPage.unCheckedItemFromCoopAdvertisersSectionAndVerify();
                searchPage.selectRecordsFromCoopAdvertisersSection(true).clickButtonOnSearchScreen("Coop Selected");
                searchPage.clickButtonOnSearchScreen("Coop Clear Selected");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyKeywordSearchFunctionalityForBasicFields(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify Keyword Search functionality for basic fields.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyFieldMenuAndClickOnItOnSearchScreen("Keyword");
                searchPage.VerifyKeywordSectionOnSearchScreen();
                string option = searchPage.selectRadioOptionFormKeywordSection("Random");
                string searchValue = searchPage.enterKeywordInSearchAreaOnScreen("Existing");
                searchPage.VerifySummaryDetailsAfterKeywordSearch(option, searchValue, "Keyword");
                searchPage.clickRefreshIconAndVerifyMessageForFieldSection("Keyword");
                searchPage.enterKeywordInSearchAreaOnScreen("Random");
                searchPage.VerifyNoDataFoundMessageOnChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC012");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyKeywordSearchFunctionalityForAnySingleColumnValue(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify Keyword search functionality for any single column value.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyFieldMenuAndClickOnItOnSearchScreen("Keyword");
                searchPage.VerifyKeywordSectionOnSearchScreen();
                string option = searchPage.selectRadioOptionFormKeywordSection("Random");
                string searchValue = searchPage.enterKeywordInSearchAreaOnScreen("Existing");
                searchPage.VerifySummaryDetailsAfterKeywordSearch(option, searchValue, "Keyword");
                searchPage.clickRefreshIconAndVerifyMessageForFieldSection("Keyword");
                searchPage.enterKeywordInSearchAreaOnScreen("Random");
                searchPage.VerifyNoDataFoundMessageOnChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyGridRecordsWhenSearchKeywordHasRecordsInIt(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify Grid Records when Search keyword has records in it.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyFieldMenuAndClickOnItOnSearchScreen("Keyword");
                searchPage.VerifyKeywordSectionOnSearchScreen();
                searchPage.selectDateRangeOptionFromSection("Last 6 Months");
                string option = searchPage.selectRadioOptionFormKeywordSection("Random");
                string searchValue = searchPage.enterKeywordInSearchAreaAndVerifyChartValue();
                searchPage.clickButtonOnSearchScreen("Apply Search");
                searchPage.VerifyNumberOfRecordCollectionsOnGrid(searchValue);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_VerifyAdCodeSearchFunctionalityForOtherFields(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify Ad Code search functionality for Other Fields.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyFieldMenuAndClickOnItOnSearchScreen("Ad Code");
                searchPage.VerifyAdCodeSectionOnScreen().enterAdCodeInAdCodeSearchAreaOnScreen("45996309");
                searchPage.VerifySummaryDetailsAfterKeywordSearch("Ad Code", "45996309", "Ad Code");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC015");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_VerifySummarySectionAfterInsertingAdCode(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016-Verify Summary section after inserting Ad code.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyFieldMenuAndClickOnItOnSearchScreen("Ad Code");
                searchPage.VerifyAdCodeSectionOnScreen().enterAdCodeInAdCodeSearchAreaOnScreen("45996309");
                searchPage.VerifySummaryDetailsAfterKeywordSearch("Ad Code", "45996309", "Ad Code");
                searchPage.clickRefreshIconAndVerifyMessageForFieldSection("Ad Code");

                searchPage.enterAdCodeInAdCodeSearchAreaOnScreen("45996309 45996816");
                searchPage.VerifySummaryDetailsAfterKeywordSearch("Ad Code", "( 45996309 OR 45996816 )", "Ad Code");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC016");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC017_VerifyGridRecordsWhenSearchAdCodeHasRecordsInIt(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017-Verify Grid Records when Search Ad code has records in it.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyFieldMenuAndClickOnItOnSearchScreen("Ad Code");
                searchPage.VerifyAdCodeSectionOnScreen().enterAdCodeInAdCodeSearchAreaOnScreen("45996309");
                searchPage.VerifySummaryDetailsAfterKeywordSearch("Ad Code", "45996309", "Ad Code");
                searchPage.clickRefreshIconAndVerifyMessageForFieldSection("Ad Code");

                searchPage.enterAdCodeInAdCodeSearchAreaOnScreen("45996309 45996816");
                searchPage.VerifySummaryDetailsAfterKeywordSearch("Ad Code", "( 45996309 OR 45996816 )", "Ad Code");

                string chartValue = searchPage.VerifyChartRecordValueOnsearchScreen();
                searchPage.clickButtonOnSearchScreen("Apply Search");
                searchPage.VerifyGridRecordsOnScreen(chartValue);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC017");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_VerifyThatSelectedParameterInEditSearchSectionShouldBeDisplayInSummarySection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC018-Verify that selected parameter in Edit search section should be display in Summary section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                string selectedDate = searchPage.VerifySelectedDateRangeORSelectDifferentDateRange(true);
                string selectedNewDate = searchPage.VerifySelectedDateRangeORSelectDifferentDateRange(false);
                searchPage.selectDateRangeOptionFromSection(selectedNewDate);
                searchPage.VerifySummaryDetailsAfterKeywordSearch("", selectedNewDate, "Date Range");
                searchPage.clickRefreshIconAndVerifyMessageForFieldSection("Date Range", selectedDate);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC018");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC019_VerifyRefreshFunctionalityInSummarySection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC019-Verify Refresh functionality in summary Section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                string selectedDate = searchPage.VerifySelectedDateRangeORSelectDifferentDateRange(true);
                string selectedNewDate = searchPage.VerifySelectedDateRangeORSelectDifferentDateRange(false);
                searchPage.selectDateRangeOptionFromSection(selectedNewDate);
                searchPage.VerifySummaryDetailsAfterKeywordSearch("", selectedNewDate, "Date Range");
                searchPage.clickRefreshIconAndVerifyMessageForFieldSection("Date Range", selectedDate);

                searchPage.VerifyFieldMenuAndClickOnItOnSearchScreen("Ad Code");
                searchPage.VerifyAdCodeSectionOnScreen().enterAdCodeInAdCodeSearchAreaOnScreen("45996309");
                searchPage.VerifySummaryDetailsAfterKeywordSearch("Ad Code", "45996309", "Ad Code");
                searchPage.clickRefreshIconAndVerifyMessageForFieldSection("Ad Code");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC019");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC020_VerifySavedSearchTabWhenThereIsNoSearchHasBeenSaved(String Bname)
        {
            TestFixtureSetUp(Bname, "TC020-Verify saved Search tab when there is no search has been saved.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                searchPage.VerifySavedSearchesButtonOnScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC020");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC021_VerifySavedSearchScreen(String Bname)
        {
            TestFixtureSetUp(Bname, "TC021-Verify Saved Search screen.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC021");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC022_VerifyAppliedSearchAndDefaultSearchTabWhenNoSearchHasBeenSelectedAsDefaultApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC022-Verify 'applied search' and 'Default search' tab when No search has been selected as Default/Applied.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                searchPage.VerifySearchTabButtonOnSavedSearchesScreen("Applied Search", true);
                searchPage.VerifySearchTabButtonOnSavedSearchesScreen("Default Search", true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC022");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC023_VerifySaveSearchCardInSavedSearchScreen(String Bname)
        {
            TestFixtureSetUp(Bname, "TC023-Verify save search Card in Saved search screen.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC023");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC024_VerifyScreenAfterClickingOnAppliedSearchButtonBesideSearchTextField(String Bname)
        {
            TestFixtureSetUp(Bname, "TC024-Verify screen after clicking on Applied Search button beside search text field.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                searchPage.clickButtonForSavedSearchCardOnScreen("Random", "Apply Search");

                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                searchPage.VerifySearchTabButtonOnSavedSearchesScreen("Applied Search", false);
                searchPage.VerifySearchTabButtonOnSavedSearchesScreen("Default Search", true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC024");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC025_VerifyScreenAfterClickingOnDefaultSearchButtonBesideSearchTextField(String Bname)
        {
            TestFixtureSetUp(Bname, "TC025-Verify screen after clicking on Default Search button beside search text field.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                searchPage.clickButtonForSavedSearchCardOnScreen("Random", "Make Default");
                searchPage.VerifySearchTabButtonOnSavedSearchesScreen("Default Search", false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC025");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC026_VerifySearchNameAfterEditSearchByClickingOnEditBesideSearchName(String Bname)
        {
            TestFixtureSetUp(Bname, "TC026-Verify screen after clicking on Default Search button beside search text field.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                searchPage.getSavedSearchNameOrClickForSavedSearchRecordOnScreen("Random", "Save", true);
                searchPage.getSavedSearchNameOrClickForSavedSearchRecordOnScreen("Random", "Clear");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC026");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC027_VerifySearchFunctionalityOfSavedSearchedWhen_ContinueEditing_CheckboxIsChecked(String Bname)
        {
            TestFixtureSetUp(Bname, "TC027-Verify Search functionality of Saved Searched when 'Continue Editing' Checkbox is checked.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                searchPage.clickButtonOnSearchScreen("Save As");
                searchPage.VerifySaveAsSectionAfterClickingOnSaveAsButton();
                searchPage.enterSearchValueOnSearchScreen();
                searchPage.clickButtonOnSearchScreen("Save!").VerifyMySearchScreen("Brand Canada");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC027");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC028_VerifySearchFunctionalityOfSavedSearchedWhen_ContinueEditing_CheckboxIsUnchecked(String Bname)
        {
            TestFixtureSetUp(Bname, "TC028-Verify Search functionality of Saved Searched when 'Continue Editing' Checkbox is unchecked.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                searchPage.clickButtonOnSearchScreen("Save As").VerifySaveAsSectionAfterClickingOnSaveAsButton();
                string searchName = searchPage.enterSearchValueOnSearchScreen();
                searchPage.checkOrUnCheckCheckboxForSavedSearch("Continue Editing", true);
                searchPage.clickButtonOnSearchScreen("Save!");
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                searchPage.enterValueInFilterSavedSearchedInputAreaOnScreen(searchName).VerifySavedSearchNameFromList(searchName);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC028");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC029_VerifySearchFunctionalityOfSavedSearchedWhen_MakeDefault_CheckboxIsChecked(String Bname)
        {
            TestFixtureSetUp(Bname, "TC029-Verify Search functionality of Saved Searched when 'Make Default' Checkbox is checked.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                searchPage.clickButtonOnSearchScreen("Save As").VerifySaveAsSectionAfterClickingOnSaveAsButton();
                string searchName = searchPage.enterSearchValueOnSearchScreen();
                searchPage.checkOrUnCheckCheckboxForSavedSearch("Make Default", false);
                searchPage.clickButtonOnSearchScreen("Save!").VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySearchTabButtonOnSavedSearchesScreen("Default Search", false);
                searchPage.clickButtonOnSearchScreen("Default Search");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC029");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC030_VerifyDeleteSavedSearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC030-Verify Delete Saved Search Functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                searchPage.clickDeleteButtonForSavedSearchRecordFromListAndVerifyMessage(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC030");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC031_VerifyEditSavedSearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC031-Verify Edit saved Search functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                searchPage.clickButtonForSavedSearchCardOnScreen("Random", "Edit Search");
                searchPage.VerifyMySearchScreen("Brand Canada");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC031");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC032_VerifyMakeDefaultSearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC032-Verify Make Default search functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                string searchTitle = searchPage.clickButtonForSavedSearchCardOnScreen("Random", "Make Default");
                searchPage.VerifySearchTabButtonOnSavedSearchesScreen("Default Search", false);
                searchPage.clickButtonOnSearchScreen("Default Search");
                searchPage.VerifySavedSearchNameFromList(searchTitle);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC032");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC033_VerifyAfterClickingApplySearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC033-Verify after Clicking Apply search.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                string searchTitle = searchPage.clickButtonForSavedSearchCardOnScreen("Random", "Apply Search");
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyAppliedSearchFieldInChartDetailsSection(searchTitle);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC033");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC034_VerifyPaginationInTheBottomOfTheScreen(String Bname)
        {
            TestFixtureSetUp(Bname, "TC034-Verify Pagination in the bottom of the screen.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                searchPage.VerifyPaginationPanelOfSavedSearched();
                searchPage.clickOnButtonFromPaginationPanel("next").clickOnButtonFromPaginationPanel("prev");
                searchPage.clickOnButtonFromPaginationPanel("page");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC034");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC035_VerifySummarySectionWhenNoSearchHasBeenApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC035-Verify summary Section when No Search has been Applied.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").clickButtonOnSearchScreen("Reset"); ;
                searchPage.VerifyResetChangesMessageOnScreen(true);
                searchPage.VerifyAppliedSearchFieldInChartDetailsSection("None Selected");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC035");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC036_VerifyResetFunctionalityWhenNoSearchHasBeenApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC036-Verify Reset Functionality when no search has been applied.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").clickButtonOnSearchScreen("Reset");
                searchPage.VerifyResetChangesMessageOnScreen(true);
                searchPage.VerifySelectedDateRangeORSelectDifferentDateRange(false);
                searchPage.clickButtonOnSearchScreen("Reset").VerifyResetChangesMessageOnScreen(true);
                searchPage.clickButtonOnSearchScreen("Reset").VerifyResetChangesMessageOnScreen(false);
                searchPage.VerifyMySearchScreen("Brand Canada");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC036");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC037_VerifyResetFunctionalityWhenSearchHasBeenApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC037-Verify Reset Functionality when search has been applied.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                string searchTitle = searchPage.clickButtonForSavedSearchCardOnScreen("Random", "Apply Search");

                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada").VerifyAppliedSearchFieldInChartDetailsSection(searchTitle);
                searchPage.clickButtonOnSearchScreen("Reset").VerifyResetChangesMessageOnScreen(true);
                searchPage.clickButtonOnSearchScreen("Reset").VerifyResetChangesMessageOnScreen(false);
                searchPage.VerifyMySearchScreen("Brand Canada");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC037");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC038_VerifyRestFunctionalityWhenDefaultSearchHasBeenSet(String Bname)
        {
            TestFixtureSetUp(Bname, "TC038-Verify Rest Functionality when Default Search has been set.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                searchPage.clickButtonOnSearchScreen("Reset").VerifyResetChangesMessageOnScreen(true);
                searchPage.clickButtonOnSearchScreen("Reset").VerifyResetChangesMessageOnScreen(false);
                searchPage.VerifyMySearchScreen("Brand Canada");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC038");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC039_VerifyOverwritingExistingSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC039-Verify Overwriting existing Search.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                string searchName = searchPage.clickButtonForSavedSearchCardOnScreen("Random", "Edit Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                searchPage.VerifyButtonDisableOrNotOnScreen("Overwrite", true);
                searchPage.VerifySelectedDateRangeORSelectDifferentDateRange(false);
                searchPage.VerifyButtonDisableOrNotOnScreen("Overwrite", false);
                searchPage.clickButtonOnSearchScreen("Overwrite");
                searchPage.VerifyOverwriteSectionWithMessageOnScreen(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite007_Search_TC039");
                throw;
            }
            driver.Quit();
        }

        #endregion
    }
}
