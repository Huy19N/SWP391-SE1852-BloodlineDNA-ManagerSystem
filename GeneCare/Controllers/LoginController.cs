using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using GeneCare.Models;
using GeneCare.Models.Utils;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
namespace GeneCare.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Users user)
        {
            if (String.IsNullOrEmpty(user.Email))
            {
                ModelState.AddModelError("Email", "Email can not null");
            }

            if (String.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("Password", "Password can not null");
            }

            ModelState.AddModelError("errorLogin", "Wrong email or password");




            return RedirectToAction("Index", "Home");
        }
        public async Task LoginWithGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
        }
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result?.Principal?.Identities.FirstOrDefault()?.Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            var email = result?.Principal?.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
            var name = result?.Principal?.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

            var user = new Users().getUser(null, email, null);
            if (user == null || user.UserId < 0)
            {
                user = new Users
                {
                    FullName = name,
                    Email = email,
                    Role = new Role { RoleId = 1 },
                    Address = "Not provided",
                    Phone = "Not provided"
                };
                await HttpContext.SignOutAsync();
                return View("SetPassword", user);
            }

            await HttpContext.SignOutAsync();
            HttpContext.Session.setObject("UserEmail", email);

            //return Json(claims);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();       
            return View("Index");
        }

        public IActionResult SetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SetPassword(Users user)
        {
            var cPassword = Request.Form["confirmPassword"];
            if (user.Password.Equals(cPassword))
            {
                ModelState.AddModelError("confirmPassword", "Passwords do not match.");
                return View(user);
            }

            if (user.addUser())
            {
                HttpContext.Session.setObject("user", user);
                return RedirectToAction("Index","Home");
            }
            return View(user);
        }
    }
}
