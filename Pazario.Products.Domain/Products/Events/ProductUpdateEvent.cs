using Pazario.Products.Domain.Abstractions;

namespace Pazario.Products.Domain.Products.Events
{
    public record ProductUpdateEvent(Guid Id) : IDomainEvent
    {
    }
}
