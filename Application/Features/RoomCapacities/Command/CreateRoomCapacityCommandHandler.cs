using Application.Common.Exceptions;
using Application.Common.Extentions;
using Application.Features.RoomCapacites.Command;
using Domain.Models.Capacities;
using Domain.Models.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoomCapacities
{
    public class CreateRoomCapacityCommandHandler
        : IRequestHandler<CreateRoomCapacityCommand, bool>
    {
        private readonly IRoomCapacityRepository _roomCapacity;
        private readonly IRoomRepository _roomRepository;

        public CreateRoomCapacityCommandHandler(
            IRoomCapacityRepository roomCapacity,
            IRoomRepository roomRepository)
        {
            _roomCapacity = roomCapacity;
            _roomRepository = roomRepository;
        }

        public async Task<bool> Handle(
            CreateRoomCapacityCommand request,
            CancellationToken cancellationToken)
        {
            var from = request.From.ToMiladiDateTime();
            var to = request.To.ToMiladiDateTime();

            int days = (to - from).Days;

            var roomCapacities = await _roomCapacity
                .Get(a =>
                    request.Rooms.Contains(a.RoomId) &&
                    (a.DateVal >= from && a.DateVal < to))
                .ToListAsync();

            foreach (var roomId in request.Rooms)
            {
                var room = await _roomRepository.FindAsync(roomId);

                if (room == null)
                    continue;

                if (room.HotelId != request.HotelId.Value)
                    throw new CustomException("خطا");

                for (int i = 0; i < days; i++)
                {
                    DateTime date = from.AddDays(i);

                    var roomCapacity = roomCapacities
                        .Where(a =>
                            a.RoomId == roomId &&
                            a.DateVal.Date == date.Date)
                        .FirstOrDefault();

                    if (roomCapacity == null)
                    {
                        var newRoomCapacity = new RoomCapacity()
                        {
                            RoomId = roomId,
                            Qty = request.Qty,
                            DateVal = date
                        };

                        _roomCapacity.Add(newRoomCapacity);
                    }
                    else
                    {
                        roomCapacity.Qty = request.Qty;

                        _roomCapacity.Update(roomCapacity);
                    }
                }

                var history = new RoomCapacityHistory(
                    roomId,
                    request.Qty,
                    from,
                    to);

                _roomCapacity.AddHistory(history);
            }

            await _roomCapacity.UnitOfWork.SaveEntitiesAsync();

            return true;
        }
    }
}