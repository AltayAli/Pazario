using FluentValidation;
using Pazario.Products.Domain.CategoryPropertyValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.CategoryPropertyValues.Commands
{
    public class AddCategoryPropertyValueCommandValidator : AbstractValidator<AddCategoryPropertyValueCommand>
    {
        public AddCategoryPropertyValueCommandValidator()
        {
            RuleForEach(x => x.Items)
                .NotEmpty()
                .WithErrorCode(CategoryPropertyValueErrors.NullValue.Code)
                .MaximumLength(50)
                .WithErrorCode(CategoryPropertyValueErrors.MaxLenght.Code);
        }
    }
}
