using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Markas;

namespace Pazario.Products.Application.Markas.Commands.UpdateMarka
{
    public class UpdateMarkaCommandHandler
        (IMarkasRepository markasRepository,
        IMarkaExistenceChecker markaExistenceChecker,
        IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateMarkaCommand>
    {
        public async Task<Result> Handle(UpdateMarkaCommand request, CancellationToken cancellationToken)
        {
            var marka = await markasRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Marka>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Marka, bool>>> {
                    m => m.Id == request.Id
                },
                IsLoadingAsNoTracking = false
            }, cancellationToken);

            if (marka is null)
            {
                return Result.Failure(MarkaErrors.NotFound);
            }

            bool markaExistsViaChecker = await markaExistenceChecker.ExistsAsync(request.Name, cancellationToken);

            if (markaExistsViaChecker)
            {
                return Result.Failure(MarkaErrors.AlreadyExists);
            }

            marka.Update(request.Name);

            await markasRepository.UpdateAsync(marka);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
