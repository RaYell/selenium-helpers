﻿namespace Selenium.WebDriver.Extensions.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    [ExcludeFromCodeCoverage]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class FirefoxFixture : IDisposable
    {
        private readonly FirefoxDriverService _service;
        private bool _disposed;

        public FirefoxFixture()
        {
            _service = FirefoxDriverService.CreateDefaultService();
            _service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            Browser = new FirefoxDriver(_service);
        }

        ~FirefoxFixture()
        {
            Dispose(false);
        }

        public IWebDriver Browser { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SuppressMessage(
            "Microsoft.Usage",
            "CA2213:DisposableFieldsShouldBeDisposed",
            MessageId = "<Browser>k__BackingField")]
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
            {
                return;
            }

            Browser.Dispose();
            _service.Dispose();
            _disposed = true;
        }
    }
}
