using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Domain.Categories.Events;
using Pazario.Products.Domain.Markas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Categories.Commands.RemoveCategory
{
    public class RemoveCatetegoryCommandHandler 
        (IUnitOfWork unitOfWork,
        ICategoriesRepository categoriesRepository)
        : ICommandHandler<RemoveCategoryCommand>
    {
        public async Task<Result> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await categoriesRepository.SelectSimpleOrDefault(new FilteringOptions<Category>
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

            category.AddDomainEvent(new RemoveCategoryEvent());
            await categoriesRepository.Delete(category, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
