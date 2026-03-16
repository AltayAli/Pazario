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
        IUnitOfWork unitOfWork)
        : ICommandHandler<AddModelCommand>
    {
        public async Task<Result> Handle(AddModelCommand request, CancellationToken cancellationToken)
        {
            bool markaExists = markasRepository.SelectSimpleOrDefault(new FilteringOptions<Marka>
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

            var model = new Model
            {
                Name = (Name)request.Name,
                MarkaId = request.MarkaId
            };

            await modelsRepository.Insert(model);
            model.AddDomainEvent(new AddModelEvent());
            await unitOfWork.SaveChangesAsync();

            return Result.Success();

        }
    }
}
