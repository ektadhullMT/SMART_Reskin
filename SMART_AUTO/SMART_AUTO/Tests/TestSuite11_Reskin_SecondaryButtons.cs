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
    public class TestSuite11_Reskin_SecondaryButtons : Base
    {
        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        Search searchPage;
        FieldOptions fieldOptions;
        UserProfile userProfile;
        Schedule schedule;
        BrandDashboard brandDashboard;
        MyExportsPage myExportsPage;
        SecondaryButtons secondaryButtons;
        SummaryTags summaryTags;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite11_Reskin_SecondaryButtons).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite11_Reskin_SecondaryButtons).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            searchPage = new Search(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            userProfile = new UserProfile(driver, test);
            schedule = new Schedule(driver, test);
            brandDashboard = new BrandDashboard(driver, test);
            myExportsPage = new MyExportsPage(driver, test);
            secondaryButtons = new SecondaryButtons(driver, test);
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
        public void TC001_VerifyTheResetAndSaveButtonInAllReportsWhenNoSearchFiltersAreApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify the Reset and Save button in all reports when no search filters are applied");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                secondaryButtons.VerifySecondaryButtons();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite11_Reskin_SecondaryButtons_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyResetAndSaveButtonFunctionalityWhenSearchOptionFiltersAreApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify Reset and Save button functionality when search option filters are applied");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.VerifyAndModifySearchOptions(true);
                homePage.newVerifyHomePage();
                secondaryButtons.VerifySecondaryButtons(true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite11_Reskin_SecondaryButtons_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_4_VerifyResetAndSaveButtonFunctionalityWhenSavedSearchOptionsAreModified(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003_4-Verify Reset and Save button functionality when saved search options are modified");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                secondaryButtons.VerifySecondaryButtons();
                homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                homePage.newVerifyHomePage();
                secondaryButtons.VerifySecondaryButtons();
                homePage.VerifyAndModifySearchOptions(true);
                homePage.newVerifyHomePage();
                secondaryButtons.VerifySecondaryButtons(true);
                homePage.VerifyAndModifySearchOptions(true, false, "QA Testing - Brand");
                string searchName = homePage.saveNewSearch(false, true, "QA Testing - Brand");
                homePage.newVerifyHomePage();
                secondaryButtons.VerifySecondaryButtons();
                homePage.VerifyAndModifySearchOptions(true);
                homePage.newVerifyHomePage();
                secondaryButtons.VerifySecondaryButtons(true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite11_Reskin_SecondaryButtons_TC003_4");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyDeleteButtonFunctionalityWhenSavedSearchIsApplied(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify Delete button functionality when saved search is applied");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.VerifyAndModifySearchOptions(true);
                homePage.newVerifyHomePage();
                string searchName = homePage.saveNewSearch(false, true, "QA Testing - Brand");
                secondaryButtons.VerifySecondaryButtons();
                secondaryButtons.clickOnSecondaryButtons("Delete");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[contains(text(), 'Successfully deleted saved search: " + searchName + "')]"), "'Search Successfully Deleted' message not present.");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite11_Reskin_SecondaryButtons_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyMakeDefaultSavedSearchFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify, Make default saved search functionality");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.VerifyAndModifySearchOptions(true);
                homePage.newVerifyHomePage();
                string searchName = homePage.saveNewSearch(false, true, "QA Testing - Brand");
                secondaryButtons.VerifySecondaryButtons();
                secondaryButtons.clickOnSecondaryButtons("default");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[contains(text(), 'Successfully updated saved search: " + searchName + "')]"), "'Search Successfully Deleted' message not present.");
                secondaryButtons.VerifySecondaryButtons(false, true);
                secondaryButtons.clickOnSecondaryButtons("default");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[contains(text(), 'Successfully updated saved search: " + searchName + "')]"), "'Search Successfully Deleted' message not present.");
                secondaryButtons.VerifySecondaryButtons();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite11_Reskin_SecondaryButtons_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyThatAfterLogin_MakeDefault_ReportShouldBeDefaultDisplayedInParticularUserAccount(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify After Login 'make default' report should be default displayed in particular user account");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.VerifyAndModifySearchOptions(true);
                homePage.newVerifyHomePage();
                string searchName = homePage.saveNewSearch(false, true, "QA Testing - Brand");
                secondaryButtons.VerifySecondaryButtons();
                secondaryButtons.clickOnSecondaryButtons("default");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[contains(text(), 'Successfully updated saved search: " + searchName + "')]"), "'Search Successfully Deleted' message not present.");
                secondaryButtons.VerifySecondaryButtons(false, true);

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
                summaryTags.VerifySummaryTags(new string[] { searchName });
                secondaryButtons.VerifySecondaryButtons(false, true);
                secondaryButtons.clickOnSecondaryButtons("default");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite11_Reskin_SecondaryButtons_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyThatUserShouldNotBeAbleToSaveSearchWithoutAnyName(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify User should not be able to save search without any name");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.VerifyAndModifySearchOptions(true);
                homePage.newVerifyHomePage();
                secondaryButtons.clickOnSecondaryButtons("Save");
                secondaryButtons.VerifySaveNewSearchPopupAndClickButton("Save");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite11_Reskin_SecondaryButtons_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyThatAfterLogin_MakeDefault_ReportShouldBeDefaultDisplayedInParticularUserAccount(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify After Login 'make default' report should be default displayed in particular user account");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                homePage.VerifyAndModifySearchOptions(true);
                homePage.newVerifyHomePage();
                string searchName = homePage.saveNewSearch(true, true, "QA Testing - Brand");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@role='alert']//div[contains(text(), 'Successfully updated saved search: " + searchName + "')]"), "'Search Successfully updated' message not present.");
                secondaryButtons.VerifySecondaryButtons(false, true);
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite11_Reskin_SecondaryButtons_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyCancelButtonFunctionalityInTheSaveModalWindow(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify Cancel button functionality in the save modal window");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                homePage.VerifyAndModifySearchOptions(true);
                homePage.newVerifyHomePage();
                secondaryButtons.clickOnSecondaryButtons("Save");
                secondaryButtons.VerifySaveNewSearchPopupAndClickButton("Cancel");
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite11_Reskin_SecondaryButtons_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyTooltipsOfAllSecondaryActionButtons(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify tool tips of all Secondary action buttons");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.newVerifyHomePage();
                homePage.VerifyAccountOrSwitchIfRequired("QA Testing - Brand");
                secondaryButtons.VerifySecondaryButtons();
                homePage.selectSavedSearchOrCreateNewSavedSearch(true, "QA Testing - Brand");
                homePage.newVerifyHomePage();
                secondaryButtons.VerifySecondaryButtons();
                secondaryButtons.VerifyTooltipsOfSecondaryButtons();
                homePage.VerifyAndModifySearchOptions(true);
                homePage.newVerifyHomePage();
                secondaryButtons.VerifySecondaryButtons(true);
                secondaryButtons.VerifyTooltipsOfSecondaryButtons();
            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite11_Reskin_SecondaryButtons_TC011");
                throw;
            }
            driver.Quit();
        }


        #endregion

    }
}
