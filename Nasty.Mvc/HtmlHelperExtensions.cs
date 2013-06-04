using System;
using System.Web.Mvc;
using Nasty.Core;

namespace Nasty.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static string Component(this HtmlHelper helper, Component component)
        {
            RenderingContext.Instance.RegisterComponent(component);
            if (component.VoidTag)
                return ComponentRenderingHelper.GetVoidTag(component);
            return ComponentRenderingHelper.GetStartTag(component);
        }

        public static IDisposable Begin(this HtmlHelper helper, Component component)
        {
            RenderingContext.Instance.RegisterComponent(component);
            helper.ViewContext.Writer.Write(ComponentRenderingHelper.GetStartTag(component));
            return new Container(helper.ViewContext, component);
        }

        class Container : IDisposable
        {
            readonly ViewContext _viewContext;
            readonly Component _component;

            public Container(ViewContext viewContext, Component component)
            {
                _viewContext = viewContext;
                _component = component;
            }

            public void Dispose()
            {
                _viewContext.Writer.Write(ComponentRenderingHelper.GetEndTag(_component));
            }
        }
    }
}
