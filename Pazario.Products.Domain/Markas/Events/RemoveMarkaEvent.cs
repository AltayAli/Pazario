using Pazario.Common.Domain.Abstractions;

namespace Pazario.Products.Domain.Markas.Events
{
    public record RemoveMarkaEvent(Guid Id) : IDomainEvent;
}
