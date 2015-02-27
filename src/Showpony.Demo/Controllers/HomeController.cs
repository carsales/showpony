using Showpony.Demo.Models;
using Showpony.Demo.Repositories;
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

        [ExperimentCheckpoint("Dharma Initiative")]
        public ActionResult Checkpoint()
        {
            return PartialView();
        }

        [EndExperiment("Dharma Initiative")]
        public ActionResult Exit()
        {
            var stats = new ShowponyRepository().SelectShowponyStats("Dharma Initiative");

            return View("Exit", new ExperimentResultsViewModel("Dharma Initiative", stats));
        }
    }
}
