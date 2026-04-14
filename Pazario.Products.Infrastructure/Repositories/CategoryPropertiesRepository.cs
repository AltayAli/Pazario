using Microsoft.AspNetCore.Http;
using Pazario.Common.Application.Abstractions;
using Pazario.Common.Infrastructure.Repositories;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Domain.CategoryProperties;
using Pazario.Products.Infrastructure.Data;

namespace Pazario.Products.Infrastructure.Repositories
{
    public sealed class CategoryPropertiesRepository(
        ProductsDbContext dataContext,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider)
        : BaseRepository<CategoryProperty, ProductsDbContext>(dataContext, httpContextAccessor, dateTimeProvider), ICategoryPropertiesRepository;
}
