

using Application.Features.Hotels.Dto;
using MediatR;

namespace Application.Features.Hotels.Query
{
    public class GetAllHotelQuery : IRequest<List<HotelDto>>
    {

    }
}
