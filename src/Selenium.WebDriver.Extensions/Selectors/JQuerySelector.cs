﻿namespace OpenQA.Selenium
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using OpenQA.Selenium.Contracts;
    using OpenQA.Selenium.Extensions;
    using PostSharp.Patterns.Contracts;
    using static JavaScriptSnippets;

    /// <summary>
    /// Searches the DOM elements using jQuery selector.
    /// </summary>
    public class JQuerySelector : SelectorBase<JQuerySelector>
    {
        private const string LibraryVariable = "window.jQuery";
        private readonly string _chain;

        /// <summary>
        /// Initializes a new instance of the <see cref="JQuerySelector"/> class.
        /// </summary>
        /// <param name="selector">A string containing a selector expression.</param>
        [SuppressMessage("ReSharper", "IntroduceOptionalParameters.Global")]
        public JQuerySelector(string selector)
            : this(selector, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JQuerySelector"/> class.
        /// </summary>
        /// <param name="selector">A string containing a selector expression.</param>
        /// <param name="context">A DOM Element, Document, or jQuery to use as context.</param>
        /// <param name="variable">A variable that has been assigned to jQuery.</param>
        /// <param name="chain">The jQuery method chain.</param>
        /// <exception cref="ArgumentNullException">
        /// Selector is null.
        /// -or- jQuery variable name is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Selector is empty.
        /// -or- jQuery variable name is empty.
        /// </exception>
        public JQuerySelector(
            [Required] string selector,
            JQuerySelector context,
            [Required] string variable = "jQuery",
            string chain = null)
            : base(selector, context)
        {
            _chain = chain;
            Variable = variable;
            Description = $"By.JQuerySelector: {RawSelector}";
        }

        /// <summary>
        /// Gets the empty selector.
        /// </summary>
        public static JQuerySelector Empty { get; } = new JQuerySelector("*");

        /// <inheritdoc/>
        public override Uri LibraryUri => new Uri("https://code.jquery.com/jquery-latest.min.js");

        /// <inheritdoc/>
        public override string CheckScript => CheckScriptCode(LibraryVariable);

        /// <summary>
        /// Gets the variable that has been assigned to jQuery.
        /// </summary>
        public virtual string Variable { get; }

        /// <inheritdoc/>
        public override string Selector => $"{Variable}('{RawSelector.Replace('\'', '"')}'"
            + (Context != null ? $", {Context.Selector}" : string.Empty) + ")"
            + (string.IsNullOrEmpty(_chain) ? string.Empty : _chain);

        /// <summary>
        /// Gets the result resolver string.
        /// </summary>
        protected override string ResultResolver => ".get()";

        /// <summary>
        /// Adds elements to the set of matched elements.
        /// </summary>
        /// <param name="selector">
        /// A string representing a selector expression to find additional elements to add to the set of matched
        /// elements.
        /// </param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentNullException">Selector is null.</exception>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Add([Required] string selector) => Chain("add", selector);

        /// <summary>
        /// Adds elements to the set of matched elements.
        /// </summary>
        /// <param name="selector">
        /// A string representing a selector expression to find additional elements to add to the set of matched
        /// elements.
        /// </param>
        /// <param name="context">The jQuery context selector.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentNullException">
        /// Selector is null.
        /// -or- Context is null.
        /// </exception>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Add([Required] string selector, [Required] JQuerySelector context) =>
            ChainWithContext("add", selector, context);

        /// <summary>
        /// Add the previous set of elements on the stack to the current set, optionally filtered by a selector.
        /// </summary>
        /// <param name="selector">
        /// A string containing a selector expression to match the current set of elements against.
        /// </param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector AddBack([NullOrNotEmpty] string selector = null) => Chain("addBack", selector);

        /// <summary>
        /// Add the previous set of elements on the stack to the current set.
        /// </summary>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <remarks>While this method is obsolete in jQuery 1.8+ we will support it.</remarks>
        public JQuerySelector AndSelf() => Chain("andSelf");

        /// <summary>
        /// Get the children of each element in the set of matched elements, optionally filtered by a selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Children([NullOrNotEmpty] string selector = null) => Chain("children", selector);

        /// <summary>
        /// For each element in the set, get the first element that matches the selector by testing the element itself
        /// and traversing up through its ancestors in the DOM tree.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentNullException">Selector is null.</exception>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Closest([Required] string selector) => Chain("closest", selector);

        /// <summary>
        /// For each element in the set, get the first element that matches the selector by testing the element itself
        /// and traversing up through its ancestors in the DOM tree.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <param name="context">The jQuery context selector.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentNullException">
        /// Selector is null.
        /// -or- Context is null.
        /// </exception>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Closest([Required] string selector, [Required] JQuerySelector context) =>
            ChainWithContext("closest", selector, context);

        /// <summary>
        /// Get the children of each element in the set of matched elements, including text and comment nodes.
        /// </summary>
        /// <returns>The Selenium jQuery selector.</returns>
        public JQuerySelector Contents() => Chain("contents");

        /// <summary>
        /// End the most recent filtering operation in the current chain and return the set of matched elements to its
        /// previous state.
        /// </summary>
        /// <returns>The Selenium jQuery selector.</returns>
        public JQuerySelector End() => Chain("end");

        /// <summary>
        /// Reduce the set of matched elements to the one at the specified index.
        /// </summary>
        /// <param name="index">An integer indicating the 0-based position of the element.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        public JQuerySelector Eq(int index) => Chain("eq", index.ToString(CultureInfo.InvariantCulture), true);

        /// <summary>
        /// Reduce the set of matched elements to those that match the selector or pass the function's test.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentNullException">Selector is null.</exception>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Filter([Required] string selector) => Chain("filter", selector);

        /// <summary>
        /// Get the descendants of each element in the current set of matched elements, filtered by a selector, jQuery
        /// object, or element.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentNullException">Selector is null.</exception>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Find([Required] string selector) => Chain("find", selector);

        /// <summary>
        /// Reduce the set of matched elements to the first in the set.
        /// </summary>
        /// <returns>The Selenium jQuery selector.</returns>
        public JQuerySelector First() => Chain("first");

        /// <summary>
        /// Reduce the set of matched elements to those that have a descendant that matches the selector or DOM
        /// element.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentNullException">Selector is null.</exception>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Has([Required] string selector) => Chain("has", selector);

        /// <summary>
        /// Check the current matched set of elements against a selector, element, or jQuery object and return true if
        /// at least one of these elements matches the given arguments.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentNullException">Selector is null.</exception>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Is([Required] string selector) => Chain("is", selector);

        /// <summary>
        /// Reduce the set of matched elements to the final one in the set.
        /// </summary>
        /// <returns>The Selenium jQuery selector.</returns>
        public JQuerySelector Last() => Chain("last");

        /// <summary>
        /// Get the immediately following sibling of each element in the set of matched elements. If a selector is
        /// provided, it retrieves the next sibling only if it matches that selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Next([NullOrNotEmpty] string selector = null) => Chain("next", selector);

        /// <summary>
        /// Get all following siblings of each element in the set of matched elements, optionally filtered by a
        /// selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector NextAll([NullOrNotEmpty] string selector = null) => Chain("nextAll", selector);

        /// <summary>
        /// Get all following siblings of each element up to but not including the element matched by the selector,
        /// DOM node, or jQuery object passed.
        /// </summary>
        /// <param name="selector">
        /// A string containing a selector expression to indicate where to stop matching following sibling elements.
        /// </param>
        /// <param name="filter">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">
        /// Selector is empty.
        /// -or- Filter is empty.
        /// </exception>
        public JQuerySelector NextUntil(
            [NullOrNotEmpty] string selector = null, [NullOrNotEmpty] string filter = null) =>
            Chain(
                "nextUntil",
                HandleSelectorWithFilter(selector == null && filter != null ? string.Empty : selector, filter),
                true);

        /// <summary>
        /// Remove elements from the set of matched elements.
        /// </summary>
        /// <param name="selector">
        /// A string containing a selector expression, a DOM element, or an array of elements to match against the
        /// set.
        /// </param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentNullException">Selector is null.</exception>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Not([Required] string selector) => Chain("not", selector);

        /// <summary>
        /// Get the closest ancestor element that is positioned.
        /// </summary>
        /// <returns>The Selenium jQuery selector.</returns>
        public JQuerySelector OffsetParent() => Chain("offsetParent");

        /// <summary>
        /// Get the parent of each element in the current set of matched elements, optionally filtered by a selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Parent([NullOrNotEmpty] string selector = null)
        {
            return Chain("parent", selector);
        }

        /// <summary>
        /// Get the ancestors of each element in the current set of matched elements, optionally filtered by a
        /// selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Parents([NullOrNotEmpty] string selector = null) => Chain("parents", selector);

        /// <summary>
        /// Get the ancestors of each element in the current set of matched elements, up to but not including the
        /// element matched by the selector, DOM node, or jQuery object.
        /// </summary>
        /// <param name="selector">
        /// A string containing a selector expression to indicate where to stop matching ancestor elements.
        /// </param>
        /// <param name="filter">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">
        /// Selector is empty.
        /// -or- Filter is empty.
        /// </exception>
        public JQuerySelector ParentsUntil(
            [NullOrNotEmpty] string selector = null, [NullOrNotEmpty] string filter = null) =>
            Chain(
                "parentsUntil",
                HandleSelectorWithFilter(selector == null && filter != null ? string.Empty : selector, filter),
                true);

        /// <summary>
        /// Get the immediately preceding sibling of each element in the set of matched elements, optionally filtered
        /// by a selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Prev([NullOrNotEmpty] string selector = null) => Chain("prev", selector);

        /// <summary>
        /// Get all preceding siblings of each element in the set of matched elements, optionally filtered by a
        /// selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector PrevAll([NullOrNotEmpty] string selector = null) => Chain("prevAll", selector);

        /// <summary>
        /// Get all preceding siblings of each element up to but not including the element matched by the selector,
        /// DOM node, or jQuery object.
        /// </summary>
        /// <param name="selector">
        /// A string containing a selector expression to indicate where to stop matching preceding sibling elements.
        /// </param>
        /// <param name="filter">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">
        /// Selector is empty.
        /// -or- Filter is empty.
        /// </exception>
        public JQuerySelector PrevUntil(
            [NullOrNotEmpty] string selector = null, [NullOrNotEmpty] string filter = null) =>
            Chain(
                "prevUntil",
                HandleSelectorWithFilter(selector == null && filter != null ? string.Empty : selector, filter),
                true);

        /// <summary>
        /// Get the siblings of each element in the set of matched elements, optionally filtered by a selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        /// <exception cref="ArgumentException">Selector is empty.</exception>
        public JQuerySelector Siblings([NullOrNotEmpty] string selector = null) => Chain("siblings", selector);

        /// <summary>
        /// Reduce the set of matched elements to a subset specified by a range of indexes.
        /// </summary>
        /// <param name="start">
        /// An integer indicating the 0-based position at which the elements begin to be selected. If negative, it
        /// indicates an offset from the end of the set.
        /// </param>
        /// <param name="end">
        /// An integer indicating the 0-based position at which the elements stop being selected. If negative, it
        /// indicates an offset from the end of the set. If omitted, the range continues until the end of the set.
        /// </param>
        /// <returns>The Selenium jQuery selector.</returns>
        public JQuerySelector Slice(int start, int? end = null) =>
            Chain("slice", end.HasValue ? $"{start}, {end}" : start.ToString(CultureInfo.InvariantCulture), true);

        /// <inheritdoc/>
        protected override void LoadExternalLibrary(IWebDriver driver) => driver.LoadJQuery();

        /// <inheritdoc/>
        protected override JQuerySelector CreateContext(string contextSelector) =>
            new JQuerySelector(contextSelector, null, Variable);

        /// <summary>
        /// Handles the selector with filter scenario by generating the proper chained function arguments.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>Chained function arguments string generated based on given selector and filter.</returns>
        private static string HandleSelectorWithFilter(string selector = null, string filter = null)
        {
            var data = string.Empty;
            if (selector != null)
            {
                data = string.IsNullOrEmpty(filter)
                    ? $"'{selector.Replace('\'', '"')}'"
                    : $"'{selector.Replace('\'', '"')}', '{filter.Replace('\'', '"')}'";
            }

            return data;
        }

        /// <summary>
        /// Chain a jQuery method to a selector.
        /// </summary>
        /// <param name="name">The jQuery method name.</param>
        /// <param name="selector">The jQuery method selector.</param>
        /// <param name="noWrap">
        /// <see langword="true"/> to not to wrap the selector into quotes; otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>The Selenium jQuery selector.</returns>
        private JQuerySelector Chain(string name, string selector = null, bool noWrap = false)
        {
            selector = selector == null
                ? string.Empty
                : (noWrap ? selector.Trim() : $"'{selector.Trim().Replace('\'', '"')}'");
            var chain = string.IsNullOrEmpty(_chain) ? string.Empty : _chain;

            return new JQuerySelector(
                RawSelector,
                Context,
                Variable,
                $"{chain}.{name}({selector})");
        }

        /// <summary>
        /// Chain a jQuery method to a selector.
        /// </summary>
        /// <param name="name">The jQuery method name.</param>
        /// <param name="selector">The jQuery method selector.</param>
        /// <param name="context">The jQuery context selector.</param>
        /// <returns>The Selenium jQuery selector.</returns>
        private JQuerySelector ChainWithContext(string name, string selector, JQuerySelector context) =>
            new JQuerySelector(
                RawSelector,
                Context,
                Variable,
                $".{name}('{selector.Replace('\'', '"')}', {context.Selector})");
    }
}
