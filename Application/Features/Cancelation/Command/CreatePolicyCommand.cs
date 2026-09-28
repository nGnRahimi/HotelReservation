

using Application.Common.MediatR;
using MediatR;

namespace Application.Features.Cancelation.Command
{
    public class CreatePolicyCommand : BaseCommandRequest , IRequest<bool>
    {
        public string Title { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public List<RuleCreateDto> Rules { get; set; }
    }

    public class RuleCreateDto
    {
        public int Hours { get; set; }
        public int RefundPercent { get; set; }
    }











}
