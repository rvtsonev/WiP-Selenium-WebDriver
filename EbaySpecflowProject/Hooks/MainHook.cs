using BoDi;
using Core.Drivers;
using Core.Helpers;
using Core.Logger;
using OpenQA.Selenium;

namespace AmazonSpecflowProject.Hooks
{
    [Binding]
    internal class MainHook
    {
        [BeforeTestRun]
        internal static void BeforeTestRun()
        {
            ConsoleLogger.Instance.SetLogLevel(filterLevel: "debug");
        }

        [BeforeScenario]
        internal void BeforeScenario(IObjectContainer objectContainer)
        {
            string driverConfig = FileHelper.OpenJsonFile("EbaySpecflowProject", "webDriverConfig");
            IWebDriver driver = WebDriverFactory.CreateDriver(browserName: "edge", configFile: driverConfig);

            objectContainer.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        internal void AfterScenario(IObjectContainer objectContainer)
        {
            var driver = objectContainer.Resolve<IWebDriver>();
            driver.Dispose();
            driver.Quit();
        }
    }
}