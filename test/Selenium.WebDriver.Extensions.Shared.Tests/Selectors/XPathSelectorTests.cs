﻿namespace Selenium.WebDriver.Extensions.Shared.Tests
{
    using System;
    using System.Collections;
    using NUnit.Framework;

    /// <summary>
    /// XPATH selector tests.
    /// </summary>
    [TestFixture]
    [Category("Unit Tests")]
#if !NET35
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public class XPathSelectorTests
    {
        /// <summary>
        /// Gets the equality test cases.
        /// </summary>
        private static IEnumerable EqualityTestCases
        {
            get
            {
                yield return new TestCaseData(By.XPath("html"), By.XPath("html"), true)
                    .SetName("XP('test') == XP('test')");
                yield return new TestCaseData(By.XPath("html"), By.XPath("body"), false)
                    .SetName("XP('test') != XP('test2')");
                yield return new TestCaseData(By.XPath("html"), null, false)
                    .SetName("XP('test') != null");
                yield return new TestCaseData(null, By.XPath("html"), false)
                    .SetName("null != XP('test')");
            }
        }

        /// <summary>
        /// Tests if the proper selector is generated.
        /// </summary>
        /// <returns>The generated query selector.</returns>
        [Test]
        public void Selector()
        {
            var selector = By.XPath("html");
            Assert.AreEqual(selector.Selector, selector.ToString());
        }

        /// <summary>
        /// Tests if the null selector is handled properly.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSelector()
        {
            By.XPath(null);
        }

        /// <summary>
        /// Tests if the call format string is handled properly.
        /// </summary>
        [Test]
        public void CallFormatString()
        {
            var formatString = By.XPath("html").CallFormatString;
            Assert.IsNotNull(formatString);
        }

        /// <summary>
        /// Tests the equality operators.
        /// </summary>
        /// <param name="selector1">First selector to compare.</param>
        /// <param name="selector2">Second selector to compare.</param>
        /// <param name="expectedResult">The expected resuXP.</param>
        [TestCaseSource("EqualityTestCases")]
        public void EqualityOperator(XPathSelector selector1, XPathSelector selector2, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, selector1 == selector2);
            if (selector1 != null)
            {
                Assert.AreEqual(expectedResult, selector1.Equals(selector2));
                if (selector2 != null)
                {
                    Assert.AreEqual(expectedResult, selector1.GetHashCode() == selector2.GetHashCode());
                }
            }

            Assert.AreNotEqual(expectedResult, selector1 != selector2);
        }

        /// <summary>
        /// Tests the equality operators.
        /// </summary>
        [Test]
        public void EqualityOperatorWrongType()
        {
            var selector1 = By.XPath("text");
            var selector2 = new object();

#pragma warning disable 252,253
            Assert.IsFalse(selector1 == selector2);
            Assert.IsTrue(selector1 != selector2);
#pragma warning restore 252,253
        }
    }
}
