using Pazario.Common.Application.Abstractions.Messaging;
using Pazario.Common.Domain.Abstractions;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Products;
using System.Linq.Expressions;

namespace Pazario.Products.Application.Products.Commands.RemoveProduct
{
    public class RemoveProductCommandHandler(
        IProductsRepository productsRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<RemoveProductCommand>
    {
        public async Task<Result> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Product>
            {
                Predicates = new List<Expression<Func<Product, bool>>>
                {
                    p => p.Id == request.Id
                },
                IsLoadingAsNoTracking = false
            }, cancellationToken);

            if (product is null)
                return Result.Failure(ProductErrors.NotFound);

            await productsRepository.DeleteAsync(product, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
