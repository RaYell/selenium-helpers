﻿namespace Selenium.WebDriver.Extensions.JQuery.Tests
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Moq;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using Selenium.WebDriver.Extensions.Core;
    using By = Selenium.WebDriver.Extensions.JQuery.By;

    [TestFixture]
    [Category("Unit Tests")]
#if !NET35
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public class WebElementExtensionsTests
    {
        [Test]
        public void FindElementWithJQuery()
        {
            var selector = By.JQuerySelector("div");
            
            var rootElement = new Mock<IWebElement>();
            rootElement.SetupGet(x => x.TagName).Returns("div");

            var element = new Mock<IWebElement>();
            element.SetupGet(x => x.TagName).Returns("span");

            var list = new List<IWebElement> { rootElement.Object };
            var driver = MockWebDriver("return jQuery('div').get();", new ReadOnlyCollection<IWebElement>(list));
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");
            var elementList = new List<IWebElement> { element.Object };
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript("return jQuery('span', jQuery('body > div')).get();"))
                .Returns(new ReadOnlyCollection<IWebElement>(elementList));
            
            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.FindElement(By.JQuerySelector("span"));

            Assert.IsNotNull(result);
            Assert.AreEqual("span", result.TagName);
        }

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

            var list = new List<IWebElement> { rootElement.Object };
            var driver = MockWebDriver("return jQuery('div').get();", new ReadOnlyCollection<IWebElement>(list));
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");
            var elementList = new List<IWebElement> { element1.Object, element2.Object };
            driver.As<IJavaScriptExecutor>()
                .Setup(x => x.ExecuteScript("return jQuery('span', jQuery('body > div')).get();"))
                .Returns(new ReadOnlyCollection<IWebElement>(elementList));

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

        [Test]
        public void FindText()
        {
            const string Result = "test";

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('span', jQuery('body > div')).text();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");
            
            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("span").Text();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindHtml()
        {
            const string Result = "<p>test</p>";

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('span', jQuery('body > div')).html();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");
            
            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("span").Html();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindAttribute()
        {
            const string Result = "http://github.com";
            
            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('a', jQuery('body > div')).attr('href');", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");
            
            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("a").Attribute("href");

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindPropertyString()
        {
            const string Result = "prop";
            
            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).prop('checked');", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").Property<string>("checked");

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindPropertyBoolean()
        {
            const bool Result = true;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).prop('checked');", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").Property("checked");

            Assert.IsNotNull(result);
            Assert.AreEqual(Result, result.Value);
        }

        [Test]
        public void FindValue()
        {
            const string Result = "test";

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).val();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").Value();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindCss()
        {
            const string Result = "hidden";

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).css('display');", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").Css("display");

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindWidth()
        {
            const long Result = 100;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).width();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").Width();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindHeight()
        {
            const long Result = 100;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).height();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").Height();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindInnerWidth()
        {
            const long Result = 100;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).innerWidth();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").InnerWidth();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindInnerHeight()
        {
            const long Result = 100;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).innerHeight();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").InnerHeight();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindOuterWidth()
        {
            const long Result = 100;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).outerWidth();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").OuterWidth();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindOuterHeight()
        {
            const long Result = 100;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).outerHeight();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").OuterHeight();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindOuterWidthWithMargin()
        {
            const long Result = 100;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).outerWidth(true);", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").OuterWidth(true);

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindOuterHeightWithMargin()
        {
            const long Result = 100;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).outerHeight(true);", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").OuterHeight(true);

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindPosition()
        {
            var dict = new Dictionary<string, object> { { "top", 100 }, { "left", 200 } };

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).position();", dict);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var position = webElement.Object.JQuery("input").Position();
            if (position == null)
            {
                Assert.Fail();
            }

            Assert.AreEqual(dict["top"], position.Value.Top);
            Assert.AreEqual(dict["left"], position.Value.Left);
        }

        [Test]
        public void FindOffset()
        {
            var dict = new Dictionary<string, object> { { "top", 100 }, { "left", 200 } };

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).offset();", dict);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var offset = webElement.Object.JQuery("input").Offset();
            if (offset == null)
            {
                Assert.Fail();
            }

            Assert.AreEqual(dict["top"], offset.Value.Top);
            Assert.AreEqual(dict["left"], offset.Value.Left);
        }

        [Test]
        public void FindScrollLeft()
        {
            const long Result = 100;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).scrollLeft();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").ScrollLeft();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindScrollTop()
        {
            const long Result = 100;
            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).scrollTop();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").ScrollTop();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindData()
        {
            const string Result = "val";

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).data('test');", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").Data("test");

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindIntData()
        {
            const bool Result = true;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).data('test');", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").Data<bool?>("test");

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindCount()
        {
            const long Result = 2;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('input', jQuery('body > div')).length;", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("input").Count();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindSerialized()
        {
            const string Result = "search=test";

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver("return jQuery('form', jQuery('body > div')).serialize();", Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("form").Serialized();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void FindSerializedArray()
        {
            const string Result = "[{\"name\":\"s\",\"value\":\"\"}]";

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver(
                "return JSON.stringify(jQuery('form', jQuery('body > div')).serializeArray());",
                Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("form").SerializedArray();

            Assert.AreEqual(Result, result);
        }

        [Test]
        public void HasClass()
        {
            const bool Result = true;

            var selector = By.QuerySelector("div");
            var driver = MockWebDriver(
                "return jQuery('form', jQuery('body > div')).hasClass('test');",
                Result);
            driver.As<IJavaScriptExecutor>().Setup(x => x.ExecuteScript(It.IsRegex("function\\(el\\)")))
                .Returns("body > div");

            var webElement = new Mock<WebElement>();
            webElement.SetupGet(x => x.TagName).Returns("div");
            webElement.SetupGet(x => x.Selector).Returns(selector);
            webElement.SetupGet(x => x.WrappedDriver).Returns(driver.Object);

            var result = webElement.Object.JQuery("form").HasClass("test");

            Assert.AreEqual(Result, result);
        }

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
            return mock;
        }
    }
}
