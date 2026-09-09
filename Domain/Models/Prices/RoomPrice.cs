using Domain.BaseEntity;
using Domain.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Prices
{
    public class RoomPrice : BaseEntity<int>
    {
        public int RoomId { get; set; }
        public long Price {  get; set; }
        public long BedPrice { get; set; }  
        public DateTime DateVal { get; set; }   
        public Room Room { get; set; }
    }
}
