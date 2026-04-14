using Pazario.Common.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Markas.Commands.AddMarka
{
    public record AddMarkaCommand : ICommand
    {
        public required string Name { get; init; }
    }
}
