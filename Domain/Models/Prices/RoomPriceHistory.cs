
using Domain.BaseEntity;
using Domain.Models.Rooms;

namespace Domain.Models.Prices
{
    public class RoomPriceHistory : BaseEntity<int>
    {
        public int RoomId { get; set; }
        public long Price { get; set; }
        public long BedPrice { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public Room Room { get; set; }
        public RoomPriceHistory() { }
        public RoomPriceHistory(int roomId, long price, long bedPrice, DateTime from, DateTime? to)
        {
            RoomId = roomId;
            Price = price;
            BedPrice = bedPrice;
            From = from;
            To = to;
        }
    }
}
