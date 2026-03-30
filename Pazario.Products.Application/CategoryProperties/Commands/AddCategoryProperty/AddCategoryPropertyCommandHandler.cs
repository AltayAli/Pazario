using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Domain.CategoryProperties;
using Pazario.Products.Domain.Common;
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
            foreach (var item in request.Items)
            {

                string normalizedName = item.Name.Trim().ToLower();

                bool propertyAlreadyExists = await propertiesRepository.SelectSimpleOrDefault(new FilteringOptions<CategoryProperty>
                {
                    Predicates = new List<Expression<Func<CategoryProperty, bool>>>
                {
                    prop => prop.CategoryId == request.CategoryId &&
                            prop.Name.Value.ToLower() == normalizedName &&
                            prop.Type == item.Type
                },
                }) is not null;


                if (propertyAlreadyExists)
                {
                    return Result.Failure(CategoryPropertyErrors.AlreadyExists);
                }

                var categoryProperty = new CategoryProperty
                {
                    Name = new Name(item.Name),
                    Type = item.Type,
                    AddToFilter = item.AddToFilter,
                    IsRequired = item.IsRequired,
                    DisplayOrder = item.DisplayOrder,
                    CategoryId = request.CategoryId
                };

                await propertiesRepository.Insert(categoryProperty, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
