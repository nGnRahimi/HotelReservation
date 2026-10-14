using Application.Features.Cancelation.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cancelation.Query
{
    public class GetReserveCancelQuery : IRequest<List<RuleDto>>
    {
        public int HotelID { get; set; }
    }
}
