using FluentValidation;
using Pazario.Products.Domain.Markas;
using System;
using System.Collections.Generic;
using System.Text;

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
