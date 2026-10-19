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
    internal class GetReserveMiniInfoQueryHandler : IRequestHandler<GetReserveMiniInfoQuery, MiniInfoDto>
    {
        private readonly IReserveRepository _reserveRepository;

        public GetReserveMiniInfoQueryHandler(IReserveRepository reserveRepository)
        {
            _reserveRepository = reserveRepository;
        }

        public async Task<MiniInfoDto> Handle(GetReserveMiniInfoQuery request, CancellationToken cancellationToken)
        {
            var data = new MiniInfoDto();

            data.Reserve = await _reserveRepository.Get(a => a.Enable && a.HotelId == request.HotelId
            && a.Status == Domain.Models.Reservation.Enum.ReserveStatus.Book).CountAsync();

            data.Cancel = await _reserveRepository.Get(a => a.Enable && a.HotelId == request.HotelId
            && a.Status == Domain.Models.Reservation.Enum.ReserveStatus.Cancel).CountAsync();

            data.Price = await _reserveRepository.Get(a => a.Enable && a.HotelId == request.HotelId
            && a.Status == Domain.Models.Reservation.Enum.ReserveStatus.Book).Select(a => a.TotalPrice).SumAsync();

            return data;
        }
    }
}
