﻿namespace Selenium.WebDriver.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Web driver extensions.
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Checks if jQuery is loaded and loads it if needed.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="version">
        /// The version of jQuery to load if it's not already loaded on the tested page. It must be the full version
        /// number matching one of the versions at <see href="https://code.jquery.com/jquery"/>. The default value will
        /// get the latest stable version.
        /// </param>
        /// <param name="timeout">The timeout value for the jQuery load.</param>
        /// <remarks>
        /// If jQuery is already loaded on a page this method will do nothing, even if the loaded version and version
        /// requested by invoking this method have different versions.
        /// The protocol is not specified in the URL so that it can be determined by the browser if the page is using
        /// HTTP or HTTPS protocol.
        /// </remarks>
        public static void LoadJQuery(this IWebDriver driver, string version = "latest", TimeSpan? timeout = null)
        {
            driver.LoadJQuery(new Uri("//code.jquery.com/jquery-" + version + ".min.js"), timeout);
        }

        /// <summary>
        /// Checks if jQuery is loaded and loads it if needed.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="jQueryUri">The URI of jQuery to load if it's not already loaded on the tested page.</param>
        /// <param name="timeout">The timeout value for the jQuery load.</param>
        /// <remarks>
        /// If jQuery is already loaded on a page this method will do nothing, even if the loaded version and version
        /// requested by invoking this method have different versions.
        /// The protocol is not specified in the URL so that it can be determined by the browser if the page is using
        /// HTTP or HTTPS protocol.
        /// </remarks>
        public static void LoadJQuery(this IWebDriver driver, Uri jQueryUri, TimeSpan? timeout = null)
        {
            if (jQueryUri == null)
            {
                driver.LoadJQuery(timeout: timeout);
                return;
            }

            driver.LoadJQuery(jQueryUri.OriginalString, timeout ?? TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>The DOM elements matching given jQuery selector.</returns>
        public static IWebElement FindElement(
            this IWebDriver driver,
            JQuerySelector by)
        {
            var result = driver.Find<IWebElement>(by, "get(0)");
            if (result == null)
            {
                throw new NoSuchElementException("No element found with jQuery command: " + by.Selector);
            }

            return result;
        }

        /// <summary>
        /// Searches for DOM element using jQuery selector.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>The first DOM element matching given jQuery selector</returns>
        public static ReadOnlyCollection<IWebElement> FindElements(
            this IWebDriver driver,
            JQuerySelector by)
        {
            var result = driver.Find<IEnumerable<IWebElement>>(by, "get()") ?? Enumerable.Empty<IWebElement>();
            return new ReadOnlyCollection<IWebElement>(result.ToList());
        }

        /// <summary>
        /// Searches for DOM element using jQuery selector and gets the combined text contents of each element in the 
        /// set of matched elements, including their descendants, or set the text contents of the matched elements.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>
        /// The combined text contents of each element in the set of matched elements, including their descendants, or 
        /// set the text contents of the matched elements.
        /// </returns>
        public static string FindText(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<string>(by, "text()");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the HTML contents of the first element in the set 
        /// of matched elements or set the HTML contents of every matched element.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>
        /// The HTML contents of the first element in the set of matched elements or set the HTML contents of every 
        /// matched element.
        /// </returns>
        public static string FindHtml(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<string>(by, "html()");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the value of an attribute for the first element 
        /// in the set of matched elements.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <param name="attributeName">The name of the attribute to get.</param>
        /// <returns>The value of an attribute for the first element in the set of matched elements.</returns>
        public static string FindAttribute(
            this IWebDriver driver,
            JQuerySelector by,
            string attributeName)
        {
            return driver.FindAttribute<string>(by, attributeName);
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the value of an attribute for the first element 
        /// in the set of matched elements.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <param name="attributeName">The name of the attribute to get.</param>
        /// <returns>The value of an attribute for the first element in the set of matched elements.</returns>
        public static T FindAttribute<T>(
            this IWebDriver driver,
            JQuerySelector by,
            string attributeName)
        {
            return driver.Find<T>(by, "attr('" + attributeName + "')");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the value of a property for the first element in 
        /// the set of matched elements.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <param name="propertyName">The name of the property to get.</param>
        /// <returns>The value of a property for the first element in the set of matched elements.</returns>
        public static string FindProperty(
            this IWebDriver driver,
            JQuerySelector by,
            string propertyName)
        {
            return driver.FindProperty<string>(by, propertyName);
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the value of a property for the first element in 
        /// the set of matched elements.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <param name="propertyName">The name of the property to get.</param>
        /// <returns>The value of a property for the first element in the set of matched elements.</returns>
        public static T FindProperty<T>(
            this IWebDriver driver,
            JQuerySelector by,
            string propertyName)
        {
            return driver.Find<T>(by, "prop('" + propertyName + "')");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current value of the first element in the set 
        /// of matched elements.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>The current value of the first element in the set of matched elements.</returns>
        public static string FindValue(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<string>(by, "val()");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and get the value of a style property for the first 
        /// element in the set of matched elements or set one or more CSS properties for every matched element.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <param name="propertyName">The CSS property name.</param>
        /// <returns>
        /// The value of a style property for the first element in the set of matched elements or set one or more CSS 
        /// properties for every matched element.
        /// </returns>
        /// <remarks>
        /// Because of the limitations of the Selenium the only valid types are: <see cref="int"/>, <see cref="bool"/> 
        /// and <see cref="string"/>.
        /// </remarks>
        public static string FindCss(
            this IWebDriver driver,
            JQuerySelector by,
            string propertyName)
        {
            return driver.FindCss<string>(by, propertyName);
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and get the value of a style property for the first 
        /// element in the set of matched elements or set one or more CSS properties for every matched element.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <param name="propertyName">The CSS property name.</param>
        /// <returns>
        /// The value of a style property for the first element in the set of matched elements or set one or more CSS 
        /// properties for every matched element.
        /// </returns>
        /// <remarks>
        /// Because of the limitations of the Selenium the only valid types are: <see cref="int"/>, <see cref="bool"/> 
        /// and <see cref="string"/>.
        /// </remarks>
        public static T FindCss<T>(
            this IWebDriver driver,
            JQuerySelector by,
            string propertyName)
        {
            return driver.Find<T>(by, "css('" + propertyName + "')");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed width for the first element 
        /// in the set of matched elements or set the width of every matched element.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>
        /// The current computed width for the first element in the set of matched elements or set the width of every 
        /// matched element.
        /// </returns>
        public static int FindWidth(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<int>(by, "width()");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed height for the first element 
        /// in the set of matched elements or set the width of every matched element.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>
        /// The current computed height for the first element in the set of matched elements or set the height of 
        /// every matched element.
        /// </returns>
        public static int FindHeight(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<int>(by, "height()");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed inner width (including 
        /// padding but not border) for the first element in the set of matched elements or set the inner width of 
        /// every matched element.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>
        /// The current computed inner width (including padding but not border) for the first element in the set of 
        /// matched elements or set the inner width of every matched element.
        /// </returns>
        public static int FindInnerWidth(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<int>(by, "innerWidth()");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed inner height (including 
        /// padding but not border) for the first element in the set of matched elements or set the inner width of 
        /// every matched element.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>
        /// The current computed inner height (including padding but not border) for the first element in the set of 
        /// matched elements or set the inner width of every matched element.
        /// </returns>
        public static int FindInnerHeight(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<int>(by, "innerHeight()");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed width for the first element 
        /// in the set of matched elements, including padding and border.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <param name="includeMargin">
        /// A flag indicating whether to include the element's margin in the calculation.
        /// </param>
        /// <returns>
        /// The current computed width for the first element in the set of matched elements, including padding and 
        /// border.
        /// </returns>
        public static int FindOuterWidth(
            this IWebDriver driver,
            JQuerySelector by,
            bool includeMargin = false)
        {
            return driver.Find<int>(by, "outerWidth(" + (includeMargin ? "true" : string.Empty) + ")");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed height for the first element 
        /// in the set of matched elements, including padding and border.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <param name="includeMargin">
        /// A flag indicating whether to include the element's margin in the calculation.
        /// </param>
        /// <returns>
        /// The current computed height for the first element in the set of matched elements, including padding and 
        /// border.
        /// </returns>
        public static int FindOuterHeight(
            this IWebDriver driver,
            JQuerySelector by,
            bool includeMargin = false)
        {
            return driver.Find<int>(by, "outerHeight(" + (includeMargin ? "true" : string.Empty) + ")");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current coordinates of the first element in 
        /// the set of matched elements, relative to the offset parent.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>
        /// The current coordinates of the first element in the set of matched elements, relative to the offset 
        /// parent.
        /// </returns>
        public static Position FindPosition(
            this IWebDriver driver,
            JQuerySelector by)
        {
            var top = driver.Find<int>(by, "position().top");
            var left = driver.Find<int>(by, "position().left");
            return new Position(top, left);
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current coordinates of the first element in 
        /// the set of matched elements, relative to the document.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>
        /// The current coordinates of the first element in the set of matched elements, relative to the document.
        /// </returns>
        public static Position FindOffset(
            this IWebDriver driver,
            JQuerySelector by)
        {
            var top = driver.Find<int>(by, "offset().top");
            var left = driver.Find<int>(by, "offset().left");
            return new Position(top, left);
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current horizontal position of the scroll bar
        /// for the first element in the set of matched elements or set the horizontal position of the scroll bar for 
        /// every matched element.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>
        /// The current horizontal position of the scroll bar for the first element in the set of matched elements or 
        /// set the horizontal position of the scroll bar for every matched element.
        /// </returns>
        public static int FindScrollLeft(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<int>(by, "scrollLeft()");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current vertical position of the scroll bar 
        /// for the first element in the set of matched elements or set the vertical position of the scroll bar for 
        /// every matched element.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>
        /// The current vertical position of the scroll bar for the first element in the set of matched elements or 
        /// set the vertical position of the scroll bar for every matched element.
        /// </returns>
        public static int FindScrollTop(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<int>(by, "scrollTop()");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and returns the value at the named data store for the 
        /// first element in the jQuery collection, as set by <c>data(name, value)</c> or by an HTML5 data-* 
        /// attribute.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <param name="key">The name of the data stored.</param>
        /// <returns>
        /// The value at the named data store for the first element in the jQuery collection, as set by 
        /// <c>data(name, value)</c> or by an HTML5 data-* attribute.
        /// </returns>
        public static string FindData(
            this IWebDriver driver,
            JQuerySelector by,
            string key)
        {
            return driver.Find<string>(by, "data('" + key + "')");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the number of elements in the jQuery object.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>The number of elements in the jQuery object.</returns>
        public static int FindCount(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<int>(by, "length");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the encoded set of form elements as a string 
        /// for submission.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>The encoded set of form elements as a string for submission.</returns>
        public static string FindSerialized(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<string>(by, "serialize()");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the set of form elements as a string representing
        /// encoded array of names and values.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <returns>The encoded set of form elements as a string representing array of names and values.</returns>
        public static string FindSerializedArray(
            this IWebDriver driver,
            JQuerySelector by)
        {
            return driver.Find<string>(by, "serializeArray()", "JSON.stringify({0})");
        }

        /// <summary>
        /// Checks if jQuery is loaded and loads it if needed.
        /// </summary>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="jQueryUri">The URI for jQuery to load if it's not already loaded on the tested page.</param>
        /// <param name="timeout">The timeout value for the jQuery load.</param>
        /// <remarks>
        /// If jQuery is already loaded on a page this method will do nothing, even if the loaded version and version
        /// requested by invoking this method have different versions.
        /// The protocol is not specified in the URL so that it can be determined by the browser if the page is using
        /// HTTP or HTTPS protocol.
        /// </remarks>
        private static void LoadJQuery(this IWebDriver driver, string jQueryUri, TimeSpan timeout)
        {
            var javaScriptDriver = (IJavaScriptExecutor)driver;

            const string CheckScript = "return typeof window.jQuery !== 'function'";
            var exists = (bool)javaScriptDriver.ExecuteScript(CheckScript);
            if (exists)
            {
                return;
            }

            var loadScript = "var jq = document.createElement('script');" +
                "jq.src = '" + jQueryUri + "';" +
                "document.getElementsByTagName('head')[0].appendChild(jq);";

            javaScriptDriver.ExecuteScript(loadScript);
            var wait = new WebDriverWait(driver, timeout);
            wait.Until(d => javaScriptDriver.ExecuteScript(CheckScript));
        }

        /// <summary>
        /// Performs a jQuery search on the <see cref="IWebDriver"/> using given <see cref="JQuerySelector"/> selector 
        /// and script format string.
        /// </summary>
        /// <typeparam name="T">The type of the result to be returned.</typeparam>
        /// <param name="driver">The Selenium web driver.</param>
        /// <param name="by">The Selenium jQuery selector.</param>
        /// <param name="scriptFormat">The format string of the script to be invoked.</param>
        /// <param name="wrapperFormat">
        /// The wrapper format string for the purpose of wrap the jQuery selection result.
        /// </param>
        /// <returns>Result of invoking the script.</returns>
        /// <remarks>
        /// Because of the limitations of the Selenium the only valid types are: <see cref="int"/>, <see cref="bool"/> 
        /// and <see cref="string"/>, <see cref="IWebElement"/> and <see cref="IEnumerable{IWebElement}"/>.
        /// </remarks>
        private static T Find<T>(
            this IWebDriver driver,
            JQuerySelector by, 
            string scriptFormat,
            string wrapperFormat = null)
        {
            if (by == null)
            {
                throw new ArgumentNullException("by");
            }

            driver.LoadJQuery();

            var javaScriptDriver = (IJavaScriptExecutor)driver;
            var script = by + "." + scriptFormat;
            if (wrapperFormat != null)
            {
                script = string.Format(CultureInfo.InvariantCulture, wrapperFormat, script);
            }

            script = "return " + script + ";";
            return (T)javaScriptDriver.ExecuteScript(script);
        }
    }
}
