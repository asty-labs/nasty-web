using System.Web.Mvc;
using Nasty.Mvc;

namespace Nasty.Samples.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            FormViewer.ExposeControllerContext(ControllerContext);
            return View();
        }

    }
}
