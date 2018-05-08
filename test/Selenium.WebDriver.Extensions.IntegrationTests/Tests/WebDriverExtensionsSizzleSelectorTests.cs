namespace Selenium.WebDriver.Extensions.IntegrationTests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using Xunit;
    using static Extensions.Tests.Shared.Trait.Category;
    using static Extensions.Tests.Shared.Trait.Name;
    using static TestCaseModule;
    using By = By;

    [Trait(CATEGORY, INTEGRATION)]
    [ExcludeFromCodeCoverage]
    public abstract class WebDriverExtensionsSizzleSelectorTests : TestsBase
    {
        protected WebDriverExtensionsSizzleSelectorTests(IWebDriver browser, bool loaded)
            : base(browser, loaded ? SIZZLE_LOADED : SIZZLE_UNLOADED)
        {
        }

        [Fact]
        public void FindElement()
        {
            var sut = By.SizzleSelector("#id1");
            var element = Browser.FindElement(sut);

            element.Should().NotBeNull();
        }

        [Fact]
        public void FindElementThatDoesNotExist()
        {
            var sut = By.SizzleSelector("#id-not");

            void Action() => Browser.FindElement(sut);

            ((Action)Action).Should().Throw<NoSuchElementException>();
        }

        [Fact]
        public void FindElements()
        {
            var sut = By.SizzleSelector("div.main");
            var elements = Browser.FindElements(sut);

            elements.Should().NotBeNull().And.HaveCount(2);
        }

        [Fact]
        public void FindElementsThatDoesNotExist()
        {
            var sut = By.SizzleSelector("div.mainNot");
            var elements = Browser.FindElements(sut);

            elements.Should().NotBeNull().And.HaveCount(0);
        }

        [Fact]
        public void FindInnerElement()
        {
            var root = Browser.FindElement(By.CssSelector("body"));
            var sut = By.SizzleSelector("div");
            var element = root.FindElement(sut);

            element.Should().NotBeNull();
        }

        [Fact]
        public void FindInnerElements()
        {
            var root = Browser.FindElement(By.SizzleSelector("body"));
            var sut = By.SizzleSelector("h1");
            var elements = root.FindElements(sut);

            elements.Should().NotBeNull().And.HaveCount(1);
        }

        [Fact]
        public void ExpectedConditionsSupport()
        {
            var condition = SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.SizzleSelector("h1"));

            var wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(3));
            wait.Until(condition);

            true.Should().BeTrue(); // assert pass
        }
    }
}