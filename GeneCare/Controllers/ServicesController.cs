using GeneCare.Models.Utils;
using GeneCare.Models;
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
            if (HttpContext.Session.getObject<Users>("user") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Title = "Civil Services";
            return View();
        }
        public IActionResult LegalServices()
        {
            if (HttpContext.Session.getObject<Users>("user") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Title = "Legal Services";
            return View();
        }
    }
}
