namespace Selenium.WebDriver.Extensions.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Nancy.Hosting.Self;
    using OpenQA.Selenium;

    [ExcludeFromCodeCoverage]
    public abstract class TestsBase : IDisposable
    {
        private readonly NancyHost _host;
        private bool _disposed;

        protected TestsBase(IWebDriver browser, string path)
        {
            var config = new HostConfiguration { UrlReservations = { CreateAutomatically = true } };

            const string serverUrl = "http://localhost:50502";
            _host = new NancyHost(config, new Uri(serverUrl));
            _host.Start();

            Browser = browser;
            Browser.Navigate().GoToUrl(new Uri($"{serverUrl}{path}"));
        }

        ~TestsBase() => Dispose(false);

        protected IWebDriver Browser { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
            {
                return;
            }

            _host.Dispose();
            _disposed = true;
        }
    }
}