using Application.Common.Extenstios;
using Application.Common.Extentions;
using Application.Common.Pagination;
using Application.Features.Reservation.Dto;
using Domain.Models.Reservation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reservation.Query
{
    internal class GetReserveListQueryHandler : IRequestHandler<GetReserveListQuery, PaginatedList<ReserveListDto>>
    {
        private readonly IReserveRepository _reserveRepository;

        public GetReserveListQueryHandler(IReserveRepository reserveRepository)
        {
            _reserveRepository = reserveRepository;
        }

        public async Task<PaginatedList<ReserveListDto>> Handle(GetReserveListQuery request, CancellationToken cancellationToken)
        {
            DateTime? from = null;
            DateTime? to = null;

            if (!String.IsNullOrEmpty(request.from) && !String.IsNullOrEmpty(request.to))
            {
                from = request.from.ToMiladiDateTime();
                to = request.to.ToMiladiDateTime();
            }
            var data = await _reserveRepository.Get(a => a.Enable &&
                 (request.HotelId != null ? request.HotelId == a.HotelId : a.UserId == request.UserId)
                 && (from != null && to != null ? (a.ReserveCreate >= from && a.ReserveCreate <= to) : true))
                .SearchQuery(request.Search)
                .OrderByDescending(a => a.ReserveCreate)
                .Select(s => new ReserveListDto()
                {
                    Id = s.Id,
                    From = s.From,
                    To = s.To,
                    HotelId = s.HotelId,
                    HotelName = s.Hotel.Name,
                    IsCanceled = s.IsCanceled,
                    CancelAmount = s.CancelAmount,
                    Status = s.Status,
                    ReserveCreate = s.ReserveCreate,
                    TotalPrice = s.TotalPrice,
                    Rooms = String.Join(",", s.ReserveDetails.Select(a => a.Room.Name).ToList()),
                    GuestName = s.ReserveDetails.FirstOrDefault().ReserveGuests.Select(a => a.FName + " " + a.LName).FirstOrDefault()

                }).PaginatedListAsync(request.PageNumber, request.PageSize, request.DisablePaging);

            return data;
        }
    }
}
