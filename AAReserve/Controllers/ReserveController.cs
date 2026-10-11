using Application.Features.Reservation.Command;
using Microsoft.AspNetCore.Mvc;

namespace AAReserve.Controllers
{
    public class ReserveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Book(ReserveCommand command)
        {
            return Ok();
        }
    }
}
