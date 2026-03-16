using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Markas;
using Pazario.Products.Domain.Models;
using System.Linq.Expressions;

namespace Pazario.Products.Application.Models.Commands.UpdateModel
{
    public class UpdateModelCommandHandler (ICachedModelsRepository modelsRepository,
                                ICachedMarkasRepository markasRepository,
                                IUnitOfWork unitOfWork) : ICommandHandler<UpdateModelCommand>
    {
        public async Task<Result> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {

            bool markaExists = await markasRepository.SelectSimpleOrDefault(new FilteringOptions<Marka>
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


            string normalizedName = request.Name.Trim().ToLower();
            bool modelExists = modelsRepository.SelectSimpleOrDefault(new FilteringOptions<Model>
            {
                Predicates = new List<Expression<Func<Model, bool>>> {
                    m => m.Name.Value.ToLower() == normalizedName
                }
            }, cancellationToken) is not null;

            if (modelExists)
            {
                return Result.Failure(ModelErrors.AlreadyExists);
            }

            Model? model = await modelsRepository.SelectSimpleOrDefault(new FilteringOptions<Model>
            {
                Predicates = new List<Expression<Func<Model, bool>>>
                {
                    model => model.Id == request.Id
                },
            }, cancellationToken);

            if (model is null) { 
                return Result.Failure(ModelErrors.NotFound);
            }
            model.MarkaId = request.MarkaId;
            model.Name = (Name)request.Name;

            await modelsRepository.Update(model,cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
