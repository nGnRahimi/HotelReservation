

using Application.Common.MediatR;
using Application.Features.Cancelation.Dto;
using MediatR;

namespace Application.Features.Cancelation.Query
{
    public class GetCancelationQuery : BaseQueryRequest , IRequest<List<PolicyDto>>
    {

    }
}
