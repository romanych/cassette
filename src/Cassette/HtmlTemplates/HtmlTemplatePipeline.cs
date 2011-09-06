﻿using System.Collections.Generic;
using Cassette.ModuleProcessing;

namespace Cassette.HtmlTemplates
{
    public class HtmlTemplatePipeline : MutablePipeline<HtmlTemplateModule>
    {
        protected override IEnumerable<IModuleProcessor<HtmlTemplateModule>> CreatePipeline(HtmlTemplateModule module, ICassetteApplication application)
        {
            yield return new AssignRenderer(new InlineHtmlTemplateModuleRenderer());
        }
    }
}