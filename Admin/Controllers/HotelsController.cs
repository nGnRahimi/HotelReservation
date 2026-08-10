using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class HotelsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
