using Application.Features.Cancelation.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Print.Printers.Create;

namespace AHotel.Controllers
{
    public class CancelationController : Controller
    {
        private readonly IMediator _mediator;

        public CancelationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateView()
        {
            return PartialView();
        }

        public async Task<IActionResult> CreatePolicy(CreatePolicyCommand command)
        {
            var r = await _mediator.Send(command);
            return Ok(r);
        }


    }
}
