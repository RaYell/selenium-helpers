﻿namespace Selenium.WebDriver.Extensions.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using OpenQA.Selenium;
    using Xunit;
    using By = Selenium.WebDriver.Extensions.Core.By;

    // ReSharper disable ExceptionNotDocumented
    // ReSharper disable ExceptionNotDocumentedOptional
    [Trait("Category", "Unit")]
    [ExcludeFromCodeCoverage]
    public class PartialLinkTextSelectorTests
    {
        public static IEnumerable<object[]> EqualityData
        {
            get
            {
                yield return new object[] { By.PartialLinkText("test"), By.PartialLinkText("test"), true };
                yield return new object[] { By.PartialLinkText("test"), By.PartialLinkText("test2"), false };
                yield return new object[] { By.PartialLinkText("test"), null, false };
                yield return new object[] { null, By.PartialLinkText("test"), false };
                yield return new object[] { By.PartialLinkText("test", "body"), By.PartialLinkText("test"), false };
                yield return new object[]
                                 {
                                     By.PartialLinkText("test", "body"), 
                                     By.PartialLinkText("test", "body"),
                                     true
                                 };
            }
        }

        [Fact]
        public void ShouldPopulateFormatStringProperty()
        {
            var formatString = By.PartialLinkText("test").CallFormatString;
            Assert.NotNull(formatString);
        }

        [Theory]
        [MemberData("EqualityData")]
        public void ShouldProperlyCompareSelectors(
            PartialLinkTextSelector selector1,
            PartialLinkTextSelector selector2,
            bool expectedResult)
        {
            Assert.Equal(expectedResult, selector1 == selector2);
            if (selector1 != null)
            {
                Assert.Equal(expectedResult, selector1.Equals(selector2));
                if (selector2 != null)
                {
                    Assert.Equal(expectedResult, selector1.GetHashCode() == selector2.GetHashCode());
                }
            }

            Assert.NotEqual(expectedResult, selector1 != selector2);
        }

        [Fact]
        public void ShouldNotCompareElementsOfDifferentType()
        {
            var selector1 = By.PartialLinkText("text");
            var selector2 = new object();

#pragma warning disable 252,253
            Assert.False(selector1 == selector2);
            Assert.True(selector1 != selector2);
#pragma warning restore 252,253
        }

        [Fact]
        public void ShouldHaveProperRunnerType()
        {
            var selector = new LinkTextSelector("test");

            Assert.Equal(typeof(QuerySelectorRunner), selector.RunnerType);
        }

        [Fact]
        public void ShouldCreateNestedSelector()
        {
            var rootSelector = new Mock<ISelector>();
            rootSelector.SetupGet(x => x.Selector).Returns("div");
            rootSelector.SetupGet(x => x.CallFormatString).Returns("{0}[{1}]");

            var driver = new Mock<IWebDriver>();
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(element\\)")))
                .Returns("body > div");

            var element = new Mock<WebElement>();
            element.SetupGet(x => x.Selector).Returns(rootSelector.Object);
            element.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var selector = By.PartialLinkText("test").Create(element.Object);
            Assert.IsType<PartialLinkTextSelector>(selector);

            var linkTextSelector = (PartialLinkTextSelector)selector;
            Assert.Equal("test", linkTextSelector.RawSelector);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingNestedSelectorWithNull()
        {
            var selector = new PartialLinkTextSelector("test");

            var ex = Assert.Throws<ArgumentNullException>(() => selector.Create(null));
            Assert.Equal("root", ex.ParamName);
        }
    }
}
