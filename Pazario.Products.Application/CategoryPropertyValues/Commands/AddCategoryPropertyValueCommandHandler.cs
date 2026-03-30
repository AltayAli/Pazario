using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.CategoryPropertyValues;
using System.Linq.Expressions;

namespace Pazario.Products.Application.CategoryPropertyValues.Commands
{
    public class AddCategoryPropertyValueCommandHandler
            (ICategoryPropertyValuesRepository _valuesRepository,
            IUnitOfWork unitOfWork)
        : ICommandHandler<AddCategoryPropertyValueCommand>
    {
        public async Task<Result> Handle(AddCategoryPropertyValueCommand request, CancellationToken cancellationToken)
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
                if(!values.Any(x => x.Value.Trim().ToLower() == item.Normalize()))
                {
                    var value = new CategoryPropertyValue
                    {
                        CategoryPropertyId = request.PropertyId,
                        Value = item
                    };

                    await _valuesRepository.Insert(value);
                }
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
