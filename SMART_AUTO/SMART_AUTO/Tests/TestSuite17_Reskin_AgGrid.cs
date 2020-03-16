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
    public class TestSuite17_Reskin_AgGrid : Base
    {
        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        Search searchPage;
        //Schedule schedule;
        Charts charts;
        FieldOptions fieldOptions;
        SecondaryButtons secondaryButtons;
        SummaryTags summaryTags;
        AgGrid agGrid;
        Carousels carousels;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite17_Reskin_AgGrid).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite17_Reskin_AgGrid).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            secondaryButtons = new SecondaryButtons(driver, test);
            //schedule = new Schedule(driver, test);
            charts = new Charts(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            summaryTags = new SummaryTags(driver, test);
            agGrid = new AgGrid(driver, test);
            carousels = new Carousels(driver, test);

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
        public void TC001_VerifyAgGridInTableView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify AgGrid in Table view");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyThatUserisAbleToSelectAResultsLayoutDropdownWithOptionTableView_DetailsViewAndThumbnailViewInAgGrid(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify User able to select a results layout  dropdown with option 'Table View', 'Detail View' and 'Thumbnail View' in AgGrid");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid(true);
                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid(true);
                homePage.selectViewForResultsDisplay("Table");
                homePage.VerifyTableViewOfAgGrid();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_4_VerifyThePaginationFunctionalityAtTheBottomLeftOfTheAgGrid(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003_4-Verify the Pagination functionality at the bottom left of  the AgGrid");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();
                agGrid.VerifyPaginationFunctionalityOfAgGrid("2", "10");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC003_4");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyTheSortingFunctionalityOnTheFieldsOfAgGrid(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify the sorting functionality on the fields of AgGrid");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();
                string column = agGrid.VerifySortingFunctionalityOfAgGrid("random", false);
                agGrid.VerifySortingFunctionalityOfAgGrid(column, true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyThatFieldsOfTheAgGridShouldDisplayAsPerTheSelectionMadeFromTheFieldOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify Fields of the AgGrid should display as per the selection made from the Field options");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                string[] agGridList1 = fieldOptions.captureColumnNamesOfAgGridInOrder();
                fieldOptions.VerifyFieldOptions();
                string[] visibleList1 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                string[] movedItems = fieldOptions.moveItemsFromHiddenToVisible(1);
                fieldOptions.VerifyAddedItemsInVisibleItems(movedItems);
                string[] visibleList2 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                fieldOptions.compareListOfItemsInOrder(visibleList1, visibleList2, false);
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                string[] agGridList2 = fieldOptions.captureColumnNamesOfAgGridInOrder();
                fieldOptions.compareListOfItemsInOrder(agGridList2, visibleList2);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyThatTheAdSelectedFromTheAgGridShouldDisplayInTheInspectorAreaBesideIt(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify the ad selected from the AgGrid should display in the inspector area beside it");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                fieldOptions.VerifyFieldOptions();
                fieldOptions.addOrRemoveSpecificFieldInFieldOptions("Ad Code");
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                string adCode = carousels.selectRecordFromResults("table");
                carousels.VerifyCheckboxInAgGrid(adCode, "table");
                agGrid.VerifyInspectorAreaForSelectedRecordFromAgGrid(adCode);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyThatOnSelectingTheRecordsFromTheAgGridShouldIncreaseTheCountOfItemsOnTheExportButton(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify on Selecting the records from the AgGrid should increase the count of items on the Export button");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                fieldOptions.VerifyFieldOptions();
                fieldOptions.addOrRemoveSpecificFieldInFieldOptions("Ad Code");
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                string adCode = carousels.selectRecordFromResults("table");
                carousels.VerifyCheckboxInAgGrid(adCode, "table");
                carousels.VerifyCheckboxInAgGrid(adCode, "carousel");
                carousels.clickOnExportOptions("");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyThatUserShouldBeAbleToDragFieldsColumn(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify User should be able to Drag fields column");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();
                agGrid.VerifyThatColumnsCanBeDragged();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyThatWhenUserSelectsARecordInAnyView_SwitchesToAnyOtherReportAndComesToOriginalReportThenSelectionPersists(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify when user makes selection in any view and switch to any other report & comes to original report then selection should persist");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                fieldOptions.VerifyFieldOptions();
                fieldOptions.addOrRemoveSpecificFieldInFieldOptions("Ad Code");
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                string adCode = carousels.selectRecordFromResults("table");
                carousels.VerifyCheckboxInAgGrid(adCode, "table");

                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                carousels.VerifyCheckboxInAgGrid(adCode, "table");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyResetSelectedFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify Reset selected functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                fieldOptions.VerifyFieldOptions();
                fieldOptions.addOrRemoveSpecificFieldInFieldOptions("Ad Code");
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                string adCode1 = carousels.selectRecordFromResults("table");
                string adCode2 = carousels.selectRecordFromResults("table");
                string adCode3 = carousels.selectRecordFromResults("table");
                carousels.VerifyCheckboxInAgGrid(adCode1, "table");
                carousels.VerifyCheckboxInAgGrid(adCode2, "table");
                carousels.VerifyCheckboxInAgGrid(adCode3, "table");
                carousels.clickOnExportOptions("Reset", 3);
                carousels.VerifyCheckboxInAgGrid(adCode1, "table", false);
                carousels.VerifyCheckboxInAgGrid(adCode2, "table", false);
                carousels.VerifyCheckboxInAgGrid(adCode3, "table", false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_13_VerifyThatTheSelectionOfRecordsMadeFromDifferentViewAndCarouselShouldBeInSyncAndRemainPersistant(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012_13-Verify the Selection of records made from different view and carousel should be in Sync & remain persist");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                fieldOptions.VerifyFieldOptions();
                fieldOptions.addOrRemoveSpecificFieldInFieldOptions("Ad Code");
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                string adCode = carousels.selectRecordFromResults("table");
                carousels.VerifyCheckboxInAgGrid(adCode, "table");

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid(true);
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail");

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid(true);
                carousels.VerifyCheckboxInAgGrid(adCode, "Details");

                carousels.VerifyCheckboxInAgGrid(adCode, "Carousel");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC012_13");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyThatSelectingTheRecordsFromTheDetailsViewShouldIncreaseTheCountOfItemsOnTheExportButton(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify On Selecting the records from the detail view should increase the count of items on the Export button");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                fieldOptions.VerifyFieldOptions();
                fieldOptions.addOrRemoveSpecificFieldInFieldOptions("Ad Code");
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid(true);
                string adCode = carousels.selectRecordFromResults("Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "Details");
                carousels.VerifyCheckboxInAgGrid(adCode, "carousel");
                carousels.clickOnExportOptions("");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_VerifyThatFieldsOfTheAgGridDetailViewAreDisplayedAsPerTheSelectionMadeFromTheFieldOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify Fields of the AgGrid Detail view should display as per the selection made from the Field options");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid(true);
                string[] agGridList1 = agGrid.captureColumnNamesOfDetailsViewAgGridInOrder();
                fieldOptions.VerifyFieldOptions();
                string[] visibleList1 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                string[] movedItems = fieldOptions.moveItemsFromHiddenToVisible(1);
                fieldOptions.VerifyAddedItemsInVisibleItems(movedItems);
                string[] visibleList2 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                fieldOptions.compareListOfItemsInOrder(visibleList1, visibleList2, false);
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                string[] agGridList2 = agGrid.captureColumnNamesOfDetailsViewAgGridInOrder();
                fieldOptions.compareListOfItemsInOrder(agGridList2, visibleList2);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC015");
                throw;
            }
            driver.Quit();
        }


        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_17_VerifyThePaginationFunctionalityAtTheBottomLeftOfTheAgGridDetailsView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016_17-Verify the Pagination functionality at the bottom left of  the AgGrid Details view");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid(true);
                agGrid.VerifyPaginationFunctionalityOfAgGrid("2", "50");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC016_17");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_VerifyThatClickingAnywhereExceptForTheViewAd_Markets_OccurrencesOrDetailsButtonShouldNotOpenTheAdModalWindowAndAdShouldBeSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC018-Verify on Clicking anywhere except for the View Ad, Markets , Occurrences or Details button  should not open the Ad Modal Window & Ad should be Selected");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid(true);
                string adCode = carousels.selectRecordFromResults("Details");
                carousels.VerifyViewAdFunctionality(false);
                carousels.VerifyCheckboxInAgGrid(adCode, "Details");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC018");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC019_VerifyThumbnailViewOfAgGrid(String Bname)
        {
            TestFixtureSetUp(Bname, "TC019-Verify Thumbnail View of AgGrid");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid(true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC019");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC020_21_VerifyThePaginationFunctionalityAtTheBottomLeftOfTheAgGridThumbnailsView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC020_21-Verify the Pagination functionality at the bottom left of  the AgGrid Thumbnails view");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid(true);
                agGrid.VerifyPaginationFunctionalityOfAgGrid("2", "100");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC020_21");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC022_VerifyThatInAdThumbnailAdShouldBeDisplayedWithSpendAmountOnItForBrandCanadaAndMediaTypeForBrandAccount(String Bname)
        {
            TestFixtureSetUp(Bname, "TC022-Verify in Ad thumbnail, Ad should be displayed with Spend amount on it for Brand Canada and Media type for Brand account");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Occurrence)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid(true);
                agGrid.VerifyThumbnailImageTagInAgGrid();

                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.VerifyTableViewOfAgGrid();

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid();
                agGrid.VerifyThumbnailImageTagInAgGrid();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC022");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC023_VerifyThatSelectingTheRecordsFromTheThumbnailViewShouldIncreaseTheCountOfItemsOnTheExportButton(String Bname)
        {
            TestFixtureSetUp(Bname, "TC023-Verify on Selecting the records from the Thumbnail view should increase the count of items on the Export button");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                fieldOptions.VerifyFieldOptions();
                fieldOptions.addOrRemoveSpecificFieldInFieldOptions("Ad Code");
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid(true);
                string adCode = carousels.selectRecordFromResults("Thumbnail");
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail");
                carousels.VerifyCheckboxInAgGrid(adCode, "carousel");
                carousels.clickOnExportOptions("");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC023");
                throw;
            }
            driver.Quit();
        }


        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC024_VerifyThatClickingAnywhereExceptForTheViewAd_Markets_OccurrencesOrDetailsButtonShouldNotOpenTheAdModalWindowAndAdShouldBeSelected(String Bname)
        {
            TestFixtureSetUp(Bname, "TC024-Verify on Clicking anywhere except for the View Ad, Markets , Occurrences or Details button  should not open the Ad Modal Window & Ad should be Selected");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Canada");
                homePage.selectOptionFromSideNavigationBar("Print Dynamics Dashboard (Ad)");
                homePage.newVerifyHomePage();
                homePage.VerifyTableViewOfAgGrid();

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid(true);
                string adCode = carousels.selectRecordFromResults("Thumbnail");
                carousels.VerifyViewAdFunctionality(false);
                carousels.VerifyCheckboxInAgGrid(adCode, "Thumbnail");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite17_Reskin_AgGrid_TC024");
                throw;
            }
            driver.Quit();
        }


        #endregion
    }
}
