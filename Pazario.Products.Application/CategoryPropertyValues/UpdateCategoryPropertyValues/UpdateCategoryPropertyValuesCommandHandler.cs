using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.CategoryPropertyValues;
using System.Linq.Expressions;

namespace Pazario.Products.Application.CategoryPropertyValues.UpdateCategoryPropertyValues
{
    public class UpdateCategoryPropertyValuesCommandHandler
            (ICategoryPropertyValuesRepository _valuesRepository,
            IUnitOfWork unitOfWork) : ICommandHandler<UpdateCategoryPropertyValuesCommand>
    {
        public async Task<Result> Handle(UpdateCategoryPropertyValuesCommand request, CancellationToken cancellationToken)
        {
            var values = await _valuesRepository.SelectAsync(new FilteringOptions<CategoryPropertyValue>
            {
                Predicates = new List<Expression<Func<CategoryPropertyValue, bool>>>
                    {
                        x => x.CategoryPropertyId == request.PropertyId
                    },
                IsLoadingAsNoTracking = true
            });

            foreach (var item in request.Items)
            {
                if (item.Id is not null)
                {
                    await UpdateCategoryPropertyValueAsync(values, item, cancellationToken);
                }
                else
                {
                    if (!values.Any(x => x.Value.Trim().ToLower() == item.Value.Normalize()))
                    {
                        await InsertNewValueAsync(request, item, cancellationToken);
                    }
                }
            }

            var removedValues = values.Where(x => !request.Items.Any(i => i.Id == x.Id)).ToList();

            await DeleteCategoryPropertyValuesAsync(removedValues, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        private async Task InsertNewValueAsync(
            UpdateCategoryPropertyValuesCommand request,
            UpdateCategoryPropertyValuesCommandItem item,
            CancellationToken cancellationToken)
        {
            var newValue = CategoryPropertyValue.Create(request.PropertyId, item.Value);

            await _valuesRepository.InsertAsync(newValue);
        }

        private async Task UpdateCategoryPropertyValueAsync(
                    IQueryable<CategoryPropertyValue> values,
                    UpdateCategoryPropertyValuesCommandItem item,
                    CancellationToken cancellationToken)
        {
            var value = values.FirstOrDefault(x => x.Id == item.Id);
            if (value is not null)
            {
                value.Update(item.Value);
                await _valuesRepository.UpdateAsync(value);
            }
        }

        private async Task DeleteCategoryPropertyValuesAsync(List<CategoryPropertyValue> removedValues, CancellationToken cancellationToken)
        {
            foreach (var value in removedValues)
            {
                await _valuesRepository.DeleteAsync(value, cancellationToken);
            }
        }
    }
}
