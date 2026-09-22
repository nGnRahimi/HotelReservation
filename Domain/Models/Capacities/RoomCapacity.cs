using Domain.BaseEntity;
using Domain.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Capacities
{
    public class RoomCapacity : BaseEntity<int>
    {
        public int RoomId { get; set; }
        public int Qty { get; set; }
        public bool State {  get; set; }
        public DateTime DateVal { get; set; }
        public Room Room { get; set; }
    }
}
