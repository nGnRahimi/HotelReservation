using Application.Features.Search.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Search.Query
{
    public class SelectedRoomQuery : IRequest<List<SearchRoomDto>>
    {
        public int HotelId { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public List<RoomCounts> Rooms { get; set; }
    }

    public class RoomCounts
    {
        public int RoomId { get; set; }
        public int Qty { get; set; }
    }
}
