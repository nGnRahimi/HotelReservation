using Application.Common.Extenstios;
using Application.Common.Extentions;
using Application.Features.RoomPrices.Dto;
using Domain.Models.Prices;
using Domain.Models.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoomPrices.Query
{
    public class GetRoomPriceQueryHandler : IRequestHandler<GetRoomPriceQuery, RoomPriceDto>
    {
        private readonly IRoomPriceRepository _roomPrice;
        private readonly IRoomRepository _roomRepository;
        public GetRoomPriceQueryHandler(IRoomPriceRepository roomPrice, IRoomRepository roomRepository)
        {
            _roomPrice = roomPrice;
            _roomRepository = roomRepository;
        }

        public async Task<RoomPriceDto> Handle(GetRoomPriceQuery request, CancellationToken cancellationToken)
        {
            //var prices = await _roomPrice.Get(a => a.Room.HotelId == request.HotelId && a.Room.Enable
            //      && (request.rooms != null ? request.rooms.Contains(a.RoomId) : true))
            //    .Include(a => a.Room)
            //    .GroupBy(r => new { r.RoomId, r.Room.Name })
            //    .Select(s => new RoomPricesName()
            //    {
            //        RoomId = s.Key.RoomId,
            //        Name = s.Key.Name,
            //        RoomPrices = s.Select(x => new RoomPriceList() 
            //        {
            //            BedPrice = x.BedPrice,
            //            DateVal = x.DateVal,
            //            Price = x.Price,
            //        }).ToList(),
            //    }).ToListAsync();

            var rooms = await _roomRepository.Get(a => a.HotelId == request.HotelId && a.Enable
                  && (request.rooms != null ? request.rooms.Contains(a.Id) : true))
                .AsNoTracking().ToListAsync();

            var pricess = await _roomPrice.Get(a => a.Room.HotelId == request.HotelId && a.Room.Enable
                  && (request.rooms != null ? request.rooms.Contains(a.RoomId) : true)
                  && a.DateVal.Date >= request.startDate.Date && a.DateVal.Date <= request.startDate.AddDays(7))
                .AsNoTracking().ToListAsync();


            var PriceDate = rooms.Select(s => new RoomPricesName()
            {
                RoomId = s.Id,
                Name = s.Name,
                RoomPrices = pricess.Where(a => a.RoomId == s.Id).Select(x => new RoomPriceList()
                {
                    BedPrice = x.BedPrice,
                    DateVal = x.DateVal,
                    Price = x.Price,
                }).ToList(),
            }).ToList();



            var data = new RoomPriceDto()
            {
                Rooms = PriceDate,
                Days = DateTimeExtenstion.GetNextSevenDays(request.startDate),
            };


            return data;
        }
    }
}
