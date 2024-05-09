using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Core.Drivers
{
    public class WebDriverFactory
    {
        /// <summary>
        /// Creates a WebDriver instance based on the specified browser and configuration.
        /// </summary>
        /// <param name="browserName">The name of the browser (e.g., "chrome", "firefox", "edge").</param>
        /// <param name="configJson">The JSON configuration for the browser.</param>
        /// <returns>The WebDriver instance.</returns>
        public static IWebDriver CreateDriver(string browserName, string configFile)
        {
            DriverOptions options = GetDriverOptions(browserName, configFile);

            if (options != null)
            {
                return CreateDriver(options);
            }
            else
            {
                throw new ArgumentException($"Unsupported browser: {browserName}");
            }
        }

        /// <summary>
        /// Creates a WebDriver instance based on the specified options.
        /// </summary>
        /// <param name="options">The options for configuring the WebDriver.</param>
        /// <returns>The WebDriver instance.</returns>
        private static IWebDriver CreateDriver(DriverOptions options)
        {
            if (options is ChromeOptions chromeOptions)
            {
                return new WebDriverInitializer().InitializeChromeDriver(chromeOptions);
            }
            else if (options is FirefoxOptions firefoxOptions)
            {
                return new WebDriverInitializer().InitializeFirefoxDriver(firefoxOptions);
            }
            else if (options is EdgeOptions edgeOptions)
            {
                return new WebDriverInitializer().InitializeEdgeDriver(edgeOptions);
            }
            else
            {
                throw new WebDriverException("Unsupported driver options.");
            }
        }

        /// <summary>
        /// Retrieves the driver options based on the specified browser name and configuration JSON.
        /// </summary>
        /// <param name="browserName">The name of the browser (e.g., "chrome", "edge").</param>
        /// <param name="configJson">The JSON configuration for the browser.</param>
        /// <returns>The driver options.</returns>
        private static DriverOptions GetDriverOptions(string browserName, string jsonConfig)
        {
            dynamic config = JsonConvert.DeserializeObject(jsonConfig);

            switch (browserName.ToLower())
            {
                case "chrome":
                    return BrowserOptions.CreateChromeOptions(config.chrome);
                case "firefox":
                    return BrowserOptions.CreateFirefoxOptions(config.firefox);
                case "edge":
                    return BrowserOptions.CreateEdgeOptions(config.edge);
                default:
                    throw new NoSuchDriverException("Unsupported driver.");
            }
        }
    }
}