﻿namespace Selenium.WebDriver.Extensions.Sizzle.Tests
{
    using System;
    using Moq;
    using OpenQA.Selenium;
    using Selenium.WebDriver.Extensions.Sizzle;
    using Xunit;

    [Trait("Category", "Unit")]
#if !NET35
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public class SizzleRunnerTests
    {
        [Fact]
        public void ShouldThrowExceptionForRunnerWithNullDriver()
        {
            var runner = new SizzleRunner();

            var ex = Assert.Throws<ArgumentNullException>(() => runner.Find<object>(null, null));
            Assert.Equal("driver", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionForNullSelector()
        {
            var runner = new SizzleRunner();
            var driver = new Mock<IWebDriver>();

            var ex = Assert.Throws<ArgumentNullException>(() => runner.Find<object>(driver.Object, null));
            Assert.Equal("by", ex.ParamName);
        }
    }
}
