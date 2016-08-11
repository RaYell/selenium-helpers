﻿namespace OpenQA.Selenium.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using OpenQA.Selenium.Extensions;
    using OpenQA.Selenium.Internal;
    using Xunit;

    [Trait("Category", "Unit")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class SizzleSelectorTests
    {
        [Fact]
        public void ShouldCreateSizzleSelector()
        {
            // Given
            // When
            var selector = By.SizzleSelector("div");

            // Then
            Assert.NotNull(selector);
            Assert.Equal("div", selector.RawSelector);
        }

        [Fact]
        public void ShouldCreateSizzleSelectorDirectly()
        {
            // Given
            // When
            var selector = new SizzleSelector("div");

            // Then
            Assert.NotNull(selector);
            Assert.Equal("div", selector.RawSelector);
        }

        [Fact]
        public void ShouldCreateSizzleSelectorWithContext()
        {
            // Given
            var context = By.SizzleSelector("body");

            // When
            var selector = By.SizzleSelector("div", context);

            // Then
            Assert.NotNull(selector);
            Assert.Equal("div", selector.RawSelector);
            Assert.Equal("body", selector.Context.RawSelector);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingSizzleSelectorWithNullValue()
        {
            // Given
            // When
            Action action = () => By.SizzleSelector(null);

            // Then
            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("selector", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingSizzleSelectorWithEmptyValue()
        {
            // Given
            // When
            Action action = () => By.SizzleSelector(string.Empty);

            // Then
            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("selector", ex.ParamName);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingSizzleSelectorWithWhiteSpaceOnlyValue()
        {
            // Given
            // When
            Action action = () => By.SizzleSelector(" ");

            // Then
            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("selector", ex.ParamName);
        }

        [Fact]
        public void ShouldFindElementBySizzleSelector()
        {
            // Given
            var driver = new WebDriverBuilder().ThatHasSizzleLoaded().ThatContainsElementLocatedBySizzle("div")
                .Build();
            var selector = By.SizzleSelector("div");

            // When
            var result = selector.FindElement(driver);

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldFindElementsBySizzleSelector()
        {
            // Given
            var driver = new WebDriverBuilder().ThatHasSizzleLoaded().ThatContainsElementsLocatedBySizzle("div")
                .Build();
            var selector = By.SizzleSelector("div");

            // When
            var result = selector.FindElements(driver);

            // Then
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ShouldThrowExceptionWhenElementIsNotFoundWithSizzleSelector()
        {
            // Given
            var driver = new WebDriverBuilder().ThatHasSizzleLoaded().ThatDoesNotContainElementLocatedBySizzle("div")
                .Build();
            var selector = By.SizzleSelector("div");

            // When
            Action action = () => selector.FindElement(driver);

            // Then
            Assert.Throws<NoSuchElementException>(action);
        }

        [Fact]
        public void ShouldReturnEmptyResultWhenNoElementsAreFoundWithSizzleSelector()
        {
            // Given
            var driver = new WebDriverBuilder().ThatHasSizzleLoaded().ThatDoesNotContainElementLocatedBySizzle("div")
                .Build();
            var selector = By.SizzleSelector("div");

            // When
            var result = selector.FindElements(driver);

            // Then
            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void ShouldFindElementWithNestedSizzleSelector()
        {
            // Given
            var driver = new WebDriverBuilder().ThatHasSizzleLoaded().ThatContainsElementLocatedBySizzle("div")
                .ThatContainsElementLocatedBySizzle("body > div").ThatCanResolvePathToElement("div")
                .Build();
            var element = new SearchContextBuilder().WithWrappedDriver(driver).ThatIsWebElement().Build();

            var selector = By.SizzleSelector("div");

            // When
            var result = selector.FindElement(element);

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldThrowExceptionWhenSearchContextIsNotWebElement()
        {
            // Given
            var driver = new WebDriverBuilder().ThatHasSizzleLoaded().ThatContainsElementLocatedBySizzle("div")
                .ThatContainsElementLocatedBySizzle("body > div").ThatCanResolvePathToElement("div")
                .Build();
            var element = new SearchContextBuilder().WithWrappedDriver(driver).Build();

            var selector = By.SizzleSelector("div");

            // When
            Action action = () => selector.FindElement(element);

            // Then
            Assert.Throws<NotSupportedException>(action);
        }

        [Fact]
        public void ShouldThrowExceptionWhenSearchContextDoesNotWrapDriver()
        {
            // Given
            var element = new SearchContextBuilder().ThatIsWebElement().Build();

            var selector = By.SizzleSelector("div");

            // When
            Action action = () => selector.FindElement(element);

            // Then
            Assert.Throws<NotSupportedException>(action);
        }

        [Fact]
        public void ShouldGetLibraryUri()
        {
            // Given
            var loader = SizzleSelector.Empty;

            // When
            var uri = loader.LibraryUri;

            // Then
            Assert.NotNull(uri);
        }
    }
}
