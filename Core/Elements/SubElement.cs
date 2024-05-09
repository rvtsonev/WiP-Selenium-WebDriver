using OpenQA.Selenium;

namespace Core.Elements
{
    public class SubElement
    {
        private readonly IWebElement _masterElement;

        /// <summary>
        /// Initializes a new instance of the SubElement class.
        /// </summary>
        /// <param name="masterElement">The master element.</param>
        public SubElement(IWebElement masterElement)
        {
            _masterElement = masterElement;
            ConsoleLogger.Instance.Debug($"Initializing SubElement for master element with id: {_masterElement}");
        }

        /// <summary>
        /// Gets a sub-element by matching its text content.
        /// </summary>
        /// <param name="locator">The CSS locator used to find the sub-element.</param>
        /// <param name="text">The text content to match.</param>
        /// <returns>A BasicElementActions object representing the sub-element.</returns>
        public BasicElementActions GetByText(string locator, string text)
        {
            ConsoleLogger.Instance.Debug($"Getting sub-element by text: '{text}' using locator: '{locator}'");
            return new BasicElementActions(
                _masterElement.FindElements(By.CssSelector(locator))
                    .First(x => x.Text.Equals(text)));
        }

        /// <summary>
        /// Gets a sub-element by matching its attribute value.
        /// </summary>
        /// <param name="locator">The CSS locator used to find the sub-element.</param>
        /// <param name="attribute">The name of the attribute.</param>
        /// <param name="text">The attribute value to match.</param>
        /// <returns>A BasicElementActions object representing the sub-element.</returns>
        public BasicElementActions GetBySubElementAttribute(string locator, string attribute, string text)
        {
            ConsoleLogger.Instance.Debug($"Getting sub-element by attribute: '{attribute}' with value: '{text}' using locator: '{locator}'");
            return new BasicElementActions(
                _masterElement.FindElements(By.CssSelector(locator))
                    .First(x => x.GetAttribute(attribute).Equals(text)));
        }

        /// <summary>
        /// Gets a sub-element by matching a part of its attribute value.
        /// </summary>
        /// <param name="locator">The CSS locator used to find the sub-element.</param>
        /// <param name="attribute">The name of the attribute.</param>
        /// <param name="text">The part of the attribute value to match.</param>
        /// <returns>A BasicElementActions object representing the sub-element.</returns>
        public BasicElementActions GetBySubElementAttributeContaining(string locator, string attribute, string text)
        {
            ConsoleLogger.Instance.Debug($"Getting sub-element by attribute: '{attribute}' containing value: '{text}' using locator: '{locator}'");
            return new BasicElementActions(
                _masterElement.FindElements(By.CssSelector(locator))
                    .First(x => x.GetAttribute(attribute).Contains(text)));
        }

        /// <summary>
        /// Gets BasicElementActions for the master element.
        /// </summary>
        /// <returns>BasicElementActions for the master element.</returns>
        public BasicElementActions GetActions()
        {
            ConsoleLogger.Instance.Debug($"Getting BasicElementActions for the master element");
            return new BasicElementActions(_masterElement);
        }

        /// <summary>
        /// Gets a sub-element by CSS locator.
        /// </summary>
        /// <param name="locator">The CSS locator used to find the sub-element.</param>
        /// <returns>BasicElementActions object representing the sub-element.</returns>
        public BasicElementActions GetSubElement(string locator)
        {
            var subElement = _masterElement.FindElement(By.CssSelector(locator));

            ConsoleLogger.Instance.Debug($"Found sub-element with id: {subElement}");
            return new BasicElementActions(subElement);
        }

        /// <summary>
        /// Gets a list of sub-elements by CSS locator.
        /// </summary>
        /// <param name="locator">The CSS locator used to find the sub-elements.</param>
        /// <returns>List of BasicElementActions objects representing the sub-elements.</returns>
        public IList<BasicElementActions> GetSubElements(string locator)
        {
            var subElements = _masterElement.FindElements(By.CssSelector(locator));
            var actions = new List<BasicElementActions>();

            foreach (var element in subElements)
            {
                ConsoleLogger.Instance.Debug($"Found sub-element with id: {element}");
                actions.Add(new BasicElementActions(element));
            }

            return actions;
        }
    }
}