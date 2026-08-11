using Application.FileUpload;
using Domain.Models.HotelGalleries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.HotelGalleries.Command
{
    public class CreateImgCommandHandler : IRequestHandler<CreateImgCommand, bool>
    {
        private readonly IHotelGalleryRepository _hotelGalleryRepository;
        private readonly IFileUploadService _fileUploadService;
        public CreateImgCommandHandler(IHotelGalleryRepository hotelGalleryRepository ,  IFileUploadService fileUploadService)
        {
            _hotelGalleryRepository = hotelGalleryRepository;
            _fileUploadService = fileUploadService;
        }
        public async Task<bool> Handle(CreateImgCommand request, CancellationToken cancellationToken)
        {
            var name = await _fileUploadService.UploadFileAsync(request.file);
            var hotelGallery = new HotelGallery()
            {
                HotelId = request.HotelId.Value,
                Path = name,
            };
            _hotelGalleryRepository.Add(hotelGallery);
            await _hotelGalleryRepository.UnitOfWork.SaveEntitiesAsync();
            return true;

        }
    }
}
