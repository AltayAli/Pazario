using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Markas;

namespace Pazario.Products.Application.Markas.Commands.AddMarka
{
    public class AddMarkaCommandHandler(
        IUnitOfWork unitOfWork,
        IMarkasRepository markasRepository,
        IMarkaExistenceChecker markaExistenceChecker)    
        : ICommandHandler<AddMarkaCommand>
    {
        public async Task<Result> Handle(AddMarkaCommand request, CancellationToken cancellationToken)
        {
            bool markaExistsViaChecker = await markaExistenceChecker.ExistsAsync(request.Name, cancellationToken);

            if (markaExistsViaChecker) { 
                return Result.Failure(MarkaErrors.AlreadyExists);
            }

            var marka = Marka.Create(request.Name);

            await markasRepository.InsertAsync(marka, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
