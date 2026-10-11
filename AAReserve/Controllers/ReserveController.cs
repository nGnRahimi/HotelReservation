using Application.Features.Reservation.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AAReserve.Controllers
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


        public async Task<IActionResult> Book(ReserveCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}
