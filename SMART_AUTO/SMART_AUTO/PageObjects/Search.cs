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

namespace SMART_AUTO
{
    public class Search
    {
        #region Private Variables

        private IWebDriver search;
        private ExtentTest test;
        Home homePage;
        FieldOptions fieldOptions;
        SummaryTags summaryTags;

        #endregion

        #region Public Methods

        public Search(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.search = driver;
            test = testReturn;
            homePage = new Home(driver, test);
            fieldOptions = new FieldOptions(driver, test);
            summaryTags = new SummaryTags(driver, test);
        }

        public IWebDriver driver
        {
            get { return this.search; }
            set { this.search = value; }
        }

        /// <summary>
        /// Verify My Search screen
        /// </summary>
        /// <param name="accountName">Account Name to Verify Search Fields</param>
        /// <returns></returns>
        public Search VerifyMySearchScreen(string accountName = "QA Testing - Brand")
        {
            Assert.True(driver._waitForElement("xpath", "//ul[@class='nav nav-tabs modal-tabs']//.//a[contains(@ng-click,'my_search')]", 20), "'Edit Search' Button not Present.");
            Assert.IsTrue(driver._getText("xpath", "//ul[@class='nav nav-tabs modal-tabs']//.//a[contains(@ng-click,'my_search')]").Contains("Edit Search"), "'Edit Search' Button Label not match.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//ul[@class='nav nav-tabs modal-tabs']//.//a[contains(@ng-click,'saved_search')]"), "'Saved Searches' Button not Present.");
            Assert.AreEqual(true, driver._getText("xpath", "//ul[@class='nav nav-tabs modal-tabs']//.//a[contains(@ng-click,'saved_search')]").Contains("Saved Searches"), "'Saved Searches' Button Label not match.");

