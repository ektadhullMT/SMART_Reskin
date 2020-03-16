using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Configuration;
using System.Data;
using AventStack.ExtentReports;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;
using OpenQA.Selenium.Interactions;

namespace SMART_AUTO
{
    public class Schedule
    {
        #region Private Variables

        private IWebDriver schedule;
        private ExtentTest test;
        Home homePage;

        #endregion

        #region Public Methods

        public Schedule(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.schedule = driver;
            test = testReturn;
            homePage = new Home(driver, test);
        }

        public IWebDriver driver
        {
            get { return this.schedule; }
            set { this.schedule = value; }
        }

        /// <summary>
        /// Verify Report Screen Details
        /// </summary>
        /// <returns></returns>
        public Schedule VerifyReportScreenDetails()
        {
            string[] menuIcons = { "User", "Files", "Help", "Search" };
            VerifyMenuIconOnTopOfScreen(menuIcons);
            driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Loading Pivot Data')]");

            if (driver._isElementPresent("xpath", "//span[@class='lead' and contains(text(),'No items were found')]") || driver._isElementPresent("xpath", "//span[@class='lead' and contains(text(),'No results found')]"))
            {
                PromoDashboard promoDashboard = new PromoDashboard(driver, test);
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("Days", "Last 6 Months");
            }

            IList<IWebElement> buttons = driver.FindElements(By.XPath("//div[contains(@class,'btn-group btn-grid-actions')]//.//button"));
            string[] buttonNames = { "Export Grid", "Schedule", "View Selected", "Reset Selected", "Pivot Options", "Export All", "View Selected", "Reset Selected", "Field Options", "Table View" };
            string[] buttonStatus = { null, "true", "true", "true", null, null, "true", "true", null, null };
            int cnt = 0;

            for (int i = 0; i < buttons.Count; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", buttons[i]);
                for (int j = 0; j < buttonNames.Length; j++)
                    if (buttons[i].Text.Contains(buttonNames[j]) == true)
                    {
                        if (buttonNames[j] != "Schedule")
                            Assert.AreEqual(buttons[i].GetAttribute("disabled"), buttonStatus[j], "'" + buttonNames[j] + "' Button not Enable or Disable on Screen.");
                        ++cnt;
                        break;
                    }
            }

            Assert.AreEqual(cnt, buttonNames.Length, "Button Name not found on Screen.");
            Results.WriteStatus(test, "Pass", "Verified, Buttons on screen.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@ag-grid='pivotCtrl.gridOptions']"), "'Pivot Grid' not Present on screen.");
            VerifyPaginationForGridSection();
            VerifyThumbnailSectionOnScreen();

            Results.WriteStatus(test, "Pass", "Verified, Report Screen Details.");
            return new Schedule(driver, test);
        }

        /// <summary>
        /// Verify Records on Reports Screen
        /// </summary>
        /// <returns></returns>
        public Schedule VerifyRecordsOnReportScreen()
        {
            if (driver._isElementPresent("xpath", "//span[@class='lead' and contains(text(),'No items were found')]") || driver._isElementPresent("xpath", "//span[@class='lead' and contains(text(),'No results found')]"))
            {
                PromoDashboard promoDashboard = new PromoDashboard(driver, test);
                promoDashboard.clickOnFilterFieldAndVerifyOrClickOptions("Days", "Last 6 Months");
            }

            return new Schedule(driver, test);
        }

