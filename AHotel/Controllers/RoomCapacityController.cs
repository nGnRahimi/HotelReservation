using Domain.Models.Capacities;
using Microsoft.AspNetCore.Mvc;

namespace AHotel.Controllers
{
    public class RoomCapacityController : Controller
    {
        private readonly IRoomCapacityRepository _roomCapacityRepository;

        public RoomCapacityController(IRoomCapacityRepository roomCapacityRepository)
        {
            _roomCapacityRepository = roomCapacityRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
