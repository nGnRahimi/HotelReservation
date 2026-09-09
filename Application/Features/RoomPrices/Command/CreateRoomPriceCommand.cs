using Application.Common.MediatR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoomPrices
{
    public class CreateRoomPriceCommand : BaseCommandRequest , IRequest<bool>
    {
     public string From { get; set; }
     public string To { get; set; }
        public long Price { get; set; }
        public long BedPrice { get; set; }
       public List<int> Rooms { get; set; }

    }
}
