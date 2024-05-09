using BoDi;
using Core.Elements;
using EbaySpecflowProject.Locators;
using OpenQA.Selenium;

namespace EbaySpecflowProject.StepDefinitions
{
    [Binding]
    internal class PopularCategoriesSteps
    {
        private IWebDriver _driver;
        internal PopularCategoriesSteps(IObjectContainer objectContainer)
        {
            _driver = objectContainer.Resolve<IWebDriver>();
        }

        [StepDefinition(@"user is able to see popular category with name '(.*)' displayed")]
        internal void ThenUserIsAbleToSeePopularCategoryWithNameDisplayed(string name)
        {
            // here we define the Container for Popular Categories
            // because there are two identical Containers for the Carousels we need to distinguish them by something
            // in this case by Class containing value 
            var carouselContainer = new ElementBuilder().
                Define(ElementLocator.Div, _driver).
                DefineByAttributeContaining("class", "carousel--slides carousel--peek").
                // [class*='carousel--slides carousel--peek'] could be added to ElementLocator.cs file
                // or even better in a separate .cs file responsible only for the Carousels
                GetActions().
                WebElement;

            // now we pass the defined container in the ElementBuilder instead of the WebDriver.
            // this ensures that we work only in the context of the container
            var carouselItemContainer = new ElementBuilder().Define(ElementLocator.Li, carouselContainer).
                // now we filter all 'li' elements by sub-element with text
                DefineBySubElementText(ElementLocator.H3, name).
                GetActions();

            Assert.That(carouselItemContainer.IsDisplayed());
        }

        [StepDefinition(@"popular category with name '(.*)' has image '(.*)'")]
        public void ThenCarouselWithTextHasImageWithId(string name, string image)
        {
            var carouselContainer = new ElementBuilder().
                Define(ElementLocator.Div, _driver).
                DefineByAttributeContaining("class", "carousel--slides carousel--peek").
                GetActions().
                WebElement;

            var carouselItemImg = new ElementBuilder().
                Define(ElementLocator.Li, carouselContainer).
                // here we define the Li element by its sub-element h3 filtered by the desired text
                DefineBySubElementText(ElementLocator.H3, name).
                // now the Li element is defined 
                // and we get an other sub-element of that container - img
                GetSubElement(ElementLocator.Img).
                // here we extract the value of the desired attribute
                GetAttribute("src");

            Assert.That(carouselItemImg.Contains(image));
        }

    }
}
