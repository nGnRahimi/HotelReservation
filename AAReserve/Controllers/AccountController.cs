using Microsoft.AspNetCore.Mvc;

namespace AAReserve.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
