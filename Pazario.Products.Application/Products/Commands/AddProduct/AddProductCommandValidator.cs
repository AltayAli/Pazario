using FluentValidation;
using Pazario.Products.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Products.Commands.AddProduct
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(ProductErrors.NullValue.Code)
                .MaximumLength(100)
                .WithErrorCode(ProductErrors.MaxLenght.Code);

            RuleFor(x => x.Description)
                .MaximumLength(100)
                .WithErrorCode(ProductErrors.MaxLenght.Code);
        }
    }
}
