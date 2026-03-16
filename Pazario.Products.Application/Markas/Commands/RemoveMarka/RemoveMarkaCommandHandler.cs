using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Markas;
using Pazario.Products.Domain.Markas.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Markas.Commands.RemoveMarka
{
    public class RemoveMarkaCommandHandler 
        (ICachedMarkasRepository markasRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RemoveMarkaCommand>
    {
        public async Task<Result> Handle(RemoveMarkaCommand request, CancellationToken cancellationToken)
        {
            var marka = await markasRepository.SelectSimpleOrDefault(new FilteringOptions<Marka>
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

            await markasRepository.Delete(marka, cancellationToken);
            marka.AddDomainEvent(new RemoveMarkaEvent(marka.Id));
            await unitOfWork.SaveChangesAsync(cancellationToken);


            return Result.Success();
        }
    }
}
