using Application.Features.Reservation.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AHotel.Controllers
{
    public class ReserveController : Controller
    {
        private readonly IMediator _mediator;

        public ReserveController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetList(int page = 1, string? search = null, string? from = null, string? to = null)
        {
            var res = await _mediator.Send(new GetReserveListQuery()
            {
                PageNumber = page,
                PageSize = 4,
                from = from,
                to = to,
                Search = search
            });
            ViewBag.search = search;
            ViewBag.from = from;
            ViewBag.to = to;
            return PartialView(res);
        }




        public async Task<IActionResult> ReserveInfo(int id)
        {
            var query = new ReserveInfoQuery()
            {
                Id = id,
            };
            var res = await _mediator.Send(query);
            return View(res);
        }
    }
}
