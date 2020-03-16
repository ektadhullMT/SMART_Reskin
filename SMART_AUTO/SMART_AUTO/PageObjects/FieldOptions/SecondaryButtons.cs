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
    public class SecondaryButtons
    {
        #region Private Variables

        private IWebDriver secondaryButtons;
        private ExtentTest test;
        Home homePage;

        #endregion

        #region Public Methods

        public SecondaryButtons(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.secondaryButtons = driver;
            test = testReturn;
            homePage = new Home(driver, test);
        }

        public IWebDriver driver
        {
            get { return this.secondaryButtons; }
            set { this.secondaryButtons = value; }
        }

        ///<summary>
        ///Verify Secondary Buttons
        ///</summary>
        ///<param name="searchModified">Whether Selected Saved Search is Modified or not</param>
        ///<returns></returns>
        public SecondaryButtons VerifySecondaryButtons(bool searchModified = false, bool VerifyDefaultSearch = false)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-saved-search-dropdown//button[@dropdowntoggle]"), "'Load Search' Button is not present.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-saved-search-dropdown//button[@tooltip='Reset this search']"), "Reset Icon is not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-saved-search-save-modal//button"), "Save Search Icon is not present");

            if (VerifyDefaultSearch)
            {
                Assert.IsFalse(driver._getText("xpath", "//cft-saved-search-dropdown//button[@dropdowntoggle]").ToLower().Contains("edit search"), "Default Search is not from Saved Search List");
                Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-saved-search-dropdown//button[@tooltip='Make this my default search']/i", "class").Contains("yellow"), "Loaded Search is not default.");
            }

            if (!driver._getText("xpath", "//cft-saved-search-dropdown//button[@dropdowntoggle]").ToLower().Contains("edit search"))
            {
                Assert.AreEqual(null, driver._getAttributeValue("xpath", "//cft-saved-search-save-modal//button", "disabled"), "Save Search Button is disabled even after Saved Search is applied");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-saved-search-dropdown//cft-scheduled-export-modal//button"), "Schedule Alert Icon is not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-saved-search-dropdown//button[@tooltip='Delete this search']"), "Delete Icon is not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-saved-search-dropdown//button[@tooltip='Make this my default search']"), "Make Default Icon is not present");
            }

            if (searchModified)
            {
                Assert.AreEqual(null, driver._getAttributeValue("xpath", "//cft-saved-search-dropdown//button[@tooltip='Reset this search']", "disabled"), "Reset Button is disabled even after changes are made to current Search");
                Assert.AreEqual(null, driver._getAttributeValue("xpath", "//cft-saved-search-save-modal//button", "disabled"), "Save Search Button is disabled even after changes are made to current Search");
            }
            else
            {
                Assert.AreNotEqual(null, driver._getAttributeValue("xpath", "//cft-saved-search-dropdown//button[@tooltip='Reset this search']", "disabled"), "Reset Button is not disabled when no changes are made to current Search");
                if (driver._getText("xpath", "//cft-saved-search-dropdown//button[@dropdowntoggle]").ToLower().Contains("edit search"))
                    Assert.AreNotEqual(null, driver._getAttributeValue("xpath", "//cft-saved-search-save-modal//button", "disabled"), "Save Search Button is not disabled when no Saved Search or change is applied");
            }

            Results.WriteStatus(test, "Pass", "Verified, Secondary Buttons");
            return new SecondaryButtons(driver, test);
        }

        ///<summary>
        ///Click On Secondary Button
        ///</summary>
        ///<param name="button">Button to be clicked</param>
        ///<returns></returns>
        public SecondaryButtons clickOnSecondaryButtons(string button)
        {
            if (button.ToLower().Contains("schedule") || button.ToLower().Contains("alert"))
                driver._click("xpath", "//cft-saved-search-dropdown//cft-scheduled-export-modal//button");
            else if (button.ToLower().Contains("delete"))
                driver._click("xpath", "//cft-saved-search-dropdown//button[@tooltip='Delete this search']");
            else if (button.ToLower().Contains("default"))
                driver._click("xpath", "//cft-saved-search-dropdown//button[@tooltip='Make this my default search']");
            else if (button.ToLower().Contains("save"))
                driver._click("xpath", "//cft-saved-search-save-modal//button");
            else if (button.ToLower().Contains("reset"))
                driver._click("xpath", "//cft-saved-search-dropdown//button[@tooltip='Reset this search']");
            else
                Results.WriteStatus(test, "Info", "No Secondary Button was clicked");

            Thread.Sleep(4000);

            Results.WriteStatus(test, "Pass", "Clicked, '" + button + "' button from Secondary Buttons");
            return new SecondaryButtons(driver, test);
        }

        /// <summary>
        /// Verify Save New Search Popup and click button
        /// </summary>
        /// <param name="button">Button to be clicked</param>
        /// <returns></returns>
        public SecondaryButtons VerifySaveNewSearchPopupAndClickButton(string button = "")
        {

            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-content']"), "Save Search popup is not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-content']//h4"), "Save Search popup header is not present.");
            Assert.AreEqual("Save Search As ...", driver._getText("xpath", "//div[@class='modal-content']//h4"), "Save Search popup header text does not match.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body']//input[@type = 'text']"), "Search Name field is not present.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body']//label"), "'Make it my default search' checkbox and text is not present.");
            Assert.AreEqual("Make it my default search", driver._getText("xpath", "//div[@class='modal-body']//label"), "'Make it my default search' text does not match.");

            string screenName = homePage.getActiveScreenNameFromSideNavigationBar();
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-content']//p"), "'This search will only be applied to your Dashboard report.' text is not present.");
            Assert.AreEqual(true, driver._getText("xpath", "//div[@class='modal-content']//p").Contains("This search will only be applied to your " + screenName + " report." ), "'This search will only be applied to your Dashboard report.' text does not match.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Cancel ']"), "'Cancel' button is not present.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Save ']"), "'Save' button is not present.");

            if (button.ToLower().Contains("save"))
            {
                if(driver._getValue("xpath", "//div[@class='modal-body']//input[@type = 'text']") != "")
                {
                    driver._click("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Save ']");
                    Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[@class='modal-content']"), "Save Search popup is still present.");
                    Results.WriteStatus(test, "Pass", "Clicked, 'Save' button on Save New Search Popup.");
                }
                else
                {
                    Assert.AreNotEqual(null, driver._getAttributeValue("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Save ']", "disabled"), "Save button is not disabled when Search Name is not entered.");
                    Results.WriteStatus(test, "Info", "Save Button was not clicked since Search Name text box is empty and Save button is disabled.");
                }
            }
            else if (button.ToLower().Contains("cancel"))
            {
                driver._click("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Cancel ']");
                Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[@class='modal-content']"), "Save Search popup is still present.");
                Results.WriteStatus(test, "Pass", "Clicked, 'Cancel' button on Save New Search Popup.");
            }

            Results.WriteStatus(test, "Pass", "Verified, Save New Search Popup.");
            return new SecondaryButtons(driver, test);
        }

        ///<summary>
        ///Verify Tooltips of Secondary Buttons
        ///</summary>
        ///<returns></returns>
        public SecondaryButtons VerifyTooltipsOfSecondaryButtons()
        {
            Actions action = new Actions(driver);

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-saved-search-dropdown//button[contains(@class, 'rounded-circle text-numerator-deep-teal')]"), "'Secondary Buttons' not present.");
            IList<IWebElement> secondaryButtonColl = driver._findElements("xpath", "//cft-saved-search-dropdown//button[contains(@class, 'rounded-circle text-numerator-deep-teal')]");

            action.MoveToElement(secondaryButtonColl[0]).Perform();
            Thread.Sleep(1000);
            if(secondaryButtonColl[0].GetAttribute("disabled") == null)
                Assert.IsTrue(driver._isElementPresent("xpath", "//bs-tooltip-container[contains(@id, 'tooltip')]//div[text()='Create an alert from this saved search']"), "Tooltip with text 'Create an alert from this saved search' not present for Schedule Icon on Upper Left Corner.");
            else
                Assert.IsTrue(driver._isElementPresent("xpath", "//bs-tooltip-container[contains(@id, 'tooltip')]//div[text()='Please reset your saved search before you can create an alert']"), "Tooltip with text 'Please reset your saved search before you can create an alert' not present for Schedule Icon on Upper Left Corner.");

            action.MoveToElement(secondaryButtonColl[1]).Perform();
            Thread.Sleep(1000);
            Assert.IsTrue(driver._isElementPresent("xpath", "//bs-tooltip-container[contains(@id, 'tooltip')]//div[text()='Delete this search']"), "Tooltip with text 'Delete this search' not present for Delete Icon on Upper Left Corner.");

            action.MoveToElement(secondaryButtonColl[2]).Perform();
            Thread.Sleep(1000);
            Assert.IsTrue(driver._isElementPresent("xpath", "//bs-tooltip-container[contains(@id, 'tooltip')]//div[text()='Make this my default search']"), "Tooltip with text 'Make this my default search' not present for Make Default Icon on Upper Left Corner.");

            action.MoveToElement(secondaryButtonColl[3]).Perform();
            Thread.Sleep(1000);
            Assert.IsTrue(driver._isElementPresent("xpath", "//bs-tooltip-container[contains(@id, 'tooltip')]//div[text()='Save a copy of this search']"), "Tooltip with text 'Save a copy of this search' not present for Save Icon on Upper Left Corner.");

            action.MoveToElement(secondaryButtonColl[4]).Perform();
            Thread.Sleep(1000);
            if (secondaryButtonColl[4].GetAttribute("disabled") == null)
                Assert.IsTrue(driver._isElementPresent("xpath", "//bs-tooltip-container[contains(@id, 'tooltip')]//div[text()='Reset this search']"), "Tooltip with text 'Reset this search' not present for Reset Icon on Upper Left Corner.");

            Results.WriteStatus(test, "Pass", "Verified, Tooltips of Secondary Buttons.");
            return new SecondaryButtons(driver, test);
        }

        #endregion
    }
}
