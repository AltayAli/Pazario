using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.CategoryProperties;
using System.Linq.Expressions;

namespace Pazario.Products.Application.CategoryProperties.Queries.GetCategoryProperties
{
    public class GetCategoryPropertiesQueryHandler 
        (ICategoryPropertiesRepository categoryPropertiesRepository)
        : IQueryHandler<GetCategoryPropertiesQuery, List<GetCategoryProperiesResponse>>
    {
        public async Task<Result<List<GetCategoryProperiesResponse>>> Handle(GetCategoryPropertiesQuery request, CancellationToken cancellationToken)
        {
            var categoryProperties = await categoryPropertiesRepository.SelectAsync(new FilteringOptions<CategoryProperty>
            {
                Predicates = new List<Expression<Func<CategoryProperty, bool>>>
                {
                    m => string.IsNullOrEmpty(request.Key) || m.Name.Value.Contains(request.Key) ,
                    m => m.CategoryId == request.CategoryId
                },
                Relations = new List<string> { "Values" }
            }, cancellationToken);

            var response = categoryProperties.Select(m => new GetCategoryProperiesResponse
            {
                Id = m.Id,
                Name = m.Name.Value,
                LastModifiedDate = m.ModifiedDate ?? m.AddedDate,
                AddToFilter = m.AddToFilter,
                IsRequired = m.IsRequired,
                DisplayOrder = m.DisplayOrder,
                Type = m.Type,
                ValuesCount = m.Values.Count
            }).ToList();

            return Result.Success(response);
        }
    }
}
