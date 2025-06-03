
using Microsoft.AspNetCore.Mvc;

namespace GeneCare.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(String Email, String Password, String ConfirmPassword)
        { 
                return View("Index");
        }

    }
}

