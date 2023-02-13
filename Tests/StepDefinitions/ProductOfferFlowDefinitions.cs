using FluentAssertions;
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
        private ProductOfferStepDefinitions _poSteps;
        private MatrixPage _matrixPage;
        private ProductOfferPage _poPage;
        private LoginPage _loginPage;
        private DashboardPage _dashboardPage;
        private CommonData _commonData;

        private string _popupMessage = "Send to Ascent was successful!";
        public ProductOfferFlowDefinitions(ProductOfferStepDefinitions poSteps, MatrixPage matrixPage, ProductOfferPage poPage, LoginPage loginPage, DashboardPage dashboardPage, CommonData commonData)
        {
            _poSteps= poSteps;
            _matrixPage = matrixPage;
            _poPage = poPage;
            _loginPage = loginPage;
            _dashboardPage = dashboardPage;
            _commonData = commonData;
        }

        [Given(@"I create New Product Offer")]
        public void GivenICreateNewProductOffer()
        {
            _poSteps.GivenIStartToCreateNewProductOffer();
            _poSteps.GivenISelectProductCalled(_commonData.ProductName);
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
            _poPage.SendToAscentAction();
        }

        [Then(@"I will get confirmation that sending is successful")]
        public void ThenIWillGetConfirmationThatSendingIsSuccessful()
        {
            _poPage.VerifySuccessPopup().Should().NotBeNull("because successful confirmation popup should appear after sending");
        }

        [Then(@"Status of that product offer will be '([^']*)'")]
        public void ThenStatusOfThatProductOfferWillBe(string status)
        {
            Assert.IsTrue(_poPage.VerifyPOStatus(status), $"PO Status was not as expected. Expected was {status}");
        }

        [Then(@"'([^']*)' will see sent product offer on his dashboard '([^']*)'")]
        public void ThenWillSeeSentProductOfferOnHisDashboard(string user, string accordion)
        {
            _poPage.navigationBar.SignOut();

            _loginPage.SignInAs(user);
            _matrixPage.navigationBar.DashboardButtonClick();

            if (accordion == "Assigned to Ascent")
            {
                _dashboardPage.AssignedToMeClick();
                _dashboardPage.AssignedToAscentClick();
            }

            Assert.IsTrue(_dashboardPage.VerifySentProductOfferListed(_commonData.MatrixName, "Manufacturer A"), $"Product offer for {_commonData.MatrixName} is not listed for negotiation!");
        }

        // --------------------------- TC2 ---------------------------

        [Given(@"I send created product offer to Ascent for negotiation")]
        public void GivenISendCreatedProductOfferToAscentForNegotiation()
        {
            WhenISendCreatedProductOfferToAscentForNegotiation();
        }

        [Given(@"I prepare for sending created product for approval to '([^']*)'")]
        public void GivenIPrepareForSendingCreatedProductForApprovalTo(string user)        
        {
            _matrixPage.OpenMatrixDetails(_commonData.MatrixName);
            _poPage.SendForCSApprovalClick();
            _poPage.SendSelectedToAscentAction(_commonData.ProductName);
        }

        [When(@"I send selected product offer for CS review")]
        public void WhenISendSelectedProductOfferForCSReview()
        {
            _poPage.CSReviewButtonClick();
        }
    }
}
