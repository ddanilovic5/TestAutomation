using PageObjects;
using TechTalk.SpecFlow;

namespace Tests.StepDefinitions
{
    [Binding]
    public sealed class ProductOfferStepDefinitions
    {
        private string _matrixName;
        private string _manufacturerName;
        private LoginPage _loginPage = new LoginPage();
        private MatrixPage _matrixPage = new MatrixPage();

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
            _loginPage.SignInAs(user);
        }

        [Given(@"User clicks on Create Product Offer button")]
        public void GivenUserClicksOnCreateProductOfferButton()
        {
            _matrixPage.OpenMatrixDetails(_matrixName);
        }

        [Given(@"Selects Product called ""([^""]*)""")]
        public void GivenSelectsProductCalled(string lemons)
        {
            throw new PendingStepException();
        }

        [Given(@"CPC is automatically filled with value ""([^""]*)""")]
        public void GivenCPCIsAutomaticallyFilledWithValue(string anticoagulant)
        {
            throw new PendingStepException();
        }

        [Given(@"Effective date is current date")]
        public void GivenEffectiveDateIsCurrentDate()
        {
            throw new PendingStepException();
        }

        [Given(@"Primary Rate type is ""([^""]*)""")]
        public void GivenPrimaryRateTypeIs(string p0)
        {
            throw new PendingStepException();
        }

        [Given(@"Participant assignment is ""([^""]*)""")]
        public void GivenParticipantAssignmentIs(string humana)
        {
            throw new PendingStepException();
        }

        [Given(@"(.*) has Bid with value of (.*)%")]
        public void GivenHasBidWithValueOf(Decimal p0, int p1)
        {
            throw new PendingStepException();
        }

        [Given(@"PB Footnote text is ""([^""]*)"" with ""([^""]*)"" participants")]
        public void GivenPBFootnoteTextIsWithParticipants(string p0, string humana)
        {
            throw new PendingStepException();
        }

        [Given(@"NPB Rate has bid of (.*)% for Access Restricted")]
        public void GivenNPBRateHasBidOfForAccessRestricted(int p0)
        {
            throw new PendingStepException();
        }

        [Given(@"NPB Footnote text is ""([^""]*)"" for All Participants")]
        public void GivenNPBFootnoteTextIsForAllParticipants(string p0)
        {
            throw new PendingStepException();
        }


        [Given(@"Product Offer is already created")]
        public void GivenProductOfferIsAlreadyCreated()
        {
            throw new PendingStepException();
        }


        [When(@"User clicks on Create button")]
        public void WhenUserClicksOnCreateButton()
        {
            throw new PendingStepException();
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
