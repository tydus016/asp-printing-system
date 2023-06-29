using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using printingsystem.Data;
using printingsystem.Models;
using printingsystem.Models.Users;
using System.Security.Claims;

namespace printingsystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly Context _context;
        public AccountController(Context context)
        {
            this._context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsersModel model)
        {
            var res = _context.users.FirstOrDefault(x => x.email == model.email);

            if (res != null)
            {
                if (res.password == model.password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("fullname", res.fullname),
                        new Claim("user_id", res.id_number),
                        // Add additional claims as needed
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuthenticationScheme");
                    var authProperties = new AuthenticationProperties
                    {
                        // Set any additional authentication properties (e.g., remember me)
                    };
                    await HttpContext.SignInAsync("MyCookieAuthenticationScheme", new ClaimsPrincipal(claimsIdentity), authProperties);
                }

            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            var fullNameClaim = User.FindFirst("fullname");
            var userIdClaim = User.FindFirst("user_id");

            var specificData = _context.users.FirstOrDefault(x => x.id_number == userIdClaim.Value);

            return View(specificData);
        }

    }
}
