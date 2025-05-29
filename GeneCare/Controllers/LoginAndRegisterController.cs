using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace GeneCare.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
