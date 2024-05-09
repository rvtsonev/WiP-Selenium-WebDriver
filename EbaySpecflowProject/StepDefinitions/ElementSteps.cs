using BoDi;
using Core.Elements;
using OpenQA.Selenium;

namespace EbaySpecflowProject.StepDefinitions
{
    [Binding]
    internal class ElementSteps
    {
        private IWebDriver _driver;
        internal ElementSteps(IObjectContainer objectContainer)
        {
            _driver = objectContainer.Resolve<IWebDriver>();
        }

        [StepDefinition(@"user is able to see element '(.*)' with attribute '(.*)' with value '(.*)' displayed")]
        internal void ThenUserIsAbleToSeeElementWithAttributeWithValueDisplayed(string element, string attribute, string value)
        {
            var isDisplayed = new ElementBuilder().
                Define(element, _driver).
                DefineByAttribute(attribute, value).
                GetActions().
                IsDisplayed();

            Assert.That(isDisplayed,
                $"ERROR: Expected to see element '{element}' with attribute '{attribute}' with value '{value}' displayed.");
        }
    }
}
