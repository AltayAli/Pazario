using FluentValidation;
using Pazario.Products.Domain.CategoryProperties;

namespace Pazario.Products.Application.CategoryPropertyValues.UpdateCategoryPropertyValues
{
    public class UpdateCategoryPropertyValuesCommandValidator : AbstractValidator<UpdateCategoryPropertyValuesCommand>
    {
        public UpdateCategoryPropertyValuesCommandValidator()
        {
            RuleForEach(x => x.Items)
                .SetValidator(new UpdateCategoryPropertyCommandItemValidator());
        }
        public class UpdateCategoryPropertyCommandItemValidator : AbstractValidator<UpdateCategoryPropertyValuesCommandItem>
        {
            public UpdateCategoryPropertyCommandItemValidator()
            {
                RuleFor(x => x.Value)
                    .NotEmpty()
                    .WithErrorCode(CategoryPropertyErrors.NullValue.Code)
                    .MaximumLength(100)
                    .WithErrorCode(CategoryPropertyErrors.MaxLenght.Code);
            }
        }
    }
}
