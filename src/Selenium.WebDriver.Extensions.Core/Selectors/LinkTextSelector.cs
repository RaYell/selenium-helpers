﻿namespace Selenium.WebDriver.Extensions.Core
{
    using System;
    using Selenium.WebDriver.Extensions.QuerySelector;
    using Selenium.WebDriver.Extensions.Shared;

    /// <summary>
    /// The link text selector.
    /// </summary>
    public class LinkTextSelector : LinkTextSelectorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkTextSelector"/> class.
        /// </summary>
        /// <param name="text">The link text.</param>
        /// <param name="baseElement">
        /// A string defining the base element on which base element the selector should be invoked.
        /// </param>
        public LinkTextSelector(string text, string baseElement = "document")
            : base(text, baseElement)
        {
            this.Selector = @"(function(text, baseElem) {
                var l = baseElem.querySelectorAll(':link');
                for (var i = 0; i < l.length; i++) { 
                    if (l[i].innerText === text) { 
                        return l[i]; 
                    } 
                }
                return null;
            })('" + text + "', " + this.BaseElement + ");";
        }

        /// <summary>
        /// Compares two selectors and returns <c>true</c> if they are equal.
        /// </summary>
        /// <param name="selector1">The first selector to compare.</param>
        /// <param name="selector2">The second selector to compare.</param>
        /// <returns><c>true</c> if the selectors are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(LinkTextSelector selector1, LinkTextSelector selector2)
        {
            if (ReferenceEquals(selector1, selector2))
            {
                return true;
            }

            if (((object)selector1 == null) || ((object)selector2 == null))
            {
                return false;
            }

            return selector1.Equals(selector2);
        }

        /// <summary>
        /// Compares two selectors and returns <c>true</c> if they are not equal.
        /// </summary>
        /// <param name="selector1">The first selector to compare.</param>
        /// <param name="selector2">The second selector to compare.</param>
        /// <returns><c>true</c> if the selectors are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(LinkTextSelector selector1, LinkTextSelector selector2)
        {
            return !(selector1 == selector2);
        }

        /// <summary>
        /// Creates a new selector using given selector as a root.
        /// </summary>
        /// <param name="root">A web element to be used as a root.</param>
        /// <returns>A new selector.</returns>
        public override ISelector Create(WebElement root)
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }

            var rootSelector = new QuerySelector(root.Path);
            return new LinkTextSelector(this.RawSelector, rootSelector.Selector);
        }

        /// <summary>
        /// Determines whether two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var selector = (LinkTextSelector)obj;
            return this.RawSelector == selector.RawSelector && this.BaseElement == selector.BaseElement;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return this.RawSelector.GetHashCode() ^ this.BaseElement.GetHashCode();
        }
    }
}