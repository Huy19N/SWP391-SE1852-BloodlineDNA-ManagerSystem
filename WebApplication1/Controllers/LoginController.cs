using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using GeneCare.Models.DTO;
using GeneCare.Models.DAO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using GeneCare.Models.Utils;
namespace GeneCare.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(UserDTO user)
        {           
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                ViewBag.Error = "Email and Password cannot be empty.";
                return View("Index");
            }
            var userDTO = new UserDAO().getUser(user.Email, user.Password);

            if (userDTO == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View("Index");
            }

            HttpContext.Session.setObject("user", userDTO);
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

           
            

            //return Json(claims);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();
            // Sign out of the authentication cookie
            await HttpContext.SignOutAsync();
            return View("Index");
        }
    }
}
