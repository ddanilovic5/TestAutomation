using OpenQA.Selenium;
using SeleniumHelpers;
using SeleniumHelpers.Extensions;
using OpenQA.Selenium.Support;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

namespace PageObjects
{
    public class ProductOfferPage
    {
        private const string _url = "/product-offers";
        public void GoTo() => Driver.Instance.Navigate().GoToUrl(_url);

        private By CreatePOButtonLocator => By.CssSelector(".product-offer-row .btn-primary");
        private By NewOfferPopupLocator => By.CssSelector("div.swal2-popup");

        private IWebElement CreatePOButton => Driver.Instance.FindElement(CreatePOButtonLocator);
        private IWebElement ConfirmPopupButton => Driver.Instance.FindElement(By.CssSelector(".swal2-actions .swal2-confirm"));


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

        #endregion

        #region Verification

       

        #endregion
    }
}
