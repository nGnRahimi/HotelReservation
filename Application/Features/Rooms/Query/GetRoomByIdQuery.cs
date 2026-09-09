using Application.Common.MediatR;
using Domain.Models.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rooms.Query
{
    public class GetRoomByIdQuery : BaseQueryRequest , IRequest<Room>
    {
        public int Id { get; set; }
    }
}
