using Domain.BaseEntity;
using Domain.Models.Rooms;

namespace Domain.Models.Capacities
{
    public class RoomCapacityHistory : BaseEntity<int>
    {
        public int RoomId { get; set; }

        public int Capacity { get; set; }

        public DateTime From { get; set; }

        public DateTime? To { get; set; }

        public Room Room { get; set; }

        public RoomCapacityHistory()
        {
        }

        public RoomCapacityHistory(
            int roomId,
            int capacity,
            DateTime from,
            DateTime? to)
        {
            RoomId = roomId;
            Capacity = capacity;
            From = from;
            To = to;
        }
    }
}