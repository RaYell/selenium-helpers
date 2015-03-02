﻿namespace Selenium.WebDriver.Extensions.JQuery.Tests
{
    using System;
    using System.Collections;
    using NUnit.Framework;
    using By = Selenium.WebDriver.Extensions.JQuery.By;

    /// <summary>
    /// JQuery selector tests.
    /// </summary>
    [TestFixture]
    [Category("Unit Tests")]
#if !NET35
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
    public class JQuerySelectorTests
    {
        /// <summary>
        /// Gets the selector test cases.
        /// </summary>
        private static IEnumerable SelectorTestCases
        {
            get
            {
                // basic tests
                yield return new TestCaseData(By.JQuerySelector("div"))
                    .Returns("jQuery('div')").SetName("jQuery('div')");
                yield return new TestCaseData(By.JQuerySelector("div", jQueryVariable: "$"))
                    .Returns("$('div')").SetName("$('div')");
                yield return new TestCaseData(By.JQuerySelector("div", By.JQuerySelector("article")))
                    .Returns("jQuery('div', jQuery('article'))").SetName("jQuery('div', jQuery('article'))");

                // functions tests
                yield return new TestCaseData(By.JQuerySelector("div").Add("span"))
                    .Returns("jQuery('div').add('span')").SetName("jQuery('div').add('span')");
                yield return new TestCaseData(By.JQuerySelector("div").AddBack())
                    .Returns("jQuery('div').addBack()").SetName("jQuery('div').addBack()");
                yield return new TestCaseData(By.JQuerySelector("div").AddBack("span"))
                    .Returns("jQuery('div').addBack('span')").SetName("jQuery('div').addBack('span')");
                yield return new TestCaseData(By.JQuerySelector("div").AndSelf())
                    .Returns("jQuery('div').andSelf()").SetName("jQuery('div').andSelf()");
                yield return new TestCaseData(By.JQuerySelector("div").Children())
                    .Returns("jQuery('div').children()").SetName("jQuery('div').children()");
                yield return new TestCaseData(By.JQuerySelector("div").Children("span"))
                    .Returns("jQuery('div').children('span')").SetName("jQuery('div').children('span')");
                yield return new TestCaseData(By.JQuerySelector("div").Closest("span"))
                    .Returns("jQuery('div').closest('span')").SetName("jQuery('div').closest('span')");
                yield return new TestCaseData(By.JQuerySelector("div").Contents())
                    .Returns("jQuery('div').contents()").SetName("jQuery('div').contents()");
                yield return new TestCaseData(By.JQuerySelector("div").End())
                    .Returns("jQuery('div').end()").SetName("jQuery('div').end()");
                yield return new TestCaseData(By.JQuerySelector("div").Eq(0))
                    .Returns("jQuery('div').eq(0)").SetName("jQuery('div').eq(0)");
                yield return new TestCaseData(By.JQuerySelector("div").Eq(-2))
                    .Returns("jQuery('div').eq(-2)").SetName("jQuery('div').eq(-2)");
                yield return new TestCaseData(By.JQuerySelector("div").Filter(".empty"))
                    .Returns("jQuery('div').filter('.empty')").SetName("jQuery('div').filter('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").Find(".empty"))
                    .Returns("jQuery('div').find('.empty')").SetName("jQuery('div').find('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").First())
                    .Returns("jQuery('div').first()").SetName("jQuery('div').first()");
                yield return new TestCaseData(By.JQuerySelector("div").Has(".empty"))
                    .Returns("jQuery('div').has('.empty')").SetName("jQuery('div').has('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").Is(".empty"))
                    .Returns("jQuery('div').is('.empty')").SetName("jQuery('div').is('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").Last())
                    .Returns("jQuery('div').last()").SetName("jQuery('div').last()");
                yield return new TestCaseData(By.JQuerySelector("div").Next(".empty"))
                    .Returns("jQuery('div').next('.empty')").SetName("jQuery('div').next('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").NextAll(".empty"))
                    .Returns("jQuery('div').nextAll('.empty')").SetName("jQuery('div').nextAll('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").NextUntil(".empty"))
                    .Returns("jQuery('div').nextUntil('.empty')").SetName("jQuery('div').nextUntil('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").NextUntil(".empty", "span"))
                    .Returns("jQuery('div').nextUntil('.empty', 'span')")
                    .SetName("jQuery('div').nextUntil('.empty', 'span')");
                yield return new TestCaseData(By.JQuerySelector("div").Not(".empty"))
                    .Returns("jQuery('div').not('.empty')").SetName("jQuery('div').not('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").OffsetParent())
                    .Returns("jQuery('div').offsetParent()").SetName("jQuery('div').offsetParent()");
                yield return new TestCaseData(By.JQuerySelector("div").Parent(".parent"))
                    .Returns("jQuery('div').parent('.parent')").SetName("jQuery('div').parent('.parent')");
                yield return new TestCaseData(By.JQuerySelector("div").Parents(".parent"))
                    .Returns("jQuery('div').parents('.parent')").SetName("jQuery('div').parents('.parent')");
                yield return new TestCaseData(By.JQuerySelector("div").ParentsUntil(".parent"))
                    .Returns("jQuery('div').parentsUntil('.parent')")
                    .SetName("jQuery('div').parentsUntil('.parent')");
                yield return new TestCaseData(By.JQuerySelector("div").ParentsUntil(".parent", "body"))
                    .Returns("jQuery('div').parentsUntil('.parent', 'body')")
                    .SetName("jQuery('div').parentsUntil('.parent', 'body')");
                yield return new TestCaseData(By.JQuerySelector("div").Prev(".empty"))
                    .Returns("jQuery('div').prev('.empty')").SetName("jQuery('div').prev('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").PrevAll(".empty"))
                    .Returns("jQuery('div').prevAll('.empty')").SetName("jQuery('div').prevAll('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").PrevUntil(".empty"))
                    .Returns("jQuery('div').prevUntil('.empty')").SetName("jQuery('div').prevUntil('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").PrevUntil(".empty", "span"))
                    .Returns("jQuery('div').prevUntil('.empty', 'span')")
                    .SetName("jQuery('div').prevUntil('.empty', 'span')");
                yield return new TestCaseData(By.JQuerySelector("div").Siblings(".empty"))
                    .Returns("jQuery('div').siblings('.empty')").SetName("jQuery('div').siblings('.empty')");
                yield return new TestCaseData(By.JQuerySelector("div").Slice(1))
                    .Returns("jQuery('div').slice(1)").SetName("jQuery('div').slice(1)");
                yield return new TestCaseData(By.JQuerySelector("div").Slice(1, 3))
                    .Returns("jQuery('div').slice(1, 3)").SetName("jQuery('div').slice(1, 3)");
                yield return new TestCaseData(By.JQuerySelector("div").Slice(-3, -1))
                    .Returns("jQuery('div').slice(-3, -1)").SetName("jQuery('div').slice(-3, -1)");

                // additional tests
                yield return new TestCaseData(By.JQuerySelector("div").AddBack(string.Empty))
                    .Returns("jQuery('div').addBack()").SetName("empty selector");
                yield return new TestCaseData(By.JQuerySelector("div").AddBack(" span "))
                    .Returns("jQuery('div').addBack('span')").SetName("trim");
                yield return new TestCaseData(By.JQuerySelector("input[type='text']"))
                    .Returns("jQuery('input[type=\"text\"]')").SetName("escape single quotes");
            }
        }

        /// <summary>
        /// Gets the equality test cases.
        /// </summary>
        private static IEnumerable EqualityTestCases
        {
            get
            {
                yield return new TestCaseData(By.JQuerySelector("div"), By.JQuerySelector("div"), true)
                    .SetName("jQuery('div') == jQuery('div')");
                yield return new TestCaseData(
                    By.JQuerySelector("div"),
                    By.JQuerySelector("div", jQueryVariable: "$"),
                    false)
                    .SetName("jQuery('div') != $('div')");
                yield return new TestCaseData(By.JQuerySelector("div"), By.JQuerySelector("span"), false)
                    .SetName("jQuery('div') != jQuery('span')");
                yield return new TestCaseData(By.JQuerySelector("div"), null, false)
                    .SetName("jQuery('div') != null");
                yield return new TestCaseData(null, By.JQuerySelector("div"), false)
                    .SetName("null != jQuery('div')");
                yield return new TestCaseData(
                    By.JQuerySelector("div", By.JQuerySelector("body")), 
                    By.JQuerySelector("div"),
                    false)
                    .SetName("jQuery('div', jQuery('body')) != jQuery('div')");
                yield return new TestCaseData(
                    By.JQuerySelector("div", By.JQuerySelector("body")), 
                    By.JQuerySelector("div", By.JQuerySelector("body")), 
                    true)
                    .SetName("jQuery('div', jQuery('body')) == jQuery('div', jQuery('body'))");
            }
        }

        /// <summary>
        /// Tests if the proper selector is generated.
        /// </summary>
        /// <param name="selector">The jQuery selector.</param>
        /// <returns>The generated jQuery selector.</returns>
        [TestCaseSource("SelectorTestCases")]
        public string Selector(JQuerySelector selector)
        {
            return selector.Selector;
        }

        /// <summary>
        /// Tests if the context property is handled properly.
        /// </summary>
        [Test]
        public void Context()
        {
            var by = By.JQuerySelector("div", By.JQuerySelector("article"));

            Assert.AreEqual(by.Selector, "jQuery('div', jQuery('article'))");
            Assert.AreEqual(by.Context.Selector, "jQuery('article')");
        }

        /// <summary>
        /// Tests if the jQuery variable property is handled properly.
        /// </summary>
        [Test]
        public void JQueryVariable()
        {
            var by = By.JQuerySelector("div", jQueryVariable: "$");

            Assert.AreEqual(by.JQueryVariable, "$");
        }

        /// <summary>
        /// Tests if the null selector is handled properly.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSelector()
        {
            By.JQuerySelector(null);
        }

        /// <summary>
        /// Tests if the call format string is handled properly.
        /// </summary>
        [Test]
        public void CallFormatString()
        {
            var formatString = By.JQuerySelector("div").CallFormatString;
            Assert.IsNotNull(formatString);
        }

        /// <summary>
        /// Tests the equality operators.
        /// </summary>
        /// <param name="selector1">First selector to compare.</param>
        /// <param name="selector2">Second selector to compare.</param>
        /// <param name="expectedResult">The expected result.</param>
        [TestCaseSource("EqualityTestCases")]
        public void EqualityOperator(JQuerySelector selector1, JQuerySelector selector2, bool expectedResult)
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
            var selector1 = By.JQuerySelector("div");
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
        public void FindTextWithNullElement()
        {
            WebElementExtensions.FindText(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindHtmlWithNullElement()
        {
            WebElementExtensions.FindHtml(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindAttributeWithNullElement()
        {
            WebElementExtensions.FindAttribute(null, null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindPropertyWithNullElement()
        {
            WebElementExtensions.FindProperty(null, null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindStringPropertyWithNullElement()
        {
            WebElementExtensions.FindProperty<string>(null, null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindValueWithNullElement()
        {
            WebElementExtensions.FindValue(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindCssWithNullElement()
        {
            WebElementExtensions.FindCss(null, null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindWidthWithNullElement()
        {
            WebElementExtensions.FindWidth(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindHeightWithNullElement()
        {
            WebElementExtensions.FindHeight(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindInnerWidthWithNullElement()
        {
            WebElementExtensions.FindInnerWidth(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindInnerHeightWithNullElement()
        {
            WebElementExtensions.FindInnerHeight(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindOuterWidthWithNullElement()
        {
            WebElementExtensions.FindOuterWidth(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindOuterHeightWithNullElement()
        {
            WebElementExtensions.FindOuterHeight(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindPositionWithNullElement()
        {
            WebElementExtensions.FindPosition(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindOffsetWithNullElement()
        {
            WebElementExtensions.FindOffset(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindScrollLeftWithNullElement()
        {
            WebElementExtensions.FindScrollLeft(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindScrollTopWithNullElement()
        {
            WebElementExtensions.FindScrollTop(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindDataWithNullElement()
        {
            WebElementExtensions.FindData(null, null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindBoolDataWithNullElement()
        {
            WebElementExtensions.FindData<bool>(null, null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindCountWithNullElement()
        {
            WebElementExtensions.FindCount(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindSerializedWithNullElement()
        {
            WebElementExtensions.FindSerialized(null, null);
        }

        /// <summary>
        /// Tests invoking functions with null element.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindSerializedArrayWithNullElement()
        {
            WebElementExtensions.FindSerializedArray(null, null);
        }
    }
}
