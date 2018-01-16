namespace Selenium.WebDriver.Extensions.IntegrationTests.EdgeTests
{
    using System.Diagnostics.CodeAnalysis;
    using Selenium.WebDriver.Extensions.IntegrationTests.Fixtures;
    using Selenium.WebDriver.Extensions.IntegrationTests.Tests;
    using Selenium.WebDriver.Extensions.Tests;
    using Xunit;
    using static Extensions.Tests.Trait.Browser;
    using static Extensions.Tests.Trait.Category;
    using static Extensions.Tests.Trait.Name;

    [Trait(CATEGORY, INTEGRATION)]
    [Trait(BROWSER, EDGE)]
    [ExcludeFromCodeCoverage]
    [Collection(INTEGRATION)]
    public class WebDriverExtensionsSizzleLoadedSelectorEdgeTests :
        WebDriverExtensionsSizzleSelectorTests, IClassFixture<EdgeFixture>
    {
        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        public WebDriverExtensionsSizzleLoadedSelectorEdgeTests(EdgeFixture fixture)
            : base(fixture.Browser, true)
        {
        }
    }
}