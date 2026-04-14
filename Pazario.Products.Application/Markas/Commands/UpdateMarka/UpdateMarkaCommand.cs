using Pazario.Common.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Markas.Commands.UpdateMarka
{
    public record UpdateMarkaCommand : ICommand
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
    }
}
