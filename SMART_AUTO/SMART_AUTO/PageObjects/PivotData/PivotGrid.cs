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
    public class PivotGrid
    {
        #region Private Variables

        private IWebDriver pivotGrid;
        private ExtentTest test;
        Home homePage;
        Charts charts;
        ViewAdPopup viewAdPopup;
        Carousels carousels;

        #endregion

        public PivotGrid(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.pivotGrid = driver;
            test = testReturn;

            homePage = new Home(driver, test);
            charts = new Charts(driver, test);
            viewAdPopup = new ViewAdPopup(driver, test);
            carousels = new Carousels(driver, test);
        }

        public IWebDriver driver
        {
            get { return this.pivotGrid; }
            set { this.pivotGrid = value; }
        }

        ///<summary>
        ///Verify Pivot Options Popup
        ///</summary>
        ///<param name="popupVisible">Whether popup should be visible or not</param>
        ///<returns></returns>
        public PivotGrid VerifyPivotOptionsPopup(bool popupVisible = true, bool fromClick = true)
        {
            if (popupVisible)
            {
                if (fromClick)
                {
                    driver._scrollintoViewElement("xpath", "//cft-pivot-options//button");
                    driver._click("xpath", "//cft-pivot-options//button");
                }
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-content']"), "'Pivot Options' popup not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-header']//h4"), "'Pivot Options' popup header not present");
                Assert.AreEqual("Set Your Pivot Options ...", driver._getText("xpath", "//div[@class='modal-header']//h4"), "'Pivot Options' popup header text does not match.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']"), "'Pivot Fields' not present in Pivot Options popup");
                Assert.AreEqual("Pivot Fields", driver._getText("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[text()]"), "'Pivot Fields' header text does not match.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//label/span"), "'Pivot Fields' options not present in Pivot Options popup");
                IList<IWebElement> pivotFieldsColl = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//label/span");
                //string[] pivotFieldOptions = { "Category", "Class", "Company", "Division", "Brand", "Media Outlet", "Submedia", "Media Year", "Media Quarter", "Media Month", "Media Week", "Media Day" };

                //foreach (string option in pivotFieldOptions)
                //{
                //    bool avail = false;
                //    foreach (IWebElement field in pivotFieldsColl)
                //        if (field.Text.ToLower().Contains(option.ToLower()))
                //        {
                //            avail = true;
                //            if (option.Equals("Class") || option.Equals("Company") || option.Equals("Division"))
                //                Assert.IsTrue(field.GetCssValue("background") != null, "'" + option + "' not selected by default");
                //            break;
                //        }
                //    Assert.IsTrue(avail, "'" + option + "' is not present in 'Pivot Fields'");
                //}

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col px-1']"), "'Metrics' field not present in Pivot Options popup");
                Assert.AreEqual("Metrics", driver._getText("xpath", "//div[@class='modal-body pt-3']//div[@class='col px-1']//div[text()]"), "'Metrics' Field's header text does not match.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col px-1']//label/span"), "'Metrics' field's options not present in Pivot Options popup");
                //Assert.IsTrue(driver._findElement("xpath", "//div[@class='modal-body pt-3']//div[@class='col px-1']//label/span[text()='Total Spend Current Period (CP)']").GetCssValue("background") != null, "'Total Spend Current Period (CP)' not selected by default");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[1]/div[text()]"), "'Formatting' field not present in Pivot Options popup");
                Assert.AreEqual("Formatting", driver._getText("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[1]/div[text()]"), "'Formatting' Field's header text does not match.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//label/span"), "'Formatting and Other Options' fields' options not present in Pivot Options popup");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//label/span[text()=' Spend in Dollars ($)']"), "'Spend in Dollars ($)' option not present in Formatting Field");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//label/span[text()=' Spend in Thousands $(000)']"), "'Spend in Thousands $(000)' option not present in Formatting Field");
                Assert.IsTrue(driver._findElement("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//label/span[text()=' Spend in Dollars ($)']").GetCssValue("background") != null, "'Spend in Dollars ($)' not selected by default");


                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[2]/div[text()]"), "'Other Options' field not present in Pivot Options popup");
                Assert.AreEqual("Other Options", driver._getText("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[2]/div[text()]"), "'Other Options' Field's header text does not match.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//label/span[text()='Show Summary Totals']"), "'Spend in Dollars ($)' option not present in Other Options Field");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//label/span[text()='Rank on']"), "'Spend in Thousands $(000)' option not present in Other Options Field");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button"), "Buttons not present on Pivot Options Popup footer");
                IList<IWebElement> buttonCollection = driver._findElements("xpath", "//div[@class='modal-footer pt-0']//button");
                string[] buttonNameList = { "Cancel", "Reset", "Apply" };
                foreach (string buttonName in buttonNameList)
                {
                    bool avail = false;
                    foreach (IWebElement button in buttonCollection)
                        if (button.Text.ToLower().Contains(buttonName.ToLower()))
                        {
                            avail = true;
                            break;
                        }
                    Assert.IsTrue(avail, "'" + buttonName + "' not found in Pivot Options Footer buttons.");
                }
            }
            else
                Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[@class='modal-content']"), "'Pivot Options' popup is present");

            Results.WriteStatus(test, "Pass", "Verified, Pivot Options popup");
            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Select/Deselect Option from Pivot Options Popup
        ///</summary>
        ///<param name="option">Option to be selected/deselected</param>
        ///<param name="select">To select or deselect given option</param>
        ///<returns></returns>
        public string select_deselectOptionFromPivotOptionsPopup(string option, bool select = true)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//label/span"), "'Pivot Fields' options not present in Pivot Options popup");
            IList<IWebElement> pivotOptionsColl = driver._findElements("xpath", "//div[@class='modal-body pt-3']//label/span");

            if (option.Equals("Random"))
            {
                Random rand = new Random();
                int x = rand.Next(0, pivotOptionsColl.Count);
                option = pivotOptionsColl[x].Text;
            }

            bool avail = false;
            foreach (IWebElement pivotOption in pivotOptionsColl)
                if (pivotOption.Text.ToLower().Equals(option.ToLower()))
                {
                    avail = true;
                    driver._scrollintoViewElement("xpath", "//div[@class='modal-body pt-3']//label/span[contains(text(), '" + option + "')]");

                    if (pivotOption.GetCssValue("background").Contains("rgba(0, 0, 0, 0)"))
                    {
                        if (!select)
                            Results.WriteStatus(test, "Pass", "'" + option + "' is already deselected.");
                        else
                        {
                            pivotOption.Click();
                            Thread.Sleep(3000);
                            Results.WriteStatus(test, "Pass", "Clicked, on '" + option + "' successfully.");
                        }
                    }
                    else
                    {
                        if (select)
                            Results.WriteStatus(test, "Pass", "'" + option + "' is already selected.");
                        else
                        {
                            pivotOption.Click();
                            Thread.Sleep(3000);
                            Results.WriteStatus(test, "Pass", "Clicked, on '" + option + "' successfully.");
                        }
                    }
                    break;
                }
            Assert.IsTrue(avail, "'" + option + "' not found in Pivot Options.");

            return option;
        }

        ///<summary>
        ///Click Button on Pivot Options Popup
        ///</summary>
        ///<param name="buttonName">Button to be clicked</param>
        ///<returns></returns>
        public PivotGrid clickButtonOnPivotOptionsPopup(string buttonName)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button"), "Buttons not present on Pivot Options Popup footer");
            IList<IWebElement> buttonCollection = driver._findElements("xpath", "//div[@class='modal-footer pt-0']//button");

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
            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Read Selected Pivot Options
        ///</summary>
        ///<returns></returns>
        public string[] readSelectedPivotOptions()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//label/span"), "'Pivot Fields' options not present in Pivot Options popup");
            IList<IWebElement> pivotFieldsColl = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//label/span");

            string selected = "";
            foreach(IWebElement pivotOption in pivotFieldsColl)
                if(!pivotOption.GetCssValue("background").Contains("rgba(0, 0, 0, 0)"))
                    selected = selected + pivotOption.Text + ";";

            string[] selectedOption = selected.Substring(0, selected.Length-1).Split(';');

            Results.WriteStatus(test, "Pass", "Read, Selected Pivot Options");
            return selectedOption;
        }

        ///<summary>
        ///Read Selected Metrics Options
        ///</summary>
        ///<returns></returns>
        public string[] readSelectedMetricsOptions()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col px-1']//label/span"), "'Pivot Fields' options not present in Pivot Options popup");
            IList<IWebElement> pivotFieldsColl = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col px-1']//label/span");

            string selected = "";
            foreach (IWebElement pivotOption in pivotFieldsColl)
                if (!pivotOption.GetCssValue("background").Contains("rgba(0, 0, 0, 0)"))
                    selected = selected + pivotOption.Text + ";";

            string[] selectedOption = selected.Substring(0, selected.Length - 1).Split(';');

            Results.WriteStatus(test, "Pass", "Read, Selected Metrics Options");
            return selectedOption;
        }

        ///<summary>
        ///Read Column Names from Pivot Grid
        ///</summary>
        ///<returns></returns>
        public string[] readColumnNamesFromPivotGrid()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//ag-grid-angular//div[@class='ag-header-cell ag-header-cell-sortable']//span[@ref='eText']"), "'Header Columns' not present in Pivot Grid");
            IList<IWebElement> columnNamesColl = driver._findElements("xpath", "//cft-pivot-table//ag-grid-angular//div[@class='ag-header-cell ag-header-cell-sortable']//span[@ref='eText']");

            string selected = "";
            foreach (IWebElement columnName in columnNamesColl)
                if (!selected.Contains(columnName.Text))
                    selected = selected + columnName.Text + ";";

            string[] selectedOption = selected.Substring(0, selected.Length - 1).Split(';');

            Results.WriteStatus(test, "Pass", "Read, Column Names from Pivot Grid");
            return selectedOption;
        }

        ///<summary>
        ///Verify Columns in Pivot Grid
        ///</summary>
        ///<param name="columnNames">Column Names from Pivot Grid</param>
        ///<param name="pivotFieldsList">Field Names from Pivot Options</param>
        ///<param name="present">Should be present or not</param>
        ///<returns></returns>
        public PivotGrid VerifyColumnsInPivotGrid(string[] pivotFieldsList, string[] columnNames, bool present = true)
        {
            if (driver._waitForElement("xpath", "//cft-pivot-table//ag-grid-angular//div[@class='ag-header-cell ag-header-cell-sortable']//span[@ref='eText']"))
            {
                foreach(string pivotField in pivotFieldsList)
                {
                    bool avail = false;
                    foreach(string column in columnNames)
                        if (column.ToLower().Equals(pivotField.ToLower()))
                        {
                            avail = true;
                            break;
                        }
                    if (present)
                        Assert.IsTrue(avail, "'" + pivotField + "' not present in Pivot Grid.");
                    else
                        Assert.IsFalse(avail, "'" + pivotField + "' not present in Pivot Grid.");
                }

                Results.WriteStatus(test, "Pass", "Verified, Displayed column names match selected pivot fields.");
            }
            else
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//cft-table//span[contains(text(), 'Error loading pivot data. At least one field must be visible to pivot.')]"), "'No rows present' error message not present.");
                Results.WriteStatus(test, "Pass", "Verified, 'No rows present' error message present.");
            }

            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Capture Spend Value in Total Column
        ///</summary>
        ///<returns></returns>
        public decimal[] captureSpendValueInTotalColumn()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-body-container']//div[@col-id='undefined']/span"), "Spend Values in Total Column not present.");
            IList<IWebElement> spendValuesCollection = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-container']//div[@col-id='undefined']/span");

            decimal[] decSpendValuesList = new decimal[10];
            for(int i = 0; i < 10; i++)
            {
                string temp = spendValuesCollection[i].Text;
                if (temp.Contains("$"))
                    temp = temp.Substring(1);
                string[] tempArr = temp.Split(',');
                temp = "";
                for (int j = 0; j < tempArr.Length; j++)
                    temp = temp + tempArr[j];
                if (!temp.Equals(""))
                    Assert.IsTrue(decimal.TryParse(temp, out decSpendValuesList[i]), "Could not convert '" + spendValuesCollection[i].Text + "' to decimal");
                else
                    decSpendValuesList[i] = 0;
            }

            Results.WriteStatus(test, "Pass", "Captured, Spend Value in Total Column");
            return decSpendValuesList;
        }

        ///<summary>
        ///Verify That Spend in Dollars or Thousands is Applied
        ///</summary>
        ///<param name="decValuesList1">First List of Decimal Values</param>
        ///<param name="decValuesList2">Second List of Decimal Values</param>
        ///<param name="thousands">Whether Spend in Thousands Criteria is applied</param>
        ///<returns></returns>
        public PivotGrid VerifyThatSpendInDollarsOrThousandsIsApplied(decimal[] decValuesList1, decimal[] decValuesList2, bool thousands = true)
        {
            if (thousands)
            {
                for (int i = 0; i < decValuesList1.Length; i++)
                    Assert.AreEqual(Math.Round(decValuesList1[i] / 1000, 1), Math.Round(decValuesList2[i], 1), "'" + decValuesList1 + "' and '" + decValuesList2 + "' do not match");
                Results.WriteStatus(test, "Pass", "Verified, Spend in Thousands is Applied");
            }
            else
            {
                for (int i = 0; i < decValuesList1.Length; i++)
                    Assert.AreEqual(decValuesList1[i] * 1000, decValuesList2[i], "'" + decValuesList1 + "' and '" + decValuesList2 + "' do not match");
                Results.WriteStatus(test, "Pass", "Verified, Spend in Dollars is Applied");
            }

            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Verify Summary Total Row in Pivot Grid
        ///</summary>
        ///<param name="columnNames">Collection of Column Names to be verified in Summary Total Row</param>
        ///<returns></returns>
        public PivotGrid VerifySummaryTotalRowInPivotGrid(string[] columnNames)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//div[@row-index='b-0']"), "'Verify Summary Total' Row not present in Pivot Grid");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@row-index='b-0']/div/span"), "'Columns' in Verify Summary Total Row of Pivot Grid not present.");
            IList<IWebElement> summaryRowColumnColl = driver._findElements("xpath", "//cft-pivot-table//div[@row-index='b-0']/div/span");

            foreach (string columnName in columnNames)
            {
                bool avail = false;
                foreach(IWebElement column in summaryRowColumnColl)
                {
                    if (columnName.ToLower().Equals("company"))
                    {
                        if (column.Text.ToLower().Contains("companies"))
                        {
                            avail = true;
                            break;
                        }
                    }
                    else if (columnName.ToLower().Contains("spend"))
                    {
                        if (column.Text.Contains("$"))
                        {
                            avail = true;
                            break;
                        }
                    }
                    else if(column.Text.ToLower().Contains(columnName.ToLower()))
                            {
                                avail = true;
                                break;
                            }
                }
                Assert.IsTrue(avail, "Column for '" + columnName + "' not found in Verify Summary Total Row.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Spend in Dollars is Applied");
            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Verify Sorting Functionality of Pivot Grid
        ///</summary>
        ///<returns></returns>
        public PivotGrid VerifySortingFunctionalityOfPivotGrid()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-header-cell-label']//span[@ref='eSortNone']"), "'Unsorted' icons not present on Pivot Grid.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@col-id='brand-n.occurrence/categoryTier2Code']//span[@ref='eSortNone']"), "'Unsorted' icon not present on 'Class' column of Pivot Grid.");

            driver._click("xpath", "//cft-pivot-table//div[@col-id='brand-n.occurrence/categoryTier2Code']//span[@ref='eSortNone']");
            Thread.Sleep(1000);
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//div[@col-id='brand-n.occurrence/categoryTier2Code']//span[@ref='eSortDesc']"), "'Sorted in Descending Order' icon not present on 'Class' column of Pivot Grid.");

            driver._click("xpath", "//cft-pivot-table//div[@col-id='brand-n.occurrence/categoryTier2Code']//span[@ref='eSortDesc']");
            Thread.Sleep(1000);
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//div[@col-id='brand-n.occurrence/categoryTier2Code']//span[@ref='eSortAsc']"), "'Sorted in Ascending Order' icon not present on 'Class' column of Pivot Grid.");

            Results.WriteStatus(test, "Pass", "Verified, Sorting Functionality of Pivot Grid");
            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Verify The Minimize Icon On Total Column
        ///</summary>
        ///<param name="columnNames">Names of Columns present in Pivot Grid</param>
        ///<param name="pivotFieldsList">Fields Selected From Pivot Options</param>
        ///<returns></returns>
        public PivotGrid VerifyTheMinimizeIconOnTotalColumn(string[] pivotFieldsList, string[] columnNames)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-header-container']/div[1]//div[@col-id='0_0']"), "'Total' Column not present in Pivot Grid.");

            string width = driver._getAttributeValue("xpath", "//cft-pivot-table//div[@class='ag-header-container']/div[1]//div[@col-id='0_0']", "style");
            int size = (width.IndexOf("x; l") - 1) - (width.IndexOf("h:") + 3);
            width = width.Substring(width.IndexOf("h:") + 3, size);
            Console.WriteLine("Width Of Total Column: " + width);
            int iWidth = 0;
            Assert.IsTrue(int.TryParse(width, out iWidth), "Couldn't convert '" + width + "' to integer");
            if (iWidth / 150 > 1)
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//div[@class='ag-header-container']//div[@col-id='0_0']//span[contains(@class, 'header-expand-icon') and not(contains(@class, 'hidden'))]"), "'Minimize Icon' not present");

            if (driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-header-container']//div[@col-id='0_0']//span[contains(@class, 'header-expand-icon') and not(contains(@class, 'hidden'))]"))
            {
                width = driver._getAttributeValue("xpath", "//cft-pivot-table//div[@class='ag-header-container']/div[1]//div[@col-id='0_0']", "style");
                size = (width.IndexOf("x; l") - 1) - (width.IndexOf("h:") + 3);
                width = width.Substring(width.IndexOf("h:") + 3, size);
                Console.WriteLine("Width Of Total Column: " + width);
                Assert.IsTrue(int.TryParse(width, out iWidth), "Couldn't convert '" + width + "' to integer");
                if (driver._getAttributeValue("xpath", "//cft-pivot-table//div[@class='ag-header-container']//div[@col-id='0_0']//span[contains(@class, 'header-expand-icon') and not(contains(@class, 'hidden'))]", "ref").ToLower().Contains("closed"))
                    Assert.Greater(iWidth / 150, 1, "'Minimize Icon' is Present when only one metric option is selected.");
                else
                {
                    Assert.AreEqual(iWidth / 150, 1, "'Minimize Icon' is not present as minimized.");
                    driver._click("xpath", "//cft-pivot-table//div[@class='ag-header-container']//div[@col-id='0_0']//span[contains(@class, 'header-expand-icon') and not(contains(@class, 'hidden'))]");
                    Thread.Sleep(3000);
                    Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-pivot-table//div[@class='ag-header-container']//div[@col-id='0_0']//span[contains(@class, 'header-expand-icon') and not(contains(@class, 'hidden'))]", "ref").ToLower().Contains("closed"), "'Minimize Icon did not get expanded'");
                    VerifyColumnsInPivotGrid(pivotFieldsList, columnNames);
                }
            }
            
            Results.WriteStatus(test, "Pass", "Verified, The Minimize Icon On Total Column");
            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Verify Filter Functionality On Text Fields In Pivot Grid
        ///</summary>
        ///<param name="columnName">Column Name on which filter is to be applied</param>
        ///<returns></returns>
        public string VerifyFilterFunctionalityOnTextFieldsInPivotGrid(string columnName = "Random")
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-header']//div[contains(@class, 'ag-header-cell-sortable')]"), "Column headers not present in Pivot Grid.");
            IList<IWebElement> columnColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-header']//div[contains(@class, 'ag-header-cell-sortable')]");
            int x = -1;
            string filterValue = "";
            string colId = "";
            if (columnName.Equals("Random"))
            {
                Random rand = new Random();
                x = rand.Next(0, columnColl.Count);
            }
            for(int i = 0; i < columnColl.Count; i++)
            {
                IList<IWebElement> columnNameColl = columnColl[i]._findElementsWithinElement("xpath", ".//span[@ref='eText']");
                if(x == i)
                {
                    columnName = columnNameColl[0].Text;
                    colId = columnColl[i].GetAttribute("col-id");
                    break;
                }
                else if (columnNameColl[0].Text.ToLower().Contains(columnName.ToLower()))
                {
                    x = i;
                    colId = columnColl[i].GetAttribute("col-id");
                    break;
                }
            }

            IList<IWebElement> columnFilterColl = columnColl[x]._findElementsWithinElement("xpath", ".//span[@ref='eMenu']");
            columnFilterColl[0].Click();
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']"), "'Filter Menu' not present");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']//input[@placeholder='Search...']"), "'Search Text box' not present in Filter Menu");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']//label[@id='selectAllContainer']/span"), "'Select All' label not present in Filter Menu");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']//label[@id='selectAllContainer']/div[@id='selectAll']"), "'Select All' checkbox not present in Filter Menu");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']//div[@id='richList']//label[@class='ag-set-filter-item']"), "'DDL and checkboxes' not present in Filter Menu");
            IList<IWebElement> filterDDLColl = driver._findElements("xpath", "//div[@class='ag-filter']//div[@id='richList']//label[@class='ag-set-filter-item']");

            driver._click("xpath", "//div[@class='ag-filter']//label[@id='selectAllContainer']");
            Thread.Sleep(2000);
            filterDDLColl = driver._findElements("xpath", "//div[@class='ag-filter']//div[@id='richList']//label[@class='ag-set-filter-item']");
            filterValue = filterDDLColl[0].Text;
            filterDDLColl[0].Click();
            Thread.Sleep(5000);

            IList<IWebElement> columnValueColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-cols-container']//div[@col-id='" + colId + "']/span");
            bool avail = true;
            foreach(IWebElement columnValue in columnValueColl)
                if (!columnValue.Text.Equals(filterValue))
                {
                    avail = false;
                    break;
                }
            Assert.IsTrue(avail, "Filter Value '" + filterValue + "' did not get applied to column '" + columnName + "' successfully.");

            Results.WriteStatus(test, "Pass", "Verified, Filter Functionality On Text Fields In Pivot Grid");
            return "";
        }

        ///<summary>
        ///Verify Applied Filter in Pivot Grid And AgGrid
        ///</summary>
        ///<returns></returns>
        public PivotGrid VerifyAppliedFilterInPivotGridAndAgGrid(string columnName, string[] filterValue, bool present = true)
        {
            string colId = "";
            string filter = "";
            for (int i = 0; i < filterValue.Length; i++)
                filter = filter + ";" + filterValue[i];

            filter = filter + ";";

            if (driver._waitForElement("xpath", "//cft-pivot-table//div[@role='grid']"))
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-header']//div[contains(@class, 'ag-header-cell-sortable')]"), "Column headers not present in Pivot Grid.");
                IList<IWebElement> columnColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-header']//div[contains(@class, 'ag-header-cell-sortable')]");

                for (int i = 0; i < columnColl.Count; i++)
                {
                    IList<IWebElement> columnNameColl = columnColl[i]._findElementsWithinElement("xpath", ".//span[@ref='eText']");
                    if (columnNameColl[0].Text.ToLower().Contains(columnName.ToLower()))
                    {
                        colId = columnColl[i].GetAttribute("col-id");
                        break;
                    }
                }

                IList<IWebElement> columnValueColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-cols-container']//div[@col-id='" + colId + "']/span");
                bool avail = true;
                foreach (IWebElement columnValue in columnValueColl)
                    if (!filter.ToLower().Contains(";" + columnValue.Text.ToLower() + ";"))
                    {
                        avail = false;
                        break;
                    }
                if(present)
                    Assert.IsTrue(avail, "Filter Value '" + filter + "' did not get applied to column '" + columnName + "' successfully.");
                else
                    Assert.IsFalse(avail, "Filter Value '" + filter + "' is present in column '" + columnName + "' successfully.");

            }

            if (driver._waitForElement("xpath", "//cft-domain-item-group//div[@role='grid']"))
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-group//div[contains(@class,'header')]//div[contains(@class, 'ag-header-cell-sortable')]"), "Column headers not present in Pivot Grid.");
                IList<IWebElement> columnColl = driver._findElements("xpath", "//cft-domain-item-group//div[contains(@class,'header')]//div[contains(@class, 'ag-header-cell-sortable')]");

                for (int i = 0; i < columnColl.Count; i++)
                {
                    IList<IWebElement> columnNameColl = columnColl[i]._findElementsWithinElement("xpath", ".//span[@ref='eText']");
                    if (columnNameColl[0].Text.ToLower().Contains(columnName.ToLower()))
                    {
                        colId = columnColl[i].GetAttribute("col-id");
                        break;
                    }
                }

                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-group//div[@class='ag-body-viewport']//div[contains(@col-id,'" + colId + "')]"), "Values not present in AgGrid.");
                IList<IWebElement> gridValueCol = driver._findElements("xpath", "//cft-domain-item-group//div[@class='ag-body-viewport']//div[contains(@col-id,'" + colId + "')]");

                bool avail = true;
                foreach (IWebElement gridValue in gridValueCol)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", gridValue);
                    if (!filter.ToLower().Contains(";" + gridValue.Text.ToLower() + ";"))
                    {
                        avail = false;
                        break;
                    }
                }
                if (present)
                    Assert.IsTrue(avail, "'" + filter + "' not applied as filter successfully.");
                else
                    Assert.IsFalse(avail, "'" + filter + "' is applied as filter on column '" + columnName + "'.");
            }


            Results.WriteStatus(test, "Pass", "Verified, Filter applied successfully to column '" + columnName + "' for filter value '" + filter + "' in Pivot Grid And AgGrid");
            return new PivotGrid(driver, test);
        }


        ///<summary>
        ///Select Value from Pivot Grid
        ///</summary>
        ///<param name="value">Value to be selected</param>
        ///<returns></returns>
        public string selectValueFromPivotGrid(string value = "")
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row']"), "'Rows' not present in Pivot Grid.");
            driver._scrollintoViewElement("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row']");
            IList<IWebElement> rowsCollection = driver._findElements("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row']");
            string companyName = "";
            int x = -1, n = -1;
            Random rand = new Random();
            if(value == "")
            {
                x = rand.Next(0, rowsCollection.Count);
                IList<IWebElement> cellCollection = rowsCollection[x]._findElementsWithinElement("xpath", ".//div[@col-id and contains(@class, 'selectable')]");
                Assert.AreNotEqual(0, cellCollection.Count, "No cells present in selected row.");
                if (x > rowsCollection.Count / 2)
                    n = x - rowsCollection.Count / 2;
                else
                    n = x;

                driver._scrollintoViewElement("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row'][" + (n + 1) + "]");
                IList<IWebElement> mainCellColl = rowsCollection[n]._findElementsWithinElement("xpath", ".//div[@col-id and contains(@class, 'selectable')]");

                foreach(IWebElement mainCell in mainCellColl)
                    if (mainCell.GetAttribute("col-id").Contains("parentAdvertiserCode"))
                    {
                        companyName = mainCell.Text;
                        break;
                    }

                x = rand.Next(0, cellCollection.Count);
                value = cellCollection[x].Text;
                cellCollection[x].Click();
                Thread.Sleep(2000);
                cellCollection = rowsCollection[x]._findElementsWithinElement("xpath", ".//div[@col-id]");
            }
            else
            {
                for(int i = 0; i < rowsCollection.Count; i++)
                {
                    IList<IWebElement> cellCollection = rowsCollection[i]._findElementsWithinElement("xpath", ".//div[@col-id and contains(@class, 'selectable')]");
                    Assert.AreNotEqual(0, cellCollection.Count, "No cells present in selected row.");
                    companyName = cellCollection[0].Text;
                    foreach (IWebElement cell in cellCollection)
                        if (cell.Text.ToLower().Contains(value.ToLower()))
                        {
                            x = i;
                            n = i;
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", cell);
                            cell.Click();
                            Thread.Sleep(2000);
                            break;
                        }
                    if (x > -1)
                        break;
                }
                Assert.Greater(x, -1, "'" + value + "' not found in pivot grid");
            }
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row' and @row-id='" + n + "']//div[contains(@class,'selected')]"), "'" + value + "' not selected in pivot grid.");
            IList<IWebElement> selectedCells = driver._findElements("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row' and @row-id='" + n + "']//div[contains(@class,'selected')]");
            Assert.AreEqual(1, selectedCells.Count, "More than one cells are selected from one row.");
            //	rgba(0, 168, 184, 0.7) none repeat scroll 0% 0% / auto padding-box border-box



            Results.WriteStatus(test, "Pass", "Selected, Value from Pivot Grid");
            return companyName;
        }

        ///<summary>
        ///Verify Filter Functionality On Numeric Fields In Pivot Grid
        ///</summary>
        ///<param name="columnName">Name of Column on which filter is to be applied</param>
        ///<param name="filterType">Type of filter to be applied</param>
        ///<param name="filterValue">Filter Value</param>
        ///<returns></returns>
        public string VerifyFilterFunctionalityOnNumericFieldsInPivotGrid(string columnName = "Random", string filterType = "Equals", string filterValue = "", bool remove = true)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-header-viewport']//div[contains(@class, 'ag-header-cell-sortable')]"), "Column headers not present in Pivot Grid.");
            IList<IWebElement> columnColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-header-viewport']//div[contains(@class, 'ag-header-cell-sortable')]");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-header-viewport']//div[contains(@class, 'ag-header-group-cell ')]"), "Column header names not present in Pivot Grid.");
            IList<IWebElement> columnHeaderColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-header-viewport']//div[contains(@class, 'ag-header-group-cell ')]");

            int x = -1;
            string colId = "";
            Random rand = new Random();
            if (columnName.Equals("Random"))
                x = rand.Next(0, columnHeaderColl.Count);
            for (int i = 0; i < columnHeaderColl.Count; i++)
            {
                IList<IWebElement> columnNameColl = columnHeaderColl[i]._findElementsWithinElement("xpath", ".//span[@ref='agLabel']");
                if (x == i)
                {
                    columnName = columnNameColl[0].Text;
                    colId = columnColl[i].GetAttribute("col-id");
                    break;
                }
                else if (columnNameColl[0].Text.ToLower().Contains(columnName.ToLower()))
                {
                    x = i;
                    colId = columnColl[i].GetAttribute("col-id");
                    break;
                }
            }

            IList<IWebElement> columnValueColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@col-id='" + colId + "' and contains(@class, 'selectable')]");
            int filteredCount = 0;
            if (filterValue.Equals(""))
            {
                int z = rand.Next(0, columnValueColl.Count);
                string compId = columnValueColl[z].GetAttribute("comp-id");
                driver._scrollintoViewElement("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@col-id='" + colId + "' and @comp-id='" + compId + "' and contains(@class, 'selectable')]/span");
                filterValue = columnValueColl[z].Text;
            }
            string typeValue = filterValue;
            while (typeValue.IndexOf("$") >= 0)
                typeValue = typeValue.Remove(typeValue.IndexOf("$"), 1);
            while (typeValue.IndexOf(",") >= 0)
                typeValue = typeValue.Remove(typeValue.IndexOf(","), 1);

            IList<IWebElement> columnFilterColl = columnColl[x]._findElementsWithinElement("xpath", ".//span[@ref='eMenu']");
            columnFilterColl[0].Click();
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']"), "'Filter Menu' not present");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']//input[@placeholder='Filter...']"), "'Filter Text box' not present in Filter Menu");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']//select[@id='filterType']"), "'Filter Type' not present in Filter Menu");

            driver._scrollintoViewElement("xpath", "//cft-pivot-table//div[@class='ag-header-viewport']//div[contains(@class, 'ag-header-cell-sortable')]");

            driver._click("xpath", "//div[@class='ag-filter']//select[@id='filterType']");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']//select[@id='filterType']/option"), "'Filter Type DDL' not present in Filter Menu");
            IList<IWebElement> filterDDLColl = driver._findElements("xpath", "//div[@class='ag-filter']//select[@id='filterType']/option");

            bool avail = false;
            foreach(IWebElement filterTypeEle in filterDDLColl)
                if (filterTypeEle.Text.ToLower().Equals(filterType.ToLower()))
                {
                    avail = true;
                    filterTypeEle.Click();
                    break;
                }
            Assert.IsTrue(avail, "'" + filterType + "' filter type not present in column '" + columnName + "'");
            decimal fromVal = 0, toVal = 0;

            if (filterType.ToLower().Equals("equals"))
            {
                driver._type("xpath", "//div[@class='ag-filter']//input[@placeholder='Filter...']", typeValue);
                Thread.Sleep(2000);

                columnValueColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@col-id='" + colId + "' and contains(@class, 'selectable')]/span");
                filteredCount = columnValueColl.Count;
                avail = true;
                foreach (IWebElement columnValue in columnValueColl)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", columnValue);
                    if (!columnValue.Text.Equals(filterValue))
                    {
                        avail = false;
                        break;
                    }
                }
                if(decimal.TryParse(typeValue, out fromVal))
                    Assert.IsTrue(avail, "Filter Value '" + filterValue + "' did not get applied to column '" + columnName + "' successfully.");
                else
                {
                    Assert.IsFalse(avail, "Filter Value '" + filterValue + "' got applied to column '" + columnName + "' for characters.");
                    return "";
                }
            }
            else if (filterType.ToLower().Equals("not equal"))
            {
                driver._type("xpath", "//div[@class='ag-filter']//input[@placeholder='Filter...']", typeValue);
                Thread.Sleep(2000);

                columnValueColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@col-id='" + colId + "' and contains(@class, 'selectable')]/span");
                filteredCount = columnValueColl.Count;
                avail = true;
                foreach (IWebElement columnValue in columnValueColl)
                {
                    if (columnValue.Text.Equals(filterValue))
                    {
                        avail = false;
                        break;
                    }
                }
                if (decimal.TryParse(typeValue, out fromVal))
                    Assert.IsTrue(avail, "Filter Value '" + filterValue + "' did not get applied to column '" + columnName + "' successfully.");
                else
                {
                    Assert.IsFalse(avail, "Filter Value '" + filterValue + "' got applied to column '" + columnName + "' for characters.");
                    return "";
                }
            }
            else
            {
                driver._type("xpath", "//div[@class='ag-filter']//input[@id='filterText']", typeValue);
                Thread.Sleep(4000);

                if (decimal.TryParse(typeValue, out fromVal))
                {
                    if (filterType.ToLower().Equals("less than"))
                    {
                        columnValueColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@col-id='" + colId + "' and contains(@class, 'selectable')]/span");
                        filteredCount = columnValueColl.Count;
                        int limit = 10;
                        if (columnValueColl.Count < 10)
                            limit = columnValueColl.Count;
                        for (int i = 0; i < limit; i++)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", columnValueColl[i]);
                            string text = columnValueColl[i].Text;
                            while (text.IndexOf("$") >= 0)
                                text = text.Substring(1);
                            while (text.IndexOf(",") >= 0)
                                text = text.Remove(text.IndexOf(","), 1);
                            decimal currValue = 0;
                            Assert.IsTrue(decimal.TryParse(text, out currValue), "Couldn't convert '" + columnValueColl[i].Text + "' to decimal");
                            Assert.Less(Math.Round(currValue, 1), Math.Round(fromVal, 1), "'" + currValue + "' is not less than '" + fromVal + "'");
                        }
                    }
                    else if (filterType.ToLower().Equals("less than or equals"))
                    {
                        columnValueColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@col-id='" + colId + "' and contains(@class, 'selectable')]/span");
                        filteredCount = columnValueColl.Count;
                        int limit = 10;
                        if (columnValueColl.Count < 10)
                            limit = columnValueColl.Count;
                        for (int i = 0; i < limit; i++)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", columnValueColl[i]);
                            string text = columnValueColl[i].Text;
                            while (text.IndexOf("$") >= 0)
                                text = text.Substring(1);
                            while (text.IndexOf(",") >= 0)
                                text = text.Remove(text.IndexOf(","), 1);
                            decimal currValue = 0;
                            Assert.IsTrue(decimal.TryParse(text, out currValue), "Couldn't convert '" + columnValueColl[i].Text + "' to decimal");
                            Assert.LessOrEqual(Math.Round(currValue, 1), Math.Round(fromVal, 1), "'" + currValue + "' is not less than or equal to '" + fromVal + "'");
                        }
                    }
                    else if (filterType.ToLower().Equals("greater than"))
                    {
                        columnValueColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@col-id='" + colId + "' and contains(@class, 'selectable')]/span");
                        filteredCount = columnValueColl.Count;
                        int limit = 10;
                        if (columnValueColl.Count < 10)
                            limit = columnValueColl.Count;
                        for (int i = 0; i < limit; i++)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", columnValueColl[i]);
                            string text = columnValueColl[i].Text;
                            while (text.IndexOf("$") >= 0)
                                text = text.Substring(1);
                            while (text.IndexOf(",") >= 0)
                                text = text.Remove(text.IndexOf(","), 1);
                            decimal currValue = 0;
                            Assert.IsTrue(decimal.TryParse(text, out currValue), "Couldn't convert '" + columnValueColl[i].Text + "' to decimal");
                            Assert.Greater(Math.Round(currValue, 1), Math.Round(fromVal, 1), "'" + currValue + "' is not greater than '" + fromVal + "'");
                        }
                    }
                    else if (filterType.ToLower().Equals("greater than or equals"))
                    {
                        columnValueColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@col-id='" + colId + "' and contains(@class, 'selectable')]/span");
                        filteredCount = columnValueColl.Count;
                        int limit = 10;
                        if (columnValueColl.Count < 10)
                            limit = columnValueColl.Count;
                        for (int i = 0; i < limit; i++)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", columnValueColl[i]);
                            string text = columnValueColl[i].Text;
                            while (text.IndexOf("$") >= 0)
                                text = text.Substring(1);
                            while (text.IndexOf(",") >= 0)
                                text = text.Remove(text.IndexOf(","), 1);
                            decimal currValue = 0;
                            Assert.IsTrue(decimal.TryParse(text, out currValue), "Couldn't convert '" + columnValueColl[i].Text + "' to decimal");
                            Assert.GreaterOrEqual(Math.Round(currValue, 1), Math.Round(fromVal, 1), "'" + currValue + "' is not greater than or equal to '" + fromVal + "'");
                        }
                    }
                    else if (filterType.ToLower().Equals("in range"))
                    {
                        int digits = typeValue.Length;
                        toVal = fromVal + (10 * digits);

                        Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-filter']//input[@id='filterToText']"), "'Upper Value' Text box not present for 'in range' filter type");
                        driver._type("xpath", "//div[@class='ag-filter']//input[@id='filterToText']", toVal.ToString());
                        Thread.Sleep(2000);

                        columnValueColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@col-id='" + colId + "' and contains(@class, 'selectable')]/span");
                        filteredCount = columnValueColl.Count;
                        int limit = 10;
                        if (columnValueColl.Count < 10)
                            limit = columnValueColl.Count;
                        for (int i = 0; i < limit; i++)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", columnValueColl[i]);
                            string text = columnValueColl[i].Text;
                            while (text.IndexOf("$") >= 0)
                                text = text.Substring(1);
                            while (text.IndexOf(",") >= 0)
                                text = text.Remove(text.IndexOf(","), 1);
                            decimal currValue = 0;
                            Assert.IsTrue(decimal.TryParse(text, out currValue), "Couldn't convert '" + columnValueColl[i].Text + "' to decimal");
                            Assert.IsTrue((Math.Round(currValue, 1) >= Math.Round(fromVal, 1)) && (Math.Round(currValue, 1) <= Math.Round(toVal, 1)), "'" + currValue + "' is not less than, greater than or equal to '" + fromVal + "'");
                        }
                    }
                }
                else
                {
                    Results.WriteStatus(test, "Pass", "Filter Value '" + filterValue + "' did not get applied to column '" + columnName + "' for characters.");
                    return "";
                }
            }

            Results.WriteStatus(test, "Pass", "Applied, '" + filterType + "' Filter Type Successfully");

            if (remove)
            {
                while (driver._getValue("xpath", "//div[@class='ag-filter']//input[@id='filterText']") != "")
                    driver.FindElement(By.XPath("//div[@class='ag-filter']//input[@id='filterText']")).SendKeys(Keys.Backspace);
                Thread.Sleep(2000);
                driver._click("xpath", "//a[contains(text(), 'Pivot Bulk Actions')]");
            }

            Results.WriteStatus(test, "Pass", "Verified, Filter Functionality On Numeric Fields In Pivot Grid");
            return "";
        }

        ///<summary>
        ///Find Values to Select Records From Pivot Grid
        ///</summary>
        ///<returns></returns>
        public PivotGrid findValuesToSelectRecordsFromGrid(bool oneColumn, bool oneRow, int num = 2)
        {
            string collectionXPath = "", colId = "", rowId = "";
            int x = -1;
            Random rand = new Random();
            if (oneColumn)
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-pinned-left-cols-container')]//div[@role='row' and @row-id='1']//div[@col-id]"), "Text Field Columns not present in Pivot Grid");
                IList<IWebElement> columnCollection = driver._findElements("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-pinned-left-cols-container')]//div[@role='row' and @row-id='1']//div[@col-id]");
                x = rand.Next(0, columnCollection.Count);
                colId = columnCollection[x].GetAttribute("col-id");
                collectionXPath = "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-pinned-left-cols-container')]//div[@col-id='" + colId + "' and contains(@class, 'selectable') and not(contains(@class, 'selected'))]";
            }
            else if (oneRow)
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-pinned-left-cols-container')]//div[@role='row']"), "Text Field Rows not present in Pivot Grid");
                IList<IWebElement> rowCollection = driver._findElements("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-pinned-left-cols-container')]//div[@role='row']");
                x = rand.Next(0, rowCollection.Count);
                rowId = rowCollection[x].GetAttribute("row-id");
                collectionXPath = "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-pinned-left-cols-container')]//div[@role='row' and @row-id='" + rowId + "']//div[@col-id and contains(@class, 'selectable') and not(contains(@class, 'selected'))]";
            }
            else
                collectionXPath = "//cft-pivot-table//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row' and contains(@class, 'ag-row ag-row-no-focus')]//div[@col-id and contains(@class, 'selectable') and not(contains(@class, 'selected'))]";

            driver._scrollintoViewElement("xpath", collectionXPath);

            for(int i = 0; i < num; i++)
            {
                IList<IWebElement> collection = driver._findElements("xpath", collectionXPath);
                int n = -1;
                if(collection.Count > 10)
                    n = rand.Next(3, 10);
                else
                    n = rand.Next(3, collection.Count);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", collection[n]);
                string value = collection[n].Text;
                collection[n].Click();
                Thread.Sleep(2000);
            }
            
            if(rowId != "")
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row' and @row-id='" + rowId + "']//div[contains(@class,'selected')]"), "'Cell' not selected in pivot grid.");
                IList<IWebElement> selectedCells = driver._findElements("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row' and @row-id='" + rowId + "']//div[contains(@class,'selected')]");
                Assert.AreEqual(1, selectedCells.Count, "More than one cells are selected from one row.");
                Assert.IsTrue(selectedCells[0].GetCssValue("background").Contains("rgba(0, 168, 184, 0.7)"), "Selected Cell is not of teal color.");
            }
            else
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row']//div[contains(@class,'selected')]"), "Cell' not selected in pivot grid.");
                IList<IWebElement> selectedCells = driver._findElements("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row']//div[contains(@class,'selected')]");
                Assert.AreEqual(num, selectedCells.Count, "'" + num + "' cells did not get selected.");
                foreach(IWebElement selected in selectedCells)
                    Assert.IsTrue(selected.GetCssValue("background").Contains("rgba(0, 168, 184, 0.7)"), "Selected Cell is not of teal color.");
            }

            Results.WriteStatus(test, "Pass", "Selected, '" + num + "' values.");
            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Verify Pivot Grid Columns for Market and Media Reports
        ///</summary>
        ///<returns></returns>
        public PivotGrid VerifyPivotGridColumnsForMarketAndMediaReports()
        {
            homePage.selectOptionFromSideNavigationBar("Print Report by Market");
            homePage.newVerifyHomePage();
            string[] expectedMarketColumnNames = new string[] { "ALBERTA BALANCE", "British Columbia Balance", "Calgary", "Edmonton", "Halifax", "Kitchener" };
            charts.VerifyPivotTable();

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-header-group-cell-with-group')]//span[@ref='agLabel']"), "Numeric Columns not present in Pivot Grid.");
            IList<IWebElement> numericColumnCollection = driver._findElements("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-header-group-cell-with-group')]//span[@ref='agLabel']");

            foreach (string expected in expectedMarketColumnNames)
            {
                bool avail = false;
                foreach (IWebElement marketColumn in numericColumnCollection)
                    if (marketColumn.Text.ToLower().Contains(expected.ToLower()))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "'" + expected + "' not present in Markets Report Pivot Grid.");
            }

            homePage.selectOptionFromSideNavigationBar("Print Report by Media");
            homePage.newVerifyHomePage();
            string[] expectedMediaColumnNames = new string[] { "Magazine", "Newspaper" };
            charts.VerifyPivotTable();

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-header-group-cell-with-group')]//span[@ref='agLabel']"), "Numeric Columns not present in Pivot Grid.");
            numericColumnCollection = driver._findElements("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-header-group-cell-with-group')]//span[@ref='agLabel']");

            foreach (string expected in expectedMediaColumnNames)
            {
                bool avail = false;
                foreach (IWebElement mediaColumn in numericColumnCollection)
                    if (mediaColumn.Text.ToLower().Contains(expected.ToLower()))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "'" + expected + "' not present in Markets Report Pivot Grid.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Pivot Grid Columns for Market and Media Reports");
            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Verify Filter Applied on Table Grid View By Selecting Pivot Grid Cell
        ///</summary>
        ///<param name="filterValue">Value to be selected in filter</param>
        ///<returns></returns>
        public PivotGrid VerifyFilterAppliedOnTableGridViewBySelectingPivotGridCell(string filterValue = "", bool VerifyViewAdPopup = false)
        {
            selectValueFromPivotGrid(filterValue);

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-group//div[@class='ag-body-viewport']//div[contains(@col-id,'advertiserName')]"), "Values not present in AgGrid.");
            IList<IWebElement> gridValueCol = driver._findElements("xpath", "//cft-domain-item-group//div[@class='ag-body-viewport']//div[contains(@col-id,'advertiserName')]");
            bool avail = true;
            for (int i = 0; i < 10; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", gridValueCol[i]);
                if (!gridValueCol[i].Text.ToLower().Contains(filterValue.ToLower()))
                {
                    avail = false;
                    break;
                }
            }
            Assert.IsTrue(avail, "'" + filterValue + "' not applied as filter successfully.");

            if (VerifyViewAdPopup)
            {
                homePage.selectViewForResultsDisplay("Details");
                homePage.VerifyDetailsViewOfAgGrid(true);
                viewAdPopup.clickOnButtonOfResultsCard("View Ad");
                carousels.VerifyViewAdFunctionality(true, true, true);
                viewAdPopup.clickOnButtonOfViewAdPopup("Close");
                carousels.VerifyViewAdFunctionality(false);
                homePage.selectViewForResultsDisplay("Table");

                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-group//div[@class='ag-body-viewport']//div[contains(@col-id,'advertiserName')]"), "Values not present in AgGrid.");
                gridValueCol = driver._findElements("xpath", "//cft-domain-item-group//div[@class='ag-body-viewport']//div[contains(@col-id,'advertiserName')]");
                avail = true;
                for (int i = 0; i < 10; i++)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", gridValueCol[i]);
                    if (!gridValueCol[i].Text.ToLower().Contains(filterValue.ToLower()))
                    {
                        avail = false;
                        break;
                    }
                }
                Assert.IsTrue(avail, "'" + filterValue + "' not applied as filter after View Ad Popup is viewed.");

            }

            Results.WriteStatus(test, "Pass", "Verified, AgGrid As Per Selected Bar From Expanded Chart");
            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Verify Mouse Pointer When Hover over zero/non-zero elements
        ///</summary>
        ///<returns></returns>
        public PivotGrid VerifyMousePointerWhenHoverOver0_Non0Elements()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@role = 'gridcell']"), "Cells not present in Pivot Grid.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@role = 'row']"), "Rows not present in Pivot Grid");
            IList<IWebElement> rowsCollection = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-viewport']//div[@role = 'row']");

            Actions action = new Actions(driver);
            bool foundSelectable = false, foundZero = false;
            for (int i = 0; i < 10; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", rowsCollection[i + 1]);
                IList<IWebElement> selectableCellCollection = rowsCollection[i]._findElementsWithinElement("xpath", ".//div[@role = 'gridcell' and contains(@class, 'selectable')]");
                if(selectableCellCollection.Count > 0 && !foundSelectable)
                {
                    action.MoveToElement(selectableCellCollection[0]).MoveByOffset(1, 0).Perform();
                    Thread.Sleep(500);
                    Assert.AreEqual("pointer", selectableCellCollection[0].GetCssValue("cursor"), "Cursor did not change to 'pointer' for non-zero cell.");
                    foundSelectable = true;
                }

                IList<IWebElement> nonSelectableCellCollection = rowsCollection[i]._findElementsWithinElement("xpath", ".//div[@role = 'gridcell' and not(contains(@class, 'selectable'))]");
                if (nonSelectableCellCollection.Count > 0 && !foundZero)
                {
                    action.MoveToElement(nonSelectableCellCollection[0]).MoveByOffset(1, 0).Perform();
                    Thread.Sleep(500);
                    Assert.AreEqual("not-allowed", nonSelectableCellCollection[0].GetCssValue("cursor"), "Cursor did not change to 'not-allowed' for non-zero cell.");
                    nonSelectableCellCollection[0].Click();
                    Thread.Sleep(1000);
                    Assert.IsFalse(driver._waitForElement("xpath", "//cft-pivot-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row' and @row-id='" + i + "']//div[contains(@class,'selected')]"), "'Non-zero Cell' got selected in pivot grid.");
                    foundZero = true;
                }
                Assert.IsTrue(rowsCollection[i].GetCssValue("background-color").Contains("rgba(236, 240, 241, 1)"), "Row containing the cell on which mouse is hovered did not change aapearance as highlighted.");

                if (foundSelectable && foundZero)
                    break;
            }

            if (foundSelectable)
                Results.WriteStatus(test, "Pass", "Verified, Mouse Pointer When Hover over non-zero elements");
            if(foundZero)
                Results.WriteStatus(test, "Pass", "Verified, Mouse Pointer When Hover over zero-valued elements");

            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Verify Column-wise Summary Total in Exported Pivot Data
        ///</summary>
        ///<param name="fileName">Name of Excel File</param>
        ///<returns></returns>
        public PivotGrid VerifyColumnwiseSummaryTotalInExportedPivotData(string fileName)
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
                if (xlWorkSheet.Name.Contains("All Ads"))
                    break;
            }

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;

            int startRow = 0, endRow = 0;

            for(int rCnt = 1; rCnt <= rw; rCnt++)
                if((range.Cells[rCnt, 4] as Excel.Range).Text.ToLower().Contains("spend cp"))
                {
                    startRow = rCnt + 1;
                    break;
                }
            for (int rCnt = startRow + 1; rCnt <= rw; rCnt++)
                if ((range.Cells[rCnt, 1] as Excel.Range).Text.ToLower().Contains("please note"))
                {
                    endRow = rCnt - 3;
                    break;
                }

            Assert.IsTrue(startRow != 0 && endRow != 0, "File not downloaded properly. Either first data row or last data row is missing.");

            for(int cCnt = 4; cCnt <=cl; cCnt++)
            {
                string columnName = (range.Cells[startRow - 2, cCnt] as Excel.Range).Text;
                int columnTotal = 0;
                for (int rCnt = startRow; rCnt <=endRow; rCnt++)
                {
                    string currCellText = (range.Cells[rCnt, cCnt] as Excel.Range).Text;

                    if (currCellText.Contains("$"))
                        currCellText = currCellText.Substring(1);
                    while (currCellText.Contains(","))
                        currCellText = currCellText.Remove(currCellText.IndexOf(","), 1);

                    int currValue = 0;

                    if (!currCellText.Equals(""))
                        Assert.IsTrue(int.TryParse(currCellText, out currValue), "Couldn't convert '" + currCellText + " (" + (range.Cells[rCnt, cCnt] as Excel.Range).Text + ")' to integer");

                    columnTotal = columnTotal + currValue;
                }

                string totalRowText = (range.Cells[endRow + 1, cCnt] as Excel.Range).Text;
                if (totalRowText.Contains("$"))
                    totalRowText = totalRowText.Substring(1);
                while (totalRowText.Contains(","))
                    totalRowText = totalRowText.Remove(totalRowText.IndexOf(","), 1);

                int totalInSheet = 0;
                Assert.IsTrue(int.TryParse(totalRowText, out totalInSheet), "Couldn't convert '" + totalRowText + " (" + (range.Cells[endRow + 1, cCnt] as Excel.Range).Text + ")' to integer");

                Assert.LessOrEqual(columnTotal, totalInSheet + 30, "Summary Total does not match to the calulated total of '" + columnName + "' column.");
                Assert.GreaterOrEqual(columnTotal, totalInSheet - 30, "Summary Total does not match to the calulated total of '" + columnName + "' column.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Column-wise Summary Total in Exported Pivot Data");
            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Verify Sorting in Pivot Grid
        ///</summary>
        ///<param name="ascending">Sorting order</param>
        ///<returns></returns>
        public PivotGrid VerifySortingInPivotGrid(bool ascending = true)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//div[contains(@class, 'pinned')]//div[contains(@class, 'ag-header-cell-sortable')]"), "Column Headers not present");
            IList<IWebElement> columnHeaderColl = driver._findElements("xpath", "//cft-pivot-table//div[contains(@class, 'pinned')]//div[contains(@class, 'ag-header-cell-sortable')]");

            Random rand = new Random();
            int x = rand.Next(0, columnHeaderColl.Count);

            string colId = columnHeaderColl[x].GetAttribute("col-id");
            IList<IWebElement> colEleColl = columnHeaderColl[x]._findElementsWithinElement("xpath", ".//span[@ref='eText']");
            string sortingColumn = colEleColl[0].Text;
            colEleColl = columnHeaderColl[x]._findElementsWithinElement("xpath", ".//span[contains(@class, 'sort') and not(contains(@class, 'hidden'))]");

            if (ascending)
            {
                while(!colEleColl[0].GetAttribute("ref").Equals("eSortAsc"))
                {
                    colEleColl[0].Click();
                    Thread.Sleep(500);
                    colEleColl = columnHeaderColl[x]._findElementsWithinElement("xpath", ".//span[contains(@class, 'sort') and not(contains(@class, 'hidden'))]");
                }
            }
            else
            {
                while (!colEleColl[0].GetAttribute("ref").Equals("eSortDesc"))
                {
                    colEleColl[0].Click();
                    Thread.Sleep(500);
                    colEleColl = columnHeaderColl[x]._findElementsWithinElement("xpath", ".//span[contains(@class, 'sort') and not(contains(@class, 'hidden'))]");
                }
            }

            Thread.Sleep(500);
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@role='gridcell' and @col-id='" + colId + "']"), "Cells not present in '" + sortingColumn + "' column.");
            IList<IWebElement> cellColl = driver._findElements("xpath", "//cft-pivot-table//div[@role='gridcell' and @col-id='" + colId + "']/span");
            int limit = 10;
            if (cellColl.Count < 10)
                limit = cellColl.Count;
            string[] cellValueList = new string[limit];
            for (int i = 0; i < limit; i++)
                cellValueList[i] = cellColl[i].Text;

            string[] capturedValuesList = cellValueList;
            Array.Sort(cellValueList);
            if (!ascending)
                Array.Reverse(cellValueList);

            Assert.IsTrue(cellValueList.SequenceEqual(capturedValuesList), "Sorting on column '" + sortingColumn + "' was not successful.");

            Results.WriteStatus(test, "Pass", "Verified, Sorting in Pivot Grid");
            return new PivotGrid(driver, test);
        }

        ///<summary>
        ///Find Selected Values in Pivot Grid
        ///</summary>
        ///<returns></returns>
        public string[] findSelectedValuesInPivotGrid()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//div[contains(@class, 'pinned-left')]//div[@row-id]"), "Rows not present in Pivot Grid.");

            string selected = "";
            int counter = 0 ;
            while(driver._isElementPresent("xpath", "//cft-pivot-table//div[contains(@class, 'pinned-left')]//div[@row-id = '" + counter + "']"))
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(By.XPath( "//cft-pivot-table//div[contains(@class, 'pinned-left')]//div[@row-id = '" + counter + "']")));
                IList<IWebElement> cellCollection = driver._findElements("xpath", "//cft-pivot-table//div[contains(@class, 'pinned-left')]//div[@row-id = '" + counter + "']/div[@role='gridcell' and contains(@class, 'selected')]/span");
                foreach (IWebElement cell in cellCollection)
                    selected = selected + cell.Text + "-";
                ++counter;
            }

            if(selected != "")
            {
                selected.Remove(selected.Length - 1);
                string[] selectedCells = selected.Split('-');
                Array.Resize(ref selectedCells, selectedCells.Length - 1);

                Results.WriteStatus(test, "Pass", "Found, '" + selectedCells.Length + "' selected cells in Pivot Grid.");
                return selectedCells;
            }
            else
            {
                Results.WriteStatus(test, "Pass", "No selected cells found in Pivot Grid.");
                return new string[0];
            }
        }


    }
}
