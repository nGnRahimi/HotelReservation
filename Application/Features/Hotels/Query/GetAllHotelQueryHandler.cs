using Application.Features.Hotels.Dto;
using Domain.Models.Hotels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Hotels.Query
{
    public class GetAllHotelQueryHandler : IRequestHandler<GetAllHotelQuery, List<HotelDto>>
    {
        private readonly IHotelRepository _hotelRepository;

        public GetAllHotelQueryHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<List<HotelDto>> Handle(GetAllHotelQuery request, CancellationToken cancellationToken)
        {
           return await _hotelRepository.Get(a => a.Enable && a.State)
                .Select(s => new HotelDto() 
                {
                Id = s.Id,
                Name = s.Name
                }).ToListAsync();
        }
    }
}
