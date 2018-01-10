namespace Selenium.WebDriver.Extensions.IntegrationTests.FirefoxTests
{
    using Selenium.WebDriver.Extensions.IntegrationTests.Fixtures;
    using Selenium.WebDriver.Extensions.IntegrationTests.Tests;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [Trait("Category", "Integration")]
    [Trait("Browser", "Firefox")]
    [ExcludeFromCodeCoverage]
    [Collection("Integration")]
    public class WebDriverExtensionsJQueryUnloadedSelectorFirefoxTests :
        WebDriverExtensionsJQuerySelectorTests, IClassFixture<FirefoxFixture>
    {
        public WebDriverExtensionsJQueryUnloadedSelectorFirefoxTests(FixtureBase fixture)
        {
            Browser = fixture.Browser;
            Browser.Navigate().GoToUrl(new Uri($"{ServerUrl}/JQueryUnloaded"));
        }
    }
}