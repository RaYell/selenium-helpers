namespace Selenium.WebDriver.Extensions.IntegrationTests.Fixtures
{
    using JetBrains.Annotations;
    using OpenQA.Selenium.Chrome;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class ChromeFixture : FixtureBase
    {
        public ChromeFixture()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            Browser = new ChromeDriver(Path.GetDirectoryName(typeof(ChromeFixture).Assembly.Location), options);
        }
    }
}
