using Application.Features.Rooms;
using Application.Features.Rooms.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AHotel.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> GetList(int page = 1 , string? search = null)
        {
            var res = await _mediator.Send(new GetRoomsQuery()
            {
                PageNumber = page,
                PageSize = 4,
                Search = search
            });
            ViewBag.search = search;
            return PartialView(res);
        }


        public IActionResult CreateView()
        {
            return PartialView();

        }
        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }


    }
}
