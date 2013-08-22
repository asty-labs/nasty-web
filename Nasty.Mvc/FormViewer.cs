using System.Web.UI;
using Nasty.Core;
using System.Web;
using System.Web.Mvc;
namespace Nasty.Mvc
{
    public class FormViewer : Control {

        private const string CurrentContextKey = "controllerContext";

        protected override void Render(HtmlTextWriter writer)
        {
            var form = FormEngine.CreateInitialForm(EntryPointClass, Parameters);
			form.Id = ID;
            var formEngine = FormEngineFactory.Instance.GetFormEngine(HttpContext.Current, (ControllerContext)HttpContext.Current.Items[CurrentContextKey]);
			var htmlFragment = formEngine.RenderMainView(form);
            writer.Write(htmlFragment.Html);
            writer.Write(htmlFragment.Script.EncodeOnLoadAsHtml());
	    }

	    public string EntryPointClass {
		    get; set;
	    }
	
	    public string Parameters {
		    get; set;
	    }

        public static void ExposeControllerContext(ControllerContext controllerContext)
        {
            HttpContext.Current.Items[CurrentContextKey] = controllerContext;
        }
    }
}