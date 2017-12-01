namespace Selenium.WebDriver.Extensions.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using OpenQA.Selenium;
    using OpenQA.Selenium.IE;

    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class InternetExplorerFixture : IDisposable
    {
        public InternetExplorerFixture() => Browser = new InternetExplorerDriver();

        public IWebDriver Browser { get; }

        public void Dispose()
        {
            Browser?.Quit();
            Browser?.Dispose();
        }
    }
}
