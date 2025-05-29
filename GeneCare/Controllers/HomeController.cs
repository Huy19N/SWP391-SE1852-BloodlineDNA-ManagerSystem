using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using GeneCare.Models;
using GeneCare.Models.DAO;
using GeneCare.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;

namespace GeneCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.Title = "Privacy Policy";
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login([FromForm]String email, [FromForm]String password)
        {
            UserDTO user = new UserDAO().getUser(email, password);
            if  (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password) || user == null)
            {
                _logger.LogWarning("Login failed for email: {Email}", email);
                var errorModel = new ErrorViewModel
                {
                    ErrorLoginEmaiPassword = true
                };
                return View(errorModel);
            }

            HttpContext.Session.SetString("UserEmail", user.Email);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Register([FromForm]String email, [FromForm]String password, [FromForm]String confpassword)
        {   
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
