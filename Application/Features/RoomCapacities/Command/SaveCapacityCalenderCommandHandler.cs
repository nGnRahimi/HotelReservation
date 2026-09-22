using Application.Common.Exceptions;
using Domain.Models.Capacities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoomCapacites.Command
{
    public class SaveCapacityCalenderCommandHandler
        : IRequestHandler<SaveCapacityCalenderCommand, bool>
    {
        private readonly IRoomCapacityRepository _roomCapacity;

        public SaveCapacityCalenderCommandHandler(
            IRoomCapacityRepository roomCapacity)
        {
            _roomCapacity = roomCapacity;
        }

        public async Task<bool> Handle(
            SaveCapacityCalenderCommand request,
            CancellationToken cancellationToken)
        {
            foreach (var item in request.CapacityLists)
            {
                if (item.Qty == 0)
                    throw new CustomException("ظرفیت نمیتواند صفر باشد");

                var capacity = await _roomCapacity
                    .Get(a =>
                        a.DateVal.Date == item.DateVal.Date &&
                        a.RoomId == item.RoomId)
                    .FirstOrDefaultAsync();

                if (capacity == null)
                {
                    var newCapacity = new RoomCapacity()
                    {
                        RoomId = item.RoomId,
                        DateVal = item.DateVal,
                        Qty = item.Qty
                    };

                    _roomCapacity.Add(newCapacity);
                }
                else
                {
                    capacity.Qty = item.Qty;

                    _roomCapacity.Update(capacity);
                }

                var history = new RoomCapacityHistory(
                    item.RoomId,
                    item.Qty,
                    item.DateVal,
                    null);

                _roomCapacity.AddHistory(history);
            }

            await _roomCapacity.UnitOfWork.SaveEntitiesAsync();

            return true;
        }
    }
}