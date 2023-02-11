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
    public class NavigationBar
    {
        private By FilterSidebarLocator => By.CssSelector("[class='filter-container']");
        private IWebElement DashboardButton => Driver.Instance.FindElement(By.CssSelector(".nav-bar-dashboard"));
        private IWebElement ProfilePicture => Driver.Instance.FindElement(By.CssSelector(".avatar-medium"));
        private IWebElement SignOutButton => Driver.Instance.FindElement(By.XPath("//*[text()='Sign out']"));

        public void DashboardButtonClick()
        {
            DashboardButton.Click();

            Driver.Wait(10, () => Driver.Instance.FindElementsNoWait(FilterSidebarLocator).Count != 0);
            Driver.Wait(10, () => Driver.Instance.FindElementsNoWait(By.CssSelector("[role='tablist']")).Count != 0);
        }

        public void SignOut()
        {
            Driver.Wait(TimeSpan.FromSeconds(1));

            ProfilePicture.Click();
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(By.CssSelector(".avatar-user-info")).Count != 0);

            SignOutButton.Click();

            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(By.Id("okta-signin-container")).Count != 0);
            Driver.Wait(3, () => Driver.Instance.FindElement(By.ClassName("navbar-brand")).Text == "Ascent rebate portal");
        }
    }
}
