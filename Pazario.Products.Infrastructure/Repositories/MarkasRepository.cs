using Microsoft.AspNetCore.Http;
using Pazario.Common.Application.Abstractions;
using Pazario.Common.Infrastructure.Repositories;
using Pazario.Products.Domain.Markas;
using Pazario.Products.Infrastructure.Data;

namespace Pazario.Products.Infrastructure.Repositories
{
    public sealed class MarkasRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<Marka,ProductsDbContext>(dataContext, httpContextAccessor, dateTimeProvider), IMarkasRepository;
}
