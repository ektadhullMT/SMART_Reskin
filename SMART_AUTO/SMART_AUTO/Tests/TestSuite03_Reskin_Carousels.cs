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
    public class TestSuite03_Reskin_Carousels : Base
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

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite03_Reskin_Carousels).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite03_Reskin_Carousels).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            userProfile = new UserProfile(driver, test);
            carousels = new Carousels(driver, test);
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
        public void TC001_VerifyCarousel(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify Carousel.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePageInDetail();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyThatAdvertiserNameAndMediaTypeAreHighlightedOnMouseHover(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify On hover mouse on any Ad Advertiser name and Media type should be highlighted.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                carousels.VerifyMouseHoverOnCarousels();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifySliderNavigationFunctionalityBelowCarousel(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify Slider navigation functionality below Carousel.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                string[] imgSourceList1 = carousels.GetImageSourceFromCarouselAndClickSlider(true, false);
                string[] imgSourceList2 = carousels.GetImageSourceFromCarouselAndClickSlider(false, false);
                fieldOptions.compareListOfItemsInOrder(imgSourceList1, imgSourceList2, false);
                string[] imgSourceList3 = carousels.GetImageSourceFromCarouselAndClickSlider(true, false);
                fieldOptions.compareListOfItemsInOrder(imgSourceList1, imgSourceList3);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC003");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyArrowSliderNavigationFunctionalityBelowCarousel(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify ' < ' ' > ' slider functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                string[] imgSourceList1 = carousels.GetImageSourceFromCarouselAndClickSlider(true, true);
                string[] imgSourceList2 = carousels.GetImageSourceFromCarouselAndClickSlider(false, true);
                fieldOptions.compareListOfItemsInOrder(imgSourceList1, imgSourceList2, false);
                string[] imgSourceList3 = carousels.GetImageSourceFromCarouselAndClickSlider(true, true);
                fieldOptions.compareListOfItemsInOrder(imgSourceList1, imgSourceList3);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyViewAdFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify View Ad Functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                carousels.clickButtonOnCarousel("View Ad");
                carousels.VerifyViewAdFunctionality();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyMarketsFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify Markets Functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                carousels.VerifyMarketsFunctionality();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyDetailsFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify Details Functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                carousels.clickButtonOnCarousel("Details");
                carousels.VerifyDetailsFunctionality();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyThatUserIsAbleToSelectAdsFromCarouselView_ClickingAnywhereOnAdFromCarouselShouldSelectTheAdCheckbox(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify User able to select Ads from carousel view (Anywhere click on Ad from carousel should select the Ad checkbox ).");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                carousels.VerifySelectAdFunctionality(false, true);
                carousels.VerifySelectAdFunctionality(true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyItemsSelectionInCarouselShouldPersistInAllDetail_Table_ThumbnailViewOfAgGrid(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify Selected items in Carousel should be persist selected in All Detail/Table/Thumbnail view of AgGrid");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                string adCode = carousels.getAdCodeFromCarousel();
                carousels.VerifySelectAdFunctionality(false);
                carousels.VerifyCheckboxInAgGrid(adCode, "Table");
                homePage.selectViewForResultsDisplay("Thumbnail");
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail");
                homePage.selectViewForResultsDisplay("Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Details");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyThatAfterDeselectAdFromCarouselSelectionShouldBeRemovedFromDetails_ThumbnailAndTableView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify After deselect ad from carousel selection should be removed from Details, thumbnail, and table view");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                string adCode = carousels.getAdCodeFromCarousel();
                carousels.VerifySelectAdFunctionality(false);
                carousels.VerifyCheckboxInAgGrid(adCode, "Table");
                homePage.selectViewForResultsDisplay("Thumbnail");
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail");
                homePage.selectViewForResultsDisplay("Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Details");

                carousels.VerifySelectAdFunctionality(false, false, true);
                homePage.selectViewForResultsDisplay("Table");
                carousels.VerifyCheckboxInAgGrid(adCode, "Table", false);
                homePage.selectViewForResultsDisplay("Thumbnail");
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail", false);
                homePage.selectViewForResultsDisplay("Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Details", false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyThatRecordSelectedFromTableViewShouldBeDisplayedAsSelectedInCarousel_DetailsAndThumbnailView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify User selected record from Table view should be displayed as selected in Carousel,Details and thumbnail view");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                string adCode = carousels.selectRecordFromResults("Table");
                homePage.selectViewForResultsDisplay("Thumbnail");
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail");
                homePage.selectViewForResultsDisplay("Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Carousel");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyThatAfterDeselectAdFromTableSelectionShouldBeRemovedFromDetails_ThumbnailAndCarouselView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify After deselect ad from Table selection should be removed from Details, thumbnail, and Carousel view");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                string adCode = carousels.selectRecordFromResults("Table");
                homePage.selectViewForResultsDisplay("Thumbnail");
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail");
                homePage.selectViewForResultsDisplay("Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Carousel");

                homePage.selectViewForResultsDisplay("Table");
                carousels.selectRecordFromResults("Table", false, adCode);
                homePage.selectViewForResultsDisplay("Thumbnail");
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail", false);
                homePage.selectViewForResultsDisplay("Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Details", false);
                carousels.VerifyCheckboxInAgGrid(adCode, "Carousel", false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC012");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyThatRecordSelectedFromDetailsViewShouldBeDisplayedAsSelectedInCarousel_TableAndThumbnailView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify User selected record from Details view should be displayed as selected in Carousel,Table and thumbnail view");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                driver._scrollintoViewElement("xpath", "//ag-grid-angular");
                homePage.selectViewForResultsDisplay("Details");
                string adCode = carousels.selectRecordFromResults("Details");
                homePage.selectViewForResultsDisplay("Thumbnail");
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail");
                homePage.selectViewForResultsDisplay("Table");
                carousels.VerifyCheckboxInAgGrid(adCode, "Table");
                carousels.VerifyCheckboxInAgGrid(adCode, "Carousel");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyThatAfterDeselectAdFromDetailsSelectionShouldBeRemovedFromCarousel_ThumbnailAndTableView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify After deselect ad from Details selection should be removed from Carousel, thumbnail, and table view");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                driver._scrollintoViewElement("xpath", "//ag-grid-angular");
                homePage.selectViewForResultsDisplay("Details");
                string adCode = carousels.selectRecordFromResults("Details");
                homePage.selectViewForResultsDisplay("Thumbnail");
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail");
                homePage.selectViewForResultsDisplay("Table");
                carousels.VerifyCheckboxInAgGrid(adCode, "Table");
                carousels.VerifyCheckboxInAgGrid(adCode, "Carousel");

                driver._scrollintoViewElement("xpath", "//ag-grid-angular");
                homePage.selectViewForResultsDisplay("Details");
                carousels.selectRecordFromResults("Details", false, adCode);
                homePage.selectViewForResultsDisplay("Thumbnail");
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail", false);
                homePage.selectViewForResultsDisplay("Table");
                carousels.VerifyCheckboxInAgGrid(adCode, "Table", false);
                carousels.VerifyCheckboxInAgGrid(adCode, "Carousel", false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_VerifyThatRecordSelectedFromThumbnailViewShouldBeDisplayedAsSelectedInCarousel_DetailsAndTableView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify User selected record from Thumbnail view should be displayed as selected in Carousel,Details and Table view");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                driver._scrollintoViewElement("xpath", "//ag-grid-angular");
                homePage.selectViewForResultsDisplay("Thumbnail");
                string adCode = carousels.selectRecordFromResults("Thumbnail");
                homePage.selectViewForResultsDisplay("Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Details");
                homePage.selectViewForResultsDisplay("Table");
                carousels.VerifyCheckboxInAgGrid(adCode, "Table");
                carousels.VerifyCheckboxInAgGrid(adCode, "Carousel");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC015");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_VerifyThatAfterDeselectAdFromThumbnailSelectionShouldBeRemovedFromDetails_CarouselAndTableView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016-Verify After deselect ad from Thumbnail selection should be removed from Details, Carousel, and table view");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                driver._scrollintoViewElement("xpath", "//ag-grid-angular");
                homePage.selectViewForResultsDisplay("Thumbnail");
                string adCode = carousels.selectRecordFromResults("Thumbnail");
                homePage.selectViewForResultsDisplay("Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Details");
                homePage.selectViewForResultsDisplay("Table");
                carousels.VerifyCheckboxInAgGrid(adCode, "Table");
                carousels.VerifyCheckboxInAgGrid(adCode, "Carousel");

                driver._scrollintoViewElement("xpath", "//ag-grid-angular");
                homePage.selectViewForResultsDisplay("Thumbnail");
                carousels.selectRecordFromResults("Thumbnail", false, adCode);
                homePage.selectViewForResultsDisplay("Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Details", false);
                homePage.selectViewForResultsDisplay("Table");
                carousels.VerifyCheckboxInAgGrid(adCode, "Table", false);
                carousels.VerifyCheckboxInAgGrid(adCode, "Carousel", false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC016");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC017_VerifyThatUserIsAbleToExportSelectedAdFromCarouselInAllFormats_XLS_PPT_Asset(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017-Verify that user is able to Export Selected ad from carousel in all format (XLS,PPT,Asset)");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                carousels.VerifyCarousels();
                carousels.clickButtonOnCarousel("Details");
                carousels.VerifyDetailsFunctionality();
                string[,] dataGrid = carousels.getDetailsFromCarousel();
                carousels.VerifyDetailsFunctionality(false);
                carousels.VerifySelectAdFunctionality(false);
                carousels.clickOnExportOptions("Export");
                carousels.VerifySelectAnExportTypePopup();
                carousels.selectOptionAndClickButtonOnSelectAnExportTypePopup("Send");
                string screenName = homePage.getActiveScreenNameFromSideNavigationBar() + " - QA Testing - Brand";
                string fileName = brandDashboard.VerifyFileDownloadedOrNotOnScreen("db", "*.xlsx");
                carousels.VerifyDataInExportedFileFromCarousel(fileName, screenName, dataGrid);

                carousels.clickOnExportOptions("Export");
                carousels.VerifySelectAnExportTypePopup();
                carousels.selectOptionAndClickButtonOnSelectAnExportTypePopup("Send", "1 Creative");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("", "*.pptx");

                carousels.clickOnExportOptions("Export");
                carousels.VerifySelectAnExportTypePopup();
                carousels.selectOptionAndClickButtonOnSelectAnExportTypePopup("Send", "Creative Assets");
                brandDashboard.VerifyFileDownloadedOrNotOnScreen("", "*.zip");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC017");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_VerifyThatClickOnResetFromExportAllRemovesAllSelections(String Bname)
        {
            TestFixtureSetUp(Bname, "TC018-Verify Click on Reset button from Export all checkbox should be Reset in carousel)");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                homePage.VerifyAndModifySearchOptions(false);
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.selectNewDateRangeOptionFromSection("Last Year");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Print");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                carousels.VerifyCarousels();
                string adCode1 = carousels.getAdCodeFromCarousel();
                carousels.VerifySelectAdFunctionality(false);
                string adCode2 = carousels.getAdCodeFromCarousel();
                carousels.VerifySelectAdFunctionality(false);
                string adCode3 = carousels.getAdCodeFromCarousel();
                carousels.VerifySelectAdFunctionality(false);
                carousels.clickOnExportOptions("Reset", 3);
                carousels.VerifyCheckboxInAgGrid(adCode1, "Carousel", false);
                carousels.VerifyCheckboxInAgGrid(adCode2, "Carousel", false);
                carousels.VerifyCheckboxInAgGrid(adCode3, "Carousel", false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC018");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC019_VerifyThatAdsShouldNotBeRepeatedInCarouselInBC_PrintDynamicsDashboard_Ad_report(String Bname)
        {
            TestFixtureSetUp(Bname, "TC019-Verify Ads should not be Repeated in carousel in BC- Print Dynamics Dashboard (Ad) report");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                homePage.VerifyAndModifySearchOptions(false, false, "QA Testing - Brand Canada");
                searchPage.selectTabOnSearchOptions("Ad Code");
                searchPage.VerifyNewAdCodeSectionOnScreen();
                searchPage.enterAdCodeInNewAdCodeSearchAreaOnScreen("437876");
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.setCustomDateRange("01/01/2015");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                carousels.VerifyCarousels(true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC019");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC020_VerifySelectedItemsInCarouselShouldBeRemainSelectedAfterUserSwitchToAnyOtherReportAndComesToOriginalReport(String Bname)
        {
            TestFixtureSetUp(Bname, "TC020-Verify Selected items in Carousel should be remain selected after user switch to any other report and comes to original report");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                carousels.VerifyCarousels(true);
                string adcode = carousels.getAdCodeFromCarousel();
                carousels.VerifySelectAdFunctionality(false);
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                carousels.VerifyCarousels(true);
                carousels.VerifyCheckboxInAgGrid(adcode, "Carousel");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC020");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC021_VerifyCarouselDataShouldBeUpdatedBasedOnSearchFilters(String Bname)
        {
            TestFixtureSetUp(Bname, "TC021-Verify Carousel data should be updated based on search filters");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                homePage.VerifyAndModifySearchOptions();
                searchPage.selectTabOnSearchOptions("Date Range");
                searchPage.selectNewDateRangeOptionFromSection("Last Month");
                searchPage.selectTabOnSearchOptions("Media");
                searchPage.selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment("Print");
                searchPage.clickButtonOnSearchOptions("Apply");
                homePage.newVerifyHomePage();
                carousels.VerifyCarousels();
                string[] mediaType = { "Print" };
                carousels.VerifyAppliedFilterOnMediaType(mediaType);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC021");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC022_VerifySortedByFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC022-Verify Sorted by functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                carousels.VerifyCarousels();
                carousels.VerifySortingOnCarousel("Spend");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC022");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC023_VerifyCarouselWhenTheDefaultSelectedSortingOptionIsFirstRunDate(String Bname)
        {
            TestFixtureSetUp(Bname, "TC023-Verify Carousel  when the default selected sorting option is First Run date");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                homePage.selectOptionFromSideNavigationBar("QA Testing - Brand - Weekly Report");
                carousels.VerifyCarousels();
                carousels.VerifySortingOnCarousel("First Run Date");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC023 ");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC024_VerifyCarouselWhenTheDefaultSelectedSortingOptionIsFirstRunDate(String Bname)
        {
            TestFixtureSetUp(Bname, "TC024-Verify Carousel  when the default selected sorting option is Spend");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired();
                homePage.selectOptionFromSideNavigationBar("QA Testing - Brand - Weekly Report");
                carousels.VerifyCarousels();
                carousels.VerifySortingOnCarousel("Spend");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite03_Reskin_Carousels_TC024 ");
                throw;
            }
            driver.Quit();
        }


        #endregion
    }
}
