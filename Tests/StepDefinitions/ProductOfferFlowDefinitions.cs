using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Tests.StepDefinitions
{
    [Binding]
    public class ProductOfferFlowDefinitions
    {
        private readonly ProductOfferStepDefinitions _poSteps;
        public ProductOfferFlowDefinitions(ProductOfferStepDefinitions poSteps)
        {
            _poSteps= poSteps;
        }

        [Given(@"Unique matrix is created with a new product offer in it")]
        public void GivenUniqueMatrixIsCreatedWithANewProductOfferInIt()
        {
            _poSteps.UniqueMatrixIsCreated(false);
            _poSteps.GivenIStartToCreateNewProductOffer();
            _poSteps.GivenISelectProductCalled(_poSteps.productName);
            _poSteps.GivenEffectiveDateIsCurrentDate();
            _poSteps.GivenParticipantAssignmentIs("Humana");
            _poSteps.GivenPrimaryRateTypeIs("Base Rebate");
            _poSteps.GivenHasBidWithValueOf("1:1", 10);
            _poSteps.GivenAddUniquePBFootnoteWithParticipants("Humana");
            _poSteps.WhenICreateProductOffer();
            _poSteps.ThenNewProductOfferIsShownInTheList();
        }

        [When(@"I send created product offer to Ascent for negotiation")]
        public void WhenISendCreatedProductOfferToAscentForNegotiation()
        {
            // open matrix then click on three dots
        }

        [Then(@"I will get confirmation that sending is successful")]
        public void ThenIWillGetConfirmationThatSendingIsSuccessful()
        {
            throw new PendingStepException();
        }

    }
}
