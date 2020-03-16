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
    public class TestSuite009_UserProfile : Base
    {
        string userName = "Brand Canada";
        string mainMenuName = "Print Dynamics";
        string dashboardName = "by Media";

        #region Private Variables

        private IWebDriver driver;
        Login loginPage;
        Home homePage;
        PivotReportScreen pivotReportScreen;
        Schedule schedule;
        UserProfile userProfile;

        #endregion

        #region Public Fixture Methods

        public IWebDriver TestFixtureSetUp(string Bname, string testCaseName)
        {
            driver = StartBrowser(Bname);
            Common.CurrentDriver = driver;
            Results.WriteTestSuiteHeading(typeof(TestSuite009_UserProfile).Name);
            starttest(Bname + " - " + testCaseName, typeof(TestSuite009_UserProfile).Name);

            loginPage = new Login(driver, test);
            homePage = new Home(driver, test);
            pivotReportScreen = new PivotReportScreen(driver, test);
            userProfile = new UserProfile(driver, test);
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
        public void TC001_VerifyUserDetailsGivenOnUserButton(String Bname)
        {
            TestFixtureSetUp(Bname, "TC001-Verify User details given on user button.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.clickUserIconVerifyUserProfileSectionContent(true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC001");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC002_VerifyEditProfileButtonShouldBeOnlyDisplayedForBrandAccounts(String Bname)
        {
            TestFixtureSetUp(Bname, "TC002-Verify 'Edit Profile' button Should be only displayed for Brand Accounts.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList("QA Testing - Promo");
                userProfile.clickUserIconVerifyUserProfileSectionContent(false);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC002");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC003_VerifyUserAbleToChangeUserAccount(String Bname)
        {
            TestFixtureSetUp(Bname, "TC003-Verify User able to change User account.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                schedule.VerifyReportScreenDetails();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC003");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC004_VerifyUserAbleToChangeUserAccount(String Bname)
        {
            TestFixtureSetUp(Bname, "TC004-Verify User able to change User account.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.enterAccountNameOnInputAreaOnScreen("Brand");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC004");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC005_VerifyUserAbleToFilterUserAccountByPartialSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC005-Verify User able to filter user account by Partial search.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.enterAccountNameOnInputAreaOnScreen("Br");
                userProfile.VerifySearchValueWithAccountNameList("Br");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC005");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC006_VerifyUserAbleToFilterUserAccountByPartialSearch(String Bname)
        {
            TestFixtureSetUp(Bname, "TC006-Verify User able to filter user account by Partial search.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                string randomSearch = userProfile.enterAccountNameOnInputAreaOnScreen("Random");
                userProfile.VerifySearchValueWithAccountNameList(randomSearch, true);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC006");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC007_VerifyClearButtonFunctionalityGivenBesidesFilterTextBox(String Bname)
        {
            TestFixtureSetUp(Bname, "TC007-Verify Clear button functionality given besides filter text box.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.enterAccountNameOnInputAreaOnScreen("Brand");
                userProfile.VerifyAndClickClearButtonForSearchValue();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC007");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC008_VerifyEditProfileButton(String Bname)
        {
            TestFixtureSetUp(Bname, "TC008-Verify 'Edit Profile' button.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.clickUserProfileOptionsOnScreen("Edit Profile");
                userProfile.VerifyEditProfileSectionOnScreen();

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC008");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC009_VerifyCountryDropdownListFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC009-Verify Country dropdown list functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.clickUserProfileOptionsOnScreen("Edit Profile");
                userProfile.clickAnyFieldFromFormAndVerifySelectedSectionInColor("Country");

                string[] opt = { "Australia", "Canada", "New Zealand", "United States" };
                userProfile.clickDropdownListAndVerifyOptions("Country", opt, "Canada");
                userProfile.clickDropdownListAndVerifyOptions("Language", null, "");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC009");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC010_VerifyUserAbleToFilterCountryFromList(String Bname)
        {
            TestFixtureSetUp(Bname, "TC010-Verify User able to filter country from list.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.clickUserProfileOptionsOnScreen("Edit Profile");
                userProfile.clickAnyFieldFromFormAndVerifySelectedSectionInColor("Country");
                userProfile.clickDropdownListAndEnterValue("Country", "United");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC010");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC011_VerifyMessageWhenUserEnterInvalidCountryInFilterTextbox(String Bname)
        {
            TestFixtureSetUp(Bname, "TC011-Verify Message when user enter invalid country in filter textbox.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.clickUserProfileOptionsOnScreen("Edit Profile");
                userProfile.clickAnyFieldFromFormAndVerifySelectedSectionInColor("Country");
                userProfile.clickDropdownListAndEnterValue("Country", "Random");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC011");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC012_VerifyLanguageDropdownListFilterFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC012-Verify Language dropdown list filter functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.clickUserProfileOptionsOnScreen("Edit Profile");
                userProfile.clickAnyFieldFromFormAndVerifySelectedSectionInColor("Language");
                userProfile.clickDropdownListAndEnterValue("Language", "English");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC012");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC013_VerifyMessageWhenUserEnterInvalidLanguageInFilterTextbox(String Bname)
        {
            TestFixtureSetUp(Bname, "TC013-Verify Message when user enter invalid Language in filter textbox.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.clickUserProfileOptionsOnScreen("Edit Profile");
                userProfile.clickAnyFieldFromFormAndVerifySelectedSectionInColor("Language");
                userProfile.clickDropdownListAndEnterValue("Language", "Random");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC013");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC014_VerifyTimeZoneDropdownListFilterFunctionality(String Bname)
        {
            TestFixtureSetUp(Bname, "TC014-Verify Time zone dropdown list filter functionality.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.clickUserProfileOptionsOnScreen("Edit Profile");
                userProfile.clickAnyFieldFromFormAndVerifySelectedSectionInColor("Timezone");
                userProfile.clickDropdownListAndEnterValue("Timezone", "kol");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC014");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC015_VerifyMessageWhenUserEnterInvalidTimeZoneInFilterTextbox(String Bname)
        {
            TestFixtureSetUp(Bname, "TC015-Verify Message when user enter invalid Time zone in filter textbox.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.clickUserProfileOptionsOnScreen("Edit Profile");
                userProfile.clickAnyFieldFromFormAndVerifySelectedSectionInColor("Timezone");
                userProfile.clickDropdownListAndEnterValue("Timezone", "Random");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC015");
                throw;
            }
            driver.Quit();
        }

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC016_VerifyUserAbleToUpdateUserProfileSuccessfully(String Bname)
        {
            TestFixtureSetUp(Bname, "TC016-Verify User able to update user profile successfully.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.clickUserProfileOptionsOnScreen("Edit Profile");
                userProfile.VerifyUpdateProfileButtonDisableOrNot(true);
                string firstName = userProfile.enterFirstOrLastNameOnEditProfileForm(false, true);
                string lastName = userProfile.enterFirstOrLastNameOnEditProfileForm(false, false);
                userProfile.VerifyUpdateProfileButtonDisableOrNot(false).clickButtonOnEditProfileScreen("Update Profile");
                userProfile.VerifyValueOfFormOnEditProfileScreen("First Name", firstName);
                userProfile.VerifyValueOfFormOnEditProfileScreen("Last Name", lastName);

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC016");
                throw;
            }
            driver.Quit();
        }

        // TC017 & TC018 not Verify Email Functionality not Verify

        [Test]
        [TestCaseSource(typeof(Base), "BrowserToRun")]
        public void TC019_VerifyUserCantUpdateProfileWithoutEnterDetails(String Bname)
        {
            TestFixtureSetUp(Bname, "TC019-Verify User can't update profile without enter details.");
            try
            {
                loginPage.navigateToLoginPage().VerifyLoginPageScreenInDetail();
                loginPage.loginUsingValidEmailIdAndPassword();

                homePage.VerifyHomePage().clickUserMenuAndSelectAccountFromList(userName);
                homePage.clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(mainMenuName, dashboardName);
                //homePage.clickSiteNavigationMenuIconAndSelectOptionFromListOnPage("Media");

                pivotReportScreen.VerifyReportScreenDetails();
                userProfile.clickUserProfileOptionsOnScreen("Edit Profile");
                userProfile.VerifyUpdateProfileButtonDisableOrNot(true);
                string firstName = userProfile.enterFirstOrLastNameOnEditProfileForm(true, true);
                string lastName = userProfile.enterFirstOrLastNameOnEditProfileForm(true, false);
                userProfile.VerifyUpdateProfileButtonDisableOrNot(false);
                userProfile.clickButtonOnEditProfileScreen("Update Profile");

            }
            catch (Exception e)
            {
                Logging.LogStop(this.driver, test, e, MethodBase.GetCurrentMethod(), Bname + "_TestSuite009_UserProfile_TC019");
                throw;
            }
            driver.Quit();
        }

        #endregion
    }
}
