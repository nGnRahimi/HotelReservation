

using Application.Common.Exceptions;
using Domain.Models.Prices;
using Domain.Models.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoomPrices.Command
{
    public class SaveCalenderCommandHandler : IRequestHandler<SaveCalenderCommand, bool>
    {
        private readonly IRoomPriceRepository _roomPrice;

        public SaveCalenderCommandHandler(IRoomPriceRepository roomPrice)
        {
            _roomPrice = roomPrice;
        }

        public async Task<bool> Handle(SaveCalenderCommand request, CancellationToken cancellationToken)
        {
           foreach(var item in request.PriceLists)
           {
                if (item.Price == 0)
                    throw new CustomException("قیمت نمیتواند صفر باشد");


                var price = await _roomPrice.Get(a => a.DateVal.Date == item.DateVal.Date && a.RoomId == item.RoomId)
                 .FirstOrDefaultAsync();

                if(price == null)
                {
                    var Newprice = new RoomPrice()
                    {
                        RoomId = item.RoomId,
                        DateVal = item.DateVal,
                        BedPrice = item.BedPrice,
                        Price = item.Price,
                    };
                    _roomPrice.Add(Newprice);
                }
                else
                {
                    price.Price = item.Price;
                    price.BedPrice = item.BedPrice;


                    _roomPrice.Update(price);

                }

                var history = new RoomPriceHistory(item.RoomId, item.Price, item.BedPrice, item.DateVal, null);
                _roomPrice.AddHistory(history);


           }

            await _roomPrice.UnitOfWork.SaveEntitiesAsync();
            return true;
        }
    }
}
