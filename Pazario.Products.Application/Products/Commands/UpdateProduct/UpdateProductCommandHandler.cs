using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Models;
using Pazario.Products.Domain.Products;
using Pazario.Products.Domain.Products.Events;
using System.Linq.Expressions;

namespace Pazario.Products.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler
        (IProductsRepository _productsRepository,
        IModelsRepository _modelsRepository,
         IUnitOfWork _unitOfWork) : ICommandHandler<UpdateProductCommand>
    {
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.ModelId.HasValue)
            {
                bool modelExists = await _modelsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Model>
                {
                    Predicates = new List<Expression<Func<Model, bool>>>
                    {
                        model => model.Id == request.ModelId.Value
                    },
                }) is not null;

                if (!modelExists)
                {
                    return Result.Failure(ModelErrors.NotFound);
                }
            }

            var product = await _productsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Product>
            {
                Predicates = new List<Expression<Func<Product, bool>>>
                {
                    product => product.Id == request.Id
                },
                IsLoadingAsNoTracking = false
            });

            if (product is null)
            {
                return Result.Failure(ModelErrors.NotFound);
            }

            product.Update(request.Name, request.Description, request.ModelId);

            await _productsRepository.UpdateAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
