namespace Selenium.WebDriver.Extensions.IntegrationTests.EdgeTests
{
    using System.Diagnostics.CodeAnalysis;
    using Selenium.WebDriver.Extensions.IntegrationTests.Fixtures;
    using Selenium.WebDriver.Extensions.IntegrationTests.Tests;
    using Xunit;
    using static Extensions.Tests.Shared.Trait.Browser;
    using static Extensions.Tests.Shared.Trait.Category;
    using static Extensions.Tests.Shared.Trait.Name;

    [Trait(CATEGORY, INTEGRATION)]
    [Trait(BROWSER, EDGE)]
    [ExcludeFromCodeCoverage]
    [Collection(INTEGRATION)]
    public class WebDriverExtensionsJQueryUnloadedSelectorEdgeTests :
        WebDriverExtensionsJQuerySelectorTests, IClassFixture<EdgeFixture>
    {
        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        public WebDriverExtensionsJQueryUnloadedSelectorEdgeTests(EdgeFixture fixture)
            : base(fixture.Browser, false)
        {
        }
    }
}