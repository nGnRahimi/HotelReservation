using Application.Features.Cancelation.Dto;
using Domain.Models.Cancelation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cancelation.Query
{
    internal class GetReserveCancelQueryHandler : IRequestHandler<GetReserveCancelQuery, List<RuleDto>>
    {
        private readonly ICancelationRepository _cancelationRepository;

        public GetReserveCancelQueryHandler(ICancelationRepository cancelationRepository)
        {
            _cancelationRepository = cancelationRepository;
        }

        public async Task<List<RuleDto>> Handle(GetReserveCancelQuery request, CancellationToken cancellationToken)
        {
            DateTime date = DateTime.Now;

            var cancel = await _cancelationRepository.Get(a => a.Enable && a.HotelId == request.HotelID
            /*&& a.From >= date && a.To <= date*/)
                .Include(a => a.CancelationRules)
                .OrderByDescending(a => a.Created)
                .FirstOrDefaultAsync();


            var data = cancel?.CancelationRules.Select(s => new RuleDto()
            {
                Hours = s.Hours,
                RefundPercent = s.RefundPercent
            }).ToList();

            return data;


        }
    }
}
