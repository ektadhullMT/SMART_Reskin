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

namespace SMART_AUTO
{
    public class Home
    {
        #region Private Variables

        private IWebDriver home;
        private ExtentTest test;

        #endregion

        #region Public Methods

        public Home(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.home = driver;
            test = testReturn;

        }

        public IWebDriver driver
        {
            get { return this.home; }
            set { this.home = value; }
        }

        /// <summary>
        /// To Verify Home Page
        /// </summary>
        /// <returns></returns>
        public Home VerifyHomePage()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//img[@alt='Product Logo']", 20), "Home Page Logo not Present.");
            Thread.Sleep(5000);
            if (driver._isElementPresent("xpath", "//span[@class='lead' and contains(text(),'Almost there')]"))
                driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Almost there')]");
            Assert.IsTrue(driver._waitForElement("xpath", "//*[@id='domain-carousel']/ol", 20), "Carousel not Present on Screen.");
            driver._waitForElementToBeHidden("xpath", "//p[@class='lead' and contains(text(),'Loading')]");
            int cnt = 0;
            IList<IWebElement> loadingCount = driver.FindElements(By.XPath("//p[@class='lead' and contains(text(),'Loading')]"));
            do
            {
                Thread.Sleep(1000);
                loadingCount = driver.FindElements(By.XPath("//p[@class='lead' and contains(text(),'Loading')]"));
                cnt++;
                if (cnt == 15)
                    break;

            } while (loadingCount.Count.Equals(0) == false);

            driver._waitForElementToBeHidden("xpath", "//p[@class='lead' and contains(text(),'Loading')]");
            Assert.AreEqual(0, loadingCount.Count, "Home Page Not Load Properly.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//span[@class='btn btn-default active']"), "Dashboard Page not Display Properly.");
            Results.WriteStatus(test, "Pass", "Verified, Home Page Screen.");

