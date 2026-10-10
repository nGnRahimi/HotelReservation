using Domain.BaseEntity;
using Domain.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Reservation
{
    public class ReserveDetail : BaseEntity<int>
    {
        public int RoomId { get; set; }
        public int ReserveId { get; set; }
        public long Price { get; set; }
        public long BedPrice { get; set; }

        public int NumberOfExtraBed { get; set; }


        public Room Room { get; set; }
        public Reserve Reserve { get; set; }

        public ICollection<ReserveGuest>? ReserveGuests { get; set; }
        public ICollection<ReserveDailyDetail>? ReserveDailyDetails { get; set; }

    }
}
