using OpenQA.Selenium.Chrome;

namespace SeleniumHelpers
{
    public static class BrowserOptionsManager
    {
        public static ChromeOptions GetChromeOptions()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments(
                "--start-maximized",
                "disable-popup-blocking",
                "--disable-application-cache");

            return chromeOptions;
        }

        public static ChromeOptions GetHeadlessChromeOptions()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            return chromeOptions;
        }
    }
}
