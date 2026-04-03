using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Domain.CategoryProperties;
using System.Linq.Expressions;

namespace Pazario.Products.Application.CategoryProperties.Commands.AddCategoryProperty
{
    public class AddCategoryPropertyCommandHandler 
            (ICategoryPropertiesRepository propertiesRepository,
            ICategoriesRepository cachedCategoriesRepository,
            IUnitOfWork unitOfWork)
        : ICommandHandler<AddCategoryPropertyCommand>
    {
        public async Task<Result> Handle(AddCategoryPropertyCommand request, CancellationToken cancellationToken)
        {
            var properties = await propertiesRepository.SelectAsync(new FilteringOptions<CategoryProperty>
            {
                Predicates = new List<Expression<Func<CategoryProperty, bool>>>
                {
                    prop => prop.CategoryId == request.CategoryId
                },
            });

            foreach (var item in request.Items)
            {
                string normalizedName = item.Name.Trim().ToLower();

                bool propertyAlreadyExists = properties.Any(prop => prop.Name.Value.ToLower() == normalizedName && prop.Type == item.Type);

                if (propertyAlreadyExists)
                {
                    return Result.Failure(CategoryPropertyErrors.AlreadyExists);
                }

                var categoryProperty = CategoryProperty.Create(
                    request.CategoryId,
                    item.Name,
                    item.Type,
                    item.AddToFilter,
                    item.IsRequired,
                    item.DisplayOrder
                );

                await propertiesRepository.InsertAsync(categoryProperty, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
