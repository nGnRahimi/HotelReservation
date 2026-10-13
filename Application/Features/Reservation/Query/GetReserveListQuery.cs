using Application.Common.MediatR;
using Application.Common.Pagination;
using Application.Features.Reservation.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservation.Query
{
    public class GetReserveListQuery : BaseQueryRequest, IRequest<PaginatedList<ReserveListDto>>
    {
        public string from { get; set; }
        public string to { get; set; }
    }
}
