using Pazario.Common.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Markas.Commands.RemoveMarka
{
    public record RemoveMarkaCommand  : ICommand
    {
        public Guid Id { get; set; }
    }
}
