using Application.Common.Extentions;
using Application.Features.RoomCapacities.Dto;
using Domain.Models.Capacities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoomCapacities.Query
{
    public class GetRoomCapacityQueryHandler
        : IRequestHandler<GetRoomCapacityQuery, RoomCapacityDto>
    {
        private readonly IRoomCapacityRepository _roomCapacity;

        public GetRoomCapacityQueryHandler(IRoomCapacityRepository roomCapacity)
        {
            _roomCapacity = roomCapacity;
        }

        public async Task<RoomCapacityDto> Handle(
            GetRoomCapacityQuery request,
            CancellationToken cancellationToken)
        {
            var capacities = await _roomCapacity.Get(a =>
                    a.Room.HotelId == request.HotelId &&
                    a.Room.Enable &&
                    (request.rooms != null
                        ? request.rooms.Contains(a.RoomId)
                        : true))
                .Include(a => a.Room)
                .GroupBy(r => new
                {
                    r.RoomId,
                    r.Room.Name
                })
                .Select(s => new RoomCapacityName()
                {
                    RoomId = s.Key.RoomId,
                    Name = s.Key.Name,

                    RoomCapacities = s.Select(x => new RoomCapacityList()
                    {
                        Qty = x.Qty,
                        DateVal = x.DateVal,
                        State = x.State

                    }).ToList(),

                })
                .ToListAsync(cancellationToken);


            var data = new RoomCapacityDto()
            {
                Rooms = capacities,
                Days = DateTimeExtention.GetNextSevenDays(request.startDate),
            };

            return data;
        }
    }
}