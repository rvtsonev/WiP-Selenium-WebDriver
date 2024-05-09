using OpenQA.Selenium;

namespace Core.Elements
{
    public class ElementContainer
    {
        private ISearchContext _searchContext;
        private string _locatorParent;
        private const int _loops = 20;
        private const int _miliseconds = 250;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementContainer"/> class.
        /// </summary>
        /// <param name="searchContext">The search context to use for finding elements.</param>
        /// <param name="locatorParent">The CSS selector for the parent element.</param>
        /// <param name="locatorChild">The optional CSS selector for the child element.</param>
        public ElementContainer(ISearchContext searchContext, string locatorParent, string? locatorChild)
        {
            if (locatorChild != null)
            {
                locatorParent = locatorChild + locatorParent;
            }

            _searchContext = searchContext;
            _locatorParent = locatorParent;
        }

        /// <summary>
        /// Adds a sub-element to the current element container.
        /// </summary>
        /// <param name="elementLocator">The CSS selector for the sub-element.</param>
        /// <returns>An instance of <see cref="ElementContainer"/> representing the sub-element.</returns>
        private IList<IWebElement> ElementContainers()
        {
            for (int i = 1; i <= 3; i++)
            {
                try
                {
                    ConsoleLogger.Instance.Info($"Starting attempt [{i}] to define component: {_locatorParent}");
                    var container = _searchContext.FindElements(By.CssSelector(_locatorParent));
                    var count = container.Count;

                    if (count.Equals(0))
                    {
                        throw new NoSuchElementException();
                    }

                    ConsoleLogger.Instance.Debug($"Containers count: {count}");
                    ConsoleLogger.Instance.Debug($"Container defined. Attempt [{i}]");
                    return container;
                }
                catch (NoSuchElementException)
                {
                    ConsoleLogger.Instance.Warn($"Attempt [{i}] failed. Waiting and starting again");
                }
            }
            ConsoleLogger.Instance.Error($"Was not able to find component: {_locatorParent}");
            throw new NoSuchElementException($"Was not able to find component: {_locatorParent}");
        }

        /// <summary>
        /// Adds a sub-element to the current element container.
        /// </summary>
        /// <param name="elementLocator">The CSS selector for the sub-element.</param>
        /// <returns>An instance of <see cref="ElementContainer"/> representing the added sub-element.</returns>
        public ElementContainer AddSubElement(string elementLocator)
        {
            ConsoleLogger.Instance.Info($"Starting action to add a sub-element: {elementLocator}");
            return new ElementBuilder().Define(elementLocator, _searchContext, _locatorParent);
        }

