﻿namespace Selenium.WebDriver.Extensions.Sizzle.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using Selenium.WebDriver.Extensions.Core;
    using Selenium.WebDriver.Extensions.Sizzle;
    using By = Selenium.WebDriver.Extensions.Sizzle.By;

    [TestFixture]
    [Category("Unit Tests")]
#if !NET35
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public class WebDriverExtensionsTests
    {
        private static IEnumerable LoadSizzleTestCases
        {
            get
            {
                yield return new TestCaseData("master", null, new object[] { true });
                yield return new TestCaseData("1.11.1", null, new object[] { false, true, true });
                yield return new TestCaseData("master", TimeSpan.FromSeconds(1), new object[] { false })
                    .Throws(typeof(WebDriverTimeoutException));
            }
        }

        private static IEnumerable LoadSizzleWithUriTestCases
        {
            get
            {
                yield return new TestCaseData(new Uri("http://my.com/sizzle.js"), null, new object[] { true });
                yield return new TestCaseData(null, null, new object[] { false, true, true });
                yield return new TestCaseData(
                    new Uri("http://my.com/sizzle.js"),
                    TimeSpan.FromSeconds(1),
                    new object[] { false }).Throws(typeof(WebDriverTimeoutException));
            }
        }

        [TestCaseSource("LoadSizzleTestCases")]
        public void LoadSizzle(string version, TimeSpan? timeout, IEnumerable<object> mockValueSequence)
        {
            var mock = new Mock<IWebDriver>();
            var sequence = mock.As<IJavaScriptExecutor>().SetupSequence(x => x.ExecuteScript(It.IsAny<string>()));
            mockValueSequence.Aggregate(sequence, (current, mockValue) => current.Returns(mockValue));
            mock.Object.Sizzle().Load(version, timeout);
        }

        [TestCaseSource("LoadSizzleWithUriTestCases")]
        public void LoadSizzleWithUri(Uri sizzleUri, TimeSpan? timeout, IEnumerable<object> mockValueSequence)
        {
            var mock = new Mock<IWebDriver>();
            var sequence = mock.As<IJavaScriptExecutor>().SetupSequence(x => x.ExecuteScript(It.IsAny<string>()));
            mockValueSequence.Aggregate(sequence, (current, mockValue) => current.Returns(mockValue));
            mock.Object.Sizzle().Load(sizzleUri, timeout);
        }

        [Test]
        public void FindElementWithSizzle()
        {
            var element = new Mock<IWebElement>();
            element.Setup(x => x.TagName).Returns("div");
            var list = new List<IWebElement> { element.Object };
            var mock = MockWebDriver("return Sizzle('div');", new ReadOnlyCollection<IWebElement>(list));
            var result = mock.Object.FindElement(By.SizzleSelector("div"));

            Assert.IsNotNull(result);
            Assert.AreEqual("div", result.TagName);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindElementWithSizzleArgumentNull()
        {
            var mock = new Mock<IWebDriver>();
            mock.Object.FindElement((SizzleSelector)null);
        }

        [Test]
        [ExpectedException(typeof(NoSuchElementException))]
        public void FindElementWithSizzleNoSuchElement()
        {
            var mock = MockWebDriver();

            mock.Object.FindElement(By.SizzleSelector("div"));
        }

        [Test]
        [ExpectedException(typeof(NoSuchElementException))]
        public void FindElementWithSizzleNoSuchElementEmptyResult()
        {
            var mock = MockWebDriver("return Sizzle('div');", Enumerable.Empty<IWebElement>());

            mock.Object.FindElement(By.SizzleSelector("div"));
        }

        [Test]
        public void FindElementsWithSizzle()
        {
            var element1 = new Mock<IWebElement>();
            element1.Setup(x => x.TagName).Returns("div");
            element1.Setup(x => x.GetAttribute("class")).Returns("test");

            var element2 = new Mock<IWebElement>();
            element2.Setup(x => x.TagName).Returns("span");
            element2.Setup(x => x.GetAttribute("class")).Returns("test");

            var list = new List<IWebElement> { element1.Object, element2.Object };
            var mock = MockWebDriver("return Sizzle('.test');", new ReadOnlyCollection<IWebElement>(list));
            var result = mock.Object.FindElements(By.SizzleSelector(".test"));

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual("div", result[0].TagName);
            Assert.AreEqual("test", result[0].GetAttribute("class"));

            Assert.AreEqual("span", result[1].TagName);
            Assert.AreEqual("test", result[1].GetAttribute("class"));
        }

        [Test]
        public void FindElementsWithSizzleNotExists()
        {
            var list = new List<object>();
            var mock = MockWebDriver("return Sizzle('.test');", new ReadOnlyCollection<object>(list));
            var result = mock.Object.FindElements(By.SizzleSelector(".test"));

            Assert.AreEqual(0, result.Count);
        }

        private static Mock<IWebDriver> MockWebDriver(string script = null, object value = null)
        {
            var mock = new Mock<IWebDriver>();
            if (script != null)
            {
                mock.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(script)).Returns(value);
            }

            mock.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript("return typeof window.Sizzle === 'function';"))
                .Returns(true);
            return mock;
        }
    }
}
