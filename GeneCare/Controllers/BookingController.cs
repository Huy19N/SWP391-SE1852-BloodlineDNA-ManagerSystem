using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeneCare.Controllers
{
    /*[Authorize]*/
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
