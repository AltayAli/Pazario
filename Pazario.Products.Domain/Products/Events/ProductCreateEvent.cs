using Pazario.Common.Domain.Abstractions;

namespace Pazario.Products.Domain.Products.Events
{
    public record ProductCreateEvent(Guid Id) : IDomainEvent
    {
    }
}
