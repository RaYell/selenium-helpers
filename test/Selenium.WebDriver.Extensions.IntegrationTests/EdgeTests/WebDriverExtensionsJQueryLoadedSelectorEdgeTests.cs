namespace Selenium.WebDriver.Extensions.IntegrationTests.EdgeTests
{
    using Selenium.WebDriver.Extensions.IntegrationTests.Fixtures;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;
    using static By;
    using static TestCaseModule;
    using static Tests.Shared.Trait;

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
