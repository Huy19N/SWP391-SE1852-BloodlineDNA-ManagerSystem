using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using GeneCare.Models;
using GeneCare.Models.DAO;
using GeneCare.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            String email = form["email"];
            String password = form["password"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return View();
            }

            UserDTO user = new UserDAO().getUser(email, password);
            if (user == null)
            {
                _logger.LogWarning("Login failed for email: {Email}", email);
                var ErrorModel = new ErrorViewModel() { ErrorLoginEmaiPassword = true };
                return View(ErrorModel);
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(IFormCollection form)
        {
            String email = form["email"];
            String password = form["password"];
            String confirmPassword = form["confpassword"];

            if(String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(confirmPassword))
            {
                _logger.LogWarning("Registration failed due to empty fields.");
                return View();
            }


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
