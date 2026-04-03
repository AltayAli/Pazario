using Microsoft.AspNetCore.Http;
using Pazario.Products.Application.Abstractions;
using Pazario.Products.Domain.CategoryPropertyValues;
using Pazario.Products.Infrastructure.Data;

namespace Pazario.Products.Infrastructure.Repositories
{
    public sealed class CategoryPropertyValuesRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<CategoryPropertyValue>(dataContext, httpContextAccessor, dateTimeProvider), ICategoryPropertyValuesRepository;
}
