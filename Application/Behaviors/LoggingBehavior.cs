using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //pre
            Console.WriteLine($"Handling {typeof(TRequest).Name}");
            var response = await next();

            //post
            Console.WriteLine($"Handled {typeof(TRequest).Name}");
            return response;
        }
    }
}
