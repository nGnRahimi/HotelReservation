
using MediatR;

namespace Application.Features.Search.Query
{
    public class SearchRoomQuery : IRequest<List<SearchRoomQuery>>
    {
        public int HotelId { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }
}
