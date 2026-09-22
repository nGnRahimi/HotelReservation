using Application.Common.Exceptions;
using Application.Common.Extentions;
using Domain.Models.Prices;
using Domain.Models.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoomPrices
{
    public class CreateRoomPriceCommandHandler : IRequestHandler<CreateRoomPriceCommand, bool>
    {

        private readonly IRoomPriceRepository _roomPrice;
        private readonly IRoomRepository _roomRepository;

      
        public CreateRoomPriceCommandHandler(IRoomPriceRepository roomPrice , IRoomRepository roomRepository)
        {
            _roomPrice = roomPrice;
            _roomRepository = roomRepository;
        }
        
        public async Task<bool> Handle(CreateRoomPriceCommand request, CancellationToken cancellationToken)
        {
            var from = request.From.ToMiladiDateTime();
            var to = request.To.ToMiladiDateTime();

            int days = (to - from).Days;
            var roomPrices = await _roomPrice.Get(a => request.Rooms.Contains(a.RoomId) && (a.DateVal >= from && a.DateVal < to))
                    .ToListAsync();



            foreach (var roomId in request.Rooms)
            {
                var room = await _roomRepository.FindAsync(roomId);
                if (room == null)
                    continue;

                if(room.HotelId != request.HotelId.Value)
                    throw new CustomException("خطا");




                for (int i = 0; i < days; i++)
                { 
                    DateTime date = from.AddDays(i);
                    var roomPrice = roomPrices.Where(a => a.RoomId == roomId && a.DateVal.Date == date.Date)
                    .FirstOrDefault();


                    if(roomPrice == null)
                    {
                        var newRoomPrice = new RoomPrice()
                        {
                            RoomId = roomId,
                            Price = request.Price,
                            BedPrice = request.BedPrice,
                            DateVal = date
                        };

                        _roomPrice.Add(newRoomPrice);

                    }

                    else
                    {
                        roomPrice.Price = request.Price;
                        roomPrice.BedPrice = request.BedPrice;


                        _roomPrice.Update(roomPrice);
                    }

                }


                var history = new RoomPriceHistory(roomId , request.Price , request.BedPrice , from , to );
                _roomPrice.AddHistory(history);



            }
            await _roomPrice.UnitOfWork.SaveEntitiesAsync();

            return true;
        }
    }
}
