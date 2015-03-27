﻿namespace Selenium.WebDriver.Extensions.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [Trait("Category", "Integration")]
    [Trait("Browser", "Chrome")]
    [ExcludeFromCodeCoverage]
    public class WebDriverExtensionsQuerySelectorChromeTests : 
        WebDriverExtensionsQuerySelectorTests, IClassFixture<ChromeFixture>
    {
        public WebDriverExtensionsQuerySelectorChromeTests(ChromeFixture fixture)
        {
            this.Browser = fixture.Browser;
            this.Browser.Navigate().GoToUrl(new Uri(Properties.Resources.QuerySelectorTestsUrl));
        }
    }
}
