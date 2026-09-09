using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoomPrices.Dto
{
    public class RoomPriceDto
    {
        public List<RoomPricesName> Rooms { get; set; }
        public List<DateTime> Days { get; set; }
    }
    
    public class RoomPricesName
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public List<RoomPriceList> RoomPrices { get; set; }
    }



    public class RoomPriceList
    {
        public DateTime DateVal { get; set; }
        public long Price {  get; set; }
        public long BedPrice { get; set; }
    }

}
