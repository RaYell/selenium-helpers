﻿namespace Selenium.WebDriver.Extensions.Tests
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Moq;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using Selenium.WebDriver.Extensions.JQuery;
    using Selenium.WebDriver.Extensions.Shared;
    using By = Selenium.WebDriver.Extensions.By;
    
    /// <summary>
    /// Web element extensions tests.
    /// </summary>
    [TestFixture]
    [Category("Unit Tests")]
#if !NET35
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public class WebElementExtensionsTests
    {
        /// <summary>
        /// Tests finding an element.
        /// </summary>
        [Test]
        public void GetPath()
        {
            var selector = new Mock<ISelector>();
            selector.SetupGet(x => x.Selector).Returns("div");
            selector.SetupGet(x => x.CallFormatString).Returns("{0}[{1}]");

            var driver = new Mock<IWebDriver>();
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("getDomPath")))
                .Returns("body > div");

            var element = new Mock<WebElement>();
            element.SetupGet(x => x.Selector).Returns(selector.Object);
            element.SetupGet(x => x.WrappedDriver).Returns(driver.Object);
            
            var path = element.Object.GetPath();
            Assert.AreEqual("body > div", path);
        }

        /// <summary>
        /// Tests finding an element.
        /// </summary>
        [Test]
        public void FindElementWithJQuery()
        {
            var selector = By.JQuerySelector("div");
            
            var rootElement = new Mock<IWebElement>();
            rootElement.SetupGet(x => x.TagName).Returns("div");

            var element = new Mock<IWebElement>();
            element.SetupGet(x => x.TagName).Returns("span");

            var driver = MockWebDriver("return jQuery('div').get(0);", rootElement.Object);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("getDomPath")))
                .Returns("body > div");
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript("return jQuery('span', jQuery('body > div')).get(0);"))
                .Returns(element.Object);
            
            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.FindElement(By.JQuerySelector("span"));

            Assert.IsNotNull(result);
            Assert.AreEqual("span", result.TagName);
        }

        /// <summary>
        /// Tests finding elements.
        /// </summary>
        [Test]
        public void FindElementsWithJQuery()
        {
            var selector = By.JQuerySelector("div");

            var rootElement = new Mock<IWebElement>();
            rootElement.SetupGet(x => x.TagName).Returns("div");

            var element1 = new Mock<IWebElement>();
            element1.Setup(x => x.TagName).Returns("span");
            element1.Setup(x => x.GetAttribute("class")).Returns("test1");

            var element2 = new Mock<IWebElement>();
            element2.Setup(x => x.TagName).Returns("span");
            element2.Setup(x => x.GetAttribute("class")).Returns("test2");

            var list = new List<IWebElement> { element1.Object, element2.Object };
            var driver = MockWebDriver("return jQuery('div').get(0);", rootElement.Object);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("getDomPath")))
                .Returns("body > div");
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript("return jQuery('span', jQuery('body > div')).get();"))
                .Returns(new ReadOnlyCollection<IWebElement>(list));

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.FindElements(By.JQuerySelector("span"));

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual("span", result[0].TagName);
            Assert.AreEqual("test1", result[0].GetAttribute("class"));

            Assert.AreEqual("span", result[1].TagName);
            Assert.AreEqual("test2", result[1].GetAttribute("class"));
        }

        /// <summary>
        /// Mocks the Selenium web driver.
        /// </summary>
        /// <param name="script">Script to mock to return value.</param>
        /// <param name="value">A value to return by the script.</param>
        /// <returns>Mocked Selenium web driver.</returns>
        private static Mock<IWebDriver> MockWebDriver(string script = null, object value = null)
        {
            var mock = new Mock<IWebDriver>();
            if (script != null)
            {
                mock.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(script)).Returns(value);
            }

            mock.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript("return typeof window.jQuery === 'function';"))
                .Returns(true);
            mock.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript("return typeof window.Sizzle === 'function';"))
                .Returns(true);
            mock.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript("return typeof document.querySelectorAll === 'function';")).Returns(true);
            return mock;
        }
    }
}
