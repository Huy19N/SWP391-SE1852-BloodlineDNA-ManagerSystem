using GeneCare.Models.DAO;
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
            var UserDAO = new UserDAO();
            if (Email == null || Password == null || ConfirmPassword == null)
            {
                ViewBag.Error = "Email and Password cannot be empty.";
                return View("Index");
            }

            if (Password != ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View("Index");
            }

            if (UserDAO.addUser(Email, Password))
            {
                ViewBag.Success = "Registration successful. Please log in.";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Error = "Registration failed. Please try again.";
                return View("Index");
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
