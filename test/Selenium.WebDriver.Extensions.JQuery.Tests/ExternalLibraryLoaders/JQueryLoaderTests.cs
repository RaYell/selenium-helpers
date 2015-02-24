﻿namespace Selenium.WebDriver.Extensions.JQuery.Tests.ExternalLibraryLoaders
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using NUnit.Framework;
    using Selenium.WebDriver.Extensions.Shared;

    /// <summary>
    /// JQuery loader tests.
    /// </summary>
    [TestFixture]
    [Category("Unit Tests")]
#if !NET35
    [ExcludeFromCodeCoverage]
#endif
    public class JQueryLoaderTests
    {
        /// <summary>
        /// Script loading test.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadScriptArgumentsNull()
        {
            var loader = new JQueryLoader();
            loader.LoadScript(null);
        }

        /// <summary>
        /// Script loading test.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ExternalLibraryLoadException))]
        public void LoadScriptArgumentsEmpty()
        {
            var loader = new JQueryLoader();
            loader.LoadScript(Enumerable.Empty<string>().ToArray());
        }
    }
}
