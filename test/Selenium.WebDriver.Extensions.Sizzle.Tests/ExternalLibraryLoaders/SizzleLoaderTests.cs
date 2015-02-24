﻿namespace Selenium.WebDriver.Extensions.Sizzle.Tests.ExternalLibraryLoaders
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using NUnit.Framework;
    using Selenium.WebDriver.Extensions.Shared;

    /// <summary>
    /// Sizzle loader tests.
    /// </summary>
    [TestFixture]
    [Category("Unit Tests")]
#if !NET35
    [ExcludeFromCodeCoverage]
#endif
    public class SizzleLoaderTests
    {
        /// <summary>
        /// Script loading test.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadScriptArgumentsNull()
        {
            var loader = new SizzleLoader();
            loader.LoadScript(null);
        }

        /// <summary>
        /// Script loading test.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ExternalLibraryLoadException))]
        public void LoadScriptArgumentsEmpty()
        {
            var loader = new SizzleLoader();
            loader.LoadScript(Enumerable.Empty<string>().ToArray());
        }
    }
}
