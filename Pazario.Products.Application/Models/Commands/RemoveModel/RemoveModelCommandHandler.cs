using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Markas;
using Pazario.Products.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Models.Commands.RemoveModel
{
    public class RemoveModelCommandHandler (IModelsRepository modelsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RemoveModelCommand>
    {
        public async Task<Result> Handle(RemoveModelCommand request, CancellationToken cancellationToken)
        {
            var model = await modelsRepository.SelectSimpleOrDefault(new FilteringOptions<Model>
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

            await modelsRepository.Delete(model,cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
