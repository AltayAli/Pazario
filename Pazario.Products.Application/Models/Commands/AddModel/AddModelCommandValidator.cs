using FluentValidation;
using Pazario.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Models.Commands.AddModel
{
    public class AddModelCommandValidator : AbstractValidator<AddModelCommand>
    {
        public AddModelCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(ModelErrors.NullValue.Code)
                .MaximumLength(100)
                .WithErrorCode(ModelErrors.MaxLenght.Code);

            RuleFor(x => x.MarkaId)
                .NotEmpty()
                .WithErrorCode(ModelErrors.NullValue.Code)
                .NotEqual(Guid.Empty)
                .WithErrorCode(ModelErrors.NullValue.Code);
        }
    }
}
