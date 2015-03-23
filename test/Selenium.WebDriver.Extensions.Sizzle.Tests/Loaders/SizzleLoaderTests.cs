﻿namespace Selenium.WebDriver.Extensions.Sizzle.Tests
{
    using System;
    using System.Linq;
    using Selenium.WebDriver.Extensions.Core;
    using Xunit;

    [Trait("Category", "Unit")]
#if !NET35
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public class SizzleLoaderTests
    {
        [Fact]
        public void ShouldLoadScriptArgumentsNull()
        {
            var loader = new SizzleLoader();
            
            var ex = Assert.Throws<ArgumentNullException>(() => loader.LoadScript(null));
            Assert.Equal("args", ex.ParamName);
        }

        [Fact]
        public void ShouldLoadScriptArgumentsEmpty()
        {
            var loader = new SizzleLoader();
            
            Assert.Throws<LoaderException>(() => loader.LoadScript(Enumerable.Empty<string>().ToArray()));
        }
    }
}
