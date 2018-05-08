namespace Selenium.WebDriver.Extensions.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using JetBrains.Annotations;
    using Nancy;

    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class TestCaseModule : NancyModule
    {
        public const string JQUERY_LOADED = "/jQueryLoaded";
        public const string JQUERY_UNLOADED = "/jQueryUnloaded";
        public const string SIZZLE_LOADED = "/SizzleLoaded";
        public const string SIZZLE_UNLOADED = "/SizzleUnloaded";

        public TestCaseModule()
        {
            Get[JQUERY_LOADED] = _ => GetHtml(new Uri("https://code.jquery.com/jquery-2.1.1.min.js"));
            Get[JQUERY_UNLOADED] = _ => GetHtml();
            Get[SIZZLE_LOADED] = _ => GetHtml(new Uri("https://cdnjs.cloudflare.com/ajax/libs/sizzle/2.0.0/sizzle.min.js"));
            Get[SIZZLE_UNLOADED] = _ => GetHtml();
        }

        private static string GetHtml(Uri externalLibUrl = null) =>
            $@"<!DOCTYPE html>
            <html>
            <head>
                <meta charset=""utf-8"" />
                <title>Selenium.WebDriver.Extensions Tests</title>
            </head>
            <body>
                <h1 class=""main"">H1 Header</h1>
                <div class=""main"" id=""id1"">Selector</div>
                <div class=""main"">Selenium WebDriver Extensions</div>
                {(externalLibUrl != null ? $@"<script src = ""{externalLibUrl.AbsoluteUri}"" type=""text/javascript""></script>" : string.Empty)}
            </body>
            </html>";
    }
}