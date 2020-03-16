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
    public class TabularGrid
    {
        #region Private Variables

        private IWebDriver tabularGrid;
        private ExtentTest test;
        Home homePage;
        ViewAdPopup viewAdPopup;
        Carousels carousels;
        SummaryTags summaryTags;

        #endregion

        #region Public Methods

        public TabularGrid(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.tabularGrid = driver;
            test = testReturn;
            homePage = new Home(driver, test);
            viewAdPopup = new ViewAdPopup(driver, test);
            carousels = new Carousels(driver, test);
            summaryTags = new SummaryTags(driver, test);
        }

        public IWebDriver driver
        {
            get { return this.tabularGrid; }
            set { this.tabularGrid = value; }
        }

        ///<summary>
        ///Verify Tabular Functionality On Charts
        ///</summary>
        ///<returns></returns>
        public TabularGrid VerifyTabularFunctionalityOnCharts(string chartTitle = "Advertiser Rankings Period over Period")
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//chart//*[name()='text' and @class='highcharts-title']/*[name()='tspan']"), "Titles not present on charts");
            IList<IWebElement> chartTitlesColl = driver._findElements("xpath", "//chart//*[name()='text' and @class='highcharts-title']/*[name()='tspan']");
            int index = 0;
            for (int i = 0; i < chartTitlesColl.Count; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", chartTitlesColl[i]);
                if (chartTitlesColl[i].Text.ToLower().Equals(chartTitle.ToLower()))
                    index = i;
            }

            Assert.IsTrue(driver._waitForElement("xpath", "//chart//chart-export//button[@uib-tooltip='Tabular']"), "Tabular buttons not present on charts.");
            IList<IWebElement> tabularButtonCollection = driver._findElements("xpath", "//chart//chart-export//button[@uib-tooltip='Tabular']");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", tabularButtonCollection[index]);
            tabularButtonCollection[index].Click();

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//span[text()='Bulk actions for your tabular view:']"), "Tabular Grid Bulk Actions Text is not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//a[@dropdowntoggle]"), "Tabular Grid Bulk Actions Button is not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//span[text()='Customize your tabular layout:']"), "Tabular Grid Bulk Options Text is not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//button[@id='viewCustomizerButton']"), "Tabular Grid Bulk Options Button is not present.");

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@role='grid']"), "Tabular Grid is not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class, 'left-header') or contains(@class, 'header-viewport')]"), "Column Headers not present.");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class, 'floating-top')]//div[@role='gridcell']"), "Top Summary Totals Row not present.");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class, 'floating-bottom')]//div[@role='gridcell']"), "Bottom Summary Totals Row not present.");

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-group//div[@role='grid']"), "AgGrid is not present.");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-group//div[@role='grid']//div[@class='ag-header-row']/div[contains(@class, 'cell-sortable')]"), "AgGrid Column Headers are not present.");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-group//div[@class='ag-body-container']//div[@col-id]"), "AgGrid Cells are not present.");

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='NU-card card p-0']"), "Master Details card not present.");

            Results.WriteStatus(test, "Pass", "Verified, Tabular Functionality On Charts.");
            return new TabularGrid(driver, test);
        }

        ///<summary>
        ///Verify Tabular Grid Bulk Actions Button And Choose Option
        ///</summary>
        ///<param name="option">Option to be selected</param>
        ///<returns></returns>
        public TabularGrid VerifyTabularGridBulkActionsButtonAndChooseOption(string option = "", bool recordSelected = false)
        {
            driver._click("xpath", "//cft-tabular-table//a[@dropdowntoggle]");

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//button[contains(@class,'dropdown')]"), "Bulk Actions DDL not present");
            IList<IWebElement> bulkActionButtonColl = driver._findElements("xpath", "//cft-tabular-table//button[contains(@class,'dropdown')]");

            string[] bulkActionNamesColl = new string[] { "Download Grid", "View Selected", "Reset Selected"};

            IWebElement optionButton = null;
            foreach(string bulkActionName in bulkActionNamesColl)
            {
                bool avail = false;
                foreach(IWebElement bulkAction in bulkActionButtonColl)
                {
                    if (bulkAction.Text.ToLower().Contains(bulkActionName.ToLower()))
                    {
                        if (bulkActionName.ToLower().Equals(option.ToLower()))
                            optionButton = bulkAction;
                        if (bulkActionName.ToLower().Contains("selected"))
                        {
                            if (recordSelected)
                            {
                                Assert.IsTrue(bulkAction.GetAttribute("disabled") == null, "'" + bulkActionName + "' is disabled when it should not have been so.");
                                Results.WriteStatus(test, "Pass", "'" + bulkActionName + "' is not disabled when it should not have been so");
                            }
                            else
                            {
                                Assert.IsTrue(bulkAction.GetAttribute("disabled") != null, "'" + bulkActionName + "' is not disabled when it should have been so.");
                                Results.WriteStatus(test, "Pass", "'" + bulkActionName + "' is disabled when it should have been so");
                            }
                        }
                        avail = true;
                        break;
                    }
                }
                Assert.IsTrue(avail, "'" + bulkActionName + "' not found in Bulk Action DDL.");
            }

            if(optionButton != null)
            {
                optionButton.Click();
                Results.WriteStatus(test, "Pass", "Clicked, '" + option + "' option from Tabular Grid Bulk Actions.");
                Thread.Sleep(15000);
            }

            Results.WriteStatus(test, "Pass", "Verified, Tabular Grid Bulk Actions Button.");
            return new TabularGrid(driver, test);
        }

        ///<summary>
        ///Select/Deselect Columns From Tabular Options Button
        ///</summary>
        ///<param name="columns">Names of columns to be selected</param>
        ///<param name="select">To select or deselect</param>
        ///<returns></returns>
        public TabularGrid Select_DeselectColumnsFromTabularOptionsButton(string[] columns , bool select = true)
        {
            driver._click("xpath", "//cft-tabular-table//button[@id='viewCustomizerButton']");

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@ref='eToolPanelColumnsContainerComp']/div"), "Tabular Options DDL not present.");
            IList<IWebElement> tabularOptionsColl = driver._findElements("xpath", "//div[@ref='eToolPanelColumnsContainerComp']/div");

            foreach(string column in columns)
            {
                bool avail = false;
                for(int i = 0; i < tabularOptionsColl.Count; i++)
                {
                    IList<IWebElement> checkboxColl = driver._findElements("xpath", "//div[@ref='eToolPanelColumnsContainerComp']/div[" + (i + 1) + "]//span[@role and contains(@class, 'checked') and not(contains(@class, 'hidden'))]");
                    IList<IWebElement> labelColl = driver._findElements("xpath", "//div[@ref='eToolPanelColumnsContainerComp']/div[" + (i + 1) + "]//span[@ref='eLabel']");
                    if (labelColl[0].Text.ToLower().Equals(column.ToLower()))
                    {
                        if (checkboxColl[0].GetAttribute("class").Contains("checkbox-checked"))
                        {
                            if (!select)
                            {
                                checkboxColl[0].Click();
                                Thread.Sleep(1000);
                                if(i == 0 || i == tabularOptionsColl.Count - 1)
                                {
                                    checkboxColl = driver._findElements("xpath", "//div[@ref='eToolPanelColumnsContainerComp']/div[" + (i + 1) + "]//span[@role and contains(@class, 'checked') and not(contains(@class, 'hidden'))]");
                                    Assert.IsTrue(checkboxColl[0].GetAttribute("class").Contains("checkbox-checked"), "'" + column + "' got deselected");
                                    Results.WriteStatus(test, "Pass", "Verified, Key Parent '" + column + "' is not deselected.");
                                }
                                for (int j = i; j < tabularOptionsColl.Count - 1; j++)
                                {
                                    if (j == 0)
                                        ++j;
                                    checkboxColl = driver._findElements("xpath", "//div[@ref='eToolPanelColumnsContainerComp']/div[" + (j + 1) + "]//span[@role and contains(@class, 'checked') and not(contains(@class, 'hidden'))]");
                                    Assert.IsTrue(checkboxColl[0].GetAttribute("class").Contains("checkbox-unchecked"), "'" + column + "' did not get deselected");
                                }
                            }
                            else
                                Results.WriteStatus(test, "Pass", "'" + column + "' is already deselected.");
                        }
                        else
                        {
                            if (select)
                            {
                                checkboxColl[0].Click();
                                Thread.Sleep(1000);
                                for (int j = 0; j <= i; j++)
                                {
                                    checkboxColl = driver._findElements("xpath", "//div[@ref='eToolPanelColumnsContainerComp']/div[" + (j + 1) + "]//span[@role and contains(@class, 'checked') and not(contains(@class, 'hidden'))]");
                                    Assert.IsTrue(checkboxColl[0].GetAttribute("class").Contains("checkbox-checked"), "'" + column + "' did not get selected");
                                }
                            }
                            else
                                Results.WriteStatus(test, "Pass", "'" + column + "' is already selected.");
                        }
                        avail = true;
                        break;
                    }
                }
                Assert.IsTrue(avail, "'" + column + "' not found in Tabular Options DDL");
            }

            driver._click("xpath", "//cft-tabular-table//button[@id='viewCustomizerButton']");
            Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[@ref='eToolPanelColumnsContainerComp']/div"), "Tabular Options DDL did not collapse.");


            Results.WriteStatus(test, "Pass", "Verified, Tabular Functionality On Charts.");
            return new TabularGrid(driver, test);
        }

        ///<summary>
        ///Capture Data From Tabular Grid
        ///</summary>
        ///<returns></returns>
        public string[,] CaptureDataFromTabularGrid()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable')]"), "'Column Headers' not present");
            IList<IWebElement> columnHeaderColl = driver._findElements("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable')]");
            int dimension1 = columnHeaderColl.Count;
            string[,] dataGrid = new string[6, dimension1];

            for(int i = 0; i < 5; i++)
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']/div[not(contains(@style, 'display: none'))]//div[@row-index='" + i + "']/div"), "Row " + (i + 1) + " not present");
                driver._scrollintoViewElement("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']/div[not(contains(@style, 'display: none'))]//div[@row-index='" + i + "']/div");
                IList<IWebElement> cellColl = driver._findElements("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']/div[not(contains(@style, 'display: none'))]//div[@row-index='" + i + "']/div/span");
                for (int j = 0; j < cellColl.Count; j++)
                    dataGrid[i + 1, j] = cellColl[j].Text;
            }

            for (int i = 0; i < columnHeaderColl.Count; i++)
                dataGrid[0, i] = columnHeaderColl[i].Text;

            for (int i = 0; i < dataGrid.GetLength(0); i++)
            {
                for (int j = 0; j < dataGrid.GetLength(1); j++)
                {
                    if (dataGrid[i, j].Contains("$"))
                        dataGrid[i, j] = dataGrid[i, j].Substring(1);
                    Console.Write(dataGrid[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Results.WriteStatus(test, "Pass", "Captured, Data From Tabular Grid");
            return dataGrid;
        }

        ///<summary>
        ///Verify Data From Tabular Grid In Exported Excel File
        ///</summary>
        ///<param name="dataGrid">Data Captured from tabular grid</param>
        ///<param name="fileName">Name of Excel File</param>
        ///<returns></returns>
        public TabularGrid VerifyDataFromTabularGridInExportedExcelFile(string fileName, string[,] dataGrid)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            int rw = 0;
            int cl = 0;
            int flag = 1;
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
                if (xlWorkSheet.Name.Contains(" Online Tabular Report"))
                    break;
            }

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;
            
            for(int i = 0, rCnt = 4; i < dataGrid.GetLength(0); i++, rCnt++)
            {
                bool avail = false;
                Console.Write("\n(" + i + ", 0):\t" + dataGrid[i, 0] + " - " + (range.Cells[rCnt, 1] as Excel.Range).Text + "\t");
                if(dataGrid[i, 0].ToLower().Equals((range.Cells[rCnt, 1] as Excel.Range).Text.ToLower()))
                {
                    flag = 1;
                    for (int j = 1, cCnt = 2; j < dataGrid.GetLength(1); j++, cCnt++)
                    {
                        if (cCnt == 5)
                            cCnt = 7;
                        Console.Write("(" + i + ", " + j + "):\t" + dataGrid[i, j] + " - " + (range.Cells[rCnt, cCnt] as Excel.Range).Text + "\t");
                        if (!(range.Cells[rCnt, cCnt] as Excel.Range).Text.ToLower().Contains(dataGrid[i, j].ToLower()))
                        {
                            --flag;
                            break;
                        }
                    }
                    if(flag > 0)
                    {
                        avail = true;
                        break;
                    }
                }
                Assert.IsTrue(avail, "Row '" + i + "' of Data Captured from Tabular Grid not found in Excel File.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Data from chart '' in exported file");
            return new TabularGrid(driver, test);
        }

        ///<summary>
        ///Find Values to Select Records From Pivot Grid
        ///</summary>
        ///<param name="num">No. of Selections</param>
        ///<param name="oneColumn">All Selections from one column</param>
        ///<param name="oneRow">All Selections from one row</param>
        ///<returns></returns>
        public string FindValuesToSelectRecordsFromGrid(bool oneColumn, bool oneRow, int num = 2)
        {
            string collectionXPath = "", colId = "", rowId = "", value = "";
            int x = -1;
            Random rand = new Random();
            if (oneColumn)
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//ag-grid-angular//div[@row-index='0']//div[@col-id]"), "Columns not present in Tabular Grid");
                IList<IWebElement> columnCollection = driver._findElements("xpath", "//cft-tabular-table//ag-grid-angular//div[@row-index='0']//div[@col-id]");
                x = rand.Next(0, columnCollection.Count);
                colId = columnCollection[x].GetAttribute("col-id");
                collectionXPath = "//cft-tabular-table//ag-grid-angular//div[@row-id]//div[@col-id='" + colId + "' and contains(@class, 'selectable') and not(contains(@class, 'selected'))]";
            }
            else if (oneRow)
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//ag-grid-angular//div[@role='row' and @row-index='0']"), "Text Field Rows not present in Tabular Grid");
                IList<IWebElement> rowCollection = driver._findElements("xpath", "//cft-tabular-table//ag-grid-angular//div[@role='row' and @row-id]");
                x = rand.Next(0, rowCollection.Count);
                rowId = rowCollection[x].GetAttribute("row-id");
                collectionXPath = "//cft-tabular-table//ag-grid-angular//div[@role='row' and @row-id='" + rowId + "']//div[@col-id and contains(@class, 'selectable') and not(contains(@class, 'selected'))]";
            }
            else
                collectionXPath = "//cft-tabular-table//div[@role='row' and @row-id]//div[@col-id and contains(@class, 'selectable') and not(contains(@class, 'selected'))]";

            driver._scrollintoViewElement("xpath", collectionXPath);

            for (int i = 0; i < num; i++)
            {
                IList<IWebElement> collection = driver._findElements("xpath", collectionXPath);
                x = rand.Next(0, 4);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", collection[x]);
                value = collection[x].Text;
                collection[x].Click();
                Thread.Sleep(2000);
            }

            if (rowId != "")
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//ag-grid-angular//div[@role='row' and @row-id='" + rowId + "']//div[contains(@class,'selected')]"), "'Cell' not selected in tabular grid.");
                IList<IWebElement> selectedCells = driver._findElements("xpath", "//cft-tabular-table//ag-grid-angular//div[@role='row' and @row-id='" + rowId + "']//div[contains(@class,'selected')]");
                Assert.AreEqual(1, selectedCells.Count, "More than one cells are selected from one row.");
                Assert.IsTrue(selectedCells[0].GetCssValue("background").Contains("rgba(0, 168, 184, 0.7)"), "Selected Cell is not of teal color.");
            }
            else
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table/div[not(contains(@style, 'none'))]//div[@role='row']//div[contains(@class,'selected')]"), "'Cell' not selected in tabular grid.");
                IList<IWebElement> selectedCells = driver._findElements("xpath", "//cft-tabular-table/div[not(contains(@style, 'none'))]//div[@role='row']//div[contains(@class,'selected')]");
                Assert.AreEqual(num, selectedCells.Count, "'" + num + "' cells did not get selected.");
                foreach (IWebElement selected in selectedCells)
                    Assert.IsTrue(selected.GetCssValue("background").Contains("rgba(0, 168, 184, 0.7)"), "Selected Cell is not of teal color.");
            }

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[contains(@class, 'floating')]//div[@role='gridcell']"), "Summary Total Row not present");
            IList<IWebElement> summaryTotalsColl = driver._findElements("xpath", "//cft-tabular-table//div[contains(@class, 'floating')]//div[@role='gridcell']");
            x = rand.Next(0, summaryTotalsColl.Count);
            summaryTotalsColl[x].Click();
            Assert.IsFalse(driver._waitForElement("xpath", "//cft-tabular-table//div[contains(@class, 'floating')]//div[@role='gridcell' and contains(@class, 'selected')]"), "Summary Totals Row Cell got selected.");
            Results.WriteStatus(test, "Pass", "Verified, Summary Totals Row Cell don't get selected.");

            Results.WriteStatus(test, "Pass", "Selected, '" + num + "' values from Tabular Grid.");
            return value;
        }

        ///<summary>
        ///Verify Sorting Functionality on Tabular Grid
        ///</summary>
        ///<param name="ascending">Sorting Order</param>
        ///<param name="columnName">Column to be sorted</param>
        ///<returns></returns>
        public TabularGrid VerifySortingFunctionalityOnTabularGrid(string columnName = "", bool ascending = true)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable')]"), "'Column Headers' not present");
            IList<IWebElement> columnHeaderColl = driver._findElements("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable')]");

            string[] columnNameCollection = new string[columnHeaderColl.Count];
            string colId = "";
            if(columnName == "")
            {
                Random rand = new Random();
                int x = rand.Next(0, columnHeaderColl.Count);
                columnName = columnHeaderColl[x].Text;
            }

            for(int i = 0; i < columnHeaderColl.Count; i++)
            {
                IList<IWebElement> labelCollection = columnHeaderColl[i]._findElementsWithinElement("xpath", ".//span[@ref='eText']");
                Assert.AreEqual(1, labelCollection.Count, "Column Name not present in Column Headers");
                columnNameCollection[i] = labelCollection[0].Text;
                if (labelCollection[0].Text.ToLower().Equals(columnName.ToLower()))
                    colId = columnHeaderColl[i].GetAttribute("col-id");
            }
            Assert.AreNotEqual("", colId, "'" + columnName + "' not found in columns");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//span[contains(@class, 'sort') and not(contains(@class, 'hidden'))]"), "Sorting icon not present on column '" + columnName + "'");
            IWebElement sortIcon = driver._findElement("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//span[contains(@class, 'sort') and not(contains(@class, 'hidden'))]");

            if (ascending)
            {
                while (!sortIcon.GetAttribute("ref").Equals("eSortAsc"))
                {
                    sortIcon.Click();
                    Thread.Sleep(2000);
                    sortIcon = driver._findElement("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//span[contains(@class, 'sort') and not(contains(@class, 'hidden'))]");
                }
            }
            else
            {
                while (!sortIcon.GetAttribute("ref").Equals("eSortDesc"))
                {
                    sortIcon.Click();
                    Thread.Sleep(2000);
                    sortIcon = driver._findElement("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//span[contains(@class, 'sort') and not(contains(@class, 'hidden'))]");
                }
            }

            string[] sortedCells = new string[5];
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@row-index]//div[@col-id='" + colId + "' and @role='gridcell']"), "'" + columnName + "' column cells not present.");
            for(int i = 0; i < 5; i++)
                sortedCells[i] = driver._getText("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@row-index='" + i + "']//div[@col-id='" + colId + "' and @role='gridcell']/span");

            string[] sortedColumnHeaders = new string[columnNameCollection.Length];
            Array.Copy(columnNameCollection, sortedColumnHeaders, columnNameCollection.Length);
            Array.Sort(sortedColumnHeaders);
            Assert.IsTrue(columnNameCollection.SequenceEqual(columnNameCollection), "Column Headers are not displayed in alphabetical order.");
            Results.WriteStatus(test, "Pass", "Verified, Columns of the Tabular Grid are displayed in Alphabetical order");

            if (sortedCells[0].Contains("$"))
            {
                int[] numberList = new int[sortedCells.Length];
                for(int i = 0; i < sortedCells.Length; i++)
                {
                    sortedCells[i] = sortedCells[i].Substring(1);
                    while (sortedCells[i].IndexOf(",") > -1)
                        sortedCells[i] = sortedCells[i].Remove(sortedCells[i].IndexOf(","), 1);
                    Assert.IsTrue(int.TryParse(sortedCells[i], out numberList[i]), "'" + sortedCells[i] + "' was not converted to int.");
                }
                int[] sortedNumberList = new int[numberList.Length];
                Array.Copy(numberList, sortedNumberList, numberList.Length);
                Array.Sort(sortedNumberList);
                if (!ascending)
                    Array.Reverse(sortedNumberList);
                Assert.IsTrue(numberList.SequenceEqual(sortedNumberList), "Sorting was unsuccessful on column 'Media Month 12'.");
            }
            else
            {
                string[] originalSortedCells = new string[sortedCells.Length];
                Array.Copy(sortedCells, originalSortedCells, sortedCells.Length);
                Array.Sort(sortedCells);
                if (!ascending)
                    Array.Reverse(sortedCells);
                Assert.IsTrue(sortedCells.SequenceEqual(originalSortedCells), "Sorting was unsuccessful on column '" + columnName + "'.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Sorting Functionality on Tabular Grid");
            return new TabularGrid(driver, test);
        }

        ///<summary>
        ///Verify Filter Functionality On Text Columns in Tabular Grid
        ///</summary>
        ///<param name="columnName">Name of Column to apply filter to</param>
        ///<param name="filterValue">Filter Value</param>
        ///<param name="reset">Whether to remove filter</param>
        ///<returns></returns>
        public string VerifyFilterFunctionalityOnTextColumnsInTabularGrid(string columnName = "", string filterValue = "", bool reset = true)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'pinned-left-header')]//div[contains(@class,'ag-header-cell-sortable')]"), "'Column Headers' not present");
            IList<IWebElement> columnHeaderColl = driver._findElements("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'pinned-left-header')]//div[contains(@class,'ag-header-cell-sortable')]");

            string[] columnNameCollection = new string[columnHeaderColl.Count];
            string colId = "";
            if (columnName == "")
            {
                Random rand = new Random();
                int x = rand.Next(0, columnHeaderColl.Count);
                columnName = columnHeaderColl[x].Text;
            }

            for (int i = 0; i < columnHeaderColl.Count; i++)
            {
                IList<IWebElement> labelCollection = columnHeaderColl[i]._findElementsWithinElement("xpath", ".//span[@ref='eText']");
                Assert.AreEqual(1, labelCollection.Count, "Column Name not present in Column Headers");
                if (labelCollection[0].Text.ToLower().Equals(columnName.ToLower()))
                {
                    colId = columnHeaderColl[i].GetAttribute("col-id");
                    break;
                }
            }
            Assert.AreNotEqual("", colId, "'" + columnName + "' not found in columns");

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@row-index='0']//div[@col-id='" + colId + "' and @role='gridcell']"), "'" + columnName + "' column cells not present.");
            IList<IWebElement> cellCollection = driver._findElements("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@col-id='" + colId + "' and @role='gridcell']");
            if (filterValue == "")
            {
                Random rand = new Random();
                int x = rand.Next(0, cellCollection.Count);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", cellCollection[x]);
                filterValue = cellCollection[x].Text;
            }

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//span[@ref='eMenu']"), "Filter icon not present on column '" + columnName + "'");
            driver._click("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//span[@ref='eMenu']");

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']"), "Filter Menu not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-filter']//input"), "Search Textbox not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-filter']//label[@id='selectAllContainer']"), "'Select All' option not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-filter']//div[@id='richList']//label"), "Filter Options not present.");

            bool avail = false;
            driver._click("xpath", "//div[@class='ag-filter']//label[@id='selectAllContainer']");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']//label[@id='selectAllContainer']//*[name()='svg' and @data-icon='square']"), "'Select All' checkbox did not get deselected.");
            driver._type("xpath", "//div[@class='ag-filter']//input", filterValue);
            Thread.Sleep(500);
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']//div[@id='richList']//label"), "Filter Value '" + filterValue + "' not present");
            IList<IWebElement> filteredOptions = driver._findElements("xpath", "//div[@class='ag-filter']//div[@id='richList']//label");
            foreach(IWebElement option in filteredOptions)
                if (option.Text.ToLower().Equals(filterValue.ToLower()))
                {
                    option.Click();
                    Thread.Sleep(1000);
                    avail = true;
                    break;
                }
            Assert.IsTrue(avail, "Filter Value '" + filterValue + "' not found in filtered options.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//div[contains(@class, 'filter-applied')]"), "Applied Filter icon not present on column '" + columnName + "'");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@row-index]//div[@col-id='" + colId + "' and @role='gridcell']"), "Cells in column '" + columnName + "' not present.");
            cellCollection = driver._findElements("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@row-index]//div[@col-id='" + colId + "' and @role='gridcell']");

            avail = true;
            for(int i = 0; i < cellCollection.Count; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", cellCollection[i]);
                if (!cellCollection[i].Text.ToLower().Equals(filterValue.ToLower()))
                {
                    avail = false;
                    break;
                }
                if (i == 4)
                    break;
            }
            Assert.IsTrue(avail, "Filter Value '" + filterValue + "' not applied to column '" + columnName + " successfully'.");

            if (reset)
            {
                driver._click("xpath", "//div[@class='ag-filter']//label[@id='selectAllContainer']");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']//label[@id='selectAllContainer']//*[name()='svg' and @data-icon='check-square']"), "'Select All' checkbox did not get selected.");
                driver._clearText("xpath", "//div[@class='ag-filter']//input");
                Assert.IsFalse(driver._waitForElement("xpath", "//div[@class='ag-filter']//div[@id='richList']//*[name()='svg' and @data-icon='square']"), "All checkboxes did not get selected.");

                Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//div[contains(@class, 'filter-applied')]"), "Applied Filter icon still present on column '" + columnName + "'");
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@row-index]//div[@col-id='" + colId + "' and @role='gridcell']"), "Cells in column '" + columnName + "' not present.");
                cellCollection = driver._findElements("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@row-index]//div[@col-id='" + colId + "' and @role='gridcell']");

                avail = true;
                foreach (IWebElement cell in cellCollection)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", cell);
                    if (!cell.Text.ToLower().Equals(filterValue.ToLower()))
                    {
                        avail = false;
                        break;
                    }
                }
                Assert.IsFalse(avail, "Filter Value '" + filterValue + "' not removed from column '" + columnName + " successfully'.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Filter Functionality On Text Columns in Tabular Grid");
            return filterValue;
        }

        ///<summary>
        ///Verify Filter Functionality On Numeric Columns in Tabular Grid
        ///</summary>
        ///<param name="columnName">Name of Column to apply filter to</param>
        ///<param name="filterValue">Filter Value</param>
        ///<param name="reset">Whether to remove filter</param>
        ///<param name="filterType">Type of Filter to be applied</param>
        ///<returns></returns>
        public string VerifyFilterFunctionalityOnNumericColumnsInTabularGrid(string columnName = "", string filterValue = "", string filterType = "Equals")
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@class='ag-header-viewport']//div[contains(@class,'ag-header-cell-sortable')]"), "'Column Headers' not present");
            IList<IWebElement> columnHeaderColl = driver._findElements("xpath", "//cft-tabular-table//div[@class='ag-header-viewport']//div[contains(@class,'ag-header-cell-sortable')]");

            string[] columnNameCollection = new string[columnHeaderColl.Count];
            string colId = "";
            if (columnName == "")
            {
                Random rand = new Random();
                int x = rand.Next(0, columnHeaderColl.Count);
                columnName = columnHeaderColl[x].Text;
            }

            for (int i = 0; i < columnHeaderColl.Count; i++)
            {
                IList<IWebElement> labelCollection = columnHeaderColl[i]._findElementsWithinElement("xpath", ".//span[@ref='eText']");
                Assert.AreEqual(1, labelCollection.Count, "Column Name not present in Column Headers");
                if (labelCollection[0].Text.ToLower().Equals(columnName.ToLower()))
                {
                    colId = columnHeaderColl[i].GetAttribute("col-id");
                    break;
                }
            }
            Assert.AreNotEqual("", colId, "'" + columnName + "' not found in columns");

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@row-index='0']//div[@col-id='" + colId + "' and @role='gridcell']"), "'" + columnName + "' column cells not present.");
            IList<IWebElement> cellCollection = driver._findElements("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@col-id='" + colId + "' and @role='gridcell']");
            if (filterValue == "")
            {
                Random rand = new Random();
                int x = rand.Next(0, cellCollection.Count);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", cellCollection[x]);
                filterValue = cellCollection[x].Text;
                if (filterValue.Contains("$"))
                    filterValue = filterValue.Substring(1);
                while (filterValue.IndexOf(",") > -1)
                    filterValue = filterValue.Remove(filterValue.IndexOf(","), 1);
            }

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//div[@class='ag-header-viewport']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//span[@ref='eMenu']"), "Filter icon not present on column '" + columnName + "'");
            if(!driver._isElementPresent("xpath", "//div[@class='ag-filter']"))
                driver._click("xpath", "//cft-tabular-table//div[@class='ag-header-viewport']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//span[@ref='eMenu']");

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']"), "Filter Menu not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-filter']//input[@id='filterText']"), "Search Textbox not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-filter']//select"), "'FIlter Type DDL' option not present.");
            driver._click("xpath", "//div[@class='ag-filter']//select");
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.XPath("//div[@class='ag-filter']//select"))).Click().Perform();
            action.MoveByOffset(2, 2).Perform();

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-filter']//select/option"), "Filter Type DDL not present");
            IList<IWebElement> filterTypeDDLColl = driver._findElements("xpath", "//div[@class='ag-filter']//select/option");

            bool avail = false;
            foreach (IWebElement filter in filterTypeDDLColl)
                if (filter.Text.ToLower().Equals(filterType.ToLower()))
                {
                    filter.Click();
                    avail = true;
                    Thread.Sleep(500);
                    break;
                }
            Assert.IsTrue(avail, "'" + filterType + "' not found in Filter Type DDL");

            driver._type("xpath", "//div[@class='ag-filter']//input[@id='filterText']", filterValue);
            Thread.Sleep(1000);
            decimal dFilterVal = 0;

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@row-index='0']//div[@col-id='" + colId + "' and @role='gridcell']"), "'" + columnName + "' column cells not present.");
            cellCollection = driver._findElements("xpath", "//cft-tabular-table//div[@class='ag-body ag-row-no-animation']//div[@col-id='" + colId + "' and @role='gridcell']");

            if (decimal.TryParse(filterValue, out dFilterVal))
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//div[@class='ag-header-viewport']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//div[contains(@class, 'filter-applied')]"), "Applied Filter icon not present on column '" + columnName + "'");
                for (int i = 0; i < cellCollection.Count; i++)
                {
                    if (i == 5)
                        break;
                    string currStringValue = cellCollection[i].Text;
                    decimal dCurrValue = 0;
                    if (currStringValue.Contains("$"))
                        currStringValue = currStringValue.Substring(1);
                    while (currStringValue.IndexOf(",") > -1)
                        currStringValue = currStringValue.Remove(currStringValue.IndexOf(","), 1);
                    Assert.IsTrue(decimal.TryParse(currStringValue, out dCurrValue), "Value '" + currStringValue + "' in row " + (i + 1) + " couldn't be converted decimal");

                    if (filterType.ToLower().Equals("equals"))
                        Assert.AreEqual(dCurrValue, dFilterVal, "Filter Value '" + filterValue + "' not applied to column '" + columnName + "' successfully.");
                    else if (filterType.ToLower().Equals("not equal"))
                        Assert.AreNotEqual(dCurrValue, dFilterVal, "Filter Value '" + filterValue + "' not applied to column '" + columnName + "' successfully.");
                    else if (filterType.ToLower().Equals("less than"))
                        Assert.Less(dCurrValue, dFilterVal, "Filter Value '" + filterValue + "' not applied to column '" + columnName + "' successfully.");
                    else if (filterType.ToLower().Equals("less than or equals"))
                        Assert.LessOrEqual(dCurrValue, dFilterVal, "Filter Value '" + filterValue + "' not applied to column '" + columnName + "' successfully.");
                    else if (filterType.ToLower().Equals("greater than"))
                        Assert.Greater(dCurrValue, dFilterVal, "Filter Value '" + filterValue + "' not applied to column '" + columnName + "' successfully.");
                    else if (filterType.ToLower().Equals("greater than or equals"))
                        Assert.GreaterOrEqual(dCurrValue, dFilterVal, "Filter Value '" + filterValue + "' not applied to column '" + columnName + "' successfully.");
                    else if (filterType.ToLower().Equals("in range"))
                    {
                        decimal toFilter = dFilterVal + 1000;
                        Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-filter']//input[@id='filterToText']"), "To Filter Textbox not present.");
                        driver._type("xpath", "//div[@class='ag-filter']//input[@id='filterToText']", toFilter.ToString());
                        Thread.Sleep(1000);
                        Assert.LessOrEqual(dCurrValue, toFilter, "Filter Value '" + toFilter + "' not applied to column '" + columnName + "' successfully.");
                        Assert.GreaterOrEqual(dCurrValue, dFilterVal, "Filter Value '" + filterValue + "' not applied to column '" + columnName + "' successfully.");
                    }
                }
            }
            else
            {
                Assert.IsFalse(driver._isElementPresent("xpath", "//cft-tabular-table//div[@class='ag-header-viewport']//div[contains(@class,'ag-header-cell-sortable') and @col-id='" + colId + "']//div[contains(@class, 'filter-applied')]"), "Applied Filter icon is present on column '" + columnName + "' for character filter value.");
                Results.WriteStatus(test, "Pass", "Verified, Character Values do not apply as filter on Numeric Columns.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Filter Functionality for filter type '" + filterType + "' On Numeric Columns in Tabular Grid");
            return filterValue;
        }

        ///<summary>
        ///Verify Mouse Pointer When Hover over zero/non-zero elements
        ///</summary>
        ///<returns></returns>
        public TabularGrid VerifyMousePointerWhenHoverOver0_Non0Elements()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//div[@role = 'gridcell']"), "Cells not present in Tabular Grid.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//div[@class='ag-body-viewport']//div[@role = 'row' and @row-index='0']"), "Rows not present in Tabular Grid");
            IList<IWebElement> rowsCollection = driver._findElements("xpath", "//cft-tabular-table//div[@class='ag-body-viewport']//div[@role = 'row']");

            Actions action = new Actions(driver);
            bool foundSelectable = false, foundZero = false;
            for (int i = 0; i < 10; i++)
            {
                driver._scrollintoViewElement("xpath", "//cft-tabular-table//div[@class='ag-body-viewport']//div[@role = 'row'][" + (i + 1) + "]");
                IList<IWebElement> selectableCellCollection = driver._findElements("xpath", "//cft-tabular-table//div[@class='ag-body-viewport']//div[@role = 'row'][" + (i + 1) + "]//div[@role = 'gridcell' and contains(@class, 'selectable')]");
                if (selectableCellCollection.Count > 0 && !foundSelectable)
                {
                    action.MoveToElement(selectableCellCollection[0]).MoveByOffset(1, 0).Perform();
                    Thread.Sleep(500);
                    Assert.AreEqual("pointer", selectableCellCollection[0].GetCssValue("cursor"), "Cursor did not change to 'pointer' for non-zero cell.");
                    foundSelectable = true;
                    Assert.IsTrue(driver._findElement("xpath", "//cft-tabular-table//div[@class='ag-body-viewport']//div[@role = 'row'][" + (i + 1) + "]").GetCssValue("background-color").Contains("rgba(236, 240, 241, 1)"), "Row containing the cell on which mouse is hovered did not change appearance as highlighted.");
                }

                IList<IWebElement> nonSelectableCellCollection = driver._findElements("xpath", "//cft-tabular-table//div[@class='ag-body-viewport']//div[@role = 'row'][" + (i + 1) + "]//div[@role = 'gridcell' and not(contains(@class, 'selectable'))]");
                if (nonSelectableCellCollection.Count > 0 && !foundZero)
                {
                    action.MoveToElement(nonSelectableCellCollection[0]).MoveByOffset(1, 0).Perform();
                    Thread.Sleep(500);
                    Assert.AreEqual("not-allowed", nonSelectableCellCollection[0].GetCssValue("cursor"), "Cursor did not change to 'not-allowed' for non-zero cell.");
                    nonSelectableCellCollection[0].Click();
                    Thread.Sleep(1000);
                    Assert.IsFalse(driver._waitForElement("xpath", "//cft-tabular-table//ag-grid-angular//div[contains(@class,'ag-body ag-row')]/div[not(contains(@style, 'none'))]//div[@role='row' and @row-id='" + i + "']//div[contains(@class,'selected')]"), "'Non-zero Cell' got selected in pivot grid.");
                    foundZero = true;
                }

                if (foundSelectable && foundZero)
                    break;
            }
              
            if (foundSelectable)
                Results.WriteStatus(test, "Pass", "Verified, Mouse Pointer When Hover over non-zero elements");
            if (foundZero)
                Results.WriteStatus(test, "Pass", "Verified, Mouse Pointer When Hover over zero-valued elements");

            return new TabularGrid(driver, test);
        }

        ///<summary>
        ///Verify Filter Applied on Table Grid View By Selecting Pivot Grid Cell
        ///</summary>
        ///<param name="filterValue">Value to be selected in filter</param>
        ///<returns></returns>
        public TabularGrid VerifyFilterAppliedOnTableGridViewBySelectingTabularGridCell(string filterValue = "", bool reset = false)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-group//div[@class='ag-body-viewport']//div[contains(@col-id,'advertiserName')]"), "Values not present in AgGrid.");
            IList<IWebElement> gridValueCol = driver._findElements("xpath", "//cft-domain-item-group//div[@class='ag-body-viewport']//div[contains(@col-id,'advertiserName')]");
            bool avail = true;
            foreach (IWebElement gridValue in gridValueCol)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", gridValue);
                if (!gridValue.Text.ToLower().Contains(filterValue.ToLower()))
                {
                    avail = false;
                    break;
                }
            }
            Assert.IsTrue(avail, "'" + filterValue + "' not applied as filter successfully.");

            if (reset)
            {
                VerifyTabularGridBulkActionsButtonAndChooseOption("Reset Selected", true);

                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-group//div[@class='ag-body-viewport']//div[contains(@col-id,'advertiserName')]"), "Values not present in AgGrid.");
                driver._scrollintoViewElement("xpath", "//cft-domain-item-group//div[@class='ag-body-viewport']//div[contains(@col-id,'advertiserName')]");
                gridValueCol = driver._findElements("xpath", "//cft-domain-item-group//div[@class='ag-body-viewport']//div[contains(@col-id,'advertiserName')]");
                avail = true;
                foreach (IWebElement gridValue in gridValueCol)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", gridValue);
                    if (!gridValue.Text.ToLower().Contains(filterValue.ToLower()))
                    {
                        avail = false;
                        break;
                    }
                }
                Assert.IsFalse(avail, "'" + filterValue + "' not removed as filter successfully.");

            }

            Results.WriteStatus(test, "Pass", "Verified, AgGrid As Per Selected Bar From Expanded Chart");
            return new TabularGrid(driver, test);
        }

        ///<summary>
        ///Verify Media Month Columns For Applied Date Filter
        ///</summary>
        ///<returns></returns>
        public TabularGrid VerifyMediaMonthColumnsForAppliedDateFilter()
        {
            string[] summary = summaryTags.captureSummaryTagsFromDashboard();
            string dateFilter = "";
            int noOfMediaColumns = 1, startMonth = 0, startYear = 0, endMonth = 0, endYear = 2019, lastColId = 201912;
            foreach(string tag in summary)
                if(tag.ToLower().Contains("month") || tag.ToLower().Contains("year") || tag.ToLower().Contains("//"))
                {
                    dateFilter = tag;
                    break;
                }
            Assert.AreNotEqual(dateFilter, "", "Date Filter not found in Summary Tags.");

            if (dateFilter.ToLower().Equals("last year") || dateFilter.ToLower().Equals("year to date"))
                noOfMediaColumns = 12;
            else if (dateFilter.ToLower().Contains("month"))
            {
                if(!dateFilter.ToLower().Equals("last month"))
                {
                    string sNum = dateFilter.Substring(5, dateFilter.IndexOf("month") - 1);
                    Assert.IsTrue(int.TryParse(sNum, out noOfMediaColumns), "Could not convert '" + sNum + "' to integer.");
                }
            }
            else
            {
                string sDate = dateFilter.Substring(0, dateFilter.IndexOf(" to "));
                DateTime date = DateTime.Today;
                Assert.IsTrue(DateTime.TryParse(sDate, out date), "Could not convert '" + sDate + "' to Date");
                startMonth = date.Month;
                startYear = date.Year;
                sDate = dateFilter.Substring(dateFilter.IndexOf(" to ") + 4);
                Assert.IsTrue(DateTime.TryParse(sDate, out date), "Could not convert '" + sDate + "' to Date");
                endMonth = date.Month;
                endYear = date.Year;
                if (startYear == endMonth)
                    noOfMediaColumns = endMonth - startMonth;
                else
                {
                    int noOfYearDiff = endYear - startYear;
                    if (noOfYearDiff == 1)
                        noOfMediaColumns = endMonth + (12 - startMonth);
                    else
                        noOfMediaColumns = (endMonth + (12 - startMonth)) + (12 * noOfYearDiff);
                }
                lastColId = endYear * 100 + endMonth;
            }

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@class='ag-header-viewport']//div[@col-id]"), "Media Columns not present.");

            string[] colIds = new string[noOfMediaColumns];
            int currColId = lastColId, currYear = endYear;
            colIds[colIds.Length - 1] = lastColId.ToString();
            for(int i = noOfMediaColumns - 2; i >= 0; i--)
            {
                --currColId;
                if(currColId % currYear == 100)
                {
                    --currYear;
                    currColId = currYear * 100 + 12;
                }
                colIds[i] = currColId.ToString();
            }

            for (int i = 0; i < colIds.Length; i++)
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-tabular-table//div[@class='ag-header-viewport']//div[@col-id='" + colIds[i] + "']"), "Media Month " + (i + 1) + " column not present.");
                Assert.IsTrue(driver._getText("xpath", "//cft-tabular-table//div[@class='ag-header-viewport']//div[@col-id='" + colIds[i] + "']").ToLower().Contains("media month " + (i + 1)), "Media Month " + (i + 1) + " column name does not match.");
                ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('ag-body-viewport')[0].scrollLeft+=100", "");
            }

            Results.WriteStatus(test, "Pass", "Verified, Media Month Columns For Applied Date Filter");
            return new TabularGrid(driver, test);
        }

        ///<summary>
        ///Verify That Data Columns From Tabular Grid Are As Per Chart Data
        ///</summary>
        ///<returns></returns>
        public TabularGrid VerifyThatDataColumnsFromTabularGridAreAsPerChartData(string chartTitle)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class, 'header-viewport')]"), "Data Column Headers not present.");
            IList<IWebElement> dataColumnsHeadersColl = driver._findElements("xpath", "//cft-tabular-table//div[@role='grid']//div[contains(@class, 'header-viewport')]");
            string[]dataColumnNamesList = new string[dataColumnsHeadersColl.Count];

            for (int i = 0; i < dataColumnsHeadersColl.Count; i++)
                dataColumnNamesList[i] = dataColumnsHeadersColl[i].Text;

            if (!chartTitle.Contains("Media"))
            {
                bool avail = true;
                foreach(string dataColumn in dataColumnNamesList)
                    if(!dataColumn.ToLower().Contains("media month"))
                    {
                        avail = false;
                        break;
                    }
                Assert.IsTrue(avail, "Media Month Columns are not present in Tabular Grid of chart '" + chartTitle + "'.");
            }
            else
            {
                string[] mediaList = new string[] { "Television", "Print", "Online Display", "Mobile", "Radio" };
                foreach (string dataColumn in dataColumnNamesList)
                {
                    bool avail = false;
                    foreach(string media in mediaList)
                        if (dataColumn.ToLower().Contains(media.ToLower()))
                        {
                            avail = true;
                            break;
                        }
                    Assert.IsTrue(avail, "'" + dataColumn + "' is not of Media Type.");
                }
            }

            Results.WriteStatus(test, "Pass", "Captured, Data Columns From Tabular Grid");
            return new TabularGrid(driver, test);
        }




        #endregion
    }
}
