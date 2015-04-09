﻿namespace Selenium.WebDriver.Extensions.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [Trait("Category", "Integration")]
    [Trait("Browser", "PhantomJs")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    public class WebDriverExtensionsSizzleUnloadedSelectorPhantomJsTests :
        WebDriverExtensionsSizzleSelectorTests, IClassFixture<PhantomJsFixture>
    {
        public WebDriverExtensionsSizzleUnloadedSelectorPhantomJsTests(PhantomJsFixture fixture)
        {
            this.Browser = fixture.Browser;
            this.Browser.Navigate().GoToUrl(new Uri(Properties.Resources.SizzleUnloadedTestsUrl));
        }
    }
}
