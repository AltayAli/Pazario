using FluentValidation;
using Pazario.Products.Domain.Markas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Markas.Commands.UpdateMarka
{
    public class UpdateMarkaCommandValidator : AbstractValidator<UpdateMarkaCommand>
    {
        public UpdateMarkaCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(MarkaErrors.NullValue.Code)
                .MaximumLength(100)
                .WithErrorCode(MarkaErrors.MaxLenght.Code);
        }
    }
}
