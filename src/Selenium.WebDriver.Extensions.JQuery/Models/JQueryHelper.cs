﻿namespace Selenium.WebDriver.Extensions.JQuery
{
    using System;
    using OpenQA.Selenium;
    using Selenium.WebDriver.Extensions.Core;
    
    /// <summary>
    /// Additional methods for <see cref="JQuerySelector"/>.
    /// </summary>
    public class JQueryHelper : HelperBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JQueryHelper"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="webElement">The web element.</param>
        /// <exception cref="ArgumentNullException">Driver is null.</exception>
        public JQueryHelper(IWebDriver driver, WebElement webElement = null)
            : base(driver, webElement)
        {
        }

        /// <summary>
        /// Checks if jQuery is loaded and loads it if needed.
        /// </summary>
        /// <param name="version">
        /// The version of jQuery to load if it's not already loaded on the tested page. It must be the full version
        /// number matching one of the versions at <see href="https://code.jquery.com/jquery"/>. The default value will
        /// get the latest stable version.
        /// </param>
        /// <param name="timeout">The timeout value for the jQuery load.</param>
        /// <remarks>
        /// If jQuery is already loaded on a page this method will do nothing, even if the loaded version and version
        /// requested by invoking this method have different versions.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Version is null.</exception>
        /// <exception cref="ArgumentException">Version is empty.</exception>
        /// <exception cref="OverflowException">
        /// Value is less than <see cref="TimeSpan.MinValue" /> or greater than <see cref="TimeSpan.MaxValue" />.-or-
        /// Value is <see cref="double.PositiveInfinity" />.-or-Value is <see cref="double.NegativeInfinity" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// This instance represents a relative URI, and this property is valid only for absolute URIs.
        /// </exception>
        /// <exception cref="UriFormatException">
        /// URI string is empty.-or- The scheme specified in URI string is not correctly formed. See 
        /// <see cref="M:System.Uri.CheckSchemeName(System.String)" />.-or- URI string contains too many slashes.-or- 
        /// The password specified in URI string is not valid.-or- The host name specified in URI string is not valid.
        /// -or- The file name specified in URI string is not valid. -or- The user name specified in URI string is not 
        /// valid.-or- The host or authority name specified in URI string cannot be terminated by backslashes.-or- 
        /// The port number specified in URI string is not valid or cannot be parsed.-or- The length of URI string 
        /// exceeds 65519 characters.-or- The length of the scheme specified in URI string exceeds 1023 characters.
        /// -or- There is an invalid character sequence in URI string.-or- The MS-DOS path specified in URI string 
        /// must start with c:\\.</exception>
        public void Load(string version = "latest", TimeSpan? timeout = null)
        {
            if (version == null)
            {
                throw new ArgumentNullException("version");
            }

            if (version.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Version cannot be empty", "version");
            }

            this.Driver.LoadExternalLibrary(
                new JQueryLoader(),
                new Uri("https://code.jquery.com/jquery-" + version + ".min.js"),
                timeout);
        }

        /// <summary>
        /// Checks if jQuery is loaded and loads it if needed.
        /// </summary>
        /// <param name="jQueryUri">The URI of jQuery to load if it's not already loaded on the tested page.</param>
        /// <param name="timeout">The timeout value for the jQuery load.</param>
        /// <remarks>
        /// If jQuery is already loaded on a page this method will do nothing, even if the loaded version and version
        /// requested by invoking this method have different versions.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Driver is null or loader is null.</exception>
        /// <exception cref="ArgumentException">Value is equal to <see cref="double.NaN" />.</exception>
        /// <exception cref="OverflowException">
        /// Value is less than <see cref="TimeSpan.MinValue" /> or greater than <see cref="TimeSpan.MaxValue" />.-or-
        /// Value is <see cref="double.PositiveInfinity" />.-or-Value is <see cref="double.NegativeInfinity" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// This instance represents a relative URI, and this property is valid only for absolute URIs.
        /// </exception>
        public void Load(Uri jQueryUri, TimeSpan? timeout = null)
        {
            this.Driver.LoadExternalLibrary(new JQueryLoader(), jQueryUri, timeout);
        }
    }
}
