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
    public class TestSuite02_Reskin_FieldOptions : Base
    {
        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        Search searchPage;
        FieldOptions fieldOptions;
        UserProfile userProfile;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite02_Reskin_FieldOptions).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite02_Reskin_FieldOptions).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            userProfile = new UserProfile(driver, test);

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
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyFieldOptionsFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify Field options functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                fieldOptions.VerifyFieldOptions();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifyThatUserShouldBeAbleToMoveOptionsFromHiddenFieldToVisibleFieldsByClickingPlusOnIcon(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify, User should be able to move options from hidden field to visible fields by clicking on ' + ' icon");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                fieldOptions.VerifyFieldOptions();
                string[] movedItems = fieldOptions.moveItemsFromHiddenToVisible(2);
                fieldOptions.VerifyAddedItemsInVisibleItems(movedItems);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC003");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyThatUserShouldBeAbleToMoveOptionsFromVisibleFieldToHiddenFieldsByClickingOnMinusIcon(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify, User should be able to move options from visible field to hidden fields by clicking on ' - ' icon");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                fieldOptions.VerifyFieldOptions();
                string[] movedItems = fieldOptions.moveItemsFromVisibleToHidden(2);
                fieldOptions.VerifyRemovedItemsInHiddenItems(movedItems);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyThatUserShouldBeAbleToMoveOptionsByDragAndDropFromVisibleFieldToHiddenFieldsAndViceVersa(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify, User able to move fields by drag and drop from hidden field to visible fields & Vice versa");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                fieldOptions.VerifyFieldOptions();
                string[] movedItems = fieldOptions.VerifyDragAndDropInHiddenAndVisibleItemFields("Hidden", "Across", 1);
                fieldOptions.VerifyAddedItemsInVisibleItems(movedItems);
                string[] newMovedItems = fieldOptions.VerifyDragAndDropInHiddenAndVisibleItemFields("Visible", "Across", 1);
                fieldOptions.VerifyRemovedItemsInHiddenItems(newMovedItems);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyThatUserShouldBeAbleToChangeAllTheOptionsOrderedByDragAndDropInBothHiddenAndVisibleFields(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify, User can change all the fields ordered by drag and drop in both hidden and visible fields");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                fieldOptions.VerifyFieldOptions();
                string[] movedItems = fieldOptions.VerifyDragAndDropInHiddenAndVisibleItemFields("Hidden", "Verticle", 1);
                fieldOptions.VerifyRemovedItemsInHiddenItems(movedItems, true);
                string[] newMovedItems = fieldOptions.VerifyDragAndDropInHiddenAndVisibleItemFields("Visible", "Verticle", 1);
                fieldOptions.VerifyAddedItemsInVisibleItems(newMovedItems, true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC006");
                throw;
            }
            driver.Quit();
        }

        //Pending TC007 because the Functionality is Under Development

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyCancelButtonFunctionalityOnFieldOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify Cancel button functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                fieldOptions.VerifyFieldOptions();
                fieldOptions.clickButtonOnFieldOptionsPopup("Cancel");
                fieldOptions.VerifyFieldOptions(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyResetButtonFunctionalityOnFieldOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify Reset button functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                fieldOptions.VerifyFieldOptions();
                string[] hiddenList1 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Hidden");
                string[] visibleList1 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                string[] movedItems = fieldOptions.moveItemsFromHiddenToVisible(2);
                fieldOptions.VerifyAddedItemsInVisibleItems(movedItems);
                string[] hiddenList2 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Hidden");
                string[] visibleList2 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                fieldOptions.compareListOfItemsInOrder(hiddenList1, hiddenList2, false);
                fieldOptions.compareListOfItemsInOrder(visibleList1, visibleList2, false);
                fieldOptions.clickButtonOnFieldOptionsPopup("Reset");
                string[] hiddenList3 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Hidden");
                string[] visibleList3 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                fieldOptions.compareListOfItemsInOrder(hiddenList1, hiddenList3);
                fieldOptions.compareListOfItemsInOrder(visibleList1, visibleList3);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyAllChangesWillOnlyApplyWhileUserClickOnTheApplyButton(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify All changes will only apply while user click on the apply button");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string[] agGridList1 = fieldOptions.captureColumnNamesOfAgGridInOrder();
                fieldOptions.VerifyFieldOptions();
                string[] visibleList1 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                string[] movedItems = fieldOptions.moveItemsFromHiddenToVisible(2);
                fieldOptions.VerifyAddedItemsInVisibleItems(movedItems);
                string[] visibleList2 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                fieldOptions.compareListOfItemsInOrder(visibleList1, visibleList2, false);
                fieldOptions.clickButtonOnFieldOptionsPopup("Cancel");
                fieldOptions.VerifyFieldOptions(false);
                fieldOptions.compareListOfItemsInOrder(agGridList1, visibleList2, false);
                string[] agGridList2 = fieldOptions.captureColumnNamesOfAgGridInOrder();
                fieldOptions.compareListOfItemsInOrder(agGridList1, agGridList2);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_12_VerifyApplyButtonFunctionalityOnFieldOptions(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011_12-Verify Apply button functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
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
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC011_12");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyResetButtonWhenAnOptionIsMovedToVisbleAndAppliedAndThenMovedBackToHidden(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify, when user move any option from 'Visible fields' to 'hidden field' and click on apply after that, again click on field option and move that from 'Hidden fields' to 'Visible field' at that time Reset button should be Disabled ");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string[] agGridList1 = fieldOptions.captureColumnNamesOfAgGridInOrder();
                fieldOptions.VerifyFieldOptions();
                string[] visibleList1 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                fieldOptions.addOrRemoveSpecificFieldInFieldOptions("Clearance");
                string[] visibleList2 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                fieldOptions.compareListOfItemsInOrder(visibleList1, visibleList2, false);
                fieldOptions.VerifyResetButtonAsDisabled_Enabled();
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                string[] agGridList2 = fieldOptions.captureColumnNamesOfAgGridInOrder();
                fieldOptions.compareListOfItemsInOrder(agGridList2, visibleList2);
                fieldOptions.VerifyFieldOptions();
                fieldOptions.addOrRemoveSpecificFieldInFieldOptions("Clearance", false);
                fieldOptions.VerifyResetButtonAsDisabled_Enabled(false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_15_VerifyAfterApplyAnySavedSearchNewlyAddedFieldOptionsShouldRemainInAgGrid(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014_15-Verify After apply any Saved search newly added field options should remain in AgGrid");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                string[] agGridList1 = fieldOptions.captureColumnNamesOfAgGridInOrder();
                fieldOptions.VerifyFieldOptions();
                string[] visibleList1 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                fieldOptions.addOrRemoveSpecificFieldInFieldOptions("Clearance");
                string[] visibleList2 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                fieldOptions.compareListOfItemsInOrder(visibleList1, visibleList2, false);
                fieldOptions.VerifyResetButtonAsDisabled_Enabled();
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                string[] agGridList2 = fieldOptions.captureColumnNamesOfAgGridInOrder();
                fieldOptions.compareListOfItemsInOrder(agGridList2, visibleList2);
                homePage.selectSavedSearchOrCreateNewSavedSearch();
                homePage.newVerifyHomePage();
                string[] agGridList3 = fieldOptions.captureColumnNamesOfAgGridInOrder();
                fieldOptions.compareListOfItemsInOrder(agGridList2, agGridList3);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC014_15");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_VerifyFieldOptionShouldNotBeDisplayedInThumbnailView(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016-Verify field option should not be displayed in Thumbnail view");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");

                homePage.selectViewForResultsDisplay("Thumbnail");
                homePage.VerifyThumbnailViewOfAgGrid();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC016");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC017_VerifyExportAndFieldOptionShouldNotBeDisplayedForThoseWhoDoNotHaveAgGridComponent(String Bname)
        {
            TestFixtureSetUp(Bname, "TC017-Verify Export and Field option should not be displayed who doesn't have AgGrid component");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand Monthly");
                homePage.selectOptionFromSideNavigationBar("Ad Ex Trend - Quarterly");
                homePage.newVerifyHomePage();
                fieldOptions.VerifyFieldOptionsAndExportIcons(false, false);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC017");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC018_VerifyExportAndFieldOptionShouldNotBeDisplayedForThoseWhoDoNotHaveAgGridComponent(String Bname)
        {
            TestFixtureSetUp(Bname, "TC018-Verify User add or remove fields from field options (In Table view) and goes to details view and again come to table view then previous added/removed field should be remain");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                fieldOptions.VerifyFieldOptions();
                fieldOptions.moveItemsFromHiddenToVisible(1);
                string[] visibleList1 = fieldOptions.getItemsFromFieldInFieldOptionsPopup("Visible");
                fieldOptions.clickButtonOnFieldOptionsPopup("Apply");
                fieldOptions.VerifyFieldOptions(false);
                string[] agGridList1 = fieldOptions.captureColumnNamesOfAgGridInOrder();
                homePage.selectViewForResultsDisplay("Details");
                homePage.selectViewForResultsDisplay("Table");
                string[] agGridList2 = fieldOptions.captureColumnNamesOfAgGridInOrder();
                fieldOptions.compareListOfItemsInOrder(agGridList1, agGridList2);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite02_Reskin_FieldOptions_TC018");
                throw;
            }
            driver.Quit();
        }

        #endregion

    }
}
