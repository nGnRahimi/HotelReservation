using Application.Common.Extentions;
using Application.Features.Search.Dto;
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

namespace Application.Features.Search.Query
{
    public class SelectedRoomQueryHandler : IRequestHandler<SelectedRoomQuery, List<SearchRoomDto>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomPriceRepository _priceRepository;
        private readonly IRoomCapacityRepository _capacityRepository;

        public SelectedRoomQueryHandler(IRoomCapacityRepository capacityRepository, IRoomPriceRepository priceRepository, IRoomRepository roomRepository)
        {
            _capacityRepository = capacityRepository;
            _priceRepository = priceRepository;
            _roomRepository = roomRepository;
        }
        public async Task<List<SearchRoomDto>> Handle(SelectedRoomQuery request, CancellationToken cancellationToken)
        {
            var from = request.From.ToMiladiDateTime();
            var to = request.To.ToMiladiDateTime();

            int days = (to - from).Days;

            var rooms = await _roomRepository.Get(a => a.Enable && a.HotelId == request.HotelId)
                .Include(a => a.Hotel)
                .AsNoTracking().ToListAsync();


            var prices = await _priceRepository.Get(a => a.Room.HotelId == request.HotelId && a.Enable
            && (a.DateVal.Date >= from && a.DateVal.Date < to))
                .Include(a => a.Room).AsNoTracking().ToListAsync();


            var capacities = await _capacityRepository.Get(a => a.Room.HotelId == request.HotelId && a.Enable
            && (a.DateVal.Date >= from && a.DateVal.Date < to))
                .Include(a => a.Room).AsNoTracking().ToListAsync();



            var data = new List<SearchRoomDto>();

            foreach (var item in request.Rooms)
            {

                if (item.Qty > 0)
                {
                    for (int j = 0; j < item.Qty; j++)
                    {
                        var room = rooms.Where(a => a.Id == item.RoomId).FirstOrDefault();
                        var roomDt = new SearchRoomDto()
                        {
                           HotelName = room.Hotel.Name,
                            RoomId = room.Id,
                            Img = room.Path,
                            RoomName = room.Name + " " + room.View,
                            Extra = room.ExtraCapacity,
                        };

                        bool CheckPrice = false;

                        for (int i = 0; i < days; i++)
                        {
                            DateTime date = from.AddDays(i);
                            var price = prices.Where(a => a.RoomId == room.Id && a.DateVal.Date == date.Date).FirstOrDefault();
                            if (price == null)
                            {
                                CheckPrice = true;
                                break;
                            }

                            var daily = new DailySearchRoom()
                            {
                                DateVal = date,
                                BedPrice = price.BedPrice,
                                Price = price.Price,
                            };

                            var capacity = capacities.Where(a => a.RoomId == room.Id && a.DateVal.Date == date.Date).FirstOrDefault();
                            if (capacity == null)
                            {
                                daily.Qty = 0;
                                daily.State = false;
                            }
                            else
                            {
                                daily.Qty = capacity.Qty;
                                daily.State = capacity.State;
                            }


                            roomDt.Details.Add(daily);
                        }

                        if (CheckPrice)
                        {
                            continue;
                        }

                        roomDt.Price = roomDt.Details.Sum(a => a.Price);
                        roomDt.Qty = roomDt.Details.Min(a => a.Qty);

                        data.Add(roomDt);
                    }
                }





            }


            return data;
        }
    }
}
