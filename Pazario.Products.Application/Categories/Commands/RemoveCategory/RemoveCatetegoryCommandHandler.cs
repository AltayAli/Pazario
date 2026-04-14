using Pazario.Common.Application.Abstractions.Messaging;
using Pazario.Common.Domain.Abstractions;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Domain.Markas;

namespace Pazario.Products.Application.Categories.Commands.RemoveCategory
{
    public class RemoveCatetegoryCommandHandler 
        (IUnitOfWork unitOfWork,
        ICategoriesRepository categoriesRepository)
        : ICommandHandler<RemoveCategoryCommand>
    {
        public async Task<Result> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoriesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Category>
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

            category.Remove();

            await categoriesRepository.DeleteAsync(category, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
