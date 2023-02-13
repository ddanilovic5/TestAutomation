using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumHelpers;
using SeleniumHelpers.Extensions;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace PageObjects
{
    public class ProductOfferPage
    {
        public NavigationBar navigationBar;
        public ProductOfferPage()
        {
            this.navigationBar = new NavigationBar();
        }

        private const string _url = "/product-offers";
        private string _productRowLocator = "[row-index='{0}']";
        private string _productNameLocator = "//*[text()='{0}']/../../..";
        public void GoTo() => Driver.Instance.Navigate().GoToUrl(_url);

        private By CreatePOButtonLocator => By.CssSelector(".product-offer-row .btn-primary");
        private By PopupLocator => By.CssSelector("div.swal2-popup");
        private By PopupMessageLocator => By.Id("swal2-title");
        private By PrimaryRateLocator => By.CssSelector("[col-id='rateTypePrimary']");
        private By CPCLocator => By.CssSelector("[col-id='cpc']");
        private By Controled1M => By.CssSelector("[col-id='controlled1OfMany']");
        private By Controled11 => By.CssSelector("[col-id='controlledExclusive']");
        private By Controled12 => By.CssSelector("[col-id='controlled1Of2']");
        private By Closed1M => By.CssSelector("[col-id='closed1OfMany']");
        private By Closed11 => By.CssSelector("[col-id='closedExclusive']");
        private By Closed12 => By.CssSelector("[col-id='closed1Of2']");
        private By RestrictedBid => By.CssSelector("[col-id='nonPreferredFormularyAccessRateRestricted']");
        private By EffectiveStartDateLocator => By.CssSelector("[col-id='effectiveStartDate']");
        private By POStatusLocator => By.CssSelector("[col-id='status']");
        private By ProductDropdownLocator => By.CssSelector("[class='dropdown-menu show']");
        private By SendToAscentButtonLocator => By.XPath("//a[text()='Send to Ascent']");
        private By SendForCSApprovalLocator => By.XPath("//*[text() =' Send for CS Approval ']");
        private By SendSelectedToAscentLocator => By.XPath("//*[text() =' Send Selected to Ascent ']");
        private By CheckForActionInputLocator => By.XPath("//*[@col-id='actionCheckbox']//input");

        private IWebElement CreatePOButton => Driver.Instance.FindElement(CreatePOButtonLocator);
        private IWebElement ConfirmPopupButton => Driver.Instance.FindElement(By.CssSelector(".swal2-actions .swal2-confirm"));
        private IWebElement PopupMessage => Driver.Instance.FindElement(PopupMessageLocator);
        private ReadOnlyCollection<IWebElement> Products => Driver.Instance.FindElements(By.CssSelector("[col-id='productOfferProduct']"));
        private IWebElement PrimaryRate => Driver.Instance.FindElement(PrimaryRateLocator);
        private IWebElement ViewFootnotesButton => Driver.Instance.FindElement(By.XPath("//*[text()=' View  Footnotes ']")); //consider adding ID
        private IWebElement FootnoteModalWindow => Driver.Instance.FindElement(By.TagName("ngb-modal-window"));
        private ReadOnlyCollection<IWebElement> AllFootnotes => Driver.Instance.FindElements(By.CssSelector(".product-offer-footnote-detail-wrapper--content"));
        private IWebElement CloseFootnoteButton => Driver.Instance.FindElement(By.CssSelector(".btn-close"));
        private IWebElement MultipleSelectButton => Driver.Instance.FindElement(By.CssSelector("[label='Multiple select']"));
        private IWebElement SendForCSApprovalButton => Driver.Instance.FindElement(SendForCSApprovalLocator);
        private IWebElement CSReviewSelectedButton => Driver.Instance.FindElement(By.XPath("//*[text() = ' CS Review Selected ']"));


        #region Methods

        public void CreatePOButtonClick()
        {
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(CreatePOButtonLocator).Count != 0);

            CreatePOButton.Click();

            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(PopupLocator).Count != 0);
            ConfirmPopupButton.Click();

            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(By.CssSelector(".modal-body--product-offer")).Count != 0);
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(By.ClassName("product-offer-section")).Count != 0);

            Driver.Wait(TimeSpan.FromSeconds(2));
        }

        public void ViewFootnotesButtonClick()
        {
            Driver.Wait(3, () => ViewFootnotesButton.Displayed);
            ViewFootnotesButton.Click();
            Driver.Wait(3, ()=> Driver.Instance.FindElementsNoWait(By.CssSelector(".comment-sidebar")).Count != 0);
        }

        public void CloseFootnoteButtonClick()
        {
            CloseFootnoteButton.Click();
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(By.CssSelector(".comment-sidebar")).Count == 0);
        }

        private void GetProductRow(string productName)
        {
            string fullProductLocator = String.Format(_productNameLocator, productName);

            IWebElement product = Driver.Instance.FindElement(By.XPath(fullProductLocator));

            if(product == null)
            {
                throw new NoSuchElementException($"Matrix doesn't have product called {productName}");
            }

            string row = product.GetAttribute("row-index");

            _productRowLocator = string.Format(_productRowLocator, row); // set value for the product row
        }

        public void SendToAscentAction()
        {
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(PopupLocator).Count == 0);

            IWebElement productDropdown = Driver.Instance.FindElements(By.CssSelector(_productRowLocator))[2];
            productDropdown.Click();

            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(ProductDropdownLocator).Count != 0);

            productDropdown.FindElement(SendToAscentButtonLocator).Click();

            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(PopupLocator).Count != 0);
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(PopupMessageLocator).Count != 0);
        }

        public void MultipleSelectButtonHover()
        {
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(By.ClassName("page-table-search")).Count != 0);

            Actions actions = new Actions(Driver.Instance);
            actions.MoveToElement(MultipleSelectButton).Perform();

            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(By.ClassName("dropdown-content")).Count != 0);
        }

        public void SendForCSApprovalClick()
        {
            MultipleSelectButtonHover();
            SendForCSApprovalButton.Click();

            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(SendSelectedToAscentLocator).Count != 0);
        }

        public void SendSelectedToAscentAction(string productName)
        {
            GetProductRow(productName);

            IWebElement productRow = Driver.Instance.FindElements(By.CssSelector(_productRowLocator))[1];
            IWebElement checkBoxInput = productRow.FindElement(CheckForActionInputLocator);

            if(checkBoxInput == null) 
            {
                throw new NoSuchElementException("Checkbox in column 'Send Selected To Ascent' was not found.");
            }

            checkBoxInput.Click();

            Driver.Wait(TimeSpan.FromSeconds(1));
        }

        public void CSReviewButtonClick()
        {
            CSReviewSelectedButton.Click();
        }

        #endregion

        #region Verification

        public IWebElement VerifySuccessPopup()
        {
            Driver.Wait(3, ()=> Driver.Instance.FindElementsNoWait(PopupMessageLocator).Count != 0);

            return PopupMessage;
        }

        public string FetchPopupMessageByText()
        {
            string popupMessage = PopupMessage.Text.Trim();

            return popupMessage;
        }

        public bool VerifyProductIsInTheList(string productName)
        {
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(By.CssSelector("[ref='leftContainer']")).Count != 0);

            GetProductRow(productName); // set row value for other methods

            return Products.FirstOrDefault(x => x.Text.Trim() == productName) != null;
        }

        public bool VerifyPrimaryRateTypes(string primaryRates)
        {
            IWebElement productRow = Driver.Instance.FindElement(By.CssSelector(_productRowLocator));
            IWebElement primaryRateElement = productRow.FindElement(PrimaryRateLocator);

            return primaryRateElement.Text.Trim().Contains(primaryRates);
        }

        public bool VerifyCPC(string cpc)
        {
            IWebElement productRow = Driver.Instance.FindElement(By.CssSelector(_productRowLocator));
            Driver.Wait(TimeSpan.FromSeconds(1));

            IWebElement cpcElement;

            try
            {
                 cpcElement = productRow.FindElement(CPCLocator);

            }
            catch (StaleElementReferenceException)
            {
                Driver.Instance.Navigate().Refresh();
                Driver.Wait(10, () => Driver.Instance.FindElementsNoWait(By.CssSelector("ref='leftContainer'")).Count != 0);
                Driver.Wait(10, () => Driver.Instance.FindElementsNoWait(CPCLocator).Count != 0);

                cpcElement = productRow.FindElement(CPCLocator);
            }

            return cpcElement.Text.Trim().Contains(cpc);
        }

        public bool VerifyControlledRebates(string ratio, string value)
        {
            IWebElement productRow = Driver.Instance.FindElements(By.CssSelector(_productRowLocator))[1];
            IWebElement rebateElement = productRow.FindElement(GetPreferredBrandRatesLocator("Controlled", ratio));

            return rebateElement.Text.Trim().Contains(value);
        }

        public bool VerifyRestrictedNPBRate(string value)
        {
            IWebElement productRow = Driver.Instance.FindElements(By.CssSelector(_productRowLocator))[1];
            IWebElement restrictedBidElement = productRow.FindElement(RestrictedBid);

            return restrictedBidElement.Text.Trim().Contains(value);
        }

        public bool VerifyFootnotePresent(string text)
        {
            Driver.Wait(3, () => Driver.Instance.FindElements(By.CssSelector("[class='modal-body']")).Count != 0);

           // IWebElement footnote = FootnoteModalWindow.FindElements(By.CssSelector(".product-offer-footnote-detail-wrapper--content")).FirstOrDefault(x => x.Text.Trim() == text);

            var nesto = FootnoteModalWindow;
            var dalje = Driver.Instance.FindElements(By.CssSelector(".product-offer-footnote-detail-wrapper--content"));

            IWebElement footnote = dalje.FirstOrDefault(x => x.Text.Trim() == text);


            if (footnote == null)
                return false;

            return true;
        }

        public bool VerifyEffectiveStartDate(DateTime date)
        {
            string currentDate = date.ToString("MM/dd/yyyy");
            IWebElement productRow = Driver.Instance.FindElements(By.CssSelector(_productRowLocator))[1];

            IWebElement effectiveDateElement = productRow.FindElement(EffectiveStartDateLocator);

            return effectiveDateElement.Text.Trim().Equals(currentDate);
        }

        public bool VerifyPOStatus(string status)
        {
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(PopupLocator).Count == 0);
            Driver.Wait(TimeSpan.FromSeconds(1));

            IWebElement productRow = Driver.Instance.FindElements(By.CssSelector(_productRowLocator))[1];
            IWebElement statusElement = productRow.FindElement(POStatusLocator);

            return statusElement.Text.Trim().Equals(status);
        }

        #endregion

        private By GetPreferredBrandRatesLocator(string type, string ration)
        {
            switch (ration)
            {
                case "1:1":
                    if (type == "Controlled")
                        return Controled11;
                    return Closed11;
                case "1:M":
                    if (type == "Controlled")
                        return Controled1M;
                    return Closed1M;
                case "1:2":
                    if (type == "Controlled")
                        return Controled12;
                    return Closed12;
                default:
                    throw new Exception("Preferred brand rate was not found.");
            }
        }

    }
}