            VerifyRecordsOnReportScreen();
            return new Home(driver, test);
        }

        /// <summary>
        /// Expand Menu Option and Select Option from List on Page
        /// </summary>
        /// <param name="optionName">Option Name to Select</param>
        /// <returns></returns>
        public Home clickSiteNavigationMenuIconAndSelectOptionFromListOnPage(string optionName)
        {
            bool avail = false;
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='baseexpand']"), "Base Icon not Present on Page.");

            if (driver._isElementPresent("xpath", "//button[@id='baseexpand' and contains(@class,'active')]") == false)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver._findElement("xpath", "//*[@id='baseexpand']"));
                driver._waitForElement("xpath", "//button[@id='baseexpand' and contains(@class,'active')]", 20);
                Results.WriteStatus(test, "Pass", "Clicked, Navigation Menu Icon on Page.");
            }

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='levelHolderClass visible']/ul/li"), "Options List not Found.");
            IList<IWebElement> ElementCollections = driver.FindElements(By.XPath("//div[@class='levelHolderClass visible']/ul/li"));

            for (int i = 0; i <= ElementCollections.Count; i++)
            {
                if (ElementCollections[i].Text.ToLower().Contains(optionName.ToLower()))
                {
                    if (ElementCollections[i].GetAttribute("class") == "active")
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver._findElement("xpath", "//*[@id='baseexpand']"));
                        Results.WriteStatus(test, "Pass", "'" + optionName + "' Navigation Option Already Open.");
                    }
                    else
                    {
                        ElementCollections[i].Click();
                        Thread.Sleep(8000);
                        Results.WriteStatus(test, "Pass", "'" + optionName + "' Navigation Option Clicked.");
                    }
                    avail = true;
                    break;
                }
            }
            Assert.AreEqual(true, avail, "'" + optionName + "' Navigation Option not Present on List.");
            return new Home(driver, test);
        }

        /// <summary>
        /// Click User Menu and Select Account from List
        /// </summary>
        /// <param name="accountName">Account Name to select</param>
        /// <returns></returns>
        public Home clickUserMenuAndSelectAccountFromList(string accountName)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[contains(@class,'btn-group ng-scope dropdown btn-group-info')]"), "User Menu not Present on screen.");

            if (driver._isElementPresent("xpath", "//div[@class='btn-group ng-scope dropdown btn-group-info open']") == false)
            {
                driver._clickByJavaScriptExecutor("//div[@class='btn-group ng-scope dropdown btn-group-info']/button");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='btn-group ng-scope dropdown btn-group-info open']", 10), "User Menu Icon List not Open.");
            }

            bool avail = false;
            IList<IWebElement> accounts = driver.FindElements(By.XPath("//ul[contains(@class,'dropdown-menu dropdown-menu-form dropdown-menu-scroll')]/li"));
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Text.Contains(accountName))
                {
                    accounts[i].Click();
                    Thread.Sleep(5000);
                    avail = true;
                    break;
                }
            }

            Assert.AreEqual(true, avail, "'" + accountName + "' Account not Present on List.");
            Results.WriteStatus(test, "Pass", "Clicked, User Menu and Selected '" + accountName + "' Account name from List.");
            return new Home(driver, test);
        }

        /// <summary>
        /// Click Menu Icon from Screen
        /// </summary>
        /// <param name="menuName">Manu Name for Click</param>
        /// <returns></returns>
        public Home clickMenuIconFromScreen(string menuName)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='pull-right menuItem']/div/a"), "Menu Items not Present on Page.");
            bool avail = false;

            IList<IWebElement> menuIcons = driver.FindElements(By.XPath("//div[@class='pull-right menuItem']/div/a"));
            for (int i = 0; i < menuIcons.Count; i++)
            {
                if (menuIcons[i].Text.Contains(menuName))
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", menuIcons[i]);
                    Thread.Sleep(5000);
                    avail = true;
                    break;
                }
            }

            Assert.AreEqual(true, avail, "Menu Name not Present on Screen.");
            Results.WriteStatus(test, "Pass", "Clicked, '" + menuName + "' Menu Icon from Screen");
            return new Home(driver, test);
        }

        /// <summary>
        /// Verify Bottom Panel of screen
        /// </summary>
        /// <returns></returns>
        public Home VerifyBottomPanelOfScreen()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@ng-repeat='crumb in footerCtrl.data.breadcrumb']", 20), "'Screen Path' at Bottom not Present.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@ng-if='footerCtrl.data.contactUs.contactNumber']"), "Call for more info text not present.");
            Assert.AreEqual("Call 888-503-7533 for more info", driver._getText("xpath", "//div[@ng-if='footerCtrl.data.contactUs.contactNumber']"), "'Call 888-503-7533 for more info' text not match.");
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@ng-if='footerCtrl.data.company.isVisible']/a"), "'Numerator' Logo not present at bottom.");
            Results.WriteStatus(test, "Pass", "Verified, Bottom Panel of Screen.");
            return new Home(driver, test);
        }

        /// <summary>
        /// Click Numerator logo from bottom
        /// </summary>
        /// <returns></returns>
        public Home clickMarketTrackLogoFromBottom()
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@ng-if='footerCtrl.data.company.isVisible']/a"), "'Numerator' Logo not present at bottom.");
            driver._clickByJavaScriptExecutor("//div[@ng-if='footerCtrl.data.company.isVisible']/a");
            Results.WriteStatus(test, "Pass", "Clicked, Numerator Logo from Bottom.");
            return new Home(driver, test);
        }

        /// <summary>
        /// Expand Menu Option and Select Option from List on Page
        /// </summary>
        /// <param name="optionName">Option Name to Select</param>
        /// <param name="subOption">Sub Option Name to Select</param>
        /// <returns></returns>
        public Home clickSiteNavigationMenuIconAndSelectOptionAndSubOptionFromListOnPage(string optionName, string subOption)
        {
            bool avail = false;
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='baseexpand']"), "Base Icon not Present on Page.");

            if (driver._isElementPresent("xpath", "//button[@id='baseexpand' and contains(@class,'active')]") == false)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver._findElement("xpath", "//*[@id='baseexpand']"));
                driver._waitForElement("xpath", "//button[@id='baseexpand' and contains(@class,'active')]", 20);
                Results.WriteStatus(test, "Pass", "Clicked, Navigation Menu Icon on Page.");
            }

            if (driver._isElementPresent("xpath", "//div[@class='levelHolderClass visible']//div[@class='backItemClass']") == true)
            {
                driver._clickByJavaScriptExecutor("//div[@class='levelHolderClass visible']//div[@class='backItemClass']/a");
                Thread.Sleep(500);
            }

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='levelHolderClass visible']/ul/li"), "Options List not Found.");
            IList<IWebElement> ElementCollections = driver.FindElements(By.XPath("//div[@class='levelHolderClass visible']/ul/li"));

            for (int i = 0; i <= ElementCollections.Count; i++)
            {
                if (ElementCollections[i].Text.Contains(optionName))
                {
                    if (ElementCollections[i].GetAttribute("class") == "active")
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver._findElement("xpath", "//*[@id='baseexpand']"));
                        Results.WriteStatus(test, "Pass", "'" + optionName + "' Navigation Option Already Open.");
                    }
                    else
                    {
                        ElementCollections[i].Click();
                        Thread.Sleep(1000);

                        IList<IWebElement> subElementCollections = ElementCollections[i]._findElementsWithinElement("xpath", ".//ul/li");
                        for (int j = 0; j <= subElementCollections.Count; j++)
                        {
                            if (subElementCollections[j].Text.ToLower().Contains(subOption.ToLower()))
                            {
                                if (subElementCollections[j].GetAttribute("class") == "active")
                                {
                                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver._findElement("xpath", "//*[@id='baseexpand']"));
                                    Results.WriteStatus(test, "Pass", "'" + optionName + "' Navigation Option Already Open.");
                                    break;
                                }
                                else
                                {
                                    subElementCollections[j].Click();
                                    Thread.Sleep(8000);
                                    avail = true;
                                    break;
                                }
                            }
                        }
                        avail = true;
                        Results.WriteStatus(test, "Pass", "'" + optionName + "' Navigation Option Clicked.");
                    }
                    avail = true;
                    break;
                }
            }
            Assert.AreEqual(true, avail, "'" + optionName + "' Navigation Option not Present on List.");
            return new Home(driver, test);
        }

        /// <summary>
        /// Verify Records on Reports Screen
        /// </summary>
        /// <returns></returns>
        public Home VerifyRecordsOnReportScreen()
        {
            if (driver._isElementPresent("xpath", "//span[@class='lead' and contains(text(),'No items were found')]") || driver._isElementPresent("xpath", "//span[@class='lead' and contains(text(),'No results found')]"))
                clickOnDayFilterFieldAndClickOption("Last Month");

            return new Home(driver, test);
        }

        /// <summary>
        /// Click on day Filter Field and Click on Option
        /// </summary>
        /// <param name="optionName">Option Name for Click</param>
        /// <returns></returns>
        public Home clickOnDayFilterFieldAndClickOption(string optionName = "")
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//nav[contains(@class,'navbar') and @role='navigation']//.//ul//.//li[@class='dropdown']"), "Filter Options not Present.");
            IList<IWebElement> fieldsCollection = driver.FindElements(By.XPath("//nav[contains(@class,'navbar') and @role='navigation']//.//ul//.//li[@class='dropdown']"));

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[contains(@id,'internal_timeframe')]/a"));
            driver._clickByJavaScriptExecutor("//*[contains(@id,'internal_timeframe')]/a");
            Thread.Sleep(500);

            IList<IWebElement> optionsCollections = driver.FindElements(By.XPath("//li[@class='dropdown open']//.//ul/li[1]//.//ul[contains(@class,'insert-ranges')]/li"));
            if (optionName != "")
                for (int j = 0; j < optionsCollections.Count; j++)
                {
                    if (optionName == optionsCollections[j].Text)
                    {
                        optionsCollections[j].Click();
                        Thread.Sleep(5000);
                        driver._waitForElement("xpath", "//*[@id='domain-carousel']/ol", 20);
                        break;
                    }
                }

            Results.WriteStatus(test, "Pass", "Clicked, Days Filter field and Clicked '" + optionName + "' option");
            return new Home(driver, test);
        }

        /// <summary>
        /// Verify Menus Icon Buttons on Top of Screen
        /// </summary>
        /// <param name="iconNames">Menu Icon Names to Verify</param>
        /// <returns></returns>
        public Home VerifyMenusIconButtonsOnTopOfScreen(string[] iconNames = null)
        {
            Assert.AreEqual(true, driver._isElementPresent("xpath", "//*[@id='baseexpand']"), "'Navigation Menu' Icon not Present on Page.");

            if (iconNames != null)
            {
                Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@class='pull-right menuItem']"), "'Menu Icons' not Present on top of Screen.");
                IList<IWebElement> menuCollections = driver._findElements("xpath", "//div[@class='pull-right menuItem']");
                foreach (IWebElement menus in menuCollections)
                    Assert.AreEqual(iconNames[menuCollections.IndexOf(menus)], menus.Text, "'" + menus.Text + "' Menu Icon not Present on Top.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Menu Icon Buttons on Top of Screen.");
            return new Home(driver, test);
        }

        /// <summary>
        /// Get the Current Report id from URL
        /// </summary>
        /// <returns></returns>
        public String getTheCurrentReportIdFromURL()
        {
            string reportId = driver.Url.Split('=').Last().ToLower();
            return reportId;
        }

        #endregion

        #region Home_New

        /// <summary>
        /// To Verify New Home Page
        /// </summary>
        /// <returns></returns>
        public Home newVerifyHomePage()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//img[@class='sidebar-logo img-fluid']", 20) || driver._waitForElement("xpath", "//img[@class='sidebar-logo-sm img-fluid']", 20), "Home Page Logo not Present.");
            Thread.Sleep(5000);
            if (driver._isElementPresent("xpath", "//span[@class='lead' and contains(text(),'Almost there')]"))
                driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Almost there')]");
            //Assert.IsTrue(driver._waitForElement("xpath", "//*[@id='domain-carousel']/ol", 20), "Carousel not Present on Screen.");
            driver._waitForElementToBeHidden("xpath", "//p[@class='lead' and contains(text(),'Loading')]");
            int cnt = 0;
            IList<IWebElement> loadingCount = driver.FindElements(By.XPath("//p[@class='lead' and contains(text(),'Loading')]"));
            do
            {
                Thread.Sleep(1000);
                loadingCount = driver.FindElements(By.XPath("//p[@class='lead' and contains(text(),'Loading')]"));
                cnt++;
                if (cnt == 15)
                    break;

            } while (loadingCount.Count.Equals(0) == false);

            driver._waitForElementToBeHidden("xpath", "//p[@class='lead' and contains(text(),'Loading')]");
            Assert.AreEqual(0, loadingCount.Count, "Home Page Not Load Properly.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//cft-saved-search-dropdown//button"), "Dashboard Page not Displayed Properly.");
            Results.WriteStatus(test, "Pass", "Verified, Home Page Screen.");

            VerifyRecordsOnReportScreen();
            return new Home(driver, test);
        }

        /// <summary>
        /// To Verify New Home Page in Detail
        /// </summary>
        /// <returns></returns>
        public Home newVerifyHomePageInDetail(string accountName = "CFT Development")
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//img[@class='sidebar-logo img-fluid']", 20), "Home Page Logo not Present.");
            Thread.Sleep(5000);
            if (driver._isElementPresent("xpath", "//span[@class='lead' and contains(text(),'Almost there')]"))
                driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Almost there')]");
            //Assert.IsTrue(driver._waitForElement("xpath", "//*[@id='domain-carousel']/ol", 20), "Carousel not Present on Screen.");
            driver._waitForElementToBeHidden("xpath", "//p[@class='lead' and contains(text(),'Loading')]");
            int cnt = 0;
            IList<IWebElement> loadingCount = driver.FindElements(By.XPath("//p[@class='lead' and contains(text(),'Loading')]"));
            do
            {
                Thread.Sleep(1000);
                loadingCount = driver.FindElements(By.XPath("//p[@class='lead' and contains(text(),'Loading')]"));
                cnt++;
                if (cnt == 15)
                    break;

            } while (loadingCount.Count.Equals(0) == false);

            driver._waitForElementToBeHidden("xpath", "//p[@class='lead' and contains(text(),'Loading')]");
            Assert.AreEqual(0, loadingCount.Count, "Home Page Not Load Properly.");

            Assert.AreEqual(true, driver._isElementPresent("xpath", "//div[@_ngcontent-c1]"), "Dashboard Page not Display Properly.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'sidebar-body')]//ul//h6"), "Side bar headings not present");
            IList<IWebElement> sideBarTitlesColl = driver._findElements("xpath", "//div[contains(@class, 'sidebar-body')]//ul//h6");

            int ReportsIndex = -1;
            for(int i = 0; i < sideBarTitlesColl.Count; i++)
                if (sideBarTitlesColl[i].Text.ToLower().Contains("reports"))
                {
                    ReportsIndex = i + 1;
                    break;
                }
            Assert.AreNotEqual(-1, ReportsIndex, "'Reports' section not found in side navigation bar");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'sidebar-body')]//ul[" + ReportsIndex + "]//li[@class='nav-item']"), "Reports not present in side navigation bar");

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]"), "Select Saved Search Button not present");

            selectSavedSearchOrCreateNewSavedSearch(true, accountName);
            
            //if(!driver._waitForElement("xpath", "//div[@class = 'd-flex NU-panel-chart']//div[@class = 'NU-chart-message']"))
            //    Assert.IsTrue(driver._waitForElement("xpath", "//div[@class = 'd-flex NU-panel-chart']//*[name()='svg']"), "Creatives Chart not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='flex-grow-1']//cft-scheduled-export-modal//button"), "Schedule Alert Button not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='flex-grow-1']//button[@aria-describedby='tooltip-1']"), "Reset Button not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='flex-grow-1']//cft-search-filter-summary/div/*/*"), "Summary Tags not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//button[@tooltip='Field Options']"), "Field Options Button not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//button[@tooltip='Export Results']"), "Export Button not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-search-filter-button//button"), "Search Button not present");

            if(!(getActiveScreenNameFromSideNavigationBar().ToLower().Contains("print report by") || getActiveScreenNameFromSideNavigationBar().ToLower().Contains("trend")))
                Assert.IsTrue(driver._isElementPresent("xpath", "//ol[contains(@class, 'breadcrumbs')]//button[text()]"), "Breadcrumb 'All Analytics' not present on Home Page.");

            Results.WriteStatus(test, "Pass", "Verified, Home Page Screen in Detail.");
            return new Home(driver, test);
        }

        /// <summary>
        /// Select Saved Search Or Create New Saved Search
        /// </summary>
        /// <returns></returns>
        public string selectSavedSearchOrCreateNewSavedSearch(bool createNew = true, string accountName = "CFT Development")
        {
            string searchName = driver._getText("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]");

            if (searchName.ToLower().Contains("untitled"))
            {
                driver._click("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='flex-grow-1']//a"), "Select Saved Search DDL not present");
                IList<IWebElement> savedSearchDDLColl = driver._findElements("xpath", "//div[@class='flex-grow-1']//a");

                if (savedSearchDDLColl.Count > 1)
                {
                    IList<IWebElement> searchNameTextColl = savedSearchDDLColl[1]._findElementsWithinElement("xpath", ".//div/span[text()]");
                    searchName = searchNameTextColl[0].Text;
                    savedSearchDDLColl[1].Click();
                }
                else if(createNew)
                {
                    VerifyAndModifySearchOptions(true, false, accountName);
                    searchName = saveNewSearch(false, true, accountName);
                }
            }
            else
            {
                driver._click("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='flex-grow-1']//a"), "Select Saved Search DDL not present");
                IList<IWebElement> savedSearchDDLColl = driver._findElements("xpath", "//div[@class='flex-grow-1']//a");

                if (savedSearchDDLColl.Count > 2)
                {
                    IList<IWebElement> searchNameTextColl = savedSearchDDLColl[2]._findElementsWithinElement("xpath", ".//div/span[text()]");
                    searchName = searchNameTextColl[0].Text;
                    savedSearchDDLColl[2].Click();
                }
                else if (createNew)
                {
                    VerifyAndModifySearchOptions(true, false, accountName);
                    searchName = saveNewSearch(false, true, accountName);
                }
            }

            driver._waitForElementToBeHidden("xpath", "//p[@class='lead' and contains(text(),'Loading')]");
            int cnt = 0;
            IList<IWebElement> loadingCount = driver.FindElements(By.XPath("//p[@class='lead' and contains(text(),'Loading')]"));
            do
            {
                Thread.Sleep(1000);
                loadingCount = driver.FindElements(By.XPath("//p[@class='lead' and contains(text(),'Loading')]"));
                cnt++;
                if (cnt == 15)
                    break;

            } while (loadingCount.Count.Equals(0) == false);

            driver._waitForElementToBeHidden("xpath", "//p[@class='lead' and contains(text(),'Loading')]");
            Assert.AreEqual(0, loadingCount.Count, "Home Page Not Load Properly.");

            Results.WriteStatus(test, "Pass", "Selected, Saved Search '" + searchName + "'.");
            return searchName;
        }

        /// <summary>
        /// Verify and Modify Search Options
        /// </summary>
        /// <returns></returns>
        public Home VerifyAndModifySearchOptions(bool randomModify = false, bool reset = false, string accountName = "CFT Developer", bool clickTag = false)
        {
            if(!clickTag)
                driver._click("xpath", "//cft-search-filter-button//button");

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-content']"), "Search Options popup not present");
            if (driver._isElementPresent("xpath", "//div[@class='modal-content']//*[contains(text(),'Loading')]"))
                Thread.Sleep(2000);
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class,'modal-body')]//button[contains(@class, 'rounded') and not(@id)]"), "Search Options popup menu not present");
            IList<IWebElement> searchOptionsColl = driver._findElements("xpath", "//div[contains(@class,'modal-body')]//button[contains(@class, 'rounded') and not(@id)]");
            string[] searchOptionNames = { "All Fields", "Ad Status", "Date Range", "Media", "Advertiser Product", "Category", "Market" };
            if(accountName.Equals("QA Testing - Brand Canada"))
            {
                string[] newsearchOptionNames = { "All Fields", "Keyword", "Ad Code", "Date Range", "Media", "Company Division Brand", "Category Class", "Language", "Clearance", "Coop Advertisers", "Market" };
                int newLength = searchOptionNames.Length - newsearchOptionNames.Length;
                if (newLength < 0)
                    newLength = (newLength * -1) + searchOptionNames.Length;
                else
                    newLength = searchOptionNames.Length - newLength;
                Array.Resize(ref searchOptionNames, newLength);
                Array.Copy(newsearchOptionNames, searchOptionNames, newsearchOptionNames.Length);
            }
            bool avail = false;

            foreach (string searcOptionName in searchOptionNames)
            {
                foreach (IWebElement searchOption in searchOptionsColl)
                    if (searchOption.Text.ToLower().Contains(searcOptionName.ToLower()))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "'" + searcOptionName + "' not found");
            }

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class,'modal-body')]//div[@class='container-fluid']"), "Search Filter Sections not present");
            IList<IWebElement> searchFilterSectionsColl = driver._findElements("xpath", "//div[contains(@class,'modal-body')]//div[@class='container-fluid']//h6");
            string[] searchFilterSectionNames = { "Ad Status", "Date Range", "Media", "Advertiser Product", "Category", "Market" };
            if (accountName.Equals("QA Testing - Brand Canada"))
            {
                string[] newsearchFilterSectionNames = { "Keyword", "Ad Code", "Date Range", "Media", "Company Division Brand", "Category Class", "Language"};
                int newLength = searchFilterSectionNames.Length - newsearchFilterSectionNames.Length;
                if (newLength < 0)
                    newLength = (newLength * -1) + searchFilterSectionNames.Length;
                else
                    newLength = searchFilterSectionNames.Length - newLength;
                Array.Resize(ref searchFilterSectionNames, newLength);
                Array.Copy(newsearchFilterSectionNames, searchFilterSectionNames, newsearchFilterSectionNames.Length);
            }
            else if(accountName.Equals("Tabular Grid"))
            {
                string[] newsearchFilterSectionNames = { "Date Range", "Media", "Advertiser Product", "Category", "Market" };
                int newLength = searchFilterSectionNames.Length - newsearchFilterSectionNames.Length;
                if (newLength < 0)
                    newLength = (newLength * -1) + searchFilterSectionNames.Length;
                else
                    newLength = searchFilterSectionNames.Length - newLength;
                Array.Resize(ref searchFilterSectionNames, newLength);
                Array.Copy(newsearchFilterSectionNames, searchFilterSectionNames, newsearchFilterSectionNames.Length);

            }
            foreach (string searchFilterSectionName in searchFilterSectionNames)
            {
                avail = false;
                foreach(IWebElement searchFilterSection in searchFilterSectionsColl)
                    if (searchFilterSection.Text.ToLower().Contains(searchFilterSectionName.ToLower()))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "'" + searchFilterSectionName + "' not found");
            }
            

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class,'modal-footer')]//button"), "'Reset All, Cancel and Apply' Buttons on Search Options not present");
            IList<IWebElement> buttonsCollection = driver._findElements("xpath", "//div[contains(@class,'modal-footer')]//button");
            avail = false;
            if (reset)
            {
                foreach (IWebElement button in buttonsCollection)
                    if (button.Text.ToLower().Contains("reset all"))
                    {
                        avail = true;
                        button.Click();
                        break;
                    }
                Assert.IsTrue(avail, "Reset Button was not found");
            }


            if (randomModify)
            {
                int j = 2;
                if (accountName.Equals("QA Testing - Brand Canada"))
                    j = 6;
                for (int i = 0; i < searchFilterSectionsColl.Count; i++)
                {
                    if (i == 0 || i == j)
                    {
                        Assert.IsTrue(driver._waitForElement("xpath", "//div[contains(@class,'modal-body')]//cft-field-editor-resolver[" + (i + 1) + "]//div[@class='container-fluid']//label"), "Search Filter Options not present");
                        IList<IWebElement> filterOptionsColl = driver._findElements("xpath", "//div[contains(@class,'modal-body')]//cft-field-editor-resolver[" + (i + 1) + "]//div[@class='container-fluid']//label");
                        Random rand = new Random();
                        int x = rand.Next(0, filterOptionsColl.Count);
                        if (filterOptionsColl[x].Text.ToLower().Contains("opt-in email"))
                            if (x == filterOptionsColl.Count - 1)
                                --x;
                            else
                                ++x;
                        filterOptionsColl[x].Click();
                    }
                }
                avail = false;
                foreach (IWebElement button in buttonsCollection)
                    if (button.Text.ToLower().Contains("apply"))
                    {
                        avail = true;
                        button.Click();
                        break;
                    }
                Assert.IsTrue(avail, "Apply Button was not found");

                Results.WriteStatus(test, "Pass", "Modified, Search Options.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Search Options Popup.");
            return new Home(driver, test);
        }

        /// <summary>
        /// Save New Search
        /// </summary>
        /// <returns></returns>
        public string saveNewSearch(bool makeDefault = false, bool save = true, string accountName = "CFT Development", string saveAs = "")
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-saved-search-save-modal/button"), "Save Search button is not present.");
            if (driver._getAttributeValue("xpath", "//cft-saved-search-save-modal/button", "disabled") != null)
                Thread.Sleep(1000);
            driver._click("xpath", "//cft-saved-search-save-modal/button");

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-content']"), "Save Search popup is not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-content']//h4"), "Save Search popup header is not present.");
            Assert.AreEqual("Save Search As ...", driver._getText("xpath", "//div[@class='modal-content']//h4"), "Save Search popup header text does not match.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body']//input[@type = 'text']"), "Search Name field is not present.");

            if(saveAs == "")
                saveAs = "searchName" + driver._randomString(3, true);
            driver._type("xpath", "//div[@class='modal-body']//input[@type = 'text']", saveAs);

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body']//label"), "'Make it my default search' checkbox and text is not present.");
            Assert.AreEqual("Make it my default search", driver._getText("xpath", "//div[@class='modal-body']//label"), "'Make it my default search' text does not match.");

            if (makeDefault)
                driver._click("xpath", "//div[@class='modal-body']//label[@for= 'makeDefaultSearch']");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-content']//p"), "'This search will only be applied to your Dashboard report.' text is not present.");
            Assert.AreEqual(true, driver._getText("xpath", "//div[@class='modal-content']//p").Contains("This search will only be applied to your ") , "'This search will only be applied to your Dashboard report.' text does not match.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Cancel ']"), "'Cancel' button is not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Save ']"), "'Save' button is not present.");

            if (save)
                driver._click("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Save ']");
            else
                driver._click("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Cancel ']");

            Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[@class='modal-content']"), "Save Search popup is still present.");

            Results.WriteStatus(test, "Pass", "Created, New Search '" + saveAs + "'.");
            return saveAs;
        }

        ///<summary>
        ///Verify Thumbnail View of AgGrid
        ///</summary>
        ///<returns></returns>
        public Home VerifyThumbnailViewOfAgGrid(bool adCodeFilter = false)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details//div[@class='row']"), "Results not present in Thumbnails view.");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']"), "Result Cards not present in Thumbnails view.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']//input"), "'Checkbox' not present on Carousel Cards");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']//span"), "'Advertiser's Name' not present on Carousel Cards");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']//img"), "'Thumbnail Image' not present on Carousel Cards");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[1]/div[@class='NU-card card p-0 NU-selectable-card']//button"), "'Card Buttons' not present on Carousel Cards");
            IList<IWebElement> cardButtonCollection = driver._findElements("xpath", "//div[1]/div[@class='NU-card card p-0 NU-selectable-card']//button");
            string[] cardButtonNames = { "View Ad", "Markets", "Details" };
            if (adCodeFilter)
            {
                    string[] newCardButtonNames = { "View Ad", "Details" };
                    Array.Copy(newCardButtonNames, cardButtonNames, newCardButtonNames.Length);
            }
            foreach (string cardButtonName in cardButtonNames)
            {
                bool avail = false;
                foreach (IWebElement cardButton in cardButtonCollection)
                    if (cardButton.Text.ToLower().Contains(cardButtonName.ToLower()))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "'" + cardButtonName + "' not found on carousel");
            }

            Results.WriteStatus(test, "Pass", "Verified, Thumbnail View of AgGrid.");
            return new Home(driver, test);
        }

        ///<summary>
        ///Verify Details View of AgGrid
        ///</summary>
        ///<returns></returns>
        public Home VerifyDetailsViewOfAgGrid(bool adCodeFilter = false)
        {
            driver._scrollintoViewElement("xpath", "//cft-domain-item-details//div");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details//div[@class='row pb-3' and not(@hidden)]"), "Results not present in Details view.");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']"), "Result Cards not Details in Thumbnails view.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']//input"), "'Checkbox' not present on Carousel Cards");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']//span"), "'Advertiser's Name' not present on Carousel Cards");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']//img"), "'Thumbnail Image' not present on Carousel Cards");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[1]/div[@class='NU-card card p-0 NU-selectable-card']//button"), "'Card Buttons' not present on Carousel Cards");
            IList<IWebElement> cardButtonCollection = driver._findElements("xpath", "//div[1]/div[@class='NU-card card p-0 NU-selectable-card']//button");
            string[] cardButtonNames = { "View Ad", "Markets", "Details" };
            if (adCodeFilter)
            {
                string[] newCardButtonNames = { "View Ad", "Details", "Download" };
                Array.Copy(newCardButtonNames, cardButtonNames, newCardButtonNames.Length);
            }
            foreach (string cardButtonName in cardButtonNames)
            {
                bool avail = false;
                foreach (IWebElement cardButton in cardButtonCollection)
                    if (cardButton.Text.ToLower().Contains(cardButtonName.ToLower()))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "'" + cardButtonName + "' not found on carousel");
            }

            Results.WriteStatus(test, "Pass", "Verified, Details View of AgGrid.");
            return new Home(driver, test);
        }

        ///<summary>
        ///Verify Table View of AgGrid
        ///</summary>
        ///<returns></returns>
        public Home VerifyTableViewOfAgGrid()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//ag-grid-angular"), "Results not present");
            driver._scrollintoViewElement("xpath", "//ag-grid-angular");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']"), "AgGrid Header Row not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']//div[@ref='eLabel']"), "AgGrid Column Headers not present");
            IList<IWebElement> headerColumnColl = driver._findElements("xpath", "//ag-grid-angular//div[@class='ag-header-viewport']//div[contains(@class,'ag-header-cell-sortable')]");

            foreach(IWebElement header in headerColumnColl)
                if (header.Text.Contains("$"))
                {
                    IList<IWebElement> sortIconColl = header._findElementsWithinElement("xpath", ".//span[contains(@ref, 'Sort')]");
                    Assert.Greater(sortIconColl.Count, 0, "Sort Icon not present for Data Column");
                    Results.WriteStatus(test, "Pass", "Sort Icon is present for Data Column");
                    break;
                }

            Assert.IsTrue(driver._waitForElement("xpath", "//ag-grid-angular//div[@class='ag-body-container']/div"), "Rows not present");
            Assert.IsTrue(driver._waitForElement("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']//div[@col-id]"), "Checkboxes in Rows not present");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-table-thumbnail//ul/li"), "Pagination not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-table-thumbnail//div[@class='btn-group']/button"), "Rows per Page buttons not present");

            Results.WriteStatus(test, "Pass", "Verified, Table View of AgGrid.");
            return new Home(driver, test);
        }

        ///<summary>
        ///Select Option From Side Navigation Bar
        ///</summary>
        ///<returns></returns>
        public Home selectOptionFromSideNavigationBar(string optionName)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'sidebar')]//li[@class='nav-item']/a"), "Side bar options not present");
            IList<IWebElement> sideBarOptionsColl = driver._findElements("xpath", "//div[contains(@class, 'sidebar')]//li[@class='nav-item']/a");
            bool avail = false;
            int dashboardCount = 0;
            bool dashboardFlag = false;
            foreach(IWebElement sideBarOption in sideBarOptionsColl)
            {
                if (optionName.ToLower().Equals("dashboard dashboard") && sideBarOption.Text.ToLower().Contains("dashboard"))
                {
                    ++dashboardCount;
                    if(dashboardCount == 2)
                    {
                        dashboardFlag = true;
                        optionName = "Dashboard";
                    }
                }
                if (sideBarOption.Text.ToLower().Contains(optionName.ToLower()))
                {
                    if (sideBarOption.Text.ToLower().Equals("dashboard") && !dashboardFlag)
                        continue;
                    avail = true;
                    if (sideBarOption.GetAttribute("class").Contains("active"))
                        Results.WriteStatus(test, "Info", "'" + optionName + "' is already selected.");
                    else
                    {
                        Actions action = new Actions(driver);
                        action.MoveToElement(sideBarOption).Click().Perform();
                        sideBarOption.Click();
                    }
                    Thread.Sleep(4000);
                    Assert.IsTrue(sideBarOption.GetAttribute("class").Contains("active"), "'" + optionName + "' is not selected properly.");
                    break;
                }

            }
            Assert.IsTrue(avail, "'" + optionName + "' not found");

            Results.WriteStatus(test, "Pass", "Selected, '" + optionName + "' From Side Navigation Bar.");
            return new Home(driver, test);
        }

        ///<summary>
        ///Select View For Results Display
        ///</summary>
        ///<returns></returns>
        public Home selectViewForResultsDisplay(string viewName)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,800)");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='py-3']//span[@class='pr-2' and text()='Select a results layout:']"), "'Select a results layout' text not present.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-group//div[@class='py-3']//button"), "'Select a results layout' button not present.");
            driver._scrollintoViewElement("xpath", "//cft-domain-item-group//div[@class='py-3']//button");
            driver._click("xpath", "//cft-domain-item-group//div[@class='py-3']//button");

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='py-3']//li"), "'Select a results layout' DDL not present.");
            IList<IWebElement> resultsLayOutDDL = driver._findElements("xpath", "//div[@class='py-3']//li");
            bool avail = false;
            for (int i = 0; i < resultsLayOutDDL.Count; i++)
                if (resultsLayOutDDL[i].Text.ToLower().Contains(viewName.ToLower()))
                {
                    avail = true;
                    resultsLayOutDDL[i].Click();
                    break;
                }
            Assert.IsTrue(avail, "'" + viewName + "' View not present in DDL");
            Thread.Sleep(1000);

            Results.WriteStatus(test, "Pass", "Selected, '" + viewName + "' From Select Results Layout DDL.");
            return new Home(driver, test);
        }

        ///<summary>
        ///Get Active Screen Name From Side Navigation Bar
        ///</summary>
        ///<returns></returns>
        public string getActiveScreenNameFromSideNavigationBar()
        {
            string screenName = "";
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'sidebar')]//li[@class='nav-item']/a"), "Side bar options not present");
            IList<IWebElement> sideBarOptionsColl = driver._findElements("xpath", "//div[contains(@class, 'sidebar')]//li[@class='nav-item']/a");
            bool avail = false;
            foreach (IWebElement sideBarOption in sideBarOptionsColl)
                    if (sideBarOption.GetAttribute("class").Contains("active"))
                {
                    avail = true;
                    screenName = sideBarOption.Text;
                    break;
                }
            Assert.IsTrue(avail, "'Active Screen' not found");
            Results.WriteStatus(test, "Pass", "'Captured, '" + screenName + "' as selected screen.");
            return screenName;
        }

        ///<summary>
        ///Verify Account or Switch if Required
        ///</summary>
        ///<param name="account">Required Account</param>
        ///<return></return>
        public Home VerifyAccountOrSwitchIfRequired(string account = "QA Testing - Brand")
        {
            string activeScreen = getActiveScreenNameFromSideNavigationBar();
            UserProfile userProfile = new UserProfile(driver, test);

            if (account.ToLower().Equals("QA Testing - Brand".ToLower()))
            {
                if (activeScreen.Equals("QA Testing - Brand - Dashboard"))
                    Results.WriteStatus(test, "Pass", "Verified, Current logged in account is 'QA Testing - Brand'");
                else
                {
                    selectOptionFromSideNavigationBar("Settings");
                    userProfile.selectAccountNameOnUserScreen("QA Testing - Brand");
                    newVerifyHomePage();
                    Results.WriteStatus(test, "Pass", "Switched, Current logged in account is 'QA Testing - Brand'");
                }
            }
            else if (account.ToLower().Equals("QA Testing - Brand Monthly".ToLower()))
            {
                if (activeScreen.Equals("1. Ad Ex Report by Media"))
                    Results.WriteStatus(test, "Pass", "Verified, Current logged in account is 'QA Testing - Brand Canada'");
                else
                {
                    selectOptionFromSideNavigationBar("Settings");
                    userProfile.selectAccountNameOnUserScreen("QA Testing - Brand Monthly");
                    newVerifyHomePage();
                    Results.WriteStatus(test, "Pass", "Switched, Current logged in account is 'QA Testing - Brand Canada'");
                }
            }
            else if (account.ToLower().Equals("CFT Development".ToLower()))
            {
                if (activeScreen.Equals("Dashboard"))
                    Results.WriteStatus(test, "Pass", "Verified, Current logged in account is 'CFT Development'");
                else
                {
                    selectOptionFromSideNavigationBar("Settings");
                    userProfile.selectAccountNameOnUserScreen("CFT Development");
                    newVerifyHomePage();
                    Results.WriteStatus(test, "Pass", "Switched, Current logged in account is 'CFT Development'");
                }
            }
            else if (account.ToLower().Equals("QA Testing - Brand Canada".ToLower()))
            {
                if (activeScreen.Equals("Print Report by Category"))
                    Results.WriteStatus(test, "Pass", "Verified, Current logged in account is 'QA Testing - Brand Canada'");
                else
                {
                    selectOptionFromSideNavigationBar("Settings");
                    userProfile.selectAccountNameOnUserScreen("QA Testing - Brand Canada");
                    newVerifyHomePage();
                    Results.WriteStatus(test, "Pass", "Switched, Current logged in account is 'QA Testing - Brand Canada'");
                }
            }
            else
                Results.WriteStatus(test, "Info", "Account was not switched, please switch it through normal procedure.");

            return new Home(driver, test);
        }

        ///<summary>
        ///Click On Dashboard Button
        ///</summary>
        ///<returns></returns>
        public Home clickOnDashboardButton(string button)
        {
            switch (button.ToLower())
            {
                case "schedule alert":
                    Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='flex-grow-1']//cft-scheduled-export-modal//button"), "Schedule Alert Button not present");
                    driver._click("xpath", "//div[@class='flex-grow-1']//cft-scheduled-export-modal//button");
                    break;
                case "reset":
                    Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='flex-grow-1']//button[@tooltip='Reset this search']"), "Reset Button not present");
                    driver._click("xpath", "//div[@class='flex-grow-1']//button[@tooltip='Reset this search']");
                    break;
                case "saved search":
                    Assert.IsTrue(driver._waitForElement("xpath", "//cft-saved-search-save-modal/button"), "Save Search button is not present.");
                    driver._click("xpath", "//cft-saved-search-save-modal/button");
                    break;
                case "delete":
                    Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='flex-grow-1']//button[@tooltip='Delete this search']"), "Delete Button not present");
                    driver._click("xpath", "//div[@class='flex-grow-1']//button[@tooltip='Delete this search']");
                    break;
                case "default":
                    Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='flex-grow-1']//button[@tooltip='Make this my default search']"), "Make Search Default Button not present");
                    driver._click("xpath", "//div[@class='flex-grow-1']//button[@tooltip='Make this my default search']");
                    break;
                default:
                    break;
            }

            Thread.Sleep(2000);

            Results.WriteStatus(test, "Pass", "Clicked, On '" + button + "'Dashboard Button");
            return new Home(driver, test);
        }
       
        ///<summary>
        ///Get All Sidebar Options
        ///</summary>
        ///<returns></returns>
        public string[] getAllSidebarOptions()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[contains(@class, 'sidebar')]//li[@class='nav-item']/a"), "Side bar options not present");
            IList<IWebElement> sideBarOptionsColl = driver._findElements("xpath", "//div[contains(@class, 'sidebar')]//li[@class='nav-item']/a");


            string[] sideBarCollection = new string[sideBarOptionsColl.Count];
            for (int i = 0; i < sideBarOptionsColl.Count; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", sideBarOptionsColl[i]);
                sideBarCollection[i] = sideBarOptionsColl[i].Text;
            }

            Results.WriteStatus(test, "Pass", "Read, All Sidebar Options");
            return sideBarCollection;
        }


        #endregion

    }
}