        /// <summary>
        /// Verify Menu Icons on Top of Screen
        /// </summary>
        /// <param name="iconsName">Menu Icon Names to Verify</param>
        /// <returns></returns>
        public Schedule VerifyMenuIconOnTopOfScreen(string[] iconsName = null)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='baseexpand']"), "'Navigation Menu' Icon not Present on Page.");

            if (iconsName != null)
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='pull-right menuItem']"), "'Menu Icons' not Present on top of Screen.");
                IList<IWebElement> menuCollections = driver._findElements("xpath", "//div[@class='pull-right menuItem']");
                foreach (IWebElement menus in menuCollections)
                    Assert.AreEqual(iconsName[menuCollections.IndexOf(menus)], menus.Text, "'" + menus.Text + "' Menu Icon not Present on Top.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Menu Icons on Top of Screen.");
            return new Schedule(driver, test);
        }

        /// <summary>
        /// Verify Pagination for Grid Section
        /// </summary>
        /// <returns></returns>
        public Schedule VerifyPaginationForGridSection()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']"), "Pagination Section not Present.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-first page-item disabled']"), "First Icon Default Disable not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-prev page-item disabled']"), "Previous Icon Default Disable not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-page page-item active']"), "Actice First Page not Present.");
            if (driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-next page-item']") == false)
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-next page-item disabled']"), "Next Icon not Present for Grid.");
            if (driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-last page-item']") == false)
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-last page-item disabled']"), "Last Icon not Present for Grid.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']//.//button[contains(text(),'10')]"), "Item Per Page '10' not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']//.//button[contains(text(),'25')]"), "Item Per Page '25' not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']//.//button[contains(text(),'50')]"), "Item Per Page '50' not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row cft-pagination-wrapper']//.//button[contains(text(),'100')]"), "Item Per Page '100' not Present.");

            Results.WriteStatus(test, "Pass", "Verified, Pagination for Grid section.");
            return new Schedule(driver, test);
        }

        /// <summary>
        /// Verify Thumbnail Section on Screen
        /// </summary>
        /// <returns></returns>
        public Schedule VerifyThumbnailSectionOnScreen()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='aditem aditem-long']"), "Ad Thumbnail not Present for Table View Screen.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='aditem aditem-long']//.//img"), "Ad Image not Present on Section.");
            IWebElement image = driver._findElement("xpath", "//div[@class='aditem aditem-long']//.//img");
            bool loaded = Convert.ToBoolean(((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].complete && typeof arguments[0].naturalWidth != 'undefined' && arguments[0].naturalWidth > 0", image));
            Assert.AreEqual(true, loaded, "'(" + image.GetAttribute("src") + ")' Image Not Load on Thumbnail Section.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='aditem aditem-long']//.//div[@class='detail-view-content']"), "Detail View section not Present on Ad Image Section.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='aditem aditem-long']//.//button[contains(text(),'View Ad')]"), "View Ad Icon not Present on Ad Image.");
            if (driver._isElementPresent("xpath", "//div[@class='aditem aditem-long']//.//button[contains(text(),'Markets')]"))
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='aditem aditem-long']//.//button[contains(text(),'Markets')]"), "Markets Icon not Present on Ad Image.");
            else
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='aditem aditem-long']//.//button[contains(text(),'Occurrences')]"), "Occurrences Icon not Present on Ad Image.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='aditem aditem-long']//.//button[contains(text(),'Details')]"), "Details Icon not Present on Ad Image.");

            Results.WriteStatus(test, "Pass", "Verified, Thumbnail Section on Screen.");
            return new Schedule(driver, test);
        }

        /// <summary>
        /// Create New Search Or Click Saved Search To Apply Search On Screen
        /// </summary>
        /// <returns></returns>
        public String createNewSearchOrClickSavedSearchToApplySearchOnScreen(bool applySavedSearch = true)
        {
            string scheduleSearchName = "";
            IList<IWebElement> menus = driver.FindElements(By.XPath("//ul[@class='nav nav-tabs modal-tabs']/li"));
            bool avail = false;
            for (int i = 0; i < menus.Count; i++)
                if (menus[i].Text.Contains("Saved Searches") == true)
                {
                    if (menus[i].GetAttribute("class") != "disabled")
                    {
                        avail = true;
                        driver._clickByJavaScriptExecutor("//ul[@class='nav nav-tabs modal-tabs']/li[" + (i + 1) + "]/a");
                        Results.WriteStatus(test, "Pass", "Clicked, Saved Searches Button on Screen.");
                        break;
                    }
                }

            if (avail == false)
            {
                Search serachPage = new Search(driver, test);
                serachPage.selectDateRangeOptionFromSection("Random");

                clickButtonOnScreen("Save As");

                Assert.IsTrue(driver._waitForElement("xpath", "//input[contains(@placeholder,'What would you like to call your search') and @type='text']", 20), "'What would you like to call your search?' textarea not Present.");
                driver._clickByJavaScriptExecutor("//input[contains(@placeholder,'What would you like to call your search') and @type='text']");
                scheduleSearchName = "Test" + driver._randomString(4, true);
                driver._type("xpath", "//input[contains(@placeholder,'What would you like to call your search') and @type='text']", scheduleSearchName);
                Results.WriteStatus(test, "Pass", "Entered Save As Search Report Name on Screen.");

                clickButtonOnScreen("Save!");
                if (applySavedSearch)
                    clickButtonOnScreen("Apply Search");
                else
                    createNewSearchOrClickSavedSearchToApplySearchOnScreen(false);
                driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Loading Pivot Data')]");
            }
            else
            {
                if (applySavedSearch)
                {
                    IList<IWebElement> savedSearches = driver.FindElements(By.XPath("//button[contains(@class,'btn-block custom-btn-default') and contains(text(),'Apply Search')]"));
                    scheduleSearchName = driver.FindElement(By.XPath("//div[@class='checkbox-header-lead-text cursor-pointer']")).Text;
                    savedSearches[0].Click();
                    Thread.Sleep(2000);
                    Results.WriteStatus(test, "Pass", "Clicked Saved Seached and Apply Saved Sareches.");
                    driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Loading Pivot Data')]");
                }
            }

            driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Loading')]");
            Thread.Sleep(2000);
            return scheduleSearchName;
        }

        /// <summary>
        /// Verify Tooltip message on filter section
        /// </summary>
        /// <param name="message">Tooltip Message to Verify</param>
        /// <returns></returns>
        public Schedule VerifyTooltipMessageOrClickButtonOnScreen(string buttonName, string message, bool clickButton = false)
        {
            driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Loading Pivot Data')]");
            Thread.Sleep(2000);

            IList<IWebElement> buttons = driver.FindElements(By.XPath("//div[@class='btn-group btn-grid-actions']//.//button"));
            for (int i = 0; i < buttons.Count; i++)
                if (buttons[i].Text.Contains(buttonName) == true)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", buttons[i]);
                    ((IJavaScriptExecutor)driver).ExecuteScript("javascript:window.scrollBy(0,-350)");
                    Actions action = new Actions(driver);
                    action.MoveToElement(buttons[i]).MoveByOffset(2, 2).Perform();
                    Thread.Sleep(500);
                    //driver.MouseHoverByJavaScript(buttons[i]);
                    if (clickButton)
                    {
                        buttons[i].Click();
                        Results.WriteStatus(test, "Pass", "Clicked on '" + buttonName + "' Button on Screen.");
                    }
                    break;
                }

            if (clickButton == false)
            {
                Assert.AreEqual(true, driver._waitForElement("xpath", "//div[@class='tooltip-inner']"), "Tooltip not Present on Screen.");
                Assert.AreEqual(message, driver._getText("xpath", "//div[@class='tooltip-inner']"), "'" + message + "' Tooltip message not match.");
                Results.WriteStatus(test, "Pass", "Verified, '" + message + "' Tooltip message for '" + buttonName + "' Button on Screen.");
            }
            return new Schedule(driver, test);
        }

        /// <summary>
        /// Verify Schedule Window
        /// </summary>
        /// <param name="searchName">Search Name to Verify</param>
        /// <returns></returns>
        public Schedule VerifyScheduleWindow(string searchName)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='popover-content popover-body']", 20), "Schedule Window not Present on Screen.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//input[@class='form-control' and @placeholder='" + searchName + "']"), "'" + searchName + "' Search Name Default not display.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//span[@class='fa fa-check text-success form-control-feedback']"), "'" + searchName + "' Search Name Feedback in Green Right color not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default dropdown-toggle']"), "Schedule Dropdown not Present.");
            //Assert.AreEqual(true, driver._getText("xpath", "//button[@class='btn btn-default dropdown-toggle']").Contains("Daily"), "Schedule Dropdown Default 'Daily' not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row']/span[contains(text(),'" + searchName + "')]"), "'" + searchName + " will be delivered every day.' not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Create Scheduled Export')]"), "'Create Scheduled Export' Button not Present.");

            Results.WriteStatus(test, "Pass", "Verified, Schedule Window on screen.");
            return new Schedule(driver, test);
        }

        /// <summary>
        /// Click Schedule Dropdown and Verify Lists
        /// </summary>
        /// <returns></returns>
        public Schedule clickScheduleDropdownAndVerifyListsOrClick(string scheduleOption = "")
        {
            driver._clickByJavaScriptExecutor("//div[@class='btn-group']/button[@class='btn btn-default dropdown-toggle']");

            IList<IWebElement> lists = driver.FindElements(By.XPath("//div[@class='btn-group open']/ul/li/a"));
            string[] listNames = { "Daily", "Weekly", "Monthly" };
            //string[] listNames = { "Weekly", "Monthly" };
            for (int i = 0; i < listNames.Length; i++)
                if (scheduleOption != "")
                {
                    if (scheduleOption.Equals(lists[i].Text))
                    {
                        lists[i].Click();
                        Thread.Sleep(500);
                        break;
                    }
                }
                else
                    Assert.AreEqual(lists[i].Text, listNames[i], "'" + lists[i].Text + "' Option not Present on Schedule Dropdown.");

            Results.WriteStatus(test, "Pass", "Clicked, Schedule Dropdown and Verified Lists.");
            return new Schedule(driver, test);
        }

        /// <summary>
        /// Verify All Days label or Select on Schedule window
        /// </summary>
        /// <param name="day">Day Name to Select</param>
        /// <returns></returns>
        public Schedule VerifyAllDaysLabelOrSelectOnScheduleWindow(string day)
        {
            IList<IWebElement> dayLists = driver.FindElements(By.XPath("//div[@class='btn-group btn-group-no-padding']/button"));
            string[] dayTitles = { "S", "M", "T", "W", "T", "F", "S" };
            for (int i = 0; i < dayTitles.Length; i++)
                if (day != "")
                {
                    dayLists[i].Click();
                    Thread.Sleep(500);
                    Results.WriteStatus(test, "Pass", "Selected, '" + day + "' Day On Schedule Window.");
                    break;
                }
                else
                    Assert.AreEqual(dayLists[i].Text, dayTitles[i], "'" + dayLists[i].Text + "' Day Option not Present on Schedule Dropdown.");

            Results.WriteStatus(test, "Pass", "Verified, All Days label on Window.");
            return new Schedule(driver, test);
        }

        /// <summary>
        /// Verify Monthly section on schedule window
        /// </summary>
        /// <returns></returns>
        public Schedule VerifyMonthlySectionOnScheduleWindow()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row clickable-row']"), "");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='input-group CFT-spinner']/input"), "Day input text area not present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='row']/span[contains(text(),'will be delivered every')]"), " 'will be delivered every' not Present.");

            Results.WriteStatus(test, "Pass", "Verified, Monthly Section on Schedule window.");
            return new Schedule(driver, test);
        }

        /// <summary>
        /// Verify Message for Month on Schedule window
        /// </summary>
        /// <param name="message">Verify message</param>
        /// <returns></returns>
        public Schedule VerifyMessageForTheMonthOnScheduleWindow(string message)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='row']/span[contains(text(),'" + message + "')]", 10), "Invalid Message not Present.");
            Results.WriteStatus(test, "Pass", "Verified, Message for the Month on Schedule  window.");
            return new Schedule(driver, test);
        }

        /// <summary>
        /// Enter Day on Monthly section on Schedule window
        /// </summary>
        /// <returns></returns>
        public String enterDayInMonthlySectionOnScheduleWindow(string dayValue)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='input-group CFT-spinner']/input"), "Day input text area not present.");
            string day = driver._randomString(1, true);
            if (dayValue.Equals("") == false)
                day = dayValue;
            driver._clickByJavaScriptExecutor("//div[@class='input-group CFT-spinner']/input");
            driver._type("xpath", "//div[@class='input-group CFT-spinner']/input", day);
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.XPath("//div[@class='input-group CFT-spinner']/input"))).MoveByOffset(0, 50).Click().Perform();
            Thread.Sleep(1000);

            Results.WriteStatus(test, "Pass", "Entered, '" + "" + "' Day in Monthly section on Schedule window.");
            return day;
        }

        /// <summary>
        /// Click Button on Screen
        /// </summary>
        /// <param name="buttonName">Button Name to Click</param>
        /// <returns></returns>
        public Schedule clickButtonOnScreen(string buttonName)
        {
            if (buttonName.Equals("Create Scheduled Export"))
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//button[@class='btn btn-default' and contains(text(),'Create Scheduled Export')]", 20), "'Create Scheduled Export' Button not Present.");
                driver._clickByJavaScriptExecutor("//button[@class='btn btn-default' and contains(text(),'Create Scheduled Export')]");
            }

            if (buttonName.Equals("Save As"))
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//button[@class='btn btn-default' and contains(text(),'Save As')]", 20), "'Save As...' Button not Present.");
                driver._clickByJavaScriptExecutor("//button[@class='btn btn-default' and contains(text(),'Save As')]");
            }

            if (buttonName.Equals("Save!"))
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//button[@class='btn btn-success' and contains(text(),'Save!')]", 20), "'Save!' Button not Present.");
                driver._clickByJavaScriptExecutor("//button[@class='btn btn-success' and contains(text(),'Save!')]");
            }

            if (buttonName.Equals("Apply Search"))
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//button[@class='btn btn-primary' and contains(text(),'Apply Search')]", 20), "'Apply Search' Button not Present.");
                driver._clickByJavaScriptExecutor("//button[@class='btn btn-primary' and contains(text(),'Apply Search')]");
            }

            Thread.Sleep(2000);
            Results.WriteStatus(test, "Pass", "Clicked, '" + buttonName + "' Button on screen.");
            return new Schedule(driver, test);
        }

        /// <summary>
        /// Verify Schedule Message on Screen
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        public Schedule VerifyScheduleMessageOnScreen(string message)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-scheduled-export-popover-form//.//span"), "Successfully created a scheduled export for Message not Present.");
            Assert.AreEqual(true, driver._getText("xpath", "//cft-scheduled-export-popover-form//.//span").Contains(message), "'" + message + "' message not match.");
            Results.WriteStatus(test, "Pass", "Verified, '" + message + "' Message on Screen.");
            return new Schedule(driver, test);
        }


        #region New Methods

        ///<summary>
        ///Verify Schedule Icon
        ///</summary>
        ///<returns></returns>
        public Schedule VerifyScheduleIcon(bool searchModified = false)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-saved-search-dropdown//button[@dropdowntoggle]"), "'Load Search' Button is not present.");

            if(driver._getText("xpath", "//cft-saved-search-dropdown//button[@dropdowntoggle]").ToLower().Contains("edit search"))
            {
                Assert.IsFalse(driver._isElementPresent("xpath", "//cft-saved-search-dropdown//cft-scheduled-export-modal//button"), "'Schedule' Icon is present in top right corner when no saved search is applied.");
                IList<IWebElement> chartScheduleIconColl = driver._findElements("xpath", "//chart-export//cft-scheduled-export-modal//button");
                Assert.Greater(chartScheduleIconColl.Count, 0, "Chart Schedule Icons not present when no saved search is applied.");
                Assert.IsTrue(chartScheduleIconColl[0].GetAttribute("disabled") != null, "'Schedule' Icons present in charts are not diabled when no saved search is applied.");
                driver._scrollintoViewElement("xpath", "//div[1]/div/chart//chart-export/div");
                Actions action = new Actions(driver);
                action.MoveToElement(chartScheduleIconColl[0]).MoveByOffset(-1, -1).Perform();
                Thread.Sleep(1000);
                Assert.IsTrue(driver._isElementPresent("xpath", "//bs-tooltip-container[contains(@id, 'tooltip')]//div[text()='You must have an applied saved search']"), "Tooltip with text 'You must have an applied saved search' not present for Schedule Icons on charts.");
            }
            else if (homePage.getActiveScreenNameFromSideNavigationBar().ToLower().Contains("quarterly") || homePage.getActiveScreenNameFromSideNavigationBar().ToLower().Contains("annual") || homePage.getActiveScreenNameFromSideNavigationBar().ToLower().Contains("year"))
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-saved-search-dropdown//cft-scheduled-export-modal//button"), "'Schedule' Icon is not present in top right corner when saved search is applied.");
                Actions action = new Actions(driver);
                action.MoveToElement(driver.FindElement(By.XPath("//cft-saved-search-dropdown//cft-scheduled-export-modal//button"))).Perform();
                Thread.Sleep(1000);
                Assert.IsTrue(driver._isElementPresent("xpath", "//bs-tooltip-container[contains(@id, 'tooltip')]//div[text()='Scheduled exports are currently unavailable for your current report type']"), "Tooltip with text 'Scheduled exports are currently unavailable for your current report type' not present for Schedule Icon on Upper Left Corner in Quarterly/Yearly Reports.");
            }
            else
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-saved-search-dropdown//cft-scheduled-export-modal//button"), "'Schedule' Icon is not present in top right corner when saved search is applied.");
                IList<IWebElement> chartScheduleIconColl = driver._findElements("xpath", "//chart-export//cft-scheduled-export-modal//button");
                int n = -1;
                for(int i = 0; i < chartScheduleIconColl.Count; i++)
                {
                    if (chartScheduleIconColl[i].Displayed)
                    {
                        n = i;
                        break;
                    }
                }
                Assert.Greater(n, -1, "Schedule Icon on chart is not present.");

                if (searchModified)
                {
                    Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-saved-search-dropdown//cft-scheduled-export-modal//button", "disabled") != null, "'Schedule' Icon is not disabled in top right corner when saved search is applied.");
                    Assert.IsTrue(chartScheduleIconColl[n].GetAttribute("disabled") != null, "'Schedule' Icons present in charts are not diabled when no saved search is applied.");
                    driver._scrollintoViewElement("xpath", "//div[1]/div/chart//chart-export/div");
                    Actions action = new Actions(driver);
                    action.MoveToElement(chartScheduleIconColl[n]).MoveByOffset(-1, -1).Perform();
                    Thread.Sleep(1000);
                    Assert.IsTrue(driver._isElementPresent("xpath", "//bs-tooltip-container[contains(@id, 'tooltip')]//div[text()='Please reset your saved search before you can create an alert']"), "Tooltip with text 'Please reset your saved search before you can create an alert' not present for Schedule Icons on charts.");
                    action.MoveToElement(driver.FindElement(By.XPath("//cft-saved-search-dropdown//cft-scheduled-export-modal//button"))).Perform();
                    Thread.Sleep(1000);
                    Assert.IsTrue(driver._isElementPresent("xpath", "//bs-tooltip-container[contains(@id, 'tooltip')]//div[text()='Please reset your saved search before you can create an alert']"), "Tooltip with text 'Please reset your saved search before you can create an alert' not present for Schedule Icon on Upper Left Corner.");
                }
                else
                {
                    Assert.IsTrue(chartScheduleIconColl[n].GetAttribute("disabled") == null, "'Schedule' Icons present in charts are diabled when saved search is applied.");
                    driver._scrollintoViewElement("xpath", "//div[1]/div/chart//chart-export/div");
                    Actions action = new Actions(driver);
                    action.MoveToElement(chartScheduleIconColl[n]).MoveByOffset(-1, -1).Perform();
                    Thread.Sleep(1000);
                    Assert.IsTrue(driver._isElementPresent("xpath", "//bs-tooltip-container[contains(@id, 'tooltip')]//div[text()='Create an alert from this saved search']"), "Tooltip with text 'Create an alert from this saved search' not present for Schedule Icons on charts.");
                }
            }

            Results.WriteStatus(test, "Pass", "Verified, Schedule Icon.");
            return new Schedule(driver, test);
        }

        ///<summary>
        ///Verify Schedule Popup Of Saved Search
        ///</summary>
        ///<param name="popupVisible">Whether popup should be visible or not</param>
        ///<param name="searchName">Search Name to be verified in Export Field</param>
        ///<return></return>
        public Schedule VerifySchedulePopupOfSavedSearch(string searchName, bool popupVisible = true)
        {
            if (popupVisible)
            {
                driver._click("xpath", "//cft-saved-search-dropdown//cft-scheduled-export-modal//button");
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-scheduled-export-modal-form//form"), "'Schedule' Popup is not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//h4"), "'Schedule' Popup header is not present.");
                Assert.AreEqual("Schedule an Export ...", driver._getText("xpath", "//cft-scheduled-export-modal-form//h4"), "'Schedule' Popup header text does not match.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//span[text()='Export']"), "'Export' Label not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//button[contains(@class, 'disabled')]/div[@class='NU-tag-label']"), "'Export' field not present.");
                Assert.AreEqual(searchName.ToLower(), driver._getText("xpath", "//cft-scheduled-export-modal-form//button[contains(@class, 'disabled')]/div[@class='NU-tag-label']").ToLower(), "'Search Name' does not match.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//span[text()='every']"), "'Every' Label not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//div[@class='row pb-2']//button[2]"), "'Every' field not present.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//span[text()='as a(n)']"), "'Every' Label not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//div[@class='row pb-2']//button[3]"), "'as a(n)' field not present.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//cft-export-icon"), "'Export' Icon not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//button[contains(@class, 'disabled') and text()=' Excel ']"), "'Excel' Button not present");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//div[@class='modal-footer pt-0']//button[text()='Cancel']"), "'Cancel' button not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//div[@class='modal-footer pt-0']//button[text()='Confirm']"), "'Confirm' button not present");
            }
            else
                Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//cft-scheduled-export-modal-form//form"), "'Schedule Popup is still present.'");

            Results.WriteStatus(test, "Pass", "Verified, Schedule Popup Of Saved Search.");
            return new Schedule(driver, test);
        }

        ///<summary>
        ///Verify and Edit Frequency View Of Schedule Popup
        ///</summary>
        ///<param name="dayOfMonth">Day of Month for Frequency 'Monthly'</param>
        ///<param name="weeDay">Day of Week for Frequency 'Weekly'</param>
        ///<returns></returns>
        public Schedule VerifyAndEditFrequencyViewOfSchedulePopup(string weekDay = "", string dayOfMonth = "", string reportName = "Dashboard", bool fromChart = false)
        {
            if (!fromChart)
            {
                driver._click("xpath", "//cft-scheduled-export-modal-form//div[@class='row pb-2']//button[2]");
                Thread.Sleep(1000);
                Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-scheduled-export-modal-form//div[@class='row pb-2']//button[2]", "class").Contains("active"), "Frequency View is not selected.");
            }
            else
            {
                if(!driver._waitForElement("xpath", "//cft-scheduled-export-modal-form//form"))
                {
                    IList<IWebElement> chartScheduleIconColl = driver._findElements("xpath", "//chart-export//cft-scheduled-export-modal//button");
                    chartScheduleIconColl[0].Click();
                }
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-scheduled-export-modal-form//form"), "'Schedule' Popup is not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//h4"), "'Schedule' Popup header is not present.");
                Assert.AreEqual("Schedule an Export ...", driver._getText("xpath", "//cft-scheduled-export-modal-form//h4"), "'Schedule' Popup header text does not match.");
            }

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-scheduled-export-modal-form//div[@class='row']//label"), "Radio Buttons not present");
            IList<IWebElement> radioButtonCollection = driver._findElements("xpath", "//cft-scheduled-export-modal-form//div[@class='row']//label");
            bool avail = false;

            if (reportName.ToLower().Contains("dashboard"))
            {
                string[] optionsList = { "Daily", "Weekly" };
                foreach(string option in optionsList)
                {
                    foreach (IWebElement radioButton in radioButtonCollection)
                    {
                        if (radioButton.Text.Contains(option))
                        {
                            avail = true;
                            break;
                        }
                    }
                    Assert.IsTrue(avail, "'" + option + "' Radio Button not present.");
                }
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-scheduled-export-modal-form//div[@class='row']//span[text()='Daily']"), "'Daily' Radio Button not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//div[@class='row']//span[text()='Weekly']"), "'Weekly' Radio Button not present");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//div[@class='btn-group']//button"), "WeekDays buttons not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//button[contains(@class, 'disabled') and text()=' Daily ']"), "'Daily' Button not present");

                if (weekDay == "" && dayOfMonth == "")
                {
                    driver._click("xpath", "//cft-scheduled-export-modal-form//div[@class='row']//span[text()='Daily']");
                    Thread.Sleep(2000);
                    if (!fromChart)
                        Assert.AreEqual("day", driver._getText("xpath", "//cft-scheduled-export-modal-form//div[@class='row pb-2']//button[2]/div[@class='NU-tag-label']"), "'Day' value of Frequency field does not match.");
                    Assert.AreEqual(true, driver.FindElement(By.XPath("//cft-scheduled-export-modal-form//div[@class='row'][1]//input")).Selected, "'Daily' Radio Button not selected.");
                    Results.WriteStatus(test, "Pass", "Selected, 'Daily' from Frequency View Of Schedule Popup.");
                }
            }
            else if (reportName.ToLower().Contains("weekly"))
            {
                avail = false;
                foreach (IWebElement radioButton in radioButtonCollection)
                {
                    if (radioButton.Text.Contains("Weekly"))
                    {
                        avail = true;
                        break;
                    }
                }
                Assert.IsTrue(avail, "'Weekly' Radio Button not present.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//div[@class='btn-group']//button"), "WeekDays buttons not present.");
            }

            avail = false;
            foreach (IWebElement radioButton in radioButtonCollection)
            {
                if (radioButton.Text.Contains("Monthly"))
                {
                    avail = true;
                    break;
                }
            }
            Assert.IsTrue(avail, "'Monthly' Radio Button not present.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//div[text()=' On day ']"), "'On day' text not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//div[text()=' of each month ']"), "'of each month' text not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//input[@type='number']"), "'Day of Month' text field not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//div[contains(@class, 'input-group-append')]/button"), "'Day of Month' Move Up and Move Down buttons not present");

            int selectedDate = 0, lastDate = 0;
            int.TryParse(driver._getValue("xpath", "//cft-scheduled-export-modal-form//input[@type='number']"), out selectedDate);
            DateTime today = DateTime.Today;
            lastDate = DateTime.DaysInMonth(today.Year, today.Month);

            while (selectedDate < 31)
            {
                driver._click("xpath", "//cft-scheduled-export-modal-form//div[contains(@class, 'input-group-append')]/button[1]");
                int.TryParse(driver._getValue("xpath", "//cft-scheduled-export-modal-form//input[@type='number']"), out selectedDate);
            }
            Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-scheduled-export-modal-form//div[contains(@class, 'input-group-append')]/button[1]", "disabled") != null, "Move Up button did not get disabled.");

            if(weekDay != "")
            {
                int index = -1;
                if (weekDay.ToLower().Equals("today"))
                    weekDay = today.ToString("dddd");
                switch (weekDay.ToLower())
                {
                    case "sunday":
                        index = 0;
                        break;
                    case "monday":
                        index = 1;
                        break;
                    case "tuesday":
                        index = 2;
                        break;
                    case "wednesday":
                        index = 3;
                        break;
                    case "thursday":
                        index = 4;
                        break;
                    case "friday":
                        index = 5;
                        break;
                    case "saturday":
                        index = 6;
                        break;
                    default:
                        break;
                }

                IList<IWebElement> weekDaysButtonColl = driver._findElements("xpath", "//cft-scheduled-export-modal-form//div[@class='btn-group']//button");
                weekDaysButtonColl[index].Click();
                Thread.Sleep(1000);
                if (!fromChart)
                    Assert.AreEqual(weekDay.ToLower(), driver._getText("xpath", "//cft-scheduled-export-modal-form//div[@class='row pb-2']//button[2]/div[@class='NU-tag-label']").ToLower(), "'Day' value of Frequency field does not match.");

                avail = false;
                foreach (IWebElement radioButton in radioButtonCollection)
                {
                    if (radioButton.Text.Contains("Weekly"))
                    {
                        avail = true;
                        IList<IWebElement> inputCollection = radioButton._findElementsWithinElement("xpath", ".//input");
                        Assert.AreEqual(true, inputCollection[0].Selected, "'Weekly' Radio Button not present.");
                        break;
                    }
                }
                Assert.IsTrue(avail, "'Weekly' Radio Button not present.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//button[contains(@class, 'disabled') and text()=' Weekly ']"), "'Daily' Button not present");
                Results.WriteStatus(test, "Pass", "Selected, 'Weekly' and Day '" + weekDay + "' of Week from Frequency View Of Schedule Popup.");
            }
            else if(dayOfMonth != "")
            {
                if (dayOfMonth.ToLower().Equals("today"))
                    dayOfMonth = today.Day.ToString();
                if (!driver._getValue("xpath", "//cft-scheduled-export-modal-form//input[@type='number']").Equals(dayOfMonth))
                {
                    int expectedDate = 0;
                    int.TryParse(driver._getValue("xpath", "//cft-scheduled-export-modal-form//input[@type='number']"), out selectedDate);
                    int.TryParse(dayOfMonth, out expectedDate);

                    while (selectedDate < expectedDate)
                    {
                        driver._click("xpath", "//cft-scheduled-export-modal-form//div[contains(@class, 'input-group-append')]/button[1]");
                        int.TryParse(driver._getValue("xpath", "//cft-scheduled-export-modal-form//input[@type='number']"), out selectedDate);
                    }
                    while (selectedDate > expectedDate)
                    {
                        driver._click("xpath", "//cft-scheduled-export-modal-form//div[contains(@class, 'input-group-append')]/button[2]");
                        int.TryParse(driver._getValue("xpath", "//cft-scheduled-export-modal-form//input[@type='number']"), out selectedDate);
                    }
                }
                else
                    driver._click("xpath", "//cft-scheduled-export-modal-form//div[@class='row']//span[text()='Monthly']");
                Thread.Sleep(1000);
                if (dayOfMonth.ToCharArray()[dayOfMonth.ToCharArray().Length - 1] == '1' && !dayOfMonth.Equals("11"))
                    dayOfMonth = dayOfMonth + "st";
                else if (dayOfMonth.ToCharArray()[dayOfMonth.ToCharArray().Length - 1] == '2' && !dayOfMonth.Equals("12"))
                    dayOfMonth = dayOfMonth + "nd";
                else if (dayOfMonth.ToCharArray()[dayOfMonth.ToCharArray().Length - 1] == '3')
                    dayOfMonth = dayOfMonth + "rd";
                else
                    dayOfMonth = dayOfMonth + "th";
                if (!fromChart)
                    Assert.AreEqual(dayOfMonth + " day of the month", driver._getText("xpath", "//cft-scheduled-export-modal-form//div[@class='row pb-2']//button[2]/div[@class='NU-tag-label']"), "'Day' value of Frequency field does not match.");

                avail = false;
                foreach (IWebElement radioButton in radioButtonCollection)
                {
                    if (radioButton.Text.Contains("Monthly"))
                    {
                        avail = true;
                        IList<IWebElement> inputCollection = radioButton._findElementsWithinElement("xpath", ".//input");
                        Assert.AreEqual(true, inputCollection[0].Selected, "'Monthly' Radio Button not present.");
                        break;
                    }
                }
                Assert.IsTrue(avail, "'Monthly' Radio Button not present.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//button[contains(@class, 'disabled') and text()=' Monthly ']"), "'Daily' Button not present");
                Results.WriteStatus(test, "Pass", "Selected, 'Monthly' and Day '" + dayOfMonth + "' of Month from Frequency View Of Schedule Popup.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Frequency View Of Schedule Popup.");
            return new Schedule(driver, test);
        }

        ///<summary>
        ///Verify and Edit File Type View Of Schedule Popup
        ///</summary>
        ///<param name="fileType">Export Format to be selected</param>
        ///<returns></returns>
        public Schedule VerifyAndEditFileTypeViewOfSchedulePopup(string fileType = "")
        {
            driver._click("xpath", "//cft-scheduled-export-modal-form//div[@class='row pb-2']//button[3]");
            Thread.Sleep(1000);
            Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-scheduled-export-modal-form//div[@class='row pb-2']//button[3]", "class").Contains("active"), "File Type View is not selected.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal-form//label/span"), "'Export Formats' not present on Select An Export Type Popup");

            if(fileType != "")
            {
                IList<IWebElement> exportFormatCollection = driver._findElements("xpath", "//cft-scheduled-export-modal-form//label/span");
                bool avail = false;
                foreach (IWebElement exportFormat in exportFormatCollection)
                    if (exportFormat.Text.ToLower().Contains(fileType.ToLower()))
                    {
                        avail = true;
                        exportFormat.Click();
                        Thread.Sleep(2000);
                        Assert.AreNotEqual(null, exportFormat.GetCssValue("background"), "'" + fileType + "' is not selected by default.");
                        break;
                    }
                Assert.IsTrue(avail, "'" + fileType + "' not found in Export Formats");
                Results.WriteStatus(test, "Pass", "Selected, '" + fileType + "' Export Format from File Type View Of Schedule Popup.");
            }

            Results.WriteStatus(test, "Pass", "Verified, File Type View Of Schedule Popup.");
            return new Schedule(driver, test);
        }

        ///<summary>
        ///Click Button on Schedule Popup
        ///</summary>
        ///<param name="buttonName">Button to be clicked</param>
        ///<returns></returns>
        public Schedule clickButtonOnSchedulePopup(string buttonName)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button[not(contains(@class,'d-none'))]"), "Buttons not present on Pivot Options Popup footer");
            IList<IWebElement> buttonCollection = driver._findElements("xpath", "//div[@class='modal-footer pt-0']//button[not(contains(@class,'d-none'))]");

            bool avail = false;
            foreach (IWebElement button in buttonCollection)
                if (button.Text.ToLower().Contains(buttonName.ToLower()))
                {
                    avail = true;
                    button.Click();
                    break;
                }
            Assert.IsTrue(avail, "'" + buttonName + "' not found in Pivot Options Footer buttons.");
            Thread.Sleep(2000);

            Results.WriteStatus(test, "Pass", "Clicked, '" + buttonName + "' Button on Pivot Options Popup.");
            return new Schedule(driver, test);
        }

        #endregion

        #endregion
    }
}
