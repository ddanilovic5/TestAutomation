using NUnit.Framework.Interfaces;
using NUnit.Framework;
using SeleniumHelpers;
using TechTalk.SpecFlow;

namespace Tests
{
    [Binding]
    public class TestBase
    {
        [BeforeScenario]
        public void TestInit()
        {
            Driver.InitializeDriver();
        }

        [AfterScenario(Order = 2)]
        public void TestCleanUp()
        {
            if(!TestCompletedWithoutErrors())
            {
                TakeScreenshot();
            }

            Driver.QuitDriver();
        }

        private void TakeScreenshot()
        {
            string fullTestName = TestContext.CurrentContext.Test.Name;
            
            if(fullTestName.Contains("("))
            {
                int indexOfBracket = fullTestName.IndexOf("(");
                fullTestName = fullTestName.Substring(0, indexOfBracket);
            }

            string screenshotFilePath = Driver.TakeScreenshot(fullTestName);
            if (screenshotFilePath != null) TestContext.AddTestAttachment(screenshotFilePath);
        }

        public bool TestCompletedWithoutErrors()
        {
            return TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Inconclusive) ||
                   TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Success);
        }
    }
}
