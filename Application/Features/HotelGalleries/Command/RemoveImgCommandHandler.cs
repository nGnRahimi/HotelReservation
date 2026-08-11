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
    public class RemoveImgCommandHandler : IRequestHandler<RemoveImgCommand, bool>
    {
        private readonly IHotelGalleryRepository _hotelGalleryRepository;
        private readonly IFileUploadService _fileUploadService;
        public RemoveImgCommandHandler(IHotelGalleryRepository hotelGalleryRepository, IFileUploadService fileUploadService)
        {
            _hotelGalleryRepository = hotelGalleryRepository;
            _fileUploadService = fileUploadService;
        }
        public async Task<bool> Handle(RemoveImgCommand request, CancellationToken cancellationToken)
        {
            var file = await _hotelGalleryRepository.FindAsync(request.Id);
            var res = await _fileUploadService.RemoveFile(file.Path);
            if (res)
            {
                _hotelGalleryRepository.Delete(file);
                await _hotelGalleryRepository.UnitOfWork.SaveEntitiesAsync();
                return true;
            }
            return false;
        }
    }
}
