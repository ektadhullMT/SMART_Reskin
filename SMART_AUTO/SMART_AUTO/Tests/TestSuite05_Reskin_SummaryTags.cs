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
    public class TestSuite05_Reskin_SummaryTags : Base
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
            Results.WriteTestSuiteHeading(typeof(TestSuite05_Reskin_SummaryTags).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite05_Reskin_SummaryTags).Name);

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
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyDefaultSummaryTagsList(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify Default summary tags list.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Running", "Last 7 Days" };
                summaryTags.VerifySummaryTags(summary);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifyThatDefaultDateRangeTagNamesShouldBeAccordingToDateRange(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify, Default date range tag names should be according to date range.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Running", "Last 7 Days" };
                summaryTags.VerifySummaryTags(summary);
                homePage.VerifyAndModifySearchOptions();
                searchPage.VerifyNewDateRangeSectionOnScreen();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC003");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyDateRangeTagDetailShouldBeChangedAcordingToDateChangeFromSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify Date range Tag detail should be changed according to date change from search");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("Settings");
                userProfile.selectAccountNameOnUserScreen("QA Testing - Brand");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Running", "Last 7 Days" };
                summaryTags.VerifySummaryTags(summary);
                homePage.VerifyAndModifySearchOptions();
                searchPage.VerifyNewDateRangeSectionOnScreen();
                summary[2] = searchPage.selectNewDateRangeOptionFromSection();
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                summaryTags.VerifySummaryTags(summary, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifySummaryTagsShouldBeDisplayedForAllTheFieldOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify Summary tags should be displayed for all the field options.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Running", "Last 7 Days" };
                summaryTags.VerifySummaryTags(summary);
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.VerifyNewDateRangeSectionOnScreen("Last Media Week");
                string dateRange = searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Keyword");
                searchPage.VerifyNewKeywordSectionOnSearchScreen();
                searchPage.enterNewKeywordInSearchAreaOnScreen("Random");
                string keyword = searchPage.selectRadioOptionFormNewKeywordSection("Random");
                keyword = " in " + keyword;
                searchPage.selectTabOnSearchOptions("Ad Code");
                searchPage.VerifyNewAdCodeSectionOnScreen();
                string adcode = searchPage.enterAdCodeInNewAdCodeSearchAreaOnScreen("45836497") + " in Ad Code";
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaSectionOnScreen();
                string mediaType = searchPage.selectSpecificMediaTypeOnSearchOptions("Random");
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                string companyDivisionBrand = searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("1 RAINBOW TAXI");
                searchPage.selectTabOnSearchOptions("Category Class");
                searchPage.VerifyCategoryClassSectionOnSearchScreen();
                string categoryClass = searchPage.selectCategoryClassOptionOnSearchScreen("APPAREL");
                searchPage.selectTabOnSearchOptions("Language");
                searchPage.VerifyNewLanguageSectionOnScreen();
                searchPage.selectSpecificLanguageOnSearchOptions("English");
                searchPage.selectTabOnSearchOptions("Coop Advertisers");
                searchPage.VerifyNewCoopAdvertisersSectionOnScreen();
                searchPage.selectSpecificCoopAdvertiserOnSearchOptions("1 PAPERBOAT");
                searchPage.selectTabOnSearchOptions("Market");
                searchPage.VerifyMarketSectionOnSearchScreen();
                string market = searchPage.selectMarketOptionOnSearchScreen("Random");
                searchPage.selectTabOnSearchOptions("Media Outlet");
                searchPage.VerifyNewMediaOutletSectionOnScreen();
                searchPage.selectSpecificMediaOutletOnSearchOptions("7 JOURS");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                string[] newSummary = { "Search Options", keyword, adcode, dateRange, mediaType, companyDivisionBrand, categoryClass, "English", "1 PAPERBOAT", market, "7 JOURS" };
                summaryTags.VerifySummaryTags(newSummary, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyTooltipsOfTheSummaryTags(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify Tool Tips of the Summary Tags.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Last Media Week" };
                summaryTags.VerifySummaryTags(summary);
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Category Class");
                searchPage.VerifyCategoryClassSectionOnSearchScreen();
                searchPage.selectCategoryClassOptionOnSearchScreen("APPAREL", "APPAREL: ACCESSORIES");
                string selection1 =  searchPage.readSelectedOptionsOnSearchOptions() ;
                searchPage.selectTabOnSearchOptions("Market");
                searchPage.VerifyMarketSectionOnSearchScreen();
                searchPage.selectMarketOptionOnSearchScreen("Alberta", "Calgary");
                searchPage.selectMarketOptionOnSearchScreen("Alberta", "Edmonton");
                string selection2 = searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("1 RAINBOW TAXI");
                string selection3 = searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                string[] selection = { selection1, selection2, selection3 };
                string[] newSummary = summaryTags.convertSearchSelectionToSummaryTags(summary, selection);
                summaryTags.VerifySummaryTags(summary, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyResetFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify Reset Functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Last Media Week" };
                summaryTags.VerifySummaryTags(summary);
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Category Class");
                searchPage.VerifyCategoryClassSectionOnSearchScreen();
                searchPage.selectCategoryClassOptionOnSearchScreen("APPAREL", "APPAREL: ACCESSORIES");
                string selection1 = searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.selectTabOnSearchOptions("Market");
                searchPage.VerifyMarketSectionOnSearchScreen();
                searchPage.selectMarketOptionOnSearchScreen("Alberta", "Calgary");
                searchPage.selectMarketOptionOnSearchScreen("Alberta", "Edmonton");
                string selection2 = searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.selectTabOnSearchOptions("Company Division Brand");
                searchPage.VerifyCompanyDivisionBrandSectionOnSearchScreen();
                searchPage.selectCompanyDivisionBrandOptionOnSearchScreen("1 RAINBOW TAXI");
                string selection3 = searchPage.readSelectedOptionsOnSearchOptions();
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                string[] selection = { selection1, selection2, selection3 };
                string[] newSummary = summaryTags.convertSearchSelectionToSummaryTags(summary, selection);
                summaryTags.VerifySummaryTags(summary, false);
                homePage.clickOnDashboardButton("Reset");
                summaryTags.VerifySummaryTags(summary);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyThatUserIsAbleToResetSingleTag(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify User able to Reset single Tag.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Last Media Week" };
                summaryTags.VerifySummaryTags(summary);
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaSectionOnScreen();
                searchPage.selectSpecificMediaTypeOnSearchOptions("Magazine");
                searchPage.clickButtonOnSearchOptions("Apply");
                Array.Resize(ref summary, summary.Length + 1);
                summary[summary.Length - 1] = "Magazine";
                summaryTags.VerifySummaryTags(summary, false);
                summaryTags.resetSingleSummaryTag("Magazine");
                string[] newSummary = { "Search Options", "Last Media Week" };
                summaryTags.VerifySummaryTags(newSummary);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_23_VerifyClickOnSpecificSummaryTagFromAllTagsOnlySpecificFieldComponentShouldBeOpenInSearchOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009_23-Verify Click on specific summary tag from all Tags Only specific field component should be open in Search options.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Last Media Week" };
                summaryTags.VerifySummaryTags(summary);
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaSectionOnScreen();
                searchPage.selectSpecificMediaTypeOnSearchOptions("Magazine");
                searchPage.clickButtonOnSearchOptions("Apply");
                Array.Resize(ref summary, summary.Length + 1);
                summary[summary.Length - 1] = "Magazine";
                summaryTags.VerifySummaryTags(summary, false);
                summaryTags.clickOnSingleSummaryTag("Magazine");
                searchPage.VerifyNewMediaSectionOnScreen();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC009_23");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_11_VerifySummaryTagShouldBeDisplayedInSummaryTagListForNewlyCreatedSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010_11-Verify Summary tag should be displayed in summary tag list for newly created search.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Last Media Week" };
                summaryTags.VerifySummaryTags(summary);
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand Canada");
                summaryTags.VerifySummaryTags(new string[]{ searchName});
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC010_11");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyWhileUserHaveAppliedSavedSearchThenItShouldNotShowResetIconOnTags(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify While user have applied Saved search then it should not show 'Reset' icon on Tags.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Last Media Week" };
                summaryTags.VerifySummaryTags(summary);
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand Canada");
                summaryTags.VerifySummaryTags(new string[] { searchName });
                summaryTags.resetSingleSummaryTag("Last Media Week");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC012");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyIfSavedSearchIsAppliedAndAfterUserFilterAnyFieldsThenOnClickingTheResetIconFromTagsThenPreviousSavedSearchFilterTagsShouldBeDisplayed(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify If Saved search is applied and after user filter any fields then on click the Reset icon from tags then previous saved search filter tags should be displayed.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Running", "Last 7 Days" };
                summaryTags.VerifySummaryTags(summary);
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                string[] newSummary = summaryTags.captureSummaryTagsFromDashboard();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Television");
                searchPage.clickButtonOnSearchOptions("Apply");
                Array.Resize(ref summary, summary.Length + 1);
                summaryTags.VerifySummaryTags(new string[] { "Television" }, false);
                summaryTags.resetSingleSummaryTag("Television");
                summaryTags.VerifySummaryTags(newSummary);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyIfSavedSearchIsAppliedAndAfterUserFilterAnyFieldsAndSaveThatSearchThenResetIconShouldBeRemovedFromTags(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify If Saved search is applied after that user filter any fields from search modal and Save that search then Reset icon should be removed from Tags.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = { "Search Options", "Running", "Last 7 Days" };
                summaryTags.VerifySummaryTags(summary);
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                string[] newSummary = summaryTags.captureSummaryTagsFromDashboard();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Television");
                searchPage.clickButtonOnSearchOptions("Apply");
                summaryTags.VerifySummaryTags(new string[]{ "Television" }, false);
                secondaryButtons.clickOnSecondaryButtons("Save");
                secondaryButtons.VerifySaveNewSearchPopupAndClickButton("Save");
                summaryTags.resetSingleSummaryTag("Television");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_VerifyThatIfUserChangesTheSearchThenTagsShouldBeChangedAccordingToSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify, If user change the search then tags should be changed according to Search");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = summaryTags.captureSummaryTagsFromDashboard();
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                string[] newSummary2 = summaryTags.captureSummaryTagsFromDashboard();
                fieldOptions.compareListOfItemsInOrder(summary, newSummary2, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC015");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_VerifyThatClickingOnCloseIconFromSavedSearchTagThatSearchShouldBeRemovedAndDefaultSearchShouldBeDisplayed(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016-Verify, Click on 'close' icon from Saved search tag that search should be removed and default search should be displayed");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                string[] summary = summaryTags.captureSummaryTagsFromDashboard();
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                string[] newSummary1 = summaryTags.captureSummaryTagsFromDashboard();
                summaryTags.removeSearchNameSummaryTag(searchName);
                string[] newSummary2 = summaryTags.captureSummaryTagsFromDashboard();
                fieldOptions.compareListOfItemsInOrder(summary, newSummary2);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC016");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC017_VerifyThatClickingOnResetButtonInTheActionButtonsShouldResetAllTags(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017-Verify, Clicking on Reset button in the Action buttons should reset all tags");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                string[] newSummary1 = summaryTags.captureSummaryTagsFromDashboard();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Online Video");
                searchPage.clickButtonOnSearchOptions("Apply");
                string[] newSummary2 = summaryTags.captureSummaryTagsFromDashboard();
                fieldOptions.compareListOfItemsInOrder(newSummary1, newSummary2, false);
                secondaryButtons.clickOnSecondaryButtons("Reset");
                string[] newSummary3 = summaryTags.captureSummaryTagsFromDashboard();
                fieldOptions.compareListOfItemsInOrder(newSummary1, newSummary3);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC017");
                throw;
            }
            driver.Quit();
        }

        ///TC018 is failing as deleting applied saved search does not lead to default saved search but to untitled search instead  

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_VerifyThatIfUserDeletesCurrentSavedSearchThenDefaultSearchShouldBeDisplayed(String Bname)
        {
            TestFixtureSetUp(Bname, "TC018-Verify, If user delete current saved search then default search should be displayed");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                string[] newSummary1 = summaryTags.captureSummaryTagsFromDashboard();
                secondaryButtons.clickOnSecondaryButtons("Default");
                searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                string[] newSummary2 = summaryTags.captureSummaryTagsFromDashboard();
                fieldOptions.compareListOfItemsInOrder(newSummary1, newSummary2, false);
                secondaryButtons.clickOnSecondaryButtons("Delete");
                string[] newSummary3 = summaryTags.captureSummaryTagsFromDashboard();
                fieldOptions.compareListOfItemsInOrder(newSummary1, newSummary3);
                secondaryButtons.clickOnSecondaryButtons("Default");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC018");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC019_VerifyThatAllTagsShouldBeDisplayedWhileUserLoginAndDefaultSearchIsApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC019-Verify, All tags should be displayed while user login and 'Default Search' is applied");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                string[] newSummary1 = summaryTags.captureSummaryTagsFromDashboard();
                secondaryButtons.clickOnSecondaryButtons("Default");

                homePage.selectOptionFromSideNavigationBar("Settings");
                loginPage.signOutOfApplication();
                loginPage.navigateToLoginPage().VerifyPasswordScreenOnLoginPage();
                string dataFromSheet = Common.DirectoryPath + ConfigurationManager.AppSettings["DataSheetDir"] + "\\Login.xlsx";
                string[] Password = Spreadsheet.GetMultipleValueOfField(dataFromSheet, "Password", "Valid");
                string password = Password[0].ToString();
                driver._type("id", "password", password);
                Results.WriteStatus(test, "Pass", "Information Inputed successfully.<b> Password : " + password);
                loginPage.clickButtonOnLoginPage("Sign in");

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();

                string[] newSummary2 = summaryTags.captureSummaryTagsFromDashboard();
                fieldOptions.compareListOfItemsInOrder(newSummary1, newSummary2);
                secondaryButtons.clickOnSecondaryButtons("Default");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC019");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC020_VerifyThatWhenUserMakesSpecificSelectionInOneReportAndSwitchToOtherReportWithSameCriteriaIdThenTheModalShouldNotBeSharedAndShouldBeUnique(String Bname)
        {
            TestFixtureSetUp(Bname, "TC020-Verify when user makes specific selection in one report and switch to other report with same criteria id, the modal should not be shared and should be unique");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.VerifyNewDateRangeSectionOnScreen("Last Year");
                searchPage.selectNewDateRangeOptionFromSection();
                searchPage.clickButtonOnSearchOptions("Apply");
                string[] newSummary1 = summaryTags.captureSummaryTagsFromDashboard();
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                string[] newSummary2 = summaryTags.captureSummaryTagsFromDashboard();
                fieldOptions.compareListOfItemsInOrder(newSummary1, newSummary2, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC020");
                throw;
            }
            driver.Quit();
        }

        ///TC021 is failing as going back to original report, the summary tags are of default and not previously selected 

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC021_VerifyThatWhenUserMakesSpecificSelectionInOneReport_SwitchToOtherReportAndAgainSwitchBackToOriginalReportThenTheModalShouldBeSameAsPreviouslySelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC021-Verify when user makes specific selection in one report, switch to other report and again switch back to the original report the modal should be same as previous selected");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.VerifyNewDateRangeSectionOnScreen("Last Year");
                searchPage.selectNewDateRangeOptionFromSection();
                searchPage.clickButtonOnSearchOptions("Apply");
                string[] newSummary1 = summaryTags.captureSummaryTagsFromDashboard();
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                string[] newSummary2 = summaryTags.captureSummaryTagsFromDashboard();
                fieldOptions.compareListOfItemsInOrder(newSummary1, newSummary2, false);
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                summaryTags.VerifySummaryTags(newSummary1);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC021");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC022_VerifyAdStatusAndDateRangeFieldSummaryTagsShouldBeAlwaysDefaultDisplayed(String Bname)
        {
            TestFixtureSetUp(Bname, "TC022-Verify Ad Status and Date Range Field Summary Tags should be always default displayed");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                summaryTags.VerifyThatAdStatusAndDateRangeFieldSummaryTagsShouldBeDefaultForUntitledSearch();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC022");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC024_VerifyTheResetIconFunctionalityOnTheSummaryTagsOfTheModifiedSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC024-Verify the reset icon functionality on the summary tags of the modified search");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.newVerifyHomePage();
                string searchName = homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                string[] summary = summaryTags.captureSummaryTagsFromDashboard();
                summaryTags.clickOnSingleSummaryTag(summary[summary.Length - 1]);
                string newSummaryTag = searchPage.randomlySelectSearchFilterFromOpenSearchTab();
                searchPage.clickButtonOnSearchOptions("Apply");
                summaryTags.resetSingleSummaryTag(newSummaryTag);
                string[] newSummary = summaryTags.captureSummaryTagsFromDashboard();
                fieldOptions.compareListOfItemsInOrder(summary, newSummary);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite05_Reskin_SummaryTags_TC024");
                throw;
            }
            driver.Quit();
        }



        #endregion
    }
}
