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
    public class Charts
    {
        #region Private Variables

        private IWebDriver charts;
        private ExtentTest test;
        FieldOptions fieldOptions;
        SummaryTags summaryTags;
        #endregion

        public Charts(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.charts = driver;
            test = testReturn;
            fieldOptions = new FieldOptions(driver, test);
            summaryTags = new SummaryTags(driver, test);
        }

        public IWebDriver driver
        {
            get { return this.charts; }
            set { this.charts = value; }
        }

        ///<summary>
        ///Verify Charts
        ///</summary>
        ///<returns></returns>
        public string VerifyCharts(bool pivotGrid = true)
        {
            string chartTitle = "";
            if (driver._waitForElement("xpath", "//cft-domain-item-chart-carousel//div"))
            {
                driver._scrollintoViewElement("xpath", "//cft-domain-item-chart-carousel//div");
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-chart-carousel//carousel//slide"), "Slides not present in Chart section");

                Assert.IsTrue(driver._waitForElement("xpath", "//carousel//slide[@aria-hidden='false']//div[@class='carousel-chart-columns-split']"), "Charts not present in Chart section");
                Assert.IsTrue(driver._waitForElement("xpath", "//slide[@aria-hidden='false']//div/div[@class='carousel-chart-columns-split']//*[name()='text' and @class='highcharts-title']/*[name()='tspan']"), "Chart Title not present in Chart");
                chartTitle = driver._getText("xpath", "//slide[@aria-hidden='false']//div/div[@class='carousel-chart-columns-split']//*[name()='text' and @class='highcharts-title']/*[name()='tspan']").Replace(' ', '_').ToLower();
                Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div/div[@class='carousel-chart-columns-split']//*[name()='text' and text()='Numerator']"), "'Numerator' text label not present in Chart");
                Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div/div[@class='carousel-chart-columns-split']//*[name()='text' and contains(@id, 'ChartTimeframe')]"), "'Dates' not present in Chart");
                if(pivotGrid)
                    Assert.IsTrue(driver._waitForElement("xpath", "//slide[@aria-hidden='false']//div/div[@class='carousel-chart-columns-split']//chart-export//button[@uib-tooltip='Expand']"), "'Expand' button not present in Chart");
                Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div/div[@class='carousel-chart-columns-split']//chart-export//button[@uib-tooltip='Download']"), "'Download' button not present in Chart");
                Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div/div[@class='carousel-chart-columns-split']//cft-scheduled-export-modal//button"), "'Schedule Export' button not present in Chart");
                Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div/div[@class='carousel-chart-columns-split']//*[name()='g' and contains(@class, 'highcharts-xaxis-labels')]"), "'X-Axis' labels not present in Chart");
                Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div/div[@class='carousel-chart-columns-split']//*[name()='g' and contains(@class, 'highcharts-yaxis-labels')]"), "'Y-Axis' labels not present in Chart");
                Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div[1]/div/chart//*[name()='g' and @class='highcharts-legend-item']"), "'Legends' not present in Chart");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/a[contains(@class,'left')]"), "Left Navigation Arrow not present in Chart section");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/a[contains(@class,'right')]"), "Right Navigation Arrow not present in Chart section");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/ol/li"), "Sliders not present in Chart section");
            }
            else if (driver._waitForElement("xpath", "//cft-domain-item-chart-grid//div"))
            {
                driver._scrollintoViewElement("xpath", "//cft-domain-item-chart-grid//div");

                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-chart-grid//div[@class='NU-chart card']"), "Charts not present in Chart section");
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-chart-grid//*[name()='text' and @class='highcharts-title']/*[name()='tspan']"), "Chart Title not present in Chart");
                chartTitle = driver._getText("xpath", "//cft-domain-item-chart-grid//*[name()='text' and @class='highcharts-title']/*[name()='tspan']").Replace(' ', '_').ToLower();
                Assert.IsTrue(driver._isElementPresent("xpath", "//*[name()='text' and text()='Numerator']"), "'Numerator' text label not present in Chart");
                Assert.IsTrue(driver._isElementPresent("xpath", "//*[name()='text' and @id]/*[name()='tspan']"), "'Dates' not present in Chart");
                if(pivotGrid)
                    Assert.IsTrue(driver._isElementPresent("xpath", "//chart-export//button[@uib-tooltip='Expand']"), "'Expand' button not present in Chart");
                Assert.IsTrue(driver._isElementPresent("xpath", "//chart-export//button[@uib-tooltip='Download']"), "'Download' button not present in Chart");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal//button"), "'Schedule Export' button not present in Chart");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[1]/div/chart//*[name()='g' and @class='highcharts-legend-item']"), "'Legends' not present in Chart");
            }
            else
                Assert.Fail("Chart Section not present");

            Results.WriteStatus(test, "Pass", "Verified, Charts");
            return chartTitle;
        }

        ///<summary>
        ///Capture Data From Chart
        ///</summary>
        ///<returns></returns>
        public string[,] captureDataFromChart(string chartTitle, bool percTotal = false)
        {
            chartTitle = chartTitle.Replace('_', ' ');
            string xpathPrefix = "";
            int dimension1 = 0, dimension2 = 0;

            if (driver._waitForElement("xpath", "//cft-domain-item-chart-carousel//div"))
                xpathPrefix = "//slide[@aria-hidden='false']";
            else
                xpathPrefix = "//cft-domain-item-chart-grid";

            IWebElement chart = null;
            if (xpathPrefix == "//slide[@aria-hidden='false']")
            {
                int sliderNo = -1;
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/ol/li"), "Sliders not present in Chart section");
                IList<IWebElement> sliderCollection = driver._findElements("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/ol/li");
                foreach (IWebElement slider in sliderCollection)
                {
                    ++sliderNo;
                    IList<IWebElement> chartsCollection = driver._findElements("xpath", xpathPrefix + "//div[@class='NU-chart card']");
                    foreach (IWebElement currChart in chartsCollection)
                    {
                        IList<IWebElement> chartTitleCollection = currChart._findElementsWithinElement("xpath", ".//*[name()='text' and @class='highcharts-title']/*[name()='tspan']");
                        if (chartTitleCollection[0].Text.ToLower().Contains(chartTitle.ToLower()))
                        {
                            chart = currChart;
                            break;
                        }
                    }
                    if (chart != null)
                        break;
                    else
                    {
                        Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/a[contains(@class,'right')]"), "Right Navigation Arrow not present in Chart section");
                        driver._click("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/a[contains(@class,'right')]");
                        Thread.Sleep(10000);
                    }
                }
            }
            else
            {
                IList<IWebElement> chartsCollection = driver._findElements("xpath", xpathPrefix + "//div[@class='NU-chart card']");
                foreach (IWebElement currChart in chartsCollection)
                {
                    IList<IWebElement> chartTitleCollection = currChart._findElementsWithinElement("xpath", ".//*[name()='text' and @class='highcharts-title']/*[name()='tspan']");
                    if (chartTitleCollection[0].Text.ToLower().Contains(chartTitle))
                    {
                        chart = currChart;
                        break;
                    }
                }
            }
            Assert.AreNotEqual(null, chart, "'" + chartTitle + "' not found.");
            IList<IWebElement> chartLegendCollection = chart._findElementsWithinElement("xpath", ".//*[name()='g' and @class='highcharts-legend-item']");
            Assert.Less(0, chartLegendCollection.Count, "Pie Chart Legends did not get updated.");

            IList<IWebElement> pieSectorCollection = chart._findElementsWithinElement("xpath", ".//*[name()='path' and not(@visibility) and @stroke-linejoin]");
            if(pieSectorCollection.Count > 0)
            {
                dimension1 = chartLegendCollection.Count + 1;
                dimension2 = 2;
                string[,] dataGrid = new string[dimension1, dimension2];
                string[] totalPerc = new string[dimension1 - 1];
                dataGrid[0, 0] = "Advertiser";
                dataGrid[1, 0] = "Ad Code";
                for (int i = 0; i < chartLegendCollection.Count; i++)
                    dataGrid[i + 1, 0] = chartLegendCollection[i].Text;

                for(int i = 0; i < pieSectorCollection.Count; i++)
                {
                    Actions action = new Actions(driver);
                    action.MoveToElement(pieSectorCollection[i]).MoveByOffset(0, -4).Perform();
                    IList<IWebElement> tooltipCollection = chart._findElementsWithinElement("xpath", ".//div[@class='highcharts-tooltip']/span");
                    Assert.Less(0, tooltipCollection.Count, "Tooltip not present");
                    Assert.IsTrue(driver._waitForElement("xpath", xpathPrefix + "//h5[@class='highchart-tooltip-title']"), "'Tooltip title' not found");
                    bool legendAvail = false;
                    for (int j = 1; j < dataGrid.GetLength(0); j++)
                    {
                        if (driver._getText("xpath", xpathPrefix + "//h5[@class='highchart-tooltip-title']").ToLower().Contains(dataGrid[j, 0].ToLower()))
                        {
                            legendAvail = true;
                            Assert.IsTrue(driver._isElementPresent("xpath", xpathPrefix + "//tbody/tr/td[not(@class)]"), "'Count' not found");
                            dataGrid[j, 1] = driver._getText("xpath", xpathPrefix + "//tbody/tr/td[not(@class)]");
                            if (i == 0 && percTotal)
                            {
                                if(driver._isElementPresent("xpath", xpathPrefix + "//tbody/tr/td[contains(text(), '%')]"))
                                    totalPerc[j - 1] = driver._getText("xpath", xpathPrefix + "//tbody/tr/td[contains(text(), '%')]");
                                else
                                {
                                    totalPerc[j - 1] = driver._getText("xpath", xpathPrefix + "//tbody/tr/td[@class='text-right']");
                                }
                            }
                            break;
                        }
                    }
                    Assert.IsTrue(legendAvail, "'" + driver._getText("xpath", xpathPrefix + "//h5[@class='highchart-tooltip-title']") + "' is not present in legends.");
                }

                for (int i = 0; i < dataGrid.GetLength(0); i++)
                {
                    Console.WriteLine();
                    for (int j = 0; j < dataGrid.GetLength(1); j++)
                        Console.Write(dataGrid[i, j] + "\t");
                }

                Results.WriteStatus(test, "Pass", "Captured, Data from Chart '" + chartTitle + "'.");
                if (percTotal)
                {
                    string[,] totalPercGrid = new string[dimension1 - 1, 1];
                    for (int m = 0; m < totalPerc.Length; m++)
                        totalPercGrid[m, 0] = totalPerc[m];
                    return totalPercGrid;
                }
                else
                    return dataGrid;
            }
            else
            {
                IList<IWebElement> xAxisLabelCollection = chart._findElementsWithinElement("xpath", ".//*[name()='g' and contains(@class,'highcharts-xaxis-labels')]//*[name()='text']");
                Assert.Less(0, xAxisLabelCollection.Count, "'" + chartTitle + "' doesn't have data represented properly.");
                string[] xAxisLabelNames = new string[xAxisLabelCollection.Count];
                dimension1 = xAxisLabelNames.Length + 1;
                dimension2 = chartLegendCollection.Count + 1;
                string[,] dataGrid = new string[dimension1, dimension2];
                dataGrid[0, 0] = "Advertiser";
                string[] totalPerc = new string[dimension1 - 1];
                for (int i = 0; i < xAxisLabelCollection.Count; i++)
                {
                    IList<IWebElement> textCollection = xAxisLabelCollection[i]._findElementsWithinElement("xpath", ".//*[name()='tspan']");
                    if(textCollection.Count > 0)
                    {
                        foreach(IWebElement text in textCollection)
                            dataGrid[i + 1, 0] = text.Text + " ";
                        dataGrid[i + 1, 0] = dataGrid[i + 1, 0].TrimEnd(' ');
                    }
                    else
                        dataGrid[i + 1, 0] = xAxisLabelCollection[i].Text;
                    if (dataGrid[i + 1, 0].Contains("..."))
                        dataGrid[i + 1, 0] = dataGrid[i + 1, 0].Remove(dataGrid[i + 1, 0].Length - 4);
                    Console.WriteLine(dataGrid[i + 1, 0]);
                }

                for (int i = 0; i < chartLegendCollection.Count; i++)
                    dataGrid[0, i + 1] = chartLegendCollection[i].Text;


                IList<IWebElement> barCollection = chart._findElementsWithinElement("xpath", ".//*[name()='rect' and @stroke='#FFFFFF']");
                Assert.Less(0, barCollection.Count, "'" + chartTitle + "' doesn't have data represented properly.");
                string xValues = ",";
                for(int i = 0; i < barCollection.Count; i++)
                {
                    if(!xValues.Contains("," + barCollection[i].GetAttribute("x") + ","))
                    {
                        xValues = xValues + barCollection[i].GetAttribute("x") + ",";
                        Actions action = new Actions(driver);
                        action.MoveToElement(barCollection[i]).MoveByOffset(0, -4).Perform();
                        IList<IWebElement> tooltipCollection = chart._findElementsWithinElement("xpath", ".//div[@class='highcharts-tooltip']/span");
                        Assert.Less(0, tooltipCollection.Count, "Tooltip not present");
                        Assert.IsTrue(driver._waitForElement("xpath", xpathPrefix + "//h5[@class='highchart-tooltip-title']"), "'Tooltip title' not found");

                        bool labelAvail = false;
                        for (int k = 1; k < dataGrid.GetLength(0); k++)
                        {
                            if (driver._getText("xpath", xpathPrefix + "//h5[@class='highchart-tooltip-title']").ToLower().Contains(dataGrid[k, 0].ToLower()))
                            {
                                Console.WriteLine(driver._getText("xpath", xpathPrefix + "//h5[@class='highchart-tooltip-title']"));
                                labelAvail = true;
                                Assert.IsTrue(driver._isElementPresent("xpath", xpathPrefix + "//tbody/tr/td[not(@class)]"), "'Count' not found");
                                IList<IWebElement> classCollection = chart._findElementsWithinElement("xpath", ".//tbody/tr/td[text()][1]");
                                IList<IWebElement> countCollection = chart._findElementsWithinElement("xpath", ".//tbody/tr/td[text()][2]");
                                for(int l = 0; l < classCollection.Count; l++)
                                {
                                    bool classAvail = false;
                                    for (int j = 1; j < dataGrid.GetLength(1); j++)
                                    {
                                        if (classCollection[l].Text.ToLower().Contains(dataGrid[0, j].ToLower()))
                                        {
                                            classAvail = true;
                                            dataGrid[k, j] = countCollection[l].Text;
                                            totalPerc[j - 1] = countCollection[l].Text;
                                            break;
                                        }
                                    }
                                    Assert.IsTrue(classAvail, "'" + classCollection[l].Text + "' not found in dataGrid");
                                }
                                break;
                            }
                        }
                        Assert.IsTrue(labelAvail, "'" + driver._getText("xpath", xpathPrefix + "//h5[@class='highchart-tooltip-title']") + "' not found in dataGrid");
                    }
                }

                bool dollar = dataGrid[1, 1].Contains("$");

                for (int i = 0; i < dataGrid.GetLength(0); i++)
                    for (int j = 0; j < dataGrid.GetLength(1); j++)
                    {
                        if (dataGrid[i, j] == null)
                            dataGrid[i, j] = "";
                        else
                        {
                            string temp = dataGrid[i, j];
                            if (dataGrid[i, j].Contains('$'))
                                temp = temp.Substring(1);
                            decimal dTemp = 0;
                            if (Decimal.TryParse(temp, out dTemp) && dataGrid[i, j].Contains("."))
                                dataGrid[i, j] = dataGrid[i, j].Substring(0, dataGrid[i, j].IndexOf('.'));
                        }
                    }

                Results.WriteStatus(test, "Pass", "Captured, Data from Chart '" + chartTitle + "'.");
                if (percTotal)
                {
                    string[,] totalPercGrid = new string[dimension1 - 1, 1];
                    for (int m = 0; m < totalPerc.Length; m++)
                        totalPercGrid[m, 0] = totalPerc[m];
                    return totalPercGrid;
                }
                return dataGrid;
            }
        }

        ///<summary>
        ///Verify Data From Chart in Exported File
        ///</summary>
        ///<param name="fileName">Name of file to be verified</param>
        ///<param name="chartTitle">Chart of Data</param>
        ///<param name="dataGrid">Data Grid</param>
        ///<param name="screenName">Name of active screen</param>
        ///<returns></returns>
        public Charts VerifyDataFromChartInExportedFile(string fileName, string screenName, string chartTitle, string[,] dataGrid)
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
                if (xlWorkSheet.Name.Contains("Chart"))
                    break;
            }

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;

            Assert.IsTrue((range.Cells[2, 2] as Excel.Range).Text.ToLower().Contains(screenName.ToLower() + " - " + chartTitle), "Heading Text of Sheet does not match");
            for(int rCnt = 18, i = 0; rCnt < 27; rCnt++, i++)
            {
                Console.WriteLine();
                for (int cCnt = 2, j = 0; j < dataGrid.GetLength(1); cCnt++, j++)
                {
                    Console.Write("\t(" + i + "," + j + ")");
                    Assert.IsTrue((range.Cells[rCnt, cCnt] as Excel.Range).Text.Contains(dataGrid[i, j]), "'" + dataGrid[i, j] + "' not found in downloaded excel file");
                }
            }

            Results.WriteStatus(test, "Pass", "Verified, Data from chart '' in exported file");
            return new Charts(driver, test);
        }

        ///<summary>
        ///Verify Export Chart Functionality
        ///</summary>
        ///<param name="exportType">Type of File to export</param>
        ///<returns></returns>
        public Charts VerifyExportChartFunctionality(string exportType)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//chart-export//button[@uib-tooltip='Download']"), "'Download' button not present in Chart");
            IList<IWebElement> exportChartButtonColl = driver._findElements("xpath", "//chart-export//button[@uib-tooltip='Download']");
            exportChartButtonColl[0].Click();
            Assert.IsTrue(driver._waitForElement("xpath", "//div[1]/div/chart//chart-export//div[@class='NU-scrolling-area']/a"), "'Download Options' DDL not present in Chart");
            IList<IWebElement> exportChartOptionColl = driver._findElements("xpath", "//div[1]/div/chart//chart-export//div[@class='NU-scrolling-area']/a");
            string[] exportTypeNames = { "Download Excel", "Download JPEG", "Download PNG", "Download PDF" };
            int index = -1, i = 0 ;
            foreach (string exportOption in exportTypeNames)
            {
                bool avail = false;
                foreach (IWebElement exportChartOption in exportChartOptionColl)
                    if (exportChartOption.Text.ToLower().Contains(exportOption.ToLower()))
                    {
                        avail = true;
                        if (exportChartOption.Text.ToLower().Contains(exportType.ToLower()))
                            index = i;
                    }
                Assert.IsTrue(avail, "'" + exportOption + "' option not found in Export Chart Option DDL");
                ++i;
            }
            exportChartOptionColl[index].Click();
            Thread.Sleep(15000);
            Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[1]/div/chart//chart-export//div[@class='NU-scrolling-area']/a"), "'Download Options' DDL not closed for Chart");

            Results.WriteStatus(test, "Pass", "Verified, Export Chart Functionality of '" + exportType + "' file type.");
            return new Charts(driver, test);
        }

        ///<summary>
        ///Update And Verify Legends In Chart
        ///</summary>
        ///<returns></returns>
        public string[] updateAndVerifyLegendsInCharts(bool reselect = false)
        {
            string legend = "";
            string presentLegends = "";
            string xpathPrefix = "";

            if (driver._waitForElement("xpath", "//cft-domain-item-chart-carousel//div"))
                xpathPrefix = "//slide[@aria-hidden='false']";

            Thread.Sleep(1000);
            Assert.IsTrue(driver._isElementPresent("xpath", xpathPrefix + "//div[2]/div/chart//*[name()='g' and @class='highcharts-legend-item']"), "'Legends' not present in Chart");
            IList<IWebElement> legendsCollection = driver._findElements("xpath", xpathPrefix + "//div[2]/div/chart//*[name()='g' and @class='highcharts-legend-item']");
            Random rand = new Random();
            int x = rand.Next(0, legendsCollection.Count);
            Thread.Sleep(500);
            legendsCollection[x].Click();
            Thread.Sleep(1000);
            IList<IWebElement> legendTextColl = legendsCollection[x]._findElementsWithinElement("xpath", ".//*[text()]");
            IList<IWebElement> legendColorColl = legendsCollection[x]._findElementsWithinElement("xpath", ".//*[name()='rect']");
            legend = legendTextColl[0].Text;
            string legendFill = legendColorColl[0].GetAttribute("fill");
            Assert.AreEqual("#93A2AD", legendFill, "'" + legend + "' is successfully deselected");
            string[] presentColors = new string[legendsCollection.Count - 1];
            for (int i = 0; i < legendsCollection.Count; i++)
                if (i != x)
                {
                    IList<IWebElement> presentLegendTextColl = legendsCollection[i]._findElementsWithinElement("xpath", ".//*[text()]");
                    IList<IWebElement> presentLegendColorColl = legendsCollection[i]._findElementsWithinElement("xpath", ".//*[name()='rect']");
                    presentLegends = presentLegends + presentLegendTextColl[0].Text + "*";
                    if (i < x)
                        presentColors[i] = presentLegendColorColl[0].GetAttribute("fill");
                    else
                        presentColors[i - 1] = presentLegendColorColl[0].GetAttribute("fill");
                }

            if (driver._isElementPresent("xpath", xpathPrefix + "//div[2]/div/chart//*[name()='path' and not(@visibility) and @stroke-linejoin]"))
            {
                IList<IWebElement> chartPartitionsCollection = driver._findElements("xpath", xpathPrefix + "//div[2]/div/chart//*[name()='path' and not(@visibility) and @stroke-linejoin]");
                foreach (IWebElement chartPartition in chartPartitionsCollection)
                    Assert.AreNotEqual(legendFill, chartPartition.GetAttribute("fill"), "'" + legend + "' legend is still represented in the chart.");
            }
            else if (driver._isElementPresent("xpath", xpathPrefix + "//div[2]/div/chart//*[name()='rect' and @stroke='#FFFFFF']"))
            {
                IList<IWebElement> chartPartitionsCollection = driver._findElements("xpath", xpathPrefix + "//div[2]/div/chart//*[name()='rect' and @stroke='#FFFFFF']");
                foreach (IWebElement chartPartition in chartPartitionsCollection)
                    Assert.AreNotEqual(legendFill, chartPartition.GetAttribute("fill"), "'" + legend + "' legend is still represented in the chart.");
            }
            string[] newLegendSelection = { legend, presentLegends};
            Results.WriteStatus(test, "Pass", "Deselected and Verified updated chart for '" + legend + "'");


            if (reselect)
            {
                string chartTitle = driver._getText("xpath", xpathPrefix + "//div[2]/div/chart//*[name()='text' and @class='highcharts-title']/*[name()='tspan']");
                string[,] prevTotalPerc= captureDataFromChart(chartTitle, true);
                legendsCollection[x].Click();
                Thread.Sleep(1000);
                legendFill = legendColorColl[0].GetAttribute("fill");
                Assert.AreNotEqual("#93A2AD", legendFill, "'' is successfully reselected");

                if (driver._isElementPresent("xpath", xpathPrefix + "//div[2]/div/chart//*[name()='path' and not(@visibility) and @stroke-linejoin]"))
                {
                    IList<IWebElement> chartPartitionsCollection = driver._findElements("xpath", xpathPrefix + "//div[2]/div/chart//*[name()='path' and not(@visibility) and @stroke-linejoin]");
                    bool avail = false;
                    foreach (IWebElement chartPartition in chartPartitionsCollection)
                    {
                        if (chartPartition.GetAttribute("fill").Equals(legendFill))
                            avail = true;
                    }
                    Assert.IsTrue(avail, "'" + legendFill + "' fill is not present in the chart");
                }
                else if (driver._isElementPresent("xpath", xpathPrefix + "//div[2]/div/chart//*[name()='rect' and @stroke='#FFFFFF']"))
                {
                    IList<IWebElement> chartPartitionsCollection = driver._findElements("xpath", xpathPrefix + "//div[2]/div/chart//*[name()='rect' and @stroke='#FFFFFF']");
                    bool avail = false;
                    foreach (IWebElement chartPartition in chartPartitionsCollection)
                    {
                        if (chartPartition.GetAttribute("fill").Equals(legendFill))
                            avail = true;
                    }
                    Assert.IsTrue(avail, "'" + legendFill + "' fill is not present in the chart");
                }
                Results.WriteStatus(test, "Pass", "Reselected and Verified updated chart for '" + legend + "'");
                string[,] newTotalPerc = captureDataFromChart(chartTitle, true);
                string[] prevTotalPercList = new string[prevTotalPerc.GetLength(0)];
                for (int n = 0; n < prevTotalPercList.Length; n++)
                    prevTotalPercList[n] = prevTotalPerc[n, 0];
                string[] newTotalPercList = new string[newTotalPerc.GetLength(0)];
                for (int n = 0; n < newTotalPercList.Length; n++)
                    newTotalPercList[n] = newTotalPerc[n, 0];
                fieldOptions.compareListOfItemsInOrder(prevTotalPercList, newTotalPercList, false);
            }

            Results.WriteStatus(test, "Pass", "Updated And Verified, Chart Legend '" + legend + "'");
            return newLegendSelection;
        }

        ///<summary>
        ///Update And Verify Pie Chart
        ///</summary>
        ///<returns></returns>
        public string[] updateAndVerifypieChart(bool goBack = false)
        {
            string xpathPrefix = "";
            string[] chartInfo = new string[2];

            if (driver._waitForElement("xpath", "//cft-domain-item-chart-carousel//div"))
                xpathPrefix = "//slide[@aria-hidden='false']";
            else
                xpathPrefix = "//cft-domain-item-chart-grid";

            if (xpathPrefix == "//cft-domain-item-chart-grid")
                Assert.IsTrue(driver._isElementPresent("xpath", "//div/div/chart//*[name()='path' and not(@visibility) and @stroke-linejoin]"), "No Pie Charts present on screen");
            else
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/ol/li"), "Sliders not present in Chart section");
                IList<IWebElement> sliderCollection = driver._findElements("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/ol/li");
                bool pieAvail = false;
                foreach (IWebElement slider in sliderCollection)
                {
                    if (driver._isElementPresent("xpath", xpathPrefix + "//div/div/chart//*[name()='path' and not(@visibility) and @stroke-linejoin]"))
                    {
                        pieAvail = true;
                        break;
                    }
                    else
                    {
                        Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/a[contains(@class,'right')]"), "Right Navigation Arrow not present in Chart section");
                        driver._click("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/a[contains(@class,'right')]");
                        Thread.Sleep(10000);
                    }
                }
                Assert.IsTrue(pieAvail, "Pie Chart is not present.");
            }

            IList<IWebElement> chartsCollection = driver._findElements("xpath", xpathPrefix + "//div[@class='NU-chart card']");
            foreach(IWebElement currChart in chartsCollection)
            {
                IList<IWebElement> pieSectorCollection = currChart._findElementsWithinElement("xpath", ".//*[name()='path' and not(@visibility) and @stroke-linejoin]");
                if(pieSectorCollection.Count > 0)
                {
                    IList<IWebElement> chartTitleCollection = currChart._findElementsWithinElement("xpath", ".//*[name()='text' and @class='highcharts-title']/*[name()='tspan']");
                    IList<IWebElement> chartLegendCollection = currChart._findElementsWithinElement("xpath", ".//*[name()='g' and @class='highcharts-legend-item']");
                    int prevLegendCount = chartLegendCollection.Count, prevSectorCount = pieSectorCollection.Count;
                    string chartTitle = chartsCollection[0].Text;
                    Random rand = new Random();
                    int x = rand.Next(0, pieSectorCollection.Count);
                    pieSectorCollection[x].Click();
                    Thread.Sleep(2000);
                    Assert.IsTrue(driver._isElementPresent("xpath", "//div/div/chart//*[name()='path' and not(@visibility) and @stroke-linejoin]"), "No Pie Chart Sectors are present on screen");
                    pieSectorCollection = currChart._findElementsWithinElement("xpath", ".//*[name()='tspan' and contains(text(), 'Back to Top 8 Competitors')]");
                    Assert.AreNotEqual(prevSectorCount, pieSectorCollection.Count, "Pie Chart did not get updated.");
                    Assert.IsTrue(driver._waitForElement("xpath", "//*[name()='tspan' and contains(text(), 'Back to Top 8 Competitors')]") || driver._waitForElement("xpath", "//*[name()='tspan' and contains(text(), 'Back to Top Companies')]"), "'Back to Top 8 Competitors/Companies' text and link not present.");
                    chartLegendCollection = currChart._findElementsWithinElement("xpath", ".//*[name()='g' and @class='highcharts-legend-item']");
                    Assert.AreNotEqual(prevLegendCount, chartLegendCollection.Count, "Pie Chart Legends did not get updated.");
                    string updatedLegend = chartLegendCollection[0].Text;
                    chartInfo[0] = chartTitle;
                    chartInfo[1] = updatedLegend;
                    if (goBack)
                    {
                        driver._click("xpath", ".//*[name()='tspan' and contains(text(), 'Back to Top 8 Competitors')]");
                        Thread.Sleep(2000);
                        chartLegendCollection = currChart._findElementsWithinElement("xpath", ".//*[name()='g' and @class='highcharts-legend-item']");
                        Assert.AreEqual(prevLegendCount, chartLegendCollection.Count, "Pie Chart Legends did not get updated.");
                    }
                    break;
                }
            }


            Results.WriteStatus(test, "Pass", "Updated And Verified, Pie Chart '" + chartInfo[0] + "'");
            return chartInfo;
        }

        ///<summary>
        ///Verify Pivot Table
        ///</summary>
        ///<returns></returns>
        public Charts VerifyPivotTable()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//span[text()='Bulk actions for your pivot view:']"), "'Bulk actions for your pivot view:' text not present");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-table//a[@dropdowntoggle]"), "'Pivot Bulk Actions' Button not present");
            Assert.AreEqual("Pivot Bulk Actions", driver._getText("xpath", "//cft-pivot-table//a[@dropdowntoggle]"), "'Pivot Bulk Actions' Button text does not match.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//span[text()='Bulk actions for your pivot view:']"), "'Customize your tabular layout:' text not present");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-pivot-options//button"), "'Pivot Options' Button not present");
            Assert.AreEqual("Pivot Options ...", driver._getText("xpath", "//cft-pivot-options//button"), "'Pivot Options' Button text does not match.");

            if (driver._waitForElement("xpath", "//cft-pivot-table//ag-grid-angular//div[@role='grid']"))
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-header']//div[contains(@class,'ag-header-cell') and @style]"), "'Pinned Columns' headers not present in the table.");
                string[] pinnedColumnNames = { "Company", "Division" };
                IList<IWebElement> pinnedColumnColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-header']//div[contains(@class,'ag-header-cell') and @style]");
                bool avail = false;
                foreach (string columnName in pinnedColumnNames)
                {
                    avail = false;
                    foreach (IWebElement pinnedColumn in pinnedColumnColl)
                        if (pinnedColumn.Text.ToLower().Contains(columnName.ToLower()))
                        {
                            avail = true;
                            break;
                        }
                    Assert.IsTrue(avail, "'" + columnName + "' not present in Pinned Columns");
                }

                avail = false;
                foreach (IWebElement pinnedColumn in pinnedColumnColl)
                    if (pinnedColumn.Text.ToLower().Contains("class") || pinnedColumn.Text.ToLower().Contains("brand"))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "'Brand/Class' not present in Pinned Columns");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-cols-container']//div[@role='row' and @row-id]"), "'Pinned Columns' not present in the table.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-header-container']//div[contains(@class,'ag-header-group-cell') and @style]"), "'Category Columns' not in Pivot Table.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-header-container']//div[contains(@class,'ag-header-cell') and @style]"), "'Spend CP' column headers not present under category columns in Pivot Table");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-body-container']//div[@role='row' and @row-id]"), "'Spend CP' columns not present under category columns in Pivot Table");
            }
            else
                Assert.IsTrue(driver._waitForElement("xpath", "//*[contains(text(), 'Error loading pivot data')]"), "Error message not dispalyed");

            Results.WriteStatus(test, "Pass", "Verified, Pivot Table");
            return new Charts(driver, test);
        }

        ///<summary>
        ///Choose Pivot Bulk Actions for Data From Pivot Table
        ///</summary>
        ///<param name="actionOption">Export Option to be clicked</param>
        ///<returns></returns>
        public Charts choosePivotBulkActionsForDataFromPivotTable(string actionOption, bool VerifyDownload = true, bool searchModified = false)
        {
            string[] actionOptionNames = { "Download Grid", "Create an Alert", "View Selected", "Reset Selected" };
            driver._click("xpath", "//cft-pivot-table//a[@dropdowntoggle]");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[contains(@class,'NU-scrolling-area')]//button[not(@hidden)]"), "Pivot Bulk Actions DDL not present.");
            IList<IWebElement> actionsOptionDDL = driver._findElements("xpath", "//div[contains(@class,'NU-scrolling-area')]//button[not(@hidden)]");
            int index = -1;
            foreach(string actionOptionName in actionOptionNames)
            {
                bool avail = false;
                for(int i = 0;  i < actionsOptionDDL.Count; i++)
                {
                    if (actionsOptionDDL[i].Text.ToLower().Contains(actionOptionName.ToLower()))
                    {
                        avail = true;
                        if (actionOption.ToLower().Equals(actionOptionName.ToLower()))
                            index = i;
                        if (actionsOptionDDL[i].Text.ToLower().Contains("download grid"))
                            Assert.AreNotEqual("true", actionsOptionDDL[i].GetAttribute("disabled"), "'Download Grid' option is disabled");
                        else
                        {
                            if (driver._getText("xpath", "//cft-saved-search-dropdown//button[@dropdowntoggle]").ToLower().Contains("untitled search") && !driver._isElementPresent("xpath", "//cft-pivot-table//div[@role='gridcell' and contains(@class, 'selected')]"))
                                Assert.AreEqual("true", actionsOptionDDL[i].GetAttribute("disabled"), "'" + actionOptionName + "' option is not disabled");
                            else if (searchModified && actionsOptionDDL[i].Text.ToLower().Contains("create an alert"))
                                    Assert.AreEqual("true", actionsOptionDDL[i].GetAttribute("disabled"), "'" + actionOptionName + "' option is not disabled");
                        }
                        break;
                    }
                }
                if(actionOption != "")
                    Assert.IsTrue(avail, "'" + actionOptionName + "' not found in actions options DDL.");
            }
            if (actionOption != "")
            {
                Assert.Less(-1, index, "'" + actionOption + "' not found in actions options DDL.");
                actionsOptionDDL[index].Click();
            }

            if (VerifyDownload)
            {
                Thread.Sleep(5000);
                Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//cft-pivot-table//div[@dropdown]//span[text()='Download in Progress ']"), "'Download In Progress' Button is still present");
                Thread.Sleep(15000);
            }

            Results.WriteStatus(test, "Pass", "Exported, Data From Pivot Table as 'Grid'");
            return new Charts(driver, test);
        }

        ///<summary>
        ///Capture Data From Pivot Table
        ///</summary>
        ///<returns></returns>
        public string[,] captureDataFromPivotTable()
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-header']//div[contains(@class,'ag-header-cell') and @style]"), "'Pinned Columns' headers not present in the table.");
            IList<IWebElement> pinnedColumnColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-header']//div[contains(@class,'ag-header-cell') and @style]");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", pinnedColumnColl[0]);
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-cols-container']//div[@role='row' and @row-id]"), "'Pinned Columns' not present in the table.");
            IList<IWebElement> pinnedRowsValuesColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-pinned-left-cols-container']//div[@role='row' and @row-id]");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-header-container']//div[contains(@class,'ag-header-group-cell') and @style]"), "'Category Columns' not in Pivot Table.");
            IList<IWebElement> categoryColumnColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-header-container']//div[contains(@class,'ag-header-group-cell') and @style]");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-pivot-table//div[@class='ag-body-container']//div[@role='row' and @row-id]"), "'Spend CP' columns not present under category columns in Pivot Table");
            IList<IWebElement> categoryRowsValuesColl = driver._findElements("xpath", "//cft-pivot-table//div[@class='ag-body-container']//div[@role='row' and @row-id]");
            int dimension1 = 0, dimension2 = 0;
            if (categoryColumnColl.Count > 7)
                dimension2 = 10;
            else
                dimension2 = 3 + categoryColumnColl.Count;
            if (pinnedRowsValuesColl.Count > 10)
                dimension1 = 12;
            else
                dimension1 = 2 + pinnedRowsValuesColl.Count;
            string[,] dataGrid = new string[dimension1, dimension2];

            for (int i = 0; i < 3; i++)
            {
                dataGrid[0, i] = pinnedColumnColl[i].Text;
                dataGrid[1, i] = "";
            }
            for (int i = 3; i < dimension2; i++)
            {
                dataGrid[0, i] = categoryColumnColl[i-3].Text;
                dataGrid[1, i] = "Spend CP";
            }

            int k = 1;
            for (int i = 2; i < dimension1; i++)
            {
                Console.WriteLine();
                int l = 1, m = 0;
                for (int j = 0; j < dimension2; j++)
                {
                    Console.Write("(" + i +","+ j + ")(" + k + "," + l + ")\t");
                    string classTagValue = "ag-pinned-left-cols-container";
                    if (j > 2)
                    {
                        classTagValue = "ag-body-container";
                        m = l - 4;
                        if(m == 0)
                            dataGrid[i, j] = driver._getText("xpath", "//cft-pivot-table//div[@class='" + classTagValue + "']//div[@role='row' and @row-id][" + k + "]/div[@col-id='undefined']/span");
                        else
                        dataGrid[i, j] = driver._getText("xpath", "//cft-pivot-table//div[@class='" + classTagValue + "']//div[@role='row' and @row-id][" + k + "]/div[@col-id='undefined_" + m + "']/span");
                    }
                    else
                        dataGrid[i, j] = driver._getText("xpath", "//cft-pivot-table//div[@class='" + classTagValue + "']//div[@role='row' and @row-id][" + k + "]/div[" + l + "]/span");
                    ++l;
                }
                ++k;
            }

            for(int i = 0; i < dataGrid.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < dataGrid.GetLength(1); j++)
                    Console.Write(dataGrid[i, j] + "\t");
            }

            Results.WriteStatus(test, "Pass", "Capture Data From Pivot Table");
            return dataGrid;
        }

        ///<summary>
        ///Verify Data From Pivot Table In Exported Excel File
        ///</summary>
        ///<returns></returns>
        public Charts VerifyDataFromPivotTableInExportedExcelFile(string fileName, string[,] dataGrid)
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

            for (int rCnt = 5, i = 0; i < dataGrid.GetLength(0); rCnt++, i++)
            {
                Console.WriteLine();
                for (int cCnt = 1, j = 0; j < dataGrid.GetLength(1); cCnt++, j++)
                {
                    Console.Write((range.Cells[rCnt, cCnt] as Excel.Range).Text + "(" + i + "," + j + ")\t");
                    string excelCellValue = "", dataGridValue = "";
                    for (int x = 0; x < (range.Cells[rCnt, cCnt] as Excel.Range).Text.Length; x++)
                        if ((range.Cells[rCnt, cCnt] as Excel.Range).Text.ToCharArray()[x] != ' ')
                            excelCellValue = excelCellValue + (range.Cells[rCnt, cCnt] as Excel.Range).Text.ToCharArray()[x].ToString();
                    for (int x = 0; x < dataGrid[i,j].Length; x++)
                        if (dataGrid[i, j].ToCharArray()[x] != ' ')
                            dataGridValue = dataGridValue + dataGrid[i, j].ToCharArray()[x].ToString();

                    Assert.IsTrue(excelCellValue.ToLower().Contains(dataGridValue.ToLower()), "'" + dataGrid[i, j] + "' not found in downloaded excel file");
                }
            }

            Results.WriteStatus(test, "Pass", "Verified, Data from chart '' in exported file");
            return new Charts(driver, test);
        }

        ///<summary>
        ///Verify Tooltips On Charts
        ///</summary>
        ///<returns></returns>
        public Charts VerifyTooltipsOnCharts(string chartTitle = "")
        {
            string xpathPrefix = "";
            int n = 0;

            if (driver._waitForElement("xpath", "//cft-domain-item-chart-carousel//div"))
                xpathPrefix = "//slide[@aria-hidden='false']";
            else
                xpathPrefix = "//cft-domain-item-chart-grid";

            Thread.Sleep(15000);
            IWebElement chart = null;
            Actions action = new Actions(driver);
            IList<IWebElement> chartsCollection = driver._findElements("xpath", xpathPrefix + "//div[@class='NU-chart card']");
            IList<IWebElement> chartTitleCollection = chartsCollection[n]._findElementsWithinElement("xpath", ".//*[name()='text' and @class='highcharts-title']/*[name()='tspan']");

            if (chartTitle != "")
            {
                if (xpathPrefix == "//slide[@aria-hidden='false']")
                {
                    int sliderNo = -1;
                    Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/ol/li"), "Sliders not present in Chart section");
                    IList<IWebElement> sliderCollection = driver._findElements("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/ol/li");
                    foreach (IWebElement slider in sliderCollection)
                    {
                        ++sliderNo;
                        chartsCollection = driver._findElements("xpath", xpathPrefix + "//div[@class='NU-chart card']");
                        foreach (IWebElement currChart in chartsCollection)
                        {
                            chartTitleCollection = currChart._findElementsWithinElement("xpath", ".//*[name()='text' and @class='highcharts-title']/*[name()='tspan']");
                            if (chartTitleCollection[0].Text.ToLower().Contains(chartTitle.ToLower()))
                            {
                                chart = currChart;
                                break;
                            }
                        }
                        if (chart != null)
                            break;
                        else
                        {
                            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/a[contains(@class,'right')]"), "Right Navigation Arrow not present in Chart section");
                            driver._click("xpath", "//cft-domain-item-chart-carousel//div[@class='carousel slide']/a[contains(@class,'right')]");
                            Thread.Sleep(10000);
                        }
                    }
                }
                else
                {
                    chartsCollection = driver._findElements("xpath", xpathPrefix + "//div[@class='NU-chart card']");
                    foreach (IWebElement currChart in chartsCollection)
                    {
                        chartTitleCollection = currChart._findElementsWithinElement("xpath", ".//*[name()='text' and @class='highcharts-title']/*[name()='tspan']");
                        if (chartTitleCollection[0].Text.ToLower().Contains(chartTitle))
                        {
                            chart = currChart;
                            break;
                        }
                    }
                }
                Assert.AreNotEqual(null, chart, "'" + chartTitle + "' not found.");
            }

            chartTitle = chartTitleCollection[0].Text;
            chart = chartsCollection[n];

            IList<IWebElement> pieSectorCollection = chart._findElementsWithinElement("xpath", ".//*[name()='path' and not(@visibility) and @stroke-linejoin]");
            if (pieSectorCollection.Count > 0)
            {
                action.MoveToElement(pieSectorCollection[0]).MoveByOffset(0, -4).Perform();
                IList<IWebElement> tooltipCollection = chart._findElementsWithinElement("xpath", ".//div[@class='highcharts-tooltip']/span");
                Assert.Less(0, tooltipCollection.Count, "Tooltip not present");
                Assert.IsTrue(driver._waitForElement("xpath", xpathPrefix + "//h5[@class='highchart-tooltip-title']"), "'Tooltip title' not found");
                Assert.IsTrue(driver._waitForElement("xpath", xpathPrefix + "//table//th[text()='% of Total']"), "'% of Total' not present in chart tooltip.");
            }
            else
            {
                IList<IWebElement> barCollection = chart._findElementsWithinElement("xpath", ".//*[name()='rect' and @stroke='#FFFFFF']");
                Assert.Less(0, barCollection.Count, "'" + chartTitle + "' doesn't have data represented properly.");
                action.MoveToElement(barCollection[0]).MoveByOffset(0, -4).Perform();
                IList<IWebElement> tooltipCollection = chart._findElementsWithinElement("xpath", ".//div[@class='highcharts-tooltip']/span");
                Assert.Less(0, tooltipCollection.Count, "Tooltip not present");
                Assert.IsTrue(driver._waitForElement("xpath", xpathPrefix + "//h5[@class='highchart-tooltip-title']"), "'Tooltip title' not found");
                Assert.IsTrue(driver._waitForElement("xpath", xpathPrefix + "//table//th[text()='% of Total']"), "'% of Total' not present in chart tooltip.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Tooltips On Chart '" + chartTitle + "'.");
            return new Charts(driver, test);
        }

        ///<summary>
        ///Verify Date on Chart
        ///</summary>
        ///<returns></returns>
        public Charts VerifyDateOnChart()
        {
            DateTime fromDate = DateTime.Today;
            DateTime toDate = DateTime.Today;
            string fDate = "", tDate = "";
            if (summaryTags.VerifySummaryTags(new string[] { "Last Month" }, false, true))
            {
                fromDate = toDate.AddMonths(-1);
                fDate = fromDate.ToString("M/d/yy");
                tDate = toDate.ToString("M/d/yy");
            }
            else if (summaryTags.VerifySummaryTags(new string[] { "Last 3 Months" }, false, true))
            {
                fromDate = toDate.AddMonths(-3);
                fDate = fromDate.ToString("M/d/yy");
                tDate = toDate.ToString("M/d/yy");
            }
            else if (summaryTags.VerifySummaryTags(new string[] { "Last 6 Months" }, false, true))
            {
                fromDate = toDate.AddMonths(-3);
                fDate = fromDate.ToString("M/d/yy");
                tDate = toDate.ToString("M/d/yy");
            }
            else if (summaryTags.VerifySummaryTags(new string[] { "Year To Date" }, false, true))
            {
                int year = toDate.Year;
                Assert.IsTrue(DateTime.TryParse("1/1/" + year.ToString(), out fromDate), "Couldn't convert 'from' date");
                fDate = fromDate.ToString("M/d/yy");
                tDate = toDate.ToString("M/d/yy");
            }
            else if (summaryTags.VerifySummaryTags(new string[] { "Last Year" }, false, true))
            {
                int year = toDate.Year;
                Assert.IsTrue(DateTime.TryParse("1/1/" + year.ToString(), out fromDate), "Couldn't convert 'from' date");
                fromDate = fromDate.AddYears(-1);
                fDate = fromDate.ToString("M/d/yy");
                tDate = toDate.ToString("M/d/yy");
            }
            else if (summaryTags.VerifySummaryTags(new string[] { "Last 14 Days" }, false, true))
            {
                fromDate = toDate.AddDays(-13);
                fDate = fromDate.ToString("M/d/yy");
                tDate = toDate.ToString("M/d/yy");
            }
            else if (summaryTags.VerifySummaryTags(new string[] { "Last 7 Days" }, false, true))
            {
                fromDate = toDate.AddDays(-6);
                fDate = fromDate.ToString("M/d/yy");
                tDate = toDate.ToString("M/d/yy");
            }
            else if (summaryTags.VerifySummaryTags(new string[] { "Yesterday" }, false, true))
            {
                fromDate = toDate.AddDays(-1);
                fDate = fromDate.ToString("M/dd/yy");
                tDate = toDate.ToString("M/d/yy");
            }
            else if (summaryTags.VerifySummaryTags(new string[] { "Today" }, false, true))
            {
                fromDate = toDate;
                fDate = fromDate.ToString("M/d/yy");
                tDate = toDate.ToString("M/d/yy");
            }
            else
            {
                fDate = driver._getText("xpath", "//div[@class='NU-tag-label' and contains(text(), '/')]");
                string[] dateSplit = fDate.Split(' ');
                Assert.IsTrue(DateTime.TryParse(dateSplit[0], out fromDate), "Couldn't convert '" + dateSplit[0] + "' to DateTime format.");
                Assert.IsTrue(DateTime.TryParse(dateSplit[2], out toDate), "Couldn't convert '" + dateSplit[2] + "' to DateTime format.");
                fDate = fromDate.ToString("M/d/yy");
                tDate = toDate.ToString("M/d/yy");
            }

            string xpathPrefix = "";

            if (driver._waitForElement("xpath", "//cft-domain-item-chart-carousel//div"))
                xpathPrefix = "//slide[@aria-hidden='false']";
            else
                xpathPrefix = "//cft-domain-item-chart-grid";
            string dateInFilter = fDate + " to " + tDate;
            driver._scrollintoViewElement("xpath", xpathPrefix + "//div[@class='NU-chart card']");
            IList<IWebElement> chartsCollection = driver._findElements("xpath", xpathPrefix + "//div[@class='NU-chart card']");
            IWebElement chart = chartsCollection[0];
            IList<IWebElement> chartDateColl = chart._findElementsWithinElement("xpath", "//*[@id = 'MediaTypeChartTimeframe']/*[name()='tspan']");
            string dateInChart = chartDateColl[0].Text;

            Assert.AreEqual(dateInFilter, dateInChart, "Date in Chart is not as per the date in Filter");

            Results.WriteStatus(test, "Pass", "Verified, Date on Chart is as per the Search Filter");
            return new Charts(driver, test);
        }

        ///<summary>
        ///Verify Data From Two Charts
        ///</summary>
        ///<param name="dataGrid1">First Data Grid</param>
        ///<param name="dataGrid2">Second Data Grid</param>
        ///<returns></returns>
        public Charts VerifyDataFromTwoCharts(string[,] dataGrid1, string[,] dataGrid2, bool match)
        {
            if (match)
            {
                Assert.IsTrue(dataGrid1.GetLength(0) == dataGrid2.GetLength(0) && dataGrid1.GetLength(1) == dataGrid2.GetLength(1), "Dimensions of both datagrids don't match.");
                for(int i = 1; i < dataGrid1.GetLength(0); i++)
                {
                    for (int j = 1; j < dataGrid1.GetLength(1); j++)
                        Assert.IsTrue(dataGrid1[i, j].Equals(dataGrid2[i, j]), "Data from both charts do not match");
                }
                Results.WriteStatus(test, "Pass", "Verified, Data From Two Charts match as expected");
            }
            else
            {
                if (dataGrid1.GetLength(0) == dataGrid2.GetLength(0) && dataGrid1.GetLength(1) == dataGrid2.GetLength(1))
                {
                    int equalValues = 0;
                    bool lastValueEqual = false;
                    for (int i = 1; i < dataGrid1.GetLength(0); i++)
                    {
                        for (int j = 1; j < dataGrid1.GetLength(1); j++)
                        {
                            if (dataGrid1[i, j].Equals(dataGrid2[i, j]))
                            {
                                lastValueEqual = true;
                                ++equalValues;
                            }
                            else if (lastValueEqual)
                            {
                                lastValueEqual = false;
                                --equalValues;
                            }
                        }
                    }
                    Assert.Less(equalValues, (dataGrid1.GetLength(0) - 1) * (dataGrid1.GetLength(1) - 1), "Data From Two Charts match");
                }
                else
                    Results.WriteStatus(test, "Pass", "Verified, Data From Two Charts don't match as expected");
            }

            Results.WriteStatus(test, "Pass", "Verified, Data From Two Charts");
            return new Charts(driver, test);
        }

        ///<summary>
        ///Verify Expand Chart Functionality
        ///</summary>
        ///<returns></returns>
        public Charts VerifyExpandChartFunctionality(bool goBack = false)
        {
            string xpathPrefix = "", chartTitle = "";

            if (driver._waitForElement("xpath", "//cft-domain-item-chart-carousel//div"))
                xpathPrefix = "//slide[@aria-hidden='false']";
            else
                xpathPrefix = "//cft-domain-item-chart-grid";

            Thread.Sleep(15000);
            IWebElement chart = null;
            IList<IWebElement> chartsCollection = driver._findElements("xpath", xpathPrefix + "//div[@class='NU-chart card']");
            IList<IWebElement> chartTitleCollection = chartsCollection[0]._findElementsWithinElement("xpath", ".//*[name()='text' and @class='highcharts-title']/*[name()='tspan']");
            chartTitle = chartTitleCollection[0].Text;
            chart = chartsCollection[0];

            IList<IWebElement> chartExpandIconColl = driver._findElements("xpath", "//chart-export//button[@uib-tooltip='Expand']");
            Assert.Greater(chartExpandIconColl.Count, 0, "Chart Expand Icon not present");
            chartExpandIconColl[0].Click();

            Thread.Sleep(20000);

            Assert.IsTrue(driver._waitForElement("xpath", "//chart-export//button[@uib-tooltip='Go Back']"), "Chart '" + chartTitle + "' did not get expanded.");

            Assert.IsTrue(driver._waitForElement("xpath", "//*[name()='text' and @class='highcharts-title']/*[name()='tspan']"), "Chart Title not present in Chart");
            Assert.IsTrue(driver._getText("xpath", "//*[name()='text' and @class='highcharts-title']/*[name()='tspan']").Contains(chartTitle), "Chart Title of Chart '" + chartTitle + "' does not match when expanded");
            Assert.IsTrue(driver._isElementPresent("xpath", "//*[name()='text' and text()='Numerator']"), "'Numerator' text label not present in Chart");
            Assert.IsTrue(driver._isElementPresent("xpath", "//*[name()='text' and @id= 'MediaTypeChartTimeframe']"), "'Dates' not present in Chart");
            Assert.IsTrue(driver._isElementPresent("xpath", "//chart-export//button[@uib-tooltip='Download']"), "'Download' button not present in Chart");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-scheduled-export-modal//button"), "'Schedule Export' button not present in Chart");
            Assert.IsTrue(driver._isElementPresent("xpath", "//*[name()='g' and @class='highcharts-legend-item']"), "'Legends' not present in Chart");

            if (goBack)
            {
                driver._click("xpath", "//chart-export//button[@uib-tooltip='Go Back']");
                Thread.Sleep(2000);
                driver._scrollintoViewElement("xpath", "//cft-saved-search-save-modal/button");
                Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-saved-search-save-modal/button", "disabled") == null, "Save Button is disabled.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Expand Chart Functionality");
            return new Charts(driver, test);
        }

        ///<summary>
        ///Verify AgGrid As Per Selected Bar From Expanded Chart
        ///</summary>
        ///<returns></returns>
        public Charts VerifyAgGridAsPerSelectedBarFromExpandedChart()
        {
            IList<IWebElement> legendsCollection = driver._findElements("xpath", "//*[name()='g' and @class='highcharts-legend-item']");
            Random rand = new Random();
            int x = rand.Next(0, legendsCollection.Count);
            IList<IWebElement> legendsNameColl = legendsCollection[x]._findElementsWithinElement("xpath", ".//*[name()='tspan']");
            string selectedLegend = legendsNameColl[0].Text;
            IList<IWebElement> legendsColorColl = legendsCollection[x]._findElementsWithinElement("xpath", ".//*[name()='rect']");
            string selectedLegendFill = legendsColorColl[0].GetAttribute("fill");

            Assert.IsTrue(driver._isElementPresent("xpath", "//*[name()='rect' and @stroke and @fill='" + selectedLegendFill + "' and not(@height='0')]"), "Representation of '" + selectedLegend + "' not present in expanded chart.");
            IList<IWebElement> barColl = driver._findElements("xpath", "//*[name()='rect' and @stroke and @fill='" + selectedLegendFill + "' and not(@height='0')]");
            x = rand.Next(0, barColl.Count);
            Actions action = new Actions(driver);
            Thread.Sleep(500);
            action.MoveToElement(barColl[x]).MoveByOffset(4, 4).Click().Perform();
            //barColl[x].Click();

            Thread.Sleep(15000);
            Assert.IsTrue(driver._waitForElement("xpath", "//*[name()='rect' and @stroke and @fill='#C0C0C0']"), "Selected bar did not change color to grey.");
            driver._scrollintoViewElement("xpath", "//cft-saved-search-save-modal/button");

            if (selectedLegend.ToLower().Equals("television"))
                selectedLegend = "TV";
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='ag-body-viewport']//div[@role='gridcell']"), "Values not present in AgGrid.");
            IList<IWebElement> gridValueCol = driver._findElements("xpath", "//div[@class='ag-body-viewport']//div[@role='gridcell']");
            string colId = "";
            foreach(IWebElement gridValue in gridValueCol)
                if (gridValue.Text.ToLower().Contains(selectedLegend.ToLower()))
                {
                    colId = gridValue.GetAttribute("col-id");
                    break;
                }
            Assert.AreNotEqual("", colId, "'" + selectedLegend + "' not found in AgGrid.");

            IList<IWebElement> colIdValueCol = driver._findElements("xpath", "//div[@class='ag-body-viewport']//div[@col-id='" + colId + "']");
            foreach (IWebElement colIdValue in colIdValueCol)
                Assert.IsTrue(colIdValue.Text.ToLower().Contains(selectedLegend.ToLower()), "AgGrid was not As Per Selected Bar From Expanded Chart");

            Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-saved-search-save-modal/button", "disabled") != null, "Save Button is not disabled.");

            Results.WriteStatus(test, "Pass", "Verified, AgGrid As Per Selected Bar From Expanded Chart");
            return new Charts(driver, test);
        }

    }
}
