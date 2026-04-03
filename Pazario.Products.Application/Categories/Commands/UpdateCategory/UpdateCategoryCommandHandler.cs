using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Domain.Categories.Events;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Markas;

namespace Pazario.Products.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler 
        (IUnitOfWork unitOfWork,
         ICategoriesRepository cachedCategoriesRepository)
        : ICommandHandler<UpdateCategoryCommand>
    {
        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await cachedCategoriesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Category>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Category, bool>>> {
                    m => m.Id == request.Id
                },
                IsLoadingAsNoTracking = false
            }, cancellationToken);

            if (category is null)
            {
                return Result.Failure(MarkaErrors.NotFound);
            }

            string normalizedName = request.Name.Trim().ToLower();

            bool categoryExists = cachedCategoriesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Category>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Category, bool>>> {
                    m => m.Name.Value.ToLower() == normalizedName && m.ParentId == request.ParentId && m.Id != request.Id
                }
            }, cancellationToken) is not null;

            if (categoryExists)
            {
                return Result.Failure(MarkaErrors.AlreadyExists);
            }

            category.Update(request.Name, request.Icon, request.ParentId);

            await cachedCategoriesRepository.UpdateAsync(category, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
