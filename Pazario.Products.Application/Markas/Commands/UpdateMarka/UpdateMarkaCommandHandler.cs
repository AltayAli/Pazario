using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Markas;
using Pazario.Products.Domain.Markas.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Markas.Commands.UpdateMarka
{
    public class UpdateMarkaCommandHandler
        (IMarkasRepository markasRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateMarkaCommand>
    {
        public async Task<Result> Handle(UpdateMarkaCommand request, CancellationToken cancellationToken)
        {
            var marka = await markasRepository.SelectSimpleOrDefault(new FilteringOptions<Marka>
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

            string normalizedName = request.Name.Trim().ToLower();

            bool markaExists = markasRepository.SelectSimpleOrDefault(new FilteringOptions<Marka>
            {
                Predicates = new List<System.Linq.Expressions.Expression<Func<Marka, bool>>> {
                    m => m.Name.Value.ToLower() == normalizedName
                }
            }, cancellationToken) is not null;

            if (markaExists)
            {
                return Result.Failure(MarkaErrors.AlreadyExists);
            }

            marka.Name = (Name)request.Name;

            await markasRepository.Update(marka);
            marka.AddDomainEvent(new UpdateMarkaEvent(marka.Id));
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
