using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace Core.Drivers
{
    internal class WebDriverInitializer
    {
        /// <summary>
        /// Initializes a Chrome WebDriver instance with the provided options.
        /// </summary>
        /// <param name="options">The ChromeOptions to configure the WebDriver.</param>
        /// <returns>A Chrome WebDriver instance.</returns>
        internal IWebDriver InitializeChromeDriver(ChromeOptions options)
        {
            return new ChromeDriver(options);
        }

        /// <summary>
        /// Initializes an Edge WebDriver instance with the provided options.
        /// </summary>
        /// <param name="options">The EdgeOptions to configure the WebDriver.</param>
        /// <returns>An Edge WebDriver instance.</returns>
        internal IWebDriver InitializeEdgeDriver(EdgeOptions options)
        {
            return new EdgeDriver(options);
        }

        /// <summary>
        /// Initializes a Firefox WebDriver instance with the provided options.
        /// </summary>
        /// <param name="options">The FirefoxOptions to configure the WebDriver.</param>
        /// <returns>A Firefox WebDriver instance.</returns>
        internal IWebDriver InitializeFirefoxDriver(FirefoxOptions options)
        {
            return new FirefoxDriver(options);
        }
    }
}
