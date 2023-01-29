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
        private By ValidDropdownSelectionLocator => By.CssSelector("select.ng-valid");
        private By DropdownListLocator => By.CssSelector(".dropdown-list li");
        private By DropdownSelectedValuesLocator => By.CssSelector(".multiselect-dropdown .selected-item");

        private IWebElement CreatePOButton => Driver.Instance.FindElement(CreatePOButtonLocator);
        private IWebElement ConfirmPopupButton => Driver.Instance.FindElement(By.CssSelector(".swal2-actions .swal2-confirm"));
        private IWebElement ProductDetailsSection => Driver.Instance.FindElement(By.XPath("//div[@id='productDetailsTitle']/.."));
        private IWebElement PrefferedBrandRatesSection => Driver.Instance.FindElement(By.XPath("//div[@id='preferredBrandRatesTitle']/.."));
        private IWebElement PreferredBrandFootnoteSection => Driver.Instance.FindElement(By.XPath("//div[@id='preferredBrandFootnotesTitle']/.."));
        private IWebElement NonPreferredBrandRatesSection => Driver.Instance.FindElement(By.XPath("//div[@id='nonPreferredBrandRatesTitle']/.."));
        private IWebElement NonPreferredBrandFootnoteSection => Driver.Instance.FindElement(By.XPath("//div[@id='nonPreferredBrandFootnotesTitle']/.."));
        private IWebElement ProductDropdown => ProductDetailsSection.FindElement(By.CssSelector("[formcontrolname='productId']"));
        private ReadOnlyCollection<IWebElement> ProductList => ProductDropdown.FindElements(By.TagName("option"));
        private IWebElement CPCLabel => ProductDetailsSection.FindElement(By.XPath("//span[text() = 'CPC']"));
        private IWebElement CPCDrowdown => Driver.Instance.FindElement(By.CssSelector("[formcontrolname = 'cpc']"));
        private IWebElement EffectiveDate => Driver.Instance.FindElement(By.CssSelector("input[placeholder='Select date']"));
        private IWebElement ParticipantAssignmentDropdown => Driver.Instance.FindElement(By.CssSelector("[formcontrolname='participants']"));
        private ReadOnlyCollection<IWebElement> ParticipantValues => ParticipantAssignmentDropdown.FindElements(By.CssSelector(".dropdown-list .multiselect-item-checkbox"));
        private IWebElement PrimaryRateDropdown => Driver.Instance.FindElement(By.CssSelector("[formcontrolname='primaryRateTypes']"));
        private ReadOnlyCollection<IWebElement> PrimaryRateValues => PrimaryRateDropdown.FindElements(By.CssSelector(".dropdown-list .multiselect-item-checkbox"));


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

        public List<IWebElement> FetchAllProducts()
        {
            List<IWebElement> products = new List<IWebElement>();

            foreach (var product in ProductList)
            {
                if (product.Text.Trim() != "Select product")
                    products.Add(product);
            }

            //SelectElement productDropdown = new SelectElement(ProductDropdown);

            //List<IWebElement> products = new List<IWebElement>(productDropdown.AllSelectedOptions);

            return products;
        }

        public void SelectProduct(string productName)
        {
            ProductDropdown.Click();

            IWebElement choosenProduct = FetchAllProducts().FirstOrDefault( x=> x.Text.Trim() == productName);

            if (choosenProduct == null)
                throw new NoSuchElementException($"Product with name: {productName} was not found.");

            choosenProduct.Click();
            ProductDropdown.SendKeys(Keys.Enter);

            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(ValidDropdownSelectionLocator).Count != 0);
        }

        public void AddEffectiveDate(DateTime dateTime)
        {
            string date = dateTime.ToString("MM/dd/yyyy");

            Driver.Wait(3, () => EffectiveDate.Displayed);
            EffectiveDate.SendKeys(date);
        }

        public void AssignParticipants(string participantName)
        {
            //should be implemented to receive array or list, in case multiple assignments
            ParticipantAssignmentDropdown.Click();

            IWebElement participant = ParticipantValues.FirstOrDefault(x => x.Text.Trim() == participantName);

            if (participant == null)
                throw new NoSuchElementException($"Participant with name: {participantName} was not found.");

            participant.Click();
            
            IWebElement selectedItem = ParticipantAssignmentDropdown.FindElement(DropdownSelectedValuesLocator);
            selectedItem.Click(); // to remove dropdown

            if (!selectedItem.Text.Contains(participantName))
                throw new Exception($"Participant - {participantName} was not selected.");
            
        }

        public void SelectPrimaryRateType(string primaryRate)
        {
            PrimaryRateDropdown.Click();

            IWebElement rate = PrimaryRateValues.FirstOrDefault(x => x.Text.Trim() == primaryRate);
            if(rate == null)
                throw new NoSuchElementException($"Primary rate with name: {primaryRate} was not found.");

            rate.Click();

            IWebElement selectedRate = PrimaryRateDropdown.FindElement(DropdownSelectedValuesLocator);
            selectedRate.Click(); // to remove dropdown

            if (!selectedRate.Text.Contains(primaryRate))
                throw new Exception($"Primary rate - {primaryRate} was not selected.");
        }


        #endregion

        #region Verification

        public bool VerifyCPCHasValue(string cpcValue)
        {
            Driver.Wait(3, () => CPCLabel.Displayed);

            SelectElement CPCSelectElement = new SelectElement(CPCDrowdown);
            string selectedOption = CPCSelectElement.SelectedOption.Text.Trim();

            return selectedOption == cpcValue;
        }


        #endregion
    }
}
