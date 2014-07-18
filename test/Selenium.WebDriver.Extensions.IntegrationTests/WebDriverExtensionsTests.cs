﻿namespace Selenium.WebDriver.Extensions.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;
    using By = Selenium.WebDriver.Extensions.By;

    /// <summary>
    /// JQuery selector tests.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class WebDriverExtensionsTests
    {
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
            this.Browser = new FirefoxDriver();
            
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo == null)
            {
                return;
            }

            var uri = new Uri(directoryInfo.FullName + Path.DirectorySeparatorChar + "TestCase.html");
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
            var element = this.Browser.FindElement(By.JQuerySelector("#id1"));
            Assert.IsNotNull(element);
        }

        /// <summary>
        /// Tests finding element by ID that doesn't exist.
        /// </summary>
        [Test]
        [ExpectedException(typeof(NoSuchElementException))]
        public void FindElementThatDoesntExist()
        {
            this.Browser.FindElement(By.JQuerySelector("#id-not"));
        }

        /// <summary>
        /// Tests finding element by class.
        /// </summary>
        [Test]
        public void FindElements()
        {
            var elements = this.Browser.FindElements(By.JQuerySelector("div.main"));
            Assert.AreEqual(2, elements.Count);
        }

        /// <summary>
        /// Tests finding element by class that doesn't exist.
        /// </summary>
        [Test]
        public void FindElementsThatDoesntExist()
        {
            var elements = this.Browser.FindElements(By.JQuerySelector("div.mainNot"));
            Assert.AreEqual(0, elements.Count);
        }

        /// <summary>
        /// Tests finding element text.
        /// </summary>
        [Test]
        public void FindText()
        {
            var text = this.Browser.FindText(By.JQuerySelector("#id1"));
            var trimmedText = text.Replace(Environment.NewLine, string.Empty).Trim();
            StringAssert.StartsWith("jQuery", trimmedText);
            StringAssert.EndsWith("Selenium", trimmedText);
        }

        /// <summary>
        /// Tests finding element HTML.
        /// </summary>
        [Test]
        public void FindHtml()
        {
            var text = this.Browser.FindHtml(By.JQuerySelector("#id1"));
            var trimmedText = text.Replace(Environment.NewLine, string.Empty).Trim();
            StringAssert.StartsWith("<span>jQuery</span>", trimmedText);
            StringAssert.EndsWith("<span>Selenium</span>", trimmedText);
        }

        /// <summary>
        /// Tests finding element attribute.
        /// </summary>
        [Test]
        public void FindAttribute()
        {
            var attribute = this.Browser.FindAttribute(By.JQuerySelector("input:first"), "type");
            Assert.AreEqual("checkbox", attribute);
        }

        /// <summary>
        /// Tests finding element attribute that doesn't exist.
        /// </summary>
        [Test]
        public void FindAttributeThatDoesntExist()
        {
            var attribute = this.Browser.FindAttribute(By.JQuerySelector("input:first"), "typeNot");
            Assert.IsNull(attribute);
        }

        /// <summary>
        /// Tests finding element property.
        /// </summary>
        [Test]
        public void FindProperty()
        {
            var property = this.Browser.FindProperty(By.JQuerySelector("input:first"), "checked");
            Assert.IsNotNull(property);
            Assert.IsTrue(property.Value);
        }

        /// <summary>
        /// Tests finding element property that doesn't exist.
        /// </summary>
        [Test]
        public void FindPropertyThatDoesntExist()
        {
            var property = this.Browser.FindProperty(By.JQuerySelector("input:first"), "checkedNot");
            Assert.IsNull(property);
        }

        /// <summary>
        /// Tests finding element value.
        /// </summary>
        [Test]
        public void FindValue()
        {
            var value = this.Browser.FindValue(By.JQuerySelector("input:first"));
            Assert.AreEqual("test-val", value);
        }

        /// <summary>
        /// Tests finding element value that doesn't exist.
        /// </summary>
        [Test]
        public void FindValueThatDoesntExist()
        {
            var value = this.Browser.FindValue(By.JQuerySelector("form"));
            Assert.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Tests finding element CSS.
        /// </summary>
        [Test]
        public void FindCss()
        {
            var value = this.Browser.FindCss(By.JQuerySelector("#id1"), "background-color");
            Assert.AreEqual("rgb(0, 128, 0)", value);
        }

        /// <summary>
        /// Tests finding element CSS.
        /// </summary>
        [Test]
        public void FindCssThatDoesntExist()
        {
            var value = this.Browser.FindCss(By.JQuerySelector("#id1"), "test");
            Assert.IsNull(value);
        }

        /// <summary>
        /// Tests finding element width.
        /// </summary>
        [Test]
        public void FindWidth()
        {
            var value = this.Browser.FindWidth(By.JQuerySelector("h1"));
            Assert.AreEqual(200, value);
        }

        /// <summary>
        /// Tests finding element height.
        /// </summary>
        [Test]
        public void FindHeight()
        {
            var value = this.Browser.FindHeight(By.JQuerySelector("h1"));
            Assert.AreEqual(20, value);
        }

        /// <summary>
        /// Tests finding element inner width.
        /// </summary>
        [Test]
        public void FindInnerWidth()
        {
            var value = this.Browser.FindInnerWidth(By.JQuerySelector("h1"));
            Assert.AreEqual(220, value);
        }

        /// <summary>
        /// Tests finding element inner height.
        /// </summary>
        [Test]
        public void FindInnerHeight()
        {
            var value = this.Browser.FindInnerHeight(By.JQuerySelector("h1"));
            Assert.AreEqual(40, value);
        }

        /// <summary>
        /// Tests finding element outer width.
        /// </summary>
        [Test]
        public void FindOuterWidth()
        {
            var value = this.Browser.FindOuterWidth(By.JQuerySelector("h1"));
            Assert.AreEqual(226, value);
        }

        /// <summary>
        /// Tests finding element outer height.
        /// </summary>
        [Test]
        public void FindOuterHeight()
        {
            var value = this.Browser.FindOuterHeight(By.JQuerySelector("h1"));
            Assert.AreEqual(46, value);
        }

        /// <summary>
        /// Tests finding element outer width with margin.
        /// </summary>
        [Test]
        public void FindOuterWidthWithMargin()
        {
            var value = this.Browser.FindOuterWidth(By.JQuerySelector("h1"), true);
            Assert.AreEqual(236, value);
        }

        /// <summary>
        /// Tests finding element outer height with margin.
        /// </summary>
        [Test]
        public void FindOuterHeightWithMargin()
        {
            var value = this.Browser.FindOuterHeight(By.JQuerySelector("h1"), true);
            Assert.AreEqual(56, value);
        }

        /// <summary>
        /// Tests finding element width that doesn't exist.
        /// </summary>
        [Test]
        public void FindWidthThatDoesntExist()
        {
            var value = this.Browser.FindWidth(By.JQuerySelector("h6"));
            Assert.IsNull(value);
        }

        /// <summary>
        /// Tests finding element height that doesn't exist.
        /// </summary>
        [Test]
        public void FindHeightThatDoesntExist()
        {
            var value = this.Browser.FindHeight(By.JQuerySelector("h6"));
            Assert.IsNull(value);
        }

        /// <summary>
        /// Tests finding element inner width that doesn't exist.
        /// </summary>
        [Test]
        public void FindInnerWidthThatDoesntExist()
        {
            var value = this.Browser.FindInnerWidth(By.JQuerySelector("h6"));
            Assert.IsNull(value);
        }

        /// <summary>
        /// Tests finding element inner height that doesn't exist.
        /// </summary>
        [Test]
        public void FindInnerHeightThatDoesntExist()
        {
            var value = this.Browser.FindInnerHeight(By.JQuerySelector("h6"));
            Assert.IsNull(value);
        }

        /// <summary>
        /// Tests finding element outer width that doesn't exist.
        /// </summary>
        [Test]
        public void FindOuterWidthThatDoesntExist()
        {
            var value = this.Browser.FindOuterWidth(By.JQuerySelector("h6"));
            Assert.IsNull(value);
        }

        /// <summary>
        /// Tests finding element outer height that doesn't exist.
        /// </summary>
        [Test]
        public void FindOuterHeightThatDoesntExist()
        {
            var value = this.Browser.FindOuterHeight(By.JQuerySelector("h6"));
            Assert.IsNull(value);
        }

        /// <summary>
        /// Tests finding element outer width with margin that doesn't exist.
        /// </summary>
        [Test]
        public void FindOuterWidthWithMarginThatDoesntExist()
        {
            var value = this.Browser.FindOuterWidth(By.JQuerySelector("h6"), true);
            Assert.IsNull(value);
        }

        /// <summary>
        /// Tests finding element outer height with margin that doesn't exist.
        /// </summary>
        [Test]
        public void FindOuterHeightWithMarginThatDoesntExist()
        {
            var value = this.Browser.FindOuterHeight(By.JQuerySelector("h6"), true);
            Assert.IsNull(value);
        }
    }
}
