﻿using System;
using System.Linq;
using System.Web;

namespace Cassette.UI
{
    public class PageAssetManager<T> : IPageAssetManager
        where T : Module
    {
        public PageAssetManager(IReferenceBuilder<T> referenceBuilder, IPlaceholderTracker placeholderTracker)
        {
            this.referenceBuilder = referenceBuilder;
            this.placeholderTracker = placeholderTracker;
        }

        readonly IReferenceBuilder<T> referenceBuilder;
        readonly IPlaceholderTracker placeholderTracker;

        public void Reference(string path, string location = null)
        {
            referenceBuilder.AddReference(path, location);
        }

        public IHtmlString Render(string location = null)
        {
            return placeholderTracker.InsertPlaceholder(
                () => CreateHtml(location)
            );
        }

        HtmlString CreateHtml(string location)
        {
            return new HtmlString(string.Join(Environment.NewLine,
                referenceBuilder.GetModules(location).Select(
                    module => module.Render().ToHtmlString()
                )
            ));
        }
    }
}
