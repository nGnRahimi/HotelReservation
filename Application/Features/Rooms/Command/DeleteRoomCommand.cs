using Application.Common.MediatR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rooms.Command
{
    public class DeleteRoomCommand : BaseCommandRequest , IRequest<bool>
    {
     public int Id { get; set; }
    }
}
