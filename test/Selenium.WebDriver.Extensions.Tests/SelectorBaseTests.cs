namespace Selenium.WebDriver.Extensions.Tests
{
    using AutoFixture.Xunit2;
    using FluentAssertions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;
    using static Shared.Trait;

    [Trait(CATEGORY, UNIT)]
    [ExcludeFromCodeCoverage]
    public class SelectorBaseTests
    {
        [Theory]
        [AutoData]
        public void ShouldCorrectlyHandleCollections(ReadOnlyCollection<object> rawResult)
        {
            var sut = SelectorBase<JQuerySelector>.ParseResult<IEnumerable<IWebElement>>(rawResult);

            sut.Should().NotBeNull();
        }

        [Fact]
        public void ShouldCorrectlyHandleDefaultValue()
        {
            const object rawResult = null;
            var sut = SelectorBase<JQuerySelector>.ParseResult<bool>(rawResult);

            sut.Should().BeFalse();
        }

        [Theory]
        [AutoData]
        public void ShouldCorrectlyHandleDoubleValue(double rawResult)
        {
            var sut = SelectorBase<JQuerySelector>.ParseResult<long>(rawResult);

            sut.GetType().Should().Be(typeof(long));
        }
    }
}
