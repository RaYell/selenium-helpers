﻿namespace Selenium.WebDriver.Extensions.QuerySelector.Tests
{
    using System;
    using System.Collections;
    using NUnit.Framework;
    
    /// <summary>
    /// Query selector tests.
    /// </summary>
    [TestFixture]
    [Category("Unit Tests")]
#if !NET35
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public class QuerySelectorTests
    {
        /// <summary>
        /// Gets the selector test cases.
        /// </summary>
        private static IEnumerable SelectorTestCases
        {
            get
            {
                yield return new TestCaseData(By.QuerySelector("div"))
                    .Returns("document.querySelectorAll('div')").SetName("document.querySelectorAll('div')");
                yield return new TestCaseData(By.QuerySelector("input[type='text']"))
                    .Returns("document.querySelectorAll('input[type=\"text\"]')").SetName("escape single quotes");
                yield return new TestCaseData(By.QuerySelector("div", "document.body"))
                    .Returns("document.body.querySelectorAll('div')")
                    .SetName("document.body.querySelectorAll('div')");
                yield return new TestCaseData(By.QuerySelector("span", By.QuerySelector("div")))
                    .Returns("document.querySelectorAll('div').length === 0 ? [] : document.querySelectorAll('div')[0].querySelectorAll('span')")
                    .SetName("document.querySelectorAll('div')[0].querySelectorAll('span')");
            }
        }

        /// <summary>
        /// Gets the equality test cases.
        /// </summary>
        private static IEnumerable EqualityTestCases
        {
            get
            {
                yield return new TestCaseData(By.QuerySelector("div"), By.QuerySelector("div"), true)
                    .SetName("QS('div') == QS('div')");
                yield return new TestCaseData(By.QuerySelector("div"), By.QuerySelector("span"), false)
                    .SetName("QS('div') != QS('span')");
                yield return new TestCaseData(By.QuerySelector("div"), null, false)
                    .SetName("QS('div') != null");
                yield return new TestCaseData(null, By.QuerySelector("div"), false)
                    .SetName("null != QS('div')");
                yield return new TestCaseData(
                    By.QuerySelector("div", By.QuerySelector("body")),
                    By.QuerySelector("div"),
                    false)
                    .SetName("QS('div', QS('body')) != QS('div')");
                yield return new TestCaseData(
                    By.QuerySelector("div", By.QuerySelector("body")),
                    By.QuerySelector("div", By.QuerySelector("body")),
                    true)
                    .SetName("QS('div', QS('body')) == QS('div', QS('body'))");
                yield return new TestCaseData(
                    By.QuerySelector("div", "body"),
                    By.QuerySelector("div", By.QuerySelector("body")),
                    false)
                    .SetName("QS('div', null) != QS('div', QS('body'))");
                yield return new TestCaseData(
                    By.QuerySelector("div", By.QuerySelector("body")),
                    By.QuerySelector("div", "body"),
                    false)
                    .SetName("QS('div', QS('body')) != QS('div', null)");
                yield return new TestCaseData(
                    By.QuerySelector("div", By.QuerySelector("body")),
                    By.QuerySelector("div", By.QuerySelector("div")),
                    false)
                    .SetName("QS('div', QS('body')) != QS('div', QS('div'))");
            }
        }

        /// <summary>
        /// Tests if the proper selector is generated.
        /// </summary>
        /// <param name="selector">The query selector.</param>
        /// <returns>The generated query selector.</returns>
        [TestCaseSource("SelectorTestCases")]
        public string Selector(QuerySelector selector)
        {
            Assert.AreEqual(selector.Selector, selector.ToString());
            return selector.Selector;
        }

        /// <summary>
        /// Tests if the null selector is handled properly.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSelector()
        {
            By.QuerySelector(null);
        }

        /// <summary>
        /// Tests if the null selector is handled properly.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullBaseElementSelector()
        {
            By.QuerySelector("div", (string)null);
        }

        /// <summary>
        /// Tests if the null selector is handled properly.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSelectorWithBaseSelector()
        {
            By.QuerySelector(null, By.QuerySelector("div"));
        }

        /// <summary>
        /// Tests if the null selector is handled properly.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullBaseSelector()
        {
            By.QuerySelector("div", (QuerySelector)null);
        }

        /// <summary>
        /// Tests if the call format string is handled properly.
        /// </summary>
        [Test]
        public void CallFormatString()
        {
            var formatString = By.QuerySelector("div").CallFormatString;
            Assert.IsNotNull(formatString);
        }

        /// <summary>
        /// Tests the equality operators.
        /// </summary>
        /// <param name="selector1">First selector to compare.</param>
        /// <param name="selector2">Second selector to compare.</param>
        /// <param name="expectedResult">The expected result.</param>
        [TestCaseSource("EqualityTestCases")]
        public void EqualityOperator(QuerySelector selector1, QuerySelector selector2, bool expectedResult)
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
            var selector1 = By.QuerySelector("div");
            var selector2 = new object();

#pragma warning disable 252,253
            Assert.IsFalse(selector1 == selector2);
            Assert.IsTrue(selector1 != selector2);
#pragma warning restore 252,253
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNullElement()
        {
            var selector = new QuerySelector("div");
            selector.Create(null);
        }
    }
}
