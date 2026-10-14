
using Application.Common.Extentions;
using Domain.Models.Reservation.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservation.Dto
{
    public class ReserveInfoDto
    {
        public int Id {  get; set; }
        public DateTime From { get; set; }
        public string FromStr { get { return From.ToPersianDate(); } }
        public DateTime To { get; set; }
        public string ToStr { get { return To.ToPersianDate(); } }

        public int HotelId { get; set; }
        public string HotelName { get; set; }

        public long TotalPrice { get; set; }
        public ReserveStatus Status { get; set; }

        public DateTime ReserveCreate { get; set; }
        public string CreateStr { get { return ReserveCreate.ToPersianDate(); } }


        public bool IsCanceled { get; set; }

        public long CancelAmount { get; set; }

        public List<ReserveInfoRoom> Rooms { get; set; }
    }


    public class ReserveInfoRoom
    {
        public int RoomId { get; set; }
        public long Price { get; set; }
        public long BedPrice { get; set; }
        public string RoomName { get; set; }
        public int NumberOfExtraBed { get; set; }

        public List<ReserveInfoGuest> Guests { get; set; }
        public List<ReserveInfoDaily> Dailies { get; set; }
    }

    public class ReserveInfoGuest
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string NationalCode { get; set; }
    }

    public class ReserveInfoDaily
    {
        public long Price { get; set; }
        public long BedPrice { get; set; }
        public DateTime DateVal { get; set; }
    }
}
