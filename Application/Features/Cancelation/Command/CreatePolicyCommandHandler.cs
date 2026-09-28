

using Application.Common.Extentions;
using Domain.Models.Cancelation;
using MediatR;

namespace Application.Features.Cancelation.Command
{
    public class CreatePolicyCommandHandler : IRequestHandler<CreatePolicyCommand, bool>
    {
        private readonly ICancelationRepository _cancelationRepository;

        public CreatePolicyCommandHandler(ICancelationRepository cancelationRepository)
        {
            _cancelationRepository = cancelationRepository;
        }

        public async Task<bool> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
        {
            var from = request.From.ToMiladiDateTime();
            var to = request.To.ToMiladiDateTime();

            var policy = new CancelationPolicy()
            {
                HotelId = request.HotelId.Value,
                From = from,
                To = to,
                Title = request.Title,
            };

            _cancelationRepository.Add(policy);
           

            foreach(var item in request.Rules)
            {
                var rule = new CancelationRule()
                {
                   
                    Hours = item.Hours,
                    RefundPercent = item.RefundPercent,
                };

                policy.AddCancelationRules(rule);
            }
          await _cancelationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
