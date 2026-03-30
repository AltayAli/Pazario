using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Products;

namespace Pazario.Products.Application.Products.Commands.RemoveProduct
{
    public class RemoveProductCommandHandler(
        IProductsRepository _productsRepository,
         IUnitOfWork _unitOfWork) : ICommandHandler<RemoveProductCommand>
    {
        public Task<Result> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
