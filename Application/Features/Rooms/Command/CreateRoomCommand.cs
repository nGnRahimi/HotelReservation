using Application.Common.MediatR;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rooms
{
    public class CreateRoomCommand : BaseCommandRequest , IRequest<bool>
    {
        public string Name { get; set; }
        public int Qty { get; set; }
        public int MainCapacity { get; set; }
        public int ExtraCapacity { get; set; }
        public IFormFile Path { get; set; }
        public string? View { get; set; }
    }
}
