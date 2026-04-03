using Microsoft.AspNetCore.Http;
using Pazario.Products.Application.Abstractions;
using Pazario.Products.Domain.CategoryProperties;
using Pazario.Products.Infrastructure.Data;

namespace Pazario.Products.Infrastructure.Repositories
{
    public sealed class CategoryPropertiesRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<CategoryProperty>(dataContext, httpContextAccessor, dateTimeProvider), ICategoryPropertiesRepository;
}
