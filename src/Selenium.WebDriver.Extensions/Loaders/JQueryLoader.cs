﻿namespace OpenQA.Selenium.Loaders
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// The jQuery loader.
    /// </summary>
    public class JQueryLoader : ExternalLibraryLoaderBase
    {
        /// <summary>
        /// Gets the default URI of the external library.
        /// </summary>
        [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
        public override Uri LibraryUri => new Uri("https://code.jquery.com/jquery-latest.min.js");

        /// <summary>
        /// Gets the JavaScript to check if the prerequisites for the selector call have been met. The script should
        /// return <c>true</c> if the prerequisites are met; otherwise, <c>false</c>.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly",
            Justification = "False positive.")]
        public override string CheckScript => DetectScriptCode + "(window.jQuery)";

        /// <summary>
        /// Gets the JavaScript to load the prerequisites for the selector.
        /// </summary>
        /// <param name="args">Load script arguments.</param>
        /// <returns>The JavaScript code to load the prerequisites for the selector.</returns>
        /// <exception cref="ArgumentNullException">Arguments array is null.</exception>
        /// <exception cref="LoaderException">No URI given as first parameter.</exception>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly",
            Justification = "False positive.")]
        public override string LoadScript(params string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args.Length == 0)
            {
                throw new LoaderException("No jQuery URI given");
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}('{1}')", LoadScriptCode, args.First());
        }
    }
}
