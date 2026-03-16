using FluentValidation;
using MediatR;
using Pazario.Products.Application.Abstractions;
using Pazario.Products.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse>
                                    (IEnumerable<IValidator<TRequest>> validators)
                                     : IPipelineBehavior<TRequest, TResponse>
                                     where TRequest : IBaseRequest
                                     where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var errors = _validators
                            .Select(v => v.Validate(context))
                            .Where(validationResult => validationResult.Errors.Any())
                            .SelectMany(result => result.Errors)
                            .Select(validationError => new ValidationError(validationError.PropertyName, validationError.ErrorMessage))
                            .ToList();

            if (errors.Any())
                throw new Exceptions.ValidationException(errors);
            
            return await next();
        }
    }
}
