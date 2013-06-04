using System.Web.Mvc;

namespace Nasty.Samples.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpContext.Items["controllerContext"] = ControllerContext;
            return View();
        }

    }
}
