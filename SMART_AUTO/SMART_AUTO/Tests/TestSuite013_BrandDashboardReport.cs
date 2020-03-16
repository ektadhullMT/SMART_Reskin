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
    public class TestSuite013_BrandDashboardReport : Base
    {
        string userName = "QA Testing - Brand";

        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        Search searchPage;
        PromoDashboard promoDashboard;
        BrandMonthlyReport brandMonthlyReport;
        Schedule schedule;
        BrandDashboard brandDashboard;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite013_BrandDashboardReport).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite013_BrandDashboardReport).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            promoDashboard = new PromoDashboard(driver, test);
            brandMonthlyReport = new BrandMonthlyReport(driver, test);
            schedule = new Schedule(driver, test);
            brandDashboard = new BrandDashboard(driver, test);

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

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifySearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify Search functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                searchPage.selectDateRangeOptionFromSection().selectMediaCheckboxOptionFromSection();
                searchPage.clickButtonOnSearchScreen("Save As");
                searchPage.VerifySaveAsSectionAfterClickingOnSaveAsButton().enterSearchValueOnSearchScreen();
                searchPage.clickButtonOnSearchScreen("Save!").clickButtonOnSearchScreen("Apply Search");
                brandDashboard.VerifyBrandDashboardScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifySaveSearchCardInSavedSearchScreen(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify Save search Card in Saved search screen.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC003");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyUserAbleToResetSavedSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify User able to Reset saved search.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                searchPage.selectDateRangeOptionFromSection().selectMediaCheckboxOptionFromSection();
                searchPage.clickButtonOnSearchScreen("Reset").VerifyResetChangesMessageOnScreen(true);
                searchPage.VerifyAppliedSearchFieldInChartDetailsSection("None Selected");
                searchPage.VerifyFieldsRefreshIconDisableOnSummaryDetailSection();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyDeleteSavedSearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify Delete Saved Search Functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false);
                searchPage.clickDeleteButtonForSavedSearchRecordFromListAndVerifyMessage(true, false);
                searchPage.clickDeleteButtonForSavedSearchRecordFromListAndVerifyMessage(true, true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyEditSavedSearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify Edit saved Search functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                searchPage.VerifySavedSearchesSectionOnScreen(false).clickButtonOnSearchScreen("Edit Search");
                searchPage.VerifyMySearchScreen("Brand Monthly");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyAfterClickingApplySearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify After Clicking Apply search.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                homePage.clickMenuIconFromScreen("Search");
                searchPage.VerifyMySearchScreen("Brand Canada");
                schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen(true);
                brandDashboard.VerifyBrandDashboardScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyFilterBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify Filter bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandMonthlyReport.VerifyFilterBarSectionOnScreen(true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC008");
                throw;
            }
            driver.Quit();
        }
        
        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyDateRangeFieldInFilterBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify Date Range Field in Filter bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandMonthlyReport.VerifyFilterBarSectionOnScreen(true);
                string[] options = { "Custom Range", "Last Month", "Last 2 Months", "Last 3 Months", "Last 6 Months", "Year To Date", "Last Year" };
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("Days", "", options);
                brandMonthlyReport.VerifyFromAndToMonthSectionOnScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyAllMediaTypeFieldDropdownForFilterBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify 'All Media Type' field drop down for Filter bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("Media Types");
                promoDashboard.VerifyFilterSectionWithCheckbox("Media Types");
                string mediaName = brandMonthlyReport.VerifyFilterSectionWithCheckboxAndSelectOption("Media Types");
                brandMonthlyReport.VerifySelectedRecordsOnCarouselSection(mediaName);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyAllAdvertiserProductsFieldDropdownForFilterBar(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify 'All Advertiser Products' field drop down for Filter bar.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("Advertiser Products");
                promoDashboard.VerifyFilterSectionOnScreen("All Advertiser Products", false);
                promoDashboard.clickButtonOnFilterSectionOnScreen("Select Displayed");
                promoDashboard.VerifyFilterListRecordsSelectedOrNotOnFilterSection(true);
                promoDashboard.enterKeywordToSerachIntoFilterTextBox(5);
                promoDashboard.clearKeywordFromSearchTextBox();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyResetAllButtonWhenNoFilterIsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify 'Reset All' button when no Filter is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC012");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyResetAllButtonWhenFilterIsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify 'Reset All' button when Filter is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.VerifyFilterBarSectionOnScreen(false);
                promoDashboard.VerifyAndClickResetAllButtonOnFilterSection(false);
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("Days", "Last 3 Months");
                promoDashboard.VerifyAndClickResetAllButtonOnFilterSection(true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyProductThumbnailAndProductDetailsInProductCarousel(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify Product thumbnail and product details in Product carousel.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandMonthlyReport.VerifyProductThumbnailInProductCarousel(true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC014");
                throw;
            }

            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_Verify_ViewAd_FunctionalityForIndividualProductThumbnail(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify 'View Ad' functionality for Individual Product Thumbnail.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandMonthlyReport.VerifyProductThumbnailInProductCarousel(true);
                brandMonthlyReport.clickButtonLinkForProductOnCarouselSection("View Ad");
                string[] tabName = { "View Ad", "Markets", "More Details", "Download" };
                brandMonthlyReport.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "View Ad");
                brandMonthlyReport.VerifyViewAdScreenOnPopupWindow().clickButtonOnPopupWindow("Close");
                brandMonthlyReport.VerifyProductDetailPopupWindowOnDashboardPage(null, "", false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC015");
                throw;
            }

            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_Verify_Markets_FunctionalityForIndividualProductThumbnail(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016-Verify 'Markets' functionality for Individual Product Thumbnail.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandMonthlyReport.VerifyProductThumbnailInProductCarousel(true);
                brandMonthlyReport.clickButtonLinkForProductOnCarouselSection("Markets");
                string[] tabName = { "View Ad", "Markets", "More Details", "Download" };
                brandMonthlyReport.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "Markets");
                brandMonthlyReport.VerifyMarketsTabOnPopupWindow().clickOnGridHeaderToVerifySortingFunctionality();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC016");
                throw;
            }

            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC017_Verify_Details_FunctionalityForIndividualProductThumbnail(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017-Verify 'Details' functionality for Individual Product Thumbnail.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandMonthlyReport.VerifyProductThumbnailInProductCarousel(true);
                brandMonthlyReport.clickButtonLinkForProductOnCarouselSection("Details");
                string[] tabName = { "View Ad", "Markets", "More Details", "Download" };
                brandMonthlyReport.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "More Details");
                brandMonthlyReport.VerifyMoreDetailsScreenOnPopupWindow();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC017");
                throw;
            }

            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_VerifyPreviousAndNextPageArrowForCarousel(String Bname)
        {
            TestFixtureSetUp(Bname, "TC018-Verify Previous and Next Page arrow for carousel.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandMonthlyReport.VerifyNavigationArrowForCarousel("Next", true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC018");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC019_VerifyCarouselSortingWhen_Spend_And_FirstRunDate_IsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC019-Verify Carousel sorting when 'Spend' and 'First Run Date' is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandMonthlyReport.VerifyProductThumbnailInProductCarousel(true);
                brandMonthlyReport.clickCarouselRadioOptionAndVerifyProduct("Spend");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC019");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC020_Verify_CountOfCreativesRunningByAdvertiseAndMediaType_ChartDetails(String Bname)
        {
            TestFixtureSetUp(Bname, "TC020-Verify 'Count of Creatives Running by Advertiser and Media Type' chart details.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandDashboard.VerifyCountOfCreativesRunningByAdvertiserAndMediaTypeChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC020");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC021_VerifyUserAbleToExpandChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC021-Verify User able to expand chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandDashboard.VerifyCountOfCreativesRunningByAdvertiserAndMediaTypeChart();
                brandDashboard.clickIconButtonOnScreenForChart("Count of Creatives Running by Advertiser and Media Type", "Expand");
                brandDashboard.VerifyFullScreenOf_CountOfCreativesRunningByAdvertiserAndMediaType_Chart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC021");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC022_VerifyGoBackButtonFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC022-Verify Go back button functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandDashboard.VerifyCountOfCreativesRunningByAdvertiserAndMediaTypeChart();
                brandDashboard.clickIconButtonOnScreenForChart("Count of Creatives Running by Advertiser and Media Type", "Expand");
                brandDashboard.VerifyFullScreenOf_CountOfCreativesRunningByAdvertiserAndMediaType_Chart();
                brandDashboard.clickIconButtonOnScreenForChart("Full Screen", "Go Back");
                brandDashboard.VerifyCountOfCreativesRunningByAdvertiserAndMediaTypeChart();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC022");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC023_VerifyUserAbleToDownloadChartInDifferentFormat(String Bname)
        {
            TestFixtureSetUp(Bname, "TC023-Verify User able to download chart in different format.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandDashboard.VerifyCountOfCreativesRunningByAdvertiserAndMediaTypeChart();
                brandDashboard.clickIconButtonOnScreenForChart("Count of Creatives Running by Advertiser and Media Type", "Download");
                brandDashboard.VerifyDownloadPopupWindowAndClickOnOption("Download PNG");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("MediaType", "*.png");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC023");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC024_VerifyUserAbleToCreateNewSchedule(String Bname)
        {
            TestFixtureSetUp(Bname, "TC024-Verify User able to create new schedule.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                homePage.clickMenuIconFromScreen("Search");
                string searchTitle = schedule.createNewSearchOrClickSavedSearchToApplySearchOnScreen();
                brandDashboard.clickIconButtonOnScreenForChart("Count of Creatives Running by Advertiser and Media Type", "Schedule");
                brandDashboard.VerifyScheduleWindow(searchTitle);
                schedule.clickButtonOnScreen("Create Scheduled Export");
                schedule.VerifyScheduleMessageOnScreen("Successfully created a scheduled export for");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC024");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC025_VerifyChartShouldBeUpdatedAccordingToUserSelectDeselectTheLegends(String Bname)
        {
            TestFixtureSetUp(Bname, "TC025-Verify Chart should be updated according to user select/deselect the legends.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandDashboard.VerifyCountOfCreativesRunningByAdvertiserAndMediaTypeChart();
                brandDashboard.VerifyLegendToClickAndVerifyForChart("Count of Creatives Running by Advertiser and Media Type");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC025");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC026_VerifyPopUpDetailsByHoveringMouseOnBarChart(String Bname)
        {
            TestFixtureSetUp(Bname, "TC026-Verify Pop-up details by hovering mouse on Bar chart.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                brandDashboard.hoverMouseOnBarChartAndGetTheTooltipRecords("Count of Creatives Running by Advertiser and Media Type");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC026");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC027_Verify_CountOfCreativesRunningByCompetitor_PieChartDrillDownFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC027-Verify 'Count of Creatives Running by Competitor' Pie chart drill down functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                string divID= brandDashboard.VerifyChartDetailsOnScreem("Count of Creatives Running by Competitor");
                brandDashboard.clickOnPieChartAndVerifyDrillDownLevel(divID);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC027");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC028_VerifyUserAbleToChange_HiddenFields_OrderByDraggingItToUpOrDownSide(String Bname)
        {
            TestFixtureSetUp(Bname, "TC028-Verify User able to change 'Hidden Fields' order by dragging it to up or down side.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.clickButtonOnViewActionSection("Field Options");
                promoDashboard.VerifyFieldsOptionsSectionOnDashboardScreen().VerifyVisibleFieldsInFieldsOptionsSection();
                promoDashboard.clickFieldIconAndVerifyFieldNameOnFieldsOptions(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC028");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC029_VerifyThatDraggingHiddenFieldsToVisibleFieldsAndViceVersa(String Bname)
        {
            TestFixtureSetUp(Bname, "TC029-Verify that dragging Hidden Fields to Visible Fields and vice versa.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.clickButtonOnViewActionSection("Field Options");
                promoDashboard.VerifyFieldsOptionsSectionOnDashboardScreen();
                brandMonthlyReport.dragAndDropFieldFromFieldOptionsSection();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC029");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC030_Verify_ResetFields_FunctionalityInVisibleFieldsSection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC030-Verify 'Reset Fields' functionality in Visible Fields section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.clickButtonOnViewActionSection("Field Options");
                promoDashboard.VerifyFieldsOptionsSectionOnDashboardScreen();
                brandMonthlyReport.dragAndDropFieldFromFieldOptionsSection().VerifyAndClickButtonFromFieldOptionsSection("Reset Fields");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC030");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC031_VerifySortingFromVisibleFieldOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC031-Verify Sorting from Visible Field options.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.clickButtonOnViewActionSection("Field Options");
                promoDashboard.VerifyFieldsOptionsSectionOnDashboardScreen();
                promoDashboard.clickOnSignForAnyFieldOnVisibleFieldsSection(true).clickOnSignForAnyFieldOnVisibleFieldsSection(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC031");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC032_VerifyGridWhen_TableView_IsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC032-Verify grid when 'Table View' is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword(1);

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("QA Testing - Promo");
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                promoDashboard.VerifyPromoDashboardScreen().VerifyActionButtonOnViewSection();
                promoDashboard.clickDetailViewButtonAndVerifyOptionsToClick("Table");
                promoDashboard.VerifyTableViewSectionOnScreen().VerifyGridSectionForTableView();
                promoDashboard.VerifyPaginationPanelForViewSection("Table View");
                promoDashboard.clickPageNumberAndIconFromGrid();
                promoDashboard.clickPageNumberAndIconFromGrid("Last").clickPageNumberAndIconFromGrid("First");
                promoDashboard.clickPageNumberAndIconFromGrid("Prev").clickPageNumberAndIconFromGrid("Next");
                promoDashboard.VerifyThumbnailSectionOnScreen();
                promoDashboard.clickButtonOnViewSection("Ad Image", "Table View");
                string[] tabName = { "View Ad", "Map", "Stores", "More Details" };
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "View Ad");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickButtonOnViewSection("View Ad", "Table View");
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "View Ad");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickButtonOnViewSection("Stores", "Table View");
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "Stores");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickButtonOnViewSection("Details", "Table View");
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "More Details");
                promoDashboard.clickButtonOnPopupWindow("Close");
                
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC032");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC033_VerifyGridWhenDetailsViewIsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC033-Verify Grid when 'Details View' is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("QA Testing - Promo");
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                promoDashboard.VerifyPromoDashboardScreen().VerifyActionButtonOnViewSection();
                promoDashboard.clickDetailViewButtonAndVerifyOptionsToClick("Details");
                promoDashboard.VerifyDetailsViewSectionOnScreen();
                promoDashboard.VerifyPaginationPanelForViewSection("Details View");

                promoDashboard.clickButtonOnViewSection("Ad Image", "Details View");
                string[] tabName = { "View Ad", "Map", "Stores", "More Details" };
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "View Ad");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickButtonOnViewSection("View Ad", "Details View");
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "View Ad");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickButtonOnViewSection("Stores", "Details View");
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "Stores");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickButtonOnViewSection("Details", "Details View");
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "More Details");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickButtonOnViewSection("Map", "Details View");
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "Map");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickPageNumberAndIconFromGrid();
                promoDashboard.clickPageNumberAndIconFromGrid("Last").clickPageNumberAndIconFromGrid("First");
                promoDashboard.clickPageNumberAndIconFromGrid("Prev").clickPageNumberAndIconFromGrid("Next");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC033");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC034_VerifyGridWhenThumbnailViewIsSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC034-Verify Grid when 'Thumbnail View' is selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("QA Testing - Promo");
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                promoDashboard.VerifyPromoDashboardScreen().VerifyActionButtonOnViewSection();
                promoDashboard.clickDetailViewButtonAndVerifyOptionsToClick("Thumbnail");
                promoDashboard.VerifyThumbnailViewSectionOnScreen();
                promoDashboard.VerifyPaginationPanelForViewSection("Thumbnail View");

                promoDashboard.clickButtonOnViewSection("Ad Image", "Thumbnail View");
                string[] tabName = { "View Ad", "Map", "Stores", "More Details" };
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "View Ad");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickButtonOnViewSection("View Ad", "Thumbnail View");
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "View Ad");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickButtonOnViewSection("Stores", "Thumbnail View");
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "Stores");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickButtonOnViewSection("Details", "Thumbnail View");
                promoDashboard.VerifyProductDetailPopupWindowOnDashboardPage(tabName, "More Details");
                promoDashboard.clickButtonOnPopupWindow("Close");

                promoDashboard.clickPageNumberAndIconFromGrid();
                promoDashboard.clickPageNumberAndIconFromGrid("Last").clickPageNumberAndIconFromGrid("First");
                promoDashboard.clickPageNumberAndIconFromGrid("Prev").clickPageNumberAndIconFromGrid("Next");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC034");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC035_VerifyViewSelectedFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC035-Verify 'view Selected' functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.VerifyButtonDisableOrNotOnScreen("View Selected", true);
                promoDashboard.selectRecordFromViewSection().clickButtonOnViewActionSection("View Selected");
                promoDashboard.VerifyButtonDisableOrNotOnScreen("View Selected", false);
                promoDashboard.VerifyViewSelectedButtonCheckedOrNotOnScreen(true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC035");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC036_Verify_ResetSelected_ButtonWhenRecordsAreSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC036-Verify 'Reset Selected' button when records are selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.selectRecordFromViewSection();
                promoDashboard.VerifyButtonDisableOrNotOnScreen("Reset Selected", false);
                promoDashboard.clickButtonOnViewActionSection("Reset Selected");
                promoDashboard.VerifyViewSelectedButtonCheckedOrNotOnScreen(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC036");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC037_Verify_ResetSelected_ButtonWhenRecordsAreNotSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC037-Verify 'Reset Selected' button when records are not selected.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.VerifyButtonDisableOrNotOnScreen("Reset Selected", true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC037");
                throw;
            }
            driver.Quit();
        }

        // Want to Update Testcase
        //[Test]
        //[TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC038_VerifyThatLabelShouldChangeTo_ViewAll_AfterClickingOnViewSelectedButton(String Bname)
        {
            TestFixtureSetUp(Bname, "TC038-Verify that label should change to 'View all' after clicking on View Selected button.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC038");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC039_VerifyExportSelectedFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC039-Verify 'Export Selected' Functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.selectRecordFromViewSection().VerifyButtonDisableOrNotOnScreen("Export Selected", false);
                promoDashboard.clickButtonOnViewActionSection("Export Selected").VerifyExportAllSectionOnDashboardScreen();
                promoDashboard.VerifyOrClickExportAllSectionInDetailOnScreen("Data Reports");
                promoDashboard.VerifyOrClickExportAllSectionInDetailOnScreen("Data Reports", "Download");
                promoDashboard.VerifyFileDownloadedOrNotOnScreen("qatest-db", "*.xlsx");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC039");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC040_VerifyExportSelectedFunctionalityForDataReportsSection(String Bname)
        {
            TestFixtureSetUp(Bname, "TC040-Verify 'Export Selected' Functionality for Data Reports Section.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.selectRecordFromViewSection().VerifyButtonDisableOrNotOnScreen("Export Selected", false);
                promoDashboard.clickButtonOnViewActionSection("Export Selected").VerifyExportAllSectionOnDashboardScreen();
                promoDashboard.VerifyOrClickExportAllSectionInDetailOnScreen("Data Reports");
                promoDashboard.VerifyOrClickExportAllSectionInDetailOnScreen("Data Reports", "Download");
                promoDashboard.VerifyFileDownloadedOrNotOnScreen("qatest-db", "*.xlsx");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC040");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC041_VerifyExportSelectedFunctionalityForPowerPointReports(String Bname)
        {
            TestFixtureSetUp(Bname, "TC041-Verify 'Export Selected' Functionality For Power Point Reports.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.selectRecordFromViewSection().VerifyButtonDisableOrNotOnScreen("Export Selected", false);
                promoDashboard.clickButtonOnViewActionSection("Export Selected").VerifyExportAllSectionOnDashboardScreen();
                promoDashboard.VerifyOrClickExportAllSectionInDetailOnScreen("Power Point Reports");
                promoDashboard.VerifyOrClickExportAllSectionInDetailOnScreen("Power Point Reports", "Download");
                promoDashboard.VerifyFileDownloadedOrNotOnScreen("qatest-db", "*.pptx");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC041");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC042_VerifyExportSelectedFunctionalityForAssetReports(String Bname)
        {
            TestFixtureSetUp(Bname, "TC042-Verify 'Export Selected' Functionality For Asset Reports.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.selectRecordFromViewSection().VerifyButtonDisableOrNotOnScreen("Export Selected", false);
                promoDashboard.clickButtonOnViewActionSection("Export Selected").VerifyExportAllSectionOnDashboardScreen();
                promoDashboard.VerifyOrClickExportAllSectionInDetailOnScreen("Asset Downloads");
                promoDashboard.VerifyOrClickExportAllSectionInDetailOnScreen("Asset Downloads", "Download");
                promoDashboard.VerifyFileDownloadedOrNotOnScreen("qatest-db", "*.zip");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC042");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC043_VerifyDataReportsWhenTotalRecordsAreMoreThan5000(String Bname)
        {
            TestFixtureSetUp(Bname, "TC043-Verify Data Reports when total records are more than 5000.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();                
                promoDashboard.clickButtonOnViewActionSection("Export All").VerifyExportAllSectionOnDashboardScreen();

                promoDashboard.VerifyOrClickExportAllSectionInDetailOnScreen("Data Reports");
                promoDashboard.VerifyTiooltipFunctionalityForReportsSection("Data Reports", "Email", "Send results via email");
                
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC043");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC044_VerifyPowerPointReportsWhenTotalRecordsAreMoreThan1000(String Bname)
        {
            TestFixtureSetUp(Bname, "TC044-Verify Power Point Reports when total records are more than 1000.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.clickButtonOnViewActionSection("Export All").VerifyExportAllSectionOnDashboardScreen();

                promoDashboard.VerifyOrClickExportAllSectionInDetailOnScreen("Power Point Reports");
                promoDashboard.VerifyTiooltipFunctionalityForReportsSection("Power Point Reports", "Email", "Send results via email");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC044");
                throw;
            }
            driver.Quit();
        }
        
        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC045_VerifyAssetDownloadsWhenTotalRecordsAreMoreThan250(String Bname)
        {
            TestFixtureSetUp(Bname, "TC045-Verify  Asset Downloads when total records are more than 250.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Dashboard");

                brandDashboard.VerifyBrandDashboardScreen();
                promoDashboard.clickButtonOnViewActionSection("Export All").VerifyExportAllSectionOnDashboardScreen();

                promoDashboard.VerifyOrClickExportAllSectionInDetailOnScreen("Asset Downloads");
                promoDashboard.VerifyTiooltipFunctionalityForReportsSection("Asset Downloads", "Email", "Send results via email");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite013_BrandDashboardReport_TC045");
                throw;
            }
            driver.Quit();
        }

        #endregion
    }
}