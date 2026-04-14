using Pazario.Common.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Models.Commands.AddModel
{
    public record AddModelCommand : ICommand
    {
        public required string Name { get; init; }
        public required Guid MarkaId { get; init; }
    }
}
