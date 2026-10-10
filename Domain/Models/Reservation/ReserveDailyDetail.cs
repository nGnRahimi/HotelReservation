using Domain.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Reservation
{
    public class ReserveDailyDetail : BaseEntity<int>
    {
        public long Price { get; set; }
        public long BedPrice { get; set; }
        public DateTime DateVal { get; set; }
        public int NumberOfExtraBed { get; set; }

        public int ReserveDetailId { get; set; }

        public ReserveDetail ReserveDetail { get; set; }
    }
}
