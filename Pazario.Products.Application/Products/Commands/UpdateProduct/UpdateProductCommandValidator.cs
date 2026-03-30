using FluentValidation;
using Pazario.Products.Domain.Models;
using Pazario.Products.Domain.Products;

namespace Pazario.Products.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithErrorCode(ModelErrors.NullValue.Code)
                .NotEqual(Guid.Empty)
                .WithErrorCode(ModelErrors.NullValue.Code);

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
