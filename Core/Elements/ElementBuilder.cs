using OpenQA.Selenium;

namespace Core.Elements
{
    public class ElementBuilder
    {
        /// <summary>
        /// Defines an element using the specified locator and search context.
        /// </summary>
        /// <param name="locator">The locator string used to find the element.</param>
        /// <param name="searchContext">The search context within which to find the element.</param>
        /// <param name="child">Optional: The child element locator.</param>
        /// <returns>An ElementContainer object representing the defined element.</returns>
        public ElementContainer Define(string locator, ISearchContext searchContext, string? child = null)
        {
            ConsoleLogger.Instance.Info($"Defining element with locator: '{locator}' and search context: '{searchContext}'");
            return new ElementContainer(searchContext, locator + " ", child);
        }
    }
}
