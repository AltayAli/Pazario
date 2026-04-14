using Microsoft.AspNetCore.Http;
using Pazario.Common.Application.Abstractions;
using Pazario.Common.Infrastructure.Repositories;
using Pazario.Products.Domain.ProductVariants;
using Pazario.Products.Infrastructure.Data;

namespace Pazario.Products.Infrastructure.Repositories
{
    public sealed class ProductVariantsRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<ProductVariant, ProductsDbContext>(dataContext, httpContextAccessor, dateTimeProvider), IProductVariantsRepository;
}
