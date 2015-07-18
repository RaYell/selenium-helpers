﻿namespace OpenQA.Selenium.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Internal;
    using Xunit;
    using By = OpenQA.Selenium.Extensions.By;

    [Trait("Category", "Unit")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class JQuerySelectorTests
    {
        [Fact]
        public void ShouldCreateJQuerySelector()
        {
            // Given
            // When
            var selector = By.JQuerySelector("div");

            // Then
            Assert.NotNull(selector);
            Assert.Equal("div", selector.RawSelector);
        }

        [Fact]
        public void ShouldCreateJQuerySelectorWithContext()
        {
            // Given
            var context = By.JQuerySelector("body");

            // When
            var selector = By.JQuerySelector("div", context);

            // Then
            Assert.NotNull(selector);
            Assert.Equal("div", selector.RawSelector);
            Assert.Equal("body", selector.Context.RawSelector);
        }

        [Fact]
        public void ShouldCreateJQuerySelectorWithJQueryVariable()
        {
            // Given
            const string Variable = "test";

            // When
            var selector = By.JQuerySelector("div", variable: Variable);

            // Then
            Assert.NotNull(selector);
            Assert.Equal("div", selector.RawSelector);
            Assert.Equal("test", selector.Variable);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingJQuerySelectorWithNullValue()
        {
            // Given
            // When
            var ex = Assert.Throws<ArgumentNullException>(() => By.JQuerySelector(null));

            // Then
            Assert.Equal("selector", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingJQuerySelectorWithEmptyValue()
        {
            // Given
            // When
            var ex = Assert.Throws<ArgumentException>(() => By.JQuerySelector(string.Empty));

            // Then
            Assert.Equal("selector", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingJQuerySelectorWithWhiteSpaceOnlyValue()
        {
            // Given
            // When
            var ex = Assert.Throws<ArgumentException>(() => By.JQuerySelector(" "));

            // Then
            Assert.Equal("selector", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingJQuerySelectorWithNullVariableValue()
        {
            // Given
            // When
            var ex = Assert.Throws<ArgumentNullException>(() => By.JQuerySelector("div", variable: null));

            // Then
            Assert.Equal("variable", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingJQuerySelectorWithEmptyVariableValue()
        {
            // Given
            // When
            var ex = Assert.Throws<ArgumentException>(() => By.JQuerySelector("div", variable: string.Empty));

            // Then
            Assert.Equal("variable", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingJQuerySelectorWithWhiteSpaceOnlyVariableValue()
        {
            // Given
            // When
            var ex = Assert.Throws<ArgumentException>(() => By.JQuerySelector("div", variable: " "));

            // Then
            Assert.Equal("variable", ex.ParamName);
        }

        [Fact]
        public void ShouldFindElementByJQuerySelector()
        {
            // Given
            var driver = new Mock<IWebDriver>();
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("window.jQuery")), It.IsAny<object[]>()))
                .Returns(true);
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("jQuery('div')")), It.IsAny<object[]>()))
                .Returns(new List<IWebElement> { new Mock<IWebElement>().Object });
            var selector = By.JQuerySelector("div");

            // When
            var result = selector.FindElement(driver.Object);

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldFindElementsByJQuerySelector()
        {
            // Given
            var driver = new Mock<IWebDriver>();
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("window.jQuery")), It.IsAny<object[]>()))
                .Returns(true);
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("jQuery('div')")), It.IsAny<object[]>()))
                .Returns(new List<IWebElement> { new Mock<IWebElement>().Object, new Mock<IWebElement>().Object });
            var selector = By.JQuerySelector("div");

            // When
            var result = selector.FindElements(driver.Object);

            // Then
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ShouldThrowExceptionWhenElementIsNotFoundWithJQuerySelector()
        {
            // Given
            var driver = new Mock<IWebDriver>();
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("window.jQuery")), It.IsAny<object[]>()))
                .Returns(true);
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("jQuery('div')")), It.IsAny<object[]>()))
                .Returns(new List<IWebElement>());
            var selector = By.JQuerySelector("div");

            // When
            Assert.Throws<NoSuchElementException>(() => selector.FindElement(driver.Object));

            // Then
        }

        [Fact]
        public void ShouldReturnEmptyResultWhenNoElementsAreFoundWithJQuerySelector()
        {
            // Given
            var driver = new Mock<IWebDriver>();
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("window.jQuery")), It.IsAny<object[]>()))
                .Returns(true);
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("jQuery('div')")), It.IsAny<object[]>()))
                .Returns(new List<IWebElement>());
            var selector = By.JQuerySelector("div");

            // When
            var result = selector.FindElements(driver.Object);

            // Then
            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void ShouldFindElementWithNestedJQuerySelector()
        {
            // Given
            var driver = new Mock<IWebDriver>();
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("window.jQuery")), It.IsAny<object[]>()))
                .Returns(true);
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("function(element)")), It.IsAny<object[]>()))
                .Returns("body > div");
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("jQuery('div')")), It.IsAny<object[]>()))
                .Returns(new List<IWebElement> { new Mock<IWebElement>().Object });
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("jQuery('body > div')")), It.IsAny<object[]>()))
                .Returns(new List<IWebElement> { new Mock<IWebElement>().Object });

            var element = new Mock<ISearchContext>();
            element.As<IWrapsDriver>().SetupGet(x => x.WrappedDriver).Returns(driver.Object);
            element.As<IWebElement>();

            var selector = By.JQuerySelector("div");

            // When
            var result = selector.FindElement(element.Object);

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldThrowExceptionWhenSearchContextIsNotWebElement()
        {
            // Given
            var driver = new Mock<IWebDriver>();
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("window.jQuery")), It.IsAny<object[]>()))
                .Returns(true);
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("function(element)")), It.IsAny<object[]>()))
                .Returns("body > div");
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("jQuery('div')")), It.IsAny<object[]>()))
                .Returns(new List<IWebElement> { new Mock<IWebElement>().Object });
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript(It.Is<string>(s => s.Contains("jQuery('body > div')")), It.IsAny<object[]>()))
                .Returns(new List<IWebElement> { new Mock<IWebElement>().Object });

            var element = new Mock<ISearchContext>();
            element.As<IWrapsDriver>().SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var selector = By.JQuerySelector("div");

            // When
            // Then
            Assert.Throws<NotSupportedException>(() => selector.FindElement(element.Object));
       }

        [Fact]
        public void ShouldThrowExceptionWhenSearchContextDoesNotWrapDriver()
        {
            // Given
            var element = new Mock<ISearchContext>();
            element.As<IWebElement>();

            var selector = By.JQuerySelector("div");

            // When
            // Then
            Assert.Throws<NotSupportedException>(() => selector.FindElement(element.Object));
        }
    }
}
