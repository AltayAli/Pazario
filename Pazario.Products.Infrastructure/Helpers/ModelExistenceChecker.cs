using Pazario.Common.Domain.Abstractions;
using Pazario.Products.Application.Models;
using Pazario.Products.Domain.Models;
using System.Linq.Expressions;

namespace Pazario.Products.Infrastructure.Helpers
{
    public class ModelExistenceChecker(IModelsRepository modelsRepository) : IModelExistenceChecker
    {
        public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
        {
            string normalizedName = name.Trim().ToLower();
            bool modelExists = await modelsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Model>
            {
                Predicates = new List<Expression<Func<Model, bool>>> {
                    m => m.Name.Value.ToLower() == normalizedName
                }
            }, cancellationToken) is not null;

            return modelExists;
        }
    }
}
