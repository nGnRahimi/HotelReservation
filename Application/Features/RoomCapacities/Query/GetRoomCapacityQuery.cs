using Application.Common.MediatR;
using Application.Features.RoomCapacities.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Features.RoomCapacities.Query
{
    public class GetRoomCapacityQuery : BaseQueryRequest, IRequest<RoomCapacityDto>
    {
        public DateTime startDate { get; set; }

        public List<int>? rooms { get; set; }
    }
}