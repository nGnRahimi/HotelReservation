using Microsoft.AspNetCore.Mvc;

namespace AAReserve.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
