using Application.Common.MediatR;
using Application.Features.RoomPrices.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoomPrices.Query
{
    public class GetRoomPriceQuery : BaseQueryRequest , IRequest<RoomPriceDto>
    {
        public DateTime startDate { get; set; } 
    }
}
