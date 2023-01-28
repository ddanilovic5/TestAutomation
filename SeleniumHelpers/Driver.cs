using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SeleniumHelpers
{
    public enum Browsers { Chrome, ChromeHeadless, Firefox }
    public class Driver
    {
        public static IWebDriver Instance { get; set; }
        public static TimeSpan PageLoadTimeout = TimeSpan.FromSeconds(45);
        public static TimeSpan ImplicitWait = TimeSpan.FromSeconds(10);

        public static Browsers CurrentBrowser =>
            (Browsers)Enum.Parse(typeof(Browsers),
                Environment.GetEnvironmentVariable("WebDriverBrowser") ?? "Chrome");

        public static void InitializeDriver()
        {
            Instance?.Quit();

            switch (CurrentBrowser)
            {
                case Browsers.Chrome:
                    Instance = new ChromeDriver(BrowserOptionsManager.GetChromeOptions());
                    break;
                case Browsers.ChromeHeadless:
                    Instance = new ChromeDriver(BrowserOptionsManager.GetHeadlessChromeOptions());
                    break;
                case Browsers.Firefox:
                    throw new Exception("Not yet implemented");
                default:
                    throw new Exception("Browser was not found. Please check environment variable.");
            }

            Instance.Manage().Timeouts().PageLoad = PageLoadTimeout;
            Instance.Manage().Timeouts().ImplicitWait = ImplicitWait;
        }

        public static void Wait(TimeSpan timeSpan)
        {
            Thread.Sleep(timeSpan);
        }

        public static void Wait(double maxSecondsCount, Func<bool> expression)
        {
            while (maxSecondsCount > 0 && !expression())
            {
                Wait(TimeSpan.FromMilliseconds(300));
                maxSecondsCount -= 0.3;
            }
        }

        public static string TakeScreenshot(string testName)
        {
            try
            {
                var fileName = Path.Combine($"{Path.GetTempPath()}", $"{testName}_{DateTime.UtcNow:yyyyMMdd}.jpg");
                var ss = ((ITakesScreenshot)Instance).GetScreenshot();
                ss.SaveAsFile(fileName, ScreenshotImageFormat.Jpeg);
                return fileName;
            }
            catch (Exception e)
            {
                Log.Error($"Failed to take screenshot\n{e}");
                return null;
            }
        }

        public static void QuitDriver()
        {
            Instance?.Quit();
        }
    }
}
