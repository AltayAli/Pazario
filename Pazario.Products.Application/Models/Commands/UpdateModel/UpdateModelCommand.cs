using Pazario.Common.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Models.Commands.UpdateModel
{
    public record UpdateModelCommand : ICommand
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required Guid MarkaId { get; init; }
    }
}
