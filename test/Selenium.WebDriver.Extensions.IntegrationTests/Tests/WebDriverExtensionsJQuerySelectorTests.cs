namespace Selenium.WebDriver.Extensions.IntegrationTests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using OpenQA.Selenium.Support.UI;
    using Xunit;
    using By = By;

    [ExcludeFromCodeCoverage]
    public class WebDriverExtensionsJQuerySelectorTests : TestsBase
    {
        private const string LOADED_PATH = "/JQueryLoaded";
        private const string UNLOADED_PATH = "/JQueryUnloaded";

        protected WebDriverExtensionsJQuerySelectorTests(IWebDriver browser, bool loaded)
            : base(browser, loaded ? LOADED_PATH : UNLOADED_PATH)
        {
        }

        [Fact]
        public void FindElement()
        {
            var sut = By.JQuerySelector("#id1");
            var element = Browser.FindElement(sut);

            element.Should().NotBeNull();
        }

        [Fact]
        public void FindElementThatDoesNotExist()
        {
            var sut = By.JQuerySelector("#id-not");

            void Action() => Browser.FindElement(sut);

            ((Action)Action).ShouldThrow<NoSuchElementException>();
        }

        [Fact]
        public void FindElements()
        {
            var sut = By.JQuerySelector("div.main");
            var elements = Browser.FindElements(sut);

            elements.Should().NotBeNull().And.HaveCount(2);
        }

        [Fact]
        public void FindElementsThatDoesNotExist()
        {
            var sut = By.JQuerySelector("div.mainNot");
            var elements = Browser.FindElements(sut);

            elements.Should().NotBeNull().And.HaveCount(0);
        }

        [Fact]
        public void FindInnerElement()
        {
            var root = Browser.FindElement(By.CssSelector("body"));
            var sut = By.JQuerySelector("div");
            var element = root.FindElement(sut);

            element.Should().NotBeNull();
        }

        [Fact]
        public void FindInnerElements()
        {
            var root = Browser.FindElement(By.JQuerySelector("body"));
            var sut = By.JQuerySelector("div");
            var elements = root.FindElements(sut);

            elements.Should().NotBeNull().And.HaveCount(2);
        }

        [Fact]
        public void ExpectedConditionsSupport()
        {
            var condition = ExpectedConditions.ElementIsVisible(By.JQuerySelector("h1"));

            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(3));
            wait.Until(condition);

            true.Should().BeTrue(); // assert pass
        }

        [Fact]
        public void PageObjectsSupport()
        {
            var page = new TestPage(Browser);

            PageFactory.InitElements(Browser, page);

            page.HeadingJQuery.Should().NotBeNull();
            page.HeadingJQuery.Text.Trim().Should().Be("H1 Header");
        }
    }
}