using Domain.Models.HotelGalleries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.HotelGalleries.Query
{
    public class GetHotelImagesQueryHandler : IRequestHandler<GetHotelImagesQuery, List<HotelGallery>>
    {
        private readonly IHotelGalleryRepository _repository;
        public GetHotelImagesQueryHandler(IHotelGalleryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<HotelGallery>> Handle(GetHotelImagesQuery request, CancellationToken cancellationToken)
        {
          return await _repository.Get(a => a.HotelId == request.HotelId).ToListAsync();
        }
    }
}
