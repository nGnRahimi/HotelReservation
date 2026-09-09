using Application.Features.RoomPrices;
using Application.Features.RoomPrices.Query;
using Application.Features.Rooms;
using Application.Features.Rooms.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AHotel.Controllers
{
    public class RoomPriceController : Controller
    {
        private readonly IMediator _mediator;

        public RoomPriceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }







        public async Task<IActionResult> List(DateTime? start = null , string? week = null)
        {
            start = start ?? DateTime.Now;
            if (!String.IsNullOrEmpty(week))
            {
             if(week == "next")
                    start = start.Value.AddDays(7);


                if (week == "last")
                    start = start.Value.AddDays(-7);

            }


            var res = await _mediator.Send(new GetRoomPriceQuery()
            {
                startDate = start.Value
            });

            ViewBag.start = start.ToString();

            return PartialView(res);
        }





        public async Task<IActionResult> CreateView()
        {
            var rooms = await _mediator.Send(new GetRoomsQuery()
            {
                DisablePaging = true
            });
            ViewBag.Rooms = new SelectList(rooms.Items, "Id", "Name");

            return PartialView();
        }



        [HttpPost]
        public async Task<IActionResult> CreatePrice(CreateRoomPriceCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }





    }
}
