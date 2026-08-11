using Application.Features.HotelGalleries.Command;
using Application.Features.HotelGalleries.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AHotel.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IMediator _mediator;
        public GalleryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {

            var res = await _mediator.Send(new GetHotelImagesQuery());
            return View(res);
        }
        

        public async Task<IActionResult> SetImg(IFormFile file)
        {
            var command = new CreateImgCommand()
            {
                file = file
            };
           await _mediator.Send(command);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveImg(int id)
        {
            var command = new RemoveImgCommand()
            {
                Id = id
            };
            await _mediator.Send(command);
            return RedirectToAction("Index");
        }


    }
}
