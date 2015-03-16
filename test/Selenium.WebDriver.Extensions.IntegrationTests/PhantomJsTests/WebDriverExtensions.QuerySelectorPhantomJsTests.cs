﻿namespace Selenium.WebDriver.Extensions.IntegrationTests
{
    using Xunit;

    [Trait("Category", "Integration")]
    [Trait("Browser", "PhantomJs")]
#if !NET35
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public class WebDriverExtensionsQuerySelectorPhantomJsTests : 
        WebDriverExtensionsQuerySelectorTests, IUseFixture<PhantomJsFixture>
    {
        public void SetFixture(PhantomJsFixture fixture)
        {
            this.Browser = fixture.Browser;
            this.Browser.Navigate().GoToUrl(Properties.Resources.QuerySelectorTestsUrl);
        }
    }
}
