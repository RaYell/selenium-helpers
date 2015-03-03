﻿namespace Selenium.WebDriver.Extensions.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    
    /// <summary>
    /// Web driver extensions.
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Searches for DOM element using given selector.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium selector.</param>
        /// <returns>The first DOM element matching given JavaScript query selector</returns>
        public static WebElement FindElement(this IWebDriver driver, ISelector by)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver");
            }

            if (by == null)
            {
                throw new ArgumentNullException("by");
            }

            var runner = (IRunner)Activator.CreateInstance(by.RunnerType);
            var results = runner.Find<IEnumerable<IWebElement>>(driver, by);
            if (results == null)
            {
                throw new NoSuchElementException("No element found for selector: " + by.Selector);
            }

            var list = results.ToList();
            if (list.Count > 0)
            {
                return new WebElement(list.First(), by);
            }

            throw new NoSuchElementException("No element found for selector: " + by.Selector);
        }

        /// <summary>
        /// Searches for DOM elements using given selector.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium Sizzle selector.</param>
        /// <returns>The DOM elements matching given JavaScript query selector.</returns>
        public static ReadOnlyCollection<WebElement> FindElements(this IWebDriver driver, ISelector by)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver");
            }

            if (by == null)
            {
                throw new ArgumentNullException("by");
            }

            var runner = (IRunner)Activator.CreateInstance(by.RunnerType);
            var results = runner.Find<IEnumerable<IWebElement>>(driver, by)
                .Select((value, index) => new WebElement(value, by, index)).ToList();
            return new ReadOnlyCollection<WebElement>(results);
        }

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="script">The script to be </param>
        /// <param name="args">The arguments to the script.</param>
        public static void ExecuteScript(this IWebDriver driver, string script, params object[] args)
        {
            driver.ExecuteScript<object>(script, args);
        }

        /// <summary>
        /// Executes JavaScript in the context of the currently selected frame or window.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="script">The script to be </param>
        /// <param name="args">The arguments to the script.</param>
        /// <returns>The value returned by the script.</returns>
        /// <remarks>
        /// For an HTML element, this method returns a <see cref="IWebElement"/>.
        /// For a number, a <see cref="long"/> is returned.
        /// For a boolean, a <see cref="bool"/> is returned.
        /// For all other cases a <see cref="string"/> is returned.
        /// For an array,we check the first element, and attempt to return a 
        /// <see cref="T:System.Collections.Generic.List`1"/> of that type, following the rules above. Nested lists 
        /// are not supported.
        /// </remarks>
        public static T ExecuteScript<T>(this IWebDriver driver, string script, params object[] args)
        {
            return (T)((IJavaScriptExecutor)driver).ExecuteScript(script, args);
        }

        /// <summary>
        /// Checks if prerequisites for the selector has been met.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="externalLibraryLoader">The external library loader.</param>
        /// <returns><c>true</c> if prerequisites are met; otherwise, <c>false</c></returns>
        public static bool CheckSelectorPrerequisites(
            this IWebDriver driver,
            ILoader externalLibraryLoader)
        {
            if (externalLibraryLoader == null)
            {
                throw new ArgumentNullException("externalLibraryLoader");
            }

            var result = driver.ExecuteScript<bool?>(externalLibraryLoader.CheckScript);
            return result.HasValue && result.Value;
        }

        /// <summary>
        /// Checks if external library is loaded and loads it if needed.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="externalLibraryLoader">The external library loader.</param>
        /// <param name="libraryUri">
        /// The URI of external library to load if it's not already loaded on the tested page.
        /// </param>
        /// <param name="timeout">The timeout value for the external library load.</param>
        /// <remarks>
        /// If external library is already loaded on a page this method will do nothing, even if the loaded version 
        /// and version requested by invoking this method have different versions.
        /// </remarks>
        public static void LoadExternalLibrary(
            this IWebDriver driver,
            ILoader externalLibraryLoader,
            Uri libraryUri,
            TimeSpan? timeout = null)
        {
            if (externalLibraryLoader == null)
            {
                throw new ArgumentNullException("externalLibraryLoader");
            }

            driver.LoadPrerequisites(
                externalLibraryLoader,
                timeout ?? TimeSpan.FromSeconds(3),
                libraryUri == null ? externalLibraryLoader.LibraryUri.OriginalString : libraryUri.OriginalString);
        }

        /// <summary>
        /// Loads the prerequisites for the selector.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="externalLibraryLoader">The external library loader.</param>
        /// <param name="timeout">The timeout value for the prerequisites load.</param>
        /// <param name="loadParams">The additional parameters for load script.</param>
        private static void LoadPrerequisites(
            this IWebDriver driver,
            ILoader externalLibraryLoader,
            TimeSpan timeout,
            params string[] loadParams)
        {
            if (driver.CheckSelectorPrerequisites(externalLibraryLoader))
            {
                return;
            }

            driver.ExecuteScript(externalLibraryLoader.LoadScript(loadParams));
            var wait = new WebDriverWait(driver, timeout);
            wait.Until(d => driver.CheckSelectorPrerequisites(externalLibraryLoader));
        }
    }
}
