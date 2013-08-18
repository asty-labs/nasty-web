using System.Web.Mvc;
using Nasty.Core;
using System;

namespace Nasty.Mvc
{
    /**
     * Servlet to handle all requests to FormEngine
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class FormEngineController : Controller {

        public void Index() {
            var req = System.Web.HttpContext.Current.Request;
            var resp = System.Web.HttpContext.Current.Response;
            var parameterProvider = new RequestParameterProvider(req);
            var viewRenderer = new MvcViewRenderer(ControllerContext);

            var formEngine = new FormEngine(parameterProvider, viewRenderer, ClientSideFormPersister.Instance,
                           new SimpleErrorHandler(new DefaultMethodInvoker()));
            var expr = formEngine.DoProcess();

            //if(resp.isCommitted()) return;

            resp.ContentType = "text/javascript";
            resp.AddHeader("Cache-Control", "no-cache");
            resp.AddHeader("Cache-Control", "no-store");
            resp.AddHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
            resp.AddHeader("Pragma", "no-cache"); // HTTP 1.0
            resp.Expires = 0;
            resp.ExpiresAbsolute = DateTime.Now; // Proxies.

            if(expr != null) resp.Output.Write(expr.Encode());
        }
    }
}