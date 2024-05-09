using OpenQA.Selenium;

namespace Core.Elements
{
    public class BasicElementActions
    {
        public IWebElement WebElement;

        /// <summary>
        /// Initializes a new instance of the BasicElementActions class.
        /// </summary>
        /// <param name="webElement">The web element to perform actions on.</param>
        public BasicElementActions(IWebElement webElement)
        {
            if (webElement == null)
            {
                throw new ArgumentNullException(nameof(webElement));
            }
            WebElement = webElement;
            ConsoleLogger.Instance.Info($"Initializing BasicElementActions for element with id: {WebElement}");
        }

        /// <summary>
        /// Gets the text of the element.
        /// </summary>
        /// <returns>The text of the element.</returns>
        public string GetText()
        {
            ConsoleLogger.Instance.Info($"Starting action to GetText");
            var text = WebElement.Text;
            ConsoleLogger.Instance.Debug($"text is: {text}");

            return text;
        }

        /// <summary>
        /// Checks if the element is displayed.
        /// </summary>
        /// <returns>True if the element is displayed; otherwise, false.</returns>
        public bool IsDisplayed()
        {
            ConsoleLogger.Instance.Info($"Starting action to get IsDisplayed");
            bool isDisplayed = WebElement.Displayed;
            ConsoleLogger.Instance.Debug($"IsDisplayed is: {isDisplayed}");

            return isDisplayed;
        }

        /// <summary>
        /// Checks if the element is enabled.
        /// </summary>
        /// <returns>True if the element is enabled; otherwise, false.</returns>
        public bool IsEnabled()
        {
            ConsoleLogger.Instance.Info($"Starting action to get IsEnabled");
            bool isEnabled = WebElement.Enabled;
            ConsoleLogger.Instance.Debug($"IsEnabled is: {isEnabled}");

            return isEnabled;
        }

        /// <summary>
        /// Clicks on an element.
        /// </summary>
        public void Click()
        {
            ConsoleLogger.Instance.Info("Starting action to Click");

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    WebElement.Click();
                    break;
                }
                catch (ElementClickInterceptedException ex)
                {
                    ConsoleLogger.Instance.Warn($"Failed to click at attempt number [{i + 1}] - {ex.GetType().Name}");
                    Thread.Sleep(1000);
                }
            }

            ConsoleLogger.Instance.Info("Action to Click: successful");
        }

        /// <summary>
        /// Clears text of an element.
        /// </summary>
        public void Clear()
        {
            ConsoleLogger.Instance.Info($"Starting action to Clear");
            WebElement.Clear();
            ConsoleLogger.Instance.Debug($"Action to Clear: successful");
        }

        /// <summary>
        /// Sends text to element.
        /// </summary>
        /// <param name="text">text that is going to be send.</param>
        public void SendText(string text)
        {
            ConsoleLogger.Instance.Info($"Starting action to SendText: {text}");
            WebElement.SendKeys(text);
            ConsoleLogger.Instance.Debug($"Action to SendText: successful");
        }

        /// <summary>
        /// Sends text to element one char at a time
        /// </summary>
        /// <param name="text">text that is going to be send.</param>
        public void SendTextCharByChar(string text)
        {
            ConsoleLogger.Instance.Info($"Starting action to SendText: {text}");
            for (int i = 0; i < text.Length; i++)
            {
                var chars = text.ToCharArray();
                WebElement.SendKeys(chars[i].ToString());
            }
            ConsoleLogger.Instance.Debug($"Action to SendText: successful");
        }

        /// <summary>
        /// Gets the value of a desired element attribute
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns>The value of the attribute.</returns>
        public string GetAttribute(string attribute)
        {
            ConsoleLogger.Instance.Debug($"Starting action to GetAttribute: {attribute}");
            var value = WebElement.GetAttribute(attribute);
            ConsoleLogger.Instance.Debug($"GetAttribute value is: {value}");

            return value;
        }

        public void ClearByAttributeValueLength()
        {
            ConsoleLogger.Instance.Debug($"Starting action to ClearByAttributeValueLength");

            WebElement.GetAttribute("value").
                ToList().
                ForEach(x => WebElement.SendKeys(Keys.Backspace));

            ConsoleLogger.Instance.Debug($"Action to ClearByAttributeValueLength: successful");
        }

        /// <summary>
        /// Gets the value of a desired element css
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns>The value of the css.</returns>
        public string GetCssValue(string cssValue)
        {
            ConsoleLogger.Instance.Debug($"Starting action to GetCssValue({cssValue})");
            var value = WebElement.GetCssValue(cssValue);
            ConsoleLogger.Instance.Debug($"Action to GetCssValue: successful");
            return value;
        }

        /// <summary>
        /// Presses the specified keyboard key.
        /// </summary>
        /// <param name="key">The key to be pressed. Currently supports "Enter".</param>
        /// <exception cref="NotImplementedException">Thrown when the provided key is not implemented.</exception>
        public void PressKey(string key)
        {
            ConsoleLogger.Instance.Debug($"Starting action to Press keyboard key: {key}");

            switch (key)
            {
                case "Enter":
                    WebElement.SendKeys(Keys.Enter);
                    ConsoleLogger.Instance.Debug($"Action to Press keyboard key: successful");
                    break;
                default:
                    throw new NotImplementedException($"Press key {key} is not implemented");
            }
        }

        /// <summary>
        /// Checks if the element has the specified attribute.
        /// </summary>
        /// <param name="attribute">The name of the attribute to check.</param>
        /// <returns>True if the element has the specified attribute; otherwise, false.</returns>
        public bool HasAttribute(string attribute)
        {
            ConsoleLogger.Instance.Debug($"Starting action to GetAttribute: {attribute}");

            if (WebElement.GetAttribute(attribute) != null)
            {
                return true;
            }

            ConsoleLogger.Instance.Debug($"GetAttribute value is: {WebElement.GetAttribute(attribute)}");

            return false;
        }
    }
}
