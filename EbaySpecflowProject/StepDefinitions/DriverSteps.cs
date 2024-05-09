using BoDi;
using OpenQA.Selenium;

namespace AmazonSpecflowProject.StepDefinitions
{
    [Binding]
    internal class DriverSteps
    {
        private IWebDriver _driver;
        internal DriverSteps(IObjectContainer objectContainer)
        {
            _driver = objectContainer.Resolve<IWebDriver>();
        }

        [StepDefinition("user navigates to '(.*)'")]
        internal void UserNavigatesTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }
    }
}
