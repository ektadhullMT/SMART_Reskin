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
    public class MyExportsPage
    {
        #region Private Variables

        private IWebDriver myExportsPage;
        private ExtentTest test;

        #endregion

        public MyExportsPage(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.myExportsPage = driver;
            test = testReturn;
        }

        public IWebDriver driver
        {
            get { return this.myExportsPage; }
            set { this.myExportsPage = value; }
        }

        ///<summary>
        ///Verify My Exports Page
        ///</summary>
        ///<param name="VerifySuccessStatus">Whether to Verify Status of All Downloads as success</param>
        ///<returns></returns>
        public MyExportsPage VerifyMyExportsPage(bool VerifySuccessStatus = false)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-exports-page//h1[text()='My Exports']"), "My Exports Page not Present");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-export-files-table/table"), "My Exports Table not Present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//thead//th"), "Table Headers are not present in My Exports Table");
            string[] columnHeaderNames = { "EXPORT", "TYPE", "FORMAT", "CREATED", "STATUS", "ACTIONS" };
            IList<IWebElement> columnHeaderColl = driver._findElements("xpath", "//cft-export-files-table//thead//th");
            foreach (string columnHeader in columnHeaderNames)
            {
                bool avail = false;
                foreach(IWebElement column in columnHeaderColl)
                    if (column.Text.ToLower().Contains(columnHeader.ToLower()))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "'" + columnHeader + "' not found in Table Headers of My Exports Table");
            }

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//tbody//th"), "Rows are not present in My Exports Table");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//tbody//th"), "Export Names are not present in My Exports Table");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//tbody//tr/td[1]"), "Type not present in My Exports Table");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//tbody//tr/td[2]"), "Format are not present in My Exports Table");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//tbody//tr//small"), "Created are not present in My Exports Table");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//tbody//tr/td[4]"), "Status are not present in My Exports Table");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//tbody//tr/td//a"), "Action are not present in My Exports Table");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//button[text()='Load More']"), "'Load More' not present on My Exports Page.");

            if (VerifySuccessStatus)
            {
                IList<IWebElement> statusCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//tr/td[4]");
                foreach (IWebElement status in statusCollection)
                    Assert.IsTrue(status.Text.ToLower().Contains("success"), "Status was not 'success' for one of the rows.");
                Results.WriteStatus(test, "Pass", "Verified, Status is 'success' for all the rows");
            }

            Results.WriteStatus(test, "Pass", "Verified, My Exports Page");
            return new MyExportsPage(driver, test);
        }

        ///<summary>
        ///Verify That Clicking On Export Name Downloads Asset
        ///</summary>
        ///<param name="exportName">Export Name</param>
        ///<returns></returns>
        public string[] VerifyThatClickingOnExportNameDownloadsAsset(string exportName = "", bool fromActionDDL = false, string index = "", string fileType = "")
        {
            IList<IWebElement> exportNameCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//th/a");
            IList<IWebElement> filetypeCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//tr/td[2]");
            int x = -1;
            if (exportName.Equals(""))
            {
                if (fileType.Equals(""))
                {
                    Random rand = new Random();
                    x = rand.Next(0, exportNameCollection.Count);
                    exportName = exportNameCollection[x].Text;
                }
                else
                {
                    for(int i = 0; i < filetypeCollection.Count; i++)
                    {
                        if (filetypeCollection[i].Text.ToLower().Contains(fileType.ToLower()))
                        {
                            x = i;
                            exportName = exportNameCollection[i].Text;
                            break;
                        }
                    }
                    Assert.AreNotEqual(x, -1, "'" + fileType + "' not found.");
                }
            }
            else if (!index.Equals(""))
                Assert.IsTrue(int.TryParse(index, out x), "Failed to parse index to integer");
            else
            {
                bool avail = false;
                for (int i = 0; i < exportNameCollection.Count; i++)
                    if (exportNameCollection[i].Text.ToLower().Contains(exportName.ToLower()))
                    {
                        avail = true;
                        x = i;
                        break;
                    }
                Assert.IsTrue(avail, "'" + exportName + "' not found in Export Names in My Exports Table");
            }

            IList<IWebElement> statusCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//tr/td[4]");
            filetypeCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//tr/td[2]");
            if (statusCollection[x].Text.ToLower().Contains("success"))
            {
                Assert.IsTrue(exportNameCollection[x].GetAttribute("class").Contains("bold"), "'" + exportName + "' is not in Bold");
                fileType = filetypeCollection[x].Text;
                if (fromActionDDL)
                {
                    IList<IWebElement> actionsIconCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//tr//a[@dropdowntoggle]");
                    driver._scrollintoViewElement("xpath", "//cft-export-files-table//tbody//tr[" + (x + 1) + "]//a[@dropdowntoggle]");
                    actionsIconCollection[x].Click();
                    Assert.IsTrue(driver._waitForElement("xpath", "//a[contains(@class,'dropdown-item')]"), "Action DDL not present");
                    driver._click("xpath", "//a[contains(@class,'dropdown-item')]");
                }
                else
                    exportNameCollection[x].Click();
            }
            Thread.Sleep(10000);
            if (fileType.Equals("excel"))
                fileType = "xlsx";
            else if (fileType.Equals("powerpoint"))
                fileType = "pptx";
            string[] exportNameAndType = { exportName, fileType, x.ToString()};

            Results.WriteStatus(test, "Pass", "Verified, That Clicking On Export Name '" + exportName + "' Downloads Asset.");
            return exportNameAndType;
        }

        ///<summary>
        ///Find And Verify Unsuccessful Downloads
        ///</summary>
        ///<returns></returns>
        public string findAndVerifyUnsuccessfulDownloads(string column = "status")
        {
            string exportName = "";
            bool avail = false;
            int x = 0, j = 0 ;
            for(int i = 0; i < 7; i++)
            {
                VerifyMyExportsPage();
                IList<IWebElement> statusCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//tr/td[4]");
                IList<IWebElement> createdTimeCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//tr//small");
                for (; j < statusCollection.Count; j++)
                {
                    if (column.ToLower().Equals("status"))
                    {
                        if (statusCollection[j].Text.ToLower().Contains("error") || statusCollection[j].Text.ToLower().Contains("zeroitems"))
                        {
                            avail = true;
                            x = j;
                            break;
                        }
                    }
                    else if (column.ToLower().Equals("created"))
                    {
                        if (createdTimeCollection[j].Text.ToLower().Contains("expired"))
                        {
                            avail = true;
                            x = j;
                            break;
                        }
                    }
                }
                if (avail)
                    break;
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//button[text()='Load More']"), "'Load More' not present on My Exports Page.");
                driver._click("xpath", "//cft-export-files-table//button[text()='Load More']");
                Thread.Sleep(2000);
            }
            if (column.ToLower().Equals("status"))
                Assert.IsTrue(avail, "No status was 'error' or 'zeroitems'.");
            else if (column.ToLower().Equals("created"))
                Assert.IsTrue(avail, "No 'Expired' exports present.");

            IList<IWebElement> exportNameCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//th/a");
            IList<IWebElement> actionButtonCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//tr/td[@class='text-center']");
            Assert.IsTrue(exportNameCollection[x].GetAttribute("class").Contains("disabled"), "'" + exportName + "' is not in disabled");
            IList<IWebElement> actionLinkCollection = actionButtonCollection[x]._findElementsWithinElement("xpath", ".//a");
            Assert.AreEqual(0, actionLinkCollection.Count, "Actions Icon for '" + exportName + "' is not disabled.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//tbody//tr[" + (x + 1) + "]//*[name()='svg' and @data-icon='exclamation-triangle']"), "Caution Icon for '" + exportName + "' is not disabled.");

            Results.WriteStatus(test, "Pass", "Found and Verified, Unsuccessful Download of Export Name '" + exportName + ".");
            return exportName;
        }

        ///<summary>
        ///Verify That Most Recent Downloads Appear on Top In My Exports Table
        ///</summary>
        ///<returns></returns>
        public MyExportsPage VerifyThatMostRecentDownloadsAppearOnTopInMyExportsTable()
        {
            IList<IWebElement> createdColumnCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//tr//small");
            DateTime[] createdColumnValues = new DateTime[createdColumnCollection.Count];
            for(int i = 0; i < createdColumnCollection.Count; i++)
            {
                int index = createdColumnCollection[i].Text.IndexOf(" ");
                string number = createdColumnCollection[i].Text.Substring(0, index);
                if (number.Equals("an") || number.Equals("a"))
                    number = "1";
                int num = 0;
                DateTime today = DateTime.Today;
                Assert.IsTrue(int.TryParse(number, out num), "Number could not be parsed to Integer type.");

                if (createdColumnCollection[i].Text.ToLower().Contains("second") || createdColumnCollection[i].Text.ToLower().Contains("minute") || createdColumnCollection[i].Text.ToLower().Contains("hour"))
                    createdColumnValues[i] = today.AddDays(0);
                else if (createdColumnCollection[i].Text.ToLower().Contains("day"))
                    createdColumnValues[i] = today.AddDays(-1 * num);
                else if (createdColumnCollection[i].Text.ToLower().Contains("month"))
                    createdColumnValues[i] = today.AddMonths(-1 * num);
                else if (createdColumnCollection[i].Text.ToLower().Contains("year"))
                    createdColumnValues[i] = today.AddYears(-1 * num);

                Console.WriteLine(createdColumnValues[i]);
            }

            DateTime[] newCreatedColumnValues = new DateTime[createdColumnValues.Length];
            Array.Copy(createdColumnValues, newCreatedColumnValues, createdColumnValues.Length);
            Array.Sort(newCreatedColumnValues);
            Array.Reverse(newCreatedColumnValues);

            Assert.IsTrue(createdColumnValues.SequenceEqual(newCreatedColumnValues), "Exports are not sorted so that the most recent download is on top.");

            Results.WriteStatus(test, "Pass", "Verified, That Most Recent Downloads Appear on Top In My Exports Table");
            return new MyExportsPage(driver, test);
        }

        ///<summary>
        ///Find Specific Value In A Column
        ///</summary>
        ///<returns></returns>
        public MyExportsPage FindSpecificValueInAColumn(string column, string value, bool present = true)
        {
            string xpath = "//cft-export-files-table//tbody//tr";

            if (column.ToLower().Contains("name"))
                xpath = xpath + "//th";
            else if (column.ToLower().Contains("type"))
                xpath = xpath + "/td[1]";
            else if (column.ToLower().Contains("format"))
                xpath = xpath + "/td[2]";
            else if (column.ToLower().Contains("created"))
                xpath = xpath + "//small";
            else if (column.ToLower().Contains("status"))
                xpath = xpath + "/td[4]";

            Assert.AreNotEqual("//cft-export-files-table//tbody//tr//", xpath, "'" + column + "' Column Name is not valid.");
            Assert.IsTrue(driver._isElementPresent("xpath", xpath), "Column '" + column + "' not present.");

            bool avail = false;
            for (int i = 0; i < 7; i++)
            {
                VerifyMyExportsPage();
                IList<IWebElement> columnValuesColl = driver._findElements("xpath", xpath);
                foreach (IWebElement columnValue in columnValuesColl)
                    if (columnValue.Text.ToLower().Contains(value.ToLower()) )
                    {
                        avail = true;
                        break;
                    }
                if (avail)
                    break;
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//button[text()='Load More']"), "'Load More' not present on My Exports Page.");
                driver._click("xpath", "//cft-export-files-table//button[text()='Load More']");
                Thread.Sleep(2000);
            }
            if(present)
                Assert.IsTrue(avail, "No '" + column + "' had value ''" + value + "'.");
            else
                Assert.IsFalse(avail, "'" + column + "' has value ''" + value + "'.");

            Results.WriteStatus(test, "Pass", "Fount, '" + value + "' Value In '" + column + "' Column");
            return new MyExportsPage(driver, test);
        }

        ///<summary>
        ///Delete Downloaded File
        ///</summary>
        ///<returns></returns>
        public MyExportsPage deleteDownloadedFile(string fileName)
        {
            string path = ExtentManager.ResultsDir;
            File.Delete(Path.Combine(path, fileName));

            Results.WriteStatus(test, "Pass", "Deleted, downloaded file '" + fileName + "'");
            return new MyExportsPage(driver, test);
        }

        ///<summary>
        ///Capture Data From Excel Sheet
        ///</summary>
        ///<returns></returns>
        public string[,] captureDataFromExcelSheet(string fileName)
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

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;

            string[,] dataGrid = new string[rw, cl];

            for (int rCnt = 1, i = 0; rCnt <= rw; rCnt++, i++)
                for (int cCnt = 1, j = 0; cCnt <= cl; cCnt++, j++)
                    dataGrid[i, j] = (range.Cells[rCnt, cCnt] as Excel.Range).Text;

            Results.WriteStatus(test, "Pass", "Captured, Data from File '" + fileName + "' in exported file");
            return dataGrid;
        }

        ///<summary>
        ///Compare Data From Excel Sheet
        ///</summary>
        ///<returns></returns>
        public string[,] compareDataFromExcelSheet(string fileName, string[,] dataGrid)
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

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;


            for (int rCnt = 1, i = 0; rCnt <= rw; rCnt++, i++)
                for (int cCnt = 1, j = 0; cCnt <= cl; cCnt++, j++)
                {
                    if(!dataGrid[i,j].Contains("Timeframe"))
                        Assert.IsTrue((range.Cells[rCnt, cCnt] as Excel.Range).Text.Contains(dataGrid[i, j]), "'" + dataGrid[i, j] + "' not found in downloaded excel file");

                }

            Results.WriteStatus(test, "Pass", "Verified, Data in file '" + fileName + "' in exported file");
            return dataGrid;
        }

        ///<summary>
        ///Capture Export Names List From My Exports Page
        ///</summary>
        ///<returns></returns>
        public string[] captureExportNamesListFromMyExportsPage()
        {
            IList<IWebElement> exportNameCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//th/a");
            string[] exportNameList = new string[exportNameCollection.Count];

            for (int i = 0; i < exportNameCollection.Count; i++)
                exportNameList[i] = exportNameCollection[i].Text;

            return exportNameList;
        }


        ///<summary>
        ///Verify Load More Button Functionality
        ///</summary>
        ///<returns></returns>
        public MyExportsPage VerifyLoadMoreButtonFunctionality()
        {
            IList<IWebElement> exportNameCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//th/a");
            int prevCount = exportNameCollection.Count;
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-files-table//button[text()='Load More']"), "'Load More' not present on My Exports Page.");
            driver._click("xpath", "//cft-export-files-table//button[text()='Load More']");
            Thread.Sleep(1000);
            exportNameCollection = driver._findElements("xpath", "//cft-export-files-table//tbody//th/a");
            int newCount = exportNameCollection.Count;
            Assert.Less(prevCount, newCount, "Load More Button not working properly");

            return new MyExportsPage(driver, test);
        }

    }
}
