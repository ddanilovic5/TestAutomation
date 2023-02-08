using OpenQA.Selenium;
using SeleniumHelpers;
using SeleniumHelpers.Extensions;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace PageObjects
{
    public class ProductOfferPage
    {
        private const string _url = "/product-offers";
        private string _productRow = "[row-index='{0}']";

        public void GoTo() => Driver.Instance.Navigate().GoToUrl(_url);

        private By CreatePOButtonLocator => By.CssSelector(".product-offer-row .btn-primary");
        private By NewOfferPopupLocator => By.CssSelector("div.swal2-popup");
        private By SuccessMessageLocator => By.Id("swal2-title");
        private By PrimaryRateLocator => By.CssSelector("[col-id='rateTypePrimary']");
        private By CPCLocator => By.CssSelector("[col-id='cpc']");
        private By Controled1M => By.CssSelector("[col-id='controlled1OfMany']");
        private By Controled11 => By.CssSelector("[col-id='controlledExclusive']");
        private By Controled12 => By.CssSelector("[col-id='controlled1Of2']");
        private By Closed1M => By.CssSelector("[col-id='closed1OfMany']");
        private By Closed11 => By.CssSelector("[col-id='closedExclusive']");
        private By Closed12 => By.CssSelector("[col-id='closed1Of2']");
        private By EffectiveStartDateLocator => By.CssSelector("col-id='effectiveStartDate'");
        private By POStatusLocator => By.CssSelector("col-id='status'");

        private IWebElement CreatePOButton => Driver.Instance.FindElement(CreatePOButtonLocator);
        private IWebElement ConfirmPopupButton => Driver.Instance.FindElement(By.CssSelector(".swal2-actions .swal2-confirm"));
        private IWebElement SuccessMessage => Driver.Instance.FindElement(SuccessMessageLocator);
        private ReadOnlyCollection<IWebElement> ProductNames => Driver.Instance.FindElements(By.CssSelector("[col-id='productOfferProduct']"));
        private IWebElement PrimaryRate => Driver.Instance.FindElement(PrimaryRateLocator);
        private IWebElement ViewFootnotesButton => Driver.Instance.FindElement(By.XPath("//*[text()=' View  Footnotes ']")); //consider adding ID
        private ReadOnlyCollection<IWebElement> AllFootnotes => Driver.Instance.FindElements(By.CssSelector(".product-offer-footnote-detail-wrapper--content"));
        private IWebElement CloseFootnoteButton => Driver.Instance.FindElement(By.CssSelector(".btn-close"));


        #region Methods

        public void CreatePOButtonClick()
        {
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(CreatePOButtonLocator).Count != 0);

            CreatePOButton.Click();

            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(NewOfferPopupLocator).Count != 0);
            ConfirmPopupButton.Click();

            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(By.CssSelector(".modal-body--product-offer")).Count != 0);
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(By.ClassName("product-offer-section")).Count != 0);
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
            IWebElement product = ProductNames.FirstOrDefault( x=> x.Text == productName);

            if(product == null)
            {
                throw new NoSuchElementException($"Matrix doesn't have product called {productName}");
            }

            string row = product.GetAttribute("row-index");

            string.Format(_productRow, row); // set value for the product row
        }

        #endregion

        #region Verification

        public bool VerifySuccessMessage()
        {
            Driver.Wait(3, ()=> Driver.Instance.FindElementsNoWait(SuccessMessageLocator).Count != 0);

            return SuccessMessage != null;
        }

        public bool VerifyProductIsInTheList(string productName)
        {
            GetProductRow(productName); // set row value for other methods

            return ProductNames.FirstOrDefault(x => x.Text.Trim() == productName) != null;
        }

        public bool VerifyPrimaryRateTypes(string primaryRates)
        {
            IWebElement productRow = Driver.Instance.FindElement(By.CssSelector(_productRow));
            IWebElement primaryRateElement = productRow.FindElement(PrimaryRateLocator);

            return primaryRateElement.Text.Trim().Contains(primaryRates);
        }

        public bool VerifyCPC(string cpc)
        {
            IWebElement productRow = Driver.Instance.FindElement(By.CssSelector(_productRow));
            IWebElement cpcElement = productRow.FindElement(CPCLocator);

            return cpcElement.Text.Trim().Contains(cpc);
        }

        public bool VerifyControlledRebates(string ratio, string value)
        {
            IWebElement productRow = Driver.Instance.FindElement(By.CssSelector(_productRow));
            IWebElement rebateElement = productRow.FindElement(GetPreferredBrandRatesLocator("Controlled", ratio));

            return rebateElement.Text.Trim().Contains(value);
        }

        public bool VerifyFootnotePresent(string text)
        {
            Driver.Wait(3, () => Driver.Instance.FindElements(By.CssSelector("[class='modal-body']")).Count != 0);

            IWebElement footnote = AllFootnotes.FirstOrDefault(x => x.Text.Trim() == text);

            if (footnote == null)
                return false;

            return true;
        }

        public bool VerifyEffectiveStartDate(DateTime date)
        {
            string currentDate = date.ToString("MM/dd/yyyy");
            IWebElement productRow = Driver.Instance.FindElement(By.CssSelector(_productRow));

            IWebElement effectiveDateElement = productRow.FindElement(EffectiveStartDateLocator);

            return effectiveDateElement.Text.Trim().Equals(currentDate);
        }

        public bool VerifyPOStatus(string status)
        {
            IWebElement productRow = Driver.Instance.FindElement(By.CssSelector(_productRow));
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
