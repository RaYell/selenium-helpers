namespace Selenium.WebDriver.Extensions.IntegrationTests.EdgeTests
{
    using System.Diagnostics.CodeAnalysis;
    using Selenium.WebDriver.Extensions.IntegrationTests;
    using Selenium.WebDriver.Extensions.IntegrationTests.Fixtures;
    using Selenium.WebDriver.Extensions.Tests.Shared;
    using Xunit;
    using static Selenium.WebDriver.Extensions.By;
    using static Selenium.WebDriver.Extensions.IntegrationTests.TestCaseModule;
    using static Selenium.WebDriver.Extensions.Tests.Shared.Trait;

    [Trait(CATEGORY, INTEGRATION)]
    [Trait(BROWSER, EDGE)]
    [ExcludeFromCodeCoverage]
    [Collection(EDGE)]
    public class WebDriverExtensionsJQueryLoadedSelectorEdgeTests : SelectorTests<JQuerySelector>
    {
        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        public WebDriverExtensionsJQueryLoadedSelectorEdgeTests(EdgeFixture fixture)
            : base(fixture, JQUERY_LOADED, x => JQuerySelector(x))
        {
        }
    }
}
