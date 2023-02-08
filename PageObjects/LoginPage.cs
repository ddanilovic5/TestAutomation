using OpenQA.Selenium;
using SeleniumHelpers;
using SeleniumHelpers.Extensions;

namespace PageObjects
{
    public class LoginPage
    {
        private const string _url = "https://www.dev.ascentrebateportal.com/login";
        public void GoTo() => Driver.Instance.Navigate().GoToUrl(_url);
        private IWebElement EmailField => Driver.Instance.FindElement(By.Id("okta-signin-username"));
        private IWebElement PasswordField => Driver.Instance.FindElement(By.Id("okta-signin-password"));
        private IWebElement SignInButton => Driver.Instance.FindElement(By.Id("okta-signin-submit"));

        public void SignInAs(string username, string password = "Xfiles01a!")
        {
            try
            {
                GoTo();

            }
            catch (Exception)
            {

                throw new Exception($"Page with url {_url} was not loaded.");
            }
            Driver.Wait(30, () => Driver.Instance.FindElementsNoWait(By.ClassName("auth-form--subtitle")).Count != 0);

            string email = "";

            switch (username)
            {
                case "Neg":
                    email = "nega@ascentrebateportal.com";
                    break;
                case "CS":
                    email = "cons@ascentrebateportal.com";
                    break;
                case "MFG":
                    email = "manufactureraa@ascentrebateportal.com";
                    break;
            }

            EmailField.Click();
            EmailField.SendKeys(email);
            PasswordField.Click();
            PasswordField.SendKeys(password);
            SignInButton.Click();

            Driver.Wait(10, () => Driver.Instance.FindElementsNoWait(By.ClassName("main-nav")).Count != 0);
            Driver.Wait(10, () => Driver.Instance.FindElementsNoWait(By.ClassName("page-table")).Count != 0);
        }
    }
}
