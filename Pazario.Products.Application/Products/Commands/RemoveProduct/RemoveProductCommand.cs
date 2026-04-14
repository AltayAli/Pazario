using Pazario.Common.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Products.Commands.RemoveProduct
{
    public record RemoveProductCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
