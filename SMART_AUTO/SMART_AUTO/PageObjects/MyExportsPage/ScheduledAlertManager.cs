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
using OpenQA.Selenium.Interactions;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Collections;

namespace SMART_AUTO
{
    public class ScheduledAlertManager
    {
        #region Private Variables

        private IWebDriver scheduledAlertManager;
        private ExtentTest test;

        #endregion

        public ScheduledAlertManager(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.scheduledAlertManager = driver;
            test = testReturn;
        }

        public IWebDriver driver
        {
            get { return this.scheduledAlertManager; }
            set { this.scheduledAlertManager = value; }
        }

        ///<summary>
        ///Verify Scheduled Alerts Manager Screen
        ///</summary>
        ///<returns></returns>
        public ScheduledAlertManager VerifyScheduledAlertsManagerScreen()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-scheduled-alerts-manager/div"), "'Scheduled Alerts Manager' Screen not present.");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-scheduled-alerts-manager//h1"), "'Scheduled Alerts Manager' Screen header not present.");
            Assert.AreEqual("Scheduled Alerts Manager", driver._getText("xpath", "//cft-scheduled-alerts-manager//h1"), "'Scheduled Alerts Manager' Screen header text does not match.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'text-numerator-gothic')]"), "Active Report Label not present.");
            Assert.IsTrue(driver._getText("xpath", "//div[contains(@class, 'text-numerator-gothic')]").ToLower().Contains("Active Report".ToLower()), "Active Report Label text does not match.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//ng-select[@role='listbox']"), "Active Report DDL not present.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//table//th"), "Columns not present.");
            IList<IWebElement> columnCollection = driver._findElements("xpath", "//table//th");
            string[] columnNamesList = new string[] { "SAVED SEARCH", "SCHEDULED TYPE", "FORMAT", "TYPE", "MODIFIED DATE", "ACTIONS" };

            foreach(string columnName in columnNamesList)
            {
                bool avail = false;
                foreach(IWebElement column in columnCollection)
                    if (column.Text.ToLower().Contains(columnName.ToLower()))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "'" + columnName + "' Column not found.");
            }

            if(driver._isElementPresent("xpath", "//table//tr/td"))
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//tr/td[1]"), "Saved Searches Column not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//tr/td[2]"), "Scheduled Type Column not present.");
                Assert.IsTrue(driver._getText("xpath", "//tr/td[2]").Contains("daily") || driver._getText("xpath", "//tr/td[2]").Contains("weekly") || driver._getText("xpath", "//tr/td[2]").Contains("Monthly"), "Scheduled Type is not correct.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//tr/td[3]"), "Format Column not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//tr/td[4]"), "Type Column not present.");
                Assert.IsTrue(driver._getText("xpath", "//tr/td[4]").Contains("excel") || driver._getText("xpath", "//tr/td[4]").Contains("powerpoint") || driver._getText("xpath", "//tr/td[4]").Contains("pdf") || driver._getText("xpath", "//tr/td[4]").Contains("zip"), "Type is not correct.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//tr/td[5]"), "Modified Date Column not present.");
                string sdate = driver._getText("xpath", "//tr/td[5]");
                DateTime date = DateTime.Today;
                sdate = sdate.Substring(0, sdate.IndexOf(","));
                //Assert.IsTrue(DateTime.TryParse(sdate, out date) , "Modified Date is not correct.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//tr/td[6]"), "Action Column not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//tr/td[6]//a"), "Action icon not present.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Scheduled Alerts Manager Screen");
            return new ScheduledAlertManager(driver, test);
        }

        ///<summary>
        ///Verify Report Names From Active Reports Dropdown
        ///</summary>
        ///<returns></returns>
        public ScheduledAlertManager VerifyReportNamesFromActiveReportsDropdown()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//ng-select[@role='listbox']"), "Active Report DDL not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//ng-select//span[@class='ng-value-label']"), "Active Report DDL value not present.");
            string selectedvalue = driver._getText("xpath", "//ng-select//span[@class='ng-value-label']");
            driver._click("xpath", "//ng-select[@role='listbox']");

            Assert.IsTrue(driver._isElementPresent("xpath", "//ng-dropdown-panel//div[@id]/span"), "Active Reports DDL Options not present.");
            IList<IWebElement> optionsColl = driver._findElements("xpath", "//ng-dropdown-panel//div[@id]/span");
            Assert.IsTrue(driver._isElementPresent("xpath", "//ng-dropdown-panel//div[@id and contains(@class, 'selected')]/span"), "Selected option not present.");
            Assert.AreEqual(selectedvalue, driver._getText("xpath", "//ng-dropdown-panel//div[@id and contains(@class, 'selected')]/span"), "Selected Report is not highlighted.");

            string[] optionsList = new string[optionsColl.Count];
            for (int i = 0; i < optionsColl.Count; i++)
                optionsList[i] = optionsColl[i].Text;

            Home home = new Home(driver, test);
            string[] sideBarOptionsList = home.getAllSidebarOptions();

            foreach(string option in optionsList)
            {
                bool avail = false;
                foreach(string sideBarOption in sideBarOptionsList)
                    if (sideBarOption.ToLower().Equals(option.ToLower()))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "'" + option + "' is not a report for this account.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Report Names From Active Reports Dropdown");
            return new ScheduledAlertManager(driver, test);
        }

        ///<summary>
        ///Select Active Report From Dropdown
        ///</summary>
        ///<param name="reportName">Name of report to select</param>
        ///<returns></returns>
        public ScheduledAlertManager SelectActiveReportFromDropdown(string reportName = "")
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//ng-select[@role='listbox']"), "Active Report DDL not present.");
            driver._click("xpath", "//ng-select[@role='listbox']");

            Assert.IsTrue(driver._isElementPresent("xpath", "//ng-dropdown-panel//div[@id]/span"), "Active Reports DDL Options not present.");
            IList<IWebElement> optionsColl = driver._findElements("xpath", "//ng-dropdown-panel//div[@id]");

            if(reportName == "")
            {
                Random rand = new Random();
                int x = rand.Next(0, optionsColl.Count - 1);
                IList<IWebElement> optionText = optionsColl[x]._findElementsWithinElement("xpath", ".//span");
                reportName = optionText[0].Text;
            }

            bool avail = false;
            foreach(IWebElement option in optionsColl)
            {
                IList<IWebElement> optionText = option._findElementsWithinElement("xpath", ".//span");
                if (optionText[0].Text.ToLower().Equals(reportName.ToLower()))
                {
                    avail = true;
                    option.Click();
                    break;
                }
            }
            Assert.IsTrue(avail, "'" + reportName + "' report not found.");
            Thread.Sleep(1000);

            Assert.IsTrue(driver._isElementPresent("xpath", "//ng-select//span[@class='ng-value-label']"), "Active Report DDL value not present.");
            string selectedvalue = driver._getText("xpath", "//ng-select//span[@class='ng-value-label']");
            Assert.AreEqual(selectedvalue.ToLower(), reportName.ToLower(), "'" + reportName + "' was not selected.");

            Results.WriteStatus(test, "Pass", "Select Active Report '" + reportName + "' From Dropdown");
            return new ScheduledAlertManager(driver, test);
        }

        ///<summary>
        ///Capture Saved Searches Name From Scheduled Alert Manager Screen
        ///</summary>
        ///<returns></returns>
        public string[] CaptureSavedSearchesNameFromScheduledAlertManagerScreen()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//tr/td[1]"), "Saved Searches Column Cells not present.");
            IList<IWebElement> cellCollection = driver._findElements("xpath", "//tr/td[1]");

            string[] searchList = new string[cellCollection.Count];
            for (int i = 0; i < cellCollection.Count; i++)
                searchList[i] = cellCollection[i].Text;

            Results.WriteStatus(test, "Pass", "Captured, Saved Searches Name From Scheduled Alert Manager Screen");
            return searchList;
        }

        ///<summary>
        ///Verify Actions Icon and Click Option
        ///</summary>
        ///<param name="option">Option to be clicked</param>
        ///<returns></returns>
        public string VerifyActionsIconAndClickOption(string option)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//tr/td[6]//a"), "Action icon not present.");
            IList<IWebElement> actionsIconColl = driver._findElements("xpath", "//tr/td[6]//a");

            Assert.IsTrue(driver._isElementPresent("xpath", "//tr/td[1]"), "Action icon not present.");
            IList<IWebElement> savedSearchesColl = driver._findElements("xpath", "//tr/td[1]");
            int currCount = savedSearchesColl.Count;

            Random rand = new Random();
            int x = rand.Next(0, actionsIconColl.Count - 1);

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", savedSearchesColl[x]);
            string savedSearch = savedSearchesColl[x].Text;
            actionsIconColl[x].Click();

            Assert.IsTrue(driver._waitForElement("xpath", "//table//button"), "Actions DDL not present.");
            IList<IWebElement> optionsColl = driver._findElements("xpath", "//table//button");

            bool avail = false;
            foreach(IWebElement button in optionsColl)
                if (button.Text.ToLower().Contains(option.ToLower()))
                {
                    avail = true;
                    button.Click();
                    break;
                }
            Assert.IsTrue(avail, "'" + option + "' option not present.");
            Thread.Sleep(1000);
            savedSearchesColl = driver._findElements("xpath", "//tr/td[1]");

            if (option.ToLower().Equals("delete"))
            {
                Assert.AreNotEqual(currCount, savedSearchesColl.Count, "'" + savedSearchesColl + "' not deleted.");
                Results.WriteStatus(test, "Pass", "'" + savedSearchesColl + "' is deleted.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Actions Icon and Clicked Option '" + option + "'");
            return savedSearch;
        }

        ///<summary>
        ///Search For Saved Search Name
        ///</summary>
        ///<returns></returns>
        public ScheduledAlertManager SearchForSavedSearchName(string savedSearchName, bool present = true)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//tr/td[1]"), "Action icon not present.");
            IList<IWebElement> savedSearchesColl = driver._findElements("xpath", "//tr/td[1]");

            bool avail = false;
            foreach(IWebElement savedSearch in savedSearchesColl)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", savedSearch);
                if (savedSearch.Text.ToLower().Equals(savedSearchName.ToLower()))
                {
                    avail = true;
                    break;
                }
            }
            if (present)
            {
                Assert.IsTrue(avail, "'" + savedSearchName + "' option not present.");
                Results.WriteStatus(test, "Pass", "'" + savedSearchName + "' option is present.");
            }
            else
            {
                Assert.IsFalse(avail, "'" + savedSearchName + "' option is present.");
                Results.WriteStatus(test, "Pass", "'" + savedSearchName + "' option is not present.");
            }

            Results.WriteStatus(test, "Pass", "Searched, For Saved Search Name '" + savedSearchName + "'");
            return new ScheduledAlertManager(driver, test);
        }

    }
}
