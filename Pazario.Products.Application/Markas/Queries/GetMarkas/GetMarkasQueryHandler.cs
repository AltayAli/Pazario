using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Markas;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Pazario.Products.Application.Markas.Queries.GetMarkas
{
    internal class GetMarkasQueryHandler (ICachedMarkasRepository markasRepository)
        : IQueryHandler<GetMarkasQuery, List<GetMarkasResponse>>
    {
        public async Task<Result<List<GetMarkasResponse>>> Handle(GetMarkasQuery request, CancellationToken cancellationToken)
        {
            var markas = await markasRepository.SelectAsync(new FilteringOptions<Marka>
            {
                Predicates = new List<Expression<Func<Marka, bool>>>
                {
                    m => m.Name.Value.Contains(request.Key) || string.IsNullOrEmpty(request.Key)
                },
                Relations = new List<string> { "Models" }
            });

            var response = markas.Select(m => new GetMarkasResponse
            {
                Id = m.Id,
                Name = m.Name.Value,
                LastModifiedDate = m.ModifiedDate ?? m.AddedDate,
                ModelsCount = m.Models.Count
            }).ToList();

            return Result.Success(response);
        }
    }
}
