using Application.Features.Cancelation.Command;
using Application.Features.Cancelation.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Print.Printers.Create;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        public async Task<IActionResult> List()
        {
            var res = await _mediator.Send(new GetCancelationQuery());
            return PartialView(res);
        }



        public IActionResult CreateView()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePolicy(CreatePolicyCommand command)
        {
            var r = await _mediator.Send(command);
            return Ok(r);
        }


        [HttpPost]
        public async Task<IActionResult> Delete( [FromBody] DeletePolicyCommand command)
        {
            var r = await _mediator.Send(command);
            return Ok(r);
        }



    }
}
