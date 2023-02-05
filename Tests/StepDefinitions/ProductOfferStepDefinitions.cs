using PageObjects;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace Tests.StepDefinitions
{
    [Binding]
    public sealed class ProductOfferStepDefinitions
    {
        private string _matrixName;
        private string _manufacturerName;
        private LoginPage _loginPage;
        private MatrixPage _matrixPage;
        private ProductOfferPage _poPage;
        private CreateProductOfferDialog _poDialog;

        [Given(@"Matrix called ""([^""]*)"" is already created")]
        public void GivenMatrixCalledIsAlreadyCreated(string testingMatrix)
        {
            _matrixName = testingMatrix;
        }

        [Given(@"Matrix's manufacturer is ""([^""]*)""")]
        public void GivenMatrixsManufacturerIs(string manufacturerName)
        {
            _manufacturerName = manufacturerName;
        }

        [Given(@"'([^']*)' is logged in")]
        public void GivenIsLoggedIn(string user)
        {
            _loginPage = new LoginPage();
            _loginPage.SignInAs(user);
        }

        [Given(@"User clicks on Create Product Offer button")]
        public void GivenUserClicksOnCreateProductOfferButton()
        {
            _matrixPage = new MatrixPage();
            _matrixPage.OpenMatrixDetails(_matrixName);
             
            _poPage= new ProductOfferPage();
            _poPage.CreatePOButtonClick();
        }

        [Given(@"Selects Product called ""([^""]*)""")]
        public void GivenSelectsProductCalled(string productName)
        {
            _poDialog = new CreateProductOfferDialog();
            _poDialog.SelectProduct(productName);
        }

        [Given(@"CPC is automatically filled with value ""([^""]*)""")]
        public void GivenCPCIsAutomaticallyFilledWithValue(string cpcValue)
        {
            _poDialog.VerifyCPCHasValue(cpcValue).Should().BeTrue($"CPC value is not as expected. Expected value - {cpcValue}");
        }

        [Given(@"Effective date is current date")]
        public void GivenEffectiveDateIsCurrentDate()
        {
            DateTime currentDate = DateTime.Now.Date;
            _poDialog.AddEffectiveDate(currentDate);
        }

        [Given(@"Participant assignment is ""([^""]*)""")]
        public void GivenParticipantAssignmentIs(string humana)
        {
            _poDialog.AssignParticipants(humana);
        }

        [Given(@"Primary Rate type is ""([^""]*)""")]
        public void GivenPrimaryRateTypeIs(string primaryRate)
        {
            _poDialog.SelectPrimaryRateType(primaryRate);
        }

        [Given(@"(.*) has Bid with value of (.*)%")]
        public void GivenHasBidWithValueOf(string p0, int bidValue)
        {
            _poDialog.ControlledBid(bidValue.ToString());
        }

        [Given(@"PB Footnote text is ""([^""]*)"" with ""([^""]*)"" participants")]
        public void GivenPBFootnoteTextIsWithParticipants(string pbfText, string participants)
        {
            _poDialog.TypePBFText(pbfText);
            _poDialog.AddPBFParticipant(participants);
            _poDialog.SavePBF();
        }

        [Given(@"NPB Rate has bid of (.*)% for Access Restricted")]
        public void GivenNPBRateHasBidOfForAccessRestricted(int bidValue)
        {
            _poDialog.NPBRatesBid(bidValue.ToString());
        }

        [Given(@"NPB Footnote text is ""([^""]*)"" with ""([^""]*)""")]
        public void GivenNPBFootnoteTextIsWithAllParticipants(string npbfText, string participants)
        {
            _poDialog.TypeNPBFText(npbfText);
            _poDialog.AddPBFParticipant(participants);
            _poDialog.SavePBF();
        }

        [Given(@"Product Offer is already created")]
        public void GivenProductOfferIsAlreadyCreated()
        {
            throw new PendingStepException();
        }

        [When(@"I create Product Offer")]
        public void WhenICreateProductOffer()
        {
            _poDialog.ClickOnCreatePO();
        }

        [Then(@"Succesfully added dialog is shown")]
        public void ThenSuccesfullyAddedDialogIsShown()
        {
            throw new PendingStepException();
        }

        [Then(@"New Product Offer is shown in the list")]
        public void ThenNewProductOfferIsShownInTheList()
        {
            throw new PendingStepException();
        }

        [Then(@"Primary Rate type should be ""([^""]*)""")]
        public void ThenPrimaryRateTypeShouldBe(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"CPC should be ""([^""]*)""")]
        public void ThenCPCShouldBe(string anticoagulant)
        {
            throw new PendingStepException();
        }

        [Then(@"(.*) in Controlled Rebates should be ""([^""]*)""")]
        public void ThenInControlledRebatesShouldBe(Decimal p0, string p1)
        {
            throw new PendingStepException();
        }

        [Then(@"Footnote text should be ""([^""]*)""")]
        public void ThenFootnoteTextShouldBe(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"Effective date should be current date")]
        public void ThenEffectiveDateShouldBeCurrentDate()
        {
            throw new PendingStepException();
        }

        [Then(@"Status is ""([^""]*)""")]
        public void ThenStatusIs(string @new)
        {
            throw new PendingStepException();
        }


        // =================================================

        [Then(@"""([^""]*)"" message is shown under the Effective date")]
        public void ThenMessageIsShownUnderTheEffectiveDate(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"""([^""]*)"" message is shown under the Ascent rates participants")]
        public void ThenMessageIsShownUnderTheAscentRatesParticipants(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"""([^""]*)"" message is shown under the Primary Rates type")]
        public void ThenMessageIsShownUnderThePrimaryRatesType(string p0)
        {
            throw new PendingStepException();
        }

    }
}
