namespace Selenium.WebDriver.Extensions.Parsers
{
    /// <summary>
    /// The <see cref="long"/> parser.
    /// </summary>
    /// <inheritdoc cref="ParserBase" />
    internal class LongParser : ParserBase
    {
        public LongParser()
            : base(new DirectCastParser())
        {
        }

        /// <inheritdoc cref="ParserBase.Parse{TResult}" />
        public override TResult Parse<TResult>(object rawResult) => rawResult is double d
            ? (TResult)(object)(long?)d
            : Successor.Parse<TResult>(rawResult);
    }
}