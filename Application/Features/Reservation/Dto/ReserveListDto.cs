using Application.Common.Extenstios;
using Application.Common.Extentions;
using Domain.Models.Reservation.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservation.Dto
{
    public class ReserveListDto
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public string FromStr { get { return From.ToPersianDate(); } }
        public DateTime To { get; set; }
        public string ToStr { get { return To.ToPersianDate(); } }

        public int HotelId { get; set; }
        public string HotelName { get; set; }

        public long TotalPrice { get; set; }
        public ReserveStatus Status { get; set; }
        public string StatusStr { get { return Status.GetDescription(); } }

        public DateTime ReserveCreate { get; set; }
        public string CreateStr { get { return ReserveCreate.ToPersianDate(); } }

        public string Rooms { get; set; }
        public string GuestName { get; set; }

        public bool IsCanceled { get; set; }

        public long CancelAmount { get; set; }
    }
}
