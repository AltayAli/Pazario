using Pazario.Common.Application.Abstractions.Messaging;
using Pazario.Common.Domain.Abstractions;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Models;
using Pazario.Products.Domain.ProductCategories;
using Pazario.Products.Domain.Products;
using System.Linq.Expressions;

namespace Pazario.Products.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler(
        IModelsRepository modelsRepository,
        IProductsRepository productsRepository,
        IProductCategoriesRepository productCategoriesRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<CreateProductCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.ModelId.HasValue)
            {
                bool modelExists = await modelsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Model>
                {
                    Predicates = new List<Expression<Func<Model, bool>>>
                    {
                        m => m.Id == request.ModelId.Value
                    }
                }) is not null;

                if (!modelExists)
                    return Result.Failure<Guid>(default, ModelErrors.NotFound);
            }

            var product = Product.Create(request.Name, request.Description, request.ModelId);
            await productsRepository.InsertAsync(product, cancellationToken);

            foreach (var categoryId in request.CategoryIds)
            {
                var productCategory = ProductCategory.Create(product.Id, categoryId);
                await productCategoriesRepository.InsertAsync(productCategory, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(product.Id);
        }
    }
}
