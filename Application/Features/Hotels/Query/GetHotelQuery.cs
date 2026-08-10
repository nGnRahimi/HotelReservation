using Application.Common.MediatR;
using Domain.Models.Hotels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Query
{
    public class GetHotelQuery : BaseQueryRequest , IRequest<Hotel>
    {
    }
}
