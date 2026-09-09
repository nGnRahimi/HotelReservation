using Domain.Models.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rooms.Command
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, bool>
    {

        private readonly IRoomRepository _roomRepository;
        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.FindAsync(request.Id);
            room.Enable = false;
            _roomRepository.Update(room);
            await _roomRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;

        }
    }
}
