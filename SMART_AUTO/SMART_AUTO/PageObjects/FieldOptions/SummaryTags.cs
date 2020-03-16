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

namespace SMART_AUTO
{
    public class SummaryTags
    {
        #region Private Variables

        private IWebDriver summaryTags;
        private ExtentTest test;

        #endregion

        public SummaryTags(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.summaryTags = driver;
            test = testReturn;
        }

        public IWebDriver driver
        {
            get { return this.summaryTags; }
            set { this.summaryTags = value; }
        }

        ///<summary>
        ///Verify Summary Tags
        ///</summary>
        ///<param name="defaultTags">Whether displayed tags are default or not</param>
        ///<param name="tags">List of tags</param>
        ///<returns></returns>
        public bool VerifySummaryTags(string[] tags, bool defaultTags = true, bool continu = false)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='NU-tag-label']"), "Summary Tags not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//button[@tooltip='Reset this search']"), "Reset Search Icon not present");
            if (defaultTags)
                Assert.AreNotEqual(null, driver._getAttributeValue("xpath", "//button[@tooltip='Reset this search']", "disabled"), "Reset Search Icon is not disabled for default search");

            IList<IWebElement> tagCollection = driver._findElements("xpath", "//div[@class='NU-tag-label']");
            foreach(string tag in tags)
            {
                int index = -1;
                bool avail = false;
                for (int i = 0;  i < tagCollection.Count; i++)
                    if (tagCollection[i].Text.ToLower().Contains(tag.ToLower()))
                    {
                        avail = true;
                        index = i;
                        break;
                    }
                if (continu && !avail)
                    return false;
                Assert.IsTrue(avail, "'" + tag + "' not found in summary tags");
                if (driver._getText("xpath", "//cft-saved-search-dropdown//button[@dropdowntoggle]").ToLower().Contains(tag))
                    Assert.AreEqual(0, index, "'" + tag + "' Search Name was not the First Summary Tag");
                Results.WriteStatus(test, "Pass", "Verified, '" + tag + "' is present in summary tags on Index " + (index + 1));
            }

