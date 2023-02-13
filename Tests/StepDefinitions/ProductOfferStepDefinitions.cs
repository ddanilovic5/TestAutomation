﻿using PageObjects;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace Tests.StepDefinitions
{
    [Binding]
    public sealed class ProductOfferStepDefinitions
    {
        private LoginPage _loginPage;
        private MatrixPage _matrixPage;
        private ProductOfferPage _poPage;
        private CreateProductOfferDialog _poDialog;
        private CommonData _commonData;

        private string _pbfText = "Testing PB footnote " + Guid.NewGuid().ToString();
        private string _npbfText = "NPB Footnote " + Guid.NewGuid().ToString();

        public ProductOfferStepDefinitions(LoginPage loginpage, MatrixPage matrixPage, ProductOfferPage poPage, CreateProductOfferDialog poDialog, CommonData commonData)
        {
            _loginPage = loginpage;
            _matrixPage = matrixPage;
            _poPage = poPage;
            _poDialog = poDialog;
            _commonData = commonData;
        }

        [Given(@"Unique matrix is created")]
        public void UniqueMatrixIsCreated()
        {
            _loginPage.SignInAs("Admin");
            _commonData.MatrixName += DateTime.UtcNow.ToString("dd/MM/yy HH:mm:ss");

            _matrixPage = new MatrixPage();
            _matrixPage.ClickOnCreateNewMatrixButton();
            _matrixPage.TypeMatrixName(_commonData.MatrixName);
            _matrixPage.SelectManufacturer(_commonData.ManufacturerName);
            _matrixPage.SaveNewMatrixClick();

            _matrixPage.navigationBar.SignOut();
        }

        [Given(@"I login as user '([^']*)'")]
        public void GivenILoginAsUser(string user)
        {
            _loginPage.SignInAs(user);
        }

        [Given(@"I start to create new Product Offer")]
        public void GivenIStartToCreateNewProductOffer()
        {
            _matrixPage.OpenMatrixDetails(_commonData.MatrixName);

            _poPage.CreatePOButtonClick();
        }

        [Given(@"I select Product called ""([^""]*)""")]
        public void GivenISelectProductCalled(string productName)
        {
            _poDialog.SelectProduct(productName);
        }

        [Given(@"Effective date is current date")]
        public void GivenEffectiveDateIsCurrentDate()
        {
            DateTime currentDate = DateTime.Now.Date;
            _poDialog.AddEffectiveDate(currentDate);
        }

        [Given(@"Participant assignment is ""([^""]*)""")]
        public void GivenParticipantAssignmentIs(string participant)
        {
            _poDialog.AssignParticipants(participant);
        }

        [Given(@"Primary Rate type is ""([^""]*)""")]
        public void GivenPrimaryRateTypeIs(string primaryRate)
        {
            _poDialog.SelectPrimaryRateType(primaryRate);
        }

        [Given(@"Controlled (.*) has Bid with value of (.*)%")]
        public void GivenHasBidWithValueOf(string ration, int bidValue)
        {
            _poDialog.ControlledBid(bidValue.ToString());
        }

        [Given(@"Add unique PB Footnote with ""([^""]*)"" participants")]
        public void GivenAddUniquePBFootnoteWithParticipants(string participants)
        {
            _poDialog.TypePBFText(_pbfText);
            _poDialog.AddPBFParticipant(participants);
            _poDialog.SavePBF();
        }

        [Given(@"NPB Rate has bid of (.*)% for Access Restricted")]
        public void GivenNPBRateHasBidOfForAccessRestricted(int bidValue)
        {
            _poDialog.NPBRatesBid(bidValue.ToString());
        }

        [Given(@"Add unique NPB Footnote for ""([^""]*)""")]
        public void GivenAddUniqueNPBFootnoteFor(string participants)
        {
            _poDialog.TypeNonPBFText(_npbfText);
            _poDialog.AddNonPBFParticipant(participants);
            _poDialog.SaveNonPBF();
        }

        [When(@"I create Product Offer")]
        public void WhenICreateProductOffer()
        {
            _poDialog.ClickOnCreatePO();
        }

        [Then(@"Success message is shown")]
        public void ThenSuccessMessageIsShown()
        {
            _poPage.FetchPopupMessageByText().Should().Be("Successfully added123");
        }

        [Then(@"New Product Offer is shown in the list")]
        public void ThenNewProductOfferIsShownInTheList()
        {
            _poPage.VerifyProductIsInTheList(_commonData.ProductName).Should().BeTrue($"Previously created product - {_commonData.ProductName} is not shown in the product list.");
        }

        [Then(@"Primary Rate type should be ""([^""]*)""")]
        public void ThenPrimaryRateTypeShouldBe(string primaryRate)
        {
            _poPage.VerifyPrimaryRateTypes(primaryRate).Should().BeTrue($"Primary rate is not as expected. Expected to be - {primaryRate}");
        }

        [Then(@"CPC should be ""([^""]*)""")]
        public void ThenCPCShouldBe(string cpc)
        {
            _poPage.VerifyCPC(cpc).Should().BeTrue($"CPC value is not as expected. Expected to be - {cpc}");
        }

        [Then(@"(.*) in Controlled Rebates should be ""([^""]*)""")]
        public void ThenInControlledRebatesShouldBe(string ratio, string value)
        {
            _poPage.VerifyControlledRebates(ratio, value).Should().BeTrue($"Controlled rebate {ratio} doesn't have expected value of {value}");
        }

        [Then(@"Restricted NP Brand Rates should be ""([^""]*)""")]
        public void ThenRestrictedNPBrandRatesShouldBe(string value)
        {
            _poPage.VerifyRestrictedNPBRate(value).Should().BeTrue($"Restricted NPB value is not as expected. Expected to be: {value}");
        }

        [Then(@"PB Footnote text should be adequate")]
        public void ThenPBFootnoteTextShouldBeAdequate()
        {
            _poPage.ViewFootnotesButtonClick();
            _poPage.VerifyFootnotePresent(_pbfText).Should().BeTrue($"PB Footnote with text - '{_pbfText}' is not present.");
        }

        [Then(@"NPB Footnote text should be adequate")]
        public void ThenNPBFootnoteTextShouldBeAdequate()
        {
            _poPage.VerifyFootnotePresent(_npbfText).Should().BeTrue($"Non PB Footnote with text - '{_npbfText}' is not present.");
            _poPage.CloseFootnoteButtonClick();
        }

        [Then(@"Effective date should be current date")]
        public void ThenEffectiveDateShouldBeCurrentDate()
        {
            _poPage.VerifyEffectiveStartDate(DateTime.Now.Date).Should().BeTrue("Effective date is not current date.");
        }

        [Then(@"Status is ""([^""]*)""")]
        public void ThenStatusIs(string status)
        {
            _poPage.VerifyPOStatus(status).Should().BeTrue($"Product Offer status is not as expected. Expected to be - {status}");
        }
    }
}
