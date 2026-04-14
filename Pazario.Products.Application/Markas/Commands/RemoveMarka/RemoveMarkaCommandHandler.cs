using Pazario.Common.Application.Abstractions.Messaging;
using Pazario.Common.Domain.Abstractions;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Markas;

namespace Pazario.Products.Application.Markas.Commands.RemoveMarka
{
    public class RemoveMarkaCommandHandler 
        (IMarkasRepository markasRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RemoveMarkaCommand>
    {
        public async Task<Result> Handle(RemoveMarkaCommand request, CancellationToken cancellationToken)
        {
            var marka = await markasRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Marka>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Marka, bool>>> {
                    m => m.Id == request.Id
                },
                IsLoadingAsNoTracking = false
            }, cancellationToken);

            if (marka == null)
            { 
                return Result.Failure(MarkaErrors.NotFound);
            }

            marka.Remove();
            await markasRepository.DeleteAsync(marka, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);


            return Result.Success();
        }
    }
}
