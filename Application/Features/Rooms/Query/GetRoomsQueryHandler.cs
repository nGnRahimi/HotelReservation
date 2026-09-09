using Application.Common.Extentions;
using Application.Common.Pagination;
using Domain.Models.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rooms.Query
{
    public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, PaginatedList<Room>>
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomsQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<PaginatedList<Room>> Handle(
            GetRoomsQuery request,
            CancellationToken cancellationToken)
        {
            var data = await _roomRepository.Get(a =>
                    a.Enable &&
                    a.HotelId == request.HotelId.Value)
                .AsNoTracking()
                .SearchQuery(request.Search)
                .PaginatedListAsync(
                    request.PageNumber,
                    request.PageSize,
                    request.DisablePaging);
            
            return data;
        }
    }
}