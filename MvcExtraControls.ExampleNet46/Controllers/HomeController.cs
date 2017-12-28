using System.Collections.Generic;
using System.Web.Mvc;
using MvcExtraControls.ExampleNet46.Models;

namespace MvcExtraControls.ExampleNet46.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new ViewModel()
            {
                StringList = new List<string> { "One", "Two", "Three" }
            };

            var nestedModel = new ViewModel()
            {
                StringList = new List<string> { "Four", "Five", "Six" }
            };

            model.NestedModel = nestedModel;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ViewModel model)
        {
            return View(model);
        }
    }
}