            Results.WriteStatus(test, "Pass", "Verified, Summary Tags");
            return true;
        }

        ///<summary>
        ///Verify Tooltips of Summary Tags
        ///</summary>
        ///<returns></returns>
        public SummaryTags VerifyTooltipsOfSummaryTags()
        {
            IList<IWebElement> tagCollection = driver._findElements("xpath", "//div[@class='NU-tag-label']");
            foreach (IWebElement tagEle in tagCollection)
            {
                string tooltipCheck = tagEle.Text;
                driver.MouseHoverByJavaScript(tagEle);
                Thread.Sleep(1000);
                Assert.IsTrue(driver._waitForElement("xpath", "//bs-tooltip-container/div[@class='tooltip-inner' and text()='" + tooltipCheck + "']"), "Tooltip for tag '" + tagEle.Text + "' not present");
            }

            Results.WriteStatus(test, "Pass", "Verified, Tooltips of Summary Tags");
            return new SummaryTags(driver, test);
        }

        ///<summary>
        ///Convert Search Selection to Summary Tags
        ///</summary>
        ///<returns></returns>
        public string[] convertSearchSelectionToSummaryTags(string[] refArray, string[] selection)
        {
            foreach(string selectResult in selection)
            {
                string[] currArray = selectResult.Split('*');
                int origLength = refArray.Length;
                Array.Resize(ref refArray, origLength + currArray.Length - 1);
                for(int i = origLength, j = 1; j < currArray.Length; i++, j++)
                    refArray[i] = currArray[j];
            }

            Results.WriteStatus(test, "Pass", "Converted, Search Selection to Summary Tags");
            return refArray;
        }

        ///<summary>
        ///Reset Single Summary Tag
        ///</summary>
        ///<returns></returns>
        public SummaryTags resetSingleSummaryTag(string tag)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tag//div[contains(@class, 'text-white')]//div[@class='NU-tag-label']"), "Summary Tags not present");
            IList<IWebElement> tagCollection = driver._findElements("xpath", "//cft-tag//div[contains(@class, 'text-white')]//div[@class='NU-tag-label']");

            if(!driver._getText("xpath", "//div[@class='flex-grow-1']//button[@aria-haspopup]").Contains("Untitled"))
            {
                IList<IWebElement> savedSearchResetIconCollection = driver._findElements("xpath", "//cft-tag//span[contains(@class,'NU-icon-reset')]");

                if (savedSearchResetIconCollection.Count == 0)
                    Results.WriteStatus(test, "Pass", "Summary Tag Reset icon not present.");
                else
                {
                    bool availReset = false;
                    for (int i = 0; i < tagCollection.Count; i++)
                        if (tagCollection[i].Text.ToLower().Contains(tag.ToLower()))
                        {
                            availReset = true;
                            savedSearchResetIconCollection[i].Click();
                            break;
                        }
                    Assert.IsTrue(availReset, "'" + tag + "' not found in summary tags");
                    Thread.Sleep(1000);
                    Results.WriteStatus(test, "Pass", "Reset, Summary Tag '" + tag + "'");
                }
            }
            else
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tag//span[contains(@class,'NU-icon-reset')]"), "Summary Tag Reset Icons not present");
                IList<IWebElement> resetIconCollection = driver._findElements("xpath", "//cft-tag//span[contains(@class,'NU-icon-reset')]");

                bool avail = false;
                for (int i = 0; i < tagCollection.Count; i++)
                    if (tagCollection[i].Text.ToLower().Contains(tag.ToLower()))
                    {
                        avail = true;
                        resetIconCollection[i].Click();
                        break;
                    }
                Assert.IsTrue(avail, "'" + tag + "' not found in summary tags");
                Thread.Sleep(1000);
                Results.WriteStatus(test, "Pass", "Reset, Summary Tag '" + tag + "'");
            }

            return new SummaryTags(driver, test);
        }
        
        ///<summary>
        ///Click On Single Summary Tag
        ///</summary>
        ///<returns></returns>
        public SummaryTags clickOnSingleSummaryTag(string tag)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='NU-tag-label']"), "Summary Tags not present");
            IList<IWebElement> tagCollection = driver._findElements("xpath", "//div[@class='NU-tag-label']");
            bool avail = false;
            for (int i = 0; i < tagCollection.Count; i++)
                if (tagCollection[i].Text.ToLower().Contains(tag.ToLower()))
                {
                    avail = true;
                    tagCollection[i].Click();
                    break;
                }
            Assert.IsTrue(avail, "'" + tag + "' not found in summary tags");
            Thread.Sleep(1000);
            Results.WriteStatus(test, "Pass", "Clicked, On Summary Tag '" + tag + "'");
            return new SummaryTags(driver, test);
        }

        ///<summary>
        ///Capture Summary Tags From Dashboard
        ///</summary>
        ///<returns></returns>
        public string[] captureSummaryTagsFromDashboard()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='NU-tag-label']"), "Summary Tags not present");
            IList<IWebElement> tagCollection = driver._findElements("xpath", "//div[@class='NU-tag-label']");
            string[] capturedTags = new string[tagCollection.Count];
            for (int i = 0; i < tagCollection.Count; i++)
                capturedTags[i] = tagCollection[i].Text;

            Results.WriteStatus(test, "Pass", "Captured, Summary Tags From Dashboard");
            return capturedTags;
        }

        ///<summary>
        ///Remove Search Name Summary Tag
        ///</summary>
        ///<returns></returns>
        public SummaryTags removeSearchNameSummaryTag(string searchName)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tag//div[contains(@class, 'text-white')]//div[@class='NU-tag-label']"), "Summary Tags not present");
            IList<IWebElement> tagCollection = driver._findElements("xpath", "//cft-tag//div[contains(@class, 'text-white')]//div[@class='NU-tag-label']");
            if (!driver._getText("xpath", "//cft-saved-search-dropdown//button[@dropdowntoggle]").ToLower().Contains("untitled search"))
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tag//span[contains(@class,'NU-icon-close')]"), "Summary Tag Remove Icons not present");
                if (tagCollection[0].Text.ToLower().Contains(searchName.ToLower()))
                    driver._click("xpath", "//cft-tag//span[contains(@class,'NU-icon-close')]");
                Thread.Sleep(3000);
                Results.WriteStatus(test, "Pass", "Removed, Summary Tag of Search Name'" + searchName + "'");
            }
            else
            {
                Assert.IsFalse(driver._isElementPresent("xpath", "//cft-tag//span[contains(@class,'NU-icon-close')]"), "Summary Tag Remove Icons are present when Saved Search is not loaded.");
                Results.WriteStatus(test, "Pass", "Verified, ");
            }

            return new SummaryTags(driver, test);
        }

        ///<summary>
        ///Verify That Ad Status and Date Range Field Summary Tags Should be default for Untitled Search
        ///</summary>
        ///<returns></returns>
        public SummaryTags VerifyThatAdStatusAndDateRangeFieldSummaryTagsShouldBeDefaultForUntitledSearch()
        {
            if (!driver._getText("xpath", "//cft-saved-search-dropdown//button[@dropdowntoggle]").ToLower().Contains("untitled search"))
            {
                Search searchPage = new Search(driver, test);
                searchPage.VerifyAndLoadSpecificSavedSearch("New Search", true, true);
                Thread.Sleep(2000);
            }

            bool adStatus = VerifySummaryTags(new string[] { "Running" }, true, true) || VerifySummaryTags(new string[] { "Breaking" }, true, true);
            bool datRange = VerifySummaryTags(new string[] { "Last Month" }, true, true) || VerifySummaryTags(new string[] { "Last 3 Months" }, true, true)
                 || VerifySummaryTags(new string[] { "Last 6 Months" }, true, true) || VerifySummaryTags(new string[] { "Year To Date" }, true, true)
                 || VerifySummaryTags(new string[] { "Last Year" }, true, true) || VerifySummaryTags(new string[] { "Last 14 Days" }, true, true)
                 || VerifySummaryTags(new string[] { "Last 7 Days" }, true, true) || VerifySummaryTags(new string[] { "Yesterday" }, true, true)
                 || VerifySummaryTags(new string[] { "Today" }, true, true) || driver._isElementPresent("xpath", "//div[@class='NU-tag-label' and contains(text(), '/')]");

            Assert.IsTrue(adStatus && datRange, "'Ad Status' or 'Date Range' or both Summary Tags are not present in default search");

            Results.WriteStatus(test, "Pass", "Verified, That Ad Status and Date Range Field Summary Tags Should be default for Default Search");
            return new SummaryTags(driver, test);
        }


    }
}
