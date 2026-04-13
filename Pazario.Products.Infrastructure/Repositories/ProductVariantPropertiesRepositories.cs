using Microsoft.AspNetCore.Http;
using Pazario.Products.Application.Abstractions;
using Pazario.Products.Domain.ProductVariantProperties;
using Pazario.Products.Infrastructure.Data;

namespace Pazario.Products.Infrastructure.Repositories
{
    public sealed class ProductVariantPropertiesRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<ProductVariantProperty>(dataContext, httpContextAccessor, dateTimeProvider), IProductVariantPropertiesRepository;
}
