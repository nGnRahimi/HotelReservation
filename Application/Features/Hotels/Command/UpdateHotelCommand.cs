using Application.Common.MediatR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Command
{
    public class UpdateHotelCommand : BaseCommandRequest ,IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public int Star { get; set; }
        public bool State { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
