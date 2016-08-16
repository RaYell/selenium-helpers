﻿namespace Selenium.WebDriver.Extensions.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using OpenQA.Selenium.Support.UI;
    using Xunit;
    using By = OpenQA.Selenium.Extensions.By;

    [ExcludeFromCodeCoverage]
    public abstract class WebDriverExtensionsSizzleSelectorTests : TestsBase
    {
        [Fact]
        public void FindElement()
        {
            // Given
            var selector = By.SizzleSelector("#id1");

            // When
            var element = this.Browser.FindElement(selector);

            // Then
            element.Should().NotBeNull();
        }

        [Fact]
        public void FindElementThatDoesNotExist()
        {
            // Given
            var selector = By.SizzleSelector("#id-not");

            // When
            Action action = () => this.Browser.FindElement(selector);

            // Then
            action.ShouldThrow<NoSuchElementException>();
        }

        [Fact]
        public void FindElements()
        {
            // Given
            var selector = By.SizzleSelector("div.main");

            // When
            var elements = this.Browser.FindElements(selector);

            // Then
            elements.Should().NotBeNull().And.HaveCount(2);
        }

        [Fact]
        public void FindElementsThatDoesNotExist()
        {
            // Given
            var selector = By.SizzleSelector("div.mainNot");

            // When
            var elements = this.Browser.FindElements(selector);

            // Then
            elements.Should().NotBeNull().And.HaveCount(0);
        }

        [Fact]
        public void FindInnerElement()
        {
            // Given
            var root = this.Browser.FindElement(By.CssSelector("body"));
            var selector = By.SizzleSelector("div");

            // When
            var element = root.FindElement(selector);

            // Then
            element.Should().NotBeNull();
        }

        [Fact]
        public void FindInnerElements()
        {
            // Given
            var root = this.Browser.FindElement(By.SizzleSelector("body"));
            var selector = By.SizzleSelector("h1");

            // When
            var elements = root.FindElements(selector);

            // Then
            elements.Should().NotBeNull().And.HaveCount(1);
        }

        [Fact]
        public void ExpectedConditionsSupport()
        {
            // Given
            var condition = ExpectedConditions.ElementIsVisible(By.SizzleSelector("h1"));

            // When
            var wait = new WebDriverWait(this.Browser, TimeSpan.FromSeconds(3));
            wait.Until(condition);

            // Then
            true.Should().BeTrue(); // assert pass
        }

        [Fact]
        public void PageObjectsSupport()
        {
            // Given
            var page = new TestPage(this.Browser);

            // When
            PageFactory.InitElements(this.Browser, page);

            // Then
            page.HeadingJQuery.Should().NotBeNull();
            page.HeadingJQuery.Text.Trim().Should().Be("H1 Header");
        }
    }
}
