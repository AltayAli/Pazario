using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Markas;
using System.Linq.Expressions;

namespace Pazario.Products.Application.Markas.Queries.GetMarka
{
    public class GetMarkaQueryHandler(IMarkasRepository markasRepository) 
                        : IQueryHandler<GetMarkaQuery, GetMarkaResponse>
    {
        public async Task<Result<GetMarkaResponse>> Handle(GetMarkaQuery request, CancellationToken cancellationToken)
        {
            var marka = await markasRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Marka>
            {
                Predicates = new List<Expression<Func<Marka, bool>>>
                {
                    m => m.Id == request.Id,
                },
                Relations = new List<string> { "Models" }
            });

            return Result.Success(new GetMarkaResponse
            {
                Id = marka.Id,
                Name = marka.Name.Value,
                LastModifiedDate = marka.ModifiedDate ?? marka.AddedDate,
                ModelsCount = marka.Models.Count
            });
        }
    }
}
