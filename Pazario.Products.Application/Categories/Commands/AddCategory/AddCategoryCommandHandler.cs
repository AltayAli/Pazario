using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Domain.Categories.Events;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Markas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Categories.Commands.AddCategory
{
    public class AddCategoryCommandHandler 
        (ICategoriesRepository categoriesRepository,
            IUnitOfWork unitOfWork)
        : ICommandHandler<AddCategoryCommand>
    {
        public async Task<Result> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            string normalizedName = request.Name.Trim().ToLower();
            bool categoryExists = categoriesRepository.SelectSimpleOrDefault(new FilteringOptions<Category>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Category, bool>>> {
                    m => m.Name.Value.ToLower() == normalizedName && m.ParentId == request.ParentId
                }
            }, cancellationToken) is not null;

            if (categoryExists)
            {
                return Result.Failure(CategoryErrors.AlreadyExists);
            }

            Icon? icon = null;

            if (!string.IsNullOrWhiteSpace(request.Icon))
            {
                icon = new Icon(request.Icon);
            }

            var category = new Category
            {
                Name = new Name(request.Name),
                ParentId = request.ParentId,
                Icon = icon,
            };

            category.AddDomainEvent(new AddCategoryEvent());
            await categoriesRepository.Insert(category,cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(category);
        }
    }
}
