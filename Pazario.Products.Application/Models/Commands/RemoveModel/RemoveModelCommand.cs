using Pazario.Common.Application.Abstractions.Messaging;


namespace Pazario.Products.Application.Models.Commands.RemoveModel
{
    public record RemoveModelCommand : ICommand
    {
        public Guid Id { get; init; }
    }
}
