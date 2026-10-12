using Application.Common.Exceptions;
using Application.Common.Extentions;
using Application.Features.Reservation.Dto;
using Domain.Models.Capacities;
using Domain.Models.Prices;
using Domain.Models.Reservation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservation.Command
{
    internal class ReserveCommandHandler : IRequestHandler<ReserveCommand, int>
    {
        private readonly IRoomCapacityRepository _roomCapacityRepository;
        private readonly IRoomPriceRepository _roomPriceRepository;
        private readonly IReserveRepository _reserveRepository;
        public ReserveCommandHandler(IRoomCapacityRepository roomCapacityRepository, IRoomPriceRepository roomPriceRepository, IReserveRepository reserveRepository)
        {
            _roomCapacityRepository = roomCapacityRepository;
            _roomPriceRepository = roomPriceRepository;
            _reserveRepository = reserveRepository;
        }

        public async Task<int> Handle(ReserveCommand request, CancellationToken cancellationToken)
        {
            var from = request.From.ToMiladiDateTime();
            var to = request.To.ToMiladiDateTime();

            if (request.UserId == null)
            {
                throw new CustomException("لطفا وارد سایت شوید");
            }


            var CapRes = await CheckCapacity(from, to, request);
            if (!CapRes)
            {
                throw new CustomException("عدم وجود ظرفیت");
            }


            var reserve = await CreateReserve(from, to, request);
            _reserveRepository.Add(reserve);
            await _reserveRepository.UnitOfWork.SaveEntitiesAsync();

            return reserve.Id;
        }



        public async Task<Reserve> CreateReserve(DateTime from, DateTime to, ReserveCommand request)
        {
            var reserve = new Reserve()
            {
                From = from,
                To = to,
                HotelId = request.ToHotelId,
                IsCanceled = false,
                ReserveCreate = DateTime.Now,
                Status = Domain.Models.Reservation.Enum.ReserveStatus.Free,
                UserId = request.UserId.Value,
                CancelAmount = 0,
            };

            List<ReserveDetail> reserveDts = new();
            foreach (var item in request.Rooms)
            {
                var pirces = await GetPrice(from, to, item.RoomId, item.NumberOfExtrabed);
                var reserveDt = new ReserveDetail()
                {
                    RoomId = item.RoomId,
                    BedPrice = pirces.BedPrice,
                    Price = pirces.Price,
                    NumberOfExtraBed = item.NumberOfExtrabed,
                    ReserveDailyDetails = pirces.Details.Select(s => new ReserveDailyDetail()
                    {
                        Price = s.Price,
                        BedPrice = s.BedPrice,
                        DateVal = s.DateVal,
                    }).ToList(),
                    ReserveGuests = item.Guests.Select(g => new ReserveGuest()
                    {
                        FName = g.Fname,
                        LName = g.Lname,
                        NationalCode = g.Code
                    }).ToList(),
                };

                reserveDts.Add(reserveDt);
            }

            reserve.TotalPrice = reserveDts.Sum(a => a.Price) + reserveDts.Sum(a => a.BedPrice);
            return reserve;
        }


        public async Task<bool> CheckCapacity(DateTime from, DateTime to, ReserveCommand request)
        {

            int days = (to - from).Days;


            var capacities = await _roomCapacityRepository.Get(a => a.Enable
            && a.Room.HotelId == request.ToHotelId && request.Rooms.Select(a => a.RoomId).Contains(a.RoomId)
            && a.DateVal.Date >= from && a.DateVal.Date < to).ToListAsync();


            foreach (var item in request.Rooms)
            {
                for (int i = 0; i < days; i++)
                {
                    var date = from.AddDays(i);

                    var capacity = capacities.Where(a => a.DateVal.Date == date.Date && a.RoomId == item.RoomId)
                        .FirstOrDefault();

                    if (capacities == null)
                    {
                        return false;
                    }

                    if (capacity.Qty == 0 || !capacity.State)
                    {
                        return false;
                    }


                    capacity.Qty = capacity.Qty - 1;
                    _roomCapacityRepository.Update(capacity);
                }
            }


            return true;
        }




        public async Task<ReservePriceDto> GetPrice(DateTime from, DateTime to, int roomId, int extraBed)
        {
            int days = (to - from).Days;


            var prices = await _roomPriceRepository.Get(a => a.Enable
            && a.RoomId == roomId
            && a.DateVal.Date >= from && a.DateVal.Date < to).ToListAsync();


            var data = new ReservePriceDto();
            for (int i = 0; i < days; i++)
            {
                var date = from.AddDays(i);
                var price = prices.Where(a => a.DateVal.Date == date.Date).FirstOrDefault();
                var daily = new ReserveDailyPriceDto()
                {
                    DateVal = date,
                    BedPrice = price.BedPrice * extraBed,
                    Price = price.Price
                };

                data.Details.Add(daily);
            }


            data.Price = data.Details.Sum(a => a.Price);
            data.BedPrice = data.Details.Sum(a => a.BedPrice);
            return data;
        }
    }
}
