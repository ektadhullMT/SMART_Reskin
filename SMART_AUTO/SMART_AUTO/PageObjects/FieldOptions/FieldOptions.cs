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
    public class FieldOptions
    {
        #region Private Variables

        private IWebDriver fieldOptions;
        private ExtentTest test;

        #endregion

        public FieldOptions(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.fieldOptions = driver;
            test = testReturn;
        }

        public IWebDriver driver
        {
            get { return this.fieldOptions; }
            set { this.fieldOptions = value; }
        }

        /// <summary>
        /// Verify Field Options Popup
        /// </summary>
        /// <returns></returns>
        public FieldOptions VerifyFieldOptions(bool popupVisible = true)
        {
            if (popupVisible)
            {
                driver._click("xpath", "//button[@tooltip='Field Options']");

                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-content']"), "Field Options popup not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-content']//h4"), "Field options popup header is not present.");
                Assert.AreEqual("Customize Your Visible Fields...", driver._getText("xpath", "//div[@class='modal-content']//h4"), "Field Options popup header text does not match.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']/div"), "' Visible Fields ' section in Field options popup is not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[contains(@class, 'text')]"), "' Visible Fields ' section header in Field options popup is not present.");
                Assert.AreEqual("Visible Fields", driver._getText("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[contains(@class, 'text')]"), "' Visible Fields ' section header text does not match.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div"), "' Visible Fields ' section items in Field options popup is not present.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']/div"), "' Hidden Fields ' section in Field options popup is not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[contains(@class, 'text')]"), "' Hidden Fields ' section header in Field options popup is not present.");
                Assert.AreEqual("Hidden Fields", driver._getText("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[contains(@class, 'text')]"), "' Hidden Fields ' section header text does not match.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div"), "' Hidden Fields ' section items in Field options popup is not present.");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button[text()='Cancel ']"), "'Cancel' button is not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button[text()='Apply ']"), "'Apply' button is not present.");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button[text()='Reset ']"), "'Reset' button is not present.");

                Results.WriteStatus(test, "Pass", "Verified, Field Options Popup.");
            }
            else
            {
                Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[@class='modal-content']"), "Field Options popup not closed");
                Results.WriteStatus(test, "Pass", "Verified, Field Options Popup is closed.");
            }
            return new FieldOptions(driver, test);
        }

        /// <summary>
        /// Move Items From Hidden To Visible
        /// </summary>
        /// <param name="num">Number of Items to be added</param>
        /// <returns></returns>
        public string[] moveItemsFromHiddenToVisible(int num = 1)
        {
            string[] movedItems = new string[num];
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/button"), "Add button not present.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/div[text()]"), "Hidden Items' names not present");
            IList<IWebElement> hiddenItemNamesCollections = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/div[text()]");
            IList<IWebElement> addButtonCollection = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/button");

            Random rand = new Random();
            int x = rand.Next(0, addButtonCollection.Count);
            for (int i = 0; i < num; i++)
            {
                hiddenItemNamesCollections = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/div[text()]");
                addButtonCollection = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/button");
                x = rand.Next(x, addButtonCollection.Count);
                movedItems[i] = hiddenItemNamesCollections[x].Text;
                addButtonCollection[x].Click();
            }

            Results.WriteStatus(test, "Pass", "Moved, " + num + " hidden items to visible items.");
            return movedItems;
        }

        /// <summary>
        /// Verify Added Items in Visible Items
        /// </summary>
        /// <param name="num">List of added Items</param>
        /// <param name="inOrder">To Verify order</param>
        /// <returns></returns>
        public FieldOptions VerifyAddedItemsInVisibleItems(string[] movedItems, bool inOrder = false)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/div[text()]"), "Visible Items' names not present");
            IList<IWebElement> visibleItemNamesCollections = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/div[text()]");

            if (inOrder)
            {
                bool avail = true;
                for (int i = 0; i < movedItems.Length; i++)
                    if (!visibleItemNamesCollections[i].Text.ToLower().Contains(movedItems[i].ToLower()))
                    {
                        avail = false;
                        break;
                    }
                Assert.IsTrue(avail, "Visible Items are not in expected order.");
            }
            else
            {
                foreach (string item in movedItems)
                {
                    bool avail = false;
                    foreach (IWebElement visibleItem in visibleItemNamesCollections)
                        if (visibleItem.Text.ToLower().Contains(item.ToLower()))
                        {
                            avail = true;
                            break;
                        }
                    Assert.IsTrue(avail, "'" + item + "' not found");
                }
            }

            Results.WriteStatus(test, "Pass", "Verified, items moved in visible items.");
            return new FieldOptions(driver, test);
        }

        /// <summary>
        /// Move Items From Visible To Hidden
        /// </summary>
        /// <param name="num">Number of Items to be added</param>
        /// <returns></returns>
        public string[] moveItemsFromVisibleToHidden(int num = 1)
        {
            string[] movedItems = new string[num];
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/button"), "Remove button not present.");
            IList<IWebElement> removeButtonCollection = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/button");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/div[text()]"), "Visible Items' names not present");
            IList<IWebElement> visibleItemNamesCollections = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/div[text()]");

            Random rand = new Random();
            int x = rand.Next(0, removeButtonCollection.Count);
            for (int i = 0; i < num; i++)
            {
                removeButtonCollection = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/button");
                visibleItemNamesCollections = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/div[text()]");
                x = rand.Next(x, removeButtonCollection.Count);
                movedItems[i] = visibleItemNamesCollections[x].Text;
                removeButtonCollection[x].Click();
                Thread.Sleep(1000);
            }

            Results.WriteStatus(test, "Pass", "Moved, " + num + " visible items to hidden items.");
            return movedItems;
        }

        /// <summary>
        /// Verify Removed Items in Hidden Items
        /// </summary>
        /// <param name="num">List of added Items</param>
        /// <param name="inOrder">To Verify order</param>
        /// <returns></returns>
        public FieldOptions VerifyRemovedItemsInHiddenItems(string[] movedItems, bool inOrder = false)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/div[text()]"), "Hidden Items' names not present");
            IList<IWebElement> hiddenItemNamesCollections = driver._findElements("xpath", "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/div[text()]");

            if (inOrder)
            {
                bool avail = true;
                for(int i = 0; i < movedItems.Length; i++)
                    if (!hiddenItemNamesCollections[i].Text.ToLower().Contains(movedItems[i].ToLower()))
                    {
                        avail = false;
                        break;
                    }
                Assert.IsTrue(avail, "Hidden Items are not in expected order.");
            }
            else
            {
                foreach (string item in movedItems)
                {
                    bool avail = false;
                    foreach (IWebElement hiddenItem in hiddenItemNamesCollections)
                        if (hiddenItem.Text.ToLower().Contains(item.ToLower()))
                        {
                            avail = true;
                            break;
                        }
                    Assert.IsTrue(avail, "'" + item + "' not found");
                }
            }

            Results.WriteStatus(test, "Pass", "Verified, new items moved to hidden items.");
            return new FieldOptions(driver, test);
        }

        /// <summary>
        /// Verify Drag and Drop in Hidden and Visible Items Fields
        /// </summary>
        /// <param name="direction">To move across fields or change order</param>
        /// <param name="field">Source field</param>
        /// <param name="num">No. of elements to be moved</param>
        /// <returns></returns>
        public string[] VerifyDragAndDropInHiddenAndVisibleItemFields(string field, string direction, int num = 1)
        {
            string sourceFieldXpath = "", destFieldXpath = "", destField = "";
            if (field.ToLower().Equals("hidden"))
            {
                sourceFieldXpath = "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/div[text()]";
                destFieldXpath = "//div[@class='modal-body pt-3']//div[@class='col pr-1']/div";
                destField = "Visible";
            }
            else
            {
                sourceFieldXpath = "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/div[text()]";
                destFieldXpath = "//div[@class='modal-body pt-3']//div[@class='col pl-1']/div";
                destField = "Hidden";
            }

            Assert.IsTrue(driver._isElementPresent("xpath", sourceFieldXpath), "'" + field + "' items not present");
            IList<IWebElement> sourceFieldItemsCollection = driver._findElements("xpath", sourceFieldXpath);
            Assert.IsTrue(driver._isElementPresent("xpath", destFieldXpath), "'" + destField + "' items not present");
            IWebElement destFieldItems = driver._findElement("xpath", destFieldXpath);

            int length = 0;
            if (!direction.ToLower().Equals("across"))
                length = sourceFieldItemsCollection.Count;
            else
                length = num;
            string[] movedItems = new string[length];


            if (direction.ToLower().Equals("across"))
            {
                Actions action = new Actions(driver);
                Random rand = new Random();
                int x = rand.Next(0, sourceFieldItemsCollection.Count);
                for(int i = 0; i < num; i++)
                {
                    movedItems[i] = sourceFieldItemsCollection[x].Text;
                    driver._waitForElementToBePopulated("xpath", destFieldXpath);
                    action.DragAndDrop(sourceFieldItemsCollection[x], destFieldItems).Build().Perform();
                    sourceFieldItemsCollection = driver._findElements("xpath", sourceFieldXpath);
                    x = rand.Next(x, sourceFieldItemsCollection.Count);
                }
                Results.WriteStatus(test, "Pass", "Moved, " + num + " '" + field + "' items to '" + destField + "' field.");
            }
            else
            {
                Actions action = new Actions(driver);
                Random rand = new Random();
                int x = rand.Next(0, sourceFieldItemsCollection.Count);
                for(int i = 0; i < num; i++)
                {
                    int targetIndex = x + 1;
                    if (x == sourceFieldItemsCollection.Count - 1)
                        targetIndex = x - 1;
                    action.DragAndDrop(sourceFieldItemsCollection[i], sourceFieldItemsCollection[targetIndex]).Build().Perform();
                    x = rand.Next(targetIndex, sourceFieldItemsCollection.Count);
                }
                Thread.Sleep(1000);
                sourceFieldItemsCollection = driver._findElements("xpath", sourceFieldXpath);
                for (int i = 0; i < length; i++)
                    movedItems[i] = sourceFieldItemsCollection[i].Text;

                Results.WriteStatus(test, "Pass", "Moved, '" + field + "' items to change the order");
            }

            Results.WriteStatus(test, "Pass", "Verified, Drag and Drop in Hidden and Visible Items Fields");
            return movedItems;
        }

        /// <summary>
        /// Click Button on Field Options Popup
        /// </summary>
        /// <param name="button">Button to be clicked</param>
        /// <returns></returns>
        public FieldOptions clickButtonOnFieldOptionsPopup(string button)
        {
            string buttonXPath = "";
            switch (button.ToLower())
            {
                case "cancel":
                    buttonXPath = "//div[@class='modal-footer pt-0']//button[text()='Cancel ']";
                    driver._waitForElementToBeHidden("xpath", "//div[@class='modal-content']");
                    break;
                case "reset":
                    buttonXPath = "//div[@class='modal-footer pt-0']//button[text()='Reset ']";
                    break;
                case "apply":
                    buttonXPath = "//div[@class='modal-footer pt-0']//button[text()='Apply ']";
                    driver._waitForElementToBeHidden("xpath", "//div[@class='modal-content']");
                    break;
                default:
                    break;
            }

            driver._click("xpath", buttonXPath);
            Thread.Sleep(2000);
            Results.WriteStatus(test, "Pass", "Clicked, '" + button + "' button on Field Options Popup.");
            return new FieldOptions(driver, test);
        }

        /// <summary>
        /// Get Items From Field in Field Options Popup
        /// </summary>
        /// <param name="field">Hidden or Visible</param>
        /// <returns></returns>
        public string[] getItemsFromFieldInFieldOptionsPopup(string field)
        {
            string fieldXpath = "";
            if (field.ToLower().Equals("hidden"))
                fieldXpath = "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/div[text()]";
            else
                fieldXpath = "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/div[text()]";
            
            Assert.IsTrue(driver._isElementPresent("xpath", fieldXpath), "'" + field + "' items not present");
            IList<IWebElement> fieldItemsCollection = driver._findElements("xpath", fieldXpath);
            string[] itemsList = new string[fieldItemsCollection.Count];

            for (int i = 0; i < itemsList.Length; i++)
                itemsList[i] = fieldItemsCollection[i].Text.ToLower();

            Results.WriteStatus(test, "Pass", "Captured, Items From '" + field + "' Field in Field Options Popup");
            return itemsList;
        }

        /// <summary>
        /// Compare List of Items in Order
        /// </summary>
        /// <param name="Equal">Should be equal or not</param>
        /// <param name="list1">First List</param>
        /// <param name="list2">Second List</param>
        /// <returns></returns>
        public FieldOptions compareListOfItemsInOrder(string[] list1, string[] list2, bool Equal = true)
        {
            bool compare = list1.SequenceEqual(list2);
            Assert.AreEqual(Equal, compare, "Given Lists' Comparison Failed");

            Results.WriteStatus(test, "Pass", "Compared, Lists of Items on Field Options Popup in Order.");
            return new FieldOptions(driver, test);
        }

        ///<summary>
        ///Capture Column Names of AgGrid in Order
        ///</summary>
        ///<returns></returns>
        public string[] captureColumnNamesOfAgGridInOrder()
        {
            if (driver._isElementPresent("xpath", "//img[@class='sidebar-logo img-fluid']"))
                driver._click("xpath", "//cft-sidebar-navigation//button[contains(@class, 'sidebar-toggle')]");

            Assert.IsTrue(driver._isElementPresent("xpath", "//ag-grid-angular"), "AgGrid not present.");
            driver._scrollintoViewElement("xpath", "//ag-grid-angular");

            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']"), "AgGrid Header Row not present");
            ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('ag-body-viewport')[0].scrollLeft-=500", "");
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']//div[@ref='eLabel']"), "AgGrid Column Headers not present");

            string[] columnNames = new string[1];
            int i = 1;
            while(driver._isElementPresent("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']//div[@col-id][" + i + "]//div[@ref='eLabel']"))
            {
                columnNames[i-1] = driver._getText("xpath", "//div[@class='ag-header-container']//div[@class='ag-header-row']//div[@col-id][" + i + "]//div[@ref='eLabel']").ToLower();
                if (columnNames[i-1].Contains("$000s"))
                    columnNames[i-1] = "spend";
                if (columnNames[i-1].Contains("media type"))
                    columnNames[i-1] = "media";
                Array.Resize(ref columnNames, columnNames.Length + 1);
                if(i > 5)
                    ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementsByClassName('ag-body-viewport')[0].scrollLeft+=20", "");
                ++i;
            }
            Array.Resize(ref columnNames, columnNames.Length - 1);

            Results.WriteStatus(test, "Pass", "Captured, Column Names of AgGrid in Order.");
            return columnNames;
        }

        ///<summary>
        ///Add Or Remove Specific Field In Field Options
        ///</summary>
        ///<param name="fieldOption">field to be added or removed</param>
        ///<param name="add">To add or remove</param>
        ///<returns></returns>
        public FieldOptions addOrRemoveSpecificFieldInFieldOptions(string fieldOption, bool add = true)
        {
            string buttonXpath = "";
            string oldFieldXpath = "";
            string newFieldXpath = "";

            if (add)
            {
                buttonXpath = "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/button";
                oldFieldXpath = "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/div[text()]";
                newFieldXpath = "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/div[text()]";
            }
            else
            {
                buttonXpath = "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/button";
                oldFieldXpath = "//div[@class='modal-body pt-3']//div[@class='col pr-1']//div[not(@class)]/div/div[text()]";
                newFieldXpath = "//div[@class='modal-body pt-3']//div[@class='col pl-1']//div[not(@class)]/div/div[text()]";
            }

            Assert.IsTrue(driver._isElementPresent("xpath", oldFieldXpath), "Source Field Options not present");
            Assert.IsTrue(driver._isElementPresent("xpath", buttonXpath), "Buttons in Source Field Options not present");
            IList<IWebElement> buttonCollection = driver._findElements("xpath", buttonXpath);
            IList<IWebElement> sourceFieldCollection = driver._findElements("xpath", oldFieldXpath);
            bool avail = false;
            for(int i = 0; i < buttonCollection.Count; i++)
                if (sourceFieldCollection[i].Text.ToLower().Contains(fieldOption.ToLower()))
                {
                    avail = true;
                    buttonCollection[i].Click();
                    break;
                }
            Assert.IsTrue(avail, "'" + fieldOption + "' not found.");

            driver._wait(1);
            Assert.IsTrue(driver._isElementPresent("xpath", newFieldXpath), "Buttons in Source Field Options not present");
            IList<IWebElement> newFieldCollection = driver._findElements("xpath", newFieldXpath);
            avail = false;
            for (int i = 0; i < newFieldCollection.Count; i++)
                if (newFieldCollection[i].Text.ToLower().Contains(fieldOption.ToLower()))
                {
                    avail = true;
                    break;
                }
            Assert.IsTrue(avail, "'" + fieldOption + "' not added/removed.");

            Results.WriteStatus(test, "Pass", "Added Or Removed, '" + fieldOption + "' Field In Field Options.");
            return new FieldOptions(driver, test);
        }

        ///<summary>
        ///Verify Reset button as disabled/enabled
        ///</summary>
        ///<param name="enabled">should be disabled or enabled</param>
        ///<returns></returns>
        public FieldOptions VerifyResetButtonAsDisabled_Enabled(bool enabled = true)
        {
            if (enabled)
            {
                Assert.AreEqual(null, driver._getAttributeValue("xpath", "//div[@class='modal-footer pt-0']//button[text()='Reset ']", "disabled"), "'Reset' button is disabled.");
                Results.WriteStatus(test, "Pass", "Verified, Reset button is enabled");
            }
            else
            {
                Assert.AreEqual("true", driver._getAttributeValue("xpath", "//div[@class='modal-footer pt-0']//button[text()='Reset ']", "disabled"), "'Reset' button is not disabled.");
                Results.WriteStatus(test, "Pass", "Verified, Reset button is disabled");
            }

            return new FieldOptions(driver, test);
        }

        ///<summary>
        ///Verify Field Options And Export Icons
        ///</summary>
        ///<returns></returns>
        public FieldOptions VerifyFieldOptionsAndExportIcons(bool fieldOptionPresent, bool exportPresent)
        {
            if (fieldOptionPresent)
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//button[@tooltip='Field Options']"), "Field Options Button not present");
                Results.WriteStatus(test, "Pass", "Field Options Icon is present");
            }
            else
            {
                Assert.IsFalse(driver._isElementPresent("xpath", "//button[@tooltip='Field Options']"), "Field Options Button is present");
                Results.WriteStatus(test, "Pass", "Field Options Icon not is present");
            }

            if (exportPresent)
            {
                Assert.IsTrue(driver._isElementPresent("xpath", "//button[@tooltip='Export Results']"), "Export Button not present");
                Results.WriteStatus(test, "Pass", "Export Icon is present");
            }
            else
            {
                Assert.IsFalse(driver._isElementPresent("xpath", "//button[@tooltip='Field Options']"), "Export Button is present");
                Results.WriteStatus(test, "Pass", "Export Icon not is present");
            }

            return new FieldOptions(driver, test);
        }

    }
}
