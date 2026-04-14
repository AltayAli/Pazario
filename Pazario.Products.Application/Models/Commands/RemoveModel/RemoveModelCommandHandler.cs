using Pazario.Common.Application.Abstractions.Messaging;
using Pazario.Common.Domain.Abstractions;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Models;

namespace Pazario.Products.Application.Models.Commands.RemoveModel
{
    public class RemoveModelCommandHandler (IModelsRepository modelsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RemoveModelCommand>
    {
        public async Task<Result> Handle(RemoveModelCommand request, CancellationToken cancellationToken)
        {
            var model = await modelsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Model>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Model, bool>>> {
                    m => m.Id == request.Id
                },
                IsLoadingAsNoTracking = false
            }, cancellationToken);

            if (model == null)
            {
                return Result.Failure(ModelErrors.NotFound);
            }

            await modelsRepository.DeleteAsync(model,cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
