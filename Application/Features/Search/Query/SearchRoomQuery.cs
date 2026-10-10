
using Application.Features.Search.Dto;
using MediatR;

namespace Application.Features.Search.Query
{
    public class SearchRoomQuery : IRequest<List<SearchRoomDto>>
    {
        public int HotelId { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }
}
