using Domain.BaseEntity;
using Domain.Models.Hotels;
using Domain.Models.Reservation.Enum;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Reservation
{
    public class Reserve : BaseEntity<int>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int HotelId { get; set; }
        public int UserId { get; set; }

        public long TotalPrice { get; set; }
        public ReserveStatus Status { get; set; }

        public DateTime ReserveCreate { get; set; }

        public bool IsCanceled { get; set; }

        public long CancelAmount { get; set; }


        public Hotel Hotel { get; set; }
        public User User { get; set; }

        public ICollection<ReserveDetail>? ReserveDetails { get; set; }
      

    }
}
