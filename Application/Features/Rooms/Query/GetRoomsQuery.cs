using Application.Common.MediatR;
using Application.Common.Pagination;
using Domain.Models.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rooms.Query
{
    public class GetRoomsQuery : BaseQueryRequest , IRequest<PaginatedList<Room>>
    {
    }
}
