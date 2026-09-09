using Domain.Models.Rooms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rooms.Query
{
    internal class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Room>
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomByIdQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Room> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            return await _roomRepository.FindAsync(request.Id);
        }
    }
}
