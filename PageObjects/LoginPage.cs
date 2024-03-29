﻿using OpenQA.Selenium;
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
                if(Driver.Instance.Url != _url) 
                {
                    GoTo();
                }
            }
            catch (Exception)
            {

                throw new Exception($"Page with url {_url} was not loaded.");
            }

            Driver.Wait(30, () => Driver.Instance.FindElementsNoWait(By.ClassName("auth-form--subtitle")).Count != 0);

            string email = "";

            switch (username)
            {
                case "Admin":
                    email = "ascent.matrix.admin@email.com";
                    password = "Test123!";
                    break;
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

            Driver.Wait(TimeSpan.FromSeconds(1));

            EmailField.Click();
            EmailField.Clear();
            EmailField.SendKeys(email);

            PasswordField.Click();
            PasswordField.Clear();
            PasswordField.SendKeys(password);
            SignInButton.Click();

            Driver.Wait(10, () => Driver.Instance.FindElementsNoWait(By.ClassName("main-nav")).Count != 0);
            Driver.Wait(10, () => Driver.Instance.FindElementsNoWait(By.ClassName("page-table")).Count != 0);
        }
    }
}
