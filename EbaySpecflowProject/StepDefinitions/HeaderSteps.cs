using BoDi;
using Core.Elements;
using EbaySpecflowProject.Locators;
using OpenQA.Selenium;

namespace EbaySpecflowProject.StepDefinitions
{
    [Binding]
    internal class HeaderSteps
    {
        private IWebDriver _driver;
        internal HeaderSteps(IObjectContainer objectContainer)
        {
            _driver = objectContainer.Resolve<IWebDriver>();
        }

        [StepDefinition(@"user is able to see Ebay logo in the header element displayed")]
        internal void ThenUserIsAbleToSeeEbayLogoInTheHeaderElementDisplayed()
        {
            var isDisplayed = new ElementBuilder().
                Define(ElementLocator.Header, _driver).
                AddSubElement(ElementLocator.Table).
                AddSubElement(ElementLocator.TBody).
                AddSubElement(ElementLocator.Tr).
                AddSubElement(ElementLocator.Td).
                AddSubElement(ElementLocator.H1).
                AddSubElement(ElementLocator.A).
                AddSubElement(ElementLocator.Img).
                DefineByAttribute("alt", "eBay Home").
                GetActions().
                IsDisplayed();

            Assert.That(isDisplayed);
        }

    }
}
