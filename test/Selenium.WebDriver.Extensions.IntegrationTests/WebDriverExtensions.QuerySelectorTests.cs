﻿namespace Selenium.WebDriver.Extensions.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.PhantomJS;
    using By = Selenium.WebDriver.Extensions.By;

    /// <summary>
    /// Query selector tests.
    /// </summary>
    /// <remarks>
    /// In order for IE tests to run it must allow local files to use scripts. You can enable that by going to
    /// Tools > Internet Options > Advanced > Security > Allow active content to run in files on My Computer.
    /// </remarks>
    [TestFixture("PhantomJS", "TestCases\\QuerySelector\\TestCase.html")]
    [TestFixture("Firefox", "TestCases\\QuerySelector\\TestCase.html")]
    [TestFixture("Chrome", "TestCases\\QuerySelector\\TestCase.html")]
    [TestFixture("IE", "TestCases\\QuerySelector\\TestCase.html")]
    [Category("Integration Tests")]
    [ExcludeFromCodeCoverage]
    public class WebDriverExtensionsQuerySelectorTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebDriverExtensionsQuerySelectorTests"/> class.
        /// </summary>
        /// <param name="driverName">The web driver name.</param>
        /// <param name="testCaseFileName">The test case file name.</param>
        public WebDriverExtensionsQuerySelectorTests(string driverName, string testCaseFileName)
        {
            this.DriverName = driverName;
            this.TestCaseFileName = testCaseFileName;
        }

        /// <summary>
        /// Gets or sets the driver name.
        /// </summary>
        private string DriverName { get; set; }

        /// <summary>
        /// Gets or sets the test case file name.
        /// </summary>
        private string TestCaseFileName { get; set; }

        /// <summary>
        /// Gets or sets the selenium web driver.
        /// </summary>
        private IWebDriver Browser { get; set; }
        
        /// <summary>
        /// Sets up the test fixture.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null)
            {
                return;
            }

#if DEBUG
            const string BuildConfig = "Debug";
#else
            const string BuildConfig = "Release";
#endif

            var driversPath = directoryInfo.FullName + Path.DirectorySeparatorChar + "bin"
                + Path.DirectorySeparatorChar + BuildConfig
                + Path.DirectorySeparatorChar + "Drivers"
                + Path.DirectorySeparatorChar;

            switch (this.DriverName)
            {
                case "PhantomJS":
                    var phantomJsService = PhantomJSDriverService.CreateDefaultService(driversPath);
                    phantomJsService.IgnoreSslErrors = true;
                    phantomJsService.SslProtocol = "any";
                    this.Browser = new PhantomJSDriver(phantomJsService);
                    break;
                case "Firefox":
                    this.Browser = new FirefoxDriver();
                    break;
                case "Chrome":
                    this.Browser = new ChromeDriver(driversPath);
                    break;
                case "IE":
                    this.Browser = new InternetExplorerDriver(driversPath);
                    break;
                default:
                    return;
            }

            var uri = new Uri(directoryInfo.FullName + Path.DirectorySeparatorChar + this.TestCaseFileName);
            this.Browser.Navigate().GoToUrl(uri.AbsoluteUri);
        }

        /// <summary>
        /// Tears down the test fixture.
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            this.Browser.Dispose();
        }

        /// <summary>
        /// Tests finding element by ID.
        /// </summary>
        [Test]
        public void FindElement()
        {
            var element = this.Browser.FindElement(By.QuerySelector("#id1"));
            Assert.IsNotNull(element);
        }

        /// <summary>
        /// Tests finding element by ID that doesn't exist.
        /// </summary>
        [Test]
        [ExpectedException(typeof(NoSuchElementException))]
        public void FindElementThatDoesntExist()
        {
            this.Browser.FindElement(By.QuerySelector("#id-not"));
        }

        /// <summary>
        /// Tests finding element by class.
        /// </summary>
        [Test]
        public void FindElements()
        {
            var elements = this.Browser.FindElements(By.QuerySelector("div.main"));
            Assert.AreEqual(2, elements.Count);
        }

        /// <summary>
        /// Tests finding element by class that doesn't exist.
        /// </summary>
        [Test]
        public void FindElementsThatDoesntExist()
        {
            var elements = this.Browser.FindElements(By.QuerySelector("div.mainNot"));
            Assert.AreEqual(0, elements.Count);
        }
    }
}
