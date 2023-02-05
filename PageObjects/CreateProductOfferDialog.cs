using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumHelpers;
using SeleniumHelpers.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjects
{
    public class CreateProductOfferDialog
    {
        private By ValidDropdownSelectionLocator => By.CssSelector("select.ng-valid");
        private By DropdownListLocator => By.CssSelector(".dropdown-list li");
        private By DropdownSelectedValuesLocator => By.CssSelector(".multiselect-dropdown .selected-item");

        private IWebElement ProductDetailsSection => Driver.Instance.FindElement(By.XPath("//div[@id='productDetailsTitle']/.."));
        private IWebElement PrefferedBrandRatesSection => Driver.Instance.FindElement(By.XPath("//div[@id='preferredBrandRatesTitle']/.."));
        private IWebElement PBFSection => Driver.Instance.FindElement(By.XPath("//div[@id='preferredBrandFootnotesTitle']/.."));
        private IWebElement NonPreferredBrandRatesSection => Driver.Instance.FindElement(By.XPath("//div[@id='nonPreferredBrandRatesTitle']/.."));
        private IWebElement NPBFSection => Driver.Instance.FindElement(By.XPath("//div[@id='nonPreferredBrandFootnotesTitle']/.."));
        private IWebElement ProductDropdown => ProductDetailsSection.FindElement(By.CssSelector("[formcontrolname='productId']"));
        private ReadOnlyCollection<IWebElement> ProductList => ProductDropdown.FindElements(By.TagName("option"));
        private IWebElement CPCLabel => ProductDetailsSection.FindElement(By.XPath("//span[text() = 'CPC']"));
        private IWebElement CPCDrowdown => Driver.Instance.FindElement(By.CssSelector("[formcontrolname = 'cpc']"));
        private IWebElement EffectiveDate => Driver.Instance.FindElement(By.CssSelector("input[placeholder='Select date']"));
        private IWebElement ParticipantAssignmentDropdown => Driver.Instance.FindElement(By.CssSelector("[formcontrolname='participants']"));
        private ReadOnlyCollection<IWebElement> ParticipantValues => ParticipantAssignmentDropdown.FindElements(By.CssSelector(".dropdown-list .multiselect-item-checkbox"));
        private IWebElement PrimaryRateDropdown => Driver.Instance.FindElement(By.CssSelector("[formcontrolname='primaryRateTypes']"));
        private ReadOnlyCollection<IWebElement> PrimaryRateValues => PrimaryRateDropdown.FindElements(By.CssSelector(".dropdown-list .multiselect-item-checkbox"));
        private By Controlled11BidLocator => By.XPath("//*[@class='row mb-4']//*[text()='1:1']//.. /.. / ..//input"); // ask DEV team to add ids for divs around checkbox; text parameter can be dynamic (1:1, 1:M or 1:2)
        private IWebElement Controlled11Bid => PrefferedBrandRatesSection.FindElement(Controlled11BidLocator);
        private IWebElement AddNewPBFButton => PBFSection.FindElement(By.CssSelector("[type='button']"));
        private IWebElement PBFTextarea => PBFSection.FindElement(By.CssSelector(".fr-element"));
        private IWebElement PBFParticipantsDropdown => PBFSection.FindElement(By.CssSelector("[class='dropdown-btn']"));
        private IWebElement PBFSaveButton => PBFSection.FindElement(By.CssSelector("[type='submit']"));
        private ReadOnlyCollection<IWebElement> PBFParticipantValues => PBFSection.FindElements(By.CssSelector(".dropdown-list .multiselect-item-checkbox"));
        private By AccessRestrictedBidLocator => By.XPath("//*[@class='row mb-4']//*[text()='Access Restricted']//.. /.. / ..//input");
        private IWebElement AccessRestrictedBid => NonPreferredBrandRatesSection.FindElement(AccessRestrictedBidLocator);
        private IWebElement AccessRestrictedBidInput => Driver.Instance.FindElement(By.CssSelector("[formcontrolname='nonPreferredFormularyAccessRateRestricted']"));
        private IWebElement AddNewNPBFButton => NPBFSection.FindElement(By.CssSelector("[type='button']"));
        private IWebElement NPBFParticipantsDropdown => NPBFSection.FindElement(By.CssSelector("[class='dropdown-btn']"));
        private ReadOnlyCollection<IWebElement> NPBFParticipantValues => NPBFSection.FindElements(By.CssSelector(".dropdown-list .multiselect-item-checkbox"));
        private IWebElement NPBFSaveButton => PBFSection.FindElement(By.CssSelector("[type='submit']"));
        private IWebElement CreatePOButtom => Driver.Instance.FindElement(By.CssSelector(".modal-footer .btn-primary"));
        private IWebElement NPBFTextarea => NPBFSection.FindElement(By.CssSelector(".fr-element"));


        #region Methods
        public List<IWebElement> FetchAllProducts()
        {
            List<IWebElement> products = new List<IWebElement>();

            foreach (var product in ProductList)
            {
                if (product.Text.Trim() != "Select product")
                    products.Add(product);
            }

            return products;
        }

        public void SelectProduct(string productName)
        {
            Driver.Wait(3, () => ProductDropdown.Displayed);
            ProductDropdown.Click();

            IWebElement choosenProduct = FetchAllProducts().FirstOrDefault(x => x.Text.Trim() == productName);

            if (choosenProduct == null)
                throw new NoSuchElementException($"Product with name: \"{productName}\" was not found.");

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
                throw new NoSuchElementException($"Participant with name: \"{participantName}\" was not found.");

            participant.Click();

            IWebElement selectedItem = ParticipantAssignmentDropdown.FindElement(DropdownSelectedValuesLocator);
            ProductDetailsSection.Click(); // to remove dropdown

            if (!selectedItem.Text.Contains(participantName))
                throw new Exception($"Participant - \"{participantName}\" was not selected.");

        }

        public void SelectPrimaryRateType(string primaryRate)
        {
            PrimaryRateDropdown.Click();

            IWebElement rate = PrimaryRateValues.FirstOrDefault(x => x.Text.Trim() == primaryRate);
            if (rate == null)
                throw new NoSuchElementException($"Primary rate with name: \"{primaryRate}\" was not found.");

            rate.Click();

            IWebElement selectedRate = PrimaryRateDropdown.FindElement(DropdownSelectedValuesLocator);
            PrefferedBrandRatesSection.Click(); // to remove dropdown

            if (!selectedRate.Text.Contains(primaryRate))
                throw new Exception($"Primary rate - {primaryRate} was not selected.");
        }

        public void ControlledBid(string percentageValue)
        {
            // at this moment, this method is only for Controlled 1:1 bid

            Driver.Wait(3, () => Controlled11Bid.Displayed);
            Controlled11Bid.Click();

            var input = Driver.Instance.FindElement(RelativeBy.WithLocator(By.TagName("input")).Below(Controlled11BidLocator)); //change to css formcontroler name

            input.Clear();
            input.SendKeys(percentageValue);
        }

        public void TypePBFText(string text)
        {
            AddNewPBFButton.Click();
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(By.CssSelector("[class='textarea-container']")).Count != 0);
            Driver.Wait(3, () => PBFTextarea.Displayed);

            PBFTextarea.Clear();
            PBFTextarea.SendKeys(text);
        }

        public void AddPBFParticipant(string participantName)
        {
            Driver.Wait(3, () => PBFParticipantsDropdown.Displayed);

            PBFParticipantsDropdown.Click();
            IWebElement participant = PBFParticipantValues.FirstOrDefault(x => x.Text.Trim() == participantName);
            if (participant == null)
                throw new NoSuchElementException($"PBF Participant with name: \"{participantName}\" was not found.");

            participant.Click();
            IWebElement selectedItem = PBFParticipantsDropdown.FindElement(DropdownSelectedValuesLocator);
            PBFSection.Click(); // to remove dropdown

            if (!selectedItem.Text.Contains(participantName))
                throw new Exception($"PBF Participant - \"{participantName}\" was not selected.");
        }

        public void SavePBF()
        {
            Driver.Wait(3, () => PBFSaveButton.Displayed);
            PBFSaveButton.Click();
        }

        public void NPBRatesBid(string percentageValue)
        {
            Driver.Wait(3, () => AccessRestrictedBid.Displayed);
            AccessRestrictedBid.Click();

            Driver.Wait(3, () => AccessRestrictedBidInput.Displayed);
            AccessRestrictedBidInput.Clear();
            AccessRestrictedBidInput.SendKeys(percentageValue);
        }

        public void TypeNPBFText(string text)
        {
            Driver.Wait(3, () => AddNewNPBFButton.Displayed);
            AddNewNPBFButton.Click();
            Driver.Wait(3, () => PBFTextarea.Displayed);

            NPBFTextarea.Click();
            NPBFTextarea.Clear();
            NPBFTextarea.SendKeys(text);
        }

        public void AddNPBFParticipant(string participantName)
        {
            Driver.Wait(3, () => NPBFParticipantsDropdown.Displayed);

            NPBFParticipantsDropdown.Click();
            IWebElement participant = NPBFParticipantValues.FirstOrDefault(x => x.Text.Trim() == participantName);
            if (participant == null)
                throw new NoSuchElementException($"NPBF Participant with name: \"{participantName}\" was not found.");

            participant.Click();
            IWebElement selectedItem = NPBFParticipantsDropdown.FindElement(DropdownSelectedValuesLocator);
            NPBFSection.Click(); // to remove dropdown

            if (!selectedItem.Text.Contains(participantName))
                throw new Exception($"NPBF Participant - \"{participantName}\" was not selected.");

        }

        public void SaveNPBF()
        {
            Driver.Wait(3, () => NPBFSaveButton.Displayed);
            NPBFSaveButton.Click();
        }

        public void ClickOnCreatePO()
        {
            Driver.Wait(3, () => CreatePOButtom.Displayed);
            CreatePOButtom.Click();
        }
        #endregion

        #region Verifications

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
