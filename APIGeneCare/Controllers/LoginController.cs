using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
namespace APIGeneCare.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Linq;
    using global::APIGeneCare.Data;

    namespace APIGeneCare.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class LoginController : ControllerBase
        {
            private readonly GeneCareContext _context;

            public LoginController(GeneCareContext context)
            {
                _context = context;
            }

            [HttpPost]
            [Route("login")]
            public async Task<IActionResult> Login([FromBody] LoginRequest request)
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                    return BadRequest("Username and password are required.");

                var user = _context.Users
                    .FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);

                if (user == null)
                    return Unauthorized("Invalid username or password.");

                return Ok(new {user});
            }
        }

        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
