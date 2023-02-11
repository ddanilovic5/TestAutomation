using PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Tests.Setup
{
    [Binding]
    public class ProductOfferSetup
    {
        private LoginPage loginPage = new LoginPage();
        private MatrixPage matrixPage = new MatrixPage();

        [AfterScenario("@DeleteMatrix", Order = 1)]
        public void DeleteMatrix()
        { 
            loginPage.SignInAs("Admin");
            matrixPage.SearchForMatrix("TA_Matrix");
            matrixPage.DeleteMatrix();
        }
    }
}
