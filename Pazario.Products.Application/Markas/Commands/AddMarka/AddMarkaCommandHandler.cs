using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Markas;
using Pazario.Products.Domain.Markas.Events;

namespace Pazario.Products.Application.Markas.Commands.AddMarka
{
    public class AddMarkaCommandHandler(
        IUnitOfWork unitOfWork,
        ICachedMarkasRepository markasRepository)    
        : ICommandHandler<AddMarkaCommand>
    {
        public async Task<Result> Handle(AddMarkaCommand request, CancellationToken cancellationToken)
        {
            string normalizedName = request.Name.Trim().ToLower();
            bool markaExists = markasRepository.SelectSimpleOrDefault(new FilteringOptions<Marka>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Marka, bool>>> {
                    m => m.Name.Value.ToLower() == normalizedName
                }
            }, cancellationToken) is not null;

            if (markaExists) { 
                return Result.Failure(MarkaErrors.AlreadyExists);
            }

            var marka = new Marka
            {
                Name = (Name)request.Name,
            };

            await markasRepository.Insert(marka, cancellationToken);
            marka.AddDomainEvent(new AddMarkaEvent(marka));
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