        /// <summary>
        /// Defines a Element container by a sub-element text.
        /// </summary>
        /// <param name="childLocator">provide the common sub-element</param>
        /// <param name="text">text to filter-out the desired container</param>
        /// <returns></returns>
        /// <exception cref="NoSuchElementException"></exception>
        public SubElement DefineBySubElementText(string childLocator, string text)
        {
            for (int i = 0; i < _loops; i++)
            {
                try
                {
                    var element = ElementContainers().
                        First(x =>
                        {
                            try
                            {
                                // because if one of the parent elements is missing the child
                                // it will not iterate trough the rest child elements

                                if (x.FindElement(By.CssSelector(childLocator)).
                                    Text.Equals(text))
                                    return true;
                            }
                            catch (Exception)
                            {
                                //ignore
                            }

                            return false;
                        });

                    return new SubElement(element);
                }
                catch (Exception)
                {
                    ConsoleLogger.Instance.Debug($"Failed to located element with text '{text}' for locator '{_locatorParent}' with child locator '{childLocator}'");
                    ConsoleLogger.Instance.Warn($"Failed attempt number '{i + 1}'");
                    Thread.Sleep(_miliseconds);
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find element with text '{text}' for locator '{_locatorParent}'");
        }

        /// <summary>
        /// Defines an element container by a sub-element's attribute value.
        /// </summary>
        /// <param name="subElementLocator">The CSS selector for the sub-element.</param>
        /// <param name="attributeName">The name of the attribute to filter by.</param>
        /// <param name="attributeValue">The value of the attribute to filter by.</param>
        /// <returns>An instance of <see cref="SubElement"/> representing the container.</returns>
        /// <exception cref="NoSuchElementException">Thrown if the container cannot be found.</exception>
        public SubElement DefineBySubElementAttribute(string subElementLocator, string attributeName, string attributeValue)
        {
            for (int i = 0; i < _loops; i++)
            {
                try
                {
                    var element = ElementContainers().
                        First(x => x.FindElement(By.CssSelector(subElementLocator)).
                        GetAttribute(attributeName).Equals(attributeValue));

                    return new SubElement(element);
                }
                catch (Exception)
                {
                    ConsoleLogger.Instance.Debug($"Failed to located element with text '{attributeValue}' for locator '{_locatorParent}' with child locator '{subElementLocator}'");
                    ConsoleLogger.Instance.Warn($"Failed attempt number '{i + 1}'");
                    Thread.Sleep(_miliseconds);
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find element with text '{attributeValue}' for locator '{_locatorParent}'");
        }

        /// <summary>
        /// Defines an element container by a sub-element's attribute containing a value.
        /// </summary>
        /// <param name="subElementLocator">The CSS selector for the sub-element.</param>
        /// <param name="attributeName">The name of the attribute to filter by.</param>
        /// <param name="attributeValue">The value that the attribute should contain.</param>
        /// <returns>An instance of <see cref="SubElement"/> representing the container.</returns>
        /// <exception cref="NoSuchElementException">Thrown if the container cannot be found.</exception>
        public SubElement DefineBySubElementTextContaining(string subElementLocator, string text)
        {
            foreach (var element in ElementContainers())
            {
                var subElement = element.FindElement(By.CssSelector(subElementLocator));

                if (subElement.Text.Contains(text))
                {
                    ConsoleLogger.Instance.Debug($"Element containing text: {element.Text} <-- located");
                    ConsoleLogger.Instance.Debug($"Element id: {subElement}");
                    return new SubElement(element);
                }
                else
                {
                    ConsoleLogger.Instance.Debug($"Element text: {element.Text}");
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find element containing text '{text}' for locator '{_locatorParent}' with child locator '{subElementLocator}'");
        }

        /// <summary>
        /// Defines an element within the container by matching the specified attribute name and value.
        /// </summary>
        /// <param name="attributeName">The name of the attribute to match.</param>
        /// <param name="attributeValue">The value of the attribute to match.</param>
        /// <returns>A SubElement representing the matched element.</returns>
        /// <exception cref="NoSuchElementException">Thrown when the element with the specified attribute is not found within the container.</exception>
        public SubElement DefineByAttribute(string attributeName, string attributeValue)
        {
            for (int i = 0; i < _loops; i++)
            {
                try
                {
                    var element = ElementContainers().
                        First(x => x.GetAttribute(attributeName).Equals(attributeValue));

                    return new SubElement(element);
                }
                catch (Exception)
                {
                    ConsoleLogger.Instance.Debug($"Failed to located element with text '{attributeValue}' for locator '{_locatorParent}'");
                    ConsoleLogger.Instance.Warn($"Failed attempt number '{i + 1}'");
                    Thread.Sleep(_miliseconds);
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find element with attribute '{attributeName}'='{attributeValue}' for locator '{_locatorParent}'");
        }

        /// <summary>
        /// Defines an element within the container by matching the specified attribute name containing the provided value.
        /// </summary>
        /// <param name="attributeName">The name of the attribute to match.</param>
        /// <param name="attributeValue">The value that the attribute should contain.</param>
        /// <returns>A SubElement representing the matched element.</returns>
        /// <exception cref="NoSuchElementException">Thrown when the element with the specified attribute containing the provided value is not found within the container.</exception>
        public SubElement DefineByAttributeContaining(string attributeName, string attributeValue)
        {
            for (int i = 0; i < _loops; i++)
            {
                try
                {
                    var elements = ElementContainers();
                    var element = elements.
                        First(x => x.GetAttribute(attributeName).Contains(attributeValue));

                    return new SubElement(element);
                }
                catch (InvalidOperationException)
                {
                    ConsoleLogger.Instance.Debug($"Failed to located element with text '{attributeValue}' for locator '{_locatorParent}'");
                    ConsoleLogger.Instance.Warn($"Failed attempt number '{i+1}'");
                    Thread.Sleep(_miliseconds);
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find attribute '{attributeName}' containing '{attributeValue}' for locator '{_locatorParent}'");
        }

        /// <summary>
        /// Defines an element within the container by matching the specified text.
        /// </summary>
        /// <param name="text">The text to match.</param>
        /// <returns>A SubElement representing the matched element.</returns>
        /// <exception cref="NoSuchElementException">Thrown when the element with the specified text is not found within the container.</exception>
        public SubElement DefineByText(string text)
        {
            for (int i = 0; i < _loops; i++)
            {
                try
                {
                    var element = ElementContainers().
                        First(x => x.Text.Equals(text));

                    return new SubElement(element);
                }
                catch (Exception)
                {
                    ConsoleLogger.Instance.Debug($"----[DEBUG] Failed to located element with text '{text}' for locator '{_locatorParent}'");
                    ConsoleLogger.Instance.Warn($"----[WARNING] Failed attempt number '{i + 1}'");
                    Thread.Sleep(_miliseconds);
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find element with text '{text}' for locator '{_locatorParent}'");
        }

        /// <summary>
        /// Defines an element within the container by matching the specified text contained within the elements.
        /// </summary>
        /// <param name="text">The text to match within the elements.</param>
        /// <returns>A SubElement representing the matched element.</returns>
        /// <exception cref="NoSuchElementException">Thrown when the element with the specified text contained within the elements is not found within the container.</exception>
        public SubElement DefineByTextContaining(string text)
        {
            for (int i = 0; i < _loops; i++)
            {
                try
                {
                    var element = ElementContainers().
                        First(x => x.Text.Contains(text));

                    return new SubElement(element);
                }
                catch (Exception)
                {
                    ConsoleLogger.Instance.Debug($"Failed to located element with text '{text}' for locator '{_locatorParent}'");
                    ConsoleLogger.Instance.Warn($"Failed attempt number '{i + 1}'");
                    Thread.Sleep(_miliseconds);
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find element containing text '{text}' for locator '{_locatorParent}'");
        }

        /// <summary>
        /// Defines a sub-element within the container by matching the specified text and index.
        /// </summary>
        /// <param name="subElementLocator">The CSS selector for the sub-element.</param>
        /// <param name="index">The index of the sub-element.</param>
        /// <param name="text">The text to match within the sub-element.</param>
        /// <returns>A SubElement representing the matched sub-element.</returns>
        /// <exception cref="NoSuchElementException">Thrown when the sub-element with the specified text and index is not found within the container.</exception>

        public SubElement DefineBySubElementTextAndIndex(string subElementLocator, int index, string text)
        {
            for (int i = 0; i < _loops; i++)
            {
                try
                {
                    var element = ElementContainers().
                        First(x => x.FindElements(By.CssSelector(subElementLocator))[index].
                        Text.Equals(text));

                    return new SubElement(element);
                }
                catch (Exception)
                {
                    ConsoleLogger.Instance.Debug($"Failed to located element with text '{text}' for locator '{_locatorParent}'");
                    ConsoleLogger.Instance.Warn($"Failed attempt number '{i + 1}'");
                    Thread.Sleep(_miliseconds);
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find element with text '{text}' for locator '{_locatorParent}' with child locator '{subElementLocator}'");
        }

        /// <summary>
        /// Defines a sub-element within the container by matching the specified attribute containing the value and index.
        /// </summary>
        /// <param name="subElementLocator">The CSS selector for the sub-element.</param>
        /// <param name="index">The index of the sub-element.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="attributeValue">The value contained within the attribute.</param>
        /// <returns>A SubElement representing the matched sub-element.</returns>
        /// <exception cref="NoSuchElementException">Thrown when the sub-element with the specified attribute containing the value and index is not found within the container.</exception>
        public SubElement DefineBySubElementAttributeContainingAndIndex(string subElementLocator, int index, string attributeName, string attributeValue)
        {
            for (int i = 0; i < _loops; i++)
            {
                try
                {
                    var element = ElementContainers().
                        First(x => x.FindElements(By.CssSelector(subElementLocator))[index].
                        GetAttribute(attributeName).Contains(attributeValue));

                    return new SubElement(element);
                }
                catch (Exception)
                {
                    ConsoleLogger.Instance.Debug($"Failed to located element with text '{attributeValue}' for locator '{_locatorParent}'");
                    ConsoleLogger.Instance.Warn($"Failed attempt number '{i + 1}'");
                    Thread.Sleep(_miliseconds);
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find element containing attribute '{attributeName}'='{attributeValue}' for locator '{_locatorParent}' with child locator '{subElementLocator}'");
        }

        /// <summary>
        /// Gets all elements from the Relative locator and filters the desired element by text
        /// Afterwards it returns the index of that filtered element.
        /// </summary>
        /// <param name="elementText">text of the desired element.</param>
        /// <returns></returns>
        public int GetIndexOfBy(string elementText)
        {
            var index = ElementContainers().ToList().FindIndex(x => x.Text.Equals(elementText));
            ConsoleLogger.Instance.Info($"Index of '{elementText}' is '{index}'.");
            return index;
        }

        /// <summary>
        /// Gets all elements from the Relative locator and filters the desired elements attribute value
        /// Afterwards it returns the index of that filtered element.
        /// </summary>
        /// <param name="attributeName">name of the attribute to filter on</param>
        /// <param name="attributeValue">value of the attribute to filter by</param>
        /// <returns></returns>
        public int GetIndexOfBy(string attributeName, string attributeValue)
        {
            var index = ElementContainers().ToList().FindIndex(x => x.GetAttribute(attributeName).Equals(attributeValue));
            ConsoleLogger.Instance.Info($"Index of '{attributeValue}' is '{index}'.");
            return index;
        }

        /// <summary>
        /// Gets all sub-elements as a list and filters them
        /// </summary>
        /// <param name="subElementLocator"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public IList<SubElement> GetAllSubElementsByIndex(string subElementLocator, int index)
        {
            var subComponents = new List<SubElement>();

            ElementContainers().
                Select(x => x.FindElements(By.CssSelector(subElementLocator))[index]).
                ToList().ForEach(x => subComponents.Add(new SubElement(x)));

            return subComponents;
        }

        /// <summary>
        /// Gets all elements from the relative locator.
        /// </summary>
        /// <returns>A list of <see cref="SubElement"/> representing all elements in the container.</returns>
        public IList<SubElement> GetAllElements()
        {
            return ElementContainers().Select(x => new SubElement(x)).ToList();
        }

        /// <summary>
        /// Defines an element container by a sub-element's attribute containing a specific value.
        /// </summary>
        /// <param name="subElementLocator">The CSS selector for the sub-element.</param>
        /// <param name="attributeName">The name of the attribute to filter by.</param>
        /// <param name="attributeValue">The value that the attribute should contain.</param>
        /// <returns>An instance of <see cref="SubElement"/> representing the container.</returns>
        /// <exception cref="NoSuchElementException">Thrown if the container cannot be found.</exception>
        public SubElement DefineBySubElementAttributeContaining(string subElementLocator, string attributeName, string attributeValue)
        {
            for (int i = 0; i < _loops; i++)
            {
                try
                {
                    var element = ElementContainers().
                        First(x => x.FindElement(By.CssSelector(subElementLocator)).
                        GetAttribute(attributeName).Contains(attributeValue));

                    return new SubElement(element);
                }
                catch (Exception)
                {
                    ConsoleLogger.Instance.Debug($"Failed to located element to containg attribute '{attributeName}' containing '{attributeValue}' for locator '{_locatorParent}' with child locator '{subElementLocator}'");
                    ConsoleLogger.Instance.Debug($"Failed attempt number '{i + 1}'");
                    Thread.Sleep(_miliseconds);
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find element with text '{attributeValue}' for locator '{_locatorParent}'");
        }

        /// <summary>
        /// Defines a container by locating sub-elements with text containing the specified value.
        /// </summary>
        /// <param name="subElementLocator">The CSS selector for the sub-elements.</param>
        /// <param name="text">The text to search for within the sub-elements.</param>
        /// <returns>An instance of <see cref="SubElement"/> representing the container.</returns>
        /// <exception cref="NoSuchElementException">Thrown if the container cannot be found.</exception>
        public SubElement DefineBySubElementsTextContaining(string subElementLocator, string text)
        {
            foreach (var element in ElementContainers())
            {
                var subElements = element.FindElements(By.CssSelector(subElementLocator));

                foreach (var subElement in subElements)
                {
                    if (subElement.Text.Contains(text))
                    {
                        ConsoleLogger.Instance.Debug($"Element containing text: {element.Text} <-- located");
                        ConsoleLogger.Instance.Debug($"Element id: {subElement}");
                        return new SubElement(element);
                    }
                    else
                    {
                        ConsoleLogger.Instance.Debug($"Element text: {element.Text}");
                        ConsoleLogger.Instance.Debug($"SubElement text: {subElement.Text}");

                    }
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find element containing text '{text}' for locator '{_locatorParent}' with child locator '{subElementLocator}'");
        }

        /// <summary>
        /// Defines a container by locating sub-elements with an attribute containing the specified value.
        /// </summary>
        /// <param name="subElementLocator">The CSS selector for the sub-elements.</param>
        /// <param name="attributeName">The name of the attribute to search for.</param>
        /// <param name="attributeValue">The value that the attribute should contain.</param>
        /// <returns>An instance of <see cref="SubElement"/> representing the container.</returns>
        /// <exception cref="NoSuchElementException">Thrown if the container cannot be found.</exception>
        public SubElement DefineBySubElementsAttributeContaining(string subElementLocator, string attributeName, string attributeValue)
        {
            foreach (var element in ElementContainers())
            {
                var subElements = element.FindElements(By.CssSelector(subElementLocator));

                foreach (var subElement in subElements)
                {
                    if (subElement.GetAttribute(attributeName).Contains(attributeValue))
                    {
                        ConsoleLogger.Instance.Debug($"Attribute: {attributeName} containing attributeValue: {attributeValue} <-- located");
                        ConsoleLogger.Instance.Debug($"Element id: {subElement}");
                        return new SubElement(element);
                    }
                    else
                    {
                        ConsoleLogger.Instance.Debug($"Element text: {element.Text}");
                        ConsoleLogger.Instance.Debug($"SubElement text: {subElement.Text}");
                    }
                }
            }

            throw new NoSuchElementException($"----[ERROR] Was not able to find element with text '{attributeValue}' for locator '{_locatorParent}'");
        }
    }
}
