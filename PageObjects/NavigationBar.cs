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
        private IWebElement ProfilePicture => Driver.Instance.FindElement(By.CssSelector(".avatar-medium"));
        private IWebElement SignOutButton => Driver.Instance.FindElement(By.XPath("//*[text()='Sign out']"));

        public void SignOut()
        {
            ProfilePicture.Click();
            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(By.CssSelector(".avatar-user-info")).Count != 0);

            SignOutButton.Click();

            Driver.Wait(3, () => Driver.Instance.FindElementsNoWait(By.Id("okta-signin-container")).Count != 0);
            Driver.Wait(3, () => Driver.Instance.FindElement(By.ClassName("navbar-brand")).Text == "Ascent rebate portal");
        }
    }
}
