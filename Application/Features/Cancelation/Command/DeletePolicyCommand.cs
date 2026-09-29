

using MediatR;

namespace Application.Features.Cancelation.Command
{
    public class DeletePolicyCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
