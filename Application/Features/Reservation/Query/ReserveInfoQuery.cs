using Application.Features.Reservation.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservation.Query
{
    public class ReserveInfoQuery : IRequest<ReserveInfoDto>
    {
        public int Id { get; set; }
    }
}
