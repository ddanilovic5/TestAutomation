﻿using NUnit.Framework.Interfaces;
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
            var screenshot = Driver.TakeScreenshot(TestContext.CurrentContext.Test.Name.Trim('('));
            if (screenshot != null) TestContext.AddTestAttachment(screenshot);
        }

        public bool TestCompletedWithoutErrors()
        {
            return TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Inconclusive) ||
                   TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Success);
        }
    }
}
