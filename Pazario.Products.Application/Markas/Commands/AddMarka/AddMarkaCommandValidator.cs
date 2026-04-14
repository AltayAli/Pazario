using FluentValidation;
using Pazario.Products.Domain.Markas;

namespace Pazario.Products.Application.Markas.Commands.AddMarka
{
    public class AddMarkaCommandValidator : AbstractValidator<AddMarkaCommand>
    {
        public AddMarkaCommandValidator() 
        { 
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(MarkaErrors.NullValue.Code)
                .MaximumLength(100)
                .WithErrorCode(MarkaErrors.MaxLenght.Code);
        }
    }
}
