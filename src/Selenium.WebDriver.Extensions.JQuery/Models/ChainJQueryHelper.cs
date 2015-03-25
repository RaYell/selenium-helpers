﻿namespace Selenium.WebDriver.Extensions.JQuery
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using OpenQA.Selenium;
    using Selenium.WebDriver.Extensions.Core;

    /// <summary>
    /// Additional methods for <see cref="JQuerySelector"/>.
    /// </summary>
    public class ChainJQueryHelper : JQueryHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChainJQueryHelper"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="webElement">The web element.</param>
        /// <exception cref="ArgumentNullException">Selector is null.</exception>
        public ChainJQueryHelper(IWebDriver driver, JQuerySelector selector, WebElement webElement = null)
            : base(driver, webElement)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            this.Selector = selector;
        }

        /// <summary>
        /// Gets or sets the driver.
        /// </summary>
        private JQuerySelector Selector { get; set; }

        /// <summary>
        /// Searches for DOM element using jQuery selector and gets the combined text contents of each element in the 
        /// set of matched elements, including their descendants, or set the text contents of the matched elements.
        /// </summary>
        /// <returns>
        /// The combined text contents of each element in the set of matched elements, including their descendants, or 
        /// set the text contents of the matched elements.
        /// </returns>
        public string Text()
        {
            return this.Find<string>("text()");
        }

        /// <summary>
        /// Sets the inner text for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="text">The text to be set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Text(string text)
        {
            this.Run("text('" + text + "')");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the HTML contents of the first element in the set 
        /// of matched elements or set the HTML contents of every matched element.
        /// </summary>
        /// <returns>
        /// The HTML contents of the first element in the set of matched elements or set the HTML contents of every 
        /// matched element.
        /// </returns>
        public string Html()
        {
            return this.Find<string>("html()");
        }

        /// <summary>
        /// Sets the inner HTML for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="html">The HTML string to be set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Html(string html)
        {
            this.Run("html('" + html + "')");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the value of an attribute for the first element 
        /// in the set of matched elements.
        /// </summary>
        /// <param name="attributeName">The name of the attribute to get.</param>
        /// <returns>The value of an attribute for the first element in the set of matched elements.</returns>
        public string Attribute(string attributeName)
        {
            return this.Find<string>("attr('" + attributeName + "')");
        }

        /// <summary>
        /// Sets the attribute value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="attributeName">The name of the attribute to set.</param>
        /// <param name="attributeValue">The value of the attribute to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Attribute(string attributeName, string attributeValue)
        {
            this.Run("attr('" + attributeName + "', '" + attributeValue + "')");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the value of a property for the first element in 
        /// the set of matched elements.
        /// </summary>
        /// <param name="propertyName">The name of the property to get.</param>
        /// <returns>The value of a property for the first element in the set of matched elements.</returns>
        /// <exception cref="TypeArgumentException">Invoked generic method with unsupported type.</exception>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public bool? Property(string propertyName)
        {
            return this.Property<bool?>(propertyName);
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the value of a property for the first element in 
        /// the set of matched elements.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        /// <param name="propertyName">The name of the property to get.</param>
        /// <returns>The value of a property for the first element in the set of matched elements.</returns>
        /// <exception cref="TypeArgumentException">Invoked generic method with unsupported type.</exception>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public T Property<T>(string propertyName)
        {
            if (!new[] { typeof(bool?), typeof(string) }.Contains(typeof(T)))
            {
                throw new TypeArgumentException("Only null-able boolean and string types are supported", "T");
            }

            return this.Find<T>("prop('" + propertyName + "')");
        }

        /// <summary>
        /// Sets the property value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="propertyValue">The value of the property to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Property(string propertyName, string propertyValue)
        {
            this.Run("prop('" + propertyName + "', '" + propertyValue + "')");
            return this;
        }

        /// <summary>
        /// Sets the property value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="propertyValue">The value of the property to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Property(string propertyName, bool propertyValue)
        {
            this.Run("prop('" + propertyName + "', " + (propertyValue ? "true" : "false") + ")");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current value of the first element in the set 
        /// of matched elements.
        /// </summary>
        /// <returns>The current value of the first element in the set of matched elements.</returns>
        public string Value()
        {
            return this.Find<string>("val()");
        }

        /// <summary>
        /// Sets the value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Value(string value)
        {
            this.Run("val('" + value + "')");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and get the value of a style property for the first 
        /// element in the set of matched elements or set one or more CSS properties for every matched element.
        /// </summary>
        /// <param name="propertyName">The CSS property name.</param>
        /// <returns>
        /// The value of a style property for the first element in the set of matched elements or set one or more CSS 
        /// properties for every matched element.
        /// </returns>
        public string Css(string propertyName)
        {
            return this.Find<string>("css('" + propertyName + "')");
        }

        /// <summary>
        /// Sets the CSS property value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="propertyName">The name of the CSS property to set.</param>
        /// <param name="propertyValue">The value of the CSS property to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Css(string propertyName, string propertyValue)
        {
            this.Run("css('" + propertyName + "', '" + propertyValue + "')");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed width for the first element 
        /// in the set of matched elements or set the width of every matched element.
        /// </summary>
        /// <returns>
        /// The current computed width for the first element in the set of matched elements or set the width of every 
        /// matched element.
        /// </returns>
        public long? Width()
        {
            return this.Find<long?>("width()");
        }

        /// <summary>
        /// Sets the width value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="value">The value of the width to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Width(decimal value)
        {
            this.Run("width(" + value + ")");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed height for the first element 
        /// in the set of matched elements or set the width of every matched element.
        /// </summary>
        /// <returns>
        /// The current computed height for the first element in the set of matched elements or set the height of 
        /// every matched element.
        /// </returns>
        public long? Height()
        {
            return this.Find<long?>("height()");
        }

        /// <summary>
        /// Sets the height value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="value">The value of the height to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Height(decimal value)
        {
            this.Run("height(" + value + ")");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed inner width (including 
        /// padding but not border) for the first element in the set of matched elements or set the inner width of 
        /// every matched element.
        /// </summary>
        /// <returns>
        /// The current computed inner width (including padding but not border) for the first element in the set of 
        /// matched elements or set the inner width of every matched element.
        /// </returns>
        public long? InnerWidth()
        {
            return this.Find<long?>("innerWidth()");
        }

        /// <summary>
        /// Sets the inner width value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="value">The value of the inner width to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper InnerWidth(decimal value)
        {
            this.Run("innerWidth(" + value + ")");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed inner height (including 
        /// padding but not border) for the first element in the set of matched elements or set the inner width of 
        /// every matched element.
        /// </summary>
        /// <returns>
        /// The current computed inner height (including padding but not border) for the first element in the set of 
        /// matched elements or set the inner width of every matched element.
        /// </returns>
        public long? InnerHeight()
        {
            return this.Find<long?>("innerHeight()");
        }

        /// <summary>
        /// Sets the inner height value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="value">The value of the inner height to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper InnerHeight(decimal value)
        {
            this.Run("innerHeight(" + value + ")");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed width for the first element 
        /// in the set of matched elements, including padding and border.
        /// </summary>
        /// <param name="includeMargin">
        /// A flag indicating whether to include the element's margin in the calculation.
        /// </param>
        /// <returns>
        /// The current computed width for the first element in the set of matched elements, including padding and 
        /// border.
        /// </returns>
        public long? OuterWidth(bool includeMargin = false)
        {
            return this.Find<long?>("outerWidth(" + (includeMargin ? "true" : string.Empty) + ")");
        }

        /// <summary>
        /// Sets the outer width value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="value">The value of the outer width to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper OuterWidth(decimal value)
        {
            this.Run("outerWidth(" + value + ")");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current computed height for the first element 
        /// in the set of matched elements, including padding and border.
        /// </summary>
        /// <param name="includeMargin">
        /// A flag indicating whether to include the element's margin in the calculation.
        /// </param>
        /// <returns>
        /// The current computed height for the first element in the set of matched elements, including padding and 
        /// border.
        /// </returns>
        public long? OuterHeight(bool includeMargin = false)
        {
            return this.Find<long?>("outerHeight(" + (includeMargin ? "true" : string.Empty) + ")");
        }

        /// <summary>
        /// Sets the outer height value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="value">The value of the outer height to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper OuterHeight(decimal value)
        {
            this.Run("outerHeight(" + value + ")");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current coordinates of the first element in 
        /// the set of matched elements, relative to the offset parent.
        /// </summary>
        /// <returns>
        /// The current coordinates of the first element in the set of matched elements, relative to the offset 
        /// parent.
        /// </returns>
        /// <exception cref="ArgumentNullException">Key is null.</exception>
        /// <exception cref="OverflowException">
        /// Position coordinate represents a number less than <see cref="Int32.MinValue" /> or greater than 
        /// <see cref="Int32.MaxValue" />.
        /// </exception>
        /// <exception cref="FormatException">Position coordinate is not of the correct format.</exception>
        /// <exception cref="KeyNotFoundException">
        /// The property is retrieved and key is not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// The property is set and the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.
        /// </exception>
        public Position? Position()
        {
            var positionDict = this.Find<IDictionary<string, object>>("position()");
            if (positionDict == null || !positionDict.ContainsKey("top") || !positionDict.ContainsKey("left"))
            {
                return null;
            }

            var top = int.Parse(positionDict["top"].ToString(), CultureInfo.InvariantCulture);
            var left = int.Parse(positionDict["left"].ToString(), CultureInfo.InvariantCulture);
            return new Position(top, left);
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current coordinates of the first element in 
        /// the set of matched elements, relative to the document.
        /// </summary>
        /// <returns>
        /// The current coordinates of the first element in the set of matched elements, relative to the document.
        /// </returns>
        /// <exception cref="ArgumentNullException">Key is null.</exception>
        /// <exception cref="OverflowException">
        /// Position coordinate represents a number less than <see cref="Int32.MinValue" /> or greater than 
        /// <see cref="Int32.MaxValue" />.
        /// </exception>
        /// <exception cref="FormatException">Position coordinate is not of the correct format.</exception>
        /// <exception cref="KeyNotFoundException">
        /// The property is retrieved and key is not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// The property is set and the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.
        /// </exception>
        public Position? Offset()
        {
            var offsetDict = this.Find<IDictionary<string, object>>("offset()");
            if (offsetDict == null || !offsetDict.ContainsKey("top") || !offsetDict.ContainsKey("left"))
            {
                return null;
            }

            var top = int.Parse(offsetDict["top"].ToString(), CultureInfo.InvariantCulture);
            var left = int.Parse(offsetDict["left"].ToString(), CultureInfo.InvariantCulture);
            return new Position(top, left);
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current horizontal position of the scroll bar
        /// for the first element in the set of matched elements or set the horizontal position of the scroll bar for 
        /// every matched element.
        /// </summary>
        /// <returns>
        /// The current horizontal position of the scroll bar for the first element in the set of matched elements or 
        /// set the horizontal position of the scroll bar for every matched element.
        /// </returns>
        public long? ScrollLeft()
        {
            return this.Find<long?>("scrollLeft()");
        }

        /// <summary>
        /// Sets the scroll left value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="value">The value of the scroll left to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper ScrollLeft(decimal value)
        {
            this.Run("scrollLeft(" + value + ")");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the current vertical position of the scroll bar 
        /// for the first element in the set of matched elements or set the vertical position of the scroll bar for 
        /// every matched element.
        /// </summary>
        /// <returns>
        /// The current vertical position of the scroll bar for the first element in the set of matched elements or 
        /// set the vertical position of the scroll bar for every matched element.
        /// </returns>
        public long? ScrollTop()
        {
            return this.Find<long?>("scrollTop()");
        }

        /// <summary>
        /// Sets the scroll top value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="value">The value of the scroll top to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper ScrollTop(decimal value)
        {
            this.Run("scrollTop(" + value + ")");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and returns the value at the named data store for the 
        /// first element in the jQuery collection, as set by <c>data(name, value)</c> or by an HTML5 data-* 
        /// attribute.
        /// </summary>
        /// <param name="key">The name of the data stored.</param>
        /// <returns>
        /// The value at the named data store for the first element in the jQuery collection, as set by 
        /// <c>data(name, value)</c> or by an HTML5 data-* attribute.
        /// </returns>
        /// <exception cref="TypeArgumentException">Invoked generic method with unsupported type.</exception>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public string Data(string key)
        {
            return this.Data<string>(key);
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and returns the value at the named data store for the 
        /// first element in the jQuery collection, as set by <c>data(name, value)</c> or by an HTML5 data-* 
        /// attribute.
        /// </summary>
        /// <typeparam name="T">The type of the result to be returned.</typeparam>
        /// <param name="key">The name of the data stored.</param>
        /// <returns>
        /// The value at the named data store for the first element in the jQuery collection, as set by 
        /// <c>data(name, value)</c> or by an HTML5 data-* attribute.
        /// </returns>
        /// <exception cref="TypeArgumentException">Invoked generic method with unsupported type.</exception>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public T Data<T>(string key)
        {
            if (!new[] { typeof(bool?), typeof(long?), typeof(string) }.Contains(typeof(T)))
            {
                throw new TypeArgumentException(
                    "Only null-able boolean, null-able long and string types are supported", 
                    "T");
            }

            return this.Find<T>("data('" + key + "')");
        }

        /// <summary>
        /// Sets the data value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="key">The name of the data stored.</param>
        /// <param name="value">The value of the data to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Data(string key, string value)
        {
            this.Run("data('" + key + "', '" + value + "')");
            return this;
        }

        /// <summary>
        /// Sets the data value for the given key for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="key">The name of the data stored.</param>
        /// <param name="value">The value of the data to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Data(string key, decimal value)
        {
            this.Run("data('" + key + "', " + value + ")");
            return this;
        }

        /// <summary>
        /// Removes the data for the given key for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="key">The name of the data stored.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper RemoveData(string key)
        {
            this.Run("removeData('" + key + "')");
            return this;
        }

        /// <summary>
        /// Sets the data value for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="key">The name of the data stored.</param>
        /// <param name="value">The value of the data to set.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Data(string key, bool value)
        {
            this.Run("data('" + key + "', " + (value ? "true" : "false") + ")");
            return this;
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the number of elements in the jQuery object.
        /// </summary>
        /// <returns>The number of elements in the jQuery object.</returns>
        public long Count()
        {
            return this.Find<long>("length");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the encoded set of form elements as a string 
        /// for submission.
        /// </summary>
        /// <returns>The encoded set of form elements as a string for submission.</returns>
        public string Serialized()
        {
            return this.Find<string>("serialize()");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and gets the set of form elements as a string representing
        /// encoded array of names and values.
        /// </summary>
        /// <returns>The encoded set of form elements as a string representing array of names and values.</returns>
        public string SerializedArray()
        {
            return this.Find<string>("serializeArray()", "JSON.stringify({0})");
        }

        /// <summary>
        /// Searches for DOM elements using jQuery selector and checks if any element of the result set has given class
        /// set.
        /// </summary>
        /// <param name="className">The class name.</param>
        /// <returns>The encoded set of form elements as a string representing array of names and values.</returns>
        public bool? HasClass(string className)
        {
            return this.Find<bool?>("hasClass('" + className + "')");
        }

        /// <summary>
        /// Adds the given class to all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="className">The name of the class to add.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper AddClass(string className)
        {
            this.Run("addClass('" + className + "')");
            return this;
        }

        /// <summary>
        /// Removes the given class to all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="className">The name of the class to remove.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper RemoveClass(string className)
        {
            this.Run("removeClass('" + className + "')");
            return this;
        }

        /// <summary>
        /// Toggles the given class to all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="className">The name of the class to toggle.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper ToggleClass(string className)
        {
            this.Run("toggleClass('" + className + "')");
            return this;
        }

        /// <summary>
        /// Toggles the given class to all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="className">The name of the class to toggle.</param>
        /// <param name="state">The value to determine whether the class should be added or removed.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper ToggleClass(string className, bool state)
        {
            this.Run("toggleClass('" + className + "', " + (state ? "true" : "false") + ")");
            return this;
        }

        /// <summary>
        /// Removes the elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Remove()
        {
            this.Run("remove()");
            return this;
        }

        /// <summary>
        /// Removes the elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="selector">
        /// The selector expression that filters the set of matched elements to be removed.
        /// </param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Remove(string selector)
        {
            this.Run("remove('" + selector + "')");
            return this;
        }

        /// <summary>
        ///  Remove all child nodes for all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Empty()
        {
            this.Run("empty()");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Show()
        {
            this.Run("show()");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <exception cref="FormatException">Format contains an invalid specification.</exception>
        /// <exception cref="InvalidOperationException">
        /// Format equals "X", but the enumeration type is unknown.
        /// </exception>
        public ChainJQueryHelper Show(Duration duration)
        {
            this.Run("show('" + duration.ToString("G").ToLowerInvariant() + "')");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Show(decimal duration)
        {
            this.Run("show(" + duration + ")");
            return this;
        }

        /// <summary>
        /// Hides all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Hide()
        {
            this.Run("hide()");
            return this;
        }

        /// <summary>
        /// Hides all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <exception cref="FormatException">Format contains an invalid specification. </exception>
        /// <exception cref="InvalidOperationException">
        /// Format equals "X", but the enumeration type is unknown.
        /// </exception>
        public ChainJQueryHelper Hide(Duration duration)
        {
            this.Run("hide('" + duration.ToString("G").ToLowerInvariant() + "')");
            return this;
        }

        /// <summary>
        /// Hides all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Hide(decimal duration)
        {
            this.Run("hide(" + duration + ")");
            return this;
        }

        /// <summary>
        /// Toggles all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Toggle()
        {
            this.Run("toggle()");
            return this;
        }

        /// <summary>
        /// Toggles all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <exception cref="FormatException">Format contains an invalid specification. </exception>
        /// <exception cref="InvalidOperationException">
        /// Format equals "X", but the enumeration type is unknown.
        /// </exception>
        public ChainJQueryHelper Toggle(Duration duration)
        {
            this.Run("toggle('" + duration.ToString("G").ToLowerInvariant() + "')");
            return this;
        }

        /// <summary>
        /// Toggles all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Toggle(decimal duration)
        {
            this.Run("toggle(" + duration + ")");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/> with sliding motion.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper SlideDown()
        {
            this.Run("slideDown()");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/> with sliding motion.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <exception cref="FormatException">Format contains an invalid specification. </exception>
        /// <exception cref="InvalidOperationException">
        /// Format equals "X", but the enumeration type is unknown.
        /// </exception>
        public ChainJQueryHelper SlideDown(Duration duration)
        {
            this.Run("slideDown('" + duration.ToString("G").ToLowerInvariant() + "')");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/> with sliding motion.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper SlideDown(decimal duration)
        {
            this.Run("slideDown(" + duration + ")");
            return this;
        }

        /// <summary>
        /// Hides all elements matching current <see cref="JQuerySelector"/> with sliding motion.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper SlideUp()
        {
            this.Run("slideUp()");
            return this;
        }

        /// <summary>
        /// Hides all elements matching current <see cref="JQuerySelector"/> with sliding motion.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <exception cref="FormatException">Format contains an invalid specification. </exception>
        /// <exception cref="InvalidOperationException">
        /// Format equals "X", but the enumeration type is unknown.
        /// </exception>
        public ChainJQueryHelper SlideUp(Duration duration)
        {
            this.Run("slideUp('" + duration.ToString("G").ToLowerInvariant() + "')");
            return this;
        }

        /// <summary>
        /// Hides all elements matching current <see cref="JQuerySelector"/> with sliding motion.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper SlideUp(decimal duration)
        {
            this.Run("slideUp(" + duration + ")");
            return this;
        }

        /// <summary>
        /// Toggles all elements matching current <see cref="JQuerySelector"/> with sliding motion.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper SlideToggle()
        {
            this.Run("slideToggle()");
            return this;
        }

        /// <summary>
        /// Toggles all elements matching current <see cref="JQuerySelector"/> with sliding motion.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <exception cref="FormatException">Format contains an invalid specification. </exception>
        /// <exception cref="InvalidOperationException">
        /// Format equals "X", but the enumeration type is unknown.
        /// </exception>
        public ChainJQueryHelper SlideToggle(Duration duration)
        {
            this.Run("slideToggle('" + duration.ToString("G").ToLowerInvariant() + "')");
            return this;
        }

        /// <summary>
        /// Toggles all elements matching current <see cref="JQuerySelector"/> with sliding motion.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper SlideToggle(decimal duration)
        {
            this.Run("slideToggle(" + duration + ")");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/> by fading them to opaque.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper FadeIn()
        {
            this.Run("fadeIn()");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/> by fading them to opaque.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <exception cref="FormatException">Format contains an invalid specification. </exception>
        /// <exception cref="InvalidOperationException">
        /// Format equals "X", but the enumeration type is unknown.
        /// </exception>
        public ChainJQueryHelper FadeIn(Duration duration)
        {
            this.Run("fadeIn('" + duration.ToString("G").ToLowerInvariant() + "')");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/> by fading them to opaque.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper FadeIn(decimal duration)
        {
            this.Run("fadeIn(" + duration + ")");
            return this;
        }

        /// <summary>
        /// Hides all elements matching current <see cref="JQuerySelector"/> by fading them to transparent.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper FadeOut()
        {
            this.Run("fadeOut()");
            return this;
        }

        /// <summary>
        /// Hides all elements matching current <see cref="JQuerySelector"/> by fading them to transparent.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <exception cref="FormatException">Format contains an invalid specification. </exception>
        /// <exception cref="InvalidOperationException">
        /// Format equals "X", but the enumeration type is unknown.
        /// </exception>
        public ChainJQueryHelper FadeOut(Duration duration)
        {
            this.Run("fadeOut('" + duration.ToString("G").ToLowerInvariant() + "')");
            return this;
        }

        /// <summary>
        /// Hides all elements matching current <see cref="JQuerySelector"/> by fading them to transparent.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper FadeOut(decimal duration)
        {
            this.Run("fadeOut(" + duration + ")");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/> by fading them to opaque.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper FadeToggle()
        {
            this.Run("fadeToggle()");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/> by fading them to opaque.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <exception cref="FormatException">Format contains an invalid specification. </exception>
        /// <exception cref="InvalidOperationException">
        /// Format equals "X", but the enumeration type is unknown.
        /// </exception>
        public ChainJQueryHelper FadeToggle(Duration duration)
        {
            this.Run("fadeToggle('" + duration.ToString("G").ToLowerInvariant() + "')");
            return this;
        }

        /// <summary>
        /// Shows all elements matching current <see cref="JQuerySelector"/> by fading them to opaque.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper FadeToggle(decimal duration)
        {
            this.Run("fadeToggle(" + duration + ")");
            return this;
        }

        /// <summary>
        /// Adjust the opacity of all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="opacity">The opacity to be set. Must be a value between 0 and 1.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <exception cref="ArgumentException">Opacity is negative or opacity is bigger than one.</exception>
        /// <exception cref="FormatException">Format contains an invalid specification. </exception>
        /// <exception cref="InvalidOperationException">
        /// Format equals "X", but the enumeration type is unknown.
        /// </exception>
        public ChainJQueryHelper FadeTo(Duration duration, decimal opacity)
        {
            if (opacity < 0)
            {
                throw new ArgumentException("Opacity cannot be negative", "opacity");
            }

            if (opacity > 1)
            {
                throw new ArgumentException("Opacity cannot be bigger than 1", "opacity");
            }

            this.Run("fadeTo('" + duration.ToString("G").ToLowerInvariant() + "', " + opacity + ")");
            return this;
        }

        /// <summary>
        /// Adjust the opacity of all elements matching current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="opacity">The opacity to be set. Must be a value between 0 and 1.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <exception cref="ArgumentException">Opacity is negative or opacity is bigger than one.</exception>
        public ChainJQueryHelper FadeTo(decimal duration, decimal opacity)
        {
            if (opacity < 0)
            {
                throw new ArgumentException("Opacity cannot be negative", "opacity");
            }

            if (opacity > 1)
            {
                throw new ArgumentException("Opacity cannot be bigger than 1", "opacity");
            }

            this.Run("fadeTo(" + duration + ", " + opacity + ")");
            return this;
        }

        /// <summary>
        /// Triggers a blur event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Blur()
        {
            return this.Trigger("blur");
        }

        /// <summary>
        /// Triggers a focus event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Focus()
        {
            return this.Trigger("focus");
        }

        /// <summary>
        /// Triggers a change event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Change()
        {
            return this.Trigger("change");
        }

        /// <summary>
        /// Triggers a click event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Click()
        {
            return this.Trigger("click");
        }

        /// <summary>
        /// Triggers a double click event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper DoubleClick()
        {
            return this.Trigger("dblclick");
        }

        /// <summary>
        /// Triggers a key up event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper KeyUp()
        {
            return this.Trigger("keyup");
        }

        /// <summary>
        /// Triggers a key down event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper KeyDown()
        {
            return this.Trigger("keydown");
        }

        /// <summary>
        /// Triggers a key press event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper KeyPress()
        {
            return this.Trigger("keypress");
        }

        /// <summary>
        /// Triggers a mouse up event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper MouseUp()
        {
            return this.Trigger("mouseup");
        }

        /// <summary>
        /// Triggers a mouse down event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper MouseDown()
        {
            return this.Trigger("mousedown");
        }

        /// <summary>
        /// Triggers a mouse out event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper MouseOut()
        {
            return this.Trigger("mouseout");
        }

        /// <summary>
        /// Triggers a mouse over event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper MouseOver()
        {
            return this.Trigger("mouseover");
        }

        /// <summary>
        /// Triggers a mouse move event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper MouseMove()
        {
            return this.Trigger("mousemove");
        }

        /// <summary>
        /// Triggers a mouse enter event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper MouseEnter()
        {
            return this.Trigger("mouseenter");
        }

        /// <summary>
        /// Triggers a mouse leave event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper MouseLeave()
        {
            return this.Trigger("mouseleave");
        }

        /// <summary>
        /// Triggers a resize event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Resize()
        {
            return this.Trigger("resize");
        }

        /// <summary>
        /// Triggers a scroll event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Scroll()
        {
            return this.Trigger("scroll");
        }

        /// <summary>
        /// Execute all handlers and behaviors attached to the matched elements for the given event type on the 
        /// current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="eventName">The name of the JavaScript event to be triggered.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        public ChainJQueryHelper Trigger(string eventName)
        {
            this.Run("trigger('" + eventName + "')");
            return this;
        }

        /// <summary>
        /// Execute all handlers attached to an element for an event on the current <see cref="JQuerySelector"/>.
        /// </summary>
        /// <param name="eventName">The name of the JavaScript event to be triggered.</param>
        /// <returns>The instance of <see cref="ChainJQueryHelper"/> to allow setter chaining.</returns>
        /// <remarks>
        /// The behavior of this method is similar to <see cref="Trigger"/>, with the following exceptions:
        /// <list type="bullet">
        /// The <c>.triggerHandler("event")</c> method will not call <c>.event()</c> on the element it is 
        /// triggered on. This means <c>.triggerHandler("submit")</c> on a form will not call <c>.submit()</c> on the 
        /// form.
        /// </list>>
        /// <list type="bullet">
        /// While <c>.trigger()</c> will operate on all elements matched by the jQuery object,
        /// <c>.triggerHandler()</c> only affects the first matched element.
        /// </list>
        /// <list type="bullet">
        /// Events triggered with <c>.triggerHandler()</c> do not bubble up the DOM hierarchy; if they are not handled 
        /// by the target element directly, they do nothing.
        /// </list>
        /// <list type="bullet">
        /// Instead of returning the jQuery object (to allow chaining), <c>.triggerHandler()</c> returns whatever 
        /// value was returned by the last handler it caused to be executed. If no handlers are triggered, it returns 
        /// undefined.
        /// </list>
        /// </remarks>
        public ChainJQueryHelper TriggerHandler(string eventName)
        {
            this.Run("triggerHandler('" + eventName + "')");
            return this;
        }

        /// <summary>
        /// Performs a jQuery search on the <see cref="IWebDriver"/> using current <see cref="JQuerySelector"/> 
        /// selector and script format string.
        /// </summary>
        /// <typeparam name="T">The type of the result to be returned.</typeparam>
        /// <param name="scriptFormat">The format string of the script to be invoked.</param>
        /// <param name="wrapperFormat">
        /// The wrapper format string for the purpose of wrap the jQuery selection result.
        /// </param>
        /// <returns>Parsed result of invoking the script.</returns>
        /// <remarks>
        /// Because of the limitations of the Selenium the only valid types are: <see cref="long"/>, 
        /// <see cref="Nullable{Long}"/>, <see cref="bool"/>, <see cref="Nullable"/>, <see cref="string"/>, 
        /// <see cref="IWebElement"/> and <see cref="IEnumerable{IWebElement}"/>.
        /// Selenium returns different types depending if element has been found or not. If there's a match a
        /// <see cref="ReadOnlyCollection{IWebElement}"/> is returned, but if there are no matches than it will return
        /// an empty <see cref="ReadOnlyCollection{T}"/>.
        /// </remarks>
        private T Find<T>(string scriptFormat, string wrapperFormat = null)
        {
            this.Driver.JQuery().Load();
            return ParseUtil.ParseResult<T>(this.ExecuteScript(this.Selector, scriptFormat, wrapperFormat));
        }

        /// <summary>
        /// Runs a jQuery script on the <see cref="IWebDriver"/> using current <see cref="JQuerySelector"/> selector.
        /// </summary>
        /// <param name="script">The script to be executed in order to set the value.</param>
        private void Run(string script)
        {
            this.Driver.JQuery().Load();
            this.ExecuteScript(this.Selector, script, null);
        }

        /// <summary>
        /// Executes jQuery script.
        /// </summary>
        /// <param name="selector">The Selenium jQuery selector.</param>
        /// <param name="scriptFormat">The format string of the script to be invoked.</param>
        /// <param name="wrapperFormat">
        /// The wrapper format string for the purpose of wrap the jQuery selection result.
        /// </param>
        /// <returns>Result of invoking the script.</returns>
        private object ExecuteScript(
            JQuerySelector selector,
            string scriptFormat,
            string wrapperFormat)
        {
            if (this.WebElement != null)
            {
                selector = this.CreateSelector();
            }

            var script = selector + "." + scriptFormat;
            if (wrapperFormat != null)
            {
                script = string.Format(CultureInfo.InvariantCulture, wrapperFormat, script);
            }

            script = "return " + script + ";";

            return this.Driver.ExecuteScript<object>(script);
        }

        /// <summary>
        /// Creates the jQuery selector limiting the scope of the search to descendants of current element.
        /// </summary>
        /// <returns>The jQuery selector limiting the scope of the search to descendants of current element.</returns>
        private JQuerySelector CreateSelector()
        {
            var rootSelector = new JQuerySelector(this.WebElement.Path, jQueryVariable: this.Selector.JQueryVariable);
            return new JQuerySelector(this.Selector.RawSelector, rootSelector, this.Selector.JQueryVariable);
        }
    }
}
