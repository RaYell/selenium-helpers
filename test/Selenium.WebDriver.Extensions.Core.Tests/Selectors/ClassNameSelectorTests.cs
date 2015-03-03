﻿namespace Selenium.WebDriver.Extensions.Core.Tests
{
    using System.Collections;
    using NUnit.Framework;
    using QS = Selenium.WebDriver.Extensions.QuerySelector.QuerySelector;

    /// <summary>
    /// Link text selector tests.
    /// </summary>
    [TestFixture]
    [Category("Unit Tests")]
#if !NET35
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public class ClassNameSelectorTests
    {
        /// <summary>
        /// Gets the equality test cases.
        /// </summary>
        private static IEnumerable QuerySelectorEqualityTestCases
        {
            get
            {
                yield return new TestCaseData(
                    By.QuerySelector("div", By.ClassName("div")),
                    By.QuerySelector("div", By.QuerySelector("div")),
                    false)
                    .SetName("QS('div', CN('div')) != QS('div', QS('div'))");
            }
        }

        /// <summary>
        /// Tests the equality operators.
        /// </summary>
        /// <param name="selector1">First selector to compare.</param>
        /// <param name="selector2">Second selector to compare.</param>
        /// <param name="expectedResult">The expected result.</param>
        [TestCaseSource("QuerySelectorEqualityTestCases")]
        public void QuerySelectorEqualityOperator(QS selector1, QS selector2, bool expectedResult)
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
    }
}
