using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Categories;
using System.Linq.Expressions;

namespace Pazario.Products.Application.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler 
        (ICategoriesRepository categoriesRepository)
        : IQueryHandler<GetCategoriesQuery, List<GetCategoriesItemResponse>>
    {
        public async Task<Result<List<GetCategoriesItemResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var markas = await categoriesRepository.SelectAsync(new FilteringOptions<Category>
            {
                Predicates = new List<Expression<Func<Category, bool>>>
                {
                    m => string.IsNullOrEmpty(request.Key) || m.Name.Value.Contains(request.Key) ,
                    m => m.ParentId == request.ParentId
                },
                Relations = new List<string> { "Children" }
            });

            var response = markas.Select(m => new GetCategoriesItemResponse
            {
                Id = m.Id,
                Name = m.Name.Value,
                LastModifiedDate = m.ModifiedDate ?? m.AddedDate,
                SubCategoriesCount = m.Children.Count,
                Icon = m.Icon != null ? m.Icon.Value : string.Empty 
            }).ToList();

            return Result.Success(response);
        }
    }
}
