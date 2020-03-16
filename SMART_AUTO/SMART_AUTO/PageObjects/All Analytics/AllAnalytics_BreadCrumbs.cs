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
    public class AllAnalytics_BreadCrumbs
    {
        #region Private Variables

        private IWebDriver allAnayticsBreadCrumbs;
        private ExtentTest test;
        Home homePage;
        Charts charts;
        ViewAdPopup viewAdPopup;
        Carousels carousels;

        #endregion

        public AllAnalytics_BreadCrumbs(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.allAnayticsBreadCrumbs = driver;
            test = testReturn;

            homePage = new Home(driver, test);
            charts = new Charts(driver, test);
            viewAdPopup = new ViewAdPopup(driver, test);
            carousels = new Carousels(driver, test);
        }

        public IWebDriver driver
        {
            get { return this.allAnayticsBreadCrumbs; }
            set { this.allAnayticsBreadCrumbs = value; }
        }

        ///<summary>
        ///Verify Reports List Breadcrumb And Select Report
        ///</summary>
        ///<returns></returns>
        public AllAnalytics_BreadCrumbs VerifyReportsListBreadcrumbAndSelectReport(bool allAnalytics, string option = "", string accountName = "QA Testing - Brand")
        {
            string currentReport = homePage.getActiveScreenNameFromSideNavigationBar();

            string[] reportsList = homePage.getAllSidebarOptions();

            bool avail = false;
            foreach (string report in reportsList)
            {
                if (report.ToLower().Equals("branded content"))
                {
                    avail = true;
                    break;
                }
            }
            if (accountName.ToLower().Equals("QA Testing - Brand".ToLower()))
                Assert.IsTrue(avail, "'Branded Content' Link is not present for account 'QA Testing - Brand'.");
            else
                Assert.IsFalse(avail, "'Branded Content' Link is present for account other than 'QA Testing - Brand'.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-sidebar-navigation//button[contains(@class, 'sidebar-toggle')]"), "'Collapse Navigation Menu' Button not present.");
            driver._click("xpath", "//cft-sidebar-navigation//button[contains(@class, 'sidebar-toggle')]");

            Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//cft-sidebar-navigation//div[contains(@class, 'block open')]"), "Navigation Menu did not collapse.");
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-sidebar-navigation//div[contains(@class, 'block')]"), "'Collapsed' Navigation Menu not present.");

            string xPath = "//ol[contains(@class, 'breadcrumbs')]//button[not(text())]";
            if (allAnalytics)
                xPath = "//ol[contains(@class, 'breadcrumbs')]//button[text()]";
            else
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//ol[contains(@class, 'breadcrumbs')]//button[not(text())]"), "'Reports List' Breadcrumb not present on Home Page.");
                Assert.AreEqual(currentReport, driver._getText("xpath", "//ol[contains(@class, 'breadcrumbs')]//button/span[text()]"), "Current Report Name not displayed as Breadcrumb name.");
            }
            driver._click("xpath", xPath);

            Assert.IsTrue(driver._waitForElement("xpath", "//li[contains(@class, 'breadcrumb-item') and contains(@class, 'open')]//div[@class='NU-scrolling-area']/a"), "Reports List from Breadcrumb not present.");
            IList<IWebElement> listItemCollection = driver._findElements("xpath", "//li[contains(@class, 'breadcrumb-item') and contains(@class, 'open')]//div[@class='NU-scrolling-area']/a");

            IWebElement select = null;
            if (!allAnalytics)
            {
                foreach (string report in reportsList)
                {
                    avail = false;
                    foreach (IWebElement item in listItemCollection)
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", item);
                        if (item.Text.ToLower().Equals(report.ToLower()))
                        {
                            avail = true;
                            if (report.ToLower().Equals(option.ToLower()))
                                select = item;
                            break;
                        }
                    }
                    Assert.IsTrue(avail, "'" + report + "' not present in Reports List.");
                }
                avail = false;
                foreach (IWebElement item in listItemCollection)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", item);
                    if (item.Text.ToLower().Equals("branded content"))
                    {
                        avail = true;
                        break;
                    }
                }
                if (accountName.ToLower().Equals("QA Testing - Brand".ToLower()))
                {
                    Assert.IsTrue(avail, "'Branded Content' Link is not present for account 'QA Testing - Brand'.");
                    Results.WriteStatus(test, "Pass", "'Branded Content' Link is present in Reports List Breadcrumb for account 'QA Testing - Brand'.");
                }
                else
                {
                    Assert.IsFalse(avail, "'Branded Content' Link is present for account other than 'QA Testing - Brand'.");
                    Results.WriteStatus(test, "Pass", "'Branded Content' Link is not present in Reports List Breadcrumb for account other than 'QA Testing - Brand'.");
                }
            }

            if (option != "")
            {
                if (allAnalytics)
                {
                    if (option.ToLower().Equals("random"))
                    {
                        Random rand = new Random();
                        int x = rand.Next(1, listItemCollection.Count);
                        select = listItemCollection[x];
                    }
                    else
                    {
                        foreach (IWebElement item in listItemCollection)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", item);
                            if (item.Text.ToLower().Equals(option.ToLower()))
                            {
                                select = item;
                                break;
                            }
                        }
                    }
                }

                Assert.AreNotEqual(null, select, "Option '" + option + "' not found to select.");
                option = select.Text;
                select.Click();

                if(!(option.ToLower().Equals("user") || option.ToLower().Equals("my exports") || option.ToLower().Equals("branded content") || option.ToLower().Equals("help center")))
                {
                    homePage.newVerifyHomePage();
                    Assert.IsTrue(driver._waitForElement("xpath", xPath), "'Reports List' Breadcrumb not present on Home Page.");
                    Assert.AreEqual(option, driver._getText("xpath", xPath), "Selected Report Name not displayed as Breadcrumb name.");
                    driver._click("xpath", xPath);
                    Assert.IsTrue(driver._waitForElement("xpath", "//li[contains(@class, 'breadcrumb-item') and contains(@class, 'open')]//div[@class='NU-scrolling-area']/a"), "Reports List from Breadcrumb not present.");
                    listItemCollection = driver._findElements("xpath", "//li[contains(@class, 'breadcrumb-item') and contains(@class, 'open')]//div[@class='NU-scrolling-area']/a");

                    avail = false;
                    foreach (IWebElement item in listItemCollection)
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", item);
                        if (item.Text.ToLower().Equals(option.ToLower()))
                        {
                            avail = true;
                            Assert.IsTrue(item.GetCssValue("background-color").Equals("rgba(224, 233, 234, 1)"), "Option '" + option + "' is not highlighted.");
                            break;
                        }
                    }
                    Assert.IsTrue(avail, "'" + option + "' not present in Reports List.");

                    driver._click("xpath", xPath);
                    Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//li[contains(@class, 'breadcrumb-item') and contains(@class, 'open')]//div[@class='NU-scrolling-area']/a"), "Reports List from Breadcrumb still present.");
                }
            }

            Results.WriteStatus(test, "Pass", "Verified, Reports List/All Analytics Breadcrumb.");
            return new AllAnalytics_BreadCrumbs(driver, test);
        }

        ///<summary>
        ///Verify Chart Selected From All Analytics Breadcrumb
        ///</summary>
        ///<returns></returns>
        public AllAnalytics_BreadCrumbs VerifyChartSelectedFromAllAnalyticsBreadcrumb()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//ol[contains(@class, 'breadcrumbs')]//button[text()]"), "'Reports List' Breadcrumb not present on Home Page.");
            string chartName = driver._getText("xpath", "//ol[contains(@class, 'breadcrumbs')]//button[text()]");

            Assert.IsTrue(driver._waitForElement("xpath", "//chart/div[@id='selected-chart']"), "Selected Chart '" + chartName + "' not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//chart/div[@id='selected-chart']//*[name()='text' and @class='highcharts-title']/*[name()='tspan']"), "Selected Chart '" + chartName + "' chart title not present.");

            if (chartName.ToLower().Contains("custom"))
                chartName = "Count of " + chartName.Substring(7);
            if (chartName.IndexOf("bc") > -1)
                chartName = chartName.Remove(chartName.IndexOf("bc"), 2);
            if (chartName.ToLower().Contains("creative") && !chartName.ToLower().Contains("creatives"))
                chartName = chartName.Insert(chartName.ToLower().IndexOf("creative") + 8, "s");
            if (chartName.ToLower().Contains("cat"))
                chartName = chartName.Substring(0, chartName.ToLower().IndexOf("cat")) + "Class";
            if (chartName.ToLower().Contains("comp"))
                chartName = chartName.Insert(chartName.ToLower().IndexOf("creative") -1, " ") + "any";

            Assert.AreEqual(chartName.ToLower(), driver._getText("xpath", "//chart/div[@id='selected-chart']//*[name()='text' and @class='highcharts-title']/*[name()='tspan']").ToLower(), "Displayed chart is not selected chart '" + chartName + "'.");

            Assert.IsTrue(driver._waitForElement("xpath", "//cft-table//ag-grid-angular//div[@role='grid']"), "Selected Chart '" + chartName + "' AgGrid not present.");

            Assert.IsTrue(driver._waitForElement("xpath", "//div[contains(@class, 'NU-card')]"), "Selected Chart '" + chartName + "' Master Detail Item not present.");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[contains(@class, 'NU-card')]//span"), "Selected Chart '" + chartName + "' Master Detail Item Header not present.");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[contains(@class, 'NU-card')]//img"), "Selected Chart '" + chartName + "' Master Detail Item Image not present.");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[contains(@class, 'NU-card')]//table"), "Selected Chart '" + chartName + "' Master Detail Item's details not present.");

            Results.WriteStatus(test, "Pass", "Verified, Chart '" + chartName + "' Selected From All Analytics Breadcrumb.");
            return new AllAnalytics_BreadCrumbs(driver, test);
        }





    }
}
