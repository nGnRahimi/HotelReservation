
using Application.Features.Search.Dto;
using MediatR;

namespace Application.Features.Search.Query
{
    public class GetHotelInfoQuery : IRequest<HotelInfoDto>
    {
        public int Id { get; set; }
    }
}
