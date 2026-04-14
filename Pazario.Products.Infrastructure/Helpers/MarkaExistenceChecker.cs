using Pazario.Common.Domain.Abstractions;
using Pazario.Products.Application.Markas;
using Pazario.Products.Domain.Markas;

namespace Pazario.Products.Infrastructure.Helpers
{
    public class MarkaExistenceChecker(IMarkasRepository markasRepository) : IMarkaExistenceChecker
    {
        public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
        {
            string normalizedName = name.Trim().ToLower();
            bool markaExists = await markasRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Marka>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Marka, bool>>> {
                    m => m.Name.Value.ToLower() == normalizedName
                }
            }, cancellationToken) is not null;

            return markaExists;
        }
    }
}
