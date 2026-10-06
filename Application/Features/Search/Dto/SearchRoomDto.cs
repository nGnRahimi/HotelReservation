

namespace Application.Features.Search.Dto
{
    public class SearchRoomDto
    {
        public string RoomName { get; set; }
        public int RoomId { get; set; }
        public string Img { get; set; }
        public int Extra {  get; set; }
        public long Price { get; set; }
        public int Qty { get; set; }
        public string HotelName { get; set; }
        public List<DailySearchRoom> Details { get; set; }

        public SearchRoomDto()
        {
            Details = new List<DailySearchRoom>();
        }
    }

    public class DailySearchRoom
    {
        public DateTime DateVal {  get; set; }
        public int Qty { get; set; }
        public bool State { get; set; }
        public long Price { get; set; }
        public long BedPrice { get; set; }
    }

}
