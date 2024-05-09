using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Core.Drivers
{
    internal class BrowserOptions
    {
        /// <summary>
        /// Creates ChromeOptions based on the provided configuration.
        /// </summary>
        /// <param name="config">The dynamic configuration for ChromeOptions.</param>
        /// <returns>ChromeOptions instance.</returns>
        internal static ChromeOptions CreateChromeOptions(dynamic config)
        {
            ChromeOptions options = new ChromeOptions();
            if (config.arguments != null)
            {
                foreach (var arg in config.arguments)
                {
                    options.AddArgument(arg.ToString());
                }
            }
            return options;
        }

        /// <summary>
        /// Creates EdgeOptions based on the provided configuration.
        /// </summary>
        /// <param name="config">The dynamic configuration for EdgeOptions.</param>
        /// <returns>EdgeOptions instance.</returns>
        internal static EdgeOptions CreateEdgeOptions(dynamic config)
        {
            EdgeOptions options = new EdgeOptions();
            if (config.arguments != null)
            {
                foreach (var arg in config.arguments)
                {
                    options.AddArgument(arg.ToString());
                }
            }
            return options;
        }

        /// <summary>
        /// Creates FirefoxOptions based on the provided configuration.
        /// </summary>
        /// <param name="config">The dynamic configuration for FirefoxOptions.</param>
        /// <returns>FirefoxOptions instance.</returns>
        internal static FirefoxOptions CreateFirefoxOptions(dynamic config)
        {
            FirefoxOptions options = new FirefoxOptions();
            if (config.arguments != null)
            {
                foreach (var arg in config.arguments)
                {
                    options.AddArgument(arg.ToString());
                }
            }
            return options;
        }
    }
}
