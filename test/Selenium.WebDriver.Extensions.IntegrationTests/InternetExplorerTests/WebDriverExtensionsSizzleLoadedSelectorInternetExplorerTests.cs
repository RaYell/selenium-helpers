namespace Selenium.WebDriver.Extensions.IntegrationTests.InternetExplorerTests
{
    using Selenium.WebDriver.Extensions.IntegrationTests.Fixtures;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;
    using static By;
    using static Tests.Shared.Trait;

    [Trait(CATEGORY, INTEGRATION)]
    [Trait(BROWSER, INTERNET_EXPLORER)]
    [ExcludeFromCodeCoverage]
    [Collection(INTERNET_EXPLORER)]
    public class WebDriverExtensionsSizzleLoadedSelectorInternetExplorerTests : SelectorTests<SizzleSelector>
    {
        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
        public WebDriverExtensionsSizzleLoadedSelectorInternetExplorerTests(InternetExplorerFixture fixture)
            : base(fixture, "/SizzleLoaded", x => SizzleSelector(x))
        {
        }
    }
}
