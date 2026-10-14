using Application.Features.Reservation.Dto;
using Domain.Models.Reservation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservation.Query
{
    public class ReserveInfoQueryHandler : IRequestHandler<ReserveInfoQuery, ReserveInfoDto>
    {
        private readonly IReserveRepository _reserveRepository;

        public ReserveInfoQueryHandler(IReserveRepository reserveRepository)
        {
            _reserveRepository = reserveRepository;
        }

        public async Task<ReserveInfoDto> Handle(ReserveInfoQuery request, CancellationToken cancellationToken)
        {
            var data = await _reserveRepository.Get(a => a.Enable && a.Id == request.Id)
                .Select(s => new ReserveInfoDto()
                {
                    Id = s.Id,
                    From = s.From,
                    To = s.To,
                    HotelId = s.HotelId,
                    HotelName = s.Hotel.Name,
                    CancelAmount = s.CancelAmount,
                    TotalPrice = s.TotalPrice,
                    Status = s.Status,
                    IsCanceled = s.IsCanceled,
                    ReserveCreate = s.ReserveCreate,
                    Rooms = s.ReserveDetails.Select(r => new ReserveInfoRoom()
                    {
                        BedPrice = r.BedPrice,
                        Price = r.Price,
                        NumberOfExtraBed = r.NumberOfExtraBed,
                        RoomId = r.RoomId,
                        RoomName = r.Room.Name,
                        Guests = r.ReserveGuests.Select(g => new ReserveInfoGuest()
                        {
                            FName = g.FName,
                            LName = g.LName,
                            NationalCode = g.NationalCode
                        }).ToList(),
                        Dailies = r.ReserveDailyDetails.Select(d => new ReserveInfoDaily()
                        {
                            BedPrice = d.BedPrice,
                            Price = d.Price,
                            DateVal = d.DateVal
                        }).ToList()
                    }).ToList(),
                }).FirstOrDefaultAsync();

            return data;
        }
    }
}
