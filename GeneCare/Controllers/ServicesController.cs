using Microsoft.AspNetCore.Mvc;

namespace GeneCare.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CivilServices()
        {
            ViewBag.Title = "Civil Services";
            return View();
        }
        public IActionResult LegalServices()
        {
            ViewBag.Title = "Legal Services";
            return View();
        }
    }
}
