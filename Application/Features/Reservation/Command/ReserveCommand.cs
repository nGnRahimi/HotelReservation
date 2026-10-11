using Application.Common.MediatR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservation.Command
{
    public class ReserveCommand : BaseCommandRequest, IRequest<bool>
    {
        public int ToHotelId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public List<RoomReserve> Rooms { get; set; }

    }

    public class RoomReserve
    {
        public int NumberOfExtrabed { get; set; }
        public int RoomId { get; set; }
        public List<RoomGuest> Guests { get; set; }
    }

    public class RoomGuest
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Code { get; set; }
    }
}
