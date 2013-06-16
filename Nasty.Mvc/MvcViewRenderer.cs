using Nasty.Core;
using System.Web.Mvc;
using System.IO;
namespace Nasty.Mvc
{
    /**
     * This is ViewRenderer implementation to resolve and render JSP-views via RequestDispatcher.include.
     * Naming convention for the view path resolution:
     * <Form package as directories>/<Form class name>[_<fragment name>].jsp
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class MvcViewRenderer : IViewRenderer {

        private readonly ControllerContext _controllerContext;

        public MvcViewRenderer(ControllerContext controllerContext)
        {
            _controllerContext = controllerContext;
        }

        public string RenderMainView(Form form, object model) {
            return RenderFragment(form, null, model);
        }

        static ViewDataDictionary PrepareViewData(object model)
        {
            if (model is ViewDataDictionary) return model as ViewDataDictionary;
            var viewData = new ViewDataDictionary();
            viewData["model"] = model;
            return viewData;
        }

        public string RenderFragment(Form form, string fragmentName, object model) {
            var viewData = PrepareViewData(model);
            viewData["currentForm"] = form;
            var viewName = fragmentName == null ? "~/Forms/" + form.GetType().Name + ".aspx" : "~/Forms/" + form.GetType().Name + "_" + fragmentName + ".ascx";
            return RenderPartialViewToString(viewName, viewData);
        }

        protected string RenderPartialViewToString(string viewName, ViewDataDictionary viewData)
        {
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(_controllerContext, viewName);
                var viewContext = new ViewContext(_controllerContext, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}