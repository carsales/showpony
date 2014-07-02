using System.Web.Mvc;

namespace Showpony.Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Static()
        {
            return PartialView();
        }

        public ActionResult Animated()
        {
            return PartialView();
        }

        [EndExperiment("Upsell")]
        public ActionResult Exit()
        {
            return View();
        }
    }
}
