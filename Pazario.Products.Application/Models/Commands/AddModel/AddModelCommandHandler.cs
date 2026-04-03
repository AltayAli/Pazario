using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Markas;
using Pazario.Products.Domain.Models;
using Pazario.Products.Domain.Models.Events;
using System.Linq.Expressions;

namespace Pazario.Products.Application.Models.Commands.AddModel
{
    public class AddModelCommandHandler 
        (IMarkasRepository markasRepository,
        IModelsRepository modelsRepository,
        IModelExistenceChecker modelExistenceChecker,
        IUnitOfWork unitOfWork)
        : ICommandHandler<AddModelCommand>
    {
        public async Task<Result> Handle(AddModelCommand request, CancellationToken cancellationToken)
        {
            bool markaExists = markasRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Marka>
            {
                Predicates = new List<Expression<Func<Marka, bool>>>
                {
                    marka => marka.Id == request.MarkaId
                },
            }) is not null;

            if (markaExists)
            {
                return Result.Failure(MarkaErrors.NotFound);
            }

            var modelExistsViaChecker = await modelExistenceChecker.ExistsAsync(request.Name, cancellationToken);

            if (modelExistsViaChecker)
            {
                return Result.Failure(ModelErrors.AlreadyExists);
            }

            var model = Model.Create(request.Name, request.MarkaId);

            await modelsRepository.InsertAsync(model);
            await unitOfWork.SaveChangesAsync();

            return Result.Success();

        }
    }
}
