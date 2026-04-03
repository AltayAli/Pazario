using Microsoft.AspNetCore.Http;
using Pazario.Products.Application.Abstractions;
using Pazario.Products.Domain.ProductVariantImages;
using Pazario.Products.Infrastructure.Data;

namespace Pazario.Products.Infrastructure.Repositories
{
    public sealed class ProductVariantImagesRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<ProductVariantImage>(dataContext, httpContextAccessor, dateTimeProvider), IProductVariantImagesRepository;
}
