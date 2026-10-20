using Application.Common.Extenstios;
using Application.Common.Extentions;
using Application.Features.RoomCapacities.Dto;
using Application.Features.RoomPrices.Dto;
using Domain.Models.Capacities;
using Domain.Models.Prices;
using Domain.Models.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoomCapacities.Query
{
    public class GetRoomCapacityQueryHandler : IRequestHandler<GetRoomCapacityQuery, RoomCapacityDto>
    {
        private readonly IRoomCapacityRepository _roomCapacityRepository;
        private readonly IRoomRepository _roomRepository;
        public GetRoomCapacityQueryHandler(IRoomCapacityRepository roomCapacityRepository, IRoomRepository roomRepository)
        {
            _roomCapacityRepository = roomCapacityRepository;
            _roomRepository = roomRepository;
        }

        public async Task<RoomCapacityDto> Handle(GetRoomCapacityQuery request, CancellationToken cancellationToken)
        {
            //var cap = await _roomCapacityRepository.Get(a => a.Room.HotelId == request.HotelId && a.Room.Enable
            //      && (request.rooms != null ? request.rooms.Contains(a.RoomId) : true))
            //    .Include(a => a.Room)
            //    .GroupBy(r => new { r.RoomId, r.Room.Name })
            //    .Select(s => new RoomCapacityName()
            //    {
            //        RoomId = s.Key.RoomId,
            //        Name = s.Key.Name,
            //        RoomCapacities = s.Select(x => new RoomCapacityList()
            //        {
            //            Qty = x.Qty,
            //            DateVal = x.DateVal,
            //            State = x.State,
            //        }).ToList(),
            //    }).ToListAsync();

            var rooms = await _roomRepository.Get(a => a.HotelId == request.HotelId && a.Enable
                  && (request.rooms != null ? request.rooms.Contains(a.Id) : true))
                .AsNoTracking().ToListAsync();

            var capacities = await _roomCapacityRepository.Get(a => a.Room.HotelId == request.HotelId && a.Room.Enable
                  && (request.rooms != null ? request.rooms.Contains(a.RoomId) : true)
                  && a.DateVal.Date >= request.startDate.Date && a.DateVal.Date <= request.startDate.AddDays(7))
                .AsNoTracking().ToListAsync();

            var capData = rooms.Select(s => new RoomCapacityName()
            {
                RoomId = s.Id,
                Name = s.Name,
                RoomCapacities = capacities.Where(a => a.RoomId == s.Id).Select(x => new RoomCapacityList()
                {
                    Qty = x.Qty,
                    DateVal = x.DateVal,
                    State = x.State,
                }).ToList(),
            }).ToList();


            var data = new RoomCapacityDto()
            {
                Rooms = capData,
                Days = DateTimeExtenstion.GetNextSevenDays(request.startDate),
            };


            return data;
        }
    }
}
