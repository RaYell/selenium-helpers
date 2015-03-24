﻿namespace Selenium.WebDriver.Extensions.Sizzle
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Selenium.WebDriver.Extensions.Core;
    
    /// <summary>
    /// The Sizzle loader.
    /// </summary>
    public class SizzleLoader : ExternalLibraryLoaderBase
    {
        /// <summary>
        /// Gets the default URI of the external library.
        /// </summary>
        public override Uri LibraryUri
        {
            get
            {
                return new Uri("https://cdnjs.cloudflare.com/ajax/libs/sizzle/2.0.0/sizzle.min.js");
            }
        }

        /// <summary>
        /// Gets the JavaScript to check if the prerequisites for the selector call have been met. The script should 
        /// return <c>true</c> if the prerequisites are ok; otherwise, <c>false</c>.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly",
            Justification = "False positive.")]
        public override string CheckScript
        {
            get
            {
                return DetectScriptCode + "(window.Sizzle)";
            }
        }

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
                throw new ArgumentNullException("args");
            }

            if (args.Length == 0)
            {
                throw new LoaderException("No Sizzle URI given");
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}('{1}')", LoadScriptCode, args.First());
        }
    }
}
