using Pazario.Common.Domain.Abstractions;

namespace Pazario.Products.Domain.Products.Events
{
    public record ProductDeleteEvent(Guid id) : IDomainEvent
    {
    }
}
