

using Application.Common.Exceptions;
using Domain.Models.Cancelation;
using MediatR;

namespace Application.Features.Cancelation.Command
{
    public class DeletePolicyCommandHandler : IRequestHandler<DeletePolicyCommand, bool>
    {
        private readonly ICancelationRepository _cancelationRepository;

        public DeletePolicyCommandHandler(ICancelationRepository cancelationRepository)
        {
            _cancelationRepository = cancelationRepository;
        }
        public async Task<bool> Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
        {
           var policy = await _cancelationRepository.FindAsync(request.Id);
            if (policy == null)
            {
                throw new CustomException("خطا");

            }

            policy.Enable = false;
            _cancelationRepository.Update(policy);

            await _cancelationRepository.UnitOfWork.SaveEntitiesAsync();
            return true;

        }
    }
}
