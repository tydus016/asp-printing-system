using Microsoft.AspNetCore.Mvc;

namespace printingsystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
