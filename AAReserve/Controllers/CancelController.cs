using Application.Features.Cancelation.Command;
using Application.Features.Cancelation.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AAReserve.Controllers
{
    public class CancelController : Controller
    {
        private readonly IMediator _mediator;

        public CancelController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetPenalty(int id)
        {
            var data = await _mediator.Send(new GetReserveCancelQuery()
            {
                HotelID = id
            });
            return PartialView(data);
        }



        public async Task<IActionResult> ConfirmCancel(CancelReserveCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(data);
        }
    }
}
