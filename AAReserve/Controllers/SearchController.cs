using Application.Features.Search.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AAReserve.Controllers
{
    public class SearchController : Controller
    {
        private readonly IMediator _mediator;

        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(string from , string to , int hotelId)
        {
            var res = await _mediator.Send(new GetHotelInfoQuery() {Id = hotelId});

            var rooms = await _mediator.Send(new SearchRoomQuery()
            {
                HotelId = hotelId,
                From = from,
                To = to,
            });

            ViewBag.rooms = rooms;


            return View(res);
        }
    }
}
