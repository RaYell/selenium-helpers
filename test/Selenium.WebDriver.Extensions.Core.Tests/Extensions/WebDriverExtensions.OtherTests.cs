﻿namespace Selenium.WebDriver.Extensions.Core.Tests
{
    using System;
    using Moq;
    using OpenQA.Selenium;
    using Xunit;
    
    [Trait("Category", "Unit")]
#if !NET35
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public class WebDriverExtensionsOtherTests
    {
        [Fact]
        public void ShouldThrowExceptionWhenQuerySelectorIsNotSupported()
        {
            var driverMock = new Mock<IWebDriver>();
            driverMock.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.IsRegex("\\(document.querySelectorAll\\)"))).Returns(false);
            Assert.Throws<QuerySelectorNotSupportedException>(() => driverMock.Object.QuerySelector().CheckSupport());
        }

        [Fact]
        public void ShouldThrowExceptionWhenCheckingSelectorPrerequisitesWithNullDriver()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => WebDriverExtensions.CheckSelectorPrerequisites(null, null));
            Assert.Equal("driver", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCheckingSelectorPrerequisitesWithoutLoader()
        {
            var driverMock = new Mock<IWebDriver>();
            driverMock.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.IsRegex("\\(document.querySelectorAll\\)"))).Returns(true);

            var ex = Assert.Throws<ArgumentNullException>(() => driverMock.Object.CheckSelectorPrerequisites(null));
            Assert.Equal("loader", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCheckingSelectorPrerequisitesWithIncorrectResponse()
        {
            var driverMock = new Mock<IWebDriver>();
            driverMock.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.IsRegex("\\(document.querySelectorAll\\)"))).Returns(null);

            Assert.False(driverMock.Object.CheckSelectorPrerequisites(new QuerySelectorLoader()));
        }

        [Fact]
        public void ShouldThrowExceptionWhenLoadingExternalLibraryWithNullDriver()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => WebDriverExtensions.LoadExternalLibrary(null, null, null));
            Assert.Equal("driver", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenLoadingExternalLibraryWithoutLoader()
        {
            var driverMock = new Mock<IWebDriver>();
            driverMock.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.IsRegex("\\(document.querySelectorAll\\)"))).Returns(true);

            var ex = Assert.Throws<ArgumentNullException>(() => driverMock.Object.LoadExternalLibrary(null, null));
            Assert.Equal("loader", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenExuecutingNullScript()
        {
            var driverMock = new Mock<IWebDriver>();
            
            var ex = Assert.Throws<ArgumentNullException>(() => driverMock.Object.ExecuteScript(null));
            Assert.Equal("script", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenExuecutingEmptyScript()
        {
            var driverMock = new Mock<IWebDriver>();

            var ex = Assert.Throws<ArgumentException>(() => driverMock.Object.ExecuteScript(string.Empty));
            Assert.Equal("script", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenExuecutingWhiteSpaceOnlyScript()
        {
            var driverMock = new Mock<IWebDriver>();

            var ex = Assert.Throws<ArgumentException>(() => driverMock.Object.ExecuteScript(" "));
            Assert.Equal("script", ex.ParamName);
        }
    }
}
