using Microsoft.AspNetCore.Http;
using Pazario.Common.Application.Abstractions;
using Pazario.Common.Infrastructure.Repositories;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Infrastructure.Data;

namespace Pazario.Products.Infrastructure.Repositories
{
    public sealed class CategoriesRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<Category,ProductsDbContext>(dataContext, httpContextAccessor, dateTimeProvider), ICategoriesRepository;
}
