using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Markas;
using Pazario.Products.Domain.Models;
using System.Linq.Expressions;

namespace Pazario.Products.Application.Models.Commands.UpdateModel
{
    public class UpdateModelCommandHandler (IModelsRepository modelsRepository,
                                IMarkasRepository markasRepository,
                                IModelExistenceChecker modelExistenceChecker,
                                IUnitOfWork unitOfWork) : ICommandHandler<UpdateModelCommand>
    {
        public async Task<Result> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {

            bool markaExists = await markasRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Marka>
            {
                Predicates = new List<Expression<Func<Marka, bool>>>
                {
                    marka => marka.Id == request.MarkaId
                },
            },cancellationToken) is not null;

            if (markaExists)
            {
                return Result.Failure(MarkaErrors.NotFound);
            }


            var modelExistsViaChecker = await modelExistenceChecker.ExistsAsync(request.Name, cancellationToken);

            if (modelExistsViaChecker)
            {
                return Result.Failure(ModelErrors.AlreadyExists);
            }

            Model? model = await modelsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Model>
            {
                Predicates = new List<Expression<Func<Model, bool>>>
                {
                    model => model.Id == request.Id
                },
            }, cancellationToken);

            if (model is null) { 
                return Result.Failure(ModelErrors.NotFound);
            }

            model.Update(request.Name, request.MarkaId);

            await modelsRepository.UpdateAsync(model,cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
