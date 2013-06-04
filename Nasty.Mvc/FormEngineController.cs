using System.Web.Mvc;
using Nasty.Core;
using System;
using Nasty.Js;
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

            var expr = new FormEngine(parameterProvider, viewRenderer, ClientSideFormPersister.Instance)
                .ProcessEvent(new ExceptionHandler());

            //if(resp.isCommitted()) return;

            resp.ContentType = "text/javascript";
            resp.AddHeader("Cache-Control", "no-cache");
            resp.AddHeader("Cache-Control", "no-store");
            resp.AddHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
            resp.AddHeader("Pragma", "no-cache"); // HTTP 1.0
            resp.Expires = 0;
            resp.ExpiresAbsolute = DateTime.Now; // Proxies.

            resp.Output.Write(expr.Encode());
        }
    }

    class ExceptionHandler : IExceptionHandler
    {
        public IJsExpression Handle(Exception e) {
            return new JsCall("alert", e.Message);
        }
    }
}