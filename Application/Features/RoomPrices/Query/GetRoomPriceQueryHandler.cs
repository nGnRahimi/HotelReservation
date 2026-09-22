using Application.Common.Extentions;
using Application.Features.RoomPrices.Dto;
using Domain.Models.Prices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoomPrices.Query
{
    public class GetRoomPriceQueryHandler : IRequestHandler<GetRoomPriceQuery, RoomPriceDto>
    {
        private readonly IRoomPriceRepository _roomPrice;

        public GetRoomPriceQueryHandler(IRoomPriceRepository roomPrice)
        {
            _roomPrice = roomPrice;
        }
        
        public async Task<RoomPriceDto> Handle(GetRoomPriceQuery request, CancellationToken cancellationToken)
        {
            var prices = await _roomPrice.Get(a => a.Room.HotelId == request.HotelId && a.Room.Enable
             &&(request.rooms != null ? request.rooms.Contains(a.RoomId) : true))
                .Include(a => a.Room)
                .GroupBy(r => new { r.RoomId , r.Room.Name})
                .Select(s => new RoomPricesName()
                {
                    RoomId = s.Key.RoomId,
                    Name = s.Key.Name,
                    RoomPrices = s.Select(x => new RoomPriceList()
                    { 
                    BedPrice = x.BedPrice,
                    DateVal = x.DateVal,
                    Price = x.Price,
                    
                    }).ToList(),
                }).ToListAsync();



            var data = new RoomPriceDto()
            {
                Rooms = prices,
                Days = DateTimeExtention.GetNextSevenDays(request.startDate),
            };

            return data;



        }
    }
}
