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
    public class AgGrid
    {
        #region Private Variables

        private IWebDriver agGrid;
        private ExtentTest test;
        Carousels carousels;
        Home homePage;

        #endregion

        public AgGrid(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.agGrid = driver;
            test = testReturn;
            carousels = new Carousels(driver, test);
            homePage = new Home(driver, test);
        }

        public IWebDriver driver
        {
            get { return this.agGrid; }
            set { this.agGrid = value; }
        }

        ///<summary>
        ///Verify Pagination Functionality of AgGrid
        ///</summary>
        ///<param name="noOfItemsPerPage">No of items per page</param>
        ///<param name="page">Page no</param>
        ///<returns></returns>
        public AgGrid VerifyPaginationFunctionalityOfAgGrid(string page = "", string noOfItemsPerPage = "")
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-group//pagination//ul/li"), "Pagination not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-group//div[@class='btn-group' and not(@dropdown)]/button"), "Rows per Page buttons not present");
            driver._scrollintoViewElement("xpath", "//cft-domain-item-group//pagination//ul/li");
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,-100)");

            if (page.ToLower() == "first")
            {
                IList<IWebElement> pageNoColl = driver._findElements("xpath", "//cft-domain-item-group//pagination//ul/li");
                pageNoColl[0].Click();
                Thread.Sleep(1000);
                pageNoColl = driver._findElements("xpath", "//cft-domain-item-group//pagination//ul/li");
                Assert.IsTrue(pageNoColl[2].GetAttribute("class").Contains("active"), "First Page was not selected");
                Results.WriteStatus(test, "Pass", "Selected, First page of AgGrid.");
            }
            else if (page.ToLower() == "previous")
            {
                IList<IWebElement> pageNoColl = driver._findElements("xpath", "//cft-domain-item-group//pagination//ul/li");
                int selectedPage = 0;
                for (int i = 2; i < pageNoColl.Count - 1; i++)
                    if (pageNoColl[i].GetAttribute("class").Contains("active"))
                    {
                        selectedPage = i;
                        break;
                    }

                pageNoColl[1].Click();
                Thread.Sleep(1000);
                pageNoColl = driver._findElements("xpath", "//cft-domain-item-group//pagination//ul/li");
                Assert.IsTrue(pageNoColl[selectedPage - 1].GetAttribute("class").Contains("active"), "First Page was not selected");
                Results.WriteStatus(test, "Pass", "Selected, Previous page, page no. '" + page + "' of AgGrid.");
            }
            else if (page.ToLower() == "next")
            {
                IList<IWebElement> pageNoColl = driver._findElements("xpath", "//cft-domain-item-group//pagination//ul/li");
                int selectedPage = 0;
                for (int i = 2; i < pageNoColl.Count - 1; i++)
                    if (pageNoColl[i].GetAttribute("class").Contains("active"))
                    {
                        selectedPage = i;
                        break;
                    }

                pageNoColl[pageNoColl.Count-2].Click();
                Thread.Sleep(1000);
                pageNoColl = driver._findElements("xpath", "//cft-domain-item-group//pagination//ul/li");
                Assert.IsTrue(pageNoColl[selectedPage + 1].GetAttribute("class").Contains("active"), "First Page was not selected");
                Results.WriteStatus(test, "Pass", "Selected, Next page, page no. '" + page + "' of AgGrid.");
            }
            else if (page.ToLower() == "last")
            {
                IList<IWebElement> pageNoColl = driver._findElements("xpath", "//cft-domain-item-group//pagination//ul/li");
                pageNoColl[pageNoColl.Count - 1].Click();
                Thread.Sleep(1000);
                pageNoColl = driver._findElements("xpath", "//cft-domain-item-group//pagination//ul/li");
                Assert.IsTrue(pageNoColl[pageNoColl.Count - 3].GetAttribute("class").Contains("active"), "First Page was not selected");
                Results.WriteStatus(test, "Pass", "Selected, Last page of AgGrid.");
            }
            else if(page != "")
            {
                IList<IWebElement> pageNoColl = driver._findElements("xpath", "//cft-domain-item-group//pagination//ul/li");
                bool avail = false;

                int iPage = 0, lastPage = 0;
                Assert.IsTrue(int.TryParse(page, out iPage), "Couldn't convert '" + page + "' to int.");
                Assert.IsTrue(int.TryParse(pageNoColl[pageNoColl.Count-3].Text, out lastPage), "Couldn't convert '" + pageNoColl[pageNoColl.Count - 3].Text + "' to int.");
                while (iPage > lastPage)
                {
                    pageNoColl[pageNoColl.Count - 2].Click();
                    Thread.Sleep(1000);
                    pageNoColl = driver._findElements("xpath", "//cft-domain-item-group//pagination//ul/li");
                    Assert.IsTrue(int.TryParse(pageNoColl[pageNoColl.Count - 3].Text, out lastPage), "Couldn't convert '" + pageNoColl[pageNoColl.Count - 3].Text + "' to int.");
                }

                for (int i = 2; i < pageNoColl.Count - 1; i++)
                    if (pageNoColl[i].Text.Contains(page))
                    {
                        pageNoColl[i].Click();
                        Thread.Sleep(1000);
                        pageNoColl = driver._findElements("xpath", "//cft-domain-item-group//pagination//ul/li");
                        Assert.IsTrue(pageNoColl[i].GetAttribute("class").Contains("active"), "Page no. '" + page + "' was not selected");
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "Page no. '" + page + "' not found for AgGrid.");
                Results.WriteStatus(test, "Pass", "Selected, page no. '" + page + "' of AgGrid.");
            }


            if (noOfItemsPerPage != "")
            {
                IList<IWebElement> noOfRowsPerPageColl = driver._findElements("xpath", "//cft-domain-item-group//div[@class='btn-group' and not(@dropdown)]/button");

                bool avail = false;
                for(int i = 0; i < noOfRowsPerPageColl.Count; i++)
                    if (noOfRowsPerPageColl[i].Text.Equals(noOfItemsPerPage))
                    {
                        noOfRowsPerPageColl[i].Click();
                        Thread.Sleep(1000);
                        noOfRowsPerPageColl = driver._findElements("xpath", "//cft-domain-item-group//div[@class='btn-group' and not(@dropdown)]/button");
                        Assert.AreEqual("true", noOfRowsPerPageColl[i].GetAttribute("aria-pressed"), "'" + noOfItemsPerPage + "' Rows per page did not get selected.");
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "No. of '" + noOfItemsPerPage + "' rows per page button not found for AgGrid.");

                int iNoOfRowsPerPage = 0;
                Assert.IsTrue(int.TryParse(noOfItemsPerPage, out iNoOfRowsPerPage), "Couldn't convert '" + noOfItemsPerPage + "' to int.");

                if(driver._isElementPresent("xpath", "//ag-grid-angular"))
                {
                    Assert.IsTrue(driver._waitForElement("xpath", "//ag-grid-angular//div[@class='ag-body-container']/div"), "Rows not present in AgGrid.");
                    IList<IWebElement> rowsCollection = driver._findElements("xpath", "//ag-grid-angular//div[@class='ag-body-container']/div");
                    Assert.AreEqual(iNoOfRowsPerPage, rowsCollection.Count, "No. of rows displayed do not match selected no. of rows per page option.");
                }
                else if(driver._isElementPresent("xpath", "//cft-domain-item-details//div"))
                {
                    Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']"), "Result Cards not Details in Thumbnails view.");
                    IList<IWebElement> cardCollection = driver._findElements("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']");
                    Assert.AreEqual(iNoOfRowsPerPage, cardCollection.Count, "No. of cards displayed do not match selected no. of rows per page option.");
                }


                Results.WriteStatus(test, "Pass", "Selected, No. of '" + noOfItemsPerPage + "' rows per page for AgGrid.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Pagination Functionality of AgGrid");
            return new AgGrid(driver, test);
        }

        ///<summary>
        ///Verify Sorting Functionality of AgGrid
        ///</summary>
        ///<param name="ascending">Whether to sort in ascending or descending order</param>
        ///<param name="column">Name of Column</param>
        ///<returns></returns>
        public string VerifySortingFunctionalityOfAgGrid(string column = "", bool ascending = true)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']//div[@col-id]"), "AgGrid Column Headers not present");
            IList<IWebElement> columnHeaderColl = driver._findElements("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']//div[@col-id]");

            int columnIndex = -1;
            if(column == "" || column.ToLower().Equals("random"))
            {
                Random rand = new Random();
                columnIndex = rand.Next(0, columnHeaderColl.Count);
                column = columnHeaderColl[columnIndex].Text;
                if (column.ToLower().Contains("date"))
                    column = columnHeaderColl[columnIndex + 1].Text;
            }
            else
            {
                for(int i = 0; i < columnHeaderColl.Count; i++)
                {
                    IList<IWebElement> columnNameColl = columnHeaderColl[i]._findElementsWithinElement("xpath", ".//span[@ref='eText']");
                    if (columnNameColl[0].Text.ToLower().Equals(column.ToLower()))
                    {
                        columnIndex = i;
                        break;
                    }
                }
            }
            Assert.Greater(columnIndex, -1, "Column to be sorted not found.");

            string colId = columnHeaderColl[columnIndex].GetAttribute("col-id");
            IList<IWebElement> columnSortColl = columnHeaderColl[columnIndex]._findElementsWithinElement("xpath", ".//span[contains(@ref, 'Sort') and not(contains(@class, 'hidden'))]");
            Assert.AreNotEqual(0, columnSortColl.Count, "Sorting Icon not present on column '" + column + "'");

            if (ascending)
            {
                while (!columnSortColl[0].GetAttribute("ref").Contains("Asc"))
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('ag-body-viewport')[0].scrollLeft+=100", "");
                    columnSortColl[0].Click();
                    Thread.Sleep(1000);
                    columnHeaderColl = driver._findElements("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']//div[@col-id]");
                    columnSortColl = columnHeaderColl[columnIndex]._findElementsWithinElement("xpath", ".//span[contains(@ref, 'Sort') and not(contains(@class, 'hidden'))]");
                }

                Assert.AreEqual("eSortAsc", columnSortColl[0].GetAttribute("ref"), "Sorted in 'Ascending' order icon not present on Column '" + column + "'.");
            }
            else
            {
                while (!columnSortColl[0].GetAttribute("ref").Contains("Desc"))
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('ag-body-viewport')[0].scrollLeft+=100", "");
                    columnSortColl[0].Click();
                    Thread.Sleep(1000);
                    columnHeaderColl = driver._findElements("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']//div[@col-id]");
                    columnSortColl = columnHeaderColl[columnIndex]._findElementsWithinElement("xpath", ".//span[contains(@ref, 'Sort') and not(contains(@class, 'hidden'))]");
                }

                Assert.AreEqual("eSortDesc", columnSortColl[0].GetAttribute("ref"), "Sorted in 'Descending' order icon not present on Column '" + column + "'.");
            }

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-body-viewport']//div[@col-id='" + colId + "']/span"), "Cells not present in Column '" + column + "'");
            IList<IWebElement> columnCellColl = driver._findElements("xpath", "//div[@class='ag-body-viewport']//div[@col-id='" + colId + "']/span");

            string[] columnCellValueList = new string[10];
            for(int i = 0; i < 10; i++)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", columnCellColl[i]);
                columnCellValueList[i] = columnCellColl[i].Text;
            }

            if(colId.Contains("occurrences") || colId.Contains("costEstimate") || colId.Contains("allocatedLineage"))
            {
                int[] iColumnCellValues = new int[columnCellValueList.Length];
                for(int i = 0; i < columnCellValueList.Length; i++)
                {
                    string temp = columnCellValueList[i];
                    if (temp.Equals("NA"))
                        iColumnCellValues[i] = 0;
                    else
                    {
                        if (temp.Contains("$"))
                            temp = temp.Substring(1);
                        while (temp.IndexOf(",") > -1)
                            temp = temp.Remove(temp.IndexOf(","), 1);

                        Assert.IsTrue(int.TryParse(temp, out iColumnCellValues[i]), "Couldn't convert '" + columnCellValueList[i] + "' at '" + (i + 1) + "' to int.");
                    }
                }

                int[] unSortedCellValues = new int[iColumnCellValues.Length];
                Array.Copy(iColumnCellValues, unSortedCellValues, iColumnCellValues.Length);
                Array.Sort(iColumnCellValues);
                if (!ascending)
                    Array.Reverse(iColumnCellValues);
                Assert.IsTrue(iColumnCellValues.SequenceEqual(unSortedCellValues), "Column '" + column + "' cell values are not sorted as expected");
            }
            else
            {
                string[] unSortedCellValues = new string[columnCellValueList.Length];
                Array.Copy(columnCellValueList, unSortedCellValues, columnCellValueList.Length);
                Array.Sort(columnCellValueList);
                if (!ascending)
                    Array.Reverse(columnCellValueList);
                Assert.IsTrue(columnCellValueList.SequenceEqual(unSortedCellValues), "Column '" + column + "' cell values are not sorted as expected");
            }

            Results.WriteStatus(test, "Pass", "Sorted, Column '" + column + "' cell values as expected");
            Results.WriteStatus(test, "Pass", "Verified, Sorting Functionality of AgGrid");
            return column;
        }

        ///<summary>
        ///Verify Inspector Area for Selected Record From AgGrid
        ///</summary>
        ///<param name="adCode">Ad Code of selected ad</param>
        ///<returns></returns>
        public AgGrid VerifyInspectorAreaForSelectedRecordFromAgGrid(string adCode)
        {
            IList<IWebElement> rowCollection = driver._findElements("xpath", "//ag-grid-angular//div[@class='ag-body-container']/div");
            foreach (IWebElement row in rowCollection)
            {
                IList<IWebElement> columnCollection = row._findElementsWithinElement("xpath", ".//div[contains(@col-id, 'adcode') or contains(@col-id, 'adId')]/span");
                if (columnCollection[0].Text.Contains(adCode))
                {
                    Assert.IsTrue(row.GetAttribute("class").Contains("selected"), "Ad Record with Ad code '" + adCode + "' not selected");
                    columnCollection[0].Click();
                    Thread.Sleep(1000);
                    break;
                }
            }

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-group//div[@class='NU-card card p-0']"), "Inspector Area not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-group//div[@class='NU-card card p-0']//span"), "Inspector Area Header not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-group//div[@class='NU-card card p-0']//button"), "View Ad Buttons on Inspector Area not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-group//div[@class='NU-card card p-0']//img"), "Inspector Area Ad image not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-group//div[@class='NU-card card p-0']//table"), "Inspector Area Ad data table not present.");

            driver._click("xpath", "//cft-domain-item-group//div[@class='NU-card card p-0']//button[text()='Details']");
            string inspectorAdCode = carousels.getAdCodeFromCarousel(false);

            Assert.AreEqual(adCode, inspectorAdCode, "Ad in Inspector Area is not the same as the selected Ad in AgGrid");

            Results.WriteStatus(test, "Pass", "Verified, Inspector Area for Selected Record From AgGrid");
            return new AgGrid(driver, test);
        }

        ///<summary>
        ///Verify That Columns Can Be Dragged
        ///</summary>
        ///<returns></returns>
        public AgGrid VerifyThatColumnsCanBeDragged(string columnName = "", bool dragRight = true)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']//div[@col-id]"), "AgGrid Column Headers not present");
            IList<IWebElement> columnHeaderColl = driver._findElements("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']//div[@col-id]");
            int columnIndex = -1;
            if(columnName == "" || columnName.ToLower().Equals("random"))
            {
                Random rand = new Random();
                columnIndex = rand.Next(1, columnHeaderColl.Count - 1);
                columnName = columnHeaderColl[columnIndex].Text;
            }
            else
            {
                for(int i = 0; i < columnHeaderColl.Count; i++)
                {
                    if(i > 5)
                        ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('ag-body-viewport')[0].scrollLeft+=20", "");
                    if (columnHeaderColl[i].Text.ToLower().Contains(columnName))
                    {
                        columnIndex = i;
                        break;
                    }
                }
                Assert.Greater(columnIndex, -1, "Column '" + columnName + "' not found.");
            }

            if (columnIndex == 0 && !dragRight)
                Results.WriteStatus(test, "Info", "Column '" + columnName + "' is the leftmost column and hence cannot be dragged left.");
            else if(columnIndex == columnHeaderColl.Count -1  && dragRight)
                Results.WriteStatus(test, "Info", "Column '" + columnName + "' is the rightmost column and hence cannot be dragged right.");
            else
            {
                Actions action = new Actions(driver);
                if (dragRight)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('ag-body-viewport')[0].scrollLeft+=50", "");
                    action.DragAndDrop(columnHeaderColl[columnIndex], columnHeaderColl[columnIndex + 1]).Perform();
                    Thread.Sleep(1000);
                    Results.WriteStatus(test, "Pass", "Column '" + columnName + "' is dragged right.");
                }
                else
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('ag-body-viewport')[0].scrollLeft-=50", "");
                    action.DragAndDrop(columnHeaderColl[columnIndex], columnHeaderColl[columnIndex - 1]).Perform();
                    Thread.Sleep(1000);
                    Results.WriteStatus(test, "Pass", "Column '" + columnName + "' is dragged left.");
                }
            }

            Results.WriteStatus(test, "Pass", "Verified, Columns Can Be Dragged");
            return new AgGrid(driver, test);
        }

        ///<summary>
        ///Capture Column Names of AgGrid in Order
        ///</summary>
        ///<returns></returns>
        public string[] captureColumnNamesOfDetailsViewAgGridInOrder()
        {
            if (driver._isElementPresent("xpath", "//img[@class='sidebar-logo img-fluid']"))
                driver._click("xpath", "//cft-sidebar-navigation//button[contains(@class, 'sidebar-toggle')]");

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]"), "Detail Cards not present");
            IList<IWebElement> cardCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]");

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", cardCollection[0]);
            IList<IWebElement> fieldNameColl = cardCollection[0]._findElementsWithinElement("xpath", ".//table//th");

            string[] columnNames = new string[fieldNameColl.Count];

            for (int i = 0; i < fieldNameColl.Count; i++)
            {
                columnNames[i] = fieldNameColl[i].Text.ToLower();
                if (columnNames[i].Contains("$000s"))
                    columnNames[i] = "spend";
                if (columnNames[i].Contains("media type"))
                    columnNames[i] = "media";
            }

            Results.WriteStatus(test, "Pass", "Captured, Column Names of AgGrid in Order.");
            return columnNames;
        }

        ///<summary>
        ///Verify Thumbnail Image Tag in AgGrid
        ///</summary>
        ///<returns></returns>
        public AgGrid VerifyThumbnailImageTagInAgGrid()
        {
            string reportName = homePage.getActiveScreenNameFromSideNavigationBar();

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']"), "Result Cards not Details in Thumbnails view.");
            IList<IWebElement> cardCollection = driver._findElements("xpath", "//cft-domain-item-details//div[@class='NU-card card p-0 NU-selectable-card']");

            IList<IWebElement> cardTagCollection = cardCollection[0]._findElementsWithinElement("xpath", ".//div[contains(@class, 'NU-card-tags')]/button");
            string cardTagValue = cardTagCollection[0].Text;

            if(reportName.ToLower().Equals("QA Testing - Brand - Dashboard"))
            {
                homePage.selectViewForResultsDisplay("Table");
                homePage.VerifyTableViewOfAgGrid();

                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-body-viewport']//div[contains(@col-id, 'media')]/span"), "Cells not present in Column 'Media'");
                IList<IWebElement> columnCellColl = driver._findElements("xpath", "//div[@class='ag-body-viewport']//div[contains(@col-id, 'media')]/span");

                bool avail = false;
                for(int i = 0; i < 15; i++)
                    if (columnCellColl[i].Text.Contains(cardTagValue))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "Card Tag is not showing 'Media Type' in Thumbnail view on AgGrid on 'QA Testing - Brand - Dashboard' report.");
                Results.WriteStatus(test, "Pass", "Card Tag is showing 'Media Type' in Thumbnail view on AgGrid on 'QA Testing - Brand - Dashboard' report.");
            }
            else if(reportName.ToLower().Equals("Print Dynamics Dashboard (Occurrence)"))
            {
                Assert.IsTrue(cardTagValue.Contains("$"), "Card Tag is not showing 'Spend Value' in Thumbnail view on AgGrid on 'Print Dynamics Dashboard (Occurrence)' report.");
                Results.WriteStatus(test, "Pass", "Card Tag is showing 'Spend Value' in Thumbnail view on AgGrid on 'Print Dynamics Dashboard (Occurrence)' report.");
            }


            Results.WriteStatus(test, "Pass", "Verified, Thumbnail Image Tag in AgGrid");
            return new AgGrid(driver, test);
        }
    }
}
