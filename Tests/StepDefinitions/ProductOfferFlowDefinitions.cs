using NUnit.Framework;
using PageObjects;
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
        private readonly MatrixPage _matrixPage;
        private readonly ProductOfferPage _poPage;
        private readonly LoginPage _loginPage;
        private readonly DashboardPage _dashboardPage;

        private string _matrixName = "TA_Matrix";
        private string _popupMessage = "Send to Ascent was successful!";
        public ProductOfferFlowDefinitions(ProductOfferStepDefinitions poSteps, MatrixPage matrixPage, ProductOfferPage poPage, LoginPage loginPage, DashboardPage dashboardPage)
        {
            _poSteps= poSteps;
            _matrixPage = matrixPage;
            _poPage = poPage;
            _loginPage = loginPage;
            _dashboardPage = dashboardPage;
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

            _poPage.navigationBar.SignOut();
        }

        [When(@"I send created product offer to Ascent for negotiation")]
        public void WhenISendCreatedProductOfferToAscentForNegotiation()
        {
            // open matrix then click on three dots
            _matrixPage.OpenMatrixDetails(_matrixName);
            _poPage.SendToAscentAction();
        }

        [Then(@"I will get confirmation that sending is successful")]
        public void ThenIWillGetConfirmationThatSendingIsSuccessful()
        {
            Assert.IsTrue(_poPage.VerifyPopupMessageByText(_popupMessage), $"Confirmation message with text '{_popupMessage}' didn't appear!");
        }

        [Then(@"'([^']*)' will see sent product offer on his dashboard '([^']*)'")]
        public void ThenWillSeeSentProductOfferOnHisDashboard(string user, string p1)
        {
            _poPage.navigationBar.SignOut();

            _loginPage.SignInAs(user);
            _matrixPage.navigationBar.DashboardButtonClick();


            Assert.IsTrue(_dashboardPage.VerifySentProductOfferListed(_matrixName, "Manufacturer A"), $"Product offer for {_matrixName} is not listed for negotiation!");

            // TODO
            // Product Offer status will be in Review
        }
    }
}
