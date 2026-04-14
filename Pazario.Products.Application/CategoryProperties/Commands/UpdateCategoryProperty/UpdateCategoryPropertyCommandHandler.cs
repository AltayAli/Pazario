using Pazario.Common.Application.Abstractions.Messaging;
using Pazario.Common.Application.Exceptions;
using Pazario.Common.Domain.Abstractions;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.CategoryProperties;
using System.Linq.Expressions;

namespace Pazario.Products.Application.CategoryProperties.Commands.UpdateCategoryProperty
{
    public class UpdateCategoryPropertyCommandHandler
            (ICategoryPropertiesRepository propertiesRepository,
            IUnitOfWork unitOfWork) : ICommandHandler<UpdateCategoryPropertyCommand, List<Guid>>
    {
        public async Task<Result<List<Guid>>> Handle(UpdateCategoryPropertyCommand request, CancellationToken cancellationToken)
        {
            var properties = await propertiesRepository.SelectAsync(new FilteringOptions<CategoryProperty>
            {
                Predicates = new List<Expression<Func<CategoryProperty, bool>>>
                {
                    prop => prop.CategoryId == request.CategoryId
                },
            }, cancellationToken);

            var propertyIds = new List<Guid>();

            foreach (var item in request.Items)
            {
                if (item.Id is null)
                {
                    var newId = await InsertPropertyAsync(request.CategoryId, item, cancellationToken);
                    propertyIds.Add(newId);
                }
                else
                {
                    var property = properties.FirstOrDefault(p => p.Id == item.Id);

                    if (property is null)
                    {
                        return Result.Failure<List<Guid>>(propertyIds, CategoryPropertyErrors.NotFound);
                    }

                    await UpdatePropertyAsync(request.CategoryId, property, item, cancellationToken);
                    propertyIds.Add(property.Id);
                }
            }

            var removedProperties = properties.Where(p => !request.Items.Any(i => i.Id == p.Id)).ToList();

            if (removedProperties.Any())
            {
                await DeletePropertyAsync(removedProperties, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(propertyIds);
        }

        private async Task<Guid> InsertPropertyAsync(Guid categoryId, UpdateCategoryPropertyCommandItem item, CancellationToken cancellationToken)
        {
            bool propertyAlreadyExists = await DoesCategoryPropertyExistAsync(categoryId, item.Name, item.Type);

            if (propertyAlreadyExists)
            {
                throw new UpdateCategoryPropertyAlreadyExistsException();
            }

            var property = CategoryProperty.Create(categoryId, item.Name, item.Type, item.AddToFilter, item.IsRequired, item.DisplayOrder);

            await propertiesRepository.InsertAsync(property, cancellationToken);

            return property.Id;
        }

        private async Task<bool> DoesCategoryPropertyExistAsync(Guid categoryId, string name, CategoryPropertyType type)
        {
            string normalizedName = name.Trim().ToLower();

            bool propertyAlreadyExists = await propertiesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<CategoryProperty>
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

        private async Task UpdatePropertyAsync(Guid categoryId, CategoryProperty property, UpdateCategoryPropertyCommandItem item, CancellationToken cancellationToken)
        {
            bool propertyAlreadyExists = await DoesCategoryPropertyExistAsync(categoryId, item.Name, item.Type);

            if (propertyAlreadyExists)
            {
                throw new UpdateCategoryPropertyAlreadyExistsException();
            }

            property.Update(item.Name, item.Type, item.AddToFilter, item.IsRequired, item.DisplayOrder);

            await propertiesRepository.UpdateAsync(property, cancellationToken);
        }

        private async Task DeletePropertyAsync(List<CategoryProperty> properties, CancellationToken cancellationToken)
        {
            foreach (var property in properties)
            {
                await propertiesRepository.DeleteAsync(property, cancellationToken);
            }
        }
    }
}
