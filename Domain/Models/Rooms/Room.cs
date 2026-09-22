using Domain.BaseEntity;
using Domain.Models.Hotels;
using Domain.Models.Prices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Rooms
{
    public class Room : BaseEntity<int>
    {
        public string Name { get; set; }    
        public int Qty { get; set; }
        public int MainCapacity { get; set; }
        public int ExtraCapacity { get; set; }
        public string Path { get; set; }
        public string? View {  get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public ICollection<RoomPrice>? RoomPrices { get; set; }
        public ICollection<RoomPriceHistory>? RoomPriceHistories { get; set; }

    }
}
