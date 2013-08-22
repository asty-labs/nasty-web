using System.Web;
using System.Web.Mvc;
using Nasty.Core;

namespace Nasty.Mvc
{
    public class DefaultMvcFormEngineFactory : FormEngineFactory
    {
        private readonly IFormPersister _formPersister = new ClientSideFormPersister();
        private readonly IMethodInvoker _methodInvoker = new SimpleErrorHandler(new DefaultMethodInvoker());

        public override FormEngine GetFormEngine(HttpContext httpContext, ControllerContext controllerContext)
        {
            var req = httpContext.Request;
            var parameterProvider = new RequestParameterProvider(req);
            var viewRenderer = new MvcViewRenderer(controllerContext);

            return new FormEngine(parameterProvider, viewRenderer, _formPersister,
                           _methodInvoker);
        }
    }
}
