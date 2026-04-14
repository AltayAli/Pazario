using Microsoft.AspNetCore.Http;
using Pazario.Common.Application.Abstractions;
using Pazario.Common.Infrastructure.Repositories;
using Pazario.Products.Domain.Products;
using Pazario.Products.Infrastructure.Data;

namespace Pazario.Products.Infrastructure.Repositories
{
    public sealed class ProductsRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<Product, ProductsDbContext>(dataContext, httpContextAccessor, dateTimeProvider), IProductsRepository;
}
