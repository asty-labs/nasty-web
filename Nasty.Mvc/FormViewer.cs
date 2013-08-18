using System.Web.UI;
using Nasty.Core;
using System.Web;
using System.Web.Mvc;
namespace Nasty.Mvc
{
    public class FormViewer : Control {

        protected override void Render(HtmlTextWriter writer)
        {
            var form = FormEngine.CreateInitialForm(EntryPointClass, Parameters);
			form.Id = ID;
            var req = HttpContext.Current.Request;
            var parameterProvider = new RequestParameterProvider(req);
            var viewRenderer = new MvcViewRenderer((ControllerContext)HttpContext.Current.Items["controllerContext"]);
			var formEngine = new FormEngine(parameterProvider, viewRenderer, ClientSideFormPersister.Instance, new DefaultMethodInvoker());
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
    }
}