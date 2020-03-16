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
    public class ViewAdPopup
    {
        #region Private Variables

        private IWebDriver viewAdPopup;
        private ExtentTest test;
        Carousels carousels;

        #endregion

        public ViewAdPopup(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.viewAdPopup = driver;
            test = testReturn;

            carousels = new Carousels(driver, test);
        }

        public IWebDriver driver
        {
            get { return this.viewAdPopup; }
            set { this.viewAdPopup = value; }
        }

        ///<summary>
        ///Click On Button of Results Card
        ///</summary>
        ///<param name="button">Button to be clicked</param>
        ///<param name="cardNo">Index of card</param>
        ///<returns></returns>
        public ViewAdPopup clickOnButtonOfResultsCard(string button, int cardNo = 1)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//div[" + cardNo + "]/div[@class='NU-card card p-0 NU-selectable-card']//button"), "'Card Buttons' not present on Carousel Cards");
            IList<IWebElement> cardButtonCollection = driver._findElements("xpath", "//div[" + cardNo + "]/div[@class='NU-card card p-0 NU-selectable-card']//button");

            bool avail = false;
            foreach (IWebElement cardButton in cardButtonCollection)
                if (cardButton.Text.ToLower().Contains(button.ToLower()))
                {
                    avail = true;
                    cardButton.Click();
                    break;
                }
            Assert.IsTrue(avail, "'" + button + "' not found on carousel");

            Results.WriteStatus(test, "Pass", "Clicked, '" + button + "' on Results Card");
            return new ViewAdPopup(driver, test);
        }

        ///<summary>
        ///Verify Downloads Functionality
        ///</summary>
        ///<param name="popupVisible">Popup should be visible or not</param>
        ///<returns></returns>
        public Carousels VerifyDownloadsFunctionality(bool popupVisible = true)
        {
            if (popupVisible)
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-content']"), "Creative Details Popup not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//h4"), "Creative Details Popup header not present");
                Assert.IsTrue(driver._getText("xpath", "//div[@class='modal-body pb-0']//h4").Contains("Creative Details for "), "Creative Details Popup header text does not match");

                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//img"), "Image not present");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-modal-footer//button[text()=' Download Asset ']"), "'Download Asset' button not present on Creative Details Popup");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-modal-footer//button[text()='Close']"), "'Close' button not present on Creative Details Popup");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//a[not(contains(@class, 'ng-hide'))]"), "'Tabs' not present on Creative Details Popup");
                IList<IWebElement> tabCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//a[not(contains(@class, 'ng-hide'))]");
                string[] tabNames = { "View Ad", "Download" };
                foreach (string tabName in tabNames)
                {
                    bool avail = false;
                    foreach (IWebElement tab in tabCollection)
                        if (tab.Text.ToLower().Contains(tabName.ToLower()))
                        {
                            avail = true;
                            break;
                        }
                    Assert.IsTrue(avail, "'" + tabName + "' not found on Creative Details Popup");
                }
            }
            else
                Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[@class='modal-content']"), "Creative Details Popup is still present");

            Results.WriteStatus(test, "Pass", "Verified, Downloads Functionality");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Click On Button of View Ad Popup
        ///</summary>
        ///<param name="button">Button to be clicked</param>
        ///<returns></returns>
        public ViewAdPopup clickOnButtonOfViewAdPopup(string button)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-modal-footer//button[text()=' Download Asset ']"), "'Download Asset' button not present on Creative Details Popup");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-modal-footer//button[text()='Close']"), "'Close' button not present on Creative Details Popup");

            if (button.ToLower().Equals("download asset"))
                driver._click("xpath", "//cft-domain-item-modal-footer//button[text()=' Download Asset ']");
            else if (button.ToLower().Equals("close"))
                driver._click("xpath", "//cft-domain-item-modal-footer//button[text()='Close']");
            if (button.ToLower().Equals("download grid"))
                driver._click("xpath", "//div[@class='modal-body pb-0']//button[@class='btn btn-link'][1]");
            if (button.ToLower().Equals("grid options"))
                driver._click("xpath", "//button//span[@class='pl-1' and text()='Grid Options']");
            if (button.ToLower().Equals("image"))
                driver._click("xpath", "//div[@class='modal-body pb-0']//img");

            Results.WriteStatus(test, "Pass", "Clicked, '" + button + "' on View Ad Popup");
            return new ViewAdPopup(driver, test);
        }

        ///<summary>
        ///Verify Occurrences Functionality
        ///</summary>
        ///<param name="popupVisible">Popup should be visible or not</param>
        ///<returns></returns>
        public ViewAdPopup VerifyOccurrencesFunctionality(bool popupVisible = true)
        {
            if (popupVisible)
            {
                carousels.clickButtonOnCarousel("Occurrences");
                string tabs = "View Ad,Occurrences,More Details";

                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-content']"), "Creative Details Popup not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//h4"), "Creative Details Popup header not present");
                Assert.IsTrue(driver._getText("xpath", "//div[@class='modal-body pb-0']//h4").Contains("Creative Details for "), "Creative Details Popup header text does not match");

                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//cft-table"), "Table not present");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div"), "Row headers not present in Table");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-body-container']/div"), "Row Values not present in Table");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-modal-footer//button[text()=' Download Asset ']"), "'Download Asset' button not present on Creative Details Popup");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-modal-footer//button[text()='Close']"), "'Close' button not present on Creative Details Popup");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//a[not(contains(@class, 'ng-hide'))]"), "'Tabs' not present on Creative Details Popup");
                IList<IWebElement> tabCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//a[not(contains(@class, 'ng-hide'))]");
                string[] tabNames = tabs.Split(',');
                foreach (string tabName in tabNames)
                {
                    bool avail = false;
                    foreach (IWebElement tab in tabCollection)
                        if (tab.Text.ToLower().Contains(tabName.ToLower()))
                        {
                            avail = true;
                            break;
                        }
                    Assert.IsTrue(avail, "'" + tabName + "' not found on Creative Details Popup");
                }
            }
            else
                Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[@class='modal-content']"), "Creative Details Popup is still present");

            Results.WriteStatus(test, "Pass", "Verified, Occurrences Functionality");
            return new ViewAdPopup(driver, test);
        }

        ///<summary>
        ///Click on Sorting Icon on Markets View
        ///</summary>
        ///<param name="ascending">Sorting order</param>
        ///<returns></returns>
        public string clickOnSortingIconOnMarketsView(bool ascending = false)
        {
            string[] columnArray = { "marketName", "mediaOutletName", "firstRunDate", "lastRunDate", "occurrences", "costEstimate" };
            Random rand = new Random();
            int x = rand.Next(0, columnArray.Length);
            string columnName = columnArray[x];
            string column = "";

            switch (columnName)
            {
                case "marketName":
                    column = "DMA";
                    break;
                case "mediaOutletName":
                    column = "Media Outlet"; 
                    break;
                case "firstRunDate":
                    column = "First Run Date"; 
                    break;
                case "lastRunDate":
                    column = "Last Run Date"; 
                    break;
                case "occurrences":
                    column = "Occurrences";
                    break;
                case "costEstimate":
                    column = "SPEND"; 
                    break;
                default:
                    break;
            }

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']"), "'" + column + "' column not present.");
            if(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortNone']"))
            {
                driver._click("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortNone']");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortDesc']"), "Descending Icon on '" + column + "' column not present.");
                if (ascending)
                {
                    driver._click("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortDesc']");
                    Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortAsc']"), "Descending Icon on '" + column + "' column not present.");
                    Results.WriteStatus(test, "Pass", "Clicked, to sort '" + column + "' column in ascending order.");
                }
                else
                    Results.WriteStatus(test, "Pass", "Clicked, to sort '" + column + "' column in descending order.");
            }
            else if(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortDesc']"))
            {
                if (ascending)
                {
                    driver._click("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortDesc']");
                    Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortAsc']"), "Descending Icon on '" + column + "' column not present.");
                    Results.WriteStatus(test, "Pass", "Clicked, to sort '" + column + "' column in ascending order.");
                }
                else
                    Results.WriteStatus(test, "Pass", "Verified, '" + column + "' column should already be in descending order.");
            }
            else if (driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortAsc']"))
            {
                if (!ascending)
                {
                    driver._click("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortAsc']");
                    Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortNone']"), "Unsorted Icon on '" + column + "' column not present.");
                    driver._click("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortNone']");
                    Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//span[@ref='eSortDesc']"), "Descending Icon on '" + column + "' column not present.");
                    Results.WriteStatus(test, "Pass", "Clicked, to sort '" + column + "' column in descending order.");
                }
                else
                    Results.WriteStatus(test, "Pass", "Verified, '" + column + "' column should already be in ascending order.");
            }
            return columnName;
        }

        ///<summary>
        ///Verify Sorting Functionality On Markets View
        ///</summary>
        ///<param name="ascending">Sorting order</param>
        ///<param name="columnName">Column to be verified</param>
        ///<returns></returns>
        public ViewAdPopup VerifySortingFunctionalityOnMarketsView(string columnName, bool ascending = false)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-body-container']//div[@col-id='" + columnName + "']"), "'" + columnName + "' column not present");
            IList<IWebElement> columnValuesColl = driver._findElements("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-body-container']//div[@col-id='" + columnName + "']");
            string[] columnValues = new string[columnValuesColl.Count];
            for (int i = 0; i < columnValuesColl.Count; i++)
                columnValues[i] = driver._getText("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-body-container']//div[@row-index='" + i + "']//div[@col-id='" + columnName + "']");
            string column = "";
            switch (columnName)
            {
                case "marketName":
                    column = "DMA";
                    break;
                case "mediaOutletName":
                    column = "Media Outlet";
                    break;
                case "firstRunDate":
                    column = "First Run Date";
                    break;
                case "lastRunDate":
                    column = "Last Run Date";
                    break;
                case "occurrences":
                    column = "Occurrences";
                    break;
                case "costEstimate":
                    column = "SPEND";
                    break;
                default:
                    break;
            }

            if (column.ToLower().Equals("occurrences") || column.ToLower().Equals("spend"))
            {
                decimal[] decValues = new decimal[columnValues.Length];
                for(int i = 0; i < columnValues.Length; i++)
                {
                    if (columnValues[i].Contains('$'))
                        columnValues[i] = columnValues[i].Substring(1);
                    Assert.IsTrue(Decimal.TryParse(columnValues[i], out decValues[i]), "'" + columnValues[i] + "' was not converted to decimal");
                }
                decimal[] unsortedValues = new decimal[decValues.Length];
                Array.Copy(decValues, unsortedValues, decValues.Length);
                Array.Sort(decValues);
                if (!ascending)
                    Array.Reverse(decValues);
                Assert.IsTrue(unsortedValues.SequenceEqual(decValues), "'" + columnName + "' was not sorted properly.");
            }
            else if(column.ToLower().Equals("first run date") || column.ToLower().Equals("last run date"))
            {
                DateTime[] dateValues = new DateTime[columnValues.Length];
                for (int i = 0; i < columnValues.Length; i++)
                    Assert.IsTrue(DateTime.TryParse(columnValues[i], out dateValues[i]), "'" + columnValues[i] + "' was not converted to decimal");
                DateTime[] unsortedValues = new DateTime[dateValues.Length];
                Array.Copy(dateValues, unsortedValues, dateValues.Length);
                Array.Sort(dateValues);
                if (!ascending)
                    Array.Reverse(dateValues);
                Assert.IsTrue(unsortedValues.SequenceEqual(dateValues), "'" + columnName + "' was not sorted properly.");
            }
            else
            {
                string[] unsortedValues = new string[columnValues.Length];
                Array.Copy(columnValues, unsortedValues, columnValues.Length);
                Array.Sort(columnValues);
                if (!ascending)
                    Array.Reverse(columnValues);
                Assert.IsTrue(unsortedValues.SequenceEqual(columnValues), "'" + columnName + "' was not sorted properly.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Sorting Functionality On Markets View for '" + columnName + "' column");
            return new ViewAdPopup(driver, test);
        }

        ///<summary>
        ///Enter Filter Value in Market View Table Columns
        ///</summary>
        ///<param name="column">Specific column to apply filter on</param>
        ///<param name="keyword">Keyword to be selected</param>
        ///<returns></returns>
        public string[] enterFilterValueInMarketViewTableColumns(string column, string keyword = "")
        {
            string[] columnArray = { "marketName", "mediaOutletName", "firstRunDate", "lastRunDate", "occurrences", "costEstimate" };
            string columnName = "";
            Random rand = new Random();
            int x = 0;

            if (column == "")
            {
                x = rand.Next(0, columnArray.Length);
                columnName = columnArray[x];

                switch (columnName)
                {
                    case "marketName":
                        column = "DMA";
                        break;
                    case "mediaOutletName":
                        column = "Media Outlet";
                        break;
                    case "firstRunDate":
                        column = "First Run Date";
                        break;
                    case "lastRunDate":
                        column = "Last Run Date";
                        break;
                    case "occurrences":
                        column = "Occurrences";
                        break;
                    case "costEstimate":
                        column = "SPEND";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (column.ToLower())
                {
                    case "DMA":
                        columnName = "marketName";
                        break;
                    case "media outlet":
                        columnName = "mediaOutletName";
                        break;
                    case "first run date":
                        columnName = "firstRunDate";
                        break;
                    case "last run date":
                        columnName = "lastRunDate";
                        break;
                    case "occurrences":
                        columnName = "occurrences";
                        break;
                    case "spend":
                        columnName = "costEstimate";
                        break;
                    default:
                        break;
                }
            }

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']"), "'" + column + "' column not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-body-container']//div[@col-id='" + columnName + "']"), "'" + column + "' column values not present.");
            IList<IWebElement> filterOptionsColl = driver._findElements("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-body-container']//div[@col-id='" + columnName + "']");
            int originalCount = filterOptionsColl.Count;
            rand = new Random();
            x = rand.Next(0, filterOptionsColl.Count);
            string filterValue = filterOptionsColl[x].Text;

            driver.MouseHoverUsingElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//i[contains(@class,'NU-icon-filters-middleweight')]"), "Filter icon on '" + column + "' column not present.");
            driver._click("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//i[contains(@class,'NU-icon-filters-middleweight')]");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']"), "Filter section on '" + column + "' column not present.");

            if (column.ToLower().Equals("spend"))
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//input[@id='filterText']"), "'Filter text Field' not present on filter section.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//select"), "'Select Keyword Field' not present on filter section.");
                if (!keyword.Equals(""))
                {
                    driver._click("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//select");
                    Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//select/option"), "'Select Keyword DDL' not present on filter section.");
                    IList<IWebElement> keywordDDLColl = driver._findElements("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//select/option");
                    bool avail = false;
                    foreach (IWebElement keywordDDL in keywordDDLColl)
                        if (keywordDDL.Text.ToLower().Equals(keyword.ToLower()))
                        {
                            avail = true;
                            keywordDDL.Click();
                            break;
                        }
                    Assert.IsTrue(avail, "'" + keyword + "' not found in Keyword DDL");
                }
                filterValue = filterValue.Substring(1);
                string filterTovalue = "";
                if (!keyword.ToLower().Equals("equals") && !keyword.ToLower().Equals("not equal"))
                    filterValue = "500";
                if (keyword.ToLower().Equals("in range"))
                    filterTovalue = "700";

                driver._type("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//input[@id='filterText']", "$" + filterValue);
                if(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//input[@id='filterToText']"))
                    driver._type("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//input[@id='filterToText']", filterTovalue);
            }
            else
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//div[@class='ag-virtual-list-item']"), "'Filter Options' not present on filter section.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//div[@id='selectAll']"), "'Select All Option' not present on filter section.");
                driver._click("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//div[@id='selectAll']");
                Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//div[@class='ag-filter-checkbox']//*[name()='svg' and @data-icon='check-square']"), "All checkboxes did not get deselected");
                Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-body-container']/div"), "All rows did not get hidden");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//input[@placeholder='Search...']"), "'Search text Field' not present on filter section.");
                driver._type("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//input[@placeholder='Search...']", filterValue);
                Thread.Sleep(500);
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//div[@class='ag-filter-checkbox']"), "Checkboxes not present in Filter section");
                IList<IWebElement> checkboxCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//div[@class='ag-filter-checkbox']");
                checkboxCollection[1].Click();
            }

            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.XPath("//div[@class='modal-body pb-0']//h4"))).Perform();
            action.Click().Perform();

            string[] columnAndValue = { column, filterValue, keyword, originalCount.ToString() };

            Results.WriteStatus(test, "Pass", "Entered, Filter Value '" + filterValue + "' in Market View Table Column '" + column + "'");
            return columnAndValue;
        }

        ///<summary>
        ///Verify Filter On Market Views Table Column
        ///</summary>
        ///<returns></returns>
        public ViewAdPopup VerifyFilterOnMarketViewsTableColumn(string[] columnAndValue)
        {
            string column = columnAndValue[0], filterValue = columnAndValue[1], keyword = columnAndValue[2], columnName = "";
            int originalCount = 0, ifilterValue = 0;
            while (filterValue.Contains(","))
                filterValue = filterValue.Remove(filterValue.IndexOf(","), 1);
            Assert.IsTrue(Int32.TryParse(columnAndValue[3], out originalCount), "'" + columnAndValue[3] + "' was not converted to int");
            Assert.IsTrue(Int32.TryParse(filterValue, out ifilterValue), "'" + filterValue + "' was not converted to int");

            switch (column.ToLower())
            {
                case "DMA":
                    columnName = "marketName";
                    break;
                case "media outlet":
                    columnName = "mediaOutletName";
                    break;
                case "first run date":
                    columnName = "firstRunDate";
                    break;
                case "last run date":
                    columnName = "lastRunDate";
                    break;
                case "occurrences":
                    columnName = "occurrences";
                    break;
                case "spend":
                    columnName = "costEstimate";
                    break;
                default:
                    break;
            }

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']//div[@col-id='" + columnName + "']//div[contains(@class, 'filter-applied-icon')]"), "Filter is not applied on '" + column + "' column");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-body-container']//div[@col-id='" + columnName + "']"), "'" + column + "' column values not present.");
            IList<IWebElement> filterOptionsColl = driver._findElements("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-body-container']//div[@col-id='" + columnName + "']");

            if (column.ToLower().Equals("spend"))
            {
                decimal[] decValues = new decimal[filterOptionsColl.Count];
                for(int i = 0; i < filterOptionsColl.Count; i++)
                {
                    string temp = filterOptionsColl[i].Text.Substring(1);
                    Assert.IsTrue(Decimal.TryParse(temp, out decValues[i]), "'" + temp + "' was not converted to decimal");
                }
                for (int i = 0; i < decValues.Length; i++)
                {
                    if (keyword.ToLower().Equals("equals"))
                        Assert.AreEqual(ifilterValue, decValues[i], "'" + filterOptionsColl[i].Text + "' does not satisfy filter condition");
                    if (keyword.ToLower().Equals("not equal"))
                        Assert.AreNotEqual(ifilterValue, decValues[i], "'" + filterOptionsColl[i].Text + "' does not satisfy filter condition");
                    if (keyword.ToLower().Equals("less than"))
                        Assert.Greater(ifilterValue, decValues[i], "'" + filterOptionsColl[i].Text + "' does not satisfy filter condition");
                    if (keyword.ToLower().Equals("less than or equals"))
                        Assert.GreaterOrEqual(ifilterValue, decValues[i], "'" + filterOptionsColl[i].Text + "' does not satisfy filter condition");
                    if (keyword.ToLower().Equals("greater than"))
                        Assert.Less(ifilterValue, decValues[i], "'" + filterOptionsColl[i].Text + "' does not satisfy filter condition");
                    if (keyword.ToLower().Equals("greater than or equals"))
                        Assert.LessOrEqual(ifilterValue, decValues[i], "'" + filterOptionsColl[i].Text + "' does not satisfy filter condition");
                    if (keyword.ToLower().Equals("in range"))
                    {
                        Assert.LessOrEqual(500, decValues[i], "'" + filterOptionsColl[i].Text + "' does not satisfy filter condition");
                        Assert.GreaterOrEqual(700, decValues[i], "'" + filterOptionsColl[i].Text + "' does not satisfy filter condition");
                    }
                }

                driver.MouseHoverUsingElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//i[contains(@class,'NU-icon-filters-middleweight')]"), "Filter icon on '" + column + "' column not present.");
                driver._click("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//i[contains(@class,'NU-icon-filters-middleweight')]");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']"), "Filter section on '" + column + "' column not present.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//input[@id='filterText']"), "'Filter text Field' not present on filter section.");
                string value = driver._getValue("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//input[@id='filterText']");
                //driver._doubleClick("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//input[@id='filterText']");
                foreach (char dig in value.ToCharArray())
                    driver._findElement("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//input[@id='filterText']").SendKeys(Keys.Backspace);
            }
            else
            {
                foreach (IWebElement filterOption in filterOptionsColl)
                    Assert.AreEqual(filterValue.ToLower(), filterOption.Text.ToLower(), "'" + filterOption.Text + "' does not satisfy filter condition.");

                driver.MouseHoverUsingElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//i[contains(@class,'NU-icon-filters-middleweight')]"), "Filter icon on '" + column + "' column not present.");
                driver._click("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']/div[@col-id='" + columnName + "']//i[contains(@class,'NU-icon-filters-middleweight')]");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']"), "Filter section on '" + column + "' column not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//div[@id='selectAll']"), "'Select All Option' not present on filter section.");
                driver._click("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@class='ag-menu']//div[@id='selectAll']");
            }

            Thread.Sleep(500);
            filterOptionsColl = driver._findElements("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-body-container']//div[@col-id='" + columnName + "']");
            Assert.AreEqual(originalCount, filterOptionsColl.Count, "Filter was not removed from '" + column + "' column.");

            Results.WriteStatus(test, "Pass", "Verified, Filter On Market Views Table Column '" + column + "' for value '" + filterValue + "'.");
            return new ViewAdPopup(driver, test);
        }

        ///<summary>
        ///Capture Data From Markets View Grid
        ///</summary>
        ///<returns></returns>
        public string[] captureDataFromMarketsViewGrid()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']//div[@col-id]"), "Column headers not present in Markets View Grid");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-body-container']//div[@col-id]"), "Rows not present in Markets View Grid");
            IList<IWebElement> headerCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']//div[@col-id]");
            IList<IWebElement> rowValuesCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-body-container']//div[@col-id]");
            string[,] dataGrid = new string[rowValuesCollection.Count / headerCollection.Count + 1, headerCollection.Count];

            for (int i = 0; i < headerCollection.Count; i++)
                dataGrid[0, i] = headerCollection[i].Text;

            for (int i = 1, k = 0; i < dataGrid.GetLength(0); i++)
                for (int j = 0; j < dataGrid.GetLength(1); j++)
                {
                    dataGrid[i, j] = rowValuesCollection[k].Text;
                    ++k;
                }

            for(int i = 1; i < dataGrid.GetLength(0); i++)
            {
                int j = dataGrid.GetLength(1) - 1;
                string temp = dataGrid[i, j];
                while(j > 0)
                    dataGrid[i, j] = dataGrid[i, --j];
                dataGrid[i, 0] = temp;
            }

            string[] dataArray = new string[dataGrid.GetLength(0)];
            for (int i = 0; i < dataGrid.GetLength(0); i++)
            {
                dataArray[i] = "\"" + dataGrid[i, 0] + "\"";
                for (int j = 1; j < dataGrid.GetLength(1); j++)
                {
                    if (dataGrid[i, j].Contains("$"))
                    {
                        dataGrid[i, j] = dataGrid[i, j].Substring(1);
                        int index = dataGrid[i, j].IndexOf(",");
                        if (index > -1)
                            dataGrid[i, j] = dataGrid[i, j].Remove(index, 1);
                    }
                    dataArray[i] = dataArray[i] + ",\"" + dataGrid[i, j] + "\"";
                }
            }

            Results.WriteStatus(test, "Pass", "Captured, Data From Markets View Grid");
            return dataArray;
        }

        ///<summary>
        ///Verify Data in Exported file from Markets View Grid
        ///</summary>
        ///<param name="fileName">File to be verified</param>
        ///<param name="dataGrid">Detail from Carousels</param>
        ///<param name="screen">Screen from where data is exported</param>
        ///<returns></returns>
        public ViewAdPopup VerifyDataInExportedFileFromMarketsViewGrid(string fileName, string adCode, string[] dataGrid)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            int rw = 0;
            int cl = 0;
            string FilePath = "";

            string sourceDir = ExtentManager.ResultsDir + "\\";
            string[] fileEntries = Directory.GetFiles(sourceDir);

            foreach (string fileEntry in fileEntries)
            {
                if (fileEntry.Contains(fileName))
                {
                    FilePath = fileEntry;
                    break;
                }
            }

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(FilePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int num = xlWorkBook.Sheets.Count;
            for (int s = 1; s <= num; s++)
            {
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(s);
                if (xlWorkSheet.Name.Contains("export"))
                    break;
            }

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;

            Assert.IsTrue((range.Cells[1, 1] as Excel.Range).Text.Contains("Market Details for " + adCode), "Heading Text of Sheet does not match");
            for(int i = 0; i < dataGrid.GetLength(0); i++)
            {
                bool avail = false;
                for (int rCnt = 4; rCnt < dataGrid.GetLength(0) + 4; rCnt++)
                    if ((range.Cells[rCnt, 1] as Excel.Range).Text.ToLower().Contains(dataGrid[i].ToLower())){
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "Row " + (i + 1) + " not found in CSV file.");
            }

            DateTime date = DateTime.Today;
            string sDate = date.ToString("MMMM dd, yyyy");
            int index = sDate.IndexOf(',');
            if(sDate.ToCharArray()[index-1]=='1')
                sDate = sDate.Insert(index, "st");
            else if (sDate.ToCharArray()[index - 1] == '2')
                sDate = sDate.Insert(index, "nd");
            else
                sDate = sDate.Insert(index, "th");
            sDate = sDate.Remove(index + 2, 1);
            index = sDate.IndexOf(' ') + 1;
            if (sDate.ToCharArray()[index] == '0')
                sDate = sDate.Remove(index, 1);
            Assert.IsTrue((range.Cells[dataGrid.Length + 5, 1] as Excel.Range).Text.Contains("Exported on " + sDate), "Footer Text of Sheet does not match");
            Assert.IsTrue((range.Cells[dataGrid.Length + 5, 1] as Excel.Range).Text.Contains(" from Competitrack.com"), "Footer Text of Sheet does not match");

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            File.Delete(FilePath);

            Results.WriteStatus(test, "Pass", "Verified, Data in Exported File from Markets View Grid");
            return new ViewAdPopup(driver, test);
        }

        ///<summary>
        ///Verify Grid Options on Market View
        ///</summary>
        ///<returns></returns>
        public ViewAdPopup VerifyGridOptionsOnMarketView()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']//div[@col-id]"), "Column headers not present in Markets View Grid");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-column-container']/div"), "Column headers not present in Markets View Grid Options");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-column-container']//span[@ref='cbSelect']"), "Checkboxes not present in Markets View Grid Options");
            IList<IWebElement> gridColumnHeaderColl = driver._findElements("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']//div[@col-id]");
            string gridColumnHeaderNames = "";
            IList<IWebElement> gridOptionCheckboxColl = driver._findElements("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-column-container']//span[@class='ag-checkbox-checked']");
            string gridOptionColumnNames = "";
            for (int i = 0; i < gridColumnHeaderColl.Count; i++)
                gridColumnHeaderNames = gridColumnHeaderNames + gridColumnHeaderColl[i].Text + ", ";
            for (int i = 0; i < gridOptionCheckboxColl.Count; i++)
                gridOptionColumnNames = gridOptionColumnNames + driver._getText("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']//div[@col-id][" + (i + 1) + "]") + ", ";

            gridColumnHeaderNames = gridColumnHeaderNames.Substring(0, gridColumnHeaderNames.Length - 2);
            gridOptionColumnNames = gridOptionColumnNames.Substring(0, gridOptionColumnNames.Length - 2);

            Assert.AreEqual(gridColumnHeaderNames,gridOptionColumnNames, "Columns in Grid and in Grid options do not match.");

            Random rand = new Random();
            int x = rand.Next(0, gridOptionCheckboxColl.Count);
            gridOptionCheckboxColl[x].Click();
            Thread.Sleep(500);

            gridColumnHeaderNames = "";
            gridOptionColumnNames = "";
            gridColumnHeaderColl = driver._findElements("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']//div[@col-id]");
            for (int i = 0; i < gridColumnHeaderColl.Count; i++)
                gridColumnHeaderNames = gridColumnHeaderNames + gridColumnHeaderColl[i].Text + ", ";
            gridOptionCheckboxColl = driver._findElements("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-column-container']//span[@ref='cbSelect']");
            for (int i = 0; i < gridOptionCheckboxColl.Count; i++)
            {
                IList<IWebElement> checkboxColl = gridOptionCheckboxColl[i]._findElementsWithinElement("xpath", ".//span[@class='ag-checkbox-checked']");
                if(checkboxColl.Count > 0)
                    gridOptionColumnNames = gridOptionColumnNames + driver._getText("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-column-container']/div[" + (i + 1) + "]//span[@ref='eLabel']") + ", ";
            }

            Assert.AreEqual(gridColumnHeaderNames.ToLower(), gridOptionColumnNames.ToLower(), "Columns in Grid and in Grid options do not match.");

            Results.WriteStatus(test, "Pass", "Verified, Grid Options on Market View");
            return new ViewAdPopup(driver, test);
        }

        ///<summary>
        ///Select tab On View Ad Popup
        ///</summary>
        ///<param name="tabName">tab to be selected</param>
        ///<returns></returns>
        public ViewAdPopup selectTabOnViewAdPopup(string tabName)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//a[not(contains(@class, 'ng-hide'))]"), "'Tabs' not present on Creative Details Popup");
            IList<IWebElement> tabCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//a[not(contains(@class, 'ng-hide'))]");
            bool avail = false;
            foreach (IWebElement tab in tabCollection)
                if (tab.Text.ToLower().Contains(tabName.ToLower()))
                {
                    avail = true;
                    tab.Click();
                    break;
                }

            Assert.IsTrue(avail, "'" + tabName + "' not found on Creative Details Popup");
            Results.WriteStatus(test, "Pass", "Selected, '" + tabName + "' on View Ad Popup");
            return new ViewAdPopup(driver, test);
        }

        ///<summary>
        ///Click To Download Files From Downloads Tab On View Ad Popup
        ///</summary>
        ///<returns></returns>
        public string[,] clickToDownloadFilesFromDownloadsTabOnViewAsPopup()
        {
            Assert.IsTrue(driver._getText("xpath", "//div[@class='modal-body pb-0']//h4").Contains("Creative Details for "), "Creative Details Popup header text does not match");
            string adCode = driver._getText("xpath", "//div[@class='modal-body pb-0']//h4");
            int index = adCode.IndexOf("for") + 4;
            adCode = adCode.Substring(index);
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//iframe"), "Download Files frame not present");
            driver.SwitchTo().Frame("iframe");
            Assert.IsTrue(driver._isElementPresent("xpath", "//table"), "Download Files Table not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//table//td/a"), "Download Files Links not present");
            IList<IWebElement> linksCollection = driver._findElements("xpath", "//table//td/a");
            string[,] fileTypeColl = new string[linksCollection.Count, 2];

            for (int i = 0; i < linksCollection.Count; i++)
            {
                int index1 = linksCollection[i].Text.IndexOf('(');
                int index2 = linksCollection[i].Text.IndexOf(')');
                int length = index2 - index1 - 1;
                fileTypeColl[i,1] = linksCollection[i].Text.Substring(index1 + 1, length).ToLower();
                if (fileTypeColl[i, 1].Equals("quicktime"))
                    fileTypeColl[i, 1] = "mov";

                fileTypeColl[i, 0] = adCode + "-" + linksCollection[i].Text.Substring(0, index1 - 1);
                fileTypeColl[i, 0] = fileTypeColl[i, 0].Replace(' ', '_');
                Console.WriteLine(fileTypeColl[i, 0] + "." + fileTypeColl[i, 1]);
                linksCollection[i].Click();

                Assert.IsTrue(driver._waitForElement("xpath", "//body//div[text()='Thank you for your order.']"), "'Thank You' text not present");
                Assert.IsTrue(driver._waitForElement("xpath", "//body//a[text()=' Go Back ']"), "'Go Back' button not present");
                driver._click("xpath", "//body//a[text()=' Go Back ']");
                linksCollection = driver._findElements("xpath", "//table//td/a");

                Thread.Sleep(500);
            }

            driver.SwitchTo().DefaultContent();

            Results.WriteStatus(test, "Pass", "Clicked, To Download Files From Downloads Tab On View Ad Popup");
            return fileTypeColl;
        }

    }
}
