﻿using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using Statiq.Common;
using Statiq.Web.Pipelines;

namespace Statiq.Web
{
    public abstract class HtmlAnalyzer : Analyzer
    {
        protected HtmlAnalyzer()
        {
            PipelinePhases.Add(nameof(AnalyzeContent), Phase.Process);
        }

        protected override sealed async Task AnalyzeDocumentAsync(IDocument document, IAnalyzerContext context)
        {
            IHtmlDocument htmlDocument = null;
            if (document.MediaTypeEquals(MediaTypes.Html))
            {
                htmlDocument = await document.ParseHtmlAsync(false);
            }

            if (htmlDocument is object)
            {
                await AnalyzeAsync(htmlDocument, document, context);
            }
        }

        protected abstract Task AnalyzeAsync(IHtmlDocument htmlDocument, IDocument document, IAnalyzerContext context);
    }
}