            Assert.AreEqual(true, driver._isElementPresent("id", "CftSearchMenu"), "Search Menu Section not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@menu-name='Basic']/div[@id='heading-Basic']"), "'Basic Fields' Tab not Present.");
            Assert.IsTrue(driver._getText("xpath", "//div[@menu-name='Basic']/div[@id='heading-Basic']").Contains("Basic Fields"), "'Basic Fields' Tab Label not match.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@menu-name='Other']/div[@id='heading-Other']"), "'Other Fields' Tab not Present.");
            Assert.IsTrue(driver._getText("xpath", "//div[@menu-name='Other']/div[@id='heading-Other']").Contains("Other Fields"), "'Other Fields' Tab Label not match.");

            if (accountName.Equals("QA Testing - Brand"))
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@menu-name='Basic']/div[@role='tabpanel']/button/span[text() = 'Date Range']"), "'Date Range' Menu Item not Present under Basic Fields Tab.");
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-timeframe-calendar/div[@class='list-group-item CFT-search-field']"), "'Date Range' Section not Present.");

                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@menu-name='Basic']/div[@role='tabpanel']/button/span[text() = 'Media']"), "'Media' Menu Item not Present under Basic Fields Tab.");
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[contains(@id,'occurrence_media')]"), "'Media' Section not Present.");

                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@menu-name='Basic']/div[@role='tabpanel']/button/span[text() = 'Market']"), "'Market' Menu Item not Present under Basic Fields Tab.");
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-ct_occurrence_dmaName']"), "'Market' Section not Present.");

                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@menu-name='Other']/div[@role='tabpanel']/button/span[text() = 'Ad Status']"), "'Ad Status' Menu Item not Present under Basic Fields Tab.");
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@menu-name='Other']/div[@role='tabpanel']/button/span[text() = 'Advertiser Product']"), "'Advertiser Product' Menu Item not Present under Basic Fields Tab.");
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@menu-name='Other']/div[@role='tabpanel']/button/span[text() = 'Category']"), "'Category' Menu Item not Present under Basic Fields Tab.");
            }

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[contains(@id,'chart-creative-mysearch')]"), "'Creatives' Chart not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and text() = 'Cancel']"), "'Cancel' Button not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-primary' and text() = 'Apply Search']"), "'Apply Search' Button not Present.");

            Results.WriteStatus(test, "Pass", "Verified, My Search Screen successfully.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Saved Searches Button on screen
        /// </summary>
        /// <returns></returns>
        public Search VerifySavedSearchesButtonOnScreen()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//ul[@class='nav nav-tabs modal-tabs']//.//a[contains(@ng-click,'saved_search')]"), "'Saved Searches' Button not Present.");
            Assert.AreEqual(true, driver._getText("xpath", "//ul[@class='nav nav-tabs modal-tabs']//.//a[contains(@ng-click,'saved_search')]").Contains("Saved Searches"), "'Saved Searches' Button Label not match.");

            if (driver._getAttributeValue("xpath", "//ul[@class='nav nav-tabs modal-tabs']//.//li[contains(@ng-class,'SavedSearchTab')]", "class") == null)
                Results.WriteStatus(test, "Pass", "Verified, No Saved Searches Records available so 'Saved Searches' Button is Disable.");
            else
                Results.WriteStatus(test, "Pass", "Verified, Saved Searches Records available so 'Saved Searches' Button is Enable.");

            return new Search(driver, test);
        }

        /// <summary>
        /// Select Date Range option from Section
        /// </summary>
        /// <param name="dateRange">Date Range to Select</param>
        /// <returns></returns>
        public Search selectDateRangeOptionFromSection(string dateRange = "Random")
        {
            IList<IWebElement> dateRangeCollections = driver.FindElements(By.XPath("//cft-field-editor-timeframe-calendar//div[contains(@style, 'display')]//ul/li"));
            bool avail = false;
            Thread.Sleep(2000);
            if (dateRange.Equals("Random"))
            {
                Random rand = new Random();
                for (int i = 0; i < 6; i++)
                {
                    int x = rand.Next(3, dateRangeCollections.Count);
                    dateRange = dateRangeCollections[x].Text;
                    driver._click("xpath", "//cft-field-editor-timeframe-calendar//div[contains(@style, 'display')]//ul/li[" + (x + 1) + "]");
                    Thread.Sleep(500);

                    if (driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Save As') and @disabled]") == false)
                        if (driver._getText("xpath", "//*[@id='CftSearchSummary']/div[1]").Contains("No Data Found") == false)
                            break;
                }
                Results.WriteStatus(test, "Pass", "Selected, '" + dateRange + "' Date Range from Section.");
            }
            else
            {
                for (int i = 0; i < dateRangeCollections.Count; i++)
                {
                    if (dateRangeCollections[i].Text == dateRange)
                    {
                        driver._click("xpath", "//cft-field-editor-timeframe-calendar//div[contains(@style, 'display')]//ul/li[" + (i + 1) + "]");
                        avail = true; Thread.Sleep(2000);
                        break;
                    }
                }

                Assert.AreEqual(true, avail, "'" + dateRange + "' Date Range not Present.");
                Results.WriteStatus(test, "Pass", "Selected, '" + dateRange + "' Date Range from Section.");
            }

            return new Search(driver, test);
        }

        /// <summary>
        /// Select Media checkbox option from  section
        /// </summary>
        /// <param name="mediaName">Media name to Select</param>
        /// <returns></returns>
        public Search selectMediaCheckboxOptionFromSection(string mediaName = "Random")
        {
            IList<IWebElement> mediaCollections = driver.FindElements(By.XPath("//div[contains(@id,'occurrence_mediaName')]//.//div[@class='CFT-search-list-group-field CFT-search-list-group-field-child']/div/div"));
            bool avail = false;

            if (mediaName.Equals("Random"))
            {
                Random rand = new Random();
                int x = rand.Next(0, mediaCollections.Count);
                mediaName = mediaCollections[x].Text;
                driver._clickByJavaScriptExecutor("//div[contains(@id,'occurrence_mediaName')]//.//div[@class='CFT-search-list-group-field CFT-search-list-group-field-child']/div/div[" + (x + 1) + "]/label");
                Results.WriteStatus(test, "Pass", "Selected, '" + mediaName + "' Date Range from Section.");
            }
            else
            {
                for (int i = 0; i < mediaCollections.Count; i++)
                {
                    if (mediaCollections[i].Text == mediaName)
                    {
                        driver._clickByJavaScriptExecutor("//div[contains(@id,'occurrence_mediaName')]//.//div[@class='CFT-search-list-group-field CFT-search-list-group-field-child']/div/div[" + (i + 1) + "]/label");
                        avail = true; Thread.Sleep(2000);
                        break;
                    }
                }

                Assert.AreEqual(true, avail, "'" + mediaName + "' Media not Present.");
                Results.WriteStatus(test, "Pass", "Selected, '" + mediaName + "' Media from Section.");
            }

            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Selected Data Range or Select Different Date Range
        /// </summary>
        /// <param name="VerifyDataRange">Verify Selected Data Range</param>
        /// <returns></returns>
        public String VerifySelectedDateRangeORSelectDifferentDateRange(bool VerifyDataRange)
        {
            IList<IWebElement> dateRangeCollections = driver.FindElements(By.XPath("//cft-field-editor-timeframe-calendar//.//div[@class='CFT-search-list-group-field CFT-search-list-group-field-child']/div/div/div[1]//.//ul/li"));
            string dateRange = "";

            for (int i = 0; i < dateRangeCollections.Count; i++)
            {
                if (driver.FindElement(By.XPath("//cft-field-editor-timeframe-calendar//.//div[@class='CFT-search-list-group-field CFT-search-list-group-field-child']/div/div/div[1]//.//ul/li[" + (i + 1) + "]/span")).GetCssValue("color").Contains("0, 74, 82") == VerifyDataRange)
                {
                    dateRange = driver._getText("xpath", "//cft-field-editor-timeframe-calendar//.//div[@class='CFT-search-list-group-field CFT-search-list-group-field-child']/div/div/div[1]//.//ul/li[" + (i + 1) + "]/span");
                    Results.WriteStatus(test, "Pass", "Verified, '" + dateRange + "' Date Range on screen");
                    if (VerifyDataRange == false)
                    {
                        driver._clickByJavaScriptExecutor("//cft-field-editor-timeframe-calendar//.//div[@class='CFT-search-list-group-field CFT-search-list-group-field-child']/div/div/div[1]//.//ul/li[" + (i + 1) + "]/span");
                        Results.WriteStatus(test, "Pass", "Selected '" + dateRange + "' Date Range on screen");
                    }
                    break;
                }
            }

            return dateRange;
        }

        /// <summary>
        /// Verify Saved Searches Section on Screen 
        /// </summary>
        /// <returns></returns>
        public Search VerifySavedSearchesSectionOnScreen(bool searchCard = true)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='list-group-item cftSearchField']", 20), "'Saved Searched' Section not Present on screen.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='CFT-search-list-group-field']//.//input[contains(@placeholder,'Filter Saved Searches By Name')]"), "'Filter Saved Searches By Name' textarea not present.");

            IList<IWebElement> buttons = driver.FindElements(By.XPath("//div[@class='CFT-search-list-group-field']//.//button"));
            string[] buttonNames = { "Applied Search", "Default Search" };
            for (int i = 0; i < buttons.Count; i++)
                Assert.AreEqual(true, buttons[i].Text.Contains(buttonNames[i]), "'" + buttonNames[i] + "' Button not Present.");

            string searchTitle = "";
            if (searchCard)
            {
                IList<IWebElement> savedSearches = driver.FindElements(By.XPath("//cft-saved-search-list-item"));
                for (int l = 0; l < savedSearches.Count; l++)
                {
                    Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Displayed, "Title of Saved Search not Present.");
                    searchTitle = savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Text;
                    Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-subtext']")).Displayed, "Created Date of '" + searchTitle + "' Saved Search not Present.");

                    IList<IWebElement> searchFields = savedSearches[l]._findElementsWithinElement("xpath", ".//table[@class='table table-details-content']//.//tr");
                    string[] fieldNames = { "Status", "Date Range" };
                    int cnt = 0;
                    for (int f = 0; f < searchFields.Count; f++)
                    {
                        if (searchFields[f].FindElement(By.XPath(".//th")).Text.Contains(fieldNames[0]) == true || searchFields[f].FindElement(By.XPath(".//th")).Text.Contains(fieldNames[1]) == true)
                            cnt++;

                        if (searchFields[f].FindElement(By.XPath(".//th")).Text == "Scheduled Exports")
                        {
                            IList<IWebElement> exports = searchFields[f].FindElements(By.XPath(".//button[contains(@class,'nested-btn-default')]"));
                            for (int e = 0; e < exports.Count; e++)
                            {
                                driver.MouseHoverByJavaScript(exports[e]);
                                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='tooltip-inner']"), "Tooltip not Present on screen.");
                                Assert.AreEqual("Schedule", driver._getText("xpath", "//div[@class='tooltip-inner']"), "'Schedule' Tooltip message not match.");
                            }
                        }
                    }
                    Assert.AreEqual(cnt, fieldNames.Length, "'Status' or 'Date Range' not present for '" + searchTitle + "' Saved Search.");

                    IList<IWebElement> buttonLists = savedSearches[l]._findElementsWithinElement("xpath", ".//button[@class='btn btn-default btn-block custom-btn-default']");
                    string[] buttonsTitle = { "Delete", "Edit Search", "Make Default", "Apply Search" };
                    for (int b = 0; b < buttonLists.Count; b++)
                        Assert.AreEqual(true, buttonLists[b].Text.Contains(buttonsTitle[b]), "'" + buttonsTitle[b] + "' Button not Present for '" + searchTitle + "' Saved Search.");
                }
            }

            Results.WriteStatus(test, "Pass", "Verified, Saved Searches Section on Screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Enter Value in Filter Saved searches input area on screen
        /// </summary>
        /// <param name="searchValueName">Search Value</param>
        /// <returns></returns>
        public Search enterValueInFilterSavedSearchedInputAreaOnScreen(string searchValueName)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='CFT-search-list-group-field']//.//input[contains(@placeholder,'Filter Saved Searches By Name')]"), "'Filter Saved Searches By Name' textarea not present.");
            driver._type("xpath", "//div[@class='CFT-search-list-group-field']//.//input[contains(@placeholder,'Filter Saved Searches By Name')]", searchValueName);
            Thread.Sleep(2000);
            Results.WriteStatus(test, "Pass", "Entered, '" + searchValueName + "' Value in Filter Saved Searches input area on screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Click Schedule from Schedule Export and perform Action
        /// </summary>
        /// <param name="action">Action to Perform</param>
        /// <returns></returns>
        public Search clickScheduleFromScheduleExportAndPerformAction(string action)
        {
            string exportScheduleName = "";
            IList<IWebElement> element = driver.FindElements(By.XPath("//cft-saved-search-list-item//button[contains(@class,'nested-btn-default')]"));

            if (element.Count == 0)
                Results.WriteStatus(test, "Pass", "Schedule not Present on Schedule Export Section.");
            else
            {
                exportScheduleName = element[0].Text;
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element[0]);
                element[0].Click();
                Results.WriteStatus(test, "Pass", "Clicked, Schedule From Schedule Exports Section.");

                if (action.Equals("Update"))
                    clickButtonFromScheduleWindow("Update");

                if (action.Equals("Delete"))
                    clickButtonFromScheduleWindow("Delete");
            }

            return new Search(driver, test);
        }

        /// <summary>
        /// Click Button from Schedule Window
        /// </summary>
        /// <param name="buttonName">Button Name to click</param>
        /// <returns></returns>
        public Search clickButtonFromScheduleWindow(string buttonName)
        {
            if (buttonName.Equals("Update"))
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'" + buttonName + "')]"), "'" + buttonName + "' Button not Present.");
                driver._clickByJavaScriptExecutor("//button[@class='btn btn-default' and contains(text(),'" + buttonName + "')]");
                Results.WriteStatus(test, "Pass", "Clicked, '" + buttonName + "' Button from Schedule Window.");
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//span[contains(text(),'Successfully updated a scheduled export for')]"), "Schedule Updated message not Present.");
            }

            if (buttonName.Equals("Delete"))
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-primary' and contains(text(),'" + buttonName + "')]"), "'" + buttonName + "' Button not Present.");
                driver._clickByJavaScriptExecutor("//button[@class='btn btn-primary' and contains(text(),'" + buttonName + "')]");
                Results.WriteStatus(test, "Pass", "Clicked, '" + buttonName + "' Button from Schedule Window.");
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//span[contains(text(),'Successfully deleted scheduled export!')]"), "Schedule Deleted message not Present.");
            }

            Results.WriteStatus(test, "Pass", "Verified, tooltip message for '" + buttonName + "' Action on Screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Schedule Window 
        /// </summary>
        /// <returns></returns>
        public Search VerifyScheduleWindow()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='popover-content popover-body']", 20), "Schedule Window not Present on Screen.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//input[@class='form-control']"), "Search Name Default not display.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//span[@class='fa fa-check text-success form-control-feedback']"), "Search Name Feedback in Green Right color not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default dropdown-toggle']"), "Schedule Dropdown not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Update')]"), "'Update' Button not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-primary' and contains(text(),'Delete')]"), "'Delete' Button not Present.");

            Results.WriteStatus(test, "Pass", "Verified, Schedule Window on screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Checked or UnChecked Fixed Date Range from Search Section
        /// </summary>
        /// <param name="unChecked">UnChecked Date Range</param>
        /// <returns></returns>
        public Search checkedOrUnCheckedFixedDateRangeFromSearchScreen(bool Checked)
        {
            if (driver._isElementPresent("xpath", "//div[@class='CFT-textbox has-success']/input[@name='startDate' and @class='form-control ng-untouched ng-pristine']") == Checked)
                driver._clickByJavaScriptExecutor("//cft-field-editor-timeframe-calendar/div[@class='list-group-item CFT-search-field']//.//form/div[@class='checkbox']/label/span");

            Assert.AreEqual(Checked, driver._isElementPresent("xpath", "//div[@class='CFT-textbox has-success']/input[@name='startDate' and contains(@class,'ng-valid')]"), "'Start Date' Textarea not Enable or Disable.");
            Assert.AreEqual(Checked, driver._isElementPresent("xpath", "//div[@class='CFT-textbox has-success']/input[@name='endDate' and contains(@class,'ng-valid')]"), "'End Date' Textarea not Enable or Disable.");
            Results.WriteStatus(test, "Pass", "Checked / UnChecked Fixed Date Range from Seaech Screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Media Field section on Screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyMediaFieldSectionOnScreen()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[not(@class='ng-hide')][3]/cft-field-editor-resolver//div[@class='list-group-item CFT-search-field']"), "Media Fields section not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//input[@type='text' and contains(@placeholder,'Filter Media Types')]"), "Search Area not Present for Media.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[not(@class='ng-hide')][3]//.//button/span[text()='Select Displayed']"), "'Select Displayed' Button not Present for Media.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[not(@class='ng-hide')][3]//.//button[contains(text(),'Exclude')]"), "'Exclude' Button not Present for Media.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[not(@class='ng-hide')][3]//div[@class='CFT-search-list-group-field']//div[@id='borderLayout_eRootPanel']"), "Media Value tree view not Present for Media.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[not(@class='ng-hide')][3]//div[@class='CFT-search-list-group-field']//button[contains(text(),'Load More')]"), "'Load More' Button not Present for Media.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[not(@class='ng-hide')][3]//div[@class='CFT-search-list-group-field']//button[contains(text(),'Clear Selected')]"), "'Clear Selected' Button not Present for Media.");

            Results.WriteStatus(test, "Pass", "Verified, Media Field Section on Screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Select Records from Right section and Verify into Selected Section
        /// </summary>
        /// <returns></returns>
        public Search selectRecordsFromRightSectionAndVerifyIntoSelectedSection()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[not(@class='ng-hide')][3]//div[@class='panel-banner']//div[@class='row']/div[1]//div[@class='ag-body-container']//div[@role='row']"), "Media Value tree view not present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[not(@class='ng-hide')][3]//div[@class='panel-banner']//div[@class='row']/div[2]"), "Selected Media Value tree view not present.");

            string mediaTitle = "";

            IList<IWebElement> totalMedia = driver.FindElements(By.XPath("//div[not(@class='ng-hide')][3]//div[@class='panel-banner']//div[@class='row']/div[1]//div[@class='ag-body-container']//div[@role='gridcell']"));
            Random rand = new Random();
            int x = rand.Next(0, totalMedia.Count);
            mediaTitle = totalMedia[x].Text;
            if (mediaTitle.Contains("("))
                mediaTitle = mediaTitle.Substring(0, mediaTitle.IndexOf("("));
            totalMedia[x].Click();
            Thread.Sleep(1000);
            Results.WriteStatus(test, "Pass", "Selected Media from Right section.");

            IList<IWebElement> selectedMedia = driver.FindElements(By.XPath("//div[not(@class='ng-hide')][3]//div[@class='panel-banner']//div[@class='row']/div[2]//div[@class='ag-body-container']//div[@role='gridcell']"));
            bool avail = false;
            for (int i = 0; i < selectedMedia.Count; i++)
            {
                if (mediaTitle.Contains(selectedMedia[i].Text))
                {
                    avail = true;
                    break;
                }
            }

            Assert.AreEqual(true, avail, "'" + mediaTitle + "' Media not available on Selected section.");
            Results.WriteStatus(test, "Pass", "Verified, '" + mediaTitle + "' Media on Selceted section.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Mouse hover on button to Verify effect
        /// </summary>
        /// <param name="buttonName">Button name to Verify Effect</param>
        /// <returns></returns>
        public Search mouseHoverOnButtonToVerifyEffect(string buttonName = "Select Displayed")
        {
            if (buttonName.Equals("Select Displayed"))
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[not(@class='ng-hide')][3]//.//button/span[text()='Select Displayed']"), "" + buttonName + " Button not Present.");
                driver.MouseHoverUsingElement("xpath", "//div[not(@class='ng-hide')][3]//cft-field-editor-multi-items-filter//button[not(text())]");
                Assert.AreEqual("rgba(0, 74, 82, 1)", driver.FindElement(By.XPath("//div[not(@class='ng-hide')][3]//cft-field-editor-multi-items-filter//button[not(text())]")).GetCssValue("color"), "'" + buttonName + "' Button not Highlighted with Blue color.");
            }
            if (buttonName.Equals("Exclude"))
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[not(@class='ng-hide')][3]//.//button[contains(text(),'Exclude')]"), "" + buttonName + " Button not Present.");
                driver.MouseHoverUsingElement("xpath", "//div[not(@class='ng-hide')][3]//.//button[contains(text(),'Exclude')]");
                Assert.AreEqual("rgba(0, 74, 82, 1)", driver.FindElement(By.XPath("//div[not(@class='ng-hide')][3]//.//button[contains(text(),'Exclude')]")).GetCssValue("color"), "'" + buttonName + "' Button not Highlighted with Blue color.");
            }
            if (buttonName.Equals("Coop Selected"))
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'setViewMode')]"), "" + buttonName + " Button not Present.");
                driver.MouseHoverUsingElement("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'setViewMode')]");
                Assert.AreEqual("rgba(0, 74, 82, 1)", driver.FindElement(By.XPath("//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'setViewMode')]")).GetCssValue("color"), "'" + buttonName + "' Button not Highlighted with Blue color.");
            }
            if (buttonName.Equals("Coop Select Displayed"))
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'selectAll')]"), "" + buttonName + " Button not Present.");
                driver.MouseHoverUsingElement("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'selectAll')]");
                Assert.AreEqual("rgba(0, 74, 82, 1)", driver.FindElement(By.XPath("//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'selectAll')]")).GetCssValue("color"), "'" + buttonName + "' Button not Highlighted with Blue color.");
            }
            if (buttonName.Equals("Coop Exclude"))
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'Excluded')]"), "" + buttonName + " Button not Present.");
                driver.MouseHoverUsingElement("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'Excluded')]");
                Assert.AreEqual("rgba(0, 74, 82, 1)", driver.FindElement(By.XPath("//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'Excluded')]")).GetCssValue("color"), "'" + buttonName + "' Button not Highlighted with Blue color.");
            }

            Results.WriteStatus(test, "Pass", "Mouse Hover on '" + buttonName + "' Button and verified Effects.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Click Button on Search screen
        /// </summary>
        /// <param name="buttonName">Button Name to Click</param>
        /// <returns></returns>
        public Search clickButtonOnSearchScreen(string buttonName)
        {
            switch (buttonName)
            {
                case "Select Displayed":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[not(@class='ng-hide')][3]//cft-field-editor-multi-items-filter//button[not(text())]"), "" + buttonName + " Button not Present.");
                        driver._clickByJavaScriptExecutor("//div[not(@class='ng-hide')][3]//cft-field-editor-multi-items-filter//button[not(text())]");
                        break;
                    }

                case "Exclude":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_media']//.//button[contains(@ng-click,'ExclusionMode')]"), "" + buttonName + " Button not Present.");
                        driver._clickByJavaScriptExecutor("//div[@id='field-brand-n_occurrence_media']//.//button[contains(@ng-click,'ExclusionMode')]");
                        break;
                    }

                case "Coop Selected":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'setViewMode')]"), "" + buttonName + " Button not Present.");
                        driver._clickByJavaScriptExecutor("//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'setViewMode')]");
                        break;
                    }

                case "Coop Clear Selected":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'deselectAll')]"), "" + buttonName + " Button not Present.");
                        driver._clickByJavaScriptExecutor("//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'deselectAll')]");
                        break;
                    }

                case "Coop Cancel":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'setFilterTerm')]"), "" + buttonName + " Button not Present.");
                        driver._clickByJavaScriptExecutor("//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'setFilterTerm')]");
                        break;
                    }

                case "Coop Select Displayed":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button/span[text()='Select Displayed']"), "" + buttonName + " Button not Present.");
                        driver._clickByJavaScriptExecutor("//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button/span[text()='Select Displayed']");
                        break;
                    }

                case "Coop Exclude":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'Excluded')]"), "" + buttonName + " Button not Present.");
                        driver._clickByJavaScriptExecutor("//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'Excluded')]");
                        break;
                    }

                case "Coop Browse":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'setBrowseViewMode')]"), "" + buttonName + " Button not Present.");
                        driver._clickByJavaScriptExecutor("//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'setBrowseViewMode')]");
                        break;
                    }

                case "Apply Search":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-primary' and contains(text(),'Apply Search')]"), "'" + buttonName + "' Button not Present.");
                        driver._clickByJavaScriptExecutor("//button[@class='btn btn-primary' and contains(text(),'Apply Search')]");
                        Thread.Sleep(2000);
                        driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Loading Pivot Data')]");
                        break;
                    }

                case "Save As":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Save As')]"), "'" + buttonName + "' Button not Present.");
                        driver._clickByJavaScriptExecutor("//button[@class='btn btn-default' and contains(text(),'Save As')]");
                        break;
                    }

                case "Reset":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Reset')]"), "'" + buttonName + "' Button not Present.");
                        driver._clickByJavaScriptExecutor("//button[@class='btn btn-default' and contains(text(),'Reset')]");
                        break;
                    }

                case "Save!":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-success' and contains(text(),'Save!')]"), "'" + buttonName + "' Button not Present.");
                        driver._clickByJavaScriptExecutor("//button[@class='btn btn-success' and contains(text(),'Save!')]");
                        break;
                    }

                case "Cancel":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Cancel')]"), "'" + buttonName + "' Button not Present.");
                        driver._clickByJavaScriptExecutor("//button[@class='btn btn-default' and contains(text(),'Cancel')]");
                        break;
                    }

                case "Reset Changes":
                case "Clear Search":
                    {
                        if (driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Reset Changes')]"))
                        {
                            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Reset Changes')]"), "'" + buttonName + "' Button not Present.");
                            driver._clickByJavaScriptExecutor("//button[@class='btn btn-default' and contains(text(),'Reset Changes')]");
                        }
                        else
                        {
                            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-primary' and contains(text(),'Clear Search')]"), "'" + buttonName + "' Button not Present.");
                            driver._clickByJavaScriptExecutor("//button[@class='btn btn-primary' and contains(text(),'Clear Search')]");
                        }
                        break;
                    }

                case "Default Search":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='CFT-search-list-group-field']//.//button[contains(text(),'Default Search')]"), "'" + buttonName + "' Button not Present.");
                        driver._clickByJavaScriptExecutor("//div[@class='CFT-search-list-group-field']//.//button[contains(text(),'Default Search')]");
                        break;
                    }

                case "Applied Search":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='CFT-search-list-group-field']//.//button[contains(text(),'Applied Search')]"), "'" + buttonName + "' Button not Present.");
                        driver._clickByJavaScriptExecutor("//div[@class='CFT-search-list-group-field']//.//button[contains(text(),'Applied Search')]");
                        break;
                    }

                case "Overwrite":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Overwrite')]"), "'" + buttonName + "' Button not Present.");
                        driver._clickByJavaScriptExecutor("//button[@class='btn btn-default' and contains(text(),'Overwrite')]");
                        break;
                    }

                case "Edit Search":
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//ul[@class='nav nav-tabs modal-tabs']//.//a[contains(@ng-click,'my_search')]"), "'" + buttonName + "' Button not present.");
                        driver._clickByJavaScriptExecutor("//ul[@class='nav nav-tabs modal-tabs']//.//a[contains(@ng-click,'my_search')]");
                        break;
                    }
            }

            Thread.Sleep(2000);
            Results.WriteStatus(test, "Pass", "Clicked '" + buttonName + "' Button on Search screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Button Disable or not on Screen
        /// </summary>
        /// <param name="buttonName">ButtonName to Verify</param>
        /// <param name="Disabled">Verify Button Disable</param>
        /// <returns></returns>
        public Search VerifyButtonDisableOrNotOnScreen(string buttonName, bool Disabled = true)
        {
            if (Disabled)
            {
                Assert.AreEqual(driver._getAttributeValue("xpath", "//button[@class='btn btn-default' and contains(text(),'" + buttonName + "')]", "disabled"), ("true"), "'" + buttonName + "' Button not Disabled.");
                Results.WriteStatus(test, "Pass", "Verified, '" + buttonName + "' Button Disabled on screen.");
            }
            else
            {
                Assert.AreEqual(driver._getAttributeValue("xpath", "//button[@class='btn btn-default' and contains(text(),'" + buttonName + "')]", "disabled"), null, "'" + buttonName + "' Button not Disabled.");
                Results.WriteStatus(test, "Pass", "Verified, '" + buttonName + "' Button Enabled on screen.");
            }

            return new Search(driver, test);
        }

        /// <summary>
        /// Enter Search Value on Search Screen
        /// </summary>
        /// <returns></returns>
        public String enterSearchValueOnSearchScreen()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//input[contains(@placeholder,'What would you like to call your search') and @type='text']", 20), "'What would you like to call your search?' textarea not Present.");
            driver._clickByJavaScriptExecutor("//input[contains(@placeholder,'What would you like to call your search') and @type='text']");
            string scheduleSearchName = "Test" + driver._randomString(4, true);
            driver._type("xpath", "//input[contains(@placeholder,'What would you like to call your search') and @type='text']", scheduleSearchName);
            Thread.Sleep(1000);
            Results.WriteStatus(test, "Pass", "Entered Save As Search Report Name on Screen.");
            return scheduleSearchName;
        }

        /// <summary>
        /// Verify Selectd Records on Select Displayed section
        /// </summary>
        /// <returns></returns>
        public Search VerifySelectedRecordsOnSelectDisplayedSection()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_media']//.//div[@id='multi-ag-grid-brand-n_occurrence_media']"), "Media Value tree view not present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_media']//.//div[@id='multi-ag-grid-brand-n_occurrence_media-selected']"), "Selected Media Value tree view not present.");

            IList<IWebElement> totalMedia = driver.FindElements(By.XPath("//div[@id='field-brand-n_occurrence_media']//.//div[@id='multi-ag-grid-brand-n_occurrence_media']//.//div[@class='ag-body-container']/div"));
            IList<IWebElement> selectedMedia = driver.FindElements(By.XPath("//div[@id='field-brand-n_occurrence_media']//.//div[@id='multi-ag-grid-brand-n_occurrence_media-selected']//.//div[@class='ag-body-container']/div"));

            for (int i = 0; i < selectedMedia.Count; i++)
                Assert.AreEqual(true, totalMedia[i].Text.Contains(selectedMedia[i].Text), "'" + selectedMedia[i].Text + "' Value not Present on Section.");

            Results.WriteStatus(test, "Pass", "Verified, Selected Records on Selcet Displayed section.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Exclude button titles on Search screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyExcludeButtonTitlesOnSearchScreen()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_media']//.//button[contains(@ng-click,'ExclusionMode')]"), "Exclude Button not Present.");
            Assert.AreEqual(true, driver._getText("xpath", "//div[@id='field-brand-n_occurrence_media']//.//button[contains(@ng-click,'ExclusionMode')]").Contains("Excluding"), "'Excluding' Title not change.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_media']//.//button[contains(@ng-click,'deselectAll')]"), "'Clear Excluded' Button Title not change.");
            Assert.AreEqual(true, driver._getText("xpath", "//div[@id='field-brand-n_occurrence_media']//.//button[contains(@ng-click,'deselectAll')]").Contains("Clear Excluded"), "'Clear Excluded' Button Title not change.");

            Results.WriteStatus(test, "Pass", "Verified, Exclude button titles on Search screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Field Menu and click on it on Search screen
        /// </summary>
        /// <param name="fieldName">Field Name to Verify</param>
        /// <returns></returns>
        public Search VerifyFieldMenuAndClickOnItOnSearchScreen(string fieldName)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@role='tablist']//.//div[@role='tabpanel']/button/span[text() = '" + fieldName + "']"), "'" + fieldName + "' Menu Item not Present under Basic Fields Tab.");
            driver._clickByJavaScriptExecutor("//div[@role='tablist']//.//div[@role='tabpanel']/button/span[text() = '" + fieldName + "']");
            Thread.Sleep(2000);

            if (fieldName.Equals("Coop Advertisers"))
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']"), "'Coop Advertisers' Section not Present.");

            Results.WriteStatus(test, "Pass", "Clicked, '" + fieldName + "' Field Menu and Verified on Search screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Select records from coop advertisers section
        /// </summary>
        /// <param name="multiple">Select Multiple Records</param>
        /// <returns></returns>
        public Search selectRecordsFromCoopAdvertisersSection(bool multiple = false)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div"), "Coop Advertisers' Records not Present.");

            IList<IWebElement> totalAdvertise = driver.FindElements(By.XPath("//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div"));
            for (int i = 0; i < totalAdvertise.Count; i++)
            {
                IList<IWebElement> cells = totalAdvertise[i]._findElementsWithinElement("xpath", ".//label/input[contains(@class,'not-empty')]");
                if (cells.Count == 0)
                {
                    cells = totalAdvertise[i]._findElementsWithinElement("xpath", ".//label/span");
                    cells[0].Click();
                    Thread.Sleep(2000);
                    break;
                }
            }

            if (multiple)
            {
                for (int i = 0; i < totalAdvertise.Count; i++)
                {
                    IList<IWebElement> cells = totalAdvertise[i]._findElementsWithinElement("xpath", ".//label/input[contains(@class,'not-empty')]");
                    if (cells.Count == 0)
                    {
                        cells = totalAdvertise[i]._findElementsWithinElement("xpath", ".//label/span");
                        cells[0].Click();
                        Thread.Sleep(2000);
                        break;
                    }
                }
            }
            Results.WriteStatus(test, "Pass", "Selected Records from Coop Advertiser Section.");
            return new Search(driver, test);
        }

        /// <summary>
        /// unChecked item from Coop Advertiser Section and Verify
        /// </summary>
        /// <returns></returns>
        public Search unCheckedItemFromCoopAdvertisersSectionAndVerify()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div"), "Coop Advertisers' Records not Present.");

            IList<IWebElement> totalAdvertise = driver.FindElements(By.XPath("//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div"));
            string unCheckedItem = totalAdvertise[0].Text;
            IList<IWebElement> recordTitle = totalAdvertise[0]._findElementsWithinElement("xpath", ".//label/span");
            recordTitle[0].Click();
            Thread.Sleep(1000);

            totalAdvertise = driver.FindElements(By.XPath("//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div"));
            for (int i = 0; i < totalAdvertise.Count; i++)
            {
                if (totalAdvertise[i].Text.Contains(unCheckedItem))
                {
                    IList<IWebElement> cells = totalAdvertise[i]._findElementsWithinElement("xpath", ".//label/input[contains(@class,'not-empty')]");
                    Assert.AreEqual(0, cells.Count, "'" + totalAdvertise[i].Text + "' Item not present on section.");
                    break;
                }
            }

            Results.WriteStatus(test, "Pass", "UChecked Item from Coop Advertiser Section and Verified.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Enter Value in Coop Advertisers input area on screen
        /// </summary>
        /// <param name="filterValue">Filter Value to search</param>
        /// <returns></returns>
        public String enterValueInCoopAdvertisersInputAreaOnScreen(string filterValue)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//input[contains(@placeholder,'Filter')]"), "'Filter Coop Advertisers' textarea not present.");
            string searchValue = "";

            if (filterValue.Equals("Keyword"))
            {
                searchValue = driver._getText("xpath", "//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div");
                searchValue = searchValue.Substring(0, 4);
            }

            if (filterValue.Equals("Letter"))
                searchValue = "12";

            if (filterValue.Equals("Random"))
                searchValue = driver._randomString(6) + driver._randomString(4, true);

            driver._type("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//input[contains(@placeholder,'Filter')]", searchValue);
            Results.WriteStatus(test, "Pass", "Entered, '" + searchValue + "' Keyword on Coop Advertisers input area on screen.");
            return searchValue;
        }

        /// <summary>
        /// Verify Filter value on coop Advertisers section
        /// </summary>
        /// <param name="filterValue">Filter Value on Section</param>
        /// <returns></returns>
        public Search VerifyFilterValueOnCoopAdvertisersSection(string filterValue, bool noDataFound = false)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div"), "Coop Advertisers' Records not Present.");

            if (noDataFound)
            {
                Assert.AreEqual(true, driver._getText("xpath", "//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div").Contains("No Rows To Show"), "'No Rows To Show' Message not found.");
                Results.WriteStatus(test, "Pass", "'No Rows To Show' Message not found on Coop Advertisers Section.");
            }
            else
            {
                if (driver._getText("xpath", "//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div").Contains("No Rows To Show"))
                    Results.WriteStatus(test, "Pass", "'No Rows To Show' Records found on Coop Advertisers Section.");
                else
                {
                    IList<IWebElement> totalAdvertise = driver.FindElements(By.XPath("//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div"));
                    for (int i = 0; i < totalAdvertise.Count; i++)
                        Assert.AreEqual(true, totalAdvertise[i].Text.Contains(filterValue), "'" + filterValue + "' Filter Value not Present on '" + totalAdvertise[i].Text + "' Record");

                    Results.WriteStatus(test, "Pass", "Verified filter Value on Coop Advertisers Section.");
                }
            }

            return new Search(driver, test);
        }

        /// <summary>
        /// Verify tooltip for each records for Coop Advertiser section
        /// </summary>
        /// <returns></returns>
        public Search VerifyTooltipForEachRecordsForCoopAdvertisersSection()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div"), "Coop Advertisers' Records not Present.");

            IList<IWebElement> totalAdvertise = driver.FindElements(By.XPath("//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div/label/span"));
            for (int i = 0; i < totalAdvertise.Count; i++)
            {
                driver.MouseHoverUsingElement("xpath", "//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div[" + (i + 1) + "]/label/span");
                Thread.Sleep(500);
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='tooltip-inner']"), "Tooltip not present.");
                Assert.AreEqual(true, driver._getText("xpath", "//div[@class='tooltip-inner']").Contains(totalAdvertise[i].Text), "'" + totalAdvertise[i].Text + "' Record tooltip not match.");
            }

            Results.WriteStatus(test, "Pass", "Verified Tooltip for each record for Coop Advertisers Section.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Exclude Button after click on it for Coop Advertisers
        /// </summary>
        /// <param name="clearExclude">Verify Clear Exclude</param>
        /// <returns></returns>
        public Search VerifyExcludeButtonAfterClickOnItForCoopAdvertisers(bool clearExclude = false)
        {
            if (clearExclude)
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'deselectAll')]"), "'Clear Excluded' Button Title not change.");
                Assert.AreEqual(true, driver._getText("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'deselectAll')]").Contains("Clear Excluded"), "'Clear Excluded' Button Title not change.");
            }
            else
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'Excluded')]"), "Exclude Button not Present.");
                Assert.AreEqual(true, driver._getText("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//button[contains(@ng-click,'Excluded')]").Contains("Excluding"), "'Excluding' Title not change.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Exclude button titles on Search screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Browse section for Coop Advertisers
        /// </summary>
        /// <returns></returns>
        public Search VerifyBrowseSectionForCoopAdvertisers()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'input-group-btn-first')]"), "Browse Filter tabs not present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]"), "Coop Advertisers' Records not Present.");

            IList<IWebElement> browseIcons = driver.FindElements(By.XPath("//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'input-group-btn-first')]/a"));
            string[] browseNames = { "#", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            for (int i = 0; i < browseNames.Length; i++)
                Assert.AreEqual(browseNames[i], browseIcons[i].Text, "'" + browseIcons[i].Text + "' tab not present.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//a[contains(@ng-click,'IsExcluded')]"), "Exclude not present for coop advertisers section.");
            Results.WriteStatus(test, "Pass", "Verified, Browse Section for Coop Advertisers.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Select any Character from filter and Verify Records
        /// </summary>
        /// <returns></returns>
        public Search selectAnyCharacterFromFilterAndVerifyRecords()
        {
            IList<IWebElement> browseIcons = driver.FindElements(By.XPath("//div[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'input-group-btn-first')]/a"));
            Random rand = new Random();
            int x = rand.Next(1, browseIcons.Count - 1);
            string selectedChar = browseIcons[x].Text;
            browseIcons[x].Click();
            Results.WriteStatus(test, "Pass", "Selected, '" + selectedChar + "' Character from Filter section.");
            Thread.Sleep(1000);

            if (driver._isElementPresent("xpath", "//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div/span[contains(text(),'No Rows To Show')]") == true)
                Results.WriteStatus(test, "Pass", "'No Rows To Show' for Coop Advertisers Section.");
            else
            {
                IList<IWebElement> totalRecords = driver.FindElements(By.XPath("//*[@id='field-brand-n_occurrence_coopAdvertiserNames']//.//div[contains(@class,'search-list-group-field-child')]/div/div"));
                for (int i = 0; i < totalRecords.Count; i++)
                    Assert.AreEqual(true, totalRecords[i].Text.Substring(0, 1).Equals(selectedChar), "'" + totalRecords[i].Text + "' Record not start with '" + selectedChar + "'.");

                Results.WriteStatus(test, "Pass", "Verified, Records with Selected Character from Filter section.");
            }

            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Keyword section on search screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyKeywordSectionOnSearchScreen()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//.//div[@class='panel-summary-header']"), "Keyword Section not present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//.//div[@class='panel-summary-header']"), "Keyword Header Section not present.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//.//div[@class='input-group input-no-btn-group']"), "Input area not present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//.//input[contains(@placeholder,'Filter Text')]"), "Filter Text area not present.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//.//div[contains(@class,'CFT-search-list-group-field-child')]"), "Radio button section not present.");
            IList<IWebElement> radioCollections = driver.FindElements(By.XPath("//cft-field-editor-keyword-search-my-search//.//div[contains(@class,'CFT-search-list-group-field-child')]/div/div"));
            string[] radioButtonTitles = { "All Fields", "Headline", "Lead Text", "Visual", "Description" };
            for (int i = 0; i < radioCollections.Count; i++)
                Assert.AreEqual(true, radioCollections[i].Text.Contains(radioButtonTitles[i]), "'" + radioButtonTitles[i] + "' Radio Button not present.");

            Results.WriteStatus(test, "Pass", "Verified, Keyword section on search screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Enter Keyword in Search area on screen
        /// </summary>
        /// <param name="filterValue">Filter Value to enter</param>
        /// <returns></returns>
        public String enterKeywordInSearchAreaOnScreen(string filterValue)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//.//input[contains(@placeholder,'Filter Text')]"), "Filter Text area not present.");
            string searchValue = "";

            if (filterValue.Equals("Existing"))
                searchValue = "test";

            if (filterValue.Equals("Random"))
                searchValue = driver._randomString(6) + driver._randomString(4, true);

            driver._type("xpath", "//cft-field-editor-keyword-search-my-search//.//input[contains(@placeholder,'Filter Text')]", searchValue);
            Thread.Sleep(4000);
            Results.WriteStatus(test, "Pass", "Entered, '" + searchValue + "' Filter Text for Keyword section on screen.");
            return searchValue;
        }

        /// <summary>
        /// Enter Keyword in Search area and Verify chart Value
        /// </summary>
        /// <returns></returns>
        public String enterKeywordInSearchAreaAndVerifyChartValue()
        {
            string recordCollection = "";
            string searchKeyword = "test";
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//.//input[contains(@placeholder,'Filter Text')]"), "Filter Text area not present.");
            driver._type("xpath", "//cft-field-editor-keyword-search-my-search//.//input[contains(@placeholder,'Filter Text')]", searchKeyword);
            Thread.Sleep(3000);

            for (int i = 0; i < 5; i++)
            {
                if (driver._getText("xpath", "//*[@id='CftSearchSummary']/div[1]").Contains("No Data Found") == true)
                {
                    recordCollection = driver._randomString(2);
                    driver._type("xpath", "//cft-field-editor-keyword-search-my-search//.//input[contains(@placeholder,'Filter Text')]", searchKeyword);
                    Thread.Sleep(3000);
                }
                else
                {
                    Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='CftSearchSummary']/div[1]//.//*[name()='text' and @class='highcharts-title']"), "Chart Record Collection not present.");
                    recordCollection = driver._getText("xpath", "//*[@id='CftSearchSummary']/div[1]//.//*[name()='text' and @class='highcharts-title']").Trim().Replace("\r\n", "").Replace(",", "");
                    break;
                }
            }

            Results.WriteStatus(test, "Pass", "Entered, '" + searchKeyword + "' Keyword and Verified Record collection in chart is '" + recordCollection + "'.");
            return recordCollection;
        }

        /// <summary>
        /// Verify Reset Chages Message on screen
        /// </summary>
        /// <param name="resetChanges">Click on Reset Change button</param>
        /// <returns></returns>
        public Search VerifyResetChangesMessageOnScreen(bool resetChanges)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//label[@class='field-title']", 20), "Reset Message not present.");
            Results.WriteStatus(test, "Pass", "Verified, Reset Changes message on screen.");

            if (resetChanges)
            {
                if (driver._isElementPresent("xpath", "//button[@class='btn btn-primary' and contains(text(),'Clear Search')]"))
                    driver._clickByJavaScriptExecutor("//button[@class='btn btn-primary' and contains(text(),'Clear Search')]");
                else
                    clickButtonOnSearchScreen("Reset Changes");
            }
            else
                clickButtonOnSearchScreen("Cancel");

            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Summary Details after keyword search
        /// </summary>
        /// <param name="radioOption">Radio option of selected</param>
        /// <param name="searchKeyword">Search Keyword to Verify</param>
        /// <returns></returns>
        public Search VerifySummaryDetailsAfterKeywordSearch(string radioOption, string searchKeyword, string fieldName)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='CftSearchSummary']/div"), "Summary Details section not present.");
            IList<IWebElement> detailCollections = driver.FindElements(By.XPath("//*[@id='CftSearchSummary']/div"));
            for (int i = 1; i < detailCollections.Count; i++)
            {
                if (driver._isElementPresent("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-header')]") == true)
                    if (driver._getText("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-header')]").Contains(fieldName) == true)
                    {
                        if (radioOption.Equals(""))
                            Assert.AreEqual(true, driver._getText("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-item')]//.//p").Contains(searchKeyword), "'" + searchKeyword + "' not present in Summary Details.");
                        else
                            Assert.AreEqual(true, driver._getText("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-item')]//.//p").Contains(searchKeyword + " in " + radioOption), "'" + searchKeyword + " in " + radioOption + "' not present in Summary Details.");
                        break;
                    }
            }

            Results.WriteStatus(test, "Pass", "Verified, Summary Details after keyword search for '" + fieldName + "' Field.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Fields Refresh icon disable on summart details section
        /// </summary>
        /// <returns></returns>
        public Search VerifyFieldsRefreshIconDisableOnSummaryDetailSection()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='CftSearchSummary']/div"), "Summary Details section not present.");
            string headerName = "";
            IList<IWebElement> detailCollections = driver.FindElements(By.XPath("//*[@id='CftSearchSummary']/div"));
            for (int i = 1; i < detailCollections.Count; i++)
            {
                if (driver._isElementPresent("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-header')]") == true)
                {
                    headerName = driver._getText("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-header')]");
                    Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-header')]//.//div[@class='notice-action']/i[@class='fa fa-refresh disabled']"), "'" + headerName + "' Field Refresh icon not disable.");
                }
            }

            Results.WriteStatus(test, "Pass", "Verified, Fields Refresh icon disable on Summary Details section.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Select Radio option from keyword section
        /// </summary>
        /// <param name="radioOption">Radio optio to Select</param>
        /// <returns></returns>
        public String selectRadioOptionFormKeywordSection(string radioOption)
        {
            string selectedRaioOption = "";
            IList<IWebElement> radioCollections = driver.FindElements(By.XPath("//cft-field-editor-keyword-search-my-search//.//div[contains(@class,'CFT-search-list-group-field-child')]/div/div"));

            if (radioOption.Equals("Random"))
            {
                Random rand = new Random();
                int x = rand.Next(0, radioCollections.Count);
                IList<IWebElement> cells = radioCollections[x]._findElementsWithinElement("xpath", ".//label/span");
                selectedRaioOption = radioCollections[x].Text;
                cells[0].Click();
                Thread.Sleep(3000);
            }
            else
            {
                for (int i = 0; i < radioCollections.Count; i++)
                {
                    if (radioCollections[i].Text.Contains(radioOption))
                    {
                        selectedRaioOption = radioOption;
                        IList<IWebElement> cells = radioCollections[i]._findElementsWithinElement("xpath", ".//label/span");
                        cells[0].Click();
                        Thread.Sleep(3000);
                        break;
                    }
                }
            }

            Results.WriteStatus(test, "Pass", "Selected, '" + selectedRaioOption + "' Radio Option from keyword section.");
            return selectedRaioOption;
        }

        /// <summary>
        /// Click Refresh icon and Verify message for field section
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns></returns>
        public Search clickRefreshIconAndVerifyMessageForFieldSection(string fieldName, string defaultSelectedValue = "")
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='CftSearchSummary']/div"), "Summary Details section not present.");
            IList<IWebElement> detailCollections = driver.FindElements(By.XPath("//*[@id='CftSearchSummary']/div"));
            for (int i = 1; i < detailCollections.Count; i++)
            {
                if (driver._isElementPresent("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-header')]") == true)
                    if (driver._getText("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-header')]").Contains(fieldName) == true)
                    {
                        Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-item')]//.//div[@class='notice-action']/i"));
                        driver._clickByJavaScriptExecutor("//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-item')]//.//div[@class='notice-action']/i");
                        Thread.Sleep(3000);
                        if (defaultSelectedValue == "")
                            Assert.AreEqual(true, driver._getText("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-item')]//.//p").Contains("No search set!"), "'No search set!' not present in Summary Details.");
                        else
                            Assert.AreEqual(true, driver._getText("xpath", "//*[@id='CftSearchSummary']/div[" + (i + 1) + "]//.//div[contains(@class,'search-summary-item')]//.//p").Contains(defaultSelectedValue), "'" + defaultSelectedValue + "' not present in Summary Details.");
                        break;
                    }
            }

            Results.WriteStatus(test, "Pass", "Clicked, Refresh Icon for '" + fieldName + "' Field and verified message for keyword section.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify No Data Found Message on Chart
        /// </summary>
        /// <returns></returns>
        public Search VerifyNoDataFoundMessageOnChart()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='CftSearchSummary']/div[1]"), "Summary Details section not present.");
            Assert.AreEqual(true, driver._getText("xpath", "//*[@id='CftSearchSummary']/div[1]").Contains("No Data Found"), "'No Data Found' message on Chart not present.");
            Results.WriteStatus(test, "Pass", "Verified, No Data Found message on chart.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Number of Records Collections on Grid
        /// </summary>
        /// <param name="records">Records of Chart</param>
        /// <returns></returns>
        public Search VerifyNumberOfRecordCollectionsOnGrid(string records)
        {
            selectNoOfRecordsOnPageFromGrid("10");
            string lastPage = "1";
            int totalRecords = 0;
            if (driver._isElementPresent("xpath", "//div[@class='CFT-view-actions-wrapper']//.//li[@class='pagination-last page-item']/a") == true)
            {
                driver._clickByJavaScriptExecutor("//div[@class='CFT-view-actions-wrapper']//.//li[@class='pagination-last page-item']/a");
                Thread.Sleep(4000);
            }
            lastPage = driver._getText("xpath", "//div[@class='CFT-view-actions-wrapper']//.//li[@class='pagination-page page-item active']");
            totalRecords = (Convert.ToInt32(lastPage) - 1) * 10;

            IList<IWebElement> recordsOnPage = driver.FindElements(By.XPath("//div[@class='CFT-view-actions-wrapper']//.//div[@class='ag-body-container']/div"));
            totalRecords = totalRecords + recordsOnPage.Count;
            Assert.AreEqual(Convert.ToInt32(records), totalRecords, "'" + records + "' Records on Grid not match");

            Results.WriteStatus(test, "Pass", "Verified, Number of Records on Grid not match with Chart Records.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Select No of Records on page from Grid
        /// </summary>
        /// <param name="pageNo">Record per Page</param>
        /// <returns></returns>
        public Search selectNoOfRecordsOnPageFromGrid(string pageNo)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='CFT-view-actions-wrapper']//.//div[@class='btn-group btn-grid-counts pull-right']/button", 20), "'Records per Page' list not present.");
            driver.MouseHoverUsingElement("xpath", "//div[@class='CFT-view-actions-wrapper']//.//div[@class='btn-group btn-grid-counts pull-right']/button");
            IList<IWebElement> noOfPages = driver.FindElements(By.XPath("//div[@class='CFT-view-actions-wrapper']//.//div[@class='btn-group btn-grid-counts pull-right']/button"));
            for (int i = 0; i < noOfPages.Count; i++)
            {
                if (noOfPages[i].Text.Contains(pageNo) == true)
                {
                    driver.MouseHoverByJavaScript(noOfPages[i]);
                    driver._clickByJavaScriptExecutor("//div[@class='CFT-view-actions-wrapper']//.//div[@class='btn-group btn-grid-counts pull-right']/button[" + (i + 1) + "]");
                    break;
                }
            }

            Results.WriteStatus(test, "Pass", "Selected, '" + pageNo + "' Records per page from Grid.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Search tab Button on saved searches screen
        /// </summary>
        /// <param name="buttonName">Button name to Verify</param>
        /// <param name="disabled">Verify Button disable or not</param>
        /// <returns></returns>
        public Search VerifySearchTabButtonOnSavedSearchesScreen(string buttonName, bool disabled)
        {
            IList<IWebElement> buttons = driver.FindElements(By.XPath("//div[@class='CFT-search-list-group-field']//.//button"));
            bool avail = false;
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Text.Contains(buttonName))
                {
                    if (disabled)
                        Assert.AreEqual("true", buttons[i].GetAttribute("disabled"), "'" + buttons[i].Text + "' Search tab Button not disabled.");
                    else
                        Assert.AreEqual(null, buttons[i].GetAttribute("disabled"), "'" + buttons[i].Text + "' Search tab Button disabled.");

                    avail = true;
                    break;
                }
            }

            Assert.AreEqual(true, avail, "'" + buttonName + "' Search tab button not present.");
            Results.WriteStatus(test, "Pass", "Verified '" + buttonName + "' Search tab on Saved Searches screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Click Button for Saved Searches Card on screen
        /// </summary>
        /// <param name="savedSearchedName">Saved Searches Name on Screen</param>
        /// <param name="buttonName">Button Name for Click</param>
        /// <returns></returns>
        public String clickButtonForSavedSearchCardOnScreen(string savedSearchedName, string buttonName)
        {
            string searchTitle = "";
            IList<IWebElement> savedSearches = driver.FindElements(By.XPath("//cft-saved-search-list-item"));

            if (savedSearchedName.Equals("Random"))
            {
                Random rand = new Random();
                int x = rand.Next(0, savedSearches.Count);

                Assert.AreEqual(true, savedSearches[x].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Displayed, "Title of Saved Search not Present.");
                searchTitle = savedSearches[x].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Text;

                IList<IWebElement> buttonLists = savedSearches[x]._findElementsWithinElement("xpath", ".//button[@class='btn btn-default btn-block custom-btn-default']");
                for (int b = 0; b < buttonLists.Count; b++)
                    if (buttonLists[b].Text.Contains(buttonName))
                    {
                        buttonLists[b].Click();
                        Thread.Sleep(3000);
                        driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Loading Pivot Data')]");
                        break;
                    }

                Results.WriteStatus(test, "Pass", "Clicked, '" + buttonName + "' Button for '" + searchTitle + "' Saved Search.");
            }
            else
            {
                bool avail = false;
                for (int l = 0; l < savedSearches.Count; l++)
                {
                    Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Displayed, "Title of Saved Search not Present.");
                    searchTitle = savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Text;

                    if (searchTitle.Contains(savedSearchedName))
                    {
                        IList<IWebElement> buttonLists = savedSearches[l]._findElementsWithinElement("xpath", ".//button[@class='btn btn-default btn-block custom-btn-default']");
                        for (int b = 0; b < buttonLists.Count; b++)
                            if (buttonLists[b].Text.Contains(buttonName))
                            {
                                buttonLists[b].Click();
                                Thread.Sleep(3000);
                                avail = true;
                                driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Loading Pivot Data')]");
                                break;
                            }
                    }
                    if (avail)
                        break;
                }

                Results.WriteStatus(test, "Pass", "Clicked, '" + buttonName + "' Button for '" + searchTitle + "' Saved Search.");
            }

            return searchTitle;
        }

        /// <summary>
        /// Click Delete button for Saved Search Record from List and Verify Message
        /// </summary>
        /// <param name="simpleSavedRecord">Click Simple Saved Record or Scheduled Saved Record</param>
        /// <param name="okay">Click Okay option for Record or Cancel</param>
        /// <returns></returns>
        public String clickDeleteButtonForSavedSearchRecordFromListAndVerifyMessage(bool simpleSavedRecord = false, bool okay = true)
        {
            IList<IWebElement> savedSearches = driver.FindElements(By.XPath("//cft-saved-search-list-item"));
            string searchTitle = "";
            bool availCount = false;
            bool avail = false;
            for (int l = 0; l < savedSearches.Count; l++)
            {
                availCount = (savedSearches[l].FindElements(By.XPath(".//button[contains(@class,'nested-btn-default btn')]")).Count == 0);
                if (availCount == simpleSavedRecord)
                {
                    Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Displayed, "Title of Saved Search not Present.");
                    searchTitle = savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Text;

                    Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//button[@class='btn btn-default btn-block custom-btn-default' and contains(text(),'Delete')]")).Displayed, "'Delete' Button not present.");
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", savedSearches[l].FindElement(By.XPath(".//button[@class='btn btn-default btn-block custom-btn-default' and contains(text(),'Delete')]")));
                    Thread.Sleep(3000);
                    Results.WriteStatus(test, "Pass", "Clicked, Delete button for '" + searchTitle + "' Saved Search Record.");

                    if (simpleSavedRecord == true)
                    {
                        Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='input-group']/span[@class='inline-form-message']")).Displayed, "Content not presnt to 'Delete' Record.");
                        Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='input-group']/span[@class='inline-form-message']")).Text.Contains("Are you sure you want to delete this search?"), "'Are you sure you want to delete this search? ' Message not match.");
                        Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='input-group']/button[contains(@class,'btn btn-default') and contains(text(),'Cancel')]")).Displayed, "'Cancel' Button not present for Saved Record.");
                        Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='input-group']/button[contains(@class,'btn btn-primary') and contains(text(),'Okay')]")).Displayed, "'Okay' Button not present for Saved Record.");

                        if (okay)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", savedSearches[l].FindElement(By.XPath(".//div[@class='input-group']/button[contains(@class,'btn btn-primary') and contains(text(),'Okay')]")));
                            Results.WriteStatus(test, "Pass", "Verfied, Message for Delete Saved Record and Clicked 'Okay' option for Record.");
                        }
                        else
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", savedSearches[l].FindElement(By.XPath(".//div[@class='input-group']/button[contains(@class,'btn btn-default') and contains(text(),'Cancel')]")));
                            Results.WriteStatus(test, "Pass", "Verfied, Message for Delete Saved Record and Clicked 'Cancel' option for Record.");
                        }
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='input-group']/span[@class='inline-form-message']")).Displayed, "Content not presnt to 'Delete' Record.");
                        Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='input-group']/button[contains(@class,'btn btn-default') and contains(text(),'Okay')]")).Displayed, "'Okay' Button not present for Saved Record.");
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", savedSearches[l].FindElement(By.XPath(".//div[@class='input-group']/button[contains(@class,'btn btn-default') and contains(text(),'Okay')]")));
                        Thread.Sleep(1000);
                        Results.WriteStatus(test, "Pass", "Verfied, Message for Delete Saved Record and Clicked 'Okay' option for Record.");
                    }

                    avail = true;
                    break;
                }
            }
            Assert.AreEqual(true, avail, "Record not present to Delete Record from list.");
            return searchTitle;
        }

        /// <summary>
        /// Get Save
        /// </summary>
        /// <param name="savedSearchedName"></param>
        /// <param name="buttonName"></param>
        /// <param name="enterNewAndSave"></param>
        /// <returns></returns>
        public Search getSavedSearchNameOrClickForSavedSearchRecordOnScreen(string savedSearchedName, string buttonName, bool enterNewAndSave = false)
        {
            string searchTitle = "";
            string newSearcheName = "Test" + driver._randomString(4, true);
            IList<IWebElement> savedSearches = driver.FindElements(By.XPath("//cft-saved-search-list-item"));

            if (savedSearchedName.Equals("Random"))
            {
                Random rand = new Random();
                int x = rand.Next(0, savedSearches.Count);

                Assert.AreEqual(true, savedSearches[x].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Displayed, "Title of Saved Search not Present.");
                searchTitle = savedSearches[x].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Text;
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", savedSearches[x].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")));

                Assert.AreEqual(true, savedSearches[x].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]")).Displayed, "Edit Section for Name not present.");
                Assert.AreEqual(true, savedSearches[x].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/input")).GetAttribute("value").Contains(searchTitle), "'" + searchTitle + "' Name not present.");
                Assert.AreEqual(true, savedSearches[x].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/button[contains(text(),'Cancel')]")).Displayed, "'Cancel Button not Present for '" + searchTitle + "' Name.");
                Assert.AreEqual(true, savedSearches[x].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/button[contains(text(),'Clear')]")).Displayed, "'Clear Button not Present for '" + searchTitle + "' Name.");
                Assert.AreEqual(true, savedSearches[x].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/button[contains(text(),'Save')]")).Displayed, "'Save Button not Present for '" + searchTitle + "' Name.");

                if (enterNewAndSave)
                {
                    IWebElement ele = savedSearches[x].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/input"));
                    ele.Clear();
                    ele.SendKeys(newSearcheName);
                    Results.WriteStatus(test, "Pass", "Entered, '" + newSearcheName + "' Title Name on Saved Search Input area.");
                }

                if (buttonName != "")
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", savedSearches[x].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/button[contains(text(),'" + buttonName + "')]")));
                    Thread.Sleep(3000);
                    Results.WriteStatus(test, "Pass", "Clicked, '" + buttonName + "' Button for Saved Search Record.");
                }

                if (buttonName.Contains("Save"))
                {
                    Assert.AreEqual(newSearcheName, savedSearches[x].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Text, "'" + newSearcheName + "' Saved Search Name not Changed.");
                    Results.WriteStatus(test, "Pass", "Verified, New Saved Search Name on Screen.");
                }
                else
                    if (buttonName.Contains("Cancel"))
                {
                    Assert.AreEqual(true, savedSearches[x].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Displayed, "Title of Saved Search not Present.");
                    Results.WriteStatus(test, "Pass", "Verified, Section after Clicking Cancel Button for Search Name.");
                }
                else
                        if (buttonName.Contains("Clear"))
                {
                    Assert.AreEqual(true, savedSearches[x].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/input")).GetAttribute("value").Contains(""), "'" + searchTitle + "' Name not not clear.");
                    Results.WriteStatus(test, "Pass", "Verified, Input Area clear for Saved Search Name.");
                }

            }
            else
            {
                for (int l = 0; l < savedSearches.Count; l++)
                {
                    Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Displayed, "Title of Saved Search not Present.");
                    searchTitle = savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Text;

                    if (searchTitle.Contains(savedSearchedName))
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")));

                        Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath("//cft-saved-search-list-item//.//div[contains(@class,'inline-form-message')]")).Displayed, "Edit Section for Name not present.");
                        Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/input")).GetAttribute("value").Contains(searchTitle), "'" + searchTitle + "' Name not present.");
                        Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/button[contains(text(),'Cancel')]")).Displayed, "'Cancel Button not Present for '" + searchTitle + "' Name.");
                        Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/button[contains(text(),'Clear')]")).Displayed, "'Clear Button not Present for '" + searchTitle + "' Name.");
                        Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/button[contains(text(),'Save')]")).Displayed, "'Save Button not Present for '" + searchTitle + "' Name.");

                        if (enterNewAndSave)
                        {
                            IWebElement ele = savedSearches[l].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/input"));
                            ele.Clear();
                            ele.SendKeys(newSearcheName);
                            Results.WriteStatus(test, "Pass", "Entered, '" + newSearcheName + "' Title Name on Saved Search Input area.");
                        }

                        if (buttonName != "")
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", savedSearches[l].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/button[contains(text(),'" + buttonName + "')]")));
                            Thread.Sleep(3000);
                            Results.WriteStatus(test, "Pass", "Clicked, '" + buttonName + "' Button for Saved Search Record.");
                        }

                        if (buttonName.Contains("Save"))
                        {
                            Assert.AreEqual(newSearcheName, savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Text, "'" + newSearcheName + "' Saved Search Name not Changed.");
                            Results.WriteStatus(test, "Pass", "Verified, New Saved Search Name on Screen.");
                        }
                        else
                            if (buttonName.Contains("Cancel"))
                        {
                            Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Displayed, "Title of Saved Search not Present.");
                            Results.WriteStatus(test, "Pass", "Verified, Section after Clicking Cancel Button for Search Name.");
                        }
                        else
                                if (buttonName.Contains("Clear"))
                        {
                            Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[contains(@class,'inline-form-message')]/input")).GetAttribute("value").Contains(""), "'" + searchTitle + "' Name not not clear.");
                            Results.WriteStatus(test, "Pass", "Verified, Input Area clear for Saved Search Name.");
                        }

                        break;
                    }
                }
            }

            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Saved Search Name from list
        /// </summary>
        /// <param name="savedSearchName">Saved Search Name to Verify</param>
        /// <returns></returns>
        public Search VerifySavedSearchNameFromList(string savedSearchName)
        {
            IList<IWebElement> savedSearches = driver.FindElements(By.XPath("//cft-saved-search-list-item"));
            bool avail = false;
            for (int l = 0; l < savedSearches.Count; l++)
            {
                Assert.AreEqual(true, savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Displayed, "Title of Saved Search not Present.");
                string searchTitle = savedSearches[l].FindElement(By.XPath(".//div[@class='checkbox-header-lead-text cursor-pointer']")).Text;

                if (searchTitle.Contains(savedSearchName))
                {
                    avail = true;
                    break;
                }
            }

            Assert.AreEqual(true, avail, "'" + savedSearchName + "' Saved Search Record not Present.");
            Results.WriteStatus(test, "Pass", "Verified, '" + savedSearchName + "' Saved Search Record from List.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Save As Section after clicking on Save As button
        /// </summary>
        /// <returns></returns>
        public Search VerifySaveAsSectionAfterClickingOnSaveAsButton()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//input[contains(@placeholder,'What would you like to call your search') and @type='text']", 20), "'What would you like to call your search?' textarea not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//label[@class='field-checkbox']/span[@class='text-muted' and contains(text(),'Make Default')]"), "'Make Default' Checkbox Label not present.");
            Assert.AreEqual("rgba(119, 119, 119, 1)", driver.FindElement(By.XPath("//label[@class='field-checkbox']/span[@class='text-muted' and contains(text(),'Make Default')]")).GetCssValue("color"), "'Make Default' Button is not UnChecked.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//label[@class='field-checkbox']/span[@class='text-muted' and contains(text(),'Continue Editing')]"), "'Continue Editing' Checkbox Label not present.");
            Assert.AreEqual("rgba(0, 74, 82, 1)", driver.FindElement(By.XPath("//label[@class='field-checkbox']/span[@class='text-muted' and contains(text(),'Continue Editing')]")).GetCssValue("color"), "'Continue Editing' Button not Checked and not Highlighted with Blue color.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Cancel')]"), "'Cancel' Button not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-success' and contains(text(),'Save!') and @disabled]"), "'Save!' Button in Disable manner not Present.");

            Results.WriteStatus(test, "Pass", "Verified, Save As section after clicking on Save As button.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Check or UnChecked Checkbox for Saved Search
        /// </summary>
        /// <param name="checkboxName">Checkbox Name to perform action</param>
        /// <param name="unChecked">UnChecked Checkbox</param>
        /// <returns></returns>
        public Search checkOrUnCheckCheckboxForSavedSearch(string checkboxName, bool unChecked)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//label[@class='field-checkbox']/span[@class='text-muted' and contains(text(),'" + checkboxName + "')]"), "'" + checkboxName + "' Checkbox Label not present.");

            if (unChecked)
            {
                if (driver.FindElement(By.XPath("//label[@class='field-checkbox']/span[@class='text-muted' and contains(text(),'" + checkboxName + "')]")).GetCssValue("color").Contains("0, 74, 82") == true)
                {
                    driver._clickByJavaScriptExecutor("//label[@class='field-checkbox']/span[@class='text-muted' and contains(text(),'" + checkboxName + "')]");
                    Results.WriteStatus(test, "Pass", "UnChecked, '" + checkboxName + "' Checkbox for Saved Search.");
                }
                else
                    Results.WriteStatus(test, "Pass", "'" + checkboxName + "' Checkbox Already Unchecked for Saved Search.");

                Assert.AreEqual("rgba(119, 119, 119, 1)", driver.FindElement(By.XPath("//label[@class='field-checkbox']/span[@class='text-muted' and contains(text(),'" + checkboxName + "')]")).GetCssValue("color"), "'" + checkboxName + "' Button not UnChecked.");
            }
            else
            {
                if (driver.FindElement(By.XPath("//label[@class='field-checkbox']/span[@class='text-muted' and contains(text(),'" + checkboxName + "')]")).GetCssValue("color").Contains("0, 74, 82") == false)
                {
                    driver._clickByJavaScriptExecutor("//label[@class='field-checkbox']/span[@class='text-muted' and contains(text(),'" + checkboxName + "')]");
                    Results.WriteStatus(test, "Pass", "Checked, '" + checkboxName + "' Checkbox for Saved Search.");
                }
                else
                    Results.WriteStatus(test, "Pass", "'" + checkboxName + "' Checkbox Already Checked for Saved Search.");

                Assert.AreEqual("rgba(0, 74, 82, 1)", driver.FindElement(By.XPath("//label[@class='field-checkbox']/span[@class='text-muted' and contains(text(),'" + checkboxName + "')]")).GetCssValue("color"), "'" + checkboxName + "' Button not Checked.");
            }

            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Applied Search field in chart details section
        /// </summary>
        /// <param name="savedSearchName">Saved Search Name to Verify</param>
        /// <returns></returns>
        public Search VerifyAppliedSearchFieldInChartDetailsSection(string savedSearchName)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-applied-my-search-summary/div"), "'Applied Search' section not present on Detail section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-applied-my-search-summary/div//.//div[contains(@class,'summary-header')]"), "'Applied Search' Header not present.");

            if (savedSearchName.Equals("None Selected") == false)
                Assert.AreEqual(false, driver._getText("xpath", "//cft-applied-my-search-summary/div[@class='search-summary-item']").Contains("None Selected"), "Applied Search not Applied for any Search.");
            Assert.AreEqual(true, driver._getText("xpath", "//cft-applied-my-search-summary/div[@class='search-summary-item']").Contains(savedSearchName), "'" + savedSearchName + " Saved Search not Applied.");

            Results.WriteStatus(test, "Pass", "Verified, Applied Search field in Chart Details Section.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Pagination Panel of Saved Searched
        /// </summary>
        /// <returns></returns>
        public Search VerifyPaginationPanelOfSavedSearched()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-saved-search-list//.//div[@class='row cft-pagination-wrapper']"), "Pagination Panel not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-saved-search-list//.//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-prev page-item disabled']"), "Previous Icon Default not Disable.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-saved-search-list//.//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-page page-item active']"), "First Page not Active.");
            if (driver._isElementPresent("xpath", "//cft-saved-search-list//.//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-next page-item']") == false)
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-saved-search-list//.//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-next page-item disabled']"), "Next Icon not Present.");

            Results.WriteStatus(test, "Pass", "Verified, Pagination Panel of Saved Searched.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Click on Button from Pagination Panel
        /// </summary>
        /// <param name="pagePosition">Page Position to Click</param>
        /// <returns></returns>
        public Search clickOnButtonFromPaginationPanel(string pagePosition)
        {
            if (driver._isElementPresent("xpath", "//cft-saved-search-list//.//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-" + pagePosition + " page-item']/a") == true)
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-saved-search-list//.//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-" + pagePosition + " page-item']/a"), "'" + pagePosition.ToUpper() + "' Icon not Enable.");
                driver._clickByJavaScriptExecutor("//cft-saved-search-list//.//div[@class='row cft-pagination-wrapper']//.//li[@class='pagination-" + pagePosition + " page-item']/a");
                Results.WriteStatus(test, "Pass", "Clicked, '" + pagePosition.ToUpper() + "' Icon Button from Pagination Panel.");
            }
            else
                Results.WriteStatus(test, "Pass", "Alerady on '" + pagePosition.ToUpper() + "' Page.");

            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Overwrite section with message on screen
        /// </summary>
        /// <param name="overWrite">Click Overwrite Button on screen</param>
        /// <returns></returns>
        public Search VerifyOverwriteSectionWithMessageOnScreen(bool overWrite)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//label[@class='field-title']", 20), "Reset Message not present.");
            Assert.AreEqual("Great we see that you are trying to overwrite the current applied search. Are you sure you want to overwrite?", driver._getText("xpath", "//label[@class='field-title']"), "Overwrite Message not match.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Cancel')]"), "'Cancel' Button not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//button[@class='btn btn-default' and contains(text(),'Overwrite')]"), "'Overwrite' Button not Present.");
            Results.WriteStatus(test, "Pass", "Verified, Reset Changes message on screen.");

            if (overWrite)
                clickButtonOnSearchScreen("Overwrite");
            else
                clickButtonOnSearchScreen("Cancel");

            return new Search(driver, test);
        }

        #region Ad Code

        /// <summary>
        /// Verify Ad Code Section on screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyAdCodeSectionOnScreen()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-adcode-search-my-search//.//div[@class='panel-summary-header']"), "Ad Code Section not present.");
            Assert.AreEqual(true, driver._getText("xpath", "//cft-field-editor-adcode-search-my-search//.//div[@class='panel-summary-header']").Contains("Ad Code"), "Ad Code Header not match.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-adcode-search-my-search//.//div[@class='CFT-textbox']"), "Enter adcodes... not present.");
            Assert.AreEqual(true, driver._getAttributeValue("xpath", "//cft-field-editor-adcode-search-my-search//.//div[@class='CFT-textbox']/textarea", "placeholder").Contains("Enter adcodes"), "Enter adcodes... Placeholder not present.");

            Results.WriteStatus(test, "Pass", "Verified, Ad Code section on search screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Enter Ad Code in Ad Code search area on screen
        /// </summary>
        /// <param name="adCode">Ad Code to Search</param>
        /// <returns></returns>
        public Search enterAdCodeInAdCodeSearchAreaOnScreen(string adCode)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-adcode-search-my-search//.//div[@class='CFT-textbox']/textarea"), "Ad Code Search area not present.");
            driver._type("xpath", "//cft-field-editor-adcode-search-my-search//.//div[@class='CFT-textbox']/textarea", adCode);
            Thread.Sleep(3000);
            Results.WriteStatus(test, "Pass", "Entered, '" + adCode + "' Ad Code on Search area on screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Chart Record value on Search screen
        /// </summary>
        /// <returns></returns>
        public String VerifyChartRecordValueOnsearchScreen()
        {
            string recordCollection = "";
            if (driver._getText("xpath", "//*[@id='CftSearchSummary']/div[1]").Contains("No Data Found") == true)
            {
                recordCollection = "No items found!";
                Results.WriteStatus(test, "Pass", "'No Data Found' for Search Record");
            }
            else
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='CftSearchSummary']/div[1]//.//*[name()='text' and @class='highcharts-title']"), "Chart Record Collection not present.");
                recordCollection = driver._getText("xpath", "//*[@id='CftSearchSummary']/div[1]//.//*[name()='text' and @class='highcharts-title']").Trim().Replace("\r\n", "").Replace(",", "");
                Results.WriteStatus(test, "Pass", "Verified, Chart Record Value on Search screen.");
            }

            return recordCollection;
        }

        /// <summary>
        /// Verify Grid Records on screen
        /// </summary>
        /// <param name="chartValue">Chart Record Value</param>
        /// <returns></returns>
        public Search VerifyGridRecordsOnScreen(string chartValue)
        {
            if (chartValue.Equals("No items found!"))
            {
                Assert.AreEqual(false, driver._isElementPresent("xpath", "//div[@class='CFT-view-actions-wrapper']//.//div[@class='ag-body-container']/div"), "'No items found!' for Grid not found.");
                Results.WriteStatus(test, "Pass", "Verified, No items found! message for Grid.");
            }
            else
                VerifyNumberOfRecordCollectionsOnGrid(chartValue);

            return new Search(driver, test);
        }

        #endregion

        #region Summary By Category

        /// <summary>
        /// Select Media checkbox option for Annual Summary
        /// </summary>
        /// <param name="mediaName">Media name to Select</param>
        /// <returns></returns>
        public Search selectMediaCheckboxOptionForAnnualSummary(string mediaName = "Random")
        {
            IList<IWebElement> mediaCollections = driver.FindElements(By.XPath("//div[contains(@id,'media')]//.//div[@class='ag-body-viewport']/div/div"));
            bool avail = false;

            if (mediaName.Equals("Random"))
            {
                Random rand = new Random();
                int x = rand.Next(0, mediaCollections.Count);
                mediaName = mediaCollections[x].Text;
                driver._clickByJavaScriptExecutor("//div[contains(@id,'media')]//.//div[@class='ag-body-viewport']/div/div[" + (x + 1) + "]/div/span");
                Results.WriteStatus(test, "Pass", "Selected, '" + mediaName + "' Date Range from Section.");
            }
            else
            {
                for (int i = 0; i < mediaCollections.Count; i++)
                {
                    if (mediaCollections[i].Text.Contains(mediaName))
                    {
                        driver._clickByJavaScriptExecutor("//div[contains(@id,'media')]//.//div[@class='ag-body-viewport']/div/div[" + (i + 1) + "]");
                        avail = true; Thread.Sleep(2000);
                        break;
                    }
                }

                Assert.AreEqual(true, avail, "'" + mediaName + "' Media not Present.");
                Results.WriteStatus(test, "Pass", "Selected, '" + mediaName + "' Media from Section.");
            }

            return new Search(driver, test);
        }

        /// <summary>
        /// Create New Search or click saved search to Apply Search on screen
        /// </summary>
        /// <param name="applySavedSearch">Click on Apply Saved Search</param>
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
                VerifyFieldMenuAndClickOnItOnSearchScreen("Media").selectMediaCheckboxOptionForAnnualSummary();
                clickButtonOnSearchScreen("Save As");

                Assert.IsTrue(driver._waitForElement("xpath", "//input[contains(@placeholder,'What would you like to call your search') and @type='text']", 20), "'What would you like to call your search?' textarea not Present.");
                driver._clickByJavaScriptExecutor("//input[contains(@placeholder,'What would you like to call your search') and @type='text']");
                scheduleSearchName = "Test" + driver._randomString(4, true);
                driver._type("xpath", "//input[contains(@placeholder,'What would you like to call your search') and @type='text']", scheduleSearchName);
                Results.WriteStatus(test, "Pass", "Entered Save As Search Report Name on Screen.");

                clickButtonOnSearchScreen("Save!");
                if (applySavedSearch)
                    clickButtonOnSearchScreen("Apply Search");
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

            Thread.Sleep(2000);
            return scheduleSearchName;
        }

        #endregion

        #endregion

        #region new Search Methods

        /// <summary>
        /// Verify New Ad Code Section on screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyNewAdCodeSectionOnScreen()
        {
            Assert.AreEqual(true, driver._waitForElement("xpath", "//cft-field-editor-adcode-search-my-search//h6"), "Ad Code Section not present.");
            Assert.AreEqual("Ad Code".ToLower(), driver._getText("xpath", "//cft-field-editor-adcode-search-my-search//h6").ToLower(), "Ad Code Header not match.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-adcode-search-my-search//input"), "Enter adcodes... not present.");
            Assert.AreEqual(true, driver._getAttributeValue("xpath", "//cft-field-editor-adcode-search-my-search//input", "placeholder").ToLower().Contains("Enter adcodes".ToLower()), "Enter adcodes... Placeholder not present.");

            Results.WriteStatus(test, "Pass", "Verified, Ad Code section on search screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Enter Ad Code in New Ad Code search area on screen
        /// </summary>
        /// <param name="adCode">Ad Code to Search</param>
        /// <returns></returns>
        public string enterAdCodeInNewAdCodeSearchAreaOnScreen(string adCode)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-adcode-search-my-search//input"), "Ad Code Search area not present.");
            driver._type("xpath", "//cft-field-editor-adcode-search-my-search//input", adCode);
            Thread.Sleep(3000);
            Results.WriteStatus(test, "Pass", "Entered, '" + adCode + "' Ad Code on Search area on screen.");
            return adCode;
        }

        /// <summary>
        /// Verify New Date Range Section on screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyNewDateRangeSectionOnScreen(string defaultRange = "Last 7 Days")
        {
            Assert.AreEqual(true, driver._waitForElement("xpath", "//cft-field-editor-timeframe-calendar//cft-field-editor-header//h6"), "Date Range Section not present.");
            Assert.AreEqual("Date Range".ToLower(), driver._getText("xpath", "//cft-field-editor-timeframe-calendar//cft-field-editor-header//h6").ToLower(), "Date Range Header not match.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-timeframe-calendar/div"), "Date Range Filter not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-timeframe-calendar//ul/li"), "Date Range Types not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-timeframe-calendar//div[contains(@class, 'first')]//table"), "First Calendar not present in Date Range Filter");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-timeframe-calendar//div[contains(@class, 'second')]//table"), "Second Calendar not present in Date Range Filter");

            IList<IWebElement> dateRangeTypeColl = driver._findElements("xpath", "//cft-field-editor-timeframe-calendar//ul/li");
            bool avail = false;
            foreach (IWebElement dateRangeType in dateRangeTypeColl)
                if (dateRangeType.Text.Contains(defaultRange))
                {
                    avail = true;
                    dateRangeType.Click();
                    Thread.Sleep(2000);
                    Assert.IsTrue(dateRangeType.GetAttribute("class").Contains("active"), "'" + defaultRange + "' Type is not selected");
                }
            Assert.IsTrue(avail, "'" + defaultRange + "' not found");

            Results.WriteStatus(test, "Pass", "Verified, Date Range section on search screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Select New Date Range option from Section
        /// </summary>
        /// <param name="dateRange">Date Range to Select</param>
        /// <returns></returns>
        public string selectNewDateRangeOptionFromSection(string dateRange = "Random")
        {
            IList<IWebElement> dateRangeCollections = driver.FindElements(By.XPath("//cft-field-editor-timeframe-calendar//ul/li"));
            bool avail = false;

            if (dateRange.Equals("Random"))
            {
                Random rand = new Random();
                int x = rand.Next(0, dateRangeCollections.Count);
                dateRange = dateRangeCollections[x].Text;
                driver._clickByJavaScriptExecutor("//cft-field-editor-timeframe-calendar//ul/li[" + (x + 1) + "]");
                Thread.Sleep(500);
                Results.WriteStatus(test, "Pass", "Selected, '" + dateRange + "' Date Range from Section.");
            }
            else
            {
                for (int i = 0; i < dateRangeCollections.Count; i++)
                {
                    if (dateRangeCollections[i].Text == dateRange)
                    {
                        driver._clickByJavaScriptExecutor("//cft-field-editor-timeframe-calendar//ul/li[" + (i + 1) + "]");
                        avail = true; Thread.Sleep(2000);
                        break;
                    }
                }

                Assert.AreEqual(true, avail, "'" + dateRange + "' Date Range not Present.");
                Results.WriteStatus(test, "Pass", "Selected, '" + dateRange + "' Date Range from Section.");
            }

            return dateRange;
        }

        ///<summary>
        ///Set Custom Date Range
        ///</summary>
        ///<param name="fromDate">Start Date</param>
        ///<param name="toDate">End Date</param>
        ///<returns></returns>
        public Search setCustomDateRange(string fromDate, string toDate = "")
        {
            DateTime date1 = DateTime.Today;
            Assert.IsTrue(DateTime.TryParse(fromDate, out date1), "From Date Conversion Failed");
            Console.WriteLine("From Date as DateTime Object: " + date1);

            int iFromYear = date1.Year;
            int iFromDay = date1.Day;

            string fromYear = iFromYear.ToString();
            string fromMonth = date1.ToString("MMM");
            string fromDay = iFromDay.ToString();


            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'yearselect')]"), "Year Field not present in First Calendar");
            bool avail = false;
            if (!driver._getText("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'yearselect')]/option[@selected]").Contains(fromYear))
            {
                driver._click("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'yearselect')]");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'yearselect')]/option"), "From Year DDL not present");
                IList<IWebElement> yearDDLCollection = driver._findElements("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'yearselect')]/option");
                foreach (IWebElement yearDDL in yearDDLCollection)
                    if (yearDDL.Text.Contains(fromYear))
                    {
                        avail = true;
                        yearDDL.Click();
                        break;
                    }
                Assert.IsTrue(avail, "'" + fromYear + "' is too Old a date!");
                Thread.Sleep(1000);
                Assert.IsTrue(driver._getText("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'yearselect')]/option[@selected]").Contains(fromYear), "From Year was not set");
            }

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'monthselect')]"), "Month Field not present in First Calendar");
            if (!driver._getText("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'monthselect')]/option[@selected]").Contains(fromMonth))
            {
                driver._click("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'monthselect')]");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'monthselect')]/option"), "From Year DDL not present");
                IList<IWebElement> monthDDLCollection = driver._findElements("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'monthselect')]/option");
                avail = false;
                foreach (IWebElement monthDDL in monthDDLCollection)
                    if (monthDDL.Text.Contains(fromMonth))
                    {
                        avail = true;
                        monthDDL.Click();
                        break;
                    }
                Assert.IsTrue(avail, "'" + fromMonth + "' not found");
                Thread.Sleep(1000);
                Assert.IsTrue(driver._getText("xpath", "//div[contains(@class, 'first')]//th/select[contains(@class, 'monthselect')]/option[@selected]").Contains(fromMonth), "From Month was not set");
            }

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'first')]//td[contains(@class,'available') and not(contains(@class, 'off'))]"), "Days Selector not present in First Calendar");
            if (!driver._isElementPresent("xpath", "//div[contains(@class, 'first')]//td[@class='available active start-date']") || (driver._isElementPresent("xpath", "//div[contains(@class, 'first')]//td[@class='available active start-date']") && !driver._getText("xpath", "//div[contains(@class, 'first')]//td[@class='available active start-date']").Equals(fromDay)))
            {
                IList<IWebElement> daysCollection = driver._findElements("xpath", "//div[contains(@class, 'first')]//td[contains(@class,'available') and not(contains(@class, 'off'))]");
                avail = false;
                foreach (IWebElement day in daysCollection)
                    if (day.Text.Contains(fromDay))
                    {
                        avail = true;
                        day.Click();
                        break;
                    }
                Assert.IsTrue(avail, "'" + fromDay + "' not found");
                Thread.Sleep(1000);
            }

            DateTime date2 = DateTime.Today;
            if (toDate.Equals(""))
            {
                toDate = date2.ToString("MM/dd/yyyy");
                Console.WriteLine("toDate: " + toDate);
            }
            else
            {
                Assert.IsTrue(DateTime.TryParse(toDate, out date2), "To Date Conversion Failed");
                int iToYear = date1.Year;
                int iToDay = date1.Day;

                string toYear = iToYear.ToString();
                string toMonth = date1.ToString("MMM");
                string toDay = iToDay.ToString();

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'yearselect')]"), "Year Field not present in second Calendar");
                if (!driver._getText("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'yearselect')]/option[@selected]").Contains(toYear))
                {
                    driver._click("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'yearselect')]");
                    Assert.IsTrue(driver._waitForElement("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'yearselect')]/option"), "to Year DDL not present");
                    IList<IWebElement> yearDDLCollection = driver._findElements("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'yearselect')]/option");
                    avail = false;
                    foreach (IWebElement yearDDL in yearDDLCollection)
                        if (yearDDL.Text.Contains(toYear))
                        {
                            avail = true;
                            yearDDL.Click();
                            break;
                        }
                    Assert.IsTrue(avail, "'" + toYear + "' is too Old a date!");
                    Thread.Sleep(1000);
                    Assert.IsTrue(driver._getText("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'yearselect')]/option[@selected]").Contains(toYear), "to Year was not set");
                }

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'monthselect')]"), "Month Field not present in second Calendar");
                if (!driver._getText("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'monthselect')]/option[@selected]").Contains(toMonth))
                {
                    driver._click("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'monthselect')]");
                    Assert.IsTrue(driver._waitForElement("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'monthselect')]/option"), "to Year DDL not present");
                    IList<IWebElement> monthDDLCollection = driver._findElements("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'monthselect')]/option");
                    avail = false;
                    foreach (IWebElement monthDDL in monthDDLCollection)
                        if (monthDDL.Text.Contains(toMonth))
                        {
                            avail = true;
                            monthDDL.Click();
                            break;
                        }
                    Assert.IsTrue(avail, "'" + toMonth + "' not found");
                    Thread.Sleep(1000);
                    Assert.IsTrue(driver._getText("xpath", "//div[contains(@class, 'second')]//th/select[contains(@class, 'monthselect')]/option[@selected]").Contains(toMonth), "to Month was not set");
                }

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'second')]//td[contains(@class,'available') and not(contains(@class, 'off'))]"), "Days Selector not present in second Calendar");
                if (!driver._isElementPresent("xpath", "//div[contains(@class, 'second')]//td[@class='available active start-date']") || (driver._isElementPresent("xpath", "//div[contains(@class, 'second')]//td[@class='available active start-date']") && !driver._getText("xpath", "//div[contains(@class, 'second')]//td[@class='available active start-date']").Equals(toDay)))
                {
                    IList<IWebElement> daysCollection = driver._findElements("xpath", "//div[contains(@class, 'second')]//td[contains(@class,'available') and not(contains(@class, 'off'))]");
                    avail = false;
                    foreach (IWebElement day in daysCollection)
                        if (day.Text.Contains(toDay))
                        {
                            avail = true;
                            day.Click();
                            break;
                        }
                    Assert.IsTrue(avail, "'" + toDay + "' not found");
                    Thread.Sleep(1000);
                    Assert.IsTrue(driver._getText("xpath", "//div[contains(@class, 'second')]//td[@class='available active start-date']").Contains(toDay), "to Day was not set");
                }
            }

            IList<IWebElement> dateRangeTypeColl = driver._findElements("xpath", "//cft-field-editor-timeframe-calendar//ul/li");
            avail = false;
            foreach (IWebElement dateRangeType in dateRangeTypeColl)
                if (dateRangeType.Text.Contains("Custom Range"))
                {
                    avail = true;
                    Assert.IsTrue(dateRangeType.GetAttribute("class").Contains("active"), "Custom Range Type is not selected");
                }
            Assert.IsTrue(avail, "'Custom Range' not found");

            string dateRange = date1.ToString("MM/dd/YYYY") + " to " + date2.ToString("MM/dd/YYYY");
            if (dateRange.ToCharArray()[0] == '0')
                dateRange = dateRange.Substring(1);
            if (dateRange.ToCharArray()[dateRange.Length - 10] == '0')
                dateRange = dateRange.Substring(dateRange.Length - 10, 1);

            Results.WriteStatus(test, "Pass", "Set, Custom Date Range as '" + fromDate + "' to '" + toDate + "'.");
            return new Search(driver, test);
        }

        ///<summary>
        ///Click Button on Search Options
        ///</summary>
        ///<param name="buttonName">Button to be clicked</param>
        ///<returns></returns>
        public Search clickButtonOnSearchOptions(string buttonName)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'modal-footer')]//button"), "Buttons are not present on Search Options");
            IList<IWebElement> buttonsCollection = driver._findElements("xpath", "//div[contains(@class, 'modal-footer')]//button");
            bool avail = false;
            foreach (IWebElement button in buttonsCollection)
                if (button.Text.ToLower().Contains(buttonName.ToLower()))
                {
                    avail = true;
                    button.Click();
                    break;
                }
            Assert.IsTrue(avail, "'" + buttonName + "' not found");
            Thread.Sleep(3000);

            Results.WriteStatus(test, "Pass", "Clicked, '" + buttonName + "' on Search Options.");
            return new Search(driver, test);
        }

        ///<summary>
        ///Select Tab on Search Options
        ///</summary>
        ///<param name="tabName">Tab to be selected</param>
        ///<returns></returns>
        public Search selectTabOnSearchOptions(string tabName)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class,'modal-body')]//button[contains(@class, 'rounded') and not(@id)]"), "Buttons are not present on Search Options");
            IList<IWebElement> tabsCollection = driver._findElements("xpath", "//div[contains(@class,'modal-body')]//button[contains(@class, 'rounded') and not(@id)]");
            bool avail = false;
            foreach (IWebElement tab in tabsCollection)
                if (tab.Text.ToLower().Contains(tabName.ToLower()))
                {
                    avail = true;
                    tab.Click();
                    break;
                }
            Assert.IsTrue(avail, "'" + tabName + "' not found");
            Thread.Sleep(3000);

            Results.WriteStatus(test, "Pass", "Clicked, '" + tabName + "' on Search Options.");
            return new Search(driver, test);
        }

        ///<summary>
        ///Edit or Remove Current Search
        ///</summary>
        ///<returns></returns>
        public Search editOrRemoveCurrentSearch(bool edit)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div/cft-tag//div[@class='NU-tag-label']"), "Current Search Name not present on Search Options");
            if (edit)
            {
                driver._click("xpath", "//div/cft-tag//span[contains(@class,'NU-icon-pencil')]");
                Results.WriteStatus(test, "Pass", "Clicked, on Edit Icon of current");
            }
            else
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//div/cft-tag//div[contains(@class,'NU-tag-action')]/span"), "Current Search Remove Icon not present on Search Options");
                driver._click("xpath", "//div/cft-tag//div[contains(@class,'NU-tag-action')]/span");
                Results.WriteStatus(test, "Pass", "Clicked, on Remove Icon of current");
            }
            Thread.Sleep(1000);
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify New Media Section on screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyNewMediaSectionOnScreen()
        {
            Assert.AreEqual(true, driver._waitForElement("xpath", "//cft-field-editor-multi-tree//h6"), "Media Section not present.");
            Assert.AreEqual("Media".ToLower(), driver._getText("xpath", "//cft-field-editor-multi-tree//h6").ToLower(), "Media Header not match.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]"), "Media Filter List not present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-viewport']"), "Selected Media Filter List not present.");

            Results.WriteStatus(test, "Pass", "Verified, Media section on search screen.");
            return new Search(driver, test);
        }

        ///<summary>
        ///Select Specific Media Type on Search Options
        ///</summary>
        ///<param name="mediaType">Media Type to be selected</param>
        ///<returns></returns>
        public string selectSpecificMediaTypeOnSearchOptions(string option, string subOption = "")
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Media Types...']"), "'Category Class' filter text field not present.");
            if (subOption != "" && !subOption.Equals("Random"))
                driver._type("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Media Types...']", subOption);
            else if (!option.Equals("Random"))
                driver._type("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Media Types...']", subOption);

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]"), "'Filter Options' not present on Category Class filter section.");
            IList<IWebElement> optionCollection = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]");
            bool avail = false;
            if (option.Equals("Random"))
            {
                Random rand = new Random();
                int x = rand.Next(0, optionCollection.Count);
                option = optionCollection[x].Text;
                optionCollection[x].Click();
                Thread.Sleep(500);
                if (option.IndexOf('(') > -1)
                    option = option.Substring(0, option.IndexOf('(') - 1);
                Results.WriteStatus(test, "Pass", "Selected, '" + option + "' Option from Section.");
            }
            else
            {
                for (int i = 0; i < optionCollection.Count; i++)
                {
                    if (optionCollection[i].Text == option)
                    {
                        avail = true;
                        if (subOption == "")
                        {
                            optionCollection[i].Click();
                            break;
                        }
                        else
                        {
                            IList<IWebElement> optionEleColl = optionCollection[i]._findElementsWithinElement("xpath", ".//span[@ref='eContracted");
                            optionEleColl[0].Click();
                            Thread.Sleep(2000);
                            optionCollection = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]");
                            if (subOption.Equals("Random"))
                            {
                                Random rand = new Random();
                                int x = rand.Next(3, optionCollection.Count);
                                subOption = optionCollection[x].Text;
                                optionCollection[x].Click();
                                Thread.Sleep(500);
                                Results.WriteStatus(test, "Pass", "Selected, '" + option + "' Option from Section.");
                            }
                            else
                            {
                                bool avail2 = false;
                                for (int j = 0; j < optionCollection.Count; j++)
                                    if (optionCollection[j].Text == subOption)
                                    {
                                        avail2 = true;
                                        optionCollection[j].Click();
                                    }
                                Assert.AreEqual(true, avail2, "'" + subOption + "' Option not Present.");

                                Thread.Sleep(2000);
                                break;
                            }
                        }
                    }
                }
                Assert.AreEqual(true, avail, "'" + option + "' Option not Present.");
            }

            Results.WriteStatus(test, "Pass", "Selected, '" + option + ", " + subOption + "' option from Category Class Section on search screen");
            return option;
        }

        /// <summary>
        /// Verify Keyword section on search screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyNewKeywordSectionOnSearchScreen(string value = "", bool clear = false)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//h6"), "Keyword Section not present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//h6"), "Keyword Header Section not present.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//input[@type='text']"), "Filter Text area not present.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//label"), "Radio button section not present.");
            IList<IWebElement> radioCollections = driver.FindElements(By.XPath("//cft-field-editor-keyword-search-my-search//label"));
            string[] radioButtonTitles = { "All Fields", "Headline", "Lead Text", "Visual", "Description" };
            for (int i = 0; i < radioCollections.Count; i++)
                Assert.AreEqual(true, radioCollections[i].Text.Contains(radioButtonTitles[i]), "'" + radioButtonTitles[i] + "' Radio Button not present.");

            if (value != "")
            {
                Assert.IsTrue(driver._getValue("xpath", "//div[@class='modal-content']//cft-field-editor-keyword-search-my-search//input[@type='text']").ToLower().Contains(value.ToLower()), "'" + value + "' not present in Filter Bar Text Box");
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='modal-content']//cft-field-editor-keyword-search-my-search//button"), "Clear button not present in Filter Bar.");
            }

            if (clear)
                driver._click("xpath", "//div[@class='modal-content']//cft-field-editor-keyword-search-my-search//button");

            Results.WriteStatus(test, "Pass", "Verified, Keyword section on search screen.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Enter Keyword in Search area on screen
        /// </summary>
        /// <param name="filterValue">Filter Value to enter</param>
        /// <returns></returns>
        public String enterNewKeywordInSearchAreaOnScreen(string filterValue, string option = "All Fields")
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='modal-content']//cft-field-editor-keyword-search-my-search//input[@type='text']"), "Filter Text area not present.");
            string searchValue = "";

            if (filterValue.Equals("Existing"))
                searchValue = "test";

            if (filterValue.Equals("Random"))
                searchValue = driver._randomString(6) + driver._randomString(4, true);

            if (driver._getValue("xpath", "//div[@class='modal-content']//cft-field-editor-keyword-search-my-search//input[@type='text']") != "")
                driver._clearText("xpath", "//div[@class='modal-content']//cft-field-editor-keyword-search-my-search//input[@type='text']");
            driver._type("xpath", "//div[@class='modal-content']//cft-field-editor-keyword-search-my-search//input[@type='text']", filterValue);

            IList<IWebElement> radioCollections = driver.FindElements(By.XPath("//cft-field-editor-keyword-search-my-search//label"));
            bool avail = false;
            foreach(IWebElement radio in radioCollections)
                if (radio.Text.ToLower().Contains(option.ToLower()))
                {
                    radio.Click();
                    avail = true;
                    break;
                }
            Assert.IsTrue(avail, "'" + option + "' not found in Radio Buttons.");

            Thread.Sleep(4000);
            Results.WriteStatus(test, "Pass", "Entered, '" + filterValue + "' Filter Text for Keyword section on screen.");
            return searchValue;
        }

        /// <summary>
        /// Select Radio option from keyword section
        /// </summary>
        /// <param name="radioOption">Radio optio to Select</param>
        /// <returns></returns>
        public String selectRadioOptionFormNewKeywordSection(string radioOption)
        {
            string selectedRaioOption = "";
            IList<IWebElement> radioCollections = driver.FindElements(By.XPath("//cft-field-editor-keyword-search-my-search//label"));

            if (radioOption.Equals("Random"))
            {
                Random rand = new Random();
                int x = rand.Next(0, radioCollections.Count);
                IList<IWebElement> cells = radioCollections[x]._findElementsWithinElement("xpath", ".//span");
                selectedRaioOption = radioCollections[x].Text;
                cells[0].Click();
                Thread.Sleep(3000);
            }
            else
            {
                for (int i = 0; i < radioCollections.Count; i++)
                {
                    if (radioCollections[i].Text.Contains(radioOption))
                    {
                        selectedRaioOption = radioOption;
                        IList<IWebElement> cells = radioCollections[i]._findElementsWithinElement("xpath", ".//span");
                        cells[0].Click();
                        Thread.Sleep(3000);
                        break;
                    }
                }
            }

            Results.WriteStatus(test, "Pass", "Selected, '" + selectedRaioOption + "' Radio Option from keyword section.");
            return selectedRaioOption;
        }

        /// <summary>
        /// Verify Company Division Brand Section on search screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyCompanyDivisionBrandSectionOnSearchScreen()
        {
            Assert.AreEqual(true, driver._waitForElement("xpath", "//cft-field-editor-multi-tree//h6[text()='Company Division Brand']"), "'Company Division Brand' Section header not present.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Companies Divisions Brands...']"), "'Company Division Brand' filter text field not present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Select Displayed ']"), "'Select Displayed' button not present on Company Division Brand filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Exclude ']"), "'Exclude' button not present on Company Division Brand filter section.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']"), "'Filter Options' not present on Company Division Brand filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-viewport']"), "'Selected Filter Options' box not present on Company Division Brand filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[1]/div/cft-field-editor-multi-items-footer//button"), "'Load More' button not present on Company Division Brand filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[2]/div/cft-field-editor-multi-items-footer//button"), "'Clear Selected' button not present on Company Division Brand filter section.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div"), "'Company Division Brand' Filter Options not present.");
            IList<IWebElement> optionCollection = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div");

            for(int i = 0; i < 10; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", optionCollection[i]);
                Thread.Sleep(500);
            }
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", optionCollection[0]);

            Results.WriteStatus(test, "Pass", "Verified, Company Division Brand Section on search screen");
            return new Search(driver, test);
        }

        ///<summary>
        ///Click And Verify Load More Button On Multi Tree Search Options
        ///</summary>
        ///<returns></returns>
        public Search clickAndVerifyLoadMoreButtonOnMultiTreeSearchOptions()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div"), "'Company Division Brand' Filter Options not present.");
            int currRow = 1;

            while (driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div[" + currRow + "]"))
            {
                driver._scrollintoViewElement("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div[" + currRow + "]");
                ++currRow;
            }

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[1]/div/cft-field-editor-multi-items-footer//button"), "'Load More' button not present on Company Division Brand filter section.");
            driver._click("xpath", "//div[1]/div/cft-field-editor-multi-items-footer//button");
            Thread.Sleep(1000);
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div[" + currRow + "]"), "'Load More' Buttin did not load more filter options.");

            Results.WriteStatus(test, "Pass", "Clicked And Verified, Load More Button On Multi Tree Search Options.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Select Company Division Brand Option on search screen
        /// </summary>
        /// <returns></returns>
        public string selectCompanyDivisionBrandOptionOnSearchScreen(string option, int noOfClicks = 1)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Companies Divisions Brands...']"), "'Category Class' filter text field not present.");
            if (!option.Equals("Random"))
            {
                driver._type("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Companies Divisions Brands...']", option);
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Clear ']"), "'Clear' button not present on Company Division Brand filter section.");
            }
            Thread.Sleep(500);

            if (driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]"))
            {
                IList<IWebElement> optionCollection = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]");
                if (option.Equals("Random"))
                {
                    Random rand = new Random();
                    for (int i = 0; i < 6; i++)
                    {
                        int x = rand.Next(0, optionCollection.Count);
                        option = optionCollection[x].Text;
                        for (int j = 0; j < noOfClicks; j++)
                        {
                            driver._click("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//div[@role='row'][" + (x + 1) + "]//span[@style]");
                            Thread.Sleep(500);
                        }
                    }
                    Results.WriteStatus(test, "Pass", "Selected, '" + option + "' Option from Section.");
                }
                else
                {
                    bool avail = false;
                    for (int i = 0; i < optionCollection.Count; i++)
                    {
                        if (optionCollection[i].Text.ToLower().Contains(option.ToLower()))
                        {
                            for (int j = 0; j < noOfClicks; j++)
                            {
                                driver._click("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//div[@role='row'][" + (i + 1) + "]//span[@style]");
                                Thread.Sleep(500);
                            }
                            avail = true; Thread.Sleep(2000);
                            break;
                        }
                    }
                    Assert.AreEqual(true, avail, "'" + option + "' Option not Present.");
                }

                Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-viewport']"), "'Selected Filter Options' box not present on Company Division Brand filter section.");
                if(driver._isElementPresent("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index]"))
                    Assert.AreEqual(null, driver._getAttributeValue("xpath", "//div[2]/div/cft-field-editor-multi-items-footer//button", "disabled"), "'Clear Selected' button is disabled on Company Division Brand filter section.");

                Results.WriteStatus(test, "Pass", "Selected, '" + option + "' option from Company Division Brand Section on search screen");
                return option;
            }
            else
            {
                Results.WriteStatus(test, "Info", "Company Division Brand Section on search screen is empty");
                return "";
            }
        }

        ///<summary>
        ///Click And Verify Buttons On Filter Bars in Search Options
        ///</summary>
        ///<returns></returns>
        public Search clickAndVerifyButtonsOnFilterBarsSearchOptions(string button)
        {
            if (button.ToLower().Equals("clear"))
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Clear ']"), "'Clear' button not present on Company Division Brand filter section.");
                driver._click("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Clear ']");
                Thread.Sleep(500);
                Assert.AreEqual("", driver._getValue("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Companies Divisions Brands...']"));
            }
            else if(button.ToLower().Equals("select displayed"))
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Select Displayed ']"), "'Select Displayed' button not present on Company Division Brand filter section.");
                driver._click("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Select Displayed ']");
                Thread.Sleep(500);

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]"), "Filter Options not present.");
                IList<IWebElement> filterOptionCollection = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-container']//span[@style]"), "Selected Options not present.");
                IList<IWebElement> selectedOptionCollection = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-container']//span[@style]");

                Assert.AreEqual(filterOptionCollection.Count, selectedOptionCollection.Count, "All Displayed Options did not get selected.");
                Assert.AreEqual(null, driver._getAttributeValue("xpath", "//div[2]/div/cft-field-editor-multi-items-footer//button", "disabled"), "'Clear Selected' button is disabled on Company Division Brand filter section.");
            }
            else if (button.ToLower().Equals("exclude"))
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Exclude ']"), "'Exclude' button not present on Company Division Brand filter section.");
                driver._click("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Exclude ']");
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Excluding ']"), "'Excluding' button not present on Company Division Brand filter section.");
            }

            Results.WriteStatus(test, "Pass", "Clicked And Verified, '" + button + "' Button On Filter Bars in Search Options.");
            return new Search(driver, test);
        }

        ///<summary>
        ///Click And Verify Clear Selected Button On Multi Tree Search Options
        ///</summary>
        ///<returns></returns>
        public Search clickAndVerifyClearSelectedButtonOnMultiTreeSearchOptions()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[2]/div/cft-field-editor-multi-items-footer//button"), "'Clear Selected' button not present on Company Division Brand filter section.");
            Assert.AreEqual(null, driver._getAttributeValue("xpath", "//div[2]/div/cft-field-editor-multi-items-footer//button", "disabled"), "'Clear Selected' button is disabled on Company Division Brand filter section.");
            driver._click("xpath", "//div[2]/div/cft-field-editor-multi-items-footer//button");
            Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-container']/div"), "'Company Division Brand' Selected Filter Options still present.");
            Assert.AreNotEqual(null, driver._getAttributeValue("xpath", "//div[2]/div/cft-field-editor-multi-items-footer//button", "disabled"), "'Clear Selected' button is disabled on Company Division Brand filter section.");

            Results.WriteStatus(test, "Pass", "Clicked And Verified, Clear Selected Button On Multi Tree Search Options.");
            return new Search(driver, test);
        }

        /// <summary>
        /// Verify Category Class Section on search screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyCategoryClassSectionOnSearchScreen()
        {
            Assert.AreEqual(true, driver._waitForElement("xpath", "//cft-field-editor-multi-tree//h6[text()='Category Class']"), "'Category Class' Section header not present.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Categories Classes...']"), "'Category Class' filter text field not present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Select Displayed ']"), "'Select Displayed' button not present on Category Class filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Exclude ']"), "'Exclude' button not present on Category Class filter section.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]"), "'Filter Options' not present on Category Class filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-viewport']"), "'Selected Filter Options' box not present on Category Class filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[1]/div/cft-field-editor-multi-items-footer//button"), "'Load More' button not present on Category Class filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[2]/div/cft-field-editor-multi-items-footer//button"), "'Clear Selected' button not present on Category Class filter section.");

            Results.WriteStatus(test, "Pass", "Verified, Category Class Section on search screen");
            return new Search(driver, test);
        }

        /// <summary>
        /// Select Category Class Option on search screen
        /// </summary>
        /// <returns></returns>
        public string selectCategoryClassOptionOnSearchScreen(string option, string subOption = "")
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Categories Classes...']"), "'Category Class' filter text field not present.");
            if (subOption != "" && !subOption.Equals("Random"))
                driver._type("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Categories Classes...']", subOption);
            else if (!option.Equals("Random"))
                driver._type("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Categories Classes...']", option);
            Thread.Sleep(500);

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]"), "'Filter Options' not present on Category Class filter section.");
            IList<IWebElement> optionCollection = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']");
            bool avail = false;
            if (option.Equals("Random"))
            {
                Random rand = new Random();
                int x = rand.Next(0, optionCollection.Count);
                driver._scrollintoViewElement("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div[" + (x + 1) + "]//span[@style]");
                option = driver._getText("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div[" + (x + 1) + "]//span[text()]");
                driver._click("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div[" + (x + 1) + "]//span[text()]");
                Thread.Sleep(500);
                if (option.IndexOf('(') > -1)
                    option = option.Substring(0, option.IndexOf('(') - 1);
                Results.WriteStatus(test, "Pass", "Selected, '" + option + "' Option from Section.");
            }
            else
            {
                for (int i = 0; i < optionCollection.Count; i++)
                {
                    if (optionCollection[i].Text.Contains(option))
                    {
                        avail = true;
                        if (subOption == "")
                        {
                            optionCollection[i].Click();
                            break;
                        }
                        else
                        {
                            if (driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div[" + (i + 1) + "]//span[@ref='eContracted"))
                                driver._click("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div[" + (i + 1) + "]//span[@ref='eContracted");
                            Thread.Sleep(2000);
                            optionCollection = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]");
                            if (subOption.Equals("Random"))
                            {
                                Random rand = new Random();
                                int x = rand.Next(3, optionCollection.Count);
                                subOption = optionCollection[x].Text;
                                optionCollection[x].Click();
                                Thread.Sleep(500);
                                Results.WriteStatus(test, "Pass", "Selected, '" + option + "' Option from Section.");
                            }
                            else
                            {
                                bool avail2 = false;
                                for (int j = 0; j < optionCollection.Count; j++)
                                    if (optionCollection[j].Text == subOption)
                                    {
                                        avail2 = true;
                                        optionCollection[j].Click();
                                    }
                                Assert.AreEqual(true, avail2, "'" + subOption + "' Option not Present.");

                                Thread.Sleep(2000);
                                break;
                            }
                        }
                    }
                }
                Assert.AreEqual(true, avail, "'" + option + "' Option not Present.");
            }

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-viewport']"), "'Selected Filter Options' box not present on Category Class filter section.");
            IList<IWebElement> selectedOptionColl = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-container']//span[@ref='eValue']//*[text()]");
            avail = false;
            for (int j = 0; j < selectedOptionColl.Count; j++)
                if (selectedOptionColl[j].Text.Contains(option))
                {
                    avail = true;
                    break;
                }
            Assert.AreEqual(true, avail, "'" + option + "' Option not Present in Selected Options box.");
            avail = false;
            for (int j = 0; j < selectedOptionColl.Count; j++)
                if (selectedOptionColl[j].Text.Contains(subOption))
                {
                    avail = true;
                    break;
                }
            Assert.AreEqual(true, avail, "'" + subOption + "' Option not Present in Selected Options box.");

            Results.WriteStatus(test, "Pass", "Selected, '" + option + ", " + subOption + "' option from Category Class Section on search screen");
            return option;
        }

        /// <summary>
        /// Verify New Language Section on screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyNewLanguageSectionOnScreen()
        {
            Assert.AreEqual(true, driver._waitForElement("xpath", "//cft-field-editor-multi-list//h6"), "Language Section not present.");
            Assert.AreEqual("Language".ToLower(), driver._getText("xpath", "//cft-field-editor-multi-list//h6").ToLower(), "Language Header not match.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-list//label"), "Language Filter List not present.");

            Results.WriteStatus(test, "Pass", "Verified, Language section on search screen.");
            return new Search(driver, test);
        }

        ///<summary>
        ///Select Language Media Type on Search Options
        ///</summary>
        ///<param name="language">Media Type to be selected</param>
        ///<returns></returns>
        public string selectSpecificLanguageOnSearchOptions(string language, bool clearprevious = true)
        {
            IList<IWebElement> languageCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
            bool avail = false;
            if (clearprevious)
            {
                foreach (IWebElement media in languageCollection)
                    if (media.GetCssValue("background") == "#e0e9ea")
                        media.Click();
                avail = true;

                languageCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
                foreach (IWebElement media in languageCollection)
                    if (media.GetCssValue("background") == "#e0e9ea")
                    {
                        avail = false;
                        Assert.AreEqual(null, media.GetCssValue("background"), "'" + media.Text + "' did not get deselected.");
                        break;
                    }
                Assert.IsTrue(avail, "Language Filter List did not get cleared");
            }
            avail = false;
            foreach (IWebElement media in languageCollection)
                if (media.Text.ToLower().Contains(language.ToLower()))
                {
                    avail = true;
                    media.Click();
                    Thread.Sleep(1000);
                    break;
                }
            Assert.IsTrue(avail, "'" + language + "' Language not found");

            languageCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
            foreach (IWebElement media in languageCollection)
                if (media.Text.ToLower().Contains(language.ToLower()))
                    Assert.AreNotEqual("#e0e9ea", media.GetCssValue("background"), "'" + language + "' did not get selected.");

            Results.WriteStatus(test, "Pass", "Selected, '" + language + "' in Language on search screen.");
            return language;
        }

        /// <summary>
        /// Verify New Coop Advertisers Section on screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyNewCoopAdvertisersSectionOnScreen()
        {
            Assert.AreEqual(true, driver._waitForElement("xpath", "//cft-field-editor-multi-list//h6"), "Coop Advertisers Section not present.");
            Assert.AreEqual("Coop Advertisers".ToLower(), driver._getText("xpath", "//cft-field-editor-multi-list//h6").ToLower(), "Coop Advertisers Header not match.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-multi-list//input[@type='text']"), "Text Field in Coop Advertisers Filter section not present");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-list//label"), "Coop Advertisers Filter List not present.");

            Results.WriteStatus(test, "Pass", "Verified, Coop Advertisers section on search screen.");
            return new Search(driver, test);
        }

        ///<summary>
        ///Select Coop Advertisers Media Type on Search Options
        ///</summary>
        ///<param name="Coop Advertisers">Media Type to be selected</param>
        ///<returns></returns>
        public string selectSpecificCoopAdvertiserOnSearchOptions(string coopAdvertisers, bool clearprevious = true)
        {
            IList<IWebElement> coopAdvertisersCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
            bool avail = false;
            if (clearprevious)
            {
                foreach (IWebElement media in coopAdvertisersCollection)
                    if (media.GetCssValue("background") == "#e0e9ea")
                        media.Click();
                avail = true;

                coopAdvertisersCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
                foreach (IWebElement media in coopAdvertisersCollection)
                    if (media.GetCssValue("background") == "#e0e9ea")
                    {
                        avail = false;
                        Assert.AreEqual(null, media.GetCssValue("background"), "'" + media.Text + "' did not get deselected.");
                        break;
                    }
                Assert.IsTrue(avail, "Coop Advertisers Filter List did not get cleared");
            }

            driver._type("xpath", "//cft-field-editor-multi-list//input[@type='text']", coopAdvertisers);
            Thread.Sleep(1000);
            coopAdvertisersCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
            avail = false;
            foreach (IWebElement media in coopAdvertisersCollection)
                if (media.Text.ToLower().Contains(coopAdvertisers.ToLower()))
                {
                    avail = true;
                    media.Click();
                    Thread.Sleep(1000);
                    break;
                }
            Assert.IsTrue(avail, "'" + coopAdvertisers + "' Coop Advertisers not found");

            coopAdvertisersCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
            foreach (IWebElement media in coopAdvertisersCollection)
                if (media.Text.ToLower().Contains(coopAdvertisers.ToLower()))
                    Assert.AreNotEqual("#e0e9ea", media.GetCssValue("background"), "'" + coopAdvertisers + "' did not get selected.");

            Results.WriteStatus(test, "Pass", "Selected, '" + coopAdvertisers + "' in Coop Advertisers on search screen.");
            return coopAdvertisers;
        }

        /// <summary>
        /// Verify New Media Outlet Section on screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyNewMediaOutletSectionOnScreen()
        {
            Assert.AreEqual(true, driver._waitForElement("xpath", "//cft-field-editor-multi-list//h6"), "Media Outlet Section not present.");
            Assert.AreEqual("Media Outlet".ToLower(), driver._getText("xpath", "//cft-field-editor-multi-list//h6").ToLower(), "Media Outlet Header not match.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-multi-list//input[@type='text']"), "Text Field in Media Outlet Filter section not present");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-list//label"), "Media Outlet Filter List not present.");

            Results.WriteStatus(test, "Pass", "Verified, Media Outlet section on search screen.");
            return new Search(driver, test);
        }

        ///<summary>
        ///Select Media Outlet Media Type on Search Options
        ///</summary>
        ///<param name="mediaOutlet">Media Type to be selected</param>
        ///<returns></returns>
        public string selectSpecificMediaOutletOnSearchOptions(string mediaOutlet, bool clearprevious = true)
        {
            IList<IWebElement> mediaOutletCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
            bool avail = false;
            if (clearprevious)
            {
                foreach (IWebElement media in mediaOutletCollection)
                    if (media.GetCssValue("background") == "#e0e9ea")
                        media.Click();
                avail = true;

                mediaOutletCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
                foreach (IWebElement media in mediaOutletCollection)
                    if (media.GetCssValue("background") == "#e0e9ea")
                    {
                        avail = false;
                        Assert.AreEqual(null, media.GetCssValue("background"), "'" + media.Text + "' did not get deselected.");
                        break;
                    }
                Assert.IsTrue(avail, "Media Outlet Filter List did not get cleared");
            }

            driver._type("xpath", "//cft-field-editor-multi-list//input[@type='text']", mediaOutlet);
            Thread.Sleep(1000);
            mediaOutletCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
            avail = false;
            foreach (IWebElement media in mediaOutletCollection)
                if (media.Text.ToLower().Contains(mediaOutlet.ToLower()))
                {
                    avail = true;
                    media.Click();
                    Thread.Sleep(1000);
                    break;
                }
            Assert.IsTrue(avail, "'" + mediaOutlet + "' Media Outlet not found");

            mediaOutletCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
            foreach (IWebElement media in mediaOutletCollection)
                if (media.Text.ToLower().Contains(mediaOutlet.ToLower()))
                    Assert.AreNotEqual("#e0e9ea", media.GetCssValue("background"), "'" + mediaOutlet + "' did not get selected.");

            Results.WriteStatus(test, "Pass", "Selected, '" + mediaOutlet + "' in Media Outlet on search screen.");
            return mediaOutlet;
        }

        /// <summary>
        /// Verify Category Class Section on search screen
        /// </summary>
        /// <returns></returns>
        public Search VerifyMarketSectionOnSearchScreen()
        {
            Assert.AreEqual(true, driver._waitForElement("xpath", "//cft-field-editor-multi-tree//h6[text()='Market']"), "'Market' Section header not present.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Markets...']"), "'Market' filter text field not present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Select Displayed ']"), "'Select Displayed' button not present on Market filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//button[text()=' Exclude ']"), "'Exclude' button not present on Market filter section.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]"), "'Filter Options' not present on Market filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-viewport']"), "'Selected Filter Options' box not present on Market filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[1]/div/cft-field-editor-multi-items-footer//button"), "'Load More' button not present on Market filter section.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[2]/div/cft-field-editor-multi-items-footer//button"), "'Clear Selected' button not present on Market filter section.");

            Results.WriteStatus(test, "Pass", "Verified, Market Section on search screen");
            return new Search(driver, test);
        }

        /// <summary>
        /// Select Market Option on search screen
        /// </summary>
        /// <returns></returns>
        public string selectMarketOptionOnSearchScreen(string option, string subOption = "")
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Markets...']"), "Market' filter text field not present.");
            if (subOption != "" && !subOption.Equals("Random"))
                driver._type("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Markets...']", subOption);
            else if (!option.Equals("Random"))
                driver._type("xpath", "//cft-field-editor-multi-items-filter//input[@placeholder='Filter Markets...']", option);

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]"), "'Filter Options' not present on Market filter section.");
            IList<IWebElement> optionCollection = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]");
            bool avail = false;
            if (option.Equals("Random"))
            {
                Random rand = new Random();
                int x = rand.Next(0, 4);
                option = optionCollection[x].Text;
                optionCollection[x].Click();
                Thread.Sleep(500);
                if (option.IndexOf('(') > -1)
                    option = option.Substring(0, option.IndexOf('(') - 1);
                Results.WriteStatus(test, "Pass", "Selected, '" + option + "' Option from Section.");
            }
            else
            {
                for (int i = 0; i < optionCollection.Count; i++)
                {
                    if (optionCollection[i].Text.Contains(option))
                    {
                        avail = true;
                        if (subOption == "")
                        {
                            optionCollection[i].Click();
                            break;
                        }
                        else
                        {
                            if (driver._isElementPresent("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div[" + (i + 1) + "]//span[@ref='eContracted"))
                                driver._click("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']/div[" + (i + 1) + "]//span[@ref='eContracted");
                            Thread.Sleep(2000);
                            optionCollection = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[1]/cft-table-tree//div[@class='ag-body-container']//span[@style]");
                            if (subOption.Equals("Random"))
                            {
                                Random rand = new Random();
                                int x = rand.Next(0, optionCollection.Count);
                                subOption = optionCollection[x].Text;
                                optionCollection[x].Click();
                                Thread.Sleep(500);
                                Results.WriteStatus(test, "Pass", "Selected, '" + option + "' Option from Section.");
                            }
                            else
                            {
                                bool avail2 = false;
                                for (int j = 0; j < optionCollection.Count; j++)
                                    if (optionCollection[j].Text == subOption)
                                    {
                                        avail2 = true;
                                        optionCollection[j].Click();
                                    }
                                Assert.AreEqual(true, avail2, "'" + subOption + "' Option not Present.");

                                Thread.Sleep(2000);
                                break;
                            }
                        }
                    }
                }
                Assert.AreEqual(true, avail, "'" + option + "' Option not Present.");
            }
            Assert.AreEqual(true, driver._waitForElement("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-container']//span[@style]"), "'Selected Filter Options' box not present on Market filter section.");
            IList<IWebElement> selectedOptionColl = driver._findElements("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-container']//*[@style]");
            if (subOption == "")
            {
                avail = false;
                for (int j = 0; j < selectedOptionColl.Count; j++)
                    if (selectedOptionColl[j].Text.Contains(option))
                    {
                        avail = true;
                        break;
                    }
                Assert.AreEqual(true, avail, "'" + option + "' Option not Present in Selected Options box.");
            }
            else
            {
                avail = false;
                for (int j = 0; j < selectedOptionColl.Count; j++)
                    if (selectedOptionColl[j].Text == subOption)
                    {
                        avail = true;
                        break;
                    }
                Assert.AreEqual(true, avail, "'" + subOption + "' Option not Present in Selected Options box.");
            }

            Results.WriteStatus(test, "Pass", "Selected, '" + option + ", " + subOption + "' option from Market Section on search screen");
            return option;
        }

        /// <summary>
        /// Verify New Media Type Section on screen in CFT Development
        /// </summary>
        /// <returns></returns>
        public Search VerifyNewMediaTypeSectionOnScreeninCFTDevelopment()
        {
            Assert.AreEqual(true, driver._waitForElement("xpath", "//cft-field-editor-multi-list//h6"), "Media Section not present.");
            Assert.AreEqual("Media".ToLower(), driver._getText("xpath", "//cft-field-editor-multi-list//h6").ToLower(), "Media Header not match.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-field-editor-multi-list//label"), "Media Outlet Filter List not present.");

            Results.WriteStatus(test, "Pass", "Verified, Media Outlet section on search screen.");
            return new Search(driver, test);
        }

        ///<summary>
        ///Select Media Type on Search Options In CFT Development
        ///</summary>
        ///<param name="mediaOutlet">Media Type to be selected</param>
        ///<returns></returns>
        public string selectSpecificMediaTypeOnSearchOptionsInCFTDevelopment(string mediaType, bool clearprevious = true)
        {
            IList<IWebElement> mediaCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
            bool avail = false;
            if (clearprevious)
            {
                foreach (IWebElement media in mediaCollection)
                    if (media.GetCssValue("background").Contains("224, 233, 234"))
                        media.Click();
                avail = true;

                mediaCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
                foreach (IWebElement media in mediaCollection)
                    if (media.GetCssValue("background").Contains("224, 233, 234"))
                    {
                        avail = false;
                        Assert.AreEqual(null, media.GetCssValue("background"), "'" + media.Text + "' did not get deselected.");
                        break;
                    }
                Assert.IsTrue(avail, "Media Outlet Filter List did not get cleared");
            }

            Thread.Sleep(1000);
            mediaCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
            avail = false;
            foreach (IWebElement media in mediaCollection)
                if (media.Text.ToLower().Contains(mediaType.ToLower()))
                {
                    avail = true;
                    media.Click();
                    Thread.Sleep(1000);
                    break;
                }
            Assert.IsTrue(avail, "'" + mediaType + "' Media Outlet not found");

            mediaCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
            foreach (IWebElement media in mediaCollection)
                if (media.Text.ToLower().Contains(mediaType.ToLower()))
                {
                    Assert.IsTrue(media.GetCssValue("background").Contains("224, 233, 234"), "'" + mediaType + "' did not get selected.");
                    break;
                }

            Results.WriteStatus(test, "Pass", "Selected, '" + mediaType + "' in Media Outlet on search screen.");
            return mediaType;
        }

        ///<summary>
        ///Read Selected Options On Search Options
        ///</summary>
        ///<returns></returns>
        public string readSelectedOptionsOnSearchOptions()
        {
            if(driver._waitForElement("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index]"))
            {

                IList<IWebElement> selectedOptionsColl = driver._findElements("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index]");
                string selection = "";
                int i = 0;
                while(i < selectedOptionsColl.Count)
                {
                    driver._scrollintoViewElement("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index][" + (i + 1) + "]");
                    bool expanded = driver._isElementPresent("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index][" + (i + 1) + "]//span[@class='ag-group-expanded']");
                    bool contracted = driver._isElementPresent("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index][" + (i + 1) + "]//span[@class='ag-group-contracted']");
                    Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index][" + (i + 1) + "]//*[text()]"), "Text not present on selected option.");
                    string optionText = driver._getText("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index][" + (i + 1) + "]//*[text()]");
                    if (expanded || contracted)
                    {
                        int selectedSubOption = 0, totalSubOption = 0;
                        int index1 = optionText.IndexOf('(');
                        int index2 = optionText.IndexOf(')');
                        int index3 = optionText.IndexOf('/');
                        Console.WriteLine(optionText.Substring(index1 + 1, index3 - index1 - 1));
                        Console.WriteLine(optionText.Substring(index3 + 1, index2 - index3 - 1));
                        Assert.IsTrue(Int32.TryParse(optionText.Substring(index1 + 1, index3 - index1 - 1), out selectedSubOption), "Selected Sub Option Count could not be converted to int");
                        Assert.IsTrue(Int32.TryParse(optionText.Substring(index3 + 1, index2 - index3 - 1), out totalSubOption), "Total Sub Option Count could not be converted to int");
                        selection = selection + "*" + optionText.Substring(0, index1 - 1);

                        if (expanded)
                        {
                            if (selectedSubOption == totalSubOption)
                            {
                                i = i + selectedSubOption + 1;
                                Results.WriteStatus(test, "Pass", "All of '" + totalSubOption + "' options are selected. Hence '" + optionText + "' is fully selected.");
                            }
                            else
                            {
                                ++i;
                                Results.WriteStatus(test, "Pass", "'" + selectedSubOption + "' out of '" + totalSubOption + "' options are selected. Hence '" + optionText + "' is partially selected.");
                            }
                        }
                        else
                        {
                            if (selectedSubOption != totalSubOption)
                            {
                                driver._click("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index][" + (i + 1) + "]//span[@class='ag-group-contracted']");
                                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index][" + (i + 1) + "]//span[@class='ag-group-expanded']"), "Selected Option's suboptions did not get expanded.");
                                selectedOptionsColl = driver._findElements("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index]");
                                Results.WriteStatus(test, "Pass", "'" + selectedSubOption + "' out of '" + totalSubOption + "' options are selected. Hence '" + optionText + "' is partially selected.");
                            }
                            else
                            {
                                ++i;
                                Results.WriteStatus(test, "Pass", "All of '" + totalSubOption + "' options are selected. Hence '" + optionText + "' is fully selected.");
                            }
                        }
                    }
                    else
                    {
                        selection = selection + "*" + optionText;
                        ++i;
                    }
                }
                Console.WriteLine(selection);

                Results.WriteStatus(test, "Pass", "Read Selected Options On Search Options");
                return selection;
            }
            else
            {
                Results.WriteStatus(test, "Info", "Selected Options Section is blank for this criterion On Search Options");
                return "";
            }
        }

        ///<summary>
        ///Deselect From Selected Options in Multi Tree
        ///</summary>
        ///<returns></returns>
        public Search DeselectFromSelectedOptionsInMultiTree(string option = "Random")
        {
            if (driver._waitForElement("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index]"))
            {
                IList<IWebElement> selectedOptionsColl = driver._findElements("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index]");
                if (option.Equals("Random"))
                {
                    Random rand = new Random();
                    for (int i = 0; i < 6; i++)
                    {
                        int x = rand.Next(0, selectedOptionsColl.Count);
                        option = selectedOptionsColl[x].Text;
                        driver._click("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-container']//div[@role='row'][" + (x + 1) + "]//span[@style]");
                        Thread.Sleep(500);
                    }
                    Results.WriteStatus(test, "Pass", "Selected, '" + option + "' Option from Section.");
                }
                else
                {
                    bool avail = false;
                    for (int i = 0; i < selectedOptionsColl.Count; i++)
                    {
                        if (selectedOptionsColl[i].Text.ToLower().Contains(option.ToLower()))
                        {
                            driver._click("xpath", "//cft-field-editor-multi-tree//div[2]/cft-table-tree//div[@class='ag-body-container']//div[@role='row'][" + (i + 1) + "]//span[@style]");
                            avail = true;
                            Thread.Sleep(2000);
                            break;
                        }
                    }
                    Assert.AreEqual(true, avail, "'" + option + "' Option not Present.");
                }

                selectedOptionsColl = driver._findElements("xpath", "//div[@class='row']/div[2]//ag-grid-angular//div[@class='ag-body-container']//div[@row-index]");

                if (selectedOptionsColl.Count > 0)
                {
                    bool avail = false;
                    for (int i = 0; i < selectedOptionsColl.Count; i++)
                    {
                        if (selectedOptionsColl[i].Text.ToLower().Contains(option.ToLower()))
                        {
                            avail = true;
                            break;
                        }
                    }
                    Assert.AreEqual(false, avail, "'" + option + "' Option is Present.");
                }
            }

            Results.WriteStatus(test, "Pass", "Deselected, '" + option + "' From Selected Options in Multi Tree");
            return new Search(driver, test);
        }

        #region Saved Search List Methids

        ///<summary>
        ///To Verify Load More Button On Saved Search DDL
        ///</summary>
        ///<returns></returns>
        public Search VerifyLoadMoreButtonOnSavedSearchDDL(string accountName = "CFT Developer", bool VerifyEnable = true)
        {
            int noOfSavedSearch = 0;
            while (true)
            {
                if (!driver._waitForElement("xpath", "//div[@class='flex-grow-1']//a"))
                    driver._click("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]");
                IList<IWebElement> savedSearchDDLColl = driver._findElements("xpath", "//div[@class='flex-grow-1']//a");

                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='flex-grow-1']//div//button[text()=' Load More ']"), "'Load More' button not present in Saved Search DDL.");

                if (noOfSavedSearch == 0)
                    noOfSavedSearch = savedSearchDDLColl.Count - 1;

                if (noOfSavedSearch < 10)
                {
                    Assert.LessOrEqual(savedSearchDDLColl.Count - 1, 10, "No. of Visible Saved Searches is More than 10");
                    Assert.AreNotEqual(null, driver._getAttributeValue("xpath", "//div[@class='flex-grow-1']//div//button[text()=' Load More ']", "disabled"), "'Load More' button is not disabled for less than 10 Saved Searches in Saved Search DDL.");
                    if (VerifyEnable)
                    {
                        savedSearchDDLColl[0].Click();
                        driver._waitForElementToBeHidden("xpath", "//div[@class='flex-grow-1']//a");
                        Assert.IsTrue(driver._getText("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]").ToLower().Contains("untitled"), "New Search not loaded");
                        homePage.VerifyAndModifySearchOptions(true, false, accountName);
                        homePage.saveNewSearch(false, true, accountName);
                        ++noOfSavedSearch;
                    }
                    else
                        break;
                }
                else
                {
                    Assert.AreEqual(null, driver._getAttributeValue("xpath", "//div[@class='flex-grow-1']//div//button[text()=' Load More ']", "disabled"), "'Load More' button is disabled for more than 10 Saved Searches in Saved Search DDL.");
                    break;
                }

            }

            Results.WriteStatus(test, "Pass", "Verified, Load More Button On Saved Search DDL");
            return new Search(driver, test);
        }

        ///<summary>
        ///Verify And Load Specific Saved Search
        ///</summary>
        ///<returns></returns>
        public Search VerifyAndLoadSpecificSavedSearch(string searchName, bool present, bool load = false)
        {
            driver._click("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='flex-grow-1']//a"), "Select Saved Search DDL not present");
            IList<IWebElement> savedSearchDDLColl = driver._findElements("xpath", "//div[@class='flex-grow-1']//a");

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='flex-grow-1']//a"), "Creatives count not present on Select Saved Search DDL");
            IList<IWebElement> creativesCountColl = driver._findElements("xpath", "//div[@class='flex-grow-1']//a/span");
            Assert.AreEqual(savedSearchDDLColl.Count, creativesCountColl.Count, "Creatives Count not present for all saved searches");

            if(!summaryTags.VerifySummaryTags(new string[] { "Search Options" }, true, true))
                Assert.IsTrue(creativesCountColl[0].GetAttribute("class").Contains("NU-icon-loading"), "'Loading' icon not present with 'New Search' Option in Saved Search DDL when a Saved Search is Applied.");
            else
                Assert.IsTrue(creativesCountColl[0].GetAttribute("class").Contains("badge-numerator-sapphire"), "'Creatives Count' icon not present with 'New Search' Option in Saved Search DDL when a Saved Search is not Applied.");

            bool avail = false;
            foreach (IWebElement savedSearch in savedSearchDDLColl)
                if (savedSearch.Text.ToLower().Contains(searchName.ToLower()))
                {
                    avail = true;
                    if (load)
                        savedSearch.Click();
                    break;
                }
            if (present)
                Assert.IsTrue(avail, "'" + searchName + "' search not present in Saved Searches DDL");
            else
                Assert.IsFalse(avail, "'" + searchName + "' search still present in Saved Searches DDL");

            return new Search(driver, test);
        }

        ///<summary>
        ///Verify That Saved Search Names Are Listed In Ascending Order
        ///</summary>
        ///<returns></returns>
        public string[] VerifyThatSavedSearchNamesAreListedInAscendingOrder()
        {
            driver._click("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='flex-grow-1']//a"), "Select Saved Search DDL not present");
            IList<IWebElement> savedSearchDDLColl = driver._findElements("xpath", "//div[@class='flex-grow-1']//a");
            string[] searchNames = new string[savedSearchDDLColl.Count];

            for (int i = 0; i < savedSearchDDLColl.Count; i++)
                searchNames[i] = savedSearchDDLColl[i].Text;

            string[] sortedSearchNames = new string[searchNames.Length];
            Array.Copy(searchNames, sortedSearchNames, searchNames.Length);

            Array.Sort(sortedSearchNames);

            fieldOptions.compareListOfItemsInOrder(searchNames, sortedSearchNames);

            Results.WriteStatus(test, "Pass", "Verified, Saved Search Names Are Listed In Ascending Order");
            return searchNames;
        }

        ///<summary>
        ///Verify That Load More Buttons Loads More Searches
        ///</summary>
        ///<returns></returns>
        public Search VerifyThatLoadMoreButtonLoadsMoreSearches(string accountName = "QA Testing - Brand")
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='flex-grow-1']//a"), "Select Saved Search DDL not present");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='flex-grow-1']//div//button[text()=' Load More ']"), "'Load More' button not present in Saved Search DDL.");

            if (!driver._waitForElement("xpath", "//div[@class='flex-grow-1']//a"))
                driver._click("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]");

            IList<IWebElement> savedSearchDDLColl = driver._findElements("xpath", "//div[@class='flex-grow-1']//a");
            string[] searchNames = new string[savedSearchDDLColl.Count];

            for (int i = 0; i < savedSearchDDLColl.Count; i++)
                searchNames[i] = savedSearchDDLColl[i].Text;

            driver._click("xpath", "//div[@class='flex-grow-1']//div//button[text()=' Load More ']");
            Thread.Sleep(2000);

            savedSearchDDLColl = driver._findElements("xpath", "//div[@class='flex-grow-1']//a");
            string[] newSearchNames = new string[savedSearchDDLColl.Count];

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='flex-grow-1']//a"), "Creatives count not present on Select Saved Search DDL");
            IList<IWebElement> creativesCountColl = driver._findElements("xpath", "//div[@class='flex-grow-1']//a/span");
            Assert.AreEqual(savedSearchDDLColl.Count, creativesCountColl.Count, "Creatives Count not present for all saved searches");

            for (int i = 0; i < savedSearchDDLColl.Count; i++)
                newSearchNames[i] = savedSearchDDLColl[i].Text;

            Assert.IsFalse(searchNames.SequenceEqual(newSearchNames), "More Saved Searches were not loaded on clicking 'Load More' button.");

            Results.WriteStatus(test, "Pass", "Verified, Load More Buttons Loads More Searches");
            return new Search(driver, test);
        }

        ///<summary>
        ///Randomly Select Search Filter From Open Search Tab
        ///</summary>
        ///<returns></returns>
        public string randomlySelectSearchFilterFromOpenSearchTab()
        {
            string xpathPrefix = "//div[contains(@class, 'modal-body')]", xpathPart = "";

            Assert.IsTrue(driver._waitForElement("xpath", xpathPrefix), "'Search Options' popup not present");

            if (driver._waitForElement("xpath", xpathPrefix + "//label"))
            {
                xpathPart = "//label";
                IList<IWebElement> optionCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
                bool avail = false;
                foreach (IWebElement option in optionCollection)
                    if (option.GetCssValue("background").Contains("224, 233, 234"))
                        option.Click();
                avail = true;

                optionCollection = driver._findElements("xpath", "//cft-field-editor-multi-list//label/span");
                foreach (IWebElement option in optionCollection)
                    if (option.GetCssValue("background").Contains("224, 233, 234"))
                    {
                        avail = false;
                        Assert.AreEqual(null, option.GetCssValue("background"), "'" + option.Text + "' did not get deselected.");
                        break;
                    }
                Assert.IsTrue(avail, "Media Outlet Filter List did not get cleared");

            }
            else if (driver._waitForElement("xpath", xpathPrefix + "//div[@row-id]/div"))
            {
                xpathPart = "//div[@row-id]/div";
                if (driver._isElementPresent("xpath", xpathPrefix + "//button[text()='Clear Selected']") && driver._getAttributeValue("xpath", xpathPrefix + "//button[text()='Clear Selected']", "disabled") == null)
                    driver._click("xpath", xpathPrefix + "//button[text()='Clear Selected']");
            }
            else if (driver._waitForElement("xpath", xpathPrefix + "//li"))
                xpathPart = "//li";

            IList<IWebElement> searchFilterValuesColl = driver._findElements("xpath", xpathPrefix + xpathPart);
            Random rand = new Random();
            int x = rand.Next(0, searchFilterValuesColl.Count);

            if (xpathPart == "//label")
                xpathPart = "//div[" + (x + 1) + "]/label";
            else if (xpathPart == "//div[@row-id]/div")
                xpathPart = "//div[@row-id][" + (x + 1) + "]/div";
            else if (xpathPart == "//li")
                xpathPart = "li[" + (x + 1) + "]";

            driver._scrollintoViewElement("xpath", xpathPrefix + xpathPart);
            string filterValue = driver._getText("xpath", xpathPrefix + xpathPart);
            driver._click("xpath", xpathPrefix + xpathPart);

            Results.WriteStatus(test, "Pass", "Selected, '" + filterValue + "' from Open Tab of Search Options Popup");
            return filterValue;
        }

        ///<summary>
        ///Return Number of Creatives of A Search
        ///</summary>
        ///<returns></returns>
        public int returnNumberOfCreativesOfASearch(string searchName)
        {
            int creativesCount = -1;

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]"), "Load Search button not present.");
            driver._click("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]");

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='flex-grow-1']//a"), "Select Saved Search DDL not present");
            IList<IWebElement> savedSearchDDLColl = driver._findElements("xpath", "//div[@class='flex-grow-1']//a");

            bool avail = false;
            foreach (IWebElement savedSearch in savedSearchDDLColl)
                if (savedSearch.Text.ToLower().Contains(searchName.ToLower()))
                {
                    avail = true;
                    IList<IWebElement> creativesColl = savedSearch._findElementsWithinElement("xpath", "./span");
                    IList<IWebElement> loadingCount = driver.FindElements(By.XPath("//p[@class='lead' and contains(text(),'Loading')]"));
                    string sCreativesCount = creativesColl[0].Text;
                    if (loadingCount.Count > 0)
                        Assert.AreEqual("0", sCreativesCount, "Creative Count is not 0 for loading search");
                    Assert.IsTrue(int.TryParse(sCreativesCount, out creativesCount), "Couldn't convert Creatives count into integer");
                    break;
                }
            Assert.IsTrue(avail, "'" + searchName + "' search not present in Saved Searches DDL");
            Assert.Greater(creativesCount, -1, "Creatives Count was not captured.");
            driver._click("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]");

            Results.WriteStatus(test, "Pass", "Creative count for Saved Search '" + searchName + "' is '" + creativesCount + "'.");
            return creativesCount;
        }

        ///<summary>
        ///Verify Filter Now Bar
        ///</summary>
        ///<returns></returns>
        public Search VerifyFilterNowBar(string value = "")
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-field-editor-keyword-search-my-search//input[@type='text']"), "Filter Bar not present on Screen.");
            Assert.AreEqual(driver._getAttributeValue("xpath", "//cft-field-editor-keyword-search-my-search//input[@type='text']", "placeholder"), "Filter results by keyword ...", "Filter Results text not present in filter bar.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//span[contains(@class, 'NU-icon-search')]"), "Search icon not present on Filter Bar.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-field-editor-keyword-search-my-search//button"), "Filter Now button not present on Filter Bar.");
            Assert.IsTrue(driver._getText("xpath", "//cft-field-editor-keyword-search-my-search//button").Contains("Filter Now"), "Filter Now text does not match on Filter Now button in filter bar.");

            if(value != "")
                Assert.IsTrue(driver._getValue("xpath", "//cft-field-editor-keyword-search-my-search//input[@type='text']").ToLower().Contains(value.ToLower()), "'" + value + "' not present in Filter Bar Text Box");

            Results.WriteStatus(test, "Pass", "Verified, Filter Bar.");
            return new Search(driver, test);
        }

        ///<summary>
        ///Search From Filter Now Bar
        ///</summary>
        ///<returns></returns>
        public Search SearchFromFilterNowBar(string searchValue)
        {
            driver._type("xpath", "//cft-field-editor-keyword-search-my-search//input[@type='text']", searchValue);
            driver._click("xpath", "//cft-field-editor-keyword-search-my-search//button");
            Thread.Sleep(2000);

            Results.WriteStatus(test, "Pass", "Searched, from Filter Now Bar successfully.");
            return new Search(driver, test);
        }

        #endregion



        #endregion
    }
}
