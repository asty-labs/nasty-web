using System.Web;
using System.Web.Mvc;
using Nasty.Core;

namespace Nasty.Mvc
{
    public abstract class FormEngineFactory
    {
        public static FormEngineFactory Instance { get; set; }

        public abstract FormEngine GetFormEngine(HttpContext httpContext, ControllerContext controllerContext);
    }
}
