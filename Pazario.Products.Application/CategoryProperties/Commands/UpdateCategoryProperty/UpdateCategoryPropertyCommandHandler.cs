using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Application.Exceptions;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Domain.CategoryProperties;
using Pazario.Products.Domain.Common;
using System.Linq.Expressions;

namespace Pazario.Products.Application.CategoryProperties.Commands.UpdateCategoryProperty
{
    public class UpdateCategoryPropertyCommandHandler
            (ICategoryPropertiesRepository propertiesRepository,
            ICategoriesRepository categoriesRepository,
            IUnitOfWork unitOfWork) : ICommandHandler<UpdateCategoryPropertyCommand>
    {
        public async Task<Result> Handle(UpdateCategoryPropertyCommand request, CancellationToken cancellationToken)
        {
            var properties = await propertiesRepository.SelectAsync(new FilteringOptions<CategoryProperty>
            {
                Predicates = new List<Expression<Func<CategoryProperty, bool>>>
                {
                    prop => prop.CategoryId == request.CategoryId
                },
            }, cancellationToken);

            foreach (var item in request.Items)
            {
                if(item.Id is null)
                {
                    await InsertPropertyAsync(request.CategoryId, item, cancellationToken);
                }
                else
                {
                    var property = properties.FirstOrDefault(p => p.Id == item.Id);

                    if (property is null)
                    {
                        return Result.Failure(CategoryPropertyErrors.NotFound);
                    }

                    await UpdatePropertyAsync(request.CategoryId, property, cancellationToken);
                }
            }

            var removedProperties = properties.Where(p => !request.Items.Any(i => i.Id == p.Id)).ToList();

            if (removedProperties.Any()) 
            { 
               await DeletePropertyAsync(removedProperties, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        private async Task InsertPropertyAsync(Guid categoryId,UpdateCategoryPropertyCommandItem item, CancellationToken cancellationToken)
        {
            bool propertyAlreadyExists = await DoesCategoryPropertyExistAsync(categoryId, item.Name, item.Type);

            if (propertyAlreadyExists)
            {
                throw new UpdateCategoryPropertyAlreadyExistsException();
            }

            var property = new CategoryProperty
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId,
                Name = new Name(item.Name),
                Type = item.Type,
                AddToFilter = item.AddToFilter,
                IsRequired = item.IsRequired,
                DisplayOrder = item.DisplayOrder
            };

            await propertiesRepository.Insert(property, cancellationToken);
        }

        private async Task<bool> DoesCategoryPropertyExistAsync(Guid categoryId, string name, CategoryPropertyType type)
        {
            string normalizedName = name.Trim().ToLower();

            bool propertyAlreadyExists = await propertiesRepository.SelectSimpleOrDefault(new FilteringOptions<CategoryProperty>
            {
                Predicates = new List<Expression<Func<CategoryProperty, bool>>>
                {
                    prop => prop.CategoryId == categoryId &&
                            prop.Name.Value.ToLower() == normalizedName &&
                            prop.Type == type
                },
            }) is not null;
            return propertyAlreadyExists;
        }

        private async Task UpdatePropertyAsync(Guid categoryId, CategoryProperty property, CancellationToken cancellationToken)
        {
            bool propertyAlreadyExists = await DoesCategoryPropertyExistAsync(categoryId, property.Name, property.Type);

            if (propertyAlreadyExists)
            {
                throw new UpdateCategoryPropertyAlreadyExistsException();
            }

            property.Name = new Name(property.Name);
            property.Type = property.Type;
            property.AddToFilter = property.AddToFilter;
            property.IsRequired = property.IsRequired;
            property.DisplayOrder = property.DisplayOrder;
            await propertiesRepository.Update(property, cancellationToken);
        }

        private async Task DeletePropertyAsync(List<CategoryProperty> properties, CancellationToken cancellationToken)
        {
            foreach (var property in properties)
            {
                await propertiesRepository.Delete(property, cancellationToken);
            }
        }
    }
}
