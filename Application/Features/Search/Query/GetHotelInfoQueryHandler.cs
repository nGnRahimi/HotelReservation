
using Application.Features.Search.Dto;
using AutoMapper;
using Domain.Models.HotelGalleries;
using Domain.Models.Hotels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Search.Query
{
    public class GetHotelInfoQueryHandler : IRequestHandler<GetHotelInfoQuery , HotelInfoDto>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        public GetHotelInfoQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<HotelInfoDto> Handle(GetHotelInfoQuery request, CancellationToken cancellationToken)
        {
            var data = await _hotelRepository.Get(a => a.Id == request.Id)
                    .Include(a => a.HotelGalleries)
                    .FirstOrDefaultAsync();

            if (hotel == null)
                return null;

            return _mapper.Map<HotelInfoDto>(hotel);
          
        }

    }
}
