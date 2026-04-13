using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Models;
using Pazario.Products.Domain.ProductCategories;
using Pazario.Products.Domain.Products;
using System.Linq.Expressions;

namespace Pazario.Products.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(
        IProductsRepository productsRepository,
        IModelsRepository modelsRepository,
        IProductCategoriesRepository productCategoriesRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateProductCommand>
    {
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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
                    return Result.Failure(ModelErrors.NotFound);
            }

            var product = await productsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Product>
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<Product, bool>>>
                {
                    p => p.Id == request.Id
                }
            });

            if (product is null)
                return Result.Failure(ProductErrors.NotFound);

            product.Update(request.Name, request.Description, request.ModelId);
            await productsRepository.UpdateAsync(product, cancellationToken);

            var existingCategories = (await productCategoriesRepository.SelectAsync(new FilteringOptions<ProductCategory>
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<ProductCategory, bool>>>
                {
                    pc => pc.ProductId == request.Id
                }
            })).ToList();

            foreach (var existing in existingCategories)
                await productCategoriesRepository.DeleteAsync(existing, cancellationToken);

            foreach (var categoryId in request.CategoryIds)
            {
                var productCategory = ProductCategory.Create(product.Id, categoryId);
                await productCategoriesRepository.InsertAsync(productCategory, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
