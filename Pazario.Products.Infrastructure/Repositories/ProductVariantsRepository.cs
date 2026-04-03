using Microsoft.AspNetCore.Http;
using Pazario.Products.Application.Abstractions;
using Pazario.Products.Domain.ProductVariants;
using Pazario.Products.Infrastructure.Data;

namespace Pazario.Products.Infrastructure.Repositories
{
    public sealed class ProductVariantsRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<ProductVariant>(dataContext, httpContextAccessor, dateTimeProvider), IProductVariantsRepository;
}
