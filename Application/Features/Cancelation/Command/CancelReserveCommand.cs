using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cancelation.Command
{
    public class CancelReserveCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
