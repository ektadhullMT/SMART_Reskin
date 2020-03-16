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
    public class TestSuite06_Reskin_ViewAdPopup : Base
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
        ViewAdPopup viewAdPopup;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite06_Reskin_ViewAdPopup).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite06_Reskin_ViewAdPopup).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            userProfile = new UserProfile(driver, test);
            carousels = new Carousels(driver, test);
            brandDashboard = new BrandDashboard(driver, test);
            viewAdPopup = new ViewAdPopup(driver, test);

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
        public void TC001_VerifyThatClickingOnTheViewAd_MarketsOrDetailsButtonInAgGridDetailOrThumbnailViewShouldOpenAModalWindowWithViewAd_Markets_MoreDetailsAndDownloadTab(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify on clicking the View Ad, Markets or Details button in AgGrid's detail or thumbnail view should open a modal window with View ad, Markets , More details & Download tab.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("View Ad");
                carousels.VerifyViewAdFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);

                viewAdPopup.clickOnButtonOfResultsCard("Details");
                carousels.VerifyDetailsFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyDetailsFunctionality(false);

                viewAdPopup.clickOnButtonOfResultsCard("Download");
                viewAdPopup.VerifyDownloadsFunctionality(true);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                viewAdPopup.VerifyDownloadsFunctionality(false);

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("View Ad");
                carousels.VerifyViewAdFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);

                viewAdPopup.clickOnButtonOfResultsCard("Details");
                carousels.VerifyDetailsFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyDetailsFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyOnClickingTheViewAd_OccurrencesOrDetailsButtonInAgGridDetailAndThumbnailViewShouldOpenAModalWindowWithViewAd_Occurrences_MoreDetailsDownloadTab(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify on clicking the View Ad , Occurrences or Details button in AgGrid's detail and thumbnail view should open a modal window with View ad, Occurrences , More details Download tab.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();

                carousels.VerifyCarousels(true);
                carousels.clickButtonOnCarousel("View Ad");
                carousels.VerifyViewAdFunctionality(true, true, true);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);

                viewAdPopup.VerifyOccurrencesFunctionality();
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                viewAdPopup.VerifyOccurrencesFunctionality(false);

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid(true);

                viewAdPopup.clickOnButtonOfResultsCard("Details");
                carousels.VerifyDetailsFunctionality(true, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifyViewAdFunctionalityInAgGridDetailAndThumbnailViewAndVerifyMagnifierFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify View Ad Functionality in AgGrid's detail and thumbnail view & Verify Magnifier functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Print");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("View Ad");
                carousels.VerifyViewAdFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("View Ad");
                carousels.VerifyViewAdFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Online Video");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("View Ad");
                carousels.VerifyViewAdFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("View Ad");
                carousels.VerifyViewAdFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Radio");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("View Ad");
                carousels.VerifyViewAdFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("View Ad");
                carousels.VerifyViewAdFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC003");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyMarketsFunctionalityOnTheCarousel_InAgGridDetailAndThumbnailViewAndVerifyMagnifierFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify Markets Functionality on the carousel, AgGrid's detail and thumbnail view.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                carousels.VerifyCarousels();
                carousels.VerifyMarketsFunctionality();
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyUserAbleToPerformSortingOnMarketDetailsGrid(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify User able to perform sorting on Market details grid.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                homePage.VerifyAndModifySearchOptions();
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Online Video");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                string column = viewAdPopup.clickOnSortingIconOnMarketsView();
                viewAdPopup.VerifySortingFunctionalityOnMarketsView(column);
                column = viewAdPopup.clickOnSortingIconOnMarketsView(true);
                viewAdPopup.VerifySortingFunctionalityOnMarketsView(column, true);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                column = viewAdPopup.clickOnSortingIconOnMarketsView();
                viewAdPopup.VerifySortingFunctionalityOnMarketsView(column);
                column = viewAdPopup.clickOnSortingIconOnMarketsView(true);
                viewAdPopup.VerifySortingFunctionalityOnMarketsView(column, true);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyTheFilterFunctionalityOfTheMarketDetailGrid(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify the Filter functionality of the Market detail grid.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Online Video");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                string[] columnAndValues = viewAdPopup.enterFilterValueInMarketViewTableColumns("Occurrences");
                viewAdPopup.VerifyFilterOnMarketViewsTableColumn(columnAndValues);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyThatUserIsAbleToFilterRecordUsing_Equals_Keyword(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify User able to filter record using 'Equals' keyword.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Online Video");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                string[] columnAndValues = viewAdPopup.enterFilterValueInMarketViewTableColumns("Spend", "Equals");
                viewAdPopup.VerifyFilterOnMarketViewsTableColumn(columnAndValues);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyThatUserIsAbleToFilterRecordUsing_NotEquals_Keyword(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify User able to filter record using 'Not Equal' keyword.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Online Video");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                string[] columnAndValues = viewAdPopup.enterFilterValueInMarketViewTableColumns("Spend", "Not equal");
                viewAdPopup.VerifyFilterOnMarketViewsTableColumn(columnAndValues);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyThatUserIsAbleToFilterRecordUsing_LessThan_Keyword(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify User able to filter record using 'Less than' keyword.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Online Video");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                string[] columnAndValues = viewAdPopup.enterFilterValueInMarketViewTableColumns("Spend", "Less than");
                viewAdPopup.VerifyFilterOnMarketViewsTableColumn(columnAndValues);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyThatUserIsAbleToFilterRecordUsing_LessThanOrEquals_Keyword(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify User able to filter record using 'Less than or Equals' keyword.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Online Video");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                string[] columnAndValues = viewAdPopup.enterFilterValueInMarketViewTableColumns("Spend", "Less than or equals");
                viewAdPopup.VerifyFilterOnMarketViewsTableColumn(columnAndValues);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyThatUserIsAbleToFilterRecordUsing_GreaterThan_Keyword(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify User able to filter record using 'Greater than' keyword.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Online Video");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                string[] columnAndValues = viewAdPopup.enterFilterValueInMarketViewTableColumns("Spend", "Greater than");
                viewAdPopup.VerifyFilterOnMarketViewsTableColumn(columnAndValues);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyThatUserIsAbleToFilterRecordUsing_GreaterThanOrEquals_Keyword(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify User able to filter record using 'Greater than or Equals' keyword.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Online Video");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                string[] columnAndValues = viewAdPopup.enterFilterValueInMarketViewTableColumns("Spend", "Greater than or equals");
                viewAdPopup.VerifyFilterOnMarketViewsTableColumn(columnAndValues);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC012");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyThatUserIsAbleToFilterRecordUsing_InRange_Keyword(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify User able to filter record using 'In Range' keyword.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Online Video");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                string[] columnAndValues = viewAdPopup.enterFilterValueInMarketViewTableColumns("Spend", "In Range");
                viewAdPopup.VerifyFilterOnMarketViewsTableColumn(columnAndValues);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyDownloadGridOptionsFunctionalityInMarketTab(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify Download Grid options functionality in Market tab.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();
                viewAdPopup.clickOnButtonOfResultsCard("Details");
                carousels.VerifyDetailsFunctionality(true, false);
                string adCode = carousels.getAdCodeFromCarousel(false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyDetailsFunctionality(false);

                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                string[] dataGrid = viewAdPopup.captureDataFromMarketsViewGrid();
                viewAdPopup.clickOnButtonOfViewAdPopup("Download Grid");
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);

                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen("export", "*.csv");
                viewAdPopup.VerifyDataInExportedFileFromMarketsViewGrid(fileName, adCode, dataGrid);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_VerifyGridOptionsFunctionalityInMarketTab(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify Grid options functionality in Market tab.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();
                viewAdPopup.clickOnButtonOfResultsCard("Markets");
                carousels.VerifyMarketsFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Grid Options");
                viewAdPopup.VerifyGridOptionsOnMarketView();
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyMarketsFunctionality(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC015");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_VerifyDetailsFunctionalityonTheAgGridDetailAndThumbnailViewAndVerifyViewAdNaviagtionFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016-Verify Details Functionality on the AgGrid's detail and thumbnail view & Verify view ad navigation functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();
                viewAdPopup.clickOnButtonOfResultsCard("Details");
                carousels.VerifyDetailsFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Image");
                carousels.VerifyViewAdFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid();
                viewAdPopup.clickOnButtonOfResultsCard("Details");
                carousels.VerifyDetailsFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Image");
                carousels.VerifyViewAdFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC016");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC017_VerifyDownloadTabFunctionalityInAdModalWindow(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017-Verify Download tab functionality in Ad Modal Window.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Print");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();
                viewAdPopup.clickOnButtonOfResultsCard("Details");
                carousels.VerifyDetailsFunctionality(true, false);
                viewAdPopup.selectTabOnViewAdPopup("Download");
                viewAdPopup.VerifyDownloadsFunctionality();
                string[,] fileTyeCollection = viewAdPopup.clickToDownloadFilesFromDownloadsTabOnViewAsPopup();
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);

                for(int i = 0; i < fileTyeCollection.GetLength(0); i++)
                    brandDashboard.VerifyFileDownloadedOrNotOnScreen(fileTyeCollection[i, 0], "*." + fileTyeCollection[i, 1]);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC017");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_VerifyDownloadAssetsButtonFunctionalityInAdModalWindow(String Bname)
        {
            TestFixtureSetUp(Bname, "TC018-Verify Download Assets button Functionality on Ad pop-up/Ad Modal Window.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Print");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();
                viewAdPopup.clickOnButtonOfResultsCard("Details");
                carousels.VerifyDetailsFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Download Asset");
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);
                Thread.Sleep(60000);
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("qatest-db-ad", "*.zip");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC018");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC019_VerifyCloseButtonFunctionalityInAdModalWindow(String Bname)
        {
            TestFixtureSetUp(Bname, "TC019-Verify Close button Functionality on Ad pop-up/Ad Modal Window.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.VerifyAndModifySearchOptions();
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.VerifyNewMediaTypeSectionOnScreeninCFTDevelopment();
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Print");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();
                viewAdPopup.clickOnButtonOfResultsCard("Details");
                carousels.VerifyDetailsFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC019");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC020_VerifyTheOpenedAdModalWindowShouldLoadEvenAfterTheBrowserRefresh(String Bname)
        {
            TestFixtureSetUp(Bname, "TC020-Verify the opened Ad Modal Window should load even after the browser refresh.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();
                viewAdPopup.clickOnButtonOfResultsCard("Details");
                carousels.VerifyDetailsFunctionality(true, false);

                driver.Navigate().Refresh();

                carousels.VerifyDetailsFunctionality(true, false);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC020");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC021_VerifyOnClickingAnywhereExceptForTheViewAd_Markets_OccurrencesOrDetailsButtonShouldNotOpenTheAdModalWindow(String Bname)
        {
            TestFixtureSetUp(Bname, "TC021-Verify on Clicking anywhere except for the View Ad, Markets, Occurrences or Details button should not open the Ad Modal Window.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid();
                string adCode = carousels.selectRecordFromResults("Details");
                carousels.VerifyViewAdFunctionality(false);
                carousels.selectRecordFromResults("Details", false, adCode);

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid();
                carousels.selectRecordFromResults("Thumbnail");
                carousels.VerifyViewAdFunctionality(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite06_Reskin_ViewAdPopup_TC021");
                throw;
            }
            driver.Quit();
        }

        #endregion

    }
}
