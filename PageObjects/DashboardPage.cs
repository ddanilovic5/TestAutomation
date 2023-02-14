using OpenQA.Selenium;
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
    public class DashboardPage
    {
        private By AssignedToMeForNegLocator => By.XPath("//*[text() = 'Assigned to me for negotiation ']");
        private By AssignedToAscentLocator => By.XPath("//*[text() = 'Assigned to Ascent ']");
        private By AssignedToMeLocator => By.XPath("//*[text() = 'Assigned to me ']");
        private By LoaderLocator => By.CssSelector("[class='ag-overlay-loading-center']");

        private IWebElement AssignedToMeForNeg => Driver.Instance.FindElement(AssignedToMeForNegLocator);
        private IWebElement AssignedToAscent => Driver.Instance.FindElement(AssignedToAscentLocator);
        private IWebElement AssignedToMe => Driver.Instance.FindElement(AssignedToMeLocator);
        private ReadOnlyCollection<IWebElement> AllAssignedProductOffers => Driver.Instance.FindElements(By.CssSelector("[ref='eValue']"));

        #region Methods

        public void AssignedToMeForNegClick()
        {
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(AssignedToMeForNegLocator).Count != 0);

            AssignedToMeForNeg.Click();

            Driver.Wait(15, () => Driver.Instance.FindElementsNoWait(LoaderLocator).Count == 0);
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(By.CssSelector(".ag-cell-wrapper")).Count != 0);
        }

        public void AssignedToAscentClick()
        {
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(AssignedToAscentLocator).Count != 0);

            AssignedToAscent.Click();

            Driver.Wait(15, () => Driver.Instance.FindElementsNoWait(LoaderLocator).Count == 0);
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(By.CssSelector(".ag-cell-wrapper")).Count != 0);
        }

        public void AssignedToMeClick()
        {
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(AssignedToMeLocator).Count != 0);

            AssignedToMe.Click();

            Driver.Wait(15, () => Driver.Instance.FindElementsNoWait(LoaderLocator).Count == 0);
            Driver.Wait(5, () => Driver.Instance.FindElementsNoWait(By.CssSelector(".ag-cell-wrapper")).Count != 0);

        }
        #endregion

        #region Verifications

        public ReadOnlyCollection<IWebElement> FetchSentProductOfferListed()
        {
            Driver.Wait(TimeSpan.FromSeconds(3));
            Driver.Wait(5, () => Driver.Instance.FindElements(By.CssSelector("[ref='eBodyViewport']")).Count != 0);
            
            return AllAssignedProductOffers;
        }

        #endregion
    }
}
