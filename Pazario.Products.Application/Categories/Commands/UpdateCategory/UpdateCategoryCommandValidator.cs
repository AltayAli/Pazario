using FluentValidation;
using Pazario.Products.Domain.Categories;

namespace Pazario.Products.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode(CategoryErrors.NullValue.Code)
                .MaximumLength(100)
                .WithErrorCode(CategoryErrors.MaxLenght.Code);

            RuleFor(x => x.Icon)
               .MaximumLength(200)
               .WithMessage(CategoryErrors.MaxLenght.Code);
        }
    }
}
