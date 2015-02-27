﻿namespace Selenium.WebDriver.Extensions
{
    using JetBrains.Annotations;
    using Selenium.WebDriver.Extensions.JQuery;
    using Selenium.WebDriver.Extensions.Shared;
    using Selenium.WebDriver.Extensions.Sizzle;
    using QS = Selenium.WebDriver.Extensions.QuerySelector.QuerySelector;

    /// <summary>
    /// Extends the selenium <see cref="OpenQA.Selenium.By"/> to enable jQuery selector to be used.
    /// </summary>
    /// <remarks>
    /// This class shadows all of the static members of the <see cref="OpenQA.Selenium.By"/>. The reason for that is
    /// to avoid warnings that the code is accessing static members of the base class.
    /// </remarks>
    [UsedImplicitly]
    public class By : OpenQA.Selenium.By
    {
        /// <summary>
        /// Gets a mechanism to find elements matching jQuery selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression</param>
        /// <param name="context">A DOM Element, Document, or jQuery to use as context.</param>
        /// <param name="jQueryVariable">A variable that has been assigned to jQuery.</param>
        /// <returns>A <see cref="JQuerySelector"/> object the driver can use to find the elements.</returns>
        public static JQuerySelector JQuerySelector(
            string selector, 
            JQuerySelector context = null, 
            string jQueryVariable = "jQuery")
        {
            return JQuery.By.JQuerySelector(selector, context, jQueryVariable);
        }

        /// <summary>
        /// Gets a mechanism to find elements matching Sizzle selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression</param>
        /// <param name="context">A DOM Element, Document, or jQuery to use as context.</param>
        /// <returns>A <see cref="SizzleSelector"/> object the driver can use to find the elements.</returns>
        public static SizzleSelector SizzleSelector(
            string selector,
            SizzleSelector context = null)
        {
            return Sizzle.By.SizzleSelector(selector, context);
        }

        /// <summary>
        /// Gets a mechanism to find elements matching JavaScript query selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression</param>
        /// <param name="baseElement">
        /// A string defining the base element on which base element the selector should be invoked.
        /// </param>
        /// <returns>
        /// A <see cref="QS"/> object the driver can 
        /// use to find the elements.
        /// </returns>
        public static QS QuerySelector(string selector, string baseElement = "document")
        {
            return Extensions.QuerySelector.By.QuerySelector(selector, baseElement);
        }

        /// <summary>
        /// Gets a mechanism to find elements matching JavaScript query selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression</param>
        /// <param name="baseSelector">A query selector on which defines a base element for the new selector.</param>
        /// <returns>
        /// A <see cref="QS"/> object the driver can 
        /// use to find the elements.
        /// </returns>
        public static QS QuerySelector(string selector, QS baseSelector)
        {
            return Extensions.QuerySelector.By.QuerySelector(selector, baseSelector);
        }

        /// <summary>
        /// Gets a mechanism to find elements by their CSS class.
        /// </summary>
        /// <param name="classNameToFind">The CSS class to find.</param>
        /// <returns>A <see cref="OpenQA.Selenium.By"/> object the driver can use to find the elements.</returns>
        public static new ClassNameSelector ClassName(string classNameToFind)
        {
            return new ClassNameSelector(classNameToFind);
        }

        /// <summary>
        /// Gets a mechanism to find elements by their cascading style sheet (CSS) selector.
        /// </summary>
        /// <param name="cssSelectorToFind">The CSS selector to find.</param>
        /// <returns>A <see cref="OpenQA.Selenium.By"/> object the driver can use to find the elements.</returns>
        public static new CssSelector CssSelector(string cssSelectorToFind)
        {
            return new CssSelector(cssSelectorToFind);
        }

        /// <summary>
        /// Gets a mechanism to find elements by their ID.
        /// </summary>
        /// <param name="idToFind">The ID to find.</param>
        /// <returns>A <see cref="OpenQA.Selenium.By"/> object the driver can use to find the elements.</returns>
        public static new IdSelector Id(string idToFind)
        {
            return new IdSelector(idToFind);
        }

        /// <summary>
        /// Gets a mechanism to find elements by their link text.
        /// </summary>
        /// <param name="linkTextToFind">The link text to find.</param>
        /// <returns>A <see cref="OpenQA.Selenium.By"/> object the driver can use to find the elements.</returns>
        public static new OpenQA.Selenium.By LinkText(string linkTextToFind)
        {
            return OpenQA.Selenium.By.LinkText(linkTextToFind);
        }

        /// <summary>
        /// Gets a mechanism to find elements by their name.
        /// </summary>
        /// <param name="nameToFind">The name to find.</param>
        /// <returns>A <see cref="OpenQA.Selenium.By"/> object the driver can use to find the elements.</returns>
        public static new NameSelector Name(string nameToFind)
        {
            return new NameSelector(nameToFind);
        }

        /// <summary>
        /// Gets a mechanism to find elements by a partial match on their link text.
        /// </summary>
        /// <param name="partialLinkTextToFind">The partial link text to find.</param>
        /// <returns>A <see cref="OpenQA.Selenium.By"/> object the driver can use to find the elements.</returns>
        public static new OpenQA.Selenium.By PartialLinkText(string partialLinkTextToFind)
        {
            return OpenQA.Selenium.By.PartialLinkText(partialLinkTextToFind);
        }

        /// <summary>
        /// Gets a mechanism to find elements by their tag name.
        /// </summary>
        /// <param name="tagNameToFind">The tag name to find.</param>
        /// <returns>A <see cref="OpenQA.Selenium.By"/> object the driver can use to find the elements.</returns>
        public static new TagNameSelector TagName(string tagNameToFind)
        {
            return new TagNameSelector(tagNameToFind);
        }

        /// <summary>
        /// Gets a mechanism to find elements by an XPath query. When searching within a WebElement using xpath be 
        /// aware that WebDriver follows standard conventions: a search prefixed with "//" will search the entire 
        /// document, not just the children of this current node.  Use ".//" to limit your search to the children of 
        /// this WebElement.
        /// </summary>
        /// <param name="xpathToFind">The XPath query to use.</param>
        /// <returns>A <see cref="OpenQA.Selenium.By"/> object the driver can use to find the elements.</returns>
        public static new OpenQA.Selenium.By XPath(string xpathToFind)
        {
            return OpenQA.Selenium.By.XPath(xpathToFind);
        }
    }
}
