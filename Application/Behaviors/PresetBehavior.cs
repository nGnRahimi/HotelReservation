using Application.Common.MediatR;
using Application.Services.CurrentUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    public class PresetBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> , IPresetModel
    {
     private readonly ICurrentUserService _currentUserService;
     public PresetBehavior(ICurrentUserService currentUserService)
     {

       _currentUserService = currentUserService;
     }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            request.UserId = _currentUserService.UserId;
            request.IsAdmin = _currentUserService.IsAdmin;
            request.HotelId = _currentUserService.HotelId;
            return await next();
        }
    }
}
