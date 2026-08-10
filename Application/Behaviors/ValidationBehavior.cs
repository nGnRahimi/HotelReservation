using Application.Common.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {

            _validators = validators;


        }

        public  async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
                .Where(f => f != null) //برو اونایی که نال نیستنو بیار
                .Select(s => s.ErrorMessage)
                .ToList();

            if (failures.Any())
            {

                throw new CustomException(String.Join(",", failures));
                
            }
            return await next();
        }
    }
}
