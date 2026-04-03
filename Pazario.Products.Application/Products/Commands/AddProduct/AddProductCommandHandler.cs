using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Models;
using Pazario.Products.Domain.Products;
using Pazario.Products.Domain.Products.Events;
using System.Linq.Expressions;

namespace Pazario.Products.Application.Products.Commands.AddProduct
{
    public class AddProductCommandHandler 
        (IProductsRepository _productsRepository,
        IModelsRepository _modelsRepository,
         IUnitOfWork _unitOfWork)
        : ICommandHandler<AddProductCommand>
    {
        public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
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

            var product = Product.Create(request.Name, request.Description, request.ModelId);

            await _productsRepository.InsertAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}
