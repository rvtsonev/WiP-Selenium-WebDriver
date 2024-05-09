# Test Automation Framework

This is a test automation framework designed to facilitate automated testing of web applications using Selenium WebDriver in C#.

## Overview

The Test Automation Framework consists of several components designed to streamline the process of writing and executing automated tests. Key components include:

- **Core Elements**: Provides classes and methods for interacting with web elements, such as clicking, typing text, and retrieving element attributes.
- **Element Builder**: A class for defining web elements using locators and search contexts.
- **Element Container**: A class for managing collections of web elements and performing actions based on their attributes and text.
- **SubElement**: Represents a single web element within a container, providing methods for interacting with it.
- **JsonSerializationHelper**: Provides methods for serializing and deserializing objects to and from JSON format.

## Packages Used

    coverlet.collector (Version 6.0.0)
    Microsoft.NET.Test.Sdk (Version 17.8.0)
    NUnit (Version 4.1.0)
    NUnit.Analyzers (Version 4.2.0)
    NUnit3TestAdapter (Version 4.5.0)
    Selenium.WebDriver (Version 4.20.0)

## Usage

To use the Test Automation Framework, follow these steps:

1. **Set Up Dependencies**: Ensure that you have Selenium WebDriver and Newtonsoft.Json dependencies installed in your project.
2. **Configure Framework**: Adjust configuration settings in the framework classes according to your project requirements.
3. **Write Tests**: Use the provided classes and methods to write automated tests for your web application.
4. **Execute Tests**: Run your tests using a testing framework such as NUnit or MSTest.

## Example

```csharp
// Example test using the Test Automation Framework
[TestFixture]
public class MyWebAppTests
{
    private IWebDriver _driver;
    
    [SetUp]
    public void SetUp()
    {
        ConsoleLogger.Instance.SetLogLevel(filterLevel: "debug");
        string driverConfig = FileHelper.OpenJsonFile("MyWebAppTests", "webDriverConfig");
        _driver = WebDriverFactory.CreateDriver(browserName: "edge", configFile: driverConfig);
        // Additional setup code...
    }

    [Test]
    // Example of how to create elements and do perform actions on them
    public void TestLogin()
    {
        _driver.Navigate().GoToUrl("https://example.com/login");

        new ElementBuilder().
            Define(ElementLocator.Username, _driver).
            DeffineByAttribute("placeholder", "Username").
            GetActions().
            SendText("my_username");

        new ElementBuilder().
            Define(ElementLocator.Password, _driver).
            DeffineByAttribute("placeholder", "Password").
            GetActions().
            SendText("my_password");

        new ElementBuilder().
            Define(ElementLocator.LoginButton, _driver).
            DeffineByAttribute("type", "button").
            GetActions().
            Click();

        // Assert login success...
    }

    [TestCase(ElementLocator.Username, "placeholder", "Username")]
    [TestCase(ElementLocator.Password, "placeholder", "Password")]
    [TestCase(ElementLocator.LoginButton, "type", "button")]
    // Example of how you can reduce code repetition and validate multiple elements
    public void ElementIsDisplayed(string elementLocator, string attribute, string attributeValue)
    {
       var isDisplayed = new ElementBuilder().
           Define(element, _driver).
           DefineByAttribute(attribute, attributeValue).
           GetActions().
           IsDisplayed();

       Assert.That(isDisplayed);
    }

    [TearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}
```

```csharp
internal class ElementLocator
{
    internal readonly static string Username = "#username";
    internal readonly static string Password = "#password";
    internal readonly static string LoginButton = "#login-button";
}

```

```json
// webDriverConfig.json
{
  "edge": {
    "useChromium": true,
    "arguments": [
      "--window-size=1920,1080"
    ]
  }
}
```

## About me
[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/radoslav-t-916aa2130/)

[![GitHub Profile](https://img.shields.io/github/followers/rvtsonev?label=Follow&style=social)](https://github.com/rvtsonev)
