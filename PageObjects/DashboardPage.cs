using OpenQA.Selenium;
using SeleniumHelpers;
using SeleniumHelpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjects
{
    public class DashboardPage
    {
        private By AssignedToMeForNegLocator => By.Id("ngb-panel-0-header");
        private By LoaderLocator => By.CssSelector("[class='ag-overlay-loading-center']");

        private IWebElement AssignedToMeForNeg => Driver.Instance.FindElement(AssignedToMeForNegLocator);
        private IReadOnlyCollection<IWebElement> AllProductOffersForNegotiation => Driver.Instance.FindElements(By.CssSelector("[ref='eValue']"));

        #region Methods

        public void AssignedToMeForNegClick()
        {
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(AssignedToMeForNegLocator).Count != 0);

            AssignedToMeForNeg.Click();

            Driver.Wait(15, () => Driver.Instance.FindElementsNoWait(LoaderLocator).Count == 0);
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(By.CssSelector(".ag-cell-wrapper")).Count != 0);
        }

        #endregion

        #region Verifications

        public bool VerifySentProductOfferListed(string matrixName, string manufacturerName)
        {
            Driver.Wait(TimeSpan.FromSeconds(3));
           
            Driver.Wait(5, () => Driver.Instance.FindElements(By.CssSelector("[ref='eBodyViewport']")).Count != 0);
            
            // TA_Matrix (Manufacturer A)
            string poName = $"{matrixName} ({manufacturerName})";

            IWebElement productOffer = AllProductOffersForNegotiation.FirstOrDefault(x => x.Text.Trim().Contains(poName));

            if(productOffer == null) 
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
