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
    public class Carousels
    {
        #region Private Variables

        private IWebDriver carousels;
        private ExtentTest test;
        Home homePage;

        #endregion

        public Carousels(IWebDriver driver, ExtentTest testReturn)
        {
            // TODO: Complete member initialization
            this.carousels = driver;
            test = testReturn;
            homePage = new Home(driver, test);
            //ViewAdPopup viewAdPopup = new ViewAdPopup(driver, test);
        }

        public IWebDriver driver
        {
            get { return this.carousels; }
            set { this.carousels = value; }
        }

        ///<summary>
        ///Verify Carousels
        ///</summary>
        ///<returns></returns>
        public Carousels VerifyCarousels(bool adCodeFilter = false)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//div[@class='item active']"), "'Carousel' not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div"), "'Carousel Cards' not present on Carousel");
            IList<IWebElement> carouselsCollection = driver._findElements("xpath", "//cft-domain-item-carousel//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div");

            Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short//input"), "'Checkbox' not present on Carousel Cards");
            Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short//label/span"), "'Advertiser's Name' not present on Carousel Cards");
            Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short//img"), "'Thumbnail Image' not present on Carousel Cards");
            Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div[contains(@class, 'NU-card-tags')]/button"), "'Media Type' not present on Carousel Cards");

            Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div[@class='row']//div[contains(@class, 'card-body')]//button"), "'Card Buttons' not present on Carousel Cards");
            IList<IWebElement> cardButtonCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//div[@class='row']//div[contains(@class, 'card-body')]//button");
            string[] cardButtonNames = { "View Ad", "Markets", "Details" };
            if (adCodeFilter)
            {
                string[] newCardButtonNames = { "View Ad", "Occurrences", "Details"};
                Array.Copy(newCardButtonNames, cardButtonNames, newCardButtonNames.Length);
            }
            foreach (string cardButtonName in cardButtonNames)
            {
                bool avail = false;
                foreach (IWebElement cardButton in cardButtonCollection)
                    if (cardButton.Text.ToLower().Contains(cardButtonName.ToLower()))
                    {
                        avail = true;
                        break;
                    }
                Assert.IsTrue(avail, "'" + cardButtonName + "' not found on carousel");
            }

            Results.WriteStatus(test, "Pass", "Verified, Carousels");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Verify Mouse Hover On Carousels
        ///</summary>
        ///<returns></returns>
        public Carousels VerifyMouseHoverOnCarousels()
        {
            IList<IWebElement> carouselsCollection = driver._findElements("xpath", "//cft-domain-item-carousel//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div");
            Actions action = new Actions(driver);
            action.MoveToElement(carouselsCollection[0]).Perform();

            IList<IWebElement> advertiserNameCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short//label");
            IList<IWebElement> mediaTypeCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//div[contains(@class, 'NU-card-tags')]/button");
            Assert.IsTrue(advertiserNameCollection[0].GetCssValue("text-decoration").Contains("underline"), "Advertiser's Name did not get highlighted on Mouse Hovering over Carousel Card.");
            Assert.IsTrue(mediaTypeCollection[0].GetCssValue("text-decoration").Contains("underline"), "'Media Type' did not get highlighted on Mouse Hovering over Carousel Card.");

            Results.WriteStatus(test, "Pass", "Verified, Mouse Hover On Carousels");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Get Image Source From Carousel and Click Slider
        ///</summary>
        ///<param name="arrow">To click arrow or slider</param>
        ///<param name="forward">To Navigate forward or backward</param>
        ///<returns></returns>
        public string[] GetImageSourceFromCarouselAndClickSlider(bool forward, bool arrow)
        {
            IList<IWebElement> imageCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short//img");
            string[] imgSourceList = new string[imageCollection.Count];

            for (int i = 0; i < imageCollection.Count; i++)
                imgSourceList[i] = imageCollection[i].GetAttribute("src");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//a[contains(@class, 'right')]"), "Forward Navigation Button not present on Carousel.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//a[contains(@class, 'left')]"), "Backward Navigation Button not present on Carousel.");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//ol/li"), "Sliders not present on Carousel.");

            if (arrow)
            {
                if (forward)
                    driver._click("xpath", "//cft-domain-item-carousel//a[contains(@class, 'right')]");
                else
                    driver._click("xpath", "//cft-domain-item-carousel//a[contains(@class, 'left')]");
            }
            else
            {
                IList<IWebElement> sliderCollection = driver._findElements("xpath", "//cft-domain-item-carousel//ol/li");
                for (int i = 0; i < sliderCollection.Count; i++)
                    if (sliderCollection[i].GetAttribute("class").Contains("active"))
                    {
                        if (forward)
                            sliderCollection[i + 1].Click();
                        else
                            sliderCollection[i - 1].Click();
                        break;
                    }
            }

            if (driver._isElementPresent("xpath", "//span[@class='lead' and contains(text(),'Almost there')]"))
                driver._waitForElementToBeHidden("xpath", "//span[@class='lead' and contains(text(),'Almost there')]");
            driver._waitForElementToBeHidden("xpath", "//p[@class='lead' and contains(text(),'Loading')]");
            int cnt = 0;
            IList<IWebElement> loadingCount = driver.FindElements(By.XPath("//p[@class='lead' and contains(text(),'Loading')]"));
            do
            {
                Thread.Sleep(1000);
                loadingCount = driver.FindElements(By.XPath("//p[@class='lead' and contains(text(),'Loading')]"));
                cnt++;
                if (cnt == 15)
                    break;

            } while (loadingCount.Count.Equals(0) == false);

            Results.WriteStatus(test, "Pass", "Captured, captured Image Source From Carousel and Clicked Slider");
            return imgSourceList;
        }

        ///<summary>
        ///Click Button On Carousel
        ///</summary>
        ///<param name="buttonName">Button to be clicked</param>
        ///<returns></returns>
        public Carousels clickButtonOnCarousel(string buttonName)
        {
            IList<IWebElement> cardCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div");
            int i = 1;

            while (cardCollection[i - 1].GetAttribute("class").Contains("NU-selected-card"))
                ++i;

            while (driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div[@class='row']/div[" + i + "]//cft-domain-item-thumbnail-short/div//p[text()='Image not available']"))
                ++i;

            IList<IWebElement> cardButtonCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//div[@class='row']/div[" + i + "]//div[contains(@class, 'card-body')]//button");
            bool avail = false;
            foreach (IWebElement cardButton in cardButtonCollection)
                if (cardButton.Text.ToLower().Contains(buttonName.ToLower()))
                {
                    avail = true;
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", cardButton);
                    Thread.Sleep(500);
                    cardButton.Click();
                    break;
                }
            Assert.IsTrue(avail, "'" + buttonName + "' button not found on carousel");

            Results.WriteStatus(test, "Pass", "Clicked, '" + buttonName + "' Button On Carousel");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Verify View Ad Functionality
        ///</summary>
        ///<param name="popupVisible">Popup should be visible or not</param>
        ///<returns></returns>
        public Carousels VerifyViewAdFunctionality(bool popupVisible = true, bool fromCarousel = true, bool occurrence = false)
        {
            if (popupVisible)
            {
                string tabs = "";

                if (fromCarousel)
                {
                    if (occurrence)
                        tabs = "View Ad,Occurrence,More Details";
                    else
                        tabs = "View Ad,Markets,More Details,Download";
                }
                else
                    tabs = "View Ad,More Details";

                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-content']"), "Creative Details Popup not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//h4"), "Creative Details Popup header not present");
                Assert.IsTrue(driver._getText("xpath", "//div[@class='modal-body pb-0']//h4").Contains("Creative Details for "), "Creative Details Popup header text does not match");
                if(!driver._waitForElement("xpath", "//creative-media[@view-type='full']//img"))
                {
                    if (driver._waitForElement("xpath", "//domain-item-view-creative//jwplayer-control/div[@aria-label='Video Player']"))
                    {
                        //if(!driver._getAttributeValue("xpath", "//domain-item-view-creative//jwplayer-control/div[@aria-label='Video Player']", "class").Contains("audio"))
                        //{
                        //    Assert.IsTrue(driver._getAttributeValue("xpath", "//domain-item-view-creative//jwplayer-control/div[@aria-label='Video Player']", "class").Contains("idle"), "Video Or Audio is either playing or has been played before");
                        //    driver._click("xpath", "//domain-item-view-creative//jwplayer-control/div[@aria-label='Video Player']");
                        //    while (driver._getAttributeValue("xpath", "//domain-item-view-creative//jwplayer-control/div[@aria-label='Video Player']", "class").Contains("buffering"))
                        //        Thread.Sleep(500);
                        //    Assert.IsTrue(driver._getAttributeValue("xpath", "//domain-item-view-creative//jwplayer-control/div[@aria-label='Video Player']", "class").Contains("playing"), "Video Or Audio is not playing or has been played before");
                        //    driver._click("xpath", "//domain-item-view-creative//jwplayer-control/div[@aria-label='Video Player']");
                        //    Thread.Sleep(500);
                        //    Assert.IsTrue(driver._getAttributeValue("xpath", "//domain-item-view-creative//jwplayer-control/div[@aria-label='Video Player']", "class").Contains("paused"), "Video Or Audio is either playing or has been played before");
                        //}
                    }
                }
                Assert.IsTrue(driver._isElementPresent("xpath", "//creative-media[@view-type='thumbnail']//img"), "Thumbnail image on the side is not present");

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

            Results.WriteStatus(test, "Pass", "Verified, View Ad Functionality");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Verify Markets Functionality
        ///</summary>
        ///<param name="popupVisible">Popup should be visible or not</param>
        ///<returns></returns>
        public Carousels VerifyMarketsFunctionality(bool popupVisible = true, bool fromCarousel = true)
        {
            if (popupVisible)
            {

                string tabs = "";

                if (fromCarousel)
                {
                    clickButtonOnCarousel("Markets");
                    tabs = "View Ad,Markets,More Details,Download";
                }
                else
                    tabs = "View Ad,More Details";

                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-content']"), "Creative Details Popup not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//h4"), "Creative Details Popup header not present");
                Assert.IsTrue(driver._getText("xpath", "//div[@class='modal-body pb-0']//h4").Contains("Creative Details for "), "Creative Details Popup header text does not match");

                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//ag-grid-angular//div[@role='grid']"), "Grid not present");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']//span[@ref='eText']"), "Grid Column headers not present");
                IList<IWebElement> columnHeaderCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//div[@class='ag-header-row']//span[@ref='eText']");
                string[] headerNames = { "DMA", "Media Outlet", "First Run Date", "Last Run Date", "Occurrences", "Spend" };
                foreach (string header in headerNames)
                {
                    bool avail = false;
                    foreach (IWebElement columnHeader in columnHeaderCollection)
                        if (columnHeader.Text.ToLower().Contains(header.ToLower()))
                        {
                            avail = true;
                            break;
                        }
                    Assert.IsTrue(avail, "'" + header + "' not found on Creative Details Popup");
                }

                Assert.IsTrue(driver._isElementPresent("xpath", "//button//span[@class='pl-1' and text()='Download Grid']"), "'Download Grid' Link not present on Creative Details Popup");
                Assert.IsTrue(driver._isElementPresent("xpath", "//button//span[@class='pl-1' and text()='Grid Options']"), "'Grid options' Link not present on Creative Details Popup");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-modal-footer//button[text()=' Download Asset ']"), "'Download Asset' button not present on Creative Details Popup");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-modal-footer//button[text()='Close']"), "'Close' button not present on Creative Details Popup");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//a[not(contains(@class, 'ng-hide'))]"), "'Tabs' not present on Creative Details Popup");
                IList<IWebElement> tabCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//a[not(contains(@class, 'ng-hide'))]");
                string[] tabNames = { "View Ad", "Markets", "More Details", "Download" };
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

            Results.WriteStatus(test, "Pass", "Verified, Markets Functionality");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Verify Details Functionality
        ///</summary>
        ///<param name="popupVisible">Popup should be visible or not</param>
        ///<returns></returns>
        public Carousels VerifyDetailsFunctionality(bool popupVisible = true, bool fromCarousel = true, bool occurrence = false)
        {
            if (popupVisible)
            {
                string tabs = "";

                if (fromCarousel)
                {
                    if (occurrence)
                        tabs = "View Ad,Occurrence,More Details";
                    else
                        tabs = "View Ad,Markets,More Details,Download";
                }
                else
                    tabs = "View Ad,More Details";

                while (driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//h4[text()='Loading Details...']"))
                    Thread.Sleep(2000);

                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-content']"), "Creative Details Popup not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body pb-0']//h4"), "Creative Details Popup header not present");
                Assert.IsTrue(driver._getText("xpath", "//div[@class='modal-body pb-0']//h4").Contains("Creative Details for "), "Creative Details Popup header text does not match");

                if (!driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//th"))
                    driver.Navigate().Refresh();
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//th"), "Row headers not present in Table");
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//td"), "Row Values not present in Table");

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

            Results.WriteStatus(test, "Pass", "Verified, Details Functionality");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Verify Select Ad Functionality
        ///</summary>
        ///<param name="checkbox">To select using checkbox or not</param>
        ///<param name="undo">To undo selection or not</param>
        ///<returns></returns>
        public Carousels VerifySelectAdFunctionality(bool checkbox, bool undo = false, bool uncheck = false)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div"), "Carousel cards not present.");
            IList<IWebElement> cardCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div");
            int i = 0;
            while (cardCollection[i].GetAttribute("class").Contains("NU-selected-card") && !uncheck)
                ++i;

            while (driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div[@class='row']/div[" + (i+1) + "]//cft-domain-item-thumbnail-short/div//p[text()='Image not available']"))
                ++i;

            if (checkbox)
            {
                IList<IWebElement> checkboxCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short//input");
                checkboxCollection[i].Click();
                Thread.Sleep(1000);
                cardCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div");
                Assert.IsTrue(cardCollection[i].GetAttribute("class").Contains("NU-selected-card"), "Ad Checkbox did not get checked");
                Results.WriteStatus(test, "Pass", "Selected, Ad by clicking on Checkbox");
            }
            else
            {
                IList<IWebElement> imageCollection = cardCollection[i]._findElementsWithinElement("xpath", ".//img");

                Actions action = new Actions(driver);
                action.MoveToElement(imageCollection[0]).Perform();
                action.Click().Perform();

                Thread.Sleep(1000);

                cardCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div");
                if (uncheck)
                {
                    Assert.IsFalse(cardCollection[i].GetAttribute("class").Contains("NU-selected-card"), "Ad Checkbox did not get Unchecked");
                    Results.WriteStatus(test, "Pass", "Deselected, Ad by clicking somewhere other than Checkbox");
                }
                else
                {
                    Assert.IsTrue(cardCollection[i].GetAttribute("class").Contains("NU-selected-card"), "Ad Checkbox did not get checked");
                    Results.WriteStatus(test, "Pass", "Selected, Ad by clicking somewhere other than Checkbox");
                }
            }

            if (undo)
            {
                IList<IWebElement> checkboxCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short//input");
                checkboxCollection[0].Click();
                Thread.Sleep(1000);
                cardCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div");
                Assert.IsFalse(cardCollection[i].GetAttribute("class").Contains("NU-selected-card"), "Ad Checkbox did not get Unchecked");
                Results.WriteStatus(test, "Pass", "Deselected, Ad by clicking on Checkbox");
            }

            Results.WriteStatus(test, "Pass", "Verified, Select Ad Functionality");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Get Ad Code From Carousel
        ///</summary>
        ///<returns></returns>
        public string getAdCodeFromCarousel(bool fromCarousel = true)
        {
            if (fromCarousel)
                clickButtonOnCarousel("Details");

            if (!driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//th", 10))
                driver.Navigate().Refresh();
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//h4"), "Creative Details Popup header not present");
            Thread.Sleep(4000);
            Assert.IsTrue(driver._getText("xpath", "//div[@class='modal-body pb-0']//h4").Contains("Creative Details for "), "Creative Details Popup header text does not match");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//td"), "Row Values not present in Table");
            string adCode = driver._getText("xpath", "//div[@class='modal-body pb-0']//tr[contains(@class, 'adid') or contains(@class, 'adcode')]/td");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-modal-footer//button[text()='Close']"), "'Close' button not present on Creative Details Popup");
            driver._click("xpath", "//cft-domain-item-modal-footer//button[text()='Close']");

            return adCode;
        }

        ///<summary>
        ///Verify Checkbox In AgGrid
        ///</summary>
        ///<param name="resultsView">Result View to select ad in</param>
        ///<param name="check">To check or uncheck</param>
        ///<param name="adCode">Adcode to Verify for</param>
        ///<returns></returns>
        public Carousels VerifyCheckboxInAgGrid(string adCode, string resultsView, bool check = true)
        {
            if (resultsView.ToLower().Contains("table"))
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//ag-grid-angular"), "Results not present");
                driver._scrollintoViewElement("xpath", "//ag-grid-angular");
                Thread.Sleep(4000);

                Assert.IsTrue(driver._waitForElement("xpath", "//ag-grid-angular//div[@class='ag-body-container']/div"), "Rows not present");
                IList<IWebElement> rowCollection = driver._findElements("xpath", "//ag-grid-angular//div[@class='ag-body-container']/div");
                string rowIndex = "";
                foreach(IWebElement row in rowCollection)
                {
                    IList<IWebElement> columnCollection = row._findElementsWithinElement("xpath", ".//div[contains(@col-id, 'adcode') or contains(@col-id, 'adId')]/span");
                    if (columnCollection[0].Text.Contains(adCode))
                    {
                        rowIndex = row.GetAttribute("row-index");
                        break;
                    }
                }
                Assert.AreNotEqual("", rowIndex, "Ad Code of Selected Carousel Card not found");

                Assert.IsTrue(driver._isElementPresent("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex + "']"), "Checkbox not present for Specific Row");
                if (check)
                    Assert.IsTrue(driver._getAttributeValue("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex + "']", "class").Contains("selected"), "Checkbox not selected for Specific Row");
                else
                    Assert.IsFalse(driver._getAttributeValue("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex + "']", "class").Contains("selected"), "Checkbox still selected for Specific Row");
            }
            else if (resultsView.ToLower().Contains("thumbnail"))
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div"), "Results not present");
                driver._scrollintoViewElement("xpath", "//cft-domain-item-details/div/div");
                Thread.Sleep(4000);

                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div//img"), "Thumbnail Images not present");
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]"), "Thumbnail Cards not present");
                IList<IWebElement> cardCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]");
                bool avail = false;

                for(int i = 0; i < cardCollection.Count; i++)
                {
                    IList<IWebElement> buttonCollection = cardCollection[i]._findElementsWithinElement("xpath", ".//button[text()='Details']");
                    string classValue = cardCollection[i].GetAttribute("class");
                    buttonCollection[0].Click();
                    if (adCode.Equals(getAdCodeFromCarousel(false)))
                    {
                        avail = true;
                        if (check)
                            Assert.IsTrue(classValue.Contains("NU-selected-card"), "Checkbox in results for selected carousel card is not checked in thumbnail view");
                        else
                            Assert.IsFalse(classValue.Contains("NU-selected-card"), "Checkbox in results for selected carousel card is checked in thumbnail view");
                        break;
                    }
                    Thread.Sleep(2000);
                }
                Assert.IsTrue(avail, "Thumbnail Card for Selected Carousel Card Ad Code not found");
            }
            else if (resultsView.ToLower().Contains("details"))
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div"), "Results not present");
                driver._scrollintoViewElement("xpath", "//cft-domain-item-details/div/div");
                Thread.Sleep(4000);

                //Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div//tr[contains(@class, 'adcode')]/td"), "Ad Codes not present");
                //IList<IWebElement> adCodeCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//tr[contains(@class, 'adcode')]/td");
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]"), "Detail Cards not present");
                IList<IWebElement> cardCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]");
                bool avail = false;
                for (int i = 0; i < cardCollection.Count; i++)
                {
                    IList<IWebElement> buttonCollection = cardCollection[i]._findElementsWithinElement("xpath", ".//button[text()='Details']");
                    string classValue = cardCollection[i].GetAttribute("class");
                    buttonCollection[0].Click();
                    if (adCode.Equals(getAdCodeFromCarousel(false)))
                    {
                        avail = true;
                        if (check)
                            Assert.IsTrue(classValue.Contains("NU-selected-card"), "Checkbox in results for selected carousel card is not checked in thumbnail view");
                        else
                            Assert.IsFalse(classValue.Contains("NU-selected-card"), "Checkbox in results for selected carousel card is checked in thumbnail view");
                        break;
                    }
                    Thread.Sleep(2000);
                }
                Assert.IsTrue(avail, "Detail Card for Selected Carousel Card Ad Code not found");
            }
            else if (resultsView.ToLower().Contains("carousel"))
            {
                if (check)
                {
                    Thread.Sleep(4000);
                    Assert.IsTrue(driver._waitForElement("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div[contains(@class, 'selected')]", 10), "No carousel is selected");
                    driver._scrollintoViewElement("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div[contains(@class, 'selected')]");
                    IList<IWebElement> selectedCardCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div[contains(@class, 'selected')]");

                    if (selectedCardCollection.Count != 0)
                    {
                        bool avail = false;
                        for (int i = 0; i < selectedCardCollection.Count; i++)
                        {
                            IList<IWebElement> buttonCollection = selectedCardCollection[i]._findElementsWithinElement("xpath", ".//button[text()='Details']");
                            buttonCollection[0].Click();
                            string currAdCode = getAdCodeFromCarousel(false);
                            if (currAdCode.ToLower().Contains(adCode.ToLower()))
                            {
                                avail = true;
                                break;
                            }
                        }
                        Assert.IsTrue(avail, "Selected Carousel Card for Selected Carousel Card Ad Code not found");
                    }
                }
                else
                {
                    Assert.IsTrue(driver._waitForElement("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div[not(contains(@class, 'selected'))]"), "All carousels are selected");
                    driver._scrollintoViewElement("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div[not(contains(@class, 'selected'))]");

                    if(!driver._waitForElement("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div[contains(@class, 'selected')]"))
                    {
                        Results.WriteStatus(test, "Pass", "Verified, Checkbox for selected carousel is not checked in '" + resultsView + "' view.");
                        return new Carousels(driver, test);
                    }

                    bool avail = false;
                    IList<IWebElement> slidesCount = driver._findElements("xpath", "//cft-domain-item-carousel//ol[@class='carousel-indicators']/li");
                    for (int num = 0; num < slidesCount.Count; num++)
                    {
                        IList<IWebElement> unselectedCardCollection = driver._findElements("xpath", "//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div[not(contains(@class, 'selected'))]");
                        if (unselectedCardCollection.Count != 0)
                        {
                            for (int i = 0; i < unselectedCardCollection.Count; i++)
                            {
                                Thread.Sleep(4000);
                                IList<IWebElement> buttonCollection = unselectedCardCollection[i]._findElementsWithinElement("xpath", ".//button[text()='Details']");
                                buttonCollection[0].Click();
                                string currAdCode = getAdCodeFromCarousel(false);
                                if (currAdCode.ToLower().Contains(adCode.ToLower()))
                                {
                                    avail = true;
                                    break;
                                }
                            }
                            if (avail)
                                break;
                            Thread.Sleep(1500);
                            driver._click("xpath", "//cft-domain-item-carousel//a[contains(@class, 'right')]");
                        }
                        Assert.IsTrue(avail, "Unselected Carousel Card for Selected Carousel Card Ad Code not found");
                    }
                }
            }

            Thread.Sleep(2000);

            if (check)
                Results.WriteStatus(test, "Pass", "Verified, Checkbox for selected carousel is checked in '" + resultsView + "' view.");
            else
                Results.WriteStatus(test, "Pass", "Verified, Checkbox for selected carousel is not checked in '" + resultsView + "' view.");

            return new Carousels(driver, test);
        }

        ///<summary>
        ///Select Record From Results
        ///</summary>
        ///<param name="resultsView">Result View to select ad in</param>
        ///<param name="check">To check or uncheck</param>
        ///<param name="adCode">Ad code to check against</param>
        ///<returns></returns>
        public string selectRecordFromResults(string resultsView, bool check = true, string adCode = "")
        {
            if (resultsView.ToLower().Equals("table"))
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//ag-grid-angular"), "Results not present");
                driver._scrollintoViewElement("xpath", "//ag-grid-angular");

                Assert.IsTrue(driver._waitForElement("xpath", "//ag-grid-angular//div[@class='ag-body-container']/div"), "Rows not present");
                IList<IWebElement> rowCollection = driver._findElements("xpath", "//ag-grid-angular//div[@class='ag-body-container']/div");
                string rowIndex = "";
                if (adCode.Equals(""))
                {
                    IList<IWebElement> checkboxColl = driver._findElements("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div");
                    int num = 0;
                    while (checkboxColl[num].GetAttribute("class").Contains("selected"))
                        ++num;
                    rowIndex = rowCollection[num].GetAttribute("row-index");
                    IList<IWebElement> rowEleCollection = rowCollection[num]._findElementsWithinElement("xpath", ".//div[contains(@col-id, 'adcode') or contains(@col-id, 'adId')]");
                    adCode = rowEleCollection[0].Text;
                    Assert.IsTrue(driver._isElementPresent("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex + "']/div[@col-id='selected']"), "Checkbox not present for Specific Row");
                }
                else
                {
                    foreach (IWebElement row in rowCollection)
                    {
                        IList<IWebElement> columnCollection = row._findElementsWithinElement("xpath", ".//div[contains(@col-id,'adcode')]/span");
                        if (columnCollection[0].Text.Contains(adCode))
                        {
                            rowIndex = row.GetAttribute("row-index");
                            break;
                        }
                    }
                    Assert.AreNotEqual("", rowIndex, "Ad Code of Selected Carousel Card not found");
                }

                if (check)
                {
                    if(!driver._getAttributeValue("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex[0] + "']", "class").Contains("selected"))
                    {
                        Actions action = new Actions(driver);
                        action.MoveToElement(driver.FindElement(By.XPath("//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex[0] + "']/div[@col-id='selected']"))).MoveByOffset(2, 2).Perform();
                        Thread.Sleep(500);
                        Assert.IsTrue(driver._getAttributeValue("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex + "']/div[@col-id='selected']", "class").Contains("hover"), "Record's background did not change in response to mouse hover");
                        driver._click("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex + "']/div[@col-id='selected']");
                    }
                    Thread.Sleep(1000);
                    Assert.AreEqual(true, driver._getAttributeValue("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex[0] + "']", "class").Contains("selected"), "Checkbox not selected for Specific Row");
                }
                else
                {
                    if(driver._getAttributeValue("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex[0] + "']", "class").Contains("selected"))
                        driver._click("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex + "']/div[@col-id='selected']");
                    Thread.Sleep(1000);
                    Assert.AreNotEqual(true, driver._getAttributeValue("xpath", "//ag-grid-angular//div[@class='ag-pinned-left-cols-container']/div[@row-index='" + rowIndex[0] + "']", "class").Contains("selected"), "Checkbox still selected for Specific Row");
                }
            }
            if (resultsView.ToLower().Equals("thumbnail"))
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div"), "Results not present");
                driver._scrollintoViewElement("xpath", "//cft-domain-item-details/div/div");

                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details//div[@class='row'][1]/div//button[text()='Details']"), "Details Button Not Present not present");
                IList<IWebElement> detailsButtonCollection = driver._findElements("xpath", "//cft-domain-item-details//div[@class='row'][1]/div//button[text()='Details']");
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]"), "Thumbnail Cards not present");
                IList<IWebElement> cardCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]");
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div//img"), "Thumbnail Images not present");
                IList<IWebElement> thumbnailImageCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//img");
                int cardIndex = 0;
                if (!adCode.Equals(""))
                {
                    bool avail = false;
                    for (int i = 0; i < thumbnailImageCollection.Count; i++)
                        if (thumbnailImageCollection[i].GetAttribute("src").Contains(adCode))
                        {
                            avail = true;
                            cardIndex = i;
                            break;
                        }
                    Assert.IsTrue(avail, "Thumbnail Card for Selected Carousel Card Ad Code not found");
                }
                else
                {
                    int num = 0;
                    while (cardCollection[num].GetAttribute("class").Contains("NU-selected-card"))
                        ++num;
                    detailsButtonCollection[num].Click();
                    adCode = getAdCodeFromCarousel(false);
                    if (!driver._waitForElement("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]"))
                        homePage.selectViewForResultsDisplay("Thumbnail");
                }

                Actions action = new Actions(driver);
                cardCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", cardCollection[0]);
                thumbnailImageCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//img");

                if (check)
                {
                    if(!cardCollection[cardIndex].GetAttribute("class").Contains("NU-selected-card"))
                        action.MoveToElement(driver._findElement("xpath", "//div[@class='py-3']//button")).Perform();
                    action.MoveToElement(thumbnailImageCollection[cardIndex]).Perform();
                    action.Click().Perform();
                    Thread.Sleep(1000);
                    cardCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]");
                    Assert.IsTrue(cardCollection[cardIndex].GetAttribute("class").Contains("NU-selected-card"), "Checkbox in results for selected carousel card is not checked in thumbnail view");
                }
                else
                {
                    if (cardCollection[cardIndex].GetAttribute("class").Contains("NU-selected-card"))
                        action.MoveToElement(driver._findElement("xpath", "//div[@class='py-3']//button")).Perform();
                    action.MoveToElement(thumbnailImageCollection[cardIndex]).Perform();
                    action.Click().Perform();
                    Thread.Sleep(1000);
                    cardCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]");
                    Assert.IsFalse(cardCollection[cardIndex].GetAttribute("class").Contains("NU-selected-card"), "Checkbox in results for selected carousel card is checked in thumbnail view");
                }
            }
            if (resultsView.ToLower().Equals("details"))
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div"), "Results not present");
                driver._scrollintoViewElement("xpath", "//cft-domain-item-details/div/div");

                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]"), "Detail Cards not present");
                IList<IWebElement> cardCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]");
                int cardIndex = 0;
                if (!adCode.Equals(""))
                {
                    bool avail = false;
                    for (int i = 0; i < cardCollection.Count; i++)
                    {
                        IList<IWebElement> buttonCollection = cardCollection[i]._findElementsWithinElement("xpath", ".//button[text()='Details']");
                        buttonCollection[0].Click();
                        string currAdCode = getAdCodeFromCarousel(false);
                        if (currAdCode.ToLower().Contains(adCode.ToLower()))
                        {
                            avail = true;
                            cardIndex = i;
                            break;
                        }
                    }
                    Assert.IsTrue(avail, "Detail Card for Selected Carousel Card Ad Code not found");
                }
                else
                {
                    int num = 0;
                    while (cardCollection[num].GetAttribute("class").Contains("NU-selected-card"))
                        ++num;
                    IList<IWebElement> buttonCollection = cardCollection[num]._findElementsWithinElement("xpath", ".//button[text()='Details']");
                    buttonCollection[0].Click();
                    adCode = getAdCodeFromCarousel(false);
                }
                Thread.Sleep(1000);

                Actions action = new Actions(driver);
                action.MoveToElement(cardCollection[cardIndex]).Perform();

                if (check)
                {
                    if(!cardCollection[cardIndex].GetAttribute("class").Contains("NU-selected-card"))
                        action.Click().Perform();
                    Thread.Sleep(1000);
                    cardCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]");
                    Assert.IsTrue(cardCollection[cardIndex].GetAttribute("class").Contains("NU-selected-card"), "Checkbox in results for selected carousel card is not checked in details view");
                }
                else
                {
                    if(cardCollection[cardIndex].GetAttribute("class").Contains("NU-selected-card"))
                        action.Click().Perform();
                    Thread.Sleep(1000);
                    cardCollection = driver._findElements("xpath", "//cft-domain-item-details/div/div//div[contains(@class, 'selectable')]");
                    Assert.IsFalse(cardCollection[cardIndex].GetAttribute("class").Contains("NU-selected-card"), "Checkbox in results for selected carousel card is checked in details view");
                }
            }

            if (check)
                Results.WriteStatus(test, "Pass", "Selected, Ad with Ad Code '" + adCode + "' in view '" + resultsView + "'");
            else
                Results.WriteStatus(test, "Pass", "Deselected, Ad with Ad Code '" + adCode + "' in view '" + resultsView + "'");

            return adCode;
        }

        ///<summary>
        ///Click On Export Option
        ///</summary>
        ///<param name="optionName">Export or reset</param>
        ///<param name="num">No. of selected items</param>
        ///<returns></returns>
        public Carousels clickOnExportOptions(string optionName, int num = 1, bool click = true, bool clickCancel = false)
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-export-button//button[1]"), "Export Button not present");
            driver.MouseHoverUsingElement("xpath", "//cft-export-button//button[1]");
            Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-export-button//button[1]", "containerclass").Contains("NU-tooltip NU-tooltip-sapphire"), "Export button is not in sapphire color");
            if (num > 0)
            {
                Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-export-button//cft-speed-dial-trigger//button", "class").Contains("green rounded-pill"), "Export button is not in green color");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-speed-dial-trigger/button/small"), "Export Button Text not present");

                Assert.AreEqual(true, driver._getText("xpath", "//cft-speed-dial-trigger/button/small").Contains(num + " item"), "Export Button Text does not match");
                driver._click("xpath", "//cft-export-button//cft-speed-dial-trigger//button");

                Assert.IsTrue(driver._waitForElement("xpath", "//cft-speed-dial-actions/button[@tooltip='Export Selected']"), "Export Selected Button not present");
                Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-speed-dial-actions/button[@tooltip='Export Selected']", "class").Contains("green rounded-circle"), "Export Selected button is not in green color");
                Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-speed-dial-actions/button[@tooltip='Export Selected']", "class").Contains("NU-floating-button-child"), "Export Selected button is not smaller than export button");
                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-speed-dial-actions/button[@tooltip='Reset Selected']"), "Reset Selected Button not present");
                Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-speed-dial-actions/button[@tooltip='Reset Selected']", "class").Contains("green rounded-circle"), "Reset Selected button is not in green color");
                Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-speed-dial-actions/button[@tooltip='Reset Selected']", "class").Contains("NU-floating-button-child"), "Reset Selected button is not smaller than export button");
            }
            else
            {
                Assert.IsTrue(driver._getAttributeValue("xpath", "//cft-export-button//button[1]", "tooltip").Contains("Export Results"), "Export button tooltip text does not match");
                if (click)
                {
                    driver._click("xpath", "//cft-export-button//button[1]");
                }
            }

            if (optionName.ToLower().Contains("export"))
                driver._click("xpath", "//cft-speed-dial-actions/button[@tooltip='Export Selected']");
            else if(optionName.ToLower().Contains("reset"))
                driver._click("xpath", "//cft-speed-dial-actions/button[@tooltip='Reset Selected']");

            Thread.Sleep(2000);

            Results.WriteStatus(test, "Pass", "Clicked, '" + optionName + "' option of Export Button");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Verify Select An Export Type Popup
        ///</summary>
        ///<returns></returns>
        public Carousels VerifySelectAnExportTypePopup(bool popupVisible = true)
        {
            if (popupVisible)
            {
                Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-content']"), "'Select An Export Type' Popup not present");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-header']//h4"), "'Select An Export Type' Popup header not present");
                Assert.IsTrue(driver._getText("xpath", "//div[@class='modal-header']//h4").Contains("Select an Export Type"), "'Select An Export Type' Popup header text does not match");

                Assert.IsTrue(driver._isElementPresent("xpath", "//cft-export-modal//cft-export-icon"), "'Export Icon' not present on Select An Export Type Popup");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body']//button"), "'File Type' not present on Select An Export Type Popup");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body']//label/span"), "'Export Formats' not present on Select An Export Type Popup");
                IList<IWebElement> exportFormatCollection = driver._findElements("xpath", "//div[@class='modal-body']//label/span");
                bool avail = false;
                foreach (IWebElement exportFormat in exportFormatCollection)
                    if (exportFormat.Text.ToLower().Contains("ad list - xls"))
                    {
                        avail = true;
                        Assert.AreNotEqual(null, exportFormat.GetCssValue("background"), "Ad List - XLS is not selected by default.");
                        break;
                    }
                Assert.IsTrue(avail, "Ad List - XLS not found in Export Formats");

                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Cancel ']"), "'Cancel' button not present on Select An Export Type Popup");
                Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Send ']"), "'Send' button not present on Select An Export Type Popup");

                Results.WriteStatus(test, "Pass", "Verified, Select An Export Type Popup");
            }
            else
            {
                Assert.IsTrue(driver._waitForElementToBeHidden("xpath", "//div[@class='modal-content']"), "'Select An Export Type' Popup still present");
                Results.WriteStatus(test, "Pass", "Verified, Select An Export Type Popup is closed");
            }
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Select Option and Click Button on Select An Export Type Popup
        ///</summary>
        ///<param name="button">Button to be clicked</param>
        ///<param name="optionName">Option to be selected</param>
        ///<returns></returns>
        public Carousels selectOptionAndClickButtonOnSelectAnExportTypePopup(string button, string optionName = "Ad List - XLS")
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//div[@class='modal-body']//label/span"), "'Export Formats' not present on Select An Export Type Popup");
            IList<IWebElement> exportFormatCollection = driver._findElements("xpath", "//div[@class='modal-body']//label/span");
            bool avail = false;
            foreach (IWebElement exportFormat in exportFormatCollection)
                if (exportFormat.Text.ToLower().Contains(optionName.ToLower()))
                {
                    avail = true;
                    exportFormat.Click();
                    break;
                }
            Assert.IsTrue(avail, "'" + optionName + "' not found in Export Formats");

            if (button.ToLower().Equals("cancel"))
                driver._click("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Cancel ']");
            else
                driver._click("xpath", "//div[@class='modal-footer pt-0']//button[text()=' Send ']");

            Thread.Sleep(90000);

            Results.WriteStatus(test, "Pass", "Clicked, '" + button + "' button after selecting option '" + optionName + "' on Select An Export Type Popup");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Get Details From Carousel
        ///</summary>
        ///<returns></returns>
        public string[, ] getDetailsFromCarousel()
        {
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//th"), "Row headers not present in Tbale");
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//td"), "Row Values not present in Tbale");
            IList<IWebElement> rowHeaderCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//th");
            IList<IWebElement> rowValueCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//td");
            string[,] detailGrid = new string[rowHeaderCollection.Count, 2];
            for (int i = 0; i < rowHeaderCollection.Count; i++)
            {
                detailGrid[i, 0] = rowHeaderCollection[i].Text;
                detailGrid[i, 1] = rowValueCollection[i].Text;
            }

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-modal-footer//button[text()='Close']"), "'Close' button not present on Creative Details Popup");
            driver._click("xpath", "//cft-domain-item-modal-footer//button[text()='Close']");

            return detailGrid;
        }

        ///<summary>
        ///Verify Data in Exported file from Carousel
        ///</summary>
        ///<param name="fileName">File to be verified</param>
        ///<param name="dataGrid">Detail from Carousels</param>
        ///<param name="screen">Screen from where data is exported</param>
        ///<returns></returns>
        public Carousels VerifyDataInExportedFileFromCarousel(string fileName, string screen, string[,] dataGrid)
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

            Assert.IsTrue((range.Cells[2, 1] as Excel.Range).Text.Contains(screen), "Heading Text of Sheet does not match");
            int titleCounter = 0;
            for (int cCnt = 1; cCnt >= cl; cCnt++)
            {
                for (int i = 0; i < dataGrid.GetLength(0); i++)
                    if ((range.Cells[5, cCnt] as Excel.Range).Text.Contains(dataGrid[i, 0]) && dataGrid[i,1] != "")
                    {
                        Assert.IsTrue((range.Cells[5, cCnt] as Excel.Range).Text.Contains(dataGrid[i, 1]), "'" + dataGrid[i, 0] + "' does not match in Excel File.");
                        ++titleCounter;
                        break;
                    }
                else if((range.Cells[5, cCnt] as Excel.Range).Text.Contains(dataGrid[i, 0]))
                    {
                        ++titleCounter;
                        break;
                    }
            }

            if (titleCounter == dataGrid.GetLength(0))
                Results.WriteStatus(test, "Pass", "All Columns of Data found in the file.");

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            File.Delete(FilePath);

            Results.WriteStatus(test, "Pass", "Verified, Data in Exported File from Carousel");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Verify Applied Filter On Media Type
        ///</summary>
        ///<param name="mediaType">Media type filter</param>
        ///<returns></returns>
        public Carousels VerifyAppliedFilterOnMediaType(string[] mediaType)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//slide"), "Slides not present in carousels");
            IList<IWebElement> slidesCollection = driver._findElements("xpath", "//cft-domain-item-carousel//slide");
            foreach(IWebElement slide in slidesCollection)
            {
                Assert.AreEqual("false", slide.GetAttribute("aria-hidden"), "Slide not visible");
                Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-carousel//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div"), "'Carousel Cards' not present on Carousel");
                IList<IWebElement> carouselsCollection = driver._findElements("xpath", "//cft-domain-item-carousel//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div");
                foreach (IWebElement carousel in carouselsCollection)
                {
                    Assert.IsTrue(driver._isElementPresent("xpath", "//slide[@aria-hidden='false']//div[contains(@class, 'NU-card-tags')]/button"), "'Media Type' not present on Carousel Cards");
                    IList<IWebElement> mediaTypeCollection = carousel._findElementsWithinElement("xpath", ".//div[contains(@class, 'NU-card-tags')]/button");
                    Assert.AreEqual(1, mediaTypeCollection.Count, "Locating Media Type Element on specific carousel was not successful");
                    bool avail = false;
                    foreach (string media in mediaType)
                        if (mediaTypeCollection[0].Text.ToLower().Contains(media.ToLower()))
                            avail = true;
                    Assert.IsTrue(avail, "'" + mediaTypeCollection[0].Text + "' media type did not match applied filters");
                }
                GetImageSourceFromCarouselAndClickSlider(true, true);
            }

            Results.WriteStatus(test, "Pass", "Verified, Applied Filter On Media Type");
            return new Carousels(driver, test);
        }

        ///<summary>
        ///Get Details From Carousel
        ///</summary>
        ///<param name="detailName">Name of the detail field</param>
        ///<returns></returns>
        public string getSpecificDetailsFromCarousel(string detailName)
        {
            while(driver._isElementPresent("xpath", "//div[@class='modal-content']//td//*[name()='svg']"))
                Thread.Sleep(2000);
            while (!driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//tr"))
                driver.Navigate().Refresh();
            Assert.IsTrue(driver._waitForElement("xpath", "//div[@class='modal-body pb-0']//td"), "Row Values not present in Tbale");
            IList<IWebElement> rowsCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//tr");
            IList<IWebElement> rowValueCollection = driver._findElements("xpath", "//div[@class='modal-body pb-0']//td");
            string detailValue = "";
            bool avail = false;
            for (int i = 0; i < rowsCollection.Count; i++)
                if (rowsCollection[i].Text.ToLower().Contains(detailName.ToLower()))
                {
                    avail = true;
                    IList<IWebElement> value = rowsCollection[i]._findElementsWithinElement("xpath", ".//td");
                    detailValue = value[0].Text;
                    break;
                }
            Assert.IsTrue(avail, "'" + detailName + "' not found.");

            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-modal-footer//button[text()='Close']"), "'Close' button not present on Creative Details Popup");
            driver._click("xpath", "//cft-domain-item-modal-footer//button[text()='Close']");

            Results.WriteStatus(test, "Pass", "Got, Value '" + detailValue + "' respective to '" + detailName + "' from Details Popup");
            return detailValue;
        }

        ///<summary>
        ///Verify Sorting on Carousel
        ///</summary>
        ///<param name="criterion">Criterion to sort</param>
        ///<returns></returns>
        public Carousels VerifySortingOnCarousel(string criterion)
        {
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//label/span[text()='Sorted by:']"), "'Sorted by' label not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//label[2]/input[@name='sortField']"), "'Spend' radio button not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//label/span[text()=' Spend']"), "'Spend' label not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//label[3]/input[@name='sortField']"), "'First Run Date' radio button not present");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//label/span[text()=' First Run Date']"), "'First Run Date' label not present");

            if (criterion.ToLower().Equals("spend"))
            {
                driver._click("xpath", "//cft-domain-item-carousel//label[2]/input[@name='sortField']");
                criterion = "Total Spend";
            }
            else
            {
                driver._click("xpath", "//cft-domain-item-carousel//label[3]/input[@name='sortField']");
                criterion = "First Run";
            }

            Thread.Sleep(1000);
            Assert.IsTrue(driver._waitForElement("xpath", "//cft-domain-item-carousel//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div"), "'Carousel Cards' not present on Carousel");
            IList<IWebElement> carouselsCollection = driver._findElements("xpath", "//cft-domain-item-carousel//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div");
            Assert.IsTrue(driver._isElementPresent("xpath", "//cft-domain-item-carousel//slide"), "Slides not present in carousels");
            IList<IWebElement> slidesCollection = driver._findElements("xpath", "//cft-domain-item-carousel//slide");
            int i = 0;
            string[] detailValues = new string[0];
            foreach (IWebElement slide in slidesCollection)
            {
                int newLength = detailValues.Length + carouselsCollection.Count;
                Array.Resize(ref detailValues, newLength);
                foreach (IWebElement carousel in carouselsCollection)
                {
                    clickButtonOnCarousel("Details");
                    detailValues[i] = getSpecificDetailsFromCarousel(criterion);
                    VerifySelectAdFunctionality(false);
                    ++i;
                }
                GetImageSourceFromCarouselAndClickSlider(true, true);
                carouselsCollection = driver._findElements("xpath", "//cft-domain-item-carousel//slide[@aria-hidden='false']//cft-domain-item-thumbnail-short/div");
            }
            if(criterion.ToLower().Equals("first run"))
            {
                DateTime[] dateList = new DateTime[detailValues.Length];
                for(int j = 0; j < detailValues.Length - 1; j++)
                {
                    int index = detailValues[j].IndexOf(", ") + 6;
                    Console.WriteLine(j + ". Detail Value: " + detailValues[j]);
                    string temp = detailValues[j].Substring(0, index);
                    Console.WriteLine("Temp: " + temp);
                    DateTime date = DateTime.Today;
                    Assert.IsTrue(DateTime.TryParse(temp, out date), "Conversion to date failed");
                    dateList[j] = date;
                    Console.WriteLine("Date: " + date);
                    Console.WriteLine("Date List: " + dateList[j]);
                }
                DateTime[] newDateList = dateList;
                Array.Sort<DateTime>(dateList);
                Array.Reverse(dateList);
                Assert.IsTrue(dateList.SequenceEqual<DateTime>(newDateList), "Sorting Functionality on First Run did not work.");
                Results.WriteStatus(test, "Pass", "Verified, Sorting Functionality on First Run");
            }
            else
            {
                Decimal[] spendList = new Decimal[detailValues.Length];
                for (int j = 0; j < detailValues.Length; j++)
                {
                    Console.WriteLine(j + ". Detail Value: " + detailValues[j]);
                    int index = detailValues[j].Length - 1;
                    string temp = detailValues[j].Substring(1, index);
                    Console.WriteLine("Temp: " + temp);
                    Decimal spend = 0;
                    Assert.IsTrue(Decimal.TryParse(temp, out spend), "Conversion to Decimal failed");
                    spendList[j] = spend;
                    Console.WriteLine("Spend: " + spend);
                    Console.WriteLine("Spend List: " + spendList[j]);
                }
                Decimal[] newspendList = spendList;
                Array.Sort<Decimal>(spendList);
                Array.Reverse(spendList);
                Assert.IsTrue(spendList.SequenceEqual<Decimal>(newspendList), "Sorting Functionality on Spend did not work.");
                Results.WriteStatus(test, "Pass", "Verified, Sorting Functionality on Spend");
            }

            return new Carousels(driver, test);
        }

        ///<summary>
        ///Convert Column Name Aray into Datagrid
        ///</summary>
        ///<returns></returns>
        public string[,] convertColumnNameArrayIntoDatagrid(string[] columnNames, string[] rowValues)
        {
            string[,] dataGrid = new string[columnNames.Length, 2];
            if (rowValues == null)
            {
                for (int i = 0; i < columnNames.Length; i++)
                    dataGrid[i, 1] = "";
            }
            else
            {
                for (int i = 0; i < columnNames.Length; i++)
                    dataGrid[i, 1] = rowValues[i];
            }

            Results.WriteStatus(test, "Pass", "Created, Data grid with given data.");
            return dataGrid;
        }
    }

}
