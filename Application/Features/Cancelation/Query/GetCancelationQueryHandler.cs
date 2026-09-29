
using Application.Features.Cancelation.Dto;
using Domain.Models.Cancelation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Cancelation.Query
{
    public class GetCancelationQueryHandler : IRequestHandler<GetCancelationQuery, List<PolicyDto>>
    {
        private readonly ICancelationRepository _cancelationRepository;

        public GetCancelationQueryHandler(ICancelationRepository cancelationRepository)
        {
            _cancelationRepository = cancelationRepository;
        }

        public async Task<List<PolicyDto>> Handle(GetCancelationQuery request, CancellationToken cancellationToken)
        {
            var data = await _cancelationRepository.Get(a => a.Enable && a.HotelId == request.HotelId.Value)
                .Include(a => a.CancelationRules)
                .Select(s => new PolicyDto()
                {
                    Id = s.Id,
                    From = s.From,
                    To = s.To,
                    Title = s.Title,
                    Rules = s.CancelationRules.Select(r => new RuleDto()
                    {
                        Hours = r.Hours,
                        RefundPercent = r.RefundPercent
                    }).ToList()
                }).ToListAsync();

            return data;
        }
    }
}
