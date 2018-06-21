namespace Selenium.WebDriver.Extensions.Parsers
{
    using System.Diagnostics.CodeAnalysis;
    using static Suppress;

    internal abstract class ParserBase : IParser
    {
        [SuppressMessage(CODE_CRACKER, CC0057)]
        protected ParserBase(IParser successor = null) => Successor = successor;

        public IParser Successor { get; }

        [SuppressMessage(SONARQUBE, S4018)]
        public abstract TResult Parse<TResult>(object rawResult);
    }
}
