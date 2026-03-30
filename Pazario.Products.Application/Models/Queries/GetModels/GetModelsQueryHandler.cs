using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Models;

namespace Pazario.Products.Application.Models.Queries.GetModels
{
    public class GetModelsQueryHandler (IModelsRepository modelsRepository)
        : IQueryHandler<GetModelsQuery, List<GetModelsQueryResponse>>
    {
        public async Task<Result<List<GetModelsQueryResponse>>> Handle(GetModelsQuery request, CancellationToken cancellationToken)
        {
            var models = await modelsRepository.SelectAsync(new FilteringOptions<Model>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Model, bool>>> {
                    m => m.MarkaId == request.MarkaId
                }
            }, cancellationToken);

            var results = models.Select(m => new GetModelsQueryResponse
            {
                Id = m.Id,
                Name = m.Name.Value,
                LastModifiedDate = m.ModifiedDate ?? m.AddedDate
            }).ToList();

            return Result.Success(results);
        }
    }
}
