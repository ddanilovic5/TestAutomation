using OpenQA.Selenium;
using System.Collections.ObjectModel;
using WebDriver = SeleniumHelpers.Driver;

namespace SeleniumHelpers.Extensions
{
    public static class WebDriverExtensions
    {

        public static IWebElement FindElement(this IWebDriver driver, By by, string errorMessage)
        {
            try
            {
                return Driver.Instance.FindElement(by);
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException(errorMessage, ex);
            }
        }

        public static IWebElement FindElementNoWait(this IWebDriver driver, By by)
        {
            Driver.Instance.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

            IWebElement result = null;
            try
            {
                result = Driver.Instance.FindElement(by);
            }
            catch (Exception)
            {
                //  ignored
            }

            //  turn on implicit wait (back on)
            Driver.Instance.Manage().Timeouts().ImplicitWait = Driver.ImplicitWait;

            return result;
        }

        public static ReadOnlyCollection<IWebElement> FindElementsNoWait(this IWebDriver webDriver, By by)
        {
            //  turn off implicit wait
            Driver.Instance.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

            var result = Driver.Instance.FindElements(by);

            //  turn on implicit wait (back on)
            Driver.Instance.Manage().Timeouts().ImplicitWait = Driver.ImplicitWait;

            return result;
        }
    }
}
