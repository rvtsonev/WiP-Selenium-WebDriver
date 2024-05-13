# Example Framework with .NET Core and Selenium WebDriver

This project aims to demonstrate an example framework built using .NET Core and Selenium WebDriver. The framework is designed to reduce code repetition and add intelligent waits for the elements, thereby improving test stability and maintainability.

## Disclamer
With the current state of the framework it is fully usable.
But keep in mind that the framework is not fully developed. It is still Work-in-Progress. 

## Please if you have any suggestions or you've found bad logic open a branch inside the repository using this name format:

 - [IDEA] <TICKET_NAME> for suggestions/missing implementations
 - [BUG] <TICKET_NAME> for bad logic/issues
 
 ##### feel free to use simple .txt files to describe the suggestions/issue

## Features

    Code Reusability: 
        The framework encourages code reusability through encapsulation and abstraction of common Selenium actions.

    Intelligent Waits: 
        Utilizes explicit and implicit waits to ensure that tests wait only as long as necessary for elements to be interactable.

    Ease of Use: 
        Provides easy-to-use methods for common actions such as clicking, sending text, and retrieving element attributes.

    Logging: 
        Integrated logging using a custom logger to provide detailed information about test execution.

    Helpers:
        All kinds of useful helpers are added here that can be further used 
        between different test project in the solution.

## Project Structure

The project consists of the following projects:

    Core: 
    -- the main framework that implements all Selenium WebDriver logic.

    EbaySpecflowProject:
    -- an example project that uses Specflow and Core.csproj to illustrate how the framework could reduce code repetition and ease test creation for test engineers.
