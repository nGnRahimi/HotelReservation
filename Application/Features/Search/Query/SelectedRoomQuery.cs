
namespace Application.Features.Search.Query
{
    public class SelectedRoomQuery 
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
