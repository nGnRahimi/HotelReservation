using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservation.Dto
{
    public class ReservePriceDto
    {
        public long Price { get; set; }
        public long BedPrice { get; set; }
        public List<ReserveDailyPriceDto> Details { get; set; }
        public ReservePriceDto()
        {
            Details = new List<ReserveDailyPriceDto>();
        }
    }

    public class ReserveDailyPriceDto
    {
        public DateTime DateVal { get; set; }
        public long Price { get; set; }
        public long BedPrice { get; set; }
    }
}
