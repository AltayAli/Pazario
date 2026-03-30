using FluentValidation;
using Pazario.Products.Domain.Models;

namespace Pazario.Products.Application.Products.Commands.RemoveProduct
{
    public class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
    {
        public RemoveProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithErrorCode(ModelErrors.NullValue.Code)
                .NotEqual(Guid.Empty)
                .WithErrorCode(ModelErrors.NullValue.Code);
        }
    }
}
