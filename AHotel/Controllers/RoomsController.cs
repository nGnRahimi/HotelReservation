using Microsoft.AspNetCore.Mvc;

namespace AHotel.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
