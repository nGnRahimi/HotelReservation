using Application.Features.Search.Query;
using Domain.Models.Hotels;
using Domain.Models.Rooms;
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

        public async Task<IActionResult> Index(string from, string to, int hotelId)
        {

            if (hotelId == 0 || String.IsNullOrEmpty(from) || String.IsNullOrEmpty(to))
            {
                return RedirectToAction("Index", "Home");
            }

            var res = await _mediator.Send(new GetHotelInfoQuery() { Id = hotelId });

            var rooms = await _mediator.Send(new SearchRoomQuery()
            {
                HotelId = hotelId,
                From = from,
                To = to,
            });


            ViewBag.rooms = rooms;
            ViewBag.hotelId = hotelId;
            ViewBag.from = from;
            ViewBag.to = to;

            return View(res);
        }



        public async Task<IActionResult> RoomReserve(SelectedRoomQuery query)
        {
            if (query.Rooms.Sum(a => a.Qty) < 1)
            {
                return RedirectToAction("Index", "Search", new { hotelId = query.HotelId, from = query.From, to = query.To });
            }

            var res = await _mediator.Send(query);
            ViewBag.hotelId = query.HotelId;
            ViewBag.from = query.From;
            ViewBag.to = query.To;
            return View(res);
        }
    }
